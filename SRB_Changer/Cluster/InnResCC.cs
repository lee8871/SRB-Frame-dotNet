using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SRB.Frame;

namespace SRB.NodeType.Charger
{
    partial class InnResCC : IClusterControl
    {
        InnResCluster cluster;
        public InnResCC(InnResCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.read();
        }


        protected override void DataUpdata()
        {
            string a ="";
            for (int i = 0; i < 15; i++)
            {
                a += cluster.mOhm(i) + "mΩ  ";
            }
            this.valueRTC.Text = a;
        }

        protected override void WriteData()
        {
            cluster.writeBankinit();
        }

        private void BatteryCC_Load(object sender, EventArgs e)
        {

        }

        private void lowVotNUM_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
