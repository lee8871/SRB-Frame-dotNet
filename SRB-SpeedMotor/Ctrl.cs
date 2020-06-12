using SRB.Frame;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace SRB.NodeType.SpeedMotor
{
    internal partial class Ctrl : INodeControl
    {
        private Interpreter bgd;
        private string Handle_text;
        public Ctrl(Node n) :
            base(n)
        {
            bgd = (Interpreter)n.Datas;
            InitializeComponent();
            Handle_text = handleBTN.Text;
            n.eBankChangeByAccess += Node_eBankChangeByAccess;
            n.eDataAccessRecv += Node_eDataAccessRecv;
        }
        private void Node_eDataAccessRecv(object sender, AccessEventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler d = new EventHandler(Node_eBankChangeByAccess);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                bgd.set_displacement = 0;
            }
        }


        private void Node_eBankChangeByAccess(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler d = new EventHandler(Node_eBankChangeByAccess);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.SpeedLAB.Text = string.Format("Speed: {0}", bgd.sensor_speed);
                this.OdometerLAB.Text = string.Format("Odometer: {0}", bgd.odometer);
            }

        }

        private int x;
        protected override void OnAccess()
        {
            if (Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.handleBTN.Capture)
                {
                    Point moues = this.PointToClient(Control.MousePosition);
                    x = moues.X - handleBTN.Location.X - (handleBTN.Size.Width / 2);
                    bgd.target_speed = x;
                    this.handleBTN.Text = Handle_text + "\n" + x;
                }
                else if (this.StopBTN.Capture)
                {
                    x = 0;
                    bgd.target_speed = x;
                    this.handleBTN.Text = Handle_text + "\n" + x ;
                }
                /*
                else if (this.BrakeBTN.Capture)
                {
                    datas.Brake_a = 10000;
                    datas.Brake_b = 10000;
                    this.handleBTN.Text = Handle_text + "\n" + "Braking";

                }*/
            }

            base.OnAccess();
        }

        public int Motor_x { get; set; }
        public int Motor_y { get; set; }

        private void handleBTN_Click(object sender, EventArgs e)
        {

        }

        private void StopBTN_Click(object sender, EventArgs e)
        {

        }

        private void BrakeBTN_Click(object sender, EventArgs e)
        {

        }

        private void OdometerLAB_Click(object sender, EventArgs e)
        {

        }
        debugFORM df;
        private void DebugBTN_Click(object sender, EventArgs e)
        {
            if(df == null)
            {
                df = new debugFORM(this.bgd);
            }
            if (df.Visible == false)
            {
                df.Show();
            }


        }
    }
}
