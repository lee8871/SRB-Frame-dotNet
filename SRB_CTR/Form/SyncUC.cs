using SRB.Frame;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SRB_CTR
{
    internal partial class SyncUC : UserControl
    {
        private SRB.Frame.Node.SyncCluster.Broadcast sync_bc;
        public SyncUC(SRB.Frame.Node.SyncCluster.Broadcast sync_bc=null)
        {
            this.sync_bc = sync_bc;
            InitializeComponent();
        }
        public override void Refresh()
        {
            base.Refresh();
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void calibrationBTN_Click(object sender, EventArgs e)
        {
            if (sync_bc.Is_calibrat_running == false)
            {
                sync_bc.calibrat(appendInfo);
            }
            else
            {
                sync_bc.calibratAbandon();
            }

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
                else
                {
                    infoRTC.AppendText(st);
                }
            }
        }


        private void recoedBTN_Click(object sender, EventArgs e)
        {
            if (this.infoRTC.Visible)
            {
                infoRTC.Hide();
            }
            else
            {
                infoRTC.Show();
            }

        }
        private void syncBTN_Click(object sender, EventArgs e)
        {
            if (sync_bc.Is_synchronize_running)
            {
                sync_bc.syncStop() ;
            }
            else
            {
                sync_bc.syncStart();
            }

        }
        int syncBtnFlag;


        Image[] syncBtn_Images =
    { Properties.Resources.clock,
            Properties.Resources.clock1,
            Properties.Resources.clock2,
            Properties.Resources.clock3
        };

        public Image syncBTN_changeImage()
        {
            if (sync_bc.Is_synchronize_running)
            {
                syncBtnFlag++;
                if (syncBtnFlag == 4)
                {
                    syncBtnFlag = 0;
                }
                syncBTN.Image = syncBtn_Images[syncBtnFlag];
                return syncBtn_Images[syncBtnFlag];
            }
            else
            {
                if (syncBtnFlag == 0)
                {
                    return null;
                }
                else
                {
                    syncBtnFlag = 0;
                    syncBTN.Image = syncBtn_Images[syncBtnFlag];
                    return syncBtn_Images[syncBtnFlag];

                }
            }
        }

        private void syncClearBTN_Click(object sender, EventArgs e)
        {
            sync_bc.calibratClean(appendInfo);

        }






        #region 测试同步效果并储存 




        private SrbThread sync_test_ST = null;
        private void syncDiffTestBTN_Click(object sender, EventArgs e)
        {
            if (sync_test_ST == null) {
                sync_test_ST = new SrbThread(sync_test_Thread);
            }
            if (this.sync_test_ST.Is_running == false)
            {
                (sender as ToolStripMenuItem).BackColor = Color.Gold;
                sync_test_ST.run(sync_bc.Bus);
            }
            else
            {
                (sender as ToolStripMenuItem).BackColor = Control.DefaultBackColor;
                sync_test_ST.stop();
            }
        }
        private void sync_test_Thread(SrbThread.dIsThreadStoping IsStoping) {
            Node[] sync_check_nodes = null;
            string sync_check_str = null;
            while (true) { 
                sync_bc.getSyncStatus(ref sync_check_str, ref sync_check_nodes);
                System.Threading.Thread.Sleep(500);
                if (IsStoping())
                {
                    string path = "./log/Sync-record/";
                    path += System.DateTime.Now.ToString("yy-MM-dd");
                    path += "/";
                    System.IO.Directory.CreateDirectory(path);//如果文件夹不存在就创建它
                    string time_str = System.DateTime.Now.ToString("HHmmss");
                    string Table_file = path + "Sync" + time_str + ".csv";
                    string Png_file = path + "Sync" + time_str + ".png";
                    string md_file = path + "Sync" + time_str + ".md";
                    string R_Argument = path + "Sync" + time_str;
                    try
                    {
                        System.IO.File.WriteAllText(Table_file, sync_check_str, System.Text.Encoding.ASCII);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString(), "不能写日志文件！");
                    }
                    
                    string cmdStr = string.Format(@"{2}/{0} {2}/{1}",
                    "R/SyncTest.r", R_Argument, Application.StartupPath);
                    SRB.Frame.LanguageR.run(cmdStr);
                    try
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + "/" + Png_file);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString(), "没有找到图像");
                    }
                    try
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + "/" + md_file);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString(), "没有找到文档");
                    }
                    return;
                }
            }

        }
        #endregion
    }
}
