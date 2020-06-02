using System;
using System.Windows.Forms;
namespace SRB.Frame.untyped
{
    internal partial class Ctrl : INodeControl
    {
        private Interpreter datas;
        public Ctrl(Node n) :
            base(n)
        {
            datas = (Interpreter)n.Datas; ;
            InitializeComponent();
            n.eDataAccessRecv += Node_eDataAccessRecv;
        }

        private void Node_eDataAccessRecv(object sender, AccessEventArgs e)
        {
            recvRTB.Text = e.ac.Recv_data.ToArrayString();
            e.Handled = true;
        }

    }
}
