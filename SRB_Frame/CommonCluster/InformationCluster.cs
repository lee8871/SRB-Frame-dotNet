using System;


namespace SRB.Frame
{
    public partial class BaseNode
    {
        public class InformationCluster : BaseNode.ICluster
        {
            public string type { get => bank.getBankString(8, 17); }
            public int major_version { get => bank.getBankByte(0); }
            public int minor_version { get => bank.getBankByte(1); }
            public int SRB_major_version { get => bank.getBankByte(2); }
            public int SRB_minor_version { get => bank.getBankByte(3); }
            public uint time_stamp { get => bank.getBankUint(4); }
            BaseNode node;
            public InformationCluster(BaseNode n)
                : base(n, 1, 23)
            {
                node = n;
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
                if (ac.Send_data.Length == 2)
                {
                    return;
                }
                throw new Exception("read only cluster can not write.");
            }
            protected override System.Windows.Forms.Control createControl()
            {
                return new InformationCC(this);
            }

            public override void readRecv(Access ac)
            {
                base.readRecv(ac);
                parent_node.onDescriptionChanged();
            }
            public override string ToString()
            {
                return "Information Cluster";
            }

            public void resetNode()
            {
                Access ac;
                byte[] b = new byte[2];
                int i = 0;
                b[i++] = cID;
                b[i++] = (byte)'R';
                ac = new Access(this, this.parent_node, Access.PortEnum.Cgf, b);
                Bus.singleAccess(ac);
            }
            public void factorySettingNode()
            {
                Access ac;
                byte[] b = new byte[2];
                int i = 0;
                b[i++] = cID;
                b[i++] = (byte)'F';
                ac = new Access(this, this.parent_node, Access.PortEnum.Cgf, b);
                Bus.singleAccess(ac);
                parent_node.onDescriptionChanged();
            }


            public void gotoUpdateMode()
            {
                node.gotoUpdateMode();
            }

        }
    }
}