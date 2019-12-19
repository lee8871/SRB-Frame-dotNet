using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRB.Frame
{
    public partial class UpdateForm : UserControl
    {
        BaseNode node;
        public UpdateForm()
        {
            InitializeComponent();
        }
        public UpdateForm(BaseNode n)
        {
            InitializeComponent();
            node = n;
            file = new Updater();
        }

        Updater upd;
        private void HoldBTN_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];
            data[0] = 5;
            while (true)
            {
                Access ac = new Access(this, node, Access.PortEnum.Udp, data);
                infoRTB.Text += ("h");
                node.Bus.singleAccess(ac);
                if(is_hold==true)
                {
                    break;
                }
            }


        }



        private void UpdateBTN_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];
            data[0] = 4;
            Access ac = new Access(this, node, Access.PortEnum.Udp, data);
            waitToIdle();
            infoRTB.Text += ("E: ");
            node.Bus.singleAccess(ac);


            file = new Updater();
            int counter = 0;
            int length = file.acccesses.Count;
            while (file.acccesses.Count!=0)
            {
                ac = new Access(this, node, Access.PortEnum.Udp, file.acccesses.Dequeue());
                waitToIdle();
                infoRTB.Text += (string.Format("W{0}/{1}:", counter, length));
                node.Bus.singleAccess(ac);
                counter++;
            }

        }

        private void RunBTN_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];
            data[0] = 3;
            Access ac = new Access(this, node, Access.PortEnum.Udp, data);
            waitToIdle();
            infoRTB.Text += ("R:");
            node.Bus.singleAccess(ac);

        }


        private void waitToIdle()
        {
            while (true)
            {
                byte[] data = new byte[1];
                data[0] = 5;
                Access ac_check = new Access(this, node, Access.PortEnum.Udp, data);
                infoRTB.Text += ("c");
                node.Bus.singleAccess(ac_check);
                if(ac_check.Status == Access.StatusEnum.RecvedDone) 
                {
                    if ((is_busy == false))
                    {
                        break;
                    }
                }
            }
        }
    }
}
