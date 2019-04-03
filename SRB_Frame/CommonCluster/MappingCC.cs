using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRB.Frame;

namespace SRB.Frame.Cluster
{
    partial class MappingCC  : IClusterControl
    {
        MappingCluster cluster;
        public MappingCC(MappingCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.read();
        }

        protected override void DataUpdata()
        {
            UpRTC.Text = cluster.mapping.ToArrayString();
        }

        protected override void WriteData()
        {
            string error;
            byte[] up = UpRTC.Text.ToByteAsCArroy(out error);
            if (up != null) { 
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
            if(up!=null)
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
