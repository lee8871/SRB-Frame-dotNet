using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRB_CTR
{
    public partial class recordForm : Form
    {
        public recordForm()
        {
            InitializeComponent();
        }        
        public void appendInfo(string st)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { appendInfo(st); }));
            }
            else
            {
                if (st == null)
                {
                    infoRTC.Clear();
                }
                else
                {
                    infoRTC.AppendText(st);
                }
            }
        }
    }


}
