using System;
using System.Drawing;
using System.Windows.Forms;
using SRB.Frame;

namespace SRB_CTR
{
    public partial class SyncBroadcastC : Form
    {
        Node.SyncCluster.Broadcast bg;
        public SyncBroadcastC(Node.SyncCluster.Broadcast bg)
        {
            this.bg = bg;
            InitializeComponent();
        }
        int sync_num=0;
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
                    infoRTC.ScrollToCaret();
                }
            }
        }


        private void do_syncBTN_Click(object sender, EventArgs e)
        {
            infoRTC.AppendText("\n## Sync" + (sync_num++) + "  " + System.DateTime.Now.ToLongTimeString() + "\n");

            bg.syncAll();
            bg.getSyncStatuc(appendInfo);
            
            /*
            text += details;
            if (details.Contains("+ Diff avariage out of range!"))
            {
                key_info += text;
                text += "+ KEY Write down!\n";
            }
            */
        }
        private void readBTN_Click(object sender, EventArgs e)
        {
            bg.getSyncStatuc(appendInfo);
        }

        private void saveBTN_Click(object sender, EventArgs e)
        {
            string path = "./log/时间同步调试记录/";
            path += System.DateTime.Now.ToString("yy-MM-dd");
            path += "/";
            System.IO.Directory.CreateDirectory(path);//如果文件夹不存在就创建它
            string time_str = System.DateTime.Now.ToString("HHmmss");
            string Commen_file = path + "常规记录" + time_str + ".md";
            string Table_file = path + "同步误差表" + time_str + ".csv";
            try
            {
                System.IO.File.WriteAllText(Commen_file, infoRTC.Text, System.Text.Encoding.UTF8);
                System.IO.File.WriteAllText(Table_file, bg.getSyncTableCsv(),System.Text.Encoding.UTF8);
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString(),"不能写日志文件！");
            }
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "/" + Table_file);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "不能打开日志文件！");
            }
            infoRTC.Clear();
        }
        bool is_syncing = false;
        bool is_reportting=false;

        private void timer500_Tick(object sender, EventArgs e)
        {
            if(is_syncing)
            {
                do_syncBTN_Click(sender, e);
            }
            else if(is_reportting)
            {
                readBTN_Click(sender, e);
            }
        }

        private void ReadBTN_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                is_reportting ^= true;
                if (is_reportting)
                {
                    ReadBTN.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    ReadBTN.BackColor = Button.DefaultBackColor ;
                }
            }
        }

        private void do_syncBTN_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                is_syncing ^= true;
                if (is_syncing)
                {
                    do_syncBTN.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    do_syncBTN.BackColor = Button.DefaultBackColor;
                }
            }

        }



        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timerAuto.Interval =(int)( timer_interval_NUM.Value * 1000);
        }

        private void SyncBroadcastC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                is_syncing = false;
                is_reportting = false;
            }
        }
    }
}
