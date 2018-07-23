using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.nsBrain.Node_Test001
{
    partial class Ctrl : UserControl
    {
        cn node;  
        public Ctrl(cn n)
        {
            node = n;
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ColorLed.ForeColor = node.color_now;
        }
    }
}
