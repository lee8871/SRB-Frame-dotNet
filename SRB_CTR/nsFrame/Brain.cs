using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRB_CTR.nsFrame
{
    class Brain
    {
        public frame parent;
        public Brain(frame f)
        {
            parent = f;
        }
        public virtual void calculate()
        {

        }


    }
}
