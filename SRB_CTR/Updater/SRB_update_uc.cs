using System.Windows.Forms;

namespace SRB_CTR
{
    internal partial class UpdateAll_uc : UserControl
    {
        private SrbOnelineMaster backlogic;
        public UpdateAll_uc(SrbOnelineMaster backlogic)
        {
            InitializeComponent();
            this.backlogic = backlogic;
        }

        private void gotoUpdateMode_Click(object sender, System.EventArgs e)
        {
            backlogic.Update_all.gotoUpdateModeAll();

        }

        private void BurnBTN_Click(object sender, System.EventArgs e)
        {
            backlogic.Update_all.burnAll();
        }

        private void GotoUpdateByPowerOnBTN_Click(object sender, System.EventArgs e)
        {
            backlogic.Update_all.gotoUpdateModeAllFromPowerOn();
        }
    }

}
