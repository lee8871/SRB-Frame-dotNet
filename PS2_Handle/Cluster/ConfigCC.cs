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
            OnlineNUM.Value = cluster.online_rumble_10ms * 10;
            loseNUM.Value = cluster.lose_rumble_10ms * 10;
            StrengthNUM.Value = cluster.Strength;
        }

        protected override void WriteData()
        {
            cluster.writeBankinit();
            cluster.online_rumble_10ms = (int)(OnlineNUM.Value / 10);
            cluster.lose_rumble_10ms = (int)(loseNUM.Value / 10);
            cluster.Strength = (int)StrengthNUM.Value;
            cluster.write();
        }
    }
}
