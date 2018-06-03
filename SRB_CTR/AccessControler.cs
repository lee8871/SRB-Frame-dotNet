using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRB_access
{
    public class AccessControler
    {
        private Queue<Access> qaWaitRecv;
        private Queue<Access> qaAccessDone;
        
        public void beginAccess(Access a)
        {
            qaWaitRecv.Enqueue(a);            
        }
    }
}
