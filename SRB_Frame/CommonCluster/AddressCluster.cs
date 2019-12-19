using System;
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
        }
    }
}
