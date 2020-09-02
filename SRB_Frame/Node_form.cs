using System;
using System.Drawing;
using System.Windows.Forms;

namespace SRB.Frame
{
    public partial class NodeForm : Form
    {
        private Node node;
        public NodeForm(Node n)
        {
            InitializeComponent();
            components = new System.ComponentModel.Container();
            this.clusters.BackColor = support.Color_BackGround;
            node = n;
            Node_eUpdateModeChanging(this, null);
            node.eUpdateModeChanging += Node_eUpdateModeChanging;
            Console.WriteLine($"create node{node.Addr}'s Form");
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            System.Console.WriteLine($"Dispose node{node.Addr}'s Form");
            node.eUpdateModeChanging -= Node_eUpdateModeChanging;
            node = null;
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void Node_eUpdateModeChanging(object sender, EventArgs e)
        {
            if (this.IsDisposed)
            {
                throw new Exception("classis disposed!");
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { Node_eUpdateModeChanging(sender,e); }));
                return;
            }
            foreach (var c in this.clusters.Controls)
            {
                GroupBox b = c as GroupBox;
                if (b.Controls.Count != 0)
                {
                    b.Controls[0].Dispose();
                }
            }
            this.clusters.Controls.Clear();
            if (node.Is_in_update)
            {
                initUpdate();
            }
            else
            {
                initNormal();
            }
        }

        public void initUpdate()
        {
            updateText();
            GroupBox b;
            b = new GroupBox();
            components.Add(b);
            b.Tag = node.Updater;
            b.Text = node.Updater.ToString();
            b.Size = b.MinimumSize = new Size(300, 18);
            b.MaximumSize = new Size(300, 300);
            b.Click += new EventHandler(b_Click);
            b.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.clusters.Controls.Add(b);
        }
        public void initNormal() 
        {
            updateText();
            GroupBox b;
            b = new GroupBox();
            components.Add(b);
            b.Tag = node.Datas;
            b.Text = node.Datas.ToString();
            b.Size = b.MinimumSize = new Size(300, 18);
            b.MaximumSize = new Size(300, 300);
            b.Click += new EventHandler(b_Click);
            b.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.clusters.Controls.Add(b);
            for (int i = 0; i < 128; i++)
            {
                Node.INodeControlOwner cluster = node.getClusters(i);
                if (cluster != null)
                {
                    if (cluster.is_follower)
                    {
                        b = new GroupBox();
                        components.Add(b);
                        b.Tag = cluster;
                        b.Text = cluster.ToString();
                        b.Size = b.MinimumSize = new Size(300, 18);
                        b.MaximumSize = new Size(300, 300);
                        b.Click += new EventHandler(b_Click);
                        b.BackColor = Color.FromKnownColor(KnownColor.Control);
                        this.clusters.Controls.Add(b);
                    }
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
                c = (b.Tag as Node.INodeControlOwner).getControl();
                components.Add(c);
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
        public void close(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler d = new EventHandler(close);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.Close();
            }
        }

        public void showAt(Control reference)
        {
            this.Location = LocationOnClient(reference);
            this.Show();
            this.Focus();
        }
        private Point LocationOnClient(Control c)
        {
            Point retval = new Point(0, 0);
            retval.Offset(new Point(c.Size.Width / 2, c.Size.Height / 2 + 20));
            do
            {
                retval.Offset(c.Location);
                c = c.Parent;
            }
            while (c != null);
            return retval;
        }
    }
}
