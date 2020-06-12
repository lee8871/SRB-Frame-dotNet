using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SRB.Frame;

namespace SRB.NodeType.SpeedMotor
{
    public partial class debugFORM : Form
    {
        Interpreter bgd;
        private SrbThread motor_test_ST;
        object speed_lock = new object();
        int speed;
        public debugFORM()
        {
            InitializeComponent();
        }
        public debugFORM(Interpreter n)
        {
            InitializeComponent();
            motor_test_ST = new SrbThread(motor_test_Thread);
            bgd = n;
        }
        protected double period_in_ms = 2;

        private double getElapsedMs(Stopwatch sw)
        {
            return (1000.0 * sw.ElapsedTicks) / Stopwatch.Frequency; ;
        }
        private void motor_test_Thread(SrbThread.dIsThreadStoping IsStoping)
        {
            Stopwatch sw = new Stopwatch();
            int speed;
            string output_csv = "时间,目标速度,传感速度,里程计\n";
            double time = 0;
            while (true)
            {
                sw.Restart();
                lock (speed_lock)
                {
                    speed = this.speed;
                }
                bgd.target_speed = speed;
                bgd.addDataAccess(1, true);
                output_csv += string.Format("{0},{1},{2},{3}\n", time,speed, bgd.sensor_speed, bgd.odometer / 10.0);
                if (IsStoping())
                {
                    string path = "./log/测速电机调试记录/";
                    path += System.DateTime.Now.ToString("yy-MM-dd");
                    path += "/";
                    System.IO.Directory.CreateDirectory(path);//如果文件夹不存在就创建它
                    string time_str = System.DateTime.Now.ToString("HHmmss");
                    string Table_file = path + "电机数据" + time_str + ".csv";
                    try
                    {
                        System.IO.File.WriteAllText(Table_file, output_csv, System.Text.Encoding.UTF8);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString(), "不能写日志文件！");
                    }
                    try
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + "/" + Table_file);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString(), "不能打开日志文件！");
                    }
                    return;
                }
                while(getElapsedMs(sw) < period_in_ms) ;
                time += period_in_ms;
            }

        }
        private void handle_Tick(object sender, EventArgs e)
        {
            int x;
            if (Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.controlStickBTN.Capture)
                {
                    Point moues = this.PointToClient(Control.MousePosition);
                    x = moues.X - controlStickBTN.Location.X - this.splitContainer1.Panel1.Width - (controlStickBTN.Size.Width / 2);
                    this.SpeedLAB.Text = x.ToString() ;
                    lock (speed_lock)
                    {
                        speed = x;
                    }
                }
            }
        }

        private void RunTestBTN_Click(object sender, EventArgs e)
        {
            if (this.motor_test_ST.Is_running == false)
            {
                RunTestBTN.BackColor = Color.LightBlue;
                motor_test_ST.run(bgd.Bus);
                this.handleTIMER.Start();
            }
            else
            {
                RunTestBTN.BackColor = Control.DefaultBackColor;
                motor_test_ST.stop();
                this.handleTIMER.Stop();
            }
        }

        private void debugFORM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                RunTestBTN.BackColor = Control.DefaultBackColor;
                motor_test_ST.stop();
                this.handleTIMER.Stop();
            }
        }
    }
}
