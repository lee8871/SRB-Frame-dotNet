using SRB.Frame;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace SRB.NodeType.Du_motor
{
    internal partial class Ctrl : INodeControl
    {
        private Interpreter datas;
        private string Handle_text;
        public Ctrl(Node n) :
            base(n)
        {
            datas = (Interpreter)n.Datas;
            InitializeComponent();
            Handle_text = handleBTN.Text;
        }

        private int x;
        private int y;
        protected override void OnAccess()
        {
            if (Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.handleBTN.Capture)
                {
                    Point moues = this.PointToClient(Control.MousePosition);
                    x = moues.X - handleBTN.Location.X - (handleBTN.Size.Width / 2);
                    y = moues.Y - handleBTN.Location.Y - (handleBTN.Size.Height / 2);
                    datas.Speed_a = (x + y);
                    datas.Speed_b = (x - y);
                    this.handleBTN.Text = Handle_text + "\n" + (x + y) + " × " + (x - y);
                }
                else if (this.StopBTN.Capture)
                {
                    x = 0;
                    y = 0;
                    datas.Speed_a = (x + y);
                    datas.Speed_b = (x - y);
                    this.handleBTN.Text = Handle_text + "\n" + (x + y) + " × " + (x - y);
                }
                else if (this.BrakeBTN.Capture)
                {
                    datas.Brake_a = 10000;
                    datas.Brake_b = 10000;
                    this.handleBTN.Text = Handle_text + "\n" + "Braking";

                }
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
    }
}
