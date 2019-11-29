namespace SRB.Frame.Cluster
{
    internal partial class ErrorCC : IClusterControl
    {
        private ErrorCluster cluster;
        public ErrorCC(ErrorCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.read();
        }

        protected override void DataUpdata()
        {
            this.errorTextL.Text = cluster.error_text + cluster.parameter.ToArrayString();
            this.pageLineL.Text = string.Format("Page{0}.Lines{1}", cluster.file, cluster.line);
        }
    }
}
