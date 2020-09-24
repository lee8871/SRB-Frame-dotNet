using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame
{
    public partial class Node
    {
        public interface ISpecializer
        {
            int specializeNode(Node n);

        }
        abstract public class INodeInterpreter: INodeControlOwner
        {
            private Node node;
            protected IBus Bus { get => node.bus; }
            protected Node Node => node;
            protected ICluster[] clusters => node.clusters;
            protected ByteBank bank => node.bank;
            public virtual string Describe => @"This node is in unknow type. It may not read the type information, or information is not in this frame.";
            public virtual string Help_net_work => "";

            public INodeInterpreter(Node parent)
            {
                this.node = parent;
            }
            public void addDataAccess(int port, bool is_send_at_once = false, int sent_len = -1)
            {
                if (is_send_at_once)
                {
                    node.bus.singleAccess(node.buildAccess(port, sent_len));
                }
                else
                {
                    node.bus.addAccess(node.buildAccess(port, sent_len));
                }
            }
            public void setPd(SRB.Frame.PerformanceDetector.PerformanceDetector pd)
            {
                node.setPd(pd);
            }
            public void addDataAccess_pd(int port, bool is_send_at_once = false, int sent_len = -1 )
            {
                if (is_send_at_once)
                {
                    node.bus.singleAccess(node.buildAccess_pd(port, sent_len));
                }
                else
                {
                    node.bus.addAccess(node.buildAccess(port, sent_len));
                }
            }
            public override string ToString()
            {
                return "Node Function";
            }

        }
    }
}
