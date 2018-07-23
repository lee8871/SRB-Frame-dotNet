using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRB_CTR.nsBrain.Cluster_error
{
    class Clu:Cluster
    {
        public string error_text;
        public int file;
        public int line;

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
            file = ac.Recv_data.GetUint16(counter);
            counter += 2;
            line = ac.Recv_data.GetUint16(counter);
            counter += 2;

            char[] cs = new char[25];
            for (i = 0; i < 24; i++)
            {
                cs[i] = (char)ac.Recv_data[counter++];
                if (cs[i] == 0)
                {
                    break;
                }
            }
            cs[24] = '\0';
            this.error_text = new string(cs, 0, i);
            base.readRecv(ac);
            return;
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new Ctrl(this);
        }
        public override string ToString()
        {
            return "Error Cluster";
        }
    }
}
