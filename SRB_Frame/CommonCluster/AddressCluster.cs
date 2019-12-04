using System;
using System.Windows.Forms;
namespace SRB.Frame.Cluster
{
    public class AddressCluster : ICluster
    {
        public const byte Cluster_ID = 0;


        public byte addr { get => bank[0]; set => bank_write_temp[0] = value; }
        public string name { get => getBankString(1, 27); set => setBankString(value, 1, 27); }
        public byte error_behavior { get => getBankByte(28); set => setBankByte(value, 28); }

        public AddressCluster(BaseNode n, byte address, byte cID = Cluster_ID)
            : base(n, cID, 19)
        {
            bank[0] = address;//此处为特殊操作,This Cluster must init address before used.
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
                            parent_node.onAddrChanged();
                        }
                    }
                }
                else
                {
                    if (addr != ac.Send_data[1])
                    {
                        base.writeRecv(ac);
                        parent_node.onAddrChanged();
                    }
                    else
                    {
                        base.writeRecv(ac);
                    }
                }
            }
        }
        public override UserControl createControl()
        {
            return new AddressCC(this);
        }
        public override string ToString()
        {
            return string.Format("Address Cluster", CID.ToHexSt());
        }

        public bool isNewAddrAvaliable(byte addr)
        {
            return parent_node.isNewAddrAvaliable(addr);
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
            ac = new Access(this,this.parent_node, Access.PortEnum.Cgf, b);
            parent_node.singleAccess(ac);
        }
        public static void ledAddrBroadcast(LedAddrType adt, IMaster parent)
        {
            Access ac;
            byte[] b = new byte[2];
            int i = 0;
            b[i++] = Cluster_ID;
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
            parent.singleAccess(ac);

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
            b[i++] = Cluster_ID;
            b[i++] = a;
            ac = new Access(this,this.parent_node, Access.PortEnum.Cgf, b);
            parent_node.singleAccess(ac);
        }

        public static void randomAddrAll(IMaster parent)
        {
            Access ac;
            byte[] b = new byte[2];
            int i = 0;
            b[i++] = Cluster_ID;
            b[i++] = 0xfa;
            ac = new Access(null, null, Access.PortEnum.Cgf, b);
            parent.singleAccess(ac);
        }

        public static void randomAddrNewNode(IMaster parent)
        {
            Access ac;
            byte[] b = new byte[2];
            int i = 0;
            b[i++] = Cluster_ID;
            b[i++] = 0xf0;
            ac = new Access(null, null, Access.PortEnum.Cgf, b);
            parent.singleAccess(ac);
        }
    }
}
