using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.SRB_Frame
{
    partial class Node_form : Form
    {
        Node node;
        public Node_form(Node n)
        {
            InitializeComponent();
            node = n;
            updateText();
            string[] st_a = n.getClusterTable();
            GroupBox b;

            b = new GroupBox();
            b.Tag = null;
            b.Text = "Function test";
            b.Size = b.MinimumSize = new Size(300, 18);
            b.MaximumSize = new Size(300, 300);
            b.Click += new EventHandler(b_Click);
            b.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.clusters.Controls.Add(b);

            for (int i =0;i<128;i++)
            {
                if (st_a[i] != null)
                {
                    b = new GroupBox();
                    b.Tag = i;
                    b.Text = st_a[i];
                    b.Size = b.MinimumSize = new Size(300, 18);
                    b.MaximumSize = new Size(300, 300);
                    b.Click +=new EventHandler(b_Click);
                    b.BackColor = Color.FromKnownColor(KnownColor.Control);
                    this.clusters.Controls.Add(b);
                }
            }
        }

        public void updateText()
        {
            this.Text = node.ToString();
        }
        public void b_Click(object sender, EventArgs e)
        {
            GroupBox b = (GroupBox)sender;
            Control c;
            if (b.Controls.Count == 0)
            {
                if (b.Tag != null)
                {
                    c = node.getClusterControl((int)b.Tag);
                }
                else
                {
                    c = node.getClusterControl();
                }
                b.Controls.Add(c);
                c.Dock = DockStyle.Fill;
                c.Enabled = false;
            }
            else 
            {
                c = b.Controls[0];
            }
            if (c.Enabled)
            {
                c.Enabled = false;
                c.Hide();
                b.AutoSize = false;
            }
            else
            {
                c.Enabled = true;
                c.Show();
                b.AutoSize = true;
            }

        }
        public void close()
        {
            EventArgs e = new EventArgs();
            this.OnClosed(e);
        }
        protected override void  OnClosed(EventArgs e)
        {
            node.clearNodeForm();
 	        base.OnClosed(e);
        }
        public void ShowAt(Control reference)
        {
            this.Location = LocationOnClient(reference);
            this.Show();
            this.Focus();
        }
        private Point LocationOnClient(Control c) 
        {
            Point retval = new Point(0, 0);
            retval.Offset(new Point(c.Size.Width / 2, c.Size.Height / 2 + 20));
            do{ 
                retval.Offset(c.Location);
                c = c.Parent;
            } 
            while(c!= null); 
            return retval; 
        }

        private void read_clusterMS_Click(object sender, EventArgs e)
        {

        }
    }
}
