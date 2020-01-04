using SRB.Frame.updater;
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
            onUpdateInfoChanged(this, null);
        }




        public void onUpdateInfoChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler d = new EventHandler(onUpdateInfoChanged);
                this.Invoke(d, new object[] { sender, e });
                return;
            }
            SupFile file=updater.Sup_file;
            if(file is object)
            {
                displayUpdateInfo(file);
            }
            else
            {
                displayNodeInfo();
            }
        }

        public void displayNodeInfo()
        {
            this.UpdateInformationgRTB.SelectionColor = System.Drawing.Color.DarkRed; 
            this.UpdateInformationgRTB.AppendText("Not load .sup file " + updater.Hardware_code + "\n");
            this.UpdateInformationgRTB.SelectionColor = System.Drawing.Color.Black;
            this.UpdateInformationgRTB.AppendText("Hardware code: " + updater.Hardware_code + "\n");
            this.UpdateInformationgRTB.AppendText(String.Format("srb version {0}.{1}\n",
                updater.Srb_version_major, updater.Srb_version_miner));
            this.UpdateInformationgRTB.AppendText(String.Format("node version {0}.{1}\n",
                updater.Node_version_major, updater.Node_version_miner));
        }


        public void displayUpdateInfo(SupFile file)
        {
            this.UpdateInformationgRTB.Clear();
            bool matched = false;
            foreach (string hc in file.Hardware_codes_array)
            {
                if (updater.Hardware_code == hc)
                {
                    matched = true;
                    break;
                }
            }
            if (matched)
            {
                this.BurnBTN.Enabled = true;
                this.UpdateInformationgRTB.SelectionColor = System.Drawing.Color.DarkGreen;
                this.UpdateInformationgRTB.AppendText("Hardware code matched: " + updater.Hardware_code + "\n");
            }
            else
            {
                this.BurnBTN.Enabled = false;
                this.UpdateInformationgRTB.SelectionColor = System.Drawing.Color.DarkRed;
                this.UpdateInformationgRTB.AppendText("Hardware code NOT matched.\n" +
                    "Hardware:  " + updater.Hardware_code + "\n" +
                    "Sup file:  ");
                foreach (string hc in file.Hardware_codes_array)
                {
                    this.UpdateInformationgRTB.AppendText(hc + ",  ");
                }
                this.UpdateInformationgRTB.AppendText("\n");
            }




            if (file.Srb_version_num > updater.Srb_version_num)
            {
                this.UpdateInformationgRTB.SelectionColor = System.Drawing.Color.DarkGreen;
            }
            else
            {
                this.UpdateInformationgRTB.SelectionColor = System.Drawing.Color.DarkRed;
            }
            this.UpdateInformationgRTB.AppendText(String.Format("srb version {0}.{1} -> {2}.{3}\n",
                updater.Srb_version_major, updater.Srb_version_miner,
                file.Srb_version_major, file.Srb_version_miner));

            

            if (file.Node_version_num > updater.Node_version_num)
            {
                this.UpdateInformationgRTB.SelectionColor = System.Drawing.Color.DarkGreen;
            }
            else
            {
                this.UpdateInformationgRTB.SelectionColor = System.Drawing.Color.DarkRed;
            }
            this.UpdateInformationgRTB.AppendText(String.Format("node version {0}.{1} -> {2}.{3}\n",
                updater.Node_version_major, updater.Node_version_miner,
                file.Node_version_major, file.Node_version_miner));

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

            onUpdateInfoChanged(this, null);
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
