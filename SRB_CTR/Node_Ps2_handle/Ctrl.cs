using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SRB_CTR.SRB_Frame;
namespace SRB_CTR.nsBrain.Node_PS2_handle
{
    partial class Ctrl : UserControl
    {
        Cn node;
        int left_x_base, left_y_base, right_x_base, right_y_base;
        public Ctrl(Cn n)
        {
            node = n;
            InitializeComponent();
            left_x_base = LeftLAB.Location.X;
            left_y_base = LeftLAB.Location.Y;
            right_x_base = RightLAB.Location.X;
            right_y_base = RightLAB.Location.Y;


            LeftLAB.Parent = this;
        }

        private void StartBTN_Click(object sender, EventArgs e)
        {
            sendTimer.Start();

        }

        private void Ctrl_Load(object sender, EventArgs e)
        {

        }

        private void StopBTN_Click(object sender, EventArgs e)
        {
            sendTimer.Stop();
        }

        private void RumbleBT_Click(object sender, EventArgs e)
        {
            node.rumble = (int)rumbleNUM.Value;
            rumble_on = true;
        }
        bool rumble_on = false;
        int counter = 0;
        
        private void sendTimer_Tick(object sender, EventArgs e)
        {
            if(rumble_on)
            {
                node.bulidUpD0(1000);
                rumble_on = false;
            }
            else
            {
                node.bulidUpD0();
            }
            if(node.handle_exist)
            {
                this.Start.Text = "Start";
            }
            else
            {
                this.Start.Text = "No Handle";
            }

            //if (node.joy_lx < -120)
            //{
            //    Console.Beep(2000, 100);
            //    MessageBox.Show("x is " + node.joy_lx);
            //}
            //if (node.joy_ly < -120)
            //{
            //    Console.Beep(2000, 100);
            //    MessageBox.Show("y is " + node.joy_ly);
            //}

            node.Parent.sendAccess();
            LeftLAB.Location = new Point(left_x_base + node.joy_lx/3, left_y_base + node.joy_ly / 3);
            RightLAB.Location = new Point(right_x_base + node.joy_rx / 3, right_y_base + node.joy_ry / 3);
            LeftLAB.Refresh();
            RightLAB.Refresh();
            StartBTN.Text = counter++.ToString();

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
