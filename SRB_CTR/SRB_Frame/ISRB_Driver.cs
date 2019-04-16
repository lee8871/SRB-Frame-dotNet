﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using SRB.Frame;

namespace SRB_CTR
{
    abstract class ISRB_Driver
    {
        public abstract bool Is_opened { get; }
        public abstract bool doAccess(Access[] acs, int n = -1 );
        public abstract bool doAccess(Access acs);
        public virtual System.Windows.Forms.Control getConfigControl()
        {
            return null;
        }
        public virtual void checkPort()
        {

        }
    }
}

