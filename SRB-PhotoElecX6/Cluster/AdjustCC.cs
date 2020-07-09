using SRB.Frame;
using System;
using System.Windows.Forms;

namespace SRB.NodeType.PhotoElecX6
{
    internal partial class AdjustCC : IClusterControl
    {
        private const string No_adjust = "NoAdjust";
        private AdjustCluster cluster;
        public AdjustCC(AdjustCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.readAll();
        }
        public string adjToName(int adj)
        {
            switch (adj)
            {
                case 1:
                    return "255";
                case 2:
                    return "1000";
                case 3:
                    return "10000";
                default:
                    return "NoAdjust";
            }
        }
        public int nameToAdj(string st)
        {
            switch (st)
            {
                case No_adjust:
                    return 0;
                case "255":
                    return 1;
                case "1000":
                    return 2;
                case "10000":
                    return 3;
                default:
                    throw (new ArgumentException("adjustment Value is not exist."));
            }
        }


        protected override void DataUpdata()
        {
            this.AdjCB.Text = adjToName(cluster.adj);
            motorATogCBOX.CheckState = cluster.motor_a_tog ? CheckState.Checked : CheckState.Unchecked;
            motorBTogCBOX.CheckState = cluster.motor_b_tog ? CheckState.Checked : CheckState.Unchecked;
        }
        protected override void WriteData()
        {
            cluster.writeBankinit();
            cluster.adj = (byte)nameToAdj(AdjCB.Text);
            cluster.motor_a_tog = motorATogCBOX.Checked;
            cluster.motor_b_tog = motorBTogCBOX.Checked;
            cluster.write();
        }
    }
}
