using SRB.Frame;
using System;
using System.Windows.Forms;

namespace SRB.NodeType.SpeedMotor
{
    internal partial class PidCC : IClusterControl
    {
        private PidCluster cluster;
        public PidCC(PidCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.readAll();
        }

        protected override void DataUpdata()
        {
            this.k0NUM.Value = cluster.k0;
            this.k1NUM.Value = cluster.k1;
            this.kpNUM.Value = (decimal)cluster.kp;
            this.kiNUM.Value = (decimal)cluster.ki;
            this.kdNUM.Value = (decimal)cluster.kd;

        }
        protected override void WriteData()
        {
            
            cluster.k0 = (ushort)this.k0NUM.Value;
            cluster.k1 = (ushort)this.k1NUM.Value;
            cluster.kp = (double)this.kpNUM.Value;
            cluster.ki = (double)this.kiNUM.Value;
            cluster.kd = (double)this.kdNUM.Value;
            cluster.write();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
