using SRB.Frame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace SRB.NodeType.PhotoElecX4
{
    public partial class Ctrl : INodeControl
    {

        private Interpreter datas;
        Node node;
        public Ctrl(Node n) :
            base(n)
        {
            datas = (Interpreter)n.Datas;
            InitializeComponent();
            node = n;
            node.eBankChangeByAccess += N_eBankChangeByAccess;
            tableInit();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            node.eBankChangeByAccess -= N_eBankChangeByAccess;
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void tableInit()
        {
            int col = ADCtable.Columns.Add("Phase", "Phase");
            ADCtable.Columns[col].Width = 40;
            ADCtable.Columns[col].ReadOnly = true;
            for (int i = 0; i < datas.Sensor_num; i++) 
            {              
                col=ADCtable.Columns.Add("S" + i, "S" + i);
                ADCtable.Columns[col].Width = 40;
                ADCtable.Columns[col].ReadOnly = true;
            }
            ADCtable.Rows.Add(16);
        }
        private void N_eBankChangeByAccess(object sender, EventArgs e)
        {
            int row = datas.phase;
            if (row == 255)
            {
                row = 16;
            }
            ADCtable[0, row].Value = datas.phase;
            ADCtable.Rows[row].HeaderCell.Value = datas.phase;
            for (int i = 0; i<datas.Sensor_num; i++)
            {
                if (datas.value(i) != -1)
                {
                    ADCtable[i+1, row].Value = datas.value(i);
                }
            }
        }
        protected override void OnAccess()
        {
            base.OnAccess();
        }

        private void DebugBTN_Click(object sender, EventArgs e)
        {
            Form f = new TestForm();
            f.Show();
        }
    }

}
