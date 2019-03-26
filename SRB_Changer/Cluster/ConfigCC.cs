using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SRB.Frame;

namespace SRB.NodeType.Charger
{
    partial class ConfigCC : IClusterControl
    {
        ConfigCluster cluster;
        public ConfigCC(ConfigCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.read();
        }


        protected override void DataUpdata()
        {
            PeriodNum.Value = cluster.period;
            AnalogCBOX.CheckState = cluster.analog ? CheckState.Checked : CheckState.Unchecked;
            RumbleCBOX.CheckState = cluster.rumble ? CheckState.Checked : CheckState.Unchecked;
        }

        protected override void WriteData()
        {
            cluster.writeBankinit();
            cluster.period = (int)PeriodNum.Value;
            cluster.analog = AnalogCBOX.Checked;
            cluster.rumble = RumbleCBOX.Checked;
            cluster.write();
        }

    }
}
