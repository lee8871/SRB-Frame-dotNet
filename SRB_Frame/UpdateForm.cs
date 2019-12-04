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
    public partial class UpdateForm : UserControl, IAccesser
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
            file = new UpdateFile();
        }

        bool is_hold = false;
        bool is_code_good = false;
        bool is_busy = false;
        bool decrypt_busy = false;



        public void accessDone(Access ac)
        {
            if (ac.Port != Access.PortEnum.Udp)
            {
                throw new Exception("Update type should Udp,but get " + ac.Port.ToString());
            }
            if (ac.Recv_data == null)
            {
                throw new Exception("cfg_receive a null recv_data");
            }
            if (!((ac.Recv_error) || (ac.Recv_busy)))
            {
                string st = "(";
                if (ac.Recv_data.Length == 1) { 
                    byte bl_flag = ac.Recv_data[0];
                    is_hold = ((bl_flag & (1 << 0)) != 0);
                    is_code_good = ((bl_flag & (1 << 1)) != 0);
                    is_busy = ((bl_flag & (1 << 2)) != 0);
                    decrypt_busy = ((bl_flag & (1 << 3)) != 0);
                    if (is_hold)  st += "Hold ";
                    if (is_busy) st += "Busy ";
                    st += ")  ";

                    infoRTB.Text+=(st);
                    return;
                }
            }
            infoRTB.Text += (" fail\n");
            is_hold = false;
            is_code_good = false;
            is_busy = false;
            decrypt_busy = false;
        }
        UpdateFile file;
        private void HoldBTN_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];
            data[0] = 5;
            while (true)
            {
                Access ac = new Access(this, node, Access.PortEnum.Udp, data);
                infoRTB.Text += ("h");
                node.singleAccess(ac);
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
            node.singleAccess(ac);


            file = new UpdateFile();
            int counter = 0;
            int length = file.acccesses.Count;
            while (file.acccesses.Count!=0)
            {
                ac = new Access(this, node, Access.PortEnum.Udp, file.acccesses.Dequeue());
                waitToIdle();
                infoRTB.Text += (string.Format("W{0}/{1}:", counter, length));
                node.singleAccess(ac);
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
            node.singleAccess(ac);

        }


        private void waitToIdle()
        {
            while (true)
            {
                byte[] data = new byte[1];
                data[0] = 5;
                Access ac_check = new Access(this, node, Access.PortEnum.Udp, data);
                infoRTB.Text += ("c");
                node.singleAccess(ac_check);
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
