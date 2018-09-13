using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace SRB_CTR.nsFrame.Cluster_base
{
    class Clu : Cluster
    {
        public byte addr;
        public byte new_addr;
        public string name;
        
        public override void write()
        {
            Access ac;
            byte[] b = new byte[20];
            int i = 0;
            b[i++] = clustr_ID;
            b[i++] = new_addr;
            foreach (char c in name.ToCharArray())
            {
                b[i++] = (byte)c;
            }
            b[19] = 100;
            ac = new Access(this.parent_node, Access.ePort.Cgf,b);
            parent_node.postAccess(ac);
        }
        public override void writeRecv(Access ac)
        {
            if (ac.Recv_error == false)
            {
                if (addr != ac.Send_data[1])
                {
                    addr = ac.Send_data[1];
                    parent_node.onAddrChanged();
                }
                OnDataChangded();
            }
        }
        public override void readRecv(Access ac)
        {
            char[] cs = new char[17];
            int counter=0;
            int read_addr = ac.Recv_data[counter++];
            if(addr != read_addr)
            {
                throw new Exception(string.Format("read addr for Node{0} is {1}!", addr, read_addr));
            }
            int i;
            for(i=0;i<16;i++)
            {
                cs[i] = (char)ac.Recv_data[counter++];
                if(cs[i] == 0)
                {
                    break;
                }
            }
            cs[16] = '\0';
            this.name = new string(cs,0,i);
            base.readRecv(ac);

        }
        public Clu(byte ID,Node n,byte addr)
            : base(ID,n)
        {
            this.addr = addr;
        }

        public override UserControl createControl()
        {
            return new Ctrl(this);
        }
        public override string ToString()
        {
            return string.Format("Base Cluster", clustr_ID.ToHexSt());
        }

        public bool isNewAddrAvaliable(byte addr)
        {
            return  parent_node.isNewAddrAvaliable(addr);
        }

    }
}
