using SRB.Frame;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace SRB.NodeType.Du_motor
{
    internal partial class Ctrl : INodeControl
    {
        private Node node;
        private string Handle_text;
        public Ctrl(Node n) :
            base(n)
        {
            node = n;
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
                    node.Speed_a = (x + y);
                    node.Speed_b = (x - y);
                    this.handleBTN.Text = Handle_text + "\n" + (x + y) + " × " + (x - y);
                }
                else if (this.StopBTN.Capture)
                {
                    x = 0;
                    y = 0;
                    node.Speed_a = (x + y);
                    node.Speed_b = (x - y);
                    this.handleBTN.Text = Handle_text + "\n" + (x + y) + " × " + (x - y);
                }
                else if (this.BrakeBTN.Capture)
                {
                    node.Brake_a = 10000;
                    node.Brake_b = 10000;
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
