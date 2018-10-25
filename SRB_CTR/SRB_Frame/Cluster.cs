using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.SRB_Frame
{
    abstract class Cluster
    {
        public byte clustr_ID;
        public abstract void write();
        public void read()
        {
            Access ac = new Access(parent_node, Access.PortEnum.Cgf, new byte[] { clustr_ID });
            parent_node.singleAccess(ac);
        }
        public virtual void readRecv(Access ac)
        {
            OnDataChangded();
        }

        public virtual void writeRecv(Access ac)
        {
            if (ac.Recv_error == false)
            {
                OnDataChangded();
            }
        }

        protected Node parent_node;

        public Node Parent_node { get => parent_node; }

        public Cluster(byte ID,Node n)
        {
            this.clustr_ID = ID;
            this.parent_node = n;
        }
        public void changeParentNode(Node n)
        {
            parent_node = n;
        }
        public abstract UserControl createControl();
        public event EventHandler eDataChanged;
        public void OnDataChangded()
        {
            if(eDataChanged!=null)
            {
                eDataChanged.Invoke(this, new EventArgs());
            }
        }

    }
}
