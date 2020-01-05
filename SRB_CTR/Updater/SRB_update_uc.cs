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


        private void GotoUpdateByPowerOnBTN_Click(object sender, System.EventArgs e)
        {
            backlogic.Update_all.gotoUpdateModeAllFromPowerOn();
        }
        private void scanUpdateBTN_Click(object sender, System.EventArgs e)
        {

            this.backlogic.scanUpdateNodes();
        }

        private void FileBTN_Click(object sender, System.EventArgs e)
        {
            backlogic.Update_all.loadFiles("./update");
            infoRTC.Clear();
            infoRTC.AppendText("lode file from './update'.\n" +
                 backlogic.Update_all.Sup_loader.ToString()); 

        }

        private void BurnBTN_Click(object sender, System.EventArgs e)
        {
            backlogic.Update_all.burnAll(appendInfo);
        }
        public void appendInfo(string st)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { appendInfo(st); }));
            }
            else
            {
                if (st == null)
                {
                    infoRTC.Clear();
                }
                else { 
                    infoRTC.AppendText(st);
                }
            }
        }







    }

}
