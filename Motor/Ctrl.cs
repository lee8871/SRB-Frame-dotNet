using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRB.Frame;
namespace SRB.NodeType.Du_motor
{
    partial class Ctrl : INodeControl
    {
        Cn node;
        public Ctrl(Cn n) :
            base(n)
        {
            node = n;
            InitializeComponent();
        }
        int x;
        int y;
        protected override void OnAccess()
        {
            if ((Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)&&(this.handleBTN.Capture))
            {
                Point moues = this.PointToClient(Control.MousePosition);
                x = moues.X - handleBTN.Location.X - (handleBTN.Size.Width / 2);
                y = moues.Y - handleBTN.Location.Y - (handleBTN.Size.Height / 2);
            }
            else
            {
                if ((x > 20)||(x<-20)) x =( x * 2) / 3;
                else x = 0;
                if ((y > 20) || (y < -20)) y = (y * 2) / 3;
                else y = 0;
            }
            node.Speed_a = (x + y);
            node.Speed_b = (x - y);
            this.handleBTN.Text = "双电机控制\n" + (x + y) + " × " + (x - y);
            base.OnAccess();
        }

        public int Motor_x { get; set; }
        public int Motor_y { get; set; }

        private void handleBTN_Click(object sender, EventArgs e)
        {

        }
    }
}
