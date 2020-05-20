using System;


namespace SRB.Frame
{
    public partial class BaseNode
    {
        public class DebugInfoCluster : BaseNode.ICluster
        {
            public const byte FIX_CID = 6;
            public byte[] Bank { get => bank.Byte_array; }
            BaseNode node;
            public DebugInfoCluster(BaseNode n)
                : base(n, FIX_CID, 16)
            {
                is_have_control = false;
                node = n;
            }
            public override void write()
            {
                throw new Exception("read only cluster can not write.");
            }
            public override void writeRecv(Access ac)
            {
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
                return "Debug Cluster";
            }

        }
    }
}