using System;
using System.Windows.Forms;

namespace SRB.Frame
{
    public partial class UpdateControl : UserControl
    {
        private BaseNode.SrbUpdater updater;
        private bool enable_write = true;

        public bool Enable_write { get => enable_write; set => OnEnable_write(value); }

        private void OnEnable_write(bool value)
        {
            this.BurnBTN.Visible = enable_write = value;
        }
        public UpdateControl(BaseNode.SrbUpdater updater)
        {
            InitializeComponent();
            this.updater = updater;
            this.UpdateInformationgRTB.Text = "HC:"+updater.Hardware_code+"\n";
        }


        private void openBTN_Click(object sender, EventArgs e)
        {
            MainOF.ShowDialog();
        }

        private void MainOF_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                updater.loadFile(MainOF.FileName);
            }
            catch(Exception exception)
            {
                this.BurnBTN.Enabled = false;
                this.UpdateFileTB.Text = exception.ToString();
            }
            this.UpdateFileTB.Text = MainOF.FileName;
            this.UpdateInformationgRTB.Text = "HC:" + updater.Hardware_code + "\n"+updater.File_information;
            this.BurnBTN.Enabled = true;
        }

        private void BurnBTN_Click(object sender, EventArgs e)
        {
            updater.update();
        }

        private void ResetBTN_Click(object sender, EventArgs e)
        {
            updater.gotoNormalMode();
        }
    }
}
