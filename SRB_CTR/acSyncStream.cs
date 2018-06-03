using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRB_CTR
{
    abstract class acSyncStream<recv>
    {
        private acSyncStream<recv> that = null;
        public acSyncStream()
        {
        }        
        abstract public int receive(recv data);
        public void connectTo(acSyncStream<recv> that)
        {
            this.that = that;
            that.that = this;
        }
        public void send(recv data)
        {
            that.receive(data);
        }


    }
}
