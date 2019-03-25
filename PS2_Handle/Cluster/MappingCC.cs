using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRB.Frame;

namespace SRB.NodeType.PS2_Handle
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
            UpRTC.Text = cluster.up_mapping.ToArrayString();
            DownRTC.Text = cluster.down_mapping.ToArrayString();
        }

        protected override void WriteData()
        {
            byte[] up = UpRTC.Text.ToByteAsCArroy();
            byte[] down = DownRTC.Text.ToByteAsCArroy();

            cluster.setMapping(up, down);
            cluster.write();
        }
    }
}
