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
    partial class NodesForm : Form
    {
        brain parent;
        public NodesForm(brain pa )
        {
            InitializeComponent();
            parent = pa;
            foreach (Node n in parent.Nodes)
            {
                if (n == null) continue;
                Button b = new Button();
                b.Text = "Node\n" + n.Addr.ToHexSt();
                b.BackColor = Color.LightSkyBlue;
                b.Size = new Size(48, 48);
                b.Click += new EventHandler(n.b_Click);
                this.nodesTable.Controls.Add(b);
            }
            setPortState();
            brainRunStateUpdate();
            cycleTB.LostFocus += new EventHandler(cycleSet);
            brainRunBTN.Click+=new EventHandler(brainRunBTN_Click);
            brainStopBTN.Click+=new EventHandler(brainStopBTN_Click);
            cycleSet(this, null);
        }


        #region brain config
        void cycleSet(object sender, EventArgs e)
        {
            int cycle;
            if (false == int.TryParse(cycleTB.Text, out cycle))
            {
                cycleTB.Text = cycle.ToString();
            }
            else
            {
                this.parent.Cycle_in_ms = cycle;
            }
        }
        void  brainStopBTN_Click(object sender, EventArgs e)
        {
            parent.stopCalculation(); brainRunStateUpdate();
        }

        void  brainRunBTN_Click(object sender, EventArgs e)
        {
            parent.runCalculation(); brainRunStateUpdate();
        }
        void brainRunStateUpdate()
        {
            if (parent.Is_calculation_running)
            {
                brainStopBTN.Visible = 
                !(brainRunBTN.Visible = false);
            }
            else
            {
                brainStopBTN.Visible =
                !(brainRunBTN.Visible = true);
            }
        }



        #endregion




        public double us_calculation = 0 , us_communation = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.SpeedValue.Text = string.Format("运算耗时：{0}us，发包耗时：{1}us", us_calculation.ToString("f2"), us_communation.ToString("f2"));
            setPortState();
        
        }

        private void setPortState() 
        {
            if (parent.Is_port_opend)
            {
                this.MasterBroadConfigBTN_c.Visible = true;
                this.MasterBroadConfigBTN_uc.Visible = false;
            }
            else
            {
                this.MasterBroadConfigBTN_c.Visible = false;
                this.MasterBroadConfigBTN_uc.Visible = true;
            }
        }

        private void MasterBroadConfigBTN_Click(object sender, EventArgs e)
        {
            Form uc = parent.getuartConfigForm();
            uc.Show(this);
        }



        private void ShowAccessBTN_Click(object sender, EventArgs e)
        {
            parent.getAccessDisplayer().Show(this);
        }


    }
}
