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
                bank = new ByteBank(banksize, true);
                this.parent_node = n;
            }
            public void writeBankinit()
            {
                bank.writeInit();
            }
            public void accessDone(Access ac)
            {
                if (ac.Port != Access.PortEnum.Cgf)
                {
                    throw new Exception("Data type should Cfg,but get " + ac.Port.ToString());
                }
                if (ac.Recv_data == null)
                {
                    throw new Exception("cfg_receive a null recv_data");
                }
                if ((ac.Recv_error) || (ac.Recv_busy))
                {
                    return;
                }
                if (ac.Send_data.Length == 1)
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
                byte[] data = new byte[bank.temp.Length + 1];
                data[0] = CID;
                for (int i = 0; i < bank.temp.Length; i++)
                {
                    data[i + 1] = bank.temp[i];
                }
                Access ac = new Access(this, parent_node, Access.PortEnum.Cgf, data);
                parent_node.bus.singleAccess(ac);

            }
            public virtual void writeRecv(Access ac)
            {
                if (ac.Recv_error == false)
                {
                    bank.writeDone();
                    OnDataChangded();
                }
            }

            public void read()
            {
                Access ac = new Access(this, parent_node, Access.PortEnum.Cgf, new byte[] { CID });
                parent_node.bus.singleAccess(ac);
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
