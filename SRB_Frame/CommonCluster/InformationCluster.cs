using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SRB.Frame.Cluster
{
    public class InformationCluster:ICluster
    {
        public string type  { get => getBankString(6,17);   }
        public int major_version { get => getBankByte(0); }
        public int minor_version { get => getBankByte(1); }
        public int SRB_major_version { get => getBankByte(2); }
        public int SRB_minor_version { get => getBankByte(3); }
        public int time_stamp { get => getBankUshort(4); }

        public InformationCluster(byte ID, Node n)
            : base(ID, n,23)
        {
            char[] ca = "Unknow".ToCharArray();
            int i;
            for (i = 0; i < ca.Length; i++)
            {
                bank[6 + i] = (byte)ca[i];
            }
            bank[6 + i] = (byte)'\0';
        }
        public override void write()
        {
            throw new Exception("read only cluster can not write.");
        }
        public override void writeRecv(Access ac)
        {
            if (ac.Send_data.Length == 3)
            {
                return;
            }
            throw new Exception("read only cluster can not write.");
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new InformationCC(this);
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
            b[i++] = Clustr_ID;
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
            b[i++] = Clustr_ID;
            b[i++] = (byte)'F';
            b[i++] = (byte)'S';
            ac = new Access(this.parent_node, Access.PortEnum.Cgf, b);
            parent_node.singleAccess(ac);
        }
    }
}
