using System;


namespace SRB.Frame
{
    public partial class Node
    {
        public class InformationCluster : Node.ICluster
        {
            public string type { get => bank.getBankString(8, 17); }

            private Version app_version=new Version("App");
            public Version App_version { 
                get
                {
                    app_version.read(bank.Byte_array, 0);
                    return app_version;
                } 
            }
            private Version srb_version = new Version("Srb");
            public Version Srb_version
            {
                get
                {
                    srb_version.read(bank.Byte_array, 3);
                    return srb_version;
                }
            }
            Node node;

            public TimeStampCluster timestampClu;
            public InformationCluster(Node n)
                : base(n, 1, 23)
            {
                timestampClu = new TimeStampCluster(n);
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



        public class TimeStampCluster : Node.ICluster
        {
            public const byte FIX_CID = 7;
            public int utc { get => (int)bank.getBankUint(0); }
           
            public TimeStampCluster(Node n)
                : base(n, FIX_CID, 4)
            {
                is_have_control = false;
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
                throw new Exception("This node do not has a control.");
            }

            public override void readRecv(Access ac)
            {
                base.readRecv(ac);
            }
            public override string ToString()
            {
                return "Time Stamp Cluster";
            }

        }
    }
}