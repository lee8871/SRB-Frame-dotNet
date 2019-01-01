using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SRB_CTR.SRB_Frame;
namespace SRB_CTR.SRB_Frame
{
    partial class UntypedNodeCtrl : UserControl
    {
        Node node;  
        public UntypedNodeCtrl(Node n)
        {
            node = n;
            InitializeComponent();
            nameL.Text = "Type = \"" +n.NodeType +"\"";
        }

        private void nameL_Click(object sender, EventArgs e)
        {

        }
    }
}
