using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.SRB_Frame
{
    internal partial class SRB_record_uc : UserControl
    {
        private SRB_Record backlogic;
        public SRB_record_uc(SRB_Record backlogic)
        {
            InitializeComponent();
            this.backlogic = backlogic;
        }
    }
        
}
