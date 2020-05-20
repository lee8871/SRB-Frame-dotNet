using System;
using System.Threading;
using System.Windows.Forms;
namespace SRB.Frame
{
    public partial class BaseNode
    {
        public class AddressCluster : BaseNode.ICluster
        {
            public byte addr { get => bank[0]; set => bank.temp[0] = value; }
            public string name { get => bank.getBankString(1, 27); set => bank.setBankString(value, 1, 27); }
            public byte error_behavior { get => bank.getBankByte(28); set => bank.setBankByte(value, 28); }
            public AddressCluster(BaseNode n)
                : base(n, 0, 29)
            {
            }

            public override void writeRecv(Access ac)
            {
                if (ac.Recv_error == false)
                {
                    if (ac.Send_data.Length == 2)
                    {
                        if (ac.Send_data[1] < 100)
                        {
                            if (addr != ac.Send_data[1])
                            {
                                bank[0] = ac.Send_data[1];
                                parent_node.onAddressChanged(ac.Send_data[1]);
                            }
                        }
                    }
                    else
                    {
                        if (addr != ac.Send_data[1])
                        {
                            parent_node.onAddressChanged(ac.Send_data[1]);
                        }
                        base.writeRecv(ac);

                    }
                    parent_node.onDescriptionChanged();
                }
            }

            public override void readRecv(Access ac)
            {
                base.readRecv(ac);
                parent_node.onDescriptionChanged();
            }

            protected override Control createControl()
            {
                return new AddressCC(this);
            }
            public override string ToString()
            {
                return string.Format("Address Cluster", CID.ToHexSt());
            }

            public bool isNewAddrAvaliable(byte addr)
            {//最终需要父亲节点开放Bus的访问给cluster
                return Bus[addr] == null;
            }
            public enum LedAddrType { High, Low, Close };
            public void ledAddr(LedAddrType adt)
            {
                Access ac;
                byte[] b = new byte[2];
                int i = 0;
                b[i++] = CID;
                switch (adt)
                {
                    case LedAddrType.Close:
                        b[i++] = 0xf5; break;
                    case LedAddrType.High:
                        b[i++] = 0xf4; break;
                    case LedAddrType.Low:
                        b[i++] = 0xf3; break;

                }
                ac = new Access(this, this.parent_node, Access.PortEnum.Cgf, b);
                Bus.singleAccess(ac);
            }
            public static void ledAddrBroadcast(LedAddrType adt, IBus bus)
            {
                Access ac;
                byte[] b = new byte[2];
                int i = 0;
                b[i++] = 0;
                switch (adt)
                {
                    case LedAddrType.Close:
                        b[i++] = 0xf5; break;
                    case LedAddrType.High:
                        b[i++] = 0xf4; break;
                    case LedAddrType.Low:
                        b[i++] = 0xf3; break;

                }
                ac = new Access(null, null, Access.PortEnum.Cgf, b);
                bus.singleAccess(ac);

            }
            public void changeAddress(byte a)
            {
                if (a > 100)
                {
                    throw new Exception("Set address can not high than 100");
                }
                Access ac;
                byte[] b = new byte[2];
                int i = 0;
                b[i++] = cID;
                b[i++] = a;
                ac = new Access(this, this.parent_node, Access.PortEnum.Cgf, b);
                Bus.singleAccess(ac);
            }

            public static void randomAddrAll(IBus bus)
            {
                Access ac;
                byte[] b = new byte[2];
                int i = 0;
                b[i++] = 0;
                b[i++] = 0xfa;
                ac = new Access(null, null, Access.PortEnum.Cgf, b);
                bus.singleAccess(ac);
            }

            public static void randomAddrNewNode(IBus bus)
            {
                Access ac;
                byte[] b = new byte[2];
                int i = 0;
                b[i++] = 0;
                b[i++] = 0xf0;
                ac = new Access(null, null, Access.PortEnum.Cgf, b);
                bus.singleAccess(ac);
            }
            public class Broadcast : IDisposable
            {
                IBus bus;
                public Broadcast(IBus bus)
                {
                    this.bus = bus;
                }
                public bool Is_scan_running
                {
                    get
                    {
                        if (scan_thread != null)
                        {
                            return scan_thread.IsAlive;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                private double scan_progress = 0;
                public double Scan_progress
                {
                    get => scan_progress;
                }

                private string scan_status = "Scan is not begin";
                public string Scan_status
                {
                    get => scan_status;
                }

                public static int scan_max_addr = 227;
                private Thread scan_thread;


                private int scan_begin;
                private int scan_end;
                private bool scan_stop = true;

                public void Dispose()
                {
                    endScan();
                    while (Is_scan_running) ;
                }

                public void scanNodes(int begin = 0, int end = -1)
                {
                    if (end < 0)
                    {
                        end = scan_max_addr;
                    }
                    scan_end = end;
                    scan_begin = begin;
                    if (Is_scan_running == false)
                    {
                        scan_thread = new Thread(new ThreadStart(scanNodeLoop));
                        scan_stop = false;
                        scan_thread.Start();
                    }
                    return;
                }
                public void endScan()
                {
                    scan_stop = true;
                }
                private void scanNodeLoop()
                {
                    bus.removeAllNode();
                    BaseNode n = bus.createTempNode(0);
                    n.checkNodeAccessable();
                    bus.removeAllNode();
                    for (int scan_num = scan_begin; scan_num < scan_end; scan_num++)
                    {
                        scan_status = string.Format("Scan {0}/{1}", scan_num, scan_end);
                        scan_progress = scan_num * 1.0 / scan_max_addr;
                        n = bus.createTempNode((byte)scan_num);
                        n.checkNodeAccessable();
                        if (n.Is_hareware_exist)
                        {
                            bus.addTempNode();
                        }
                        if (scan_stop)
                        {
                            scan_status = "Scan breaked";
                            return;
                        }
                    }
                    scan_status = "Scan done";
                    return;
                }


                public void autoSetAddress()
                {
                    scan_status = "Set address";
                    byte new_addr = 10;
                    for (byte i = 100; i < 227; i++)
                    {
                        if (bus[i] != null)
                        {
                            while (bus[new_addr] != null)
                            {
                                new_addr++;
                            }
                            if (new_addr >= 100)
                            {
                                throw new Exception("Auto set addr error, Addr is high than 100");
                            }
                            bus[i].changeAddr((byte)new_addr);
                            new_addr++;
                        }
                    }
                    scan_status = "Set address done";
                    return;
                }
                public void autoSetAddressLoop()
                {
                }

                public void scanUpdateNodes(int begin = 0, int end = -1)
                {
                    bus.removeAllUpdateNode();
                    if (end < 0)
                    {
                        end = scan_max_addr;
                    }
                    scan_end = end;
                    scan_begin = begin;
                    if (Is_scan_running == false)
                    {
                        scan_thread = new Thread(new ThreadStart(scanUpdateNodeLoop));
                        scan_stop = false;
                        scan_thread.Start();
                    }
                    return;
                }

                private void scanUpdateNodeLoop()
                {
                    for (int scan_num = scan_begin; scan_num < scan_end; scan_num++)
                    {
                        scan_status = string.Format("Scan update {0}/{1}", scan_num, scan_end);
                        scan_progress = scan_num * 1.0 / scan_max_addr;
                        if (bus[scan_num] == null)
                        {
                            BaseNode n = bus.createTempNode((byte)scan_num);
                            n.checkNodeUpdateable();
                            if (n.Is_in_update)
                            {
                                bus.addTempNode();
                            }
                            if (scan_stop)
                            {
                                scan_status = "Scan update breaked";
                                return;
                            }
                        }
                    }
                    scan_status = "Scan update finish";
                }
            }
        }
    }
}
