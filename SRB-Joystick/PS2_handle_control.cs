using SRB.Frame;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SRB.NodeType.Joystick
{
    internal partial class PS2HandleControl : INodeControl
    {
        private Interpreter datas;
        private int left_x_base, left_y_base, right_x_base, right_y_base;
        public PS2HandleControl(Node n) :
            base(n)
        {
            datas = (Interpreter)n.Datas;
            InitializeComponent();
            left_x_base = LeftLAB.Location.X;
            left_y_base = LeftLAB.Location.Y;
            right_x_base = RightLAB.Location.X;
            right_y_base = RightLAB.Location.Y;
            n.eBankChangeByAccess += Node_eBankChangeByAccess;
            n.eDataAccessRecv += Node_eDataAccessRecv;
            LeftLAB.Parent = this;
        }
        private void Node_eDataAccessRecv(object sender, Node.AccessEventArgs e)
        {
            datas.rumble_l = 0xff;
            datas.rumble_r = 0xff;
        }


        private void Node_eBankChangeByAccess(object sender, EventArgs e)
        {
            LeftLAB.Location = new Point(left_x_base + datas.joy_lx / 3, left_y_base + datas.joy_ly / 3);
            RightLAB.Location = new Point(right_x_base + datas.joy_rx / 3, right_y_base + datas.joy_ry / 3);
            LeftLAB.Refresh();
            RightLAB.Refresh();

            setColor(select, datas.select);
            setColor(Start, datas.start);

            setColor(left, datas.left);
            setColor(right, datas.right);
            setColor(down, datas.down);
            setColor(up, datas.up);

            setColor(square, datas.square);
            setColor(trag, datas.trag);
            setColor(circle, datas.circle);
            setColor(cross, datas.cross);

            setColor(l1, datas.L1);
            setColor(l2, datas.L2);
            setColor(l3, datas.L3);
            setColor(r1, datas.R1);
            setColor(r2, datas.R2);
            setColor(r3, datas.R3);
        }

        private void Rumble_L_BTN_Click(object sender, EventArgs e)
        {
            datas.rumble_l_strength = 0x50;
            datas.rumble_l = 100;
            if (is_running == false)
            {
                datas.addDataAccess(0);
            }

        }

        private void RumbleBT_R_Click(object sender, EventArgs e)
        {
            datas.rumble_r = 100;
            if (is_running == false)
            {
                datas.addDataAccess(0);
            }
        }

        private int counter = 0;
        private void setColor(Label l, bool keydown)
        {
            if (keydown)
            {
                l.ForeColor = support.Color_red;
            }
            else
            {
                l.ForeColor = support.Color_dank;
            }
        }

    }
}
