﻿namespace SRB.Frame
{
    internal partial class ErrorCC : IClusterControl
    {
        private ErrorCluster cluster;
        public ErrorCC(ErrorCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.readAll();
        }

        protected override void DataUpdata()
        {
            this.errorTextL.Text = cluster.error_text + cluster.parameter.ToArrayString();
            this.pageLineL.Text = string.Format("err{0}:", cluster.err_num);        }
    }
}
