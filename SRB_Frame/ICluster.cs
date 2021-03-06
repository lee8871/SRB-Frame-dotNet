﻿using System;
using System.Windows.Forms;

namespace SRB.Frame
{
    public partial class Node
    {

        public abstract class ICluster : INodeControlOwner, IAccesser
        {
            protected ByteBank bank;
            protected byte cID;
            protected Node parent_node;
            protected IBus Bus => parent_node.bus;

            protected ICluster[] following_clusters = null; 
            public Node Parent_node { get => parent_node; }
            public byte CID { get => cID; }
            class ClusterRedefineException: SrbException
            {
                public Node node;
                public ICluster new_cluster;
                public ClusterRedefineException(Node node,
                 ICluster new_cluster)
                {
                    this.node = node;
                    this.new_cluster = new_cluster;
                }
                public override string ToString()
                {
                    ICluster original_cluster;
                    original_cluster = node.clusters[new_cluster.CID];
                    return string.Format("Cluster redefined at node {0}\n" +
                        "cluster{1} ({2}) -> ({3})",
                        node.ToString(), original_cluster.CID,
                        original_cluster.ToString(), new_cluster.ToString()); 
                }
            }

            public ICluster(Node n, byte ID, int banksize)
            {
                this.cID = ID;
                if (n.clusters[this.cID]!=null)
                {
                    throw new ClusterRedefineException(n, this);
                }
                n.clusters[this.cID] = this;
                bank = new ByteBank(banksize);
                this.parent_node = n;
            }
            public void writeBankinit()
            {
            }
            public bool is_not_exist = false;
            public void accessDone(Access ac)
            {
                if (ac.Port != AccessPort.Cgf)
                {
                    throw new Exception("Data type should Cfg,but get " + ac.Port.ToString());
                }
                else if (ac.Recv_data == null)
                {
                    throw new Exception("cfg_receive a null recv_data");
                }
                else if (ac.Recv_error) 
                {
                    switch( ac.Recv_data[0] )
                    {
                        case (int)(AccessNodeError.RE_CFG_EMPTY_CLUSTER):
                            is_not_exist = true;
                            break;
                    }
                }
                else if (ac.Recv_busy)
                {
                    throw new Exception("cfg_receive a Recv_busy recv_data");
                }
                else if (ac.Send_data.Length == 1)
                {
                    readRecv(ac);
                }
                else
                {
                    writeRecv(ac);
                }
            }
            public virtual void write()
            {
                Access ac = Bus.accessRequest(this, parent_node, AccessPort.Cgf);
                var data = ac.Send_data;
                data[0] = CID;
                data.load(bank.Load_ba, 1, 0, bank.Length);
                parent_node.bus.singleAccess(ac);
            }
            public virtual void writeRecv(Access ac)
            {
                if (ac.Recv_error == false)
                {
                    OnDataChangded();
                }
            }

            public void read()
            {
                Access ac = Bus.accessRequest(this, parent_node, AccessPort.Cgf,CID);
                parent_node.bus.singleAccess(ac);
            }
            public void readAll()
            {
                if (following_clusters == null)
                {
                    read();
                }
                else {
                    Access ac;
                    foreach (ICluster c in following_clusters)
                    {
                        ac = Bus.accessRequest(c, parent_node, AccessPort.Cgf, c.CID);
                        parent_node.bus.addAccess(ac);
                    }
                    ac = Bus.accessRequest(this, parent_node, AccessPort.Cgf, this.CID);
                    parent_node.bus.addAccess(ac);
                    parent_node.bus.sendAccess();
                }
            }

            public virtual void readRecv(Access ac)
            {
                //todo:
                //check datalen
                if (ac.Recv_data_len != 0)
                {
                    for (int i = 0; i < bank.Length; i++)
                    {
                        bank[i] = ac.Recv_data[i];
                    }
                }
                OnDataChangded();
            }

            public void changeParentNode(Node n)
            {
                parent_node = n;
            }


            public event EventHandler eDataChanged;

            public virtual void OnDataChangded()
            {
                if (eDataChanged != null)
                {
                    eDataChanged.Invoke(this, new EventArgs());
                }
            }


            public abstract override string ToString();
        }
    }
}
