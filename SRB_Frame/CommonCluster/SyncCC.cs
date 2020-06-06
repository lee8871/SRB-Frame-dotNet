using System;
using System.Drawing;
using System.Windows.Forms;

namespace SRB.Frame
{
    internal partial class SyncCC : IClusterControl
    {
        private Node.SyncCluster cluster;
        public SyncCC(Node.SyncCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.read();
        }

        protected override void DataUpdata()
        {
            if (cluster.us4 == 255)
            {
                TimeLAB.Text = ("Sync miss");
            }
            else
            {
                TimeLAB.Text = string.Format("Sync {0} Clock is: {1}.{2}:{3}",
                    cluster.sno, cluster.ms / 1000, cluster.ms % 1000, cluster.us4);
            }
            calibrationLAB.Text = string.Format("calibration = {0}",
                cluster.CalibrationClu.calibration_value);
        }
    }
}