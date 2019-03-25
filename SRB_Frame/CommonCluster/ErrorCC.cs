using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SRB.Frame.Cluster
{
    partial class ErrorCC : IClusterControl
    {
        ErrorCluster cluster;
        public ErrorCC(ErrorCluster c):base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.read();
        }

        protected override void DataUpdata()
        {
            this.errorTextL.Text = cluster.error_text + cluster.parameter.ToArrayString();
            this.pageLineL.Text = string.Format("Page{0}.Lines{1}",cluster.file,cluster.line);
        }
    }
}
