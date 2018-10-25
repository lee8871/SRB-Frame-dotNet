using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SRB_CTR.SRB_Frame.Cluster_info
{
    class Clu:Cluster
    {
        public string type ="Unknow";
        public int major_version;
        public int minor_version;
        public int time_stamp;

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
            time_stamp = support.byteToUint16(ac.Recv_data[counter++], ac.Recv_data[counter++]);
            char[] cs = new char[17];
            for (i = 0; i < 16; i++)
            {
                cs[i] = (char)ac.Recv_data[counter++];
                if (cs[i] == 0)
                {
                    break;
                }
            }            
            cs[16] = '\0';
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

        public void resetNode()
        {
            Access ac;
            byte[] b = new byte[3];
            int i = 0;
            b[i++] = clustr_ID;
            b[i++] = (byte)'R';
            b[i++] = (byte)'N';
            ac = new Access(this.parent_node, Access.PortEnum.Cgf, b);
            parent_node.singleAccess(ac);
        }
        public void factorySettingNode()
        {
            Access ac;
            byte[] b = new byte[3];
            int i = 0;
            b[i++] = clustr_ID;
            b[i++] = (byte)'F';
            b[i++] = (byte)'S';
            ac = new Access(this.parent_node, Access.PortEnum.Cgf, b);
            parent_node.singleAccess(ac);
        }
    }
}
