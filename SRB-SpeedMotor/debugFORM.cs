﻿using System;
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
        Ctrl ctrl;
        private SrbThread motor_test_ST;
        private SrbThread get_speed_table_ST;
        private SrbThread TestSequence_ST;
        object speed_lock = new object();
        int speed;
        public debugFORM()
        {
            InitializeComponent();
        }
        public debugFORM(Interpreter n, Ctrl c)
        {
            ctrl = c;
            InitializeComponent();
            motor_test_ST = new SrbThread(motor_test_Thread);
            get_speed_table_ST = new SrbThread(get_speed_table_Thread);
            TestSequence_ST = new SrbThread(TestSequence_Thread);
            bgd = n;
        }
        protected double period_in_ms = 2;

        private double getElapsedMs(Stopwatch sw)
        {
            return (1000.0 * sw.ElapsedTicks) / Stopwatch.Frequency; ;
        }
        private void get_speed_table_Thread(SrbThread.dIsThreadStoping IsStoping)
        {
            Stopwatch sw = new Stopwatch();
            string output_csv = "pwm,speed\n";
            double time = 0;
            ushort pwm = 50;
            for(pwm = 0; pwm < 1000; pwm += 1)
            {
                for (int i = 0; i < 20; i++)
                {
                    sw.Restart();
                    bgd.test_pwm_clu.Direction = 0;
                    bgd.test_pwm_clu.Pwm = pwm;
                    bgd.test_pwm_clu.write();
                    bgd.addDataAccess(1, true);

                    output_csv += string.Format("{0},{1}\n", pwm, bgd.sensor_speed);
                    if (IsStoping())
                    {
                        bgd.test_pwm_clu.Direction = 4;
                        bgd.test_pwm_clu.Pwm = 0;
                        bgd.test_pwm_clu.write();
                        return;
                    }
                    while (getElapsedMs(sw) < period_in_ms) ;
                }
            }


            bgd.test_pwm_clu.Direction = 4;
            bgd.test_pwm_clu.Pwm = 0;
            bgd.test_pwm_clu.write();
            string path = "./log/SpeedMotor-test-record/";
            path += System.DateTime.Now.ToString("yy-MM-dd");
            path += "/";
            System.IO.Directory.CreateDirectory(path);//如果文件夹不存在就创建它
            string time_str = System.DateTime.Now.ToString("HHmmss");
            string Table_file = path + "PWM-speed-test" + time_str + ".csv";
            string Png_file = path + "PWM-speed-test" + time_str + ".png";
            try
            {
                System.IO.File.WriteAllText(Table_file, output_csv, System.Text.Encoding.ASCII);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "不能写日志文件！");
            }
            using (Process myPro = new Process())
            {
                string cmdStr = string.Format(@"{3}/{0} {3}/{1} {3}/{2}",
                "R/moto4.r", Table_file, Png_file, Application.StartupPath);

                string cmdExe = @"C:\Program Files\R\R-4.0.1\bin\rscript.exe";
                //指定启动进程是调用的应用程序和命令行参数
                ProcessStartInfo psi = new ProcessStartInfo(cmdExe, cmdStr);
                myPro.StartInfo = psi;
                myPro.Start();
                myPro.WaitForExit();
            }
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "/" + Png_file);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "没有找到图像");
            }

            return;

        }
        private void TestSequence_Thread(SrbThread.dIsThreadStoping IsStoping)
        {
            int max_speed = (bgd.pid_clu.k0 * 1024 / (bgd.pid_clu.k1 + 1024)) - 10;
            int s0 = 0;
            int s1 = max_speed / 4;
            int s2 = max_speed * 3 / 4;
            int[] speed_table_1 = { s0,s1,s2,s1,s2,s1,s2, s0,-s1, -s2, -s1, -s2, -s1, -s2, s0 };

            Stopwatch sw = new Stopwatch();
            string output_csv = "time,target_s,sensor_s,odometer\n";
            double time = 0;
            while (true)
            {
                sw.Restart();
                int i = ((int)time) / 1000;
                if (i >= speed_table_1.Length)
                {
                    break;
                }
                bgd.target_speed = speed_table_1[((int)time)/1000];
                bgd.addDataAccess(1, true);
                output_csv += string.Format("{0},{1},{2},{3}\n", time, bgd.target_speed, bgd.sensor_speed, bgd.odometer / 10.0);
                if (IsStoping())
                {
                    break;
                }
                while(getElapsedMs(sw) < period_in_ms) ;
                time += period_in_ms;
            }
            string path = "./log/SpeedMotor-test-record/";
            path += System.DateTime.Now.ToString("yy-MM-dd");
            path += "/";
            System.IO.Directory.CreateDirectory(path);//如果文件夹不存在就创建它
            string time_str = System.DateTime.Now.ToString("HHmmss");
            string Table_file = path + "speed_table_1" + time_str + ".csv";
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
        public void setSpeed(int s)
        {
            lock (speed_lock)
            {
                this.speed = s;
            }
        }

        private void RunTestBTN_Click(object sender, EventArgs e)
        {
            if (this.motor_test_ST.Is_running == false)
            {
                RunTestBTN.BackColor = Color.LightBlue;
                motor_test_ST.run(bgd.Bus);
            }
            else
            {
                RunTestBTN.BackColor = Control.DefaultBackColor;
                motor_test_ST.stop();
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
            }
        }
        private void getSpeedTableBTN_Click(object sender, EventArgs e)
        {
            if (this.get_speed_table_ST.Is_running == false)
            {
                getSpeedTableBTN.BackColor = Color.LightBlue;
                get_speed_table_ST.run(bgd.Bus);
            }
            else
            {
                getSpeedTableBTN.BackColor = Control.DefaultBackColor;
                get_speed_table_ST.stop();
            }

        }

        private void TestSequenceBTN_Click(object sender, EventArgs e)
        {

            if (this.TestSequence_ST.Is_running == false)
            {
                (sender as Button).BackColor = Color.LightBlue;
                TestSequence_ST.run(bgd.Bus);
            }
            else
            {
                (sender as Button).BackColor = Control.DefaultBackColor;
                TestSequence_ST.stop();
            }
        }
    }
}
