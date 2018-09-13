using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB_CTR.nsFrame;

namespace SRB_CTR.nsFrame
{
    class Node
    {
        frame parent = null;

        internal frame Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public object Tag;
        bool is_hardware_exist = false;
        int access_fail_counter = 0;
        protected bool Can_post_access
        {
            get{ return (parent!=null);}
        }
        public byte Addr
        {
            get { return baseClu.addr; }
        }
        public string Name
        {
            get { return baseClu.name; }
        }
        public string NodeType
        {
            get { return this.infoClu.type; }
        }
        protected Cluster[] clusters = new Cluster[128];

        private Cluster_base.Clu baseClu;
        private Cluster_info.Clu infoClu;
        private Cluster_error.Clu errorClu;

        public SRB_CTR.nsFrame.frame.eNodeChange eAddr_change;
        public SRB_CTR.nsFrame.frame.eNodeChange eDescription_change;

        virtual public void onAddrChanged()
        {
            this.parent.nodeAddrChange(this);
            if (nf != null)
            {
                nf.updateText();
            }
            if (eAddr_change != null)
            {
                eAddr_change.Invoke(this);
            }
            onDescriptionChanged();
        }
        virtual public void onDescriptionChanged()
        {
            this.parent.nodeDescriptionChange(this);
            if (nf != null)
            {
                nf.updateText();
            }
            if(eDescription_change != null)
            {
                eDescription_change.Invoke(this);
            }
        }
        #region base method
        public override string ToString()
        {
            return String.Format("{0}   (Addr:{1} Type:{2})", Name, Addr.ToString(), NodeType);
        }
        public Node(byte addr, frame frm = null)
        {
            baseClu = new Cluster_base.Clu(0, this, addr);
            infoClu = new Cluster_info.Clu(1, this);
            errorClu = new Cluster_error.Clu(2, this);
            register(frm);
            clusters[baseClu.clustr_ID] = baseClu;
            clusters[infoClu.clustr_ID] = infoClu;
            clusters[errorClu.clustr_ID] = errorClu;
            baseClu.read();
            infoClu.read();
        }
        public Node(Node n)
        {
            this.Tag = n.Tag;
            this.is_hardware_exist = n.is_hardware_exist;
            this.access_fail_counter = n.access_fail_counter;
            this.clusters = n.clusters;
            this.baseClu = n.baseClu;
            this.baseClu.changeParentNode(this);
            this.infoClu = n.infoClu;
            this.infoClu.changeParentNode(this);
            this.errorClu = n.errorClu;
            this.errorClu.changeParentNode(this);
            n.clearNodeForm();
            n.parent.nodeReplace(n,this);
        }
        #endregion
        public bool Is_hareware_exist
        {
            get {return is_hardware_exist;}
        }

        public void register(frame frm)
        {
            frm.nodeRegister(this);
        }
        public void unregister()
        {
            parent.nodeUnregister(this);
        }

        #region access
        public virtual void postAccess(Access ac)
        {
            parent.addAccess(ac);
        }
        public void accessDone(Access ac)
        {
            if (ac.status == Access.Status.RecvedDone)
            {
                is_hardware_exist = true;
                access_fail_counter = 0;
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
                    case Access.ePort.Cmd:
                        cmdAccessDone(ac);
                        break;
                    case Access.ePort.Cgf:
                        cfgAccessDone(ac);
                        break;
                }
            }
            else
            {
                access_fail_counter++;
                if(access_fail_counter >= 3)
                {
                    is_hardware_exist =false;
                }
            }
        }

        protected void cfgAccessDone(Access ac)
        {
            int clusterID = ac.Send_data[0];
            if (clusters[clusterID] != null)
            {
                if (ac.Recv_data == null)
                {
                    throw new Exception("cfg_receive a null recv_data");
                }
                if ((ac.Recv_error)||(ac.Recv_busy))
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
        protected virtual void cmdAccessDone(Access ac)
        {

        }


        #endregion
        #region Form
        Node_form nf;
        public Node_form getForm()
        {
            if (nf == null)
            {
                nf = new Node_form(this);
            }
            return nf;
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
            return new UntypedNodeCtrl(this);
        }
        internal System.Windows.Forms.Control getClusterControl(int i)
        {
            if ((i >= 0) && (i < 128))
            {
                if(clusters[i] == null)
                {
                    throw new Exception(string.Format(
                        "发生了非预期的错误，节点{0}的{1}簇申请控件，但是这个簇不存在或者已经被销毁了。",
                       this.Addr.ToString(), i.AsByteToHexSt()));
                }
                return this.clusters[i].createControl();
            }
            else
            {
                throw new Exception(string.Format(
                    "节点{0}的{1}簇申请控件，{1}不是合法的索引。",
                   this.Addr.ToString(), i.AsByteToHexSt()));
            }
        }
        #endregion

        public bool isNewAddrAvaliable(byte addr)
        {
            if (parent.Nodes[addr] != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
