using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SRB_CTR.nsFrame;

namespace SRB_CTR.nsBrain.Cluster_led_phase
{
    partial class Ctrl : UserControl
    {
        Clu cluster;
        public Ctrl(Clu c)
        {
            InitializeComponent();
            cluster = c;
            c.eDataChanged += new EventHandler(c_dataChanged);
            c_dataChanged(this, null);
            this.CycleNUM.Value = new decimal(cluster.cycle_sec);
            this.fadeNUM.Value = new decimal(cluster.fase_sec);
        }

        void c_dataChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler d = new EventHandler(c_dataChanged);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.cycleL.Text = cluster.cycle_sec.ToString("f3");
                this.fadeL.Text = cluster.fase_sec.ToString("f3");
            }
        }

        private void write(object sender, EventArgs e)
        {
            cluster.cycle_sec = (double)CycleNUM.Value;
            cluster.fase_sec = (double)fadeNUM.Value;
            cluster.write();
        }

        private void read(object sender, EventArgs e)
        {
            cluster.read();
        }
    }
}
