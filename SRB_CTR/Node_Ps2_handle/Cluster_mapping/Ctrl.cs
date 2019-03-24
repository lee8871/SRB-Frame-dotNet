using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SRB_CTR;

namespace SRB_CTR.nsBrain.Node_PS2_handle.Cluster_mapping
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
            UpRTC.Text = cluster.up_mapping.ToArrayString();
            DownRTC.Text = cluster.down_mapping.ToArrayString();
        }

        private void write(object sender, EventArgs e)
        {
            byte[] up = UpRTC.Text.ToByteAsCArroy();
            byte[] down = DownRTC.Text.ToByteAsCArroy();

            cluster.setMapping(up, down);
            cluster.write();
            try
            {
            }
            catch(Exception msg )
            {
                MessageBox.Show(msg.ToString());
            }
        }
        private void read(object sender, EventArgs e)
        {
            cluster.read();
        }




        private void RTC_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
