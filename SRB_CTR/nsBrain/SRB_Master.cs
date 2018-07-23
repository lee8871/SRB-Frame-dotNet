using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SRB_CTR.nsBrain
{
    public abstract class SRB_Master
    {
        public abstract bool Is_opened();
        public abstract bool doAccess(Access[] acs, int n);
        public virtual System.Windows.Forms.Form getConfigForm()
        {
            return null;
        }
    }
}

