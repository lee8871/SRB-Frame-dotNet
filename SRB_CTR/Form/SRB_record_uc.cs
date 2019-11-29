using System.Windows.Forms;

namespace SRB_CTR
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
