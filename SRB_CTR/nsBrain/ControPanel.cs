using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.nsBrain
{
     partial class ControPanel : Form
    {
        brain parent;
        private bool mouse_in_handle0;
        private bool speed_lock;
        public ControPanel(brain pa)
        {
            parent = pa;
            InitializeComponent();
            handleBTN.MouseEnter += new EventHandler(handleBTN_MouseEnter);
            handleBTN.MouseLeave += new EventHandler(handleBTN_MouseLeave);
            unlockBTN_Click(this,null);
        }

        void handleBTN_MouseLeave(object sender, EventArgs e)
        {
            mouse_in_handle0 = false;
        }

        void handleBTN_MouseEnter(object sender, EventArgs e)
        {
            mouse_in_handle0 = true;
        }
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            if ((mouse_in_handle0)&(Control.MouseButtons == System.Windows.Forms.MouseButtons.Left))
            {
                Point moues = this.PointToClient(Control.MousePosition);
                int x= moues.X - handleBTN.Location.X - (handleBTN.Size.Width / 2);
                int y= moues.Y - handleBTN.Location.Y - (handleBTN.Size.Height / 2);
                Motor_x = (x + y) * 2;
                Motor_y = (x - y) * 2; 
                this.handleBTN.Text = Motor_x.ToString() + " × " + Motor_y.ToString();
            }
            else
            {
                if (speed_lock == false)
                {
                    Motor_x = 0;
                    Motor_y = 0;
                    this.handleBTN.Text = Motor_x.ToString() + " × " + Motor_y.ToString();
                }
            }
        }

        private void lockBTN_Click(object sender, EventArgs e)
        {
            this.unlockBTN.Visible = 
                this.speed_lock = !(
                this.lockBTN.Visible = false);
        }

        private void unlockBTN_Click(object sender, EventArgs e)
        {
            this.unlockBTN.Visible =
                this.speed_lock = !(
                this.lockBTN.Visible = true);
        }

        public int Motor_x { get; set; }

        public int Motor_y { get; set; }
    }
}
