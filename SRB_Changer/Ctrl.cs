using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRB.Frame;

namespace SRB.NodeType.Charger
{
    partial class Ctrl : UserControl
    {
        Node node;

        public Ctrl(Node n)
        {
            node = n;
            InitializeComponent();
        }


    }
}
