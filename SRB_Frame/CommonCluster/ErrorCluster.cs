using System;
namespace SRB.Frame.Cluster
{
    public class ErrorCluster : ICluster
    {
        public const byte Cluster_ID = 2;
        public string error_text { get => bank.getBankString(4, 24); }
        public int file { get => bank.getBankUshort(0); }
        public int line { get => bank.getBankUshort(2); }
        public byte[] parameter { get => getParameter(); }

        public ErrorCluster(BaseNode n, byte ID = Cluster_ID)
            : base(n, ID, 28) { }
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
            int i = 0, j = 0;
            for (; i < srt_len; i++)
            {
                if (bank[i + diff] == 0)
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
            return new ErrorCC(this);
        }
        public override string ToString()
        {
            return "Error Cluster";
        }
    }
}
