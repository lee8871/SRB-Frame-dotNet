using SRB.Frame;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace SRB.NodeType.SpeedMotorF
{
    public partial class Ctrl : INodeControl
    {
        private Interpreter bgd;
        private string Handle_text;
        public Ctrl(Node n) :
            base(n)
        {
            bgd = (Interpreter)n.Datas;
            bgd.pid_clu.read();
            max_speed = (bgd.pid_clu.k0 * 1024 / (bgd.pid_clu.k1 + 1024)) - 10;
            InitializeComponent();
            Handle_text = handleBTN.Text;
            handleTextRefresh();
            Node.eDataAccessRecv += Node_eDataAccessRecv;
            this.Disposed += Ctrl_Disposed;
        }

        private void Ctrl_Disposed(object sender, EventArgs e)
        {
            Node.eDataAccessRecv -= Node_eDataAccessRecv;
        }

        private void Node_eDataAccessRecv(object sender, AccessEventArgs e)
        {
            bgd.set_displacement = 0;
        }
        protected override void refreshData()
        {
            base.refreshData();
            this.SpeedLAB.Text = string.Format("Speed: {0}", bgd.sensor_speed);
            this.OdometerLAB.Text = string.Format("Odometer: {0}", bgd.odometer);
        }

        int max_speed;
        private int speed;

        public int Speed => speed;

        private void Handle_tick_Tick(object sender, EventArgs e)
        {
            if (Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.handleBTN.Capture)
                {
                    Point moues = this.PointToClient(Control.MousePosition);
                    speed = gain * (moues.X - handleBTN.Location.X - (handleBTN.Size.Width / 2));
                    if (speed >= max_speed)
                    {
                        speed = max_speed;
                    }
                    if (speed <= -max_speed)
                    {
                        speed = -max_speed;
                    }
                    handleTextRefresh();
                    return;
                }
            }
            this.Handle_tick.Stop();
        }
        protected override void OnAccess()
        {
            bgd.target_speed = Speed;
            base.OnAccess();
        }

        public int Motor_x { get; set; }
        public int Motor_y { get; set; }

        debugFORM df;
        private void DebugBTN_Click(object sender, EventArgs e)
        {
            if (df == null)
            {
                df = new debugFORM(this.bgd,this);
                this.components.Add(df);
            }
            if (df.Visible == false)
            {
                df.Show();
            }


        }
        int[] gain_table = {1, 2, 5, 10,15,20};
        int gain_point = 3;
        int gain => gain_table[gain_point];

        private void handleBTN_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                gain_point++;
                gain_point %= gain_table.Length; 
                handleTextRefresh();
            }

            if (e.Button == MouseButtons.Left)
            {
                Handle_tick.Start();
            }
        }
        private void handleTextRefresh()
        {
            handleBTN.Text = string.Format("电机操纵杆({0}倍数)\n速度{1}", gain, speed);
            if (df != null)
            {
                df.setSpeed(speed);
            }
        }

        private void StopBTN_Click(object sender, EventArgs e)
        {
            speed = 0;
            handleTextRefresh();
        }
    }
}
