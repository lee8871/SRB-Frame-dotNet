using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.nsBrain.Cluster_info
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
                this.typeL.Text = "Type: " + cluster.type;
                this.versionL.Text = 
                   string.Format("Version: {0}.{1}", cluster.major_version, cluster.minor_version);
            }
        }

        private void write(object sender, EventArgs e)
        {
            cluster.write();
        }

        private void read(object sender, EventArgs e)
        {
            cluster.read();
        }
    }
}
