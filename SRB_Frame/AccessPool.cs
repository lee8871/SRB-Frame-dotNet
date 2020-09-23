using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame
{    public class AccessPool
    {
        object locker;
        Queue<Access> idle_node = new Queue<Access>();
        Queue<Access> all_node = new Queue<Access>();
        public Access request()
        {
            Access a;
            lock (locker)
            {
                if (idle_node.Count == 0)
                {
                    a = new Access(this);
                    all_node.Enqueue(a);

                }
                else
                {
                    a = idle_node.Dequeue();
                }
            }
            return a;
        }
        public void free(Access a)
        {
            lock (locker)
            {
                a.init();
                idle_node.Enqueue(a);
            }
        }

    }
}
