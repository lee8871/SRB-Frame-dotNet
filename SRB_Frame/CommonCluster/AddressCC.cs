using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB.Frame.Cluster
{
    partial class AddressCC : IClusterControl
    {
        AddressCluster cluster;
        public AddressCC(AddressCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.read();
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



        protected override void DataUpdata()
        {
            if (cluster.addr <= 199)
            {
                this.AddrNUM.Value = cluster.addr;
            }
            this.AddrL.Text = cluster.addr.ToString();
            this.NodeNameTB.Text = cluster.name;
            this.NodeNameL.Text = cluster.name;
            int addr_color = ((int)cluster.addr).enterRound(0, 99);
            if (cluster.addr < 100)
            {
                this.highBTN.BackColor = num_to_color[cluster.addr / 10];
                this.lowBTN.BackColor = num_to_color[cluster.addr % 10];
            }
            else
            {
                this.highBTN.BackColor = Color.Gray;
                this.lowBTN.BackColor = num_to_color[0];

            }
        }

        protected override void WriteData()
        {
            cluster.writeBankinit();
            if (NodeNameTB.Text != "")
            {
                cluster.name = NodeNameTB.Text;
            }
            byte new_addr = (byte)((int)AddrNUM.Value);
            if (new_addr == cluster.addr)
            {
                cluster.error_behavior = 1;
                cluster.write();
            }
            else
            {
                if (cluster.isNewAddrAvaliable(new_addr))
                {
                    cluster.addr = new_addr;
                    cluster.error_behavior = 1;
                    cluster.write();
                }
                else
                {
                    DialogResult res;
                    string st = string.Format("There is anther node which use the address({0}), click OK to contiual.", new_addr);
                    res = MessageBox.Show(st, "New Address Exist", MessageBoxButtons.OKCancel);
                    if (res == DialogResult.OK)
                    {
                        cluster.addr = new_addr;
                        cluster.error_behavior = 1;
                        cluster.write();
                    }
                }
            }
        }


        private void highBTN_Click(object sender, EventArgs e)
        {
            this.cluster.ledAddr(AddressCluster.LedAddrType.High);
        }

        private void lowBTN_Click(object sender, EventArgs e)
        {
            this.cluster.ledAddr(AddressCluster.LedAddrType.Low);
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.cluster.ledAddr(AddressCluster.LedAddrType.Close);
        }
    }
}
