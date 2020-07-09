using System;

namespace SRB.Frame
{
    internal partial class MappingCC : IClusterControl
    {
        private MappingCluster cluster;
        public MappingCC(MappingCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.readAll();
        }

        protected override void DataUpdata()
        {
            UpRTC.Text = cluster.mapping.ToArrayString();
        }

        protected override void WriteData()
        {
            string error;
            byte[] up = UpRTC.Text.ToByteAsCArroy(out error);
            if (up != null)
            {
                if (cluster.setMapping(up))
                {
                    cluster.write();
                }
            }
        }

        private void UpRTC_TextChanged(object sender, EventArgs e)
        {
            string error;
            byte[] up = UpRTC.Text.ToByteAsCArroy(out error);
            if (up != null)
            {
                StatusLAB.Text = cluster.checkMapping(up);
            }
            else
            {
                StatusLAB.Text = error;
            }
        }
    }
}
