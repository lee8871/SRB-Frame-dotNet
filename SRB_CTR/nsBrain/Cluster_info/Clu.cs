using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRB_CTR.nsBrain.Cluster_info
{
    class Clu:Cluster
    {
        public string type;
        public int major_version;
        public int minor_version;

        public Clu(byte ID, Node n)
            : base(ID, n) { }
        public override void write()
        {
            throw new Exception("read only cluster can not write.");
        }
        public override void readRecv(Access ac)
        {
            int counter = 0;
            int i = 0;
            major_version = ac.Recv_data[counter++];
            minor_version = ac.Recv_data[counter++];
            char[] cs = new char[9];
            for (i = 0; i < 8; i++)
            {
                cs[i] = (char)ac.Recv_data[counter++];
                if (cs[i] == 0)
                {
                    break;
                }
            }            cs[8] = '\0';
            this.type = new string(cs, 0, i);
            base.readRecv(ac);
            return;
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new Ctrl(this);
        }
        public override string ToString()
        {
            return "Information Cluster";
        }
    }
}
