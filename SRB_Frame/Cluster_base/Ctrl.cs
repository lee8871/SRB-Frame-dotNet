using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB.Frame.Cluster_base
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
            this.AddrNUM.Value = cluster.addr;
            this.NodeNameTB.Text = cluster.name;
        }
        Color[] num_to_color = {
            Color.White,
            Color.Pink,
            Color.FromArgb(255,126,126),
            Color.Orange,
            Color.Yellow,
            Color.GreenYellow,
            Color.SpringGreen,
            Color.Cyan,
            Color.DeepSkyBlue,
            Color.FromArgb(180,140,255),
        };

        void  c_dataChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler d = new EventHandler(c_dataChanged);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.AddrL.Text = cluster.addr.ToString();
                this.NodeNameL.Text = cluster.name;
                int addr_color = ((int)cluster.addr).enterRound(0, 99);
                this.highBTN.BackColor = num_to_color[cluster.addr / 10];
                this.lowBTN.BackColor = num_to_color[cluster.addr % 10];
            }
        }

        private void writeBTN_Click(object sender, EventArgs e)
        {
            cluster.writeBankinit();
            if (NodeNameTB.Text!="")
            {
                cluster.name = NodeNameTB.Text;
            }
            byte new_addr = (byte)((int)AddrNUM.Value);
            if (new_addr == cluster.addr)
            {
                cluster.write();
            }
            else
            {
                if (cluster.isNewAddrAvaliable(new_addr))
                {
                    cluster.addr_new = new_addr;
                    cluster.write();
                }
                else
                {
                    DialogResult res;
                    string st = string.Format("There is anther node which use the address({0}), click OK to contiual.", new_addr);
                    res = MessageBox.Show(st, "New Address Exist", MessageBoxButtons.OKCancel);
                    if (res == DialogResult.OK)
                    {
                        cluster.addr_new = new_addr;
                        cluster.write();
                    }
                }
            }
        }

        private void readBTN_Click(object sender, EventArgs e)
        {
            cluster.read();
        }

        private void highBTN_Click(object sender, EventArgs e)
        {
            this.cluster.ledAddr(Clu.LedAddrType.High);
        }

        private void lowBTN_Click(object sender, EventArgs e)
        {
            this.cluster.ledAddr(Clu.LedAddrType.Low);
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.cluster.ledAddr(Clu.LedAddrType.Close);
        }
    }
}
