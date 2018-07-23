using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB_CTR.nsBrain;
using SRB_CTR.nsBrain.Node_Test001;

namespace SRB_CTR.nsBrain
{
    class Node
    {
        object lock_access_queue = new object();
        public Queue<Access> access_queue = new Queue<Access>();

  
         public byte Addr
         {
             get { return baseClu.addr; }
         }
         public string Name
         {
             get { return baseClu.name; }
         }
         public override string ToString()
         {
             return String.Format("Node<{0}@{1}>", Name, Addr.ToHexSt());
         }
         protected Cluster[] clusters = new Cluster[128];
         private Cluster_base.Clu baseClu;
         private Cluster_info.Clu infoClu;
         private Cluster_error.Clu errorClu;
        public List<Access> accessesL= new List<Access>();
        public Node(byte a)
        {
            baseClu = new Cluster_base.Clu(0, this, a);
            infoClu = new Cluster_info.Clu(1, this);
            errorClu = new Cluster_error.Clu(2, this);
            clusters[baseClu.clustr_ID] = baseClu;
            clusters[infoClu.clustr_ID] = infoClu;
            clusters[errorClu.clustr_ID] = errorClu;
            baseClu.read();
        }
        public void accessDone(Access ac)
        {
            switch (ac.Port)
            {
                case Access.ePort.D0:
                    d0AccessDone(ac);
                    break;
                case Access.ePort.D1:
                    d1AccessDone(ac);
                    break;
                case Access.ePort.D2:
                    d2AccessDone(ac);
                    break;
                case Access.ePort.D3:
                    d3AccessDone(ac);
                    break;
                case Access.ePort.Cgf:
                    cfgAccessDone(ac);
                    break;
            }
        }
        protected void cfgAccessDone(Access ac)
        {
            int clusterID = ac.Send_data[0];
            if (clusters[clusterID] != null)
            {
                if (ac.Recv_data == null)
                {
                    return;
                }
                if (ac.Recv_data.Length == 0)
                {
                    clusters[clusterID].writeRecv(ac);
                }
                else
                {
                    clusters[clusterID].readRecv(ac);
                }
            }
        }
        protected virtual void d0AccessDone(Access ac)
        {

        }
        protected virtual void d1AccessDone(Access ac)
        {

        }
        protected virtual void d2AccessDone(Access ac)
        {

        }
        protected virtual void d3AccessDone(Access ac)
        {

        }
        public virtual Access[] bulidUp()
        {
            Access[] a;
            lock(lock_access_queue)
            {
                a = access_queue.ToArray();
                access_queue.Clear();
            }
            return a;
        }
        Node_form nf;
        public void b_Click(object sender, EventArgs e)
        {
            if (nf == null)
            {
                nf = new Node_form(this);
            }

            nf.ShowAt((System.Windows.Forms.Control)sender);
            
        }
        public void clearNodeForm()
        {
            foreach (Cluster c in clusters)
            {
                if (c == null) continue;
            }
            nf = null;
        }
        public string[] getClusterTable()
        {
            string[] st_a = new string[128];
            for(int i = 0 ; i<128;i++)
            {
                if (clusters[i] != null)
                    st_a[i] = clusters[i].ToString();
            }
            return st_a;
        }


        internal virtual System.Windows.Forms.Control getClusterControl()
        {
            return null;
        }


        internal System.Windows.Forms.Control getClusterControl(int i)
        {
            if ((i >= 0) && (i < 128))
            {
                if(clusters[i] == null)
                {
                    throw new Exception(string.Format(
                        "发生了非预期的错误，节点{0}的{1}簇申请控件，但是这个簇不存在或者已经被销毁了。",
                       this.Addr.ToHexSt(), i.AsByteToHexSt()));
                }
                return this.clusters[i].createControl();
            }
            else
            {
                throw new Exception(string.Format(
                    "节点{0}的{1}簇申请控件，{1}不是合法的索引。",
                   this.Addr.ToHexSt(), i.AsByteToHexSt()));
            }
        }
    }
}
