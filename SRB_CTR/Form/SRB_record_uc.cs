using System.Windows.Forms;

namespace SRB_CTR
{
    internal partial class RecordUC : UserControl
    {
        private SRB_Record backlogic;
        public RecordUC(SRB_Record backlogic)
        {
            InitializeComponent();
            this.backlogic = backlogic;
        }
    }

}
