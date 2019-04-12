using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRB.Frame;

namespace SRB.NodeType.PS2_Handle
{
    partial class PS2HandleControl : INodeControl
    {
        Node node;
        int left_x_base, left_y_base, right_x_base, right_y_base;
        public PS2HandleControl(Node n):
            base(n)
        {
            node = n;
            InitializeComponent();
            left_x_base = LeftLAB.Location.X;
            left_y_base = LeftLAB.Location.Y;
            right_x_base = RightLAB.Location.X;
            right_y_base = RightLAB.Location.Y;
            node.eBankChangeByAccess += Node_eBankChangeByAccess;
            node.eDataAccessRecv += Node_eDataAccessRecv;


            LeftLAB.Parent = this;
        }
        private void Node_eDataAccessRecv(object sender, BaseNode.AccessEventArgs e)
        {
            node.rumble_l = 0xff;
            node.rumble_r = 0xff;
        }


        private void Node_eBankChangeByAccess(object sender, EventArgs e)
        {
            if (node.handle_exist)
            {
                this.Start.Text = "Start";
            }
            else
            {
                this.Start.Text = "No Handle";
            }
            LeftLAB.Location = new Point(left_x_base + node.joy_lx / 3, left_y_base + node.joy_ly / 3);
            RightLAB.Location = new Point(right_x_base + node.joy_rx / 3, right_y_base + node.joy_ry / 3);
            LeftLAB.Refresh();
            RightLAB.Refresh();

            setColor(select, node.select);
            setColor(Start, node.start);

            setColor(left, node.left);
            setColor(right, node.right);
            setColor(down, node.down);
            setColor(up, node.up);

            setColor(square, node.square);
            setColor(trag, node.trag);
            setColor(circle, node.circle);
            setColor(cross, node.cross);

            setColor(l1, node.L1);
            setColor(l2, node.L2);
            setColor(l3, node.L3);
            setColor(r1, node.R1);
            setColor(r2, node.R2);
            setColor(r3, node.R3);
        }

        private void Rumble_L_BTN_Click(object sender, EventArgs e)
        {
            node.rumble_l_strength = 0x50;
            node.rumble_l = 100;
            if (is_running == false)
            {
                node.singleAccess(0);
            }

        }

        private void RumbleBT_R_Click(object sender, EventArgs e)
        {
            node.rumble_r = 100;
            if (is_running==false)
            {
                node.singleAccess(0);
            }
        }
        int counter = 0;     
        private void setColor(Label l,bool keydown)
        {
            if (keydown)
            {
                l.ForeColor = Support.Color_red;
            }
            else
            {
                l.ForeColor = Support.Color_dank;
            }
        }

    }
}
