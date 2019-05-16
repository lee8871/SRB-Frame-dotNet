using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRB.Frame;

namespace SRB_CTR
{
    internal partial class AccessDisplayer : Form
    {
        const int ac_num_in_page = 200;
        SrbFrame parent;
        public AccessDisplayer(SrbFrame pa)
        {
            parent = pa;
            InitializeComponent();
            mainDGV.SelectionChanged += new EventHandler(mainDGV_SelectionChanged);
            pageTB.TextChanged += new EventHandler(pageTB_TextChanged);

        }

        private List<DataGridViewRow> dgvr_list = new List<DataGridViewRow>();
        private List<DataGridViewRow[]> dgvr_array_list = new List<DataGridViewRow[]>();

        DataGridViewRow[] last_dgvr_array;

        private int dgvr_array_counter = ac_num_in_page;
        public void addAccess(Access ac)
        {
            if (dgvr_array_counter >= ac_num_in_page)
            {
                dgvr_array_list.Add(last_dgvr_array = new DataGridViewRow[ac_num_in_page]);
                dgvr_array_counter = 0;
            }
            last_dgvr_array[dgvr_array_counter++] = createDGVRow(ac);
        }
        private int current_page = 0;


        //delegate void dAddAccess(Access[] acs, int length);
        public void addAccess(Access[] acs, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Access ac = acs[i];
                addAccess(ac);
            }
        }

        void mainDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (mainDGV.SelectedRows.Count != 0)
            {
                Access ac = (Access)(mainDGV.SelectedRows[0].Tag);
                if (ac != null)
                {
                    this.mainRTC.Clear();
                    this.mainRTC.SelectionColor = Color.Navy;
                    //this.mainRTC.AppendText(ac.original_SendByte.ToHexSt() + "\r\n");
                    this.mainRTC.SelectionColor = Color.DarkSalmon;
                   // this.mainRTC.AppendText(ac.original_RecvByte.ToHexSt() + "\r\n");
                }
            }
        }

        private int last_current_page = 0;

        private void main_dgv_updateT_Tick(object sender, EventArgs e)
        {
            this.pageNum.Text = dgvr_array_list.Count.ToString();
            if (dgvr_array_list.Count == 0)
            {
                return;
            }
            if (last_current_page != current_page)
            {
                mainDGV.Rows.Clear();
                last_current_page = current_page;
            }
            else
            {
                if(mainDGV.Rows.Count<ac_num_in_page)
                {
                    Queue<DataGridViewRow> dgvr_queue = new Queue<DataGridViewRow>();
                    for (int i = mainDGV.Rows.Count; i < ac_num_in_page; i++)
                    {
                        if (null == dgvr_array_list[current_page][i])
                        {
                            break;
                        }
                        dgvr_queue.Enqueue(dgvr_array_list[current_page][i]);
                    }
                    mainDGV.Rows.AddRange(dgvr_queue.ToArray());
                }
            }
        }

        private DataGridViewRow createDGVRow(Access ac)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(mainDGV,new object[]{
                ac.sendTime.ToString("hh:mm:ss"),
                ac.Status,
                ac.Addr.ToString() + '.' + ac.Port.ToString(),
                ac.Send_data.ToHexSt(),
                ac.Recv_data.ToHexSt(),
            });
            row.Tag = ac;
            return row;
        }


        void pageTB_TextChanged(object sender, EventArgs e)
        {
            if (pageTB.Text == "new")
            {
                current_page = -1;

            }
            if (int.TryParse(pageTB.Text, out current_page))
            {
                if (current_page < 0)
                {
                    current_page = 0;
                }
                if (current_page >= dgvr_array_list.Count)
                {
                    current_page = dgvr_array_list.Count - 1;
                }
            }
        }

        private void rightBTN_Click(object sender, EventArgs e)
        {
            current_page += 1;
            if (current_page >= dgvr_array_list.Count)
            {
                current_page = dgvr_array_list.Count - 1;
            }
            pageTB.Text = current_page.ToString();
        }

        private void fastRightBTN_Click(object sender, EventArgs e)
        {
            current_page += 10;
            if (current_page >= dgvr_array_list.Count)
            {
                current_page = dgvr_array_list.Count - 1;
            }
            pageTB.Text = current_page.ToString();
        }

        private void leftBTN_Click(object sender, EventArgs e)
        {
            current_page -= 1;
            if (current_page < 0)
            {
                current_page = 0;
            }
            pageTB.Text = current_page.ToString();

        }

        private void fastLeftBTN_Click(object sender, EventArgs e)
        {
            current_page -= 10;
            if (current_page < 0)
            {
                current_page = 0;
            }
            pageTB.Text = current_page.ToString();
        }


        protected override void OnClosed(EventArgs e)
        {
            //parent.closeAccessDisplayer();
            base.OnClosed(e);
        }
    }
}
