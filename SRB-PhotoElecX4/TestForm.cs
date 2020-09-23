using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRB.NodeType.PhotoElecX4
{
    public partial class TestForm : Form
    {

        private Interpreter datas;
        public TestForm()
        {
            InitializeComponent();
        }
        public TestForm(Interpreter datas)
        {
            InitializeComponent();
            this.datas = datas;
            tableInit();
        }


        private void tableInit()
        {
            int col = ADCtable.Columns.Add("Phase", "Phase");
            ADCtable.Columns[col].Width = 40;
            ADCtable.Columns[col].ReadOnly = true;
            for (int i = 0; i < datas.Sensor_num; i++)
            {
                col = ADCtable.Columns.Add("S" + i, "S" + i);
                ADCtable.Columns[col].Width = 40;
                ADCtable.Columns[col].ReadOnly = true;
            }
            ADCtable.Rows.Add(16);
        }




    }
}
