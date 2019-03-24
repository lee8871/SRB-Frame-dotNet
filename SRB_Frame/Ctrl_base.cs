using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SRB.Frame
{
    public partial class Ctrl_base : UserControl
    {
        ICluster cluster;
        public Ctrl_base(ICluster c)
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
