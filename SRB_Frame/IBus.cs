using System.Collections;
using System.Collections.Generic;

namespace SRB.Frame
{
    public abstract partial class IBus : IEnumerable<BaseNode>
    {

        public delegate void dAddNode(IBus bus, BaseNode n);
        public event dAddNode eNodeAdd;


        protected string name = "Undefined";
        protected string type = "Uninitialized";

        List<BaseNode> node_list = new List<BaseNode>();
        public string Name => name;
        public string Type => type;
        BaseNode temp_node = null;

        public BaseNode createTempNode(byte address)
        {
            if(temp_node != null)
            {
                temp_node.Dispose();
            }
            temp_node = new BaseNode(address,this);
            return temp_node;
;        }
        public BaseNode addTempNode()
        {
            if (temp_node == null)
            {
                throw new System.Exception("你没有事先 createTempNode 就调用了addTempNode，此时临时节点是不存在的.请修改程序。");
            }
            BaseNode n = temp_node;
            temp_node = null;
            node_list.Add(n);
            if (eNodeAdd != null)
            {
                eNodeAdd.Invoke(this, n);
            }
            return n;
        }
        public BaseNode createNode(byte address)
        {
            BaseNode n = new BaseNode(address, this);
            node_list.Add(n);
            if (eNodeAdd != null)
            {
                eNodeAdd.Invoke(this, n);
            }
            return n;
        }
        public void removeNode(BaseNode n)
        {
            node_list.Remove(n);
            n.Dispose();
        }
        public void removeAllNode()
        {
            foreach(var n in node_list)
            {
                n.Dispose();
            }
            node_list.Clear();
        }

        public void removeAllUpdateNode()
        {
            for (int i = node_list.Count - 1; i >= 0; i--)
            {
                if (node_list[i].Is_in_update)
                {
                    node_list[i].Dispose();
                    node_list.RemoveAt(i);
                }
            }
        }
        public void checkNodes()
        {

        }

        public IEnumerator<BaseNode> GetEnumerator()
        {
            return ((IEnumerable<BaseNode>)node_list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<BaseNode>)node_list).GetEnumerator();
        }

        public int Count { get => node_list.Count; }


        public BaseNode this[byte addr]
        {
            get
            {
                foreach (BaseNode n in node_list)
                {
                    if (n.Addr == addr) { return n; }
                }
                return null;
            }
        }
        public BaseNode this[int addr]
        {
            get
            {
                foreach (BaseNode n in node_list)
                {
                    if (n.Addr == addr) { return n; }
                }
                return null;
            }
        }
        public BaseNode this[string name]
        {
            get
            {
                foreach (BaseNode n in node_list)
                {
                    if (n.Name == name) { return n; }
                }
                return null;
            }
        }
    };


    public abstract partial class IBus {

        private ISRB_Record record;
        public ISRB_Record Record { get => record; set => record = value; }
        private object lock_access_queue = new object();
        private Queue<Access> access_queue = new Queue<Access>();
        public void addAccess(Access ac)
        {
            lock (lock_access_queue)
            {
                access_queue.Enqueue(ac);
            }
        }
        public void singleAccess(Access ac)
        {
            doAccess(ac);
            ac.onAccessDone();
            record.addAccess(ac);
        }
        public void sendAccess()
        {
            Access[] acs;
            lock (lock_access_queue)
            {
                acs = access_queue.ToArray();
                access_queue.Clear();
            }
            doAccess(acs, acs.Length);
            foreach (Access ac in acs)
            {
                ac.onAccessDone();
                record.addAccess(ac);
            }
        }
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

