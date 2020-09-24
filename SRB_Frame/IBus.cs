using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SRB.Frame
{
    public abstract partial class IBus : IEnumerable<Node>
    {
        public delegate void dAddNode(IBus bus, Node n);
        public event dAddNode eNodeAdd;


        string name = "Undefined";
        string type = "Uninitialized";
        public string Name => name;
        public string Type => type;

        List<Node> node_list = new List<Node>();
        Node temp_node = null;

        public Node createTempNode(byte address)
        {
            if (temp_node != null)
            {
                temp_node.Dispose();
            }
            temp_node = new Node(address, this);
            return temp_node;
            ;
        }
        public Node addTempNode()
        {
            if (temp_node == null)
            {
                throw new System.Exception("你没有事先 createTempNode 就调用了addTempNode，此时临时节点是不存在的.请修改程序。");
            }
            Node n = temp_node;
            temp_node = null;
            node_list.Add(n);
            eNodeAdd?.Invoke(this, n);
            return n;
        }
        public Node createNode(byte address)
        {
            Node n = new Node(address, this);
            node_list.Add(n);
            eNodeAdd?.Invoke(this, n);
            return n;
        }
        public void removeNode(Node n)
        {
            node_list.Remove(n);
            n.Dispose();
        }
        public void removeAllNode()
        {
            for (int i = node_list.Count - 1; i >= 0; i--)
            {
                node_list[i].Dispose();
                node_list.RemoveAt(i);
            }
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

        public IEnumerator<Node> GetEnumerator()
        {
            return ((IEnumerable<Node>)node_list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Node>)node_list).GetEnumerator();
        }

        public int Count { get => node_list.Count; }
        public Node this[byte addr]
        {
            get
            {
                foreach (Node n in node_list)
                {
                    if (n.Addr == addr) { return n; }
                }
                return null;
            }
        }
        public Node this[int addr]
        {
            get
            {
                foreach (Node n in node_list)
                {
                    if (n.Addr == addr) { return n; }
                }
                return null;
            }
        }
        public Node this[string name]
        {
            get
            {
                foreach (Node n in node_list)
                {
                    if (n.Name == name) { return n; }
                }
                return null;
            }
        }
    };


    public abstract partial class IBus
    {

        #region thread closing


        public interface IbusUser
        {
            void stopUseBus(IBus bus);
        }
        object bus_user_lock = new object();

        List<IbusUser> user_list = new List<IbusUser>();

        public void addUser(IbusUser user)
        {
            lock (bus_user_lock)
            {
                if (user_list.Contains(user))
                {
                    throw new PerformedException("新加入的用户已经存在于总线用户列表中了。");
                }
                user_list.Add(user);

            }
        }
        public void removeUser(IbusUser user)
        {
            lock (bus_user_lock)
            {
                user_list.Remove(user);
            }
        }

        public void closeAllUser()
        {
            for (int i = user_list.Count - 1; i >= 0; i--)
            {
                IbusUser user = user_list[i];
                user.stopUseBus(this);
            }
            int retry = 2500;
            while (true)
            {
                lock (bus_user_lock)
                {
                    if (user_list.Count == 0)
                    {
                        return;
                    }
                }
                Thread.Sleep(20);
                if (retry-- < 0)
                {
                    string est = string.Format("Bus {0} close all user time out. Unstopable user is :", this.ToString());
                    foreach (IbusUser user in user_list)
                    {
                        est += user.ToString() + "\n";
                    }
                    throw new TimeoutException(est);
                }
            }
        }






        #endregion



    };


    public abstract partial class IBus
    {

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
            ac.free();
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
                ac.free();
            }
        }
        public abstract bool Is_opened { get; }

        protected abstract bool doAccess(Access[] acs, int n = -1);
        protected abstract bool doAccess(Access acs);

        public virtual System.Windows.Forms.Control getConfigControl()
        {
            return null;
        }
        public virtual void checkPort()
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract partial class IBus
    {
        AccessPool access_pool=new AccessPool();
        public Access accessRequest(IAccesser a, Node n, AccessPort p)
        {
            Access access = access_pool.request();
            access.loadAccess(a, n, p);
            return access;
        }
        public Access accessRequest(IAccesser a, Node n, AccessPort p,byte[] b)
        {
            Access access = access_pool.request();
            access.loadAccess(a, n, p);
            access.Send_data.load(b);
            return access;
        }
        public Access accessRequest(IAccesser a, Node n, AccessPort p, byte b)
        {
            Access access = access_pool.request();
            access.loadAccess(a, n, p);
            access.Send_data[0]=b;
            return access;
        }
    }
}

