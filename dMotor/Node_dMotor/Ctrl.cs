using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SRB_CTR.SRB_Frame;
namespace SRB_CTR.nsBrain.Node_dMotor
{
    partial class Ctrl : UserControl
    {
        Cn node;
        public Ctrl(Cn n)
        {
            node = n;
            InitializeComponent();
            handleBTN.MouseEnter += new EventHandler(handleBTN_MouseEnter);
            handleBTN.MouseLeave += new EventHandler(handleBTN_MouseLeave);
        }

        void handleBTN_MouseLeave(object sender, EventArgs e)
        {
            sendTimer.Stop();
        }

        void handleBTN_MouseEnter(object sender, EventArgs e)
        {
            sendTimer.Start();
        }

        private void sendTimer_Tick(object sender, EventArgs e)
        {
            if (Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {
                Point moues = this.PointToClient(Control.MousePosition);
                int x = moues.X - handleBTN.Location.X - (handleBTN.Size.Width / 2);
                int y = moues.Y - handleBTN.Location.Y - (handleBTN.Size.Height / 2);
                node.Speed_a = (x + y) ;
                node.Speed_b = (x - y);
                this.handleBTN.Text = "双电机控制\n"+ (x + y) + " × " + (x - y) ;
                node.bulidUpD0();
                node.Parent.sendAccess();
            }
        }

        public int Motor_x { get; set; }
        public int Motor_y { get; set; }

        private void handleBTN_Click(object sender, EventArgs e)
        {

        }
    }
}
