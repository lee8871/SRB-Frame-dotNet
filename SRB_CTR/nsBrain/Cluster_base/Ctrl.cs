using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.nsBrain.Cluster_base
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
            this.AddrTB.Text = cluster.addr.ToHexSt();
            this.NodeNameTB.Text = cluster.name;
        }

        void  c_dataChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler d = new EventHandler(c_dataChanged);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.AddrL.Text = cluster.addr.ToHexSt();
                this.NodeNameL.Text = cluster.name;
            }
        }

        private void writeBTN_Click(object sender, EventArgs e)
        {
            if(NodeNameTB.Text!="")
            {
                cluster.name = NodeNameTB.Text;
            }
            if (AddrTB.Text != "")
            {
                cluster.new_addr = AddrTB.byte_value;
            }
            cluster.write();
        }

        private void readBTN_Click(object sender, EventArgs e)
        {

            cluster.read();
        }
    }
}
