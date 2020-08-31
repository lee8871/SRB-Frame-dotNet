using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRB.Frame;
using System.Windows.Forms;
using SRB.Support;

namespace SRB.NodeType.SpeedMotorF
{


    public struct MotorStatus
    {
        public double timel;
        public double target_speed;
        public double sensor_speed;
        public double odometer;
        public MotorStatus (double timel, double target_speed, double sensor_speed, double odometer)
        {
            this.timel = timel;
            this.target_speed = target_speed;
            this.sensor_speed = sensor_speed;
            this.odometer = odometer;
        }
    };
    class TestSequence
    {
        Interpreter bgd; 
        SrbThread st;
        public SrbThread St => st;
        public TestSequence(Interpreter bgd )
        {
            this.bgd = bgd;
            st = new SrbThread(TestSequence_Thread);
        }
        public int[] target_speed_table;
        public double period_in_ms = 2;

        protected double getElapsedMs(Stopwatch sw)
        {
            return (1000.0 * sw.ElapsedTicks) / Stopwatch.Frequency; ;
        }
        public int[] initSpeedArray()
        {
            int max_speed = (bgd.pid_clu.k0 * 1024 / (bgd.pid_clu.k1 + 1024)) - 10;
            int s0 = 0;
            int s1 = max_speed / 4;
            int s2 = max_speed * 3 / 4;
            int[] speed_table_1 = { s0, s1, s2, s1, s2, -s1, -s2, -s1, -s2,  s0 };
            int[] speed_array = new int[500*speed_table_1.Length];
            for(int i = 0; i < speed_array.Length; i++)
            {
                speed_array[i] = speed_table_1[i / 500];
            }      
            return speed_array;
        }


        public void TestSequence_Thread(SrbThread.dIsThreadStoping IsStoping)
        {
            if (target_speed_table == null)
            {
                target_speed_table = initSpeedArray();
            }
            Stopwatch sw = new Stopwatch();
            MotorStatus[] motor_status_array = new MotorStatus[target_speed_table.Length];

            double time = 0;
            sw.Restart();
            for (int i = 0; i < target_speed_table.Length; i++)
            {
                bgd.target_speed = target_speed_table[i];
                bgd.addDataAccess(1, true);
                motor_status_array[i] = new MotorStatus(getElapsedMs(sw), bgd.target_speed, bgd.sensor_speed, bgd.odometer);
                if (IsStoping())
                {
                    break;
                }
                time += period_in_ms;
                while (getElapsedMs(sw) < time) ;
            }
            if(eGetMotorStatus != null)
            {
                eGetMotorStatus.Invoke(motor_status_array);
            }
        }
        public delegate void dGetMotorStatus(MotorStatus[] motor_status_array);
        public event dGetMotorStatus eGetMotorStatus;

        public void saveToCsv(MotorStatus[] motor_status_array)
        {
            string file = FileRecordMaker.getStringFile("SpeedMotorF", "test-sequence", "csv");

            string output_csv = "time,target_s,sensor_s,odometer\n";
            foreach(MotorStatus sta in motor_status_array)
            {
                output_csv += string.Format("{0},{1},{2},{3}\n", sta.timel,sta.target_speed,sta.sensor_speed,sta.odometer / 10.0);
            }


            try
            {
                System.IO.File.WriteAllText(file, output_csv, System.Text.Encoding.UTF8);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "不能写日志文件！");
            }
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "/" + file);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "不能打开日志文件！");
            }
            return;
        }

/*
        public void TestSequence_Thread_old(SrbThread.dIsThreadStoping IsStoping)
        {
            Stopwatch sw = new Stopwatch();
            string output_csv = "time,target_s,sensor_s,odometer\n";
            double time = 0;
            MotorStatus[] motor_status_array = new MotorStatus[];
            while (true)
            {
                sw.Restart();
                int i = ((int)time) / 1000;
                if (i >= speed_table_1.Length)
                {
                    break;
                }
                bgd.target_speed = speed_table_1[((int)time) / 1000];
                bgd.addDataAccess(1, true);
                output_csv += string.Format("{0},{1},{2},{3}\n", time, bgd.target_speed, bgd.sensor_speed, bgd.odometer / 10.0);
                if (IsStoping())
                {
                    break;
                }
                while (getElapsedMs(sw) < period_in_ms) ;
                time += period_in_ms;
            }
            string path = "./log/SpeedMotorF-test-record/";
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

        */

    }
}
