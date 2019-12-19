namespace SRB.Frame.ud
{
    internal partial class MappingCC : IClusterControl
    {
        private MappingCluster cluster;
        public MappingCC(MappingCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.read();
        }

        protected override void DataUpdata()
        {
            UpRTC.Text = cluster.up_mapping.ToArrayString();
            DownRTC.Text = cluster.down_mapping.ToArrayString();
        }

        protected override void WriteData()
        {
            string error;
            byte[] up = UpRTC.Text.ToByteAsCArroy(out error);
            byte[] down = DownRTC.Text.ToByteAsCArroy(out error);

            cluster.setMapping(up, down);
            cluster.write();
        }
    }
}
