using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SRB.Frame.Cluster_error
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
                this.errorTextL.Text = cluster.error_text + cluster.parameter.ToArrayString();
                this.pageLineL.Text = string.Format("Page{0}.Lines{1}",cluster.file,cluster.line);
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

        private void Ctrl_Load(object sender, EventArgs e)
        {

        }
    }
}
