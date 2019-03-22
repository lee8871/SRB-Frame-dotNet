using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SRB_CTR.SRB_Frame.Cluster_error
{
    public  class Clu:Cluster
    {
        public string error_text { get => getBankString(4,24); } 

        public int file { get => getBankUshort(0); }
        public int line { get => getBankUshort(2); }

        public byte[] parameter { get => getParameter(); }

        public Clu(byte ID, Node n)
            : base(ID, n,28) { }
        public override void write()
        {
            throw new Exception("read only cluster can not write.");
        }
        public override void writeRecv(Access ac)
        {
            throw new Exception("read only cluster can not write.");
        }
        public byte[] getParameter()
        {
            int diff = 4;
            int srt_len = 24;
            int i = 0,j = 0;
            for (; i < srt_len; i++)
            {
                if(bank[i + diff] ==0)
                {
                    i++;
                    break;
                }
            }
            byte[] rev = new byte[srt_len - i];
            for (; i < srt_len; i++)
            {
                rev[j++] = bank[i + diff];
            }
            return rev;
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
