using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRB.Frame;
using System.Windows.Forms;
using SRB.Support;
using System.Xml;

namespace SRB.NodeType.SpeedMotorF
{


    public struct MotorStatus
    {
        public double time;
        public double target_speed;
        public double sensor_speed;
        public double odometer;
        public MotorStatus (double timel, double target_speed, double sensor_speed, double odometer)
        {
            this.time = timel;
            this.target_speed = target_speed;
            this.sensor_speed = sensor_speed;
            this.odometer = odometer;
        }
    };
    class TestSequence
    {
        Interpreter bgd;
        SrbThread st;
        public bool is_open_csv = false;
        public bool is_open_svg = true;
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
                output_csv += string.Format("{0},{1},{2},{3}\n", sta.time,sta.target_speed,sta.sensor_speed,sta.odometer / 10.0);
            }


            try
            {
                System.IO.File.WriteAllText(file, output_csv, System.Text.Encoding.UTF8);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "不能写日志文件！");
            }
            if (is_open_csv)
            {
                try
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "/" + file);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString(), "不能打开日志文件！");
                }
            }
            return;
        }

        public void saveToSvg(MotorStatus[] motor_status_array)
        {
            string file = FileRecordMaker.getStringFile("SpeedMotorF", "test-sequence", "svg");
            var setting = new XmlWriterSettings();
            setting.Indent = true;

            XmlWriter writer = XmlWriter.Create(file, setting);
            double Y_zoom = 0.1;
            double X_zoom = 1;
            double w = motor_status_array.Length * period_in_ms * X_zoom + 100;
            double h = 15000 * 2 * Y_zoom + 100;


            writer.WriteStartDocument();
            writer.WriteStartElement("svg", "http://www.w3.org/2000/svg");
            writer.WriteAttributeString("version", "1.1");
            writer.WriteAttributeString("id", "svg_head");
            writer.WriteAttributeString("width", $"{w}");
            writer.WriteAttributeString("height", $"{h}");

            writer.WriteStartElement("g");
            writer.WriteAttributeString("id", "grid");
            writer.WriteAttributeString("transform", $"translate(50, {h/2})");



            string d_st = "";
            double x_grid_space = 1000 * X_zoom * 0.1;
            double y_grid_space = 1000 * X_zoom * 0.2;

            void buildGrid(string width) {
                for (double x = x_grid_space; x < w; x += x_grid_space)
                {
                    d_st += $"M {x} {-h / 2} L {x} {h / 2}";
                }
                for (double y = y_grid_space; y < h / 2; y += y_grid_space)
                {
                    d_st += $"M 0 {y} L {w} {y}";
                    d_st += $"M 0 {-y} L {w} {-y}";
                }
                writer.WriteStartElement("path");
                writer.WriteAttributeString("id", "grid");
                writer.WriteAttributeString("d", d_st);
                writer.WriteAttributeString("stroke", "black");
                writer.WriteAttributeString("stroke-width", width);
                writer.WriteAttributeString("fill", "none");
                writer.WriteEndElement();
            }
            d_st = "";
            x_grid_space = 1000 * X_zoom * 0.1;
            y_grid_space = 1000 * X_zoom * 0.2;
            buildGrid("0.2");
            d_st = "";
            x_grid_space = 1000 * X_zoom ;
            y_grid_space = 1000 * X_zoom;
            buildGrid("0.4");

            for (double x = x_grid_space; x < w; x += x_grid_space)
            {
                writer.WriteStartElement("text"); 
                writer.WriteAttributeString("x", $"{x}");
                writer.WriteAttributeString("y", "0");
                writer.WriteAttributeString("fill", "black");
                writer.WriteAttributeString("font-family","微软雅黑");
                writer.WriteAttributeString("font-size", "32px");
                writer.WriteString($"{x /1000/ X_zoom}s");
                writer.WriteEndElement();
            }


            writer.WriteStartElement("path");
            writer.WriteAttributeString("id", "Coordinate");
            writer.WriteAttributeString("d", $"M 0 0 L {w - 50} 0  M 0 {-h / 2} L  0 {h / 2}");
            writer.WriteAttributeString("stroke", "black");
            writer.WriteAttributeString("stroke-width", "2");
            writer.WriteAttributeString("fill", "none");
            writer.WriteEndElement();



            string target_st = "M 0 0 ";
            string senseor_st = "M 0 0 ";
            foreach (var sta in motor_status_array) 
            {
                string x = $"{X_zoom * sta.time}";
                target_st += $"L {x} {-sta.target_speed* Y_zoom}";
                senseor_st += $"L {x} {-sta.sensor_speed * Y_zoom}";
            }
            writer.WriteStartElement("path");
            writer.WriteAttributeString("id", "target_speed");
            writer.WriteAttributeString("d", target_st);
            writer.WriteAttributeString("stroke", "DarkViolet");
            writer.WriteAttributeString("stroke-width", "1");
            writer.WriteAttributeString("fill", "none");
            writer.WriteEndElement();

            writer.WriteStartElement("path");
            writer.WriteAttributeString("id", "senseor_speed");
            writer.WriteAttributeString("d", senseor_st);
            writer.WriteAttributeString("stroke", "Green");
            writer.WriteAttributeString("stroke-width", "1");
            writer.WriteAttributeString("fill", "none");
            writer.WriteEndElement();


            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.Flush();
            writer.Close();
            writer.Dispose();

            try
            {
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "不能写入文件！");
            }
            if (is_open_svg)
            {
                try
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "/" + file);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString(), "不能打开文件！");
                }
            }
            return;
        }
    }
}
