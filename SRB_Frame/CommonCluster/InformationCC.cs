using System;
using System.Windows.Forms;


namespace SRB.Frame
{
    public partial class InformationCC : IClusterControl
    {
        private Node.InformationCluster cluster;
        public InformationCC(Node.InformationCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.read();
        }

        protected override void DataUpdata()
        {
            this.typeL.Text = "Type: " + cluster.type;
            this.versionL.Text =
            string.Format("{0} {1}", cluster.App_version, cluster.Srb_version);
            TimeStampTT.SetToolTip(this.versionL, "TimeStamp = " + cluster.timestampClu.utc);
        }


        private void ResetNodeBTN_Click(object sender, EventArgs e)
        {
            cluster.resetNode();
        }

        private void factorySettingBTN_Click(object sender, EventArgs e)
        {
            string st = string.Format
                ("Are you sure to reset Node {0} to factory setting?",
                cluster.Parent_node.Addr);
            if (MessageBox.Show(this, st, "Factory Setting", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                cluster.factorySettingNode();
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            cluster.gotoUpdateMode();
        }

    }
}
