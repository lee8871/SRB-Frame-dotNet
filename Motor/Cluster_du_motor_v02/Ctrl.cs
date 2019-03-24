using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB.NodeType.Du_motor.Cluster_Du_Motor_v02
{
    partial class Ctrl : UserControl
    {
        Clu cluster;
        public Ctrl(Clu c)
        {
            InitializeComponent();
            cluster = c;
            c.eDataChanged += new EventHandler(c_dataChanged);
            cluster.read();
        }
        public string periodToFreq(int period)
        {
            double f = 16000000 / period;
            return f.ToString("F2") + "Hz";
        }
        public int freqToPeriod(string st)
        {
            switch (st)
            {
                case "1kHz":
                    return 16000;
                case "2kHz":
                    return 8000;
                case "2.5kHz":
                    return 6400;
                case "5kHz":
                    return 3200;
                case "10kHz":
                    return 1600;
                case "20kHz":
                    return 800;
                default :
                    return 1600;
            }
        }

        void c_dataChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler d = new EventHandler(c_dataChanged);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                freqL.Text = string.Format("Freq. is {0} <- {1}.\n The max speed is {2}.",
                    periodToFreq(cluster.period), FreqCB.Text, cluster.period);

                motor_a_minNUM.Value = cluster.min_pwm_a / 16;
                motor_b_minNUM.Value = cluster.min_pwm_b / 16;
                SetDelayNUM.Value = cluster.lose_control_ms;
                motorMinL.Text = string.Format("Port A {0} <- {1}, PortB  {2} <- {3}",
                    cluster.min_pwm_a / 16, motor_a_minNUM.Value,
                    cluster.min_pwm_b / 16, motor_b_minNUM.Value);

                setLoseBehaviorBC();
                behaviorL.Text = string.Format("[{0}] <- [{1}]",
                    last_behavior, behaviorCB.Text);
            }
        }

        private void write(object sender, EventArgs e)
        {
            cluster.writeBankinit();
            cluster.period = (ushort)freqToPeriod(FreqCB.Text);
            cluster.min_pwm_a = (ushort)(16 * motor_a_minNUM.Value);
            cluster.min_pwm_b = (ushort)(16 * motor_b_minNUM.Value);
            cluster.lose_control_ms = (byte)SetDelayNUM.Value;
            setLoseBehavior();
            cluster.write();
        }
        public void setLoseBehavior()
        {
            switch (behaviorCB.Text)
            {
                case "Close No Break":
                    cluster.lose_behavior = 0;
                    break;
                case "Close And Break":
                    cluster.lose_behavior = 1;
                    break;
                case "Keep Last Cmd":
                    cluster.lose_behavior = 2;
                    break;
            }
        }
        string last_behavior;
        public void setLoseBehaviorBC()
        {
            switch (cluster.lose_behavior)
            {
                case 0:
                    last_behavior = behaviorCB.Text = "Close No Break";
                    break;
                case 1:
                    last_behavior = behaviorCB.Text = "Close And Break";
                    break;
                case 2:
                    last_behavior = behaviorCB.Text = "Keep Last Cmd";
                    break;
            }
        }
        private void read(object sender, EventArgs e)
        {
            cluster.read();
        }

        private void behaveCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            behaviorL.Text = string.Format("[{0}] <- [{1}]",
                last_behavior, behaviorCB.Text);
        }

        private void motor_X_minNUM_ValueChanged(object sender, EventArgs e)
        {
            motorMinL.Text = string.Format("Port A {0} <- {1}, PortB  {2} <- {3}",
                cluster.min_pwm_a / 16, motor_a_minNUM.Value,
                cluster.min_pwm_b / 16, motor_b_minNUM.Value);
        }

        private void FreqCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            freqL.Text = string.Format("Freq. is {0} <- {1}",
                periodToFreq(cluster.period), FreqCB.Text);
        }
    }
}
