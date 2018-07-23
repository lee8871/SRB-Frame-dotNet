using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.nsBrain.Cluster_base
{
    class Clu:Cluster
    {
        public byte addr;
        public byte new_addr;
        public string name;
        
        public override void write()
        {
            Access ac;
            byte[] b = new byte[18];
            int i = 0;
            b[i++] = clustr_ID;
            b[i++] = new_addr;
            foreach (char c in name.ToCharArray())
            {
                b[i++] = (byte)c;
            }
            ac = new Access(addr, Access.ePort.Cgf,b);
            parent_node.access_queue.Enqueue(ac);
        }
        public override void writeRecv(Access ac)
        {
            if (ac.Recv_error == false)
            {
                addr = ac.Send_data[1];
                OnDataChangded();
            }
        }
        public override void readRecv(Access ac)
        {
            char[] cs = new char[17];
            int counter=0;
            this.addr = ac.Recv_data[counter++];
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
            return string.Format("Base Cluster<ID={0}>", clustr_ID.ToHexSt());
        }
    }
}
