using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SRB_CTR.SRB_Frame;

namespace SRB_CTR.nsBrain.Node_PS2_handle.Cluster_handle_cfg
{
    partial class Ctrl : UserControl
    {
        Clu cluster;
        public Ctrl(Clu c)
        {
            InitializeComponent();
            cluster = c;
            c.eDataChanged += new EventHandler(c_dataChanged);
            cluster.read();
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
                PeriodNum.Value = cluster.period;
                AnalogCBOX.CheckState = cluster.analog ? CheckState.Checked : CheckState.Unchecked;
                RumbleCBOX.CheckState = cluster.rumble ? CheckState.Checked : CheckState.Unchecked;


            }
        }

        private void write(object sender, EventArgs e)
        {
            cluster.writeBankinit();
            cluster.period = (int)PeriodNum.Value;
            cluster.analog = AnalogCBOX.Checked;
            cluster.rumble = RumbleCBOX.Checked;
            cluster.write();
        }
        private void read(object sender, EventArgs e)
        {
            cluster.read();
        }

    }
}
