﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SRB_CTR.nsFrame
{
    abstract class SRB_Master
    {
        public abstract bool Is_opened();
        public abstract bool doAccess(Access[] acs, int n = -1 );
        public abstract bool doAccess(Access acs);
        public virtual System.Windows.Forms.Control getConfigControl()
        {
            return null;
        }
    }
}

