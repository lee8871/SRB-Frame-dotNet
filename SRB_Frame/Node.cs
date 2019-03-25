using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRB.Frame
{
    public class Node:IDisposable
    {
        ISRB_Master parent = null;
        public ISRB_Master Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public object Tag;
        bool is_hardware_exist = false;
        int access_fail_counter = 0;
        protected bool Can_post_access
        {
            get { return (parent != null); }
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
        protected ICluster[] clusters = new ICluster[128];

        protected Cluster.AddressCluster baseClu;
        protected Cluster.InformationCluster infoClu;
        protected Cluster.ErrorCluster errorClu;

        public EventHandler eAddr_change;
        public EventHandler eDescription_change;

        virtual public void onAddrChanged()
        {
            this.parent.nodeAddrChange(this);
            if (node_form != null)
            {
                node_form.updateText();
            }
            if (eAddr_change != null)
            {
                eAddr_change.Invoke(this,new EventArgs());
            }
           // 为什么地址变了就要更新描述呢?应该在外部选择合并触发 onDescriptionChanged();
        }
        virtual public void onDescriptionChanged()
        {
            this.parent.nodeDescriptionChange(this);
            if (node_form != null)
            {
                node_form.updateText();
            }
            if (eDescription_change != null)
            {
                eDescription_change.Invoke(this, new EventArgs());
            }
        }

        #region base method
        public override string ToString()
        {
            return String.Format("{0}   (Addr:{1} Type:{2})", Name, Addr.ToString(), NodeType);
        }
        public Node(byte addr, ISRB_Master frm = null)
        {
            baseClu = new Cluster.AddressCluster(0, this, addr);
            infoClu = new Cluster.InformationCluster(1, this);
            errorClu = new Cluster.ErrorCluster(2, this);
            register(frm);
            clusters[baseClu.Clustr_ID] = baseClu;
            clusters[infoClu.Clustr_ID] = infoClu;
            clusters[errorClu.Clustr_ID] = errorClu;
            baseClu.read();
            infoClu.read();
            frm.nodeDescriptionChange(this);
        }
        public void ledAddr(Cluster.AddressCluster.LedAddrType adt)
        {
            baseClu.ledAddr(adt);
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
            n.parent.nodeReplace(n, this);
        }
        #endregion

        public bool Is_hareware_exist
        {
            get { return is_hardware_exist; }
        }

        public void register(ISRB_Master frm)
        {
            frm.nodeRegister(this);
        }
        public void unregister()
        {
            parent.nodeUnregister(this);
            Parent = null;
        }
        public void Dispose()
        {
            if (this.node_form != null)
            {
                //this.nf.Close();
                this.node_form.close(this,null);
            }
        }



        #region access
        private Access buildAccess(int port, int sent_len = -1)
        {
            Mapping mapping = mappings[port];
            if ((sent_len < 0) || (sent_len > mapping.Down_len))
            {
                sent_len = mapping.Down_len;
            }
            byte[] pd = new byte[sent_len];
            for (int i = 0; i < sent_len; i++)
            {
                pd[i] = bank[((int)mapping.Down_mapping[i])];
            }
            return new Access(this, (Access.PortEnum)port, pd);
        }

        public virtual void addAccess(Access ac)
        {
            parent.addAccess(ac);
        }

        public virtual void addAccess(int port, int sent_len = -1)
        {
            parent.addAccess(buildAccess(port, sent_len));
        }

        public virtual void singleAccess(Access ac)
        {
            parent.singleAccess(ac);
        }
        public virtual void singleAccess(int port, int sent_len = -1)
        {
            parent.singleAccess(buildAccess(port, sent_len));
        }
        public event EventHandler<AccessEventArgs> eDataAccessRecv;

        public class AccessEventArgs : EventArgs
        {
            public bool Handled = false;
            public Access ac;
            public AccessEventArgs(Access access):base()
            {
                ac = access;
            }
        }
        public void accessDone(Access ac)
        {
            if (ac.Status == Access.StatusEnum.RecvedDone)
            {
                is_hardware_exist = true;
                access_fail_counter = 0;
                switch (ac.Port)
                {
                    case Access.PortEnum.D0:
                    case Access.PortEnum.D1:
                    case Access.PortEnum.D2:
                    case Access.PortEnum.D3:
                        AccessEventArgs e = new AccessEventArgs(ac);
                        if (eDataAccessRecv != null)
                        {
                            eDataAccessRecv.Invoke(this, e);
                        }
                        
                        if(e.Handled==false)
                        {
                            dataAccessDone(ac);
                        }
                        break;
                    case Access.PortEnum.Cmd:
                        cmdAccessDone(ac);
                        break;
                    case Access.PortEnum.Cgf:
                        cfgAccessDone(ac);
                        break;

                }
            }
            else
            {
                access_fail_counter++;
                if (access_fail_counter >= 3)
                {
                    is_hardware_exist = false;
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
                if ((ac.Recv_error) || (ac.Recv_busy))
                {
                    return;
                }
                if (ac.Send_data.Length == 1)
                {
                    clusters[clusterID].readRecv(ac);
                }
                else
                {
                    clusters[clusterID].writeRecv(ac);
                }
            }
        }
        protected virtual void dataAccessDone(Access ac)
        {
            int port;
            port = (int)ac.Port;
            int recv_len = ac.Recv_data.Length;
            Mapping mapping = mappings[port];
            if (recv_len > mapping.Up_len)
            {
                recv_len = mapping.Up_len;
            }
            for (int i = 0; i < recv_len; i++)
            {
                bank[((int)mapping.Up_mapping[i])] = ac.Recv_data[i];
            }
            return;
        }
        protected virtual void cmdAccessDone(Access ac)
        {

        }


        #endregion
        #region Form
        Node_form node_form;
        public Node_form getForm()
        {
            if (node_form == null)
            {
                node_form = new Node_form(this);
            }
            return node_form;
        }
        public void clearNodeForm()
        {
            node_form = null;
        }
        public string[] getClusterTable()
        {
            string[] st_a = new string[128];
            for (int i = 0; i < 128; i++)
            {
                if (clusters[i] != null)
                    st_a[i] = clusters[i].ToString();
            }
            return st_a;
        }

        public virtual System.Windows.Forms.Control getClusterControl()
        {
            return new UntypedNodeCtrl(this);
        }
        public System.Windows.Forms.Control getClusterControl(int i)
        {
            if ((i >= 0) && (i < 128))
            {
                if (clusters[i] == null)
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

            return parent.isNewAddrAvaliable(addr);
        }
        public virtual string Describe()
        {
            return @"This node is in unknow type. It may not read the type information, or information is not in this frame.";
        }
        public virtual string ToolTip()
        {
            return ToString() + "\n" + Describe();
        }




        #region bank
        protected byte[] bank;
        private Mapping[] mappings;

        public void bankInit(byte[][] raw)
        {
            bank = new byte[256];
            mappings = new Mapping[4];
            for (int i = 0; i < 4; i++)
            {
                mappings[i] = new Mapping(raw[i]);
            }
        }
        public void mappingUpdata(int mapping_num, byte[] raw)
        {
            mappings[mapping_num] = new Mapping(raw);
        }


        public void bankWrite(byte data, int byte_Location)
        {
            bank[byte_Location] = data;
        }
        public byte bankReadByte(int byte_Location)
        {
            return bank[byte_Location];
        }


        public void bankWrite(byte data, int byte_Location, int mask)
        {
            data = (byte)((data & mask) | (bank[byte_Location] & ~mask));
            bankWrite(data, byte_Location);
        }
        public void bankWrite(byte data, int byte_Location, int bit_len, int bit_location)
        {
            int mask = 1 << bit_len - 1;
            data <<= bit_location;
            mask <<= bit_location;
            bankWrite(data, byte_Location, mask);
        }
        public byte bankReadByte(int byte_Location, int bit_len, int bit_location)
        {
            byte mask = (byte)(1 << bit_len - 1);
            return (byte)((bank[byte_Location] >> bit_location) & mask);
        }
        public void bankWrite(ushort data, int byte_Location)
        {
            bank[byte_Location] = data.ByteLow();
            bank[byte_Location + 1] = data.ByteHigh();
        }
        public void bankWrite(ushort data, int byte_Location, int mask)
        {
            ushort bankdata = Support.byteToUint16(bank[byte_Location], bank[byte_Location + 1]);

            data = (ushort)((data & mask) | (bankdata & ~mask));
            bankWrite(data, byte_Location);
        }
        public void bankWrite(ushort data, int byte_Location, int bit_len, int bit_location)
        {
            int mask = 1 << bit_len - 1;
            data <<= bit_location;
            mask <<= bit_location;
            bankWrite(data, byte_Location, mask);
        }
        public void bankWrite(int data, int byte_Location)
        {
            bank[byte_Location] = (byte)data;
            data >>= 8;
            bank[byte_Location + 1] = (byte)data;
            data >>= 8;
            bank[byte_Location + 2] = (byte)data;
            data >>= 8;
            bank[byte_Location + 3] = (byte)data;
        }
        public void bankWrite(int data, int byte_Location, int mask)
        {
            int bankdata;
            bankdata = bank[byte_Location] + bank[byte_Location + 1] << 8 +
                bank[byte_Location + 2] << 16 + bank[byte_Location + 3] << 24;
            data = (ushort)((data & mask) | (bankdata & ~mask));
            bankWrite(data, byte_Location);
        }
        public void bankWrite(int data, int byte_Location, int bit_len, int bit_location)
        {
            int mask = 1 << bit_len - 1;
            data <<= bit_location;
            mask <<= bit_location;
            bankWrite(data, byte_Location, mask);
        }
        public void bankWrite(bool data, int byte_Location, int bit_location)
        {
            if (data)
            {
                bank[byte_Location] |= (byte)(1 << bit_location);

            }
            else
            {
                bank[byte_Location] &= (byte)~(1 << bit_location);
            }
        }

        #endregion
    }

}
