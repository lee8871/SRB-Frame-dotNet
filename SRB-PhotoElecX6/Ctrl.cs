using SRB.Frame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace SRB.NodeType.PhotoElecX6
{
    internal partial class Ctrl : INodeControl
    {
        private Interpreter datas;
        BaseNode node;
        public Ctrl(BaseNode n) :
            base(n)
        {
            datas = (Interpreter)n.Datas;
            InitializeComponent();
            n.eBankChangeByAccess += N_eBankChangeByAccess;
            node = n;
            tableInit();
        }

        private void tableInit()
        {
            for (int i = 0; i < 6; i++) {
                
               int col=ADCtable.Columns.Add("S" + i, "S" + i);

                ADCtable.Columns[col].Width = 40;
                ADCtable.Columns[col].ReadOnly = true;

            }
        }

        private void N_eBankChangeByAccess(object sender, EventArgs e)
        {
            for (int i = 0; i<6; i++)
            {
                if (datas.value(i) != -1)
                {
                    ADCtable[i, 0].Value = datas.value(i);
                }

            }
        }

        protected override void OnAccess()
        {
            base.OnAccess();
        }
    }

}
