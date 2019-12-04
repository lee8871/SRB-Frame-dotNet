using System.Collections;
using System.Collections.Generic;

namespace SRB.Frame
{
    public abstract partial class IBus : IEnumerable<BaseNode>
    {
        protected string name = "Undefined";
        protected string type = "Uninitialized";

        Queue<BaseNode> nodesQ = new Queue<BaseNode>();
        public string Name => name;
        public string Type => type;

        public BaseNode addNode(byte address)
        {
            nodesQ.Enqueue(new BaseNode(address, this);

        }
        public void removeNode()
        {

        }

        public IEnumerator<BaseNode> GetEnumerator()
        {
            return ((IEnumerable<BaseNode>)nodesQ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<BaseNode>)nodesQ).GetEnumerator();
        }


        BaseNode this[byte addr]
        {
            get
            {
                foreach (BaseNode n in nodesQ)
                {
                    if (n.Addr == addr) { return n; }
                }
                return null;
            }
        }
        BaseNode this[string name]
        {
            get
            {
                foreach (BaseNode n in nodesQ)
                {
                    if (n.Name == name) { return n; }
                }
                return null;
            }
        }
    };


    public abstract partial class IBus {



        public abstract bool Is_opened { get; }
        public abstract bool doAccess(Access[] acs, int n = -1);
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

