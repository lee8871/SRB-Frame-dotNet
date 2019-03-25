using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace SRB.Frame.Cluster
{
    public class AddressCluster : ICluster
    {
        public const byte Cluster_ID = 0;


        public byte addr { get => bank[0]; set => bank_write[0] = value; }
        public string name { get => getBankString(1, 17); set => setBankString(value, 1, 17); }
        public byte error_behavior { get => getBankByte(18); set => setBankByte(value,18); }

        public override void writeRecv(Access ac)
        {
            if (ac.Recv_error == false)
            {
                if(ac.Send_data.Length==2)
                {
                    return;
                }
                if (addr != ac.Send_data[1])
                {
                    base.writeRecv(ac);
                    parent_node.onAddrChanged();
                }
                else {
                    base.writeRecv(ac);
                }
            }
        }
        public AddressCluster(Node n, byte address, byte cID = Cluster_ID)
            : base(n, cID, 19)
        {
            bank[0]  = address;//此处为特殊操作,This Cluster must init address before used.
        }

        public override UserControl createControl()
        {
            return new AddressCC(this);
        }
        public override string ToString()
        {
            return string.Format("Address Cluster", Clustr_ID.ToHexSt());
        }

        public bool isNewAddrAvaliable(byte addr)
        {
            return  parent_node.isNewAddrAvaliable(addr);
        }
        public enum LedAddrType{High,Low,Close};
        public void ledAddr(LedAddrType adt)
        {
            Access ac;
            byte[] b = new byte[2];
            int i = 0;
            b[i++] = Clustr_ID;
            switch(adt)
            {
                case LedAddrType.Close:
                    b[i++] = 0xf5;break;
                case LedAddrType.High:
                    b[i++] = 0xf4; break;
                case LedAddrType.Low:
                    b[i++] = 0xf3; break;

            }
            ac = new Access(this.parent_node, Access.PortEnum.Cgf, b);
            parent_node.singleAccess(ac);
        }
        public static void ledAddrBroadcast(LedAddrType adt, ISRB_Master parent)
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
            ac = new Access(null, Access.PortEnum.Cgf, b);
            parent.singleAccess(ac);

        }
    }
}
