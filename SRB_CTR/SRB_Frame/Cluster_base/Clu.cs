using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace SRB_CTR.SRB_Frame.Cluster_base
{
    public class Clu : Cluster
    {
        public byte error_behave;

        public byte addr { get => bank[0]; set => bank[0] = value; }
        public string name { get => getBankString(1, 17); set => setBankString(value, 1, 17); }
        public byte error_behavior { get => getBankByte(18); set => setBankByte(value,18); }
        public byte addr_new { get => bank_write[0]; set => bank_write[0] = value; }

        //public override void write()
        //{
        //    Access ac;
        //    byte[] b = new byte[20];
        //    int i = 0;
        //    b[i++] = clustr_ID;
        //    b[i++] = new_addr;
        //    foreach (char c in name.ToCharArray())
        //    {
        //        b[i++] = (byte)c;
        //    }
        //    b[19] = 1;
        //    ac = new Access(this.parent_node, Access.PortEnum.Cgf,b);
        //    parent_node.singleAccess(ac);
        //}
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
        //public override void readRecv(Access ac)
        //{
        //    if(ac.Recv_data_len==0)
        //    {
        //        base.readRecv(ac);
        //        return;

        //    }
        //    char[] cs = new char[17];
        //    int counter=0;
        //    int read_addr = ac.Recv_data[counter++];
        //    if(addr != read_addr)
        //    {
        //        throw new Exception(string.Format("read addr for Node{0} is {1}!", addr, read_addr));
        //    }
        //    int i;
        //    for(i=0;i<16;i++)
        //    {
        //        cs[i] = (char)ac.Recv_data[counter++];
        //        if(cs[i] == 0)
        //        {
        //            break;
        //        }
        //    }
        //    cs[16] = '\0';
        //    this.name = new string(cs,0,i);
        //    this.error_behave = ac.Recv_data[18];
        //    base.readRecv(ac);

        //}
        public Clu(byte ID,Node n,byte addr)
            : base(ID,n,19)
        {
            bank[0]  = addr;
        }

        public override UserControl createControl()
        {
            return new Ctrl(this);
        }
        public override string ToString()
        {
            return string.Format("Base Cluster", Clustr_ID.ToHexSt());
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
    }
}
