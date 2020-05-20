using System;

namespace SRB.Frame
{
    public delegate void dNodeUpdateEvent(BaseNode n);
    public partial class BaseNode : IAccesser, IDisposable
    {


        public static ISpecializer specializer;
        private ByteBank bank;
        private IBus bus;
        private INodeInterpreter datas;
        private SrbUpdater updater;

        public SrbUpdater Updater => updater;

        public INodeInterpreter Datas { get => datas; set => datas = value; }
        public virtual string Help_net_work { get => "https://github.com/lee8871/SRB-Introduction"; }
        private object tag;
        public object Tag { get => tag; set => tag = value; }
        byte addr;
        public byte Addr => addr;
        public string Name => baseClu.name;
        public string NodeType => this.infoClu.type;

        private ICluster[] clusters = new ICluster[128];

        private AddressCluster baseClu;
        private InformationCluster infoClu;
        private ErrorCluster errorClu;
        private SyncCluster syncClu;

        private DebugInfoCluster debugClu;

        public event dNodeUpdateEvent eChangeDescription;
        public event dNodeUpdateEvent eDispossing;
        public event EventHandler eUpdateModeChanging;

        private void onAddressChanged(byte new_addr)
        {
            addr = new_addr;
        }
        private void onDescriptionChanged()
        {
            if (node_form != null)
            {
                node_form.updateText();
            }
            if (eChangeDescription != null)
            {
                eChangeDescription.Invoke(this);
            }
        }
        public void changeAddr(byte addr)
        {
            this.baseClu.changeAddress(addr);
        }

        #region base method
        public string getAddrName()
        {
            if (is_in_update)
            {
                return string.Format("{0}\n{1}", Addr, "UpdateNode");
            }
            else
            {
                return string.Format("{0}\n{1}", Addr, Name);
            }
        }
        public override string ToString()
        {
            if (is_in_update)
            {
                return string.Format("{0}\n{1}", Addr, "UpdateNode");
            }
            else
            {
                return String.Format("{0}   (Addr:{1} Type:{2})", Name, Addr.ToString(), NodeType);
            }
        }
        public BaseNode(byte addr, IBus bus)
        {
            this.addr = addr;
            this.bus = bus;
            bank = new ByteBank(256, false);
        }
        bool is_in_update = false;
        public bool Is_in_update => is_in_update;
        public void checkNodeUpdateable()
        {
            Access ac = new Access(this, this, Access.PortEnum.Udp, new byte[] { 5 });
            bus.singleAccess(ac);
            if (ac.Status == Access.StatusEnum.RecvedDone)
            {
                is_in_update = true;
                if (updater == null)
                {
                    updater = new SrbUpdater(this);
                }
                updater.sendInfoPkg();
                updater.sendAppInfoPkg();
            }
        }


        public void checkNodeAccessable()
        {
            for(int i = 0;i< clusters.Length; i++)
            {
                clusters[i] = null;
            }
            this.datas = null;
            baseClu = new AddressCluster(this);
            baseClu.read();
            if (Is_hareware_exist)
            {
                infoClu = new InformationCluster(this);
                errorClu = new ErrorCluster(this);
                syncClu = new SyncCluster(this);
                debugClu = new DebugInfoCluster(this);
                infoClu.read();
                infoClu.timestampClu.read();
                specializer.specializeNode(this);
            }
        }

        public void gotoUpdateMode()
        {

            if (is_in_update == false)
            {
                if (updater == null)
                {
                    updater = new SrbUpdater(this);
                }
                try
                {
                    updater.gotoUpdateMode();
                }
                catch (SrbUpdater.UpdateTimeoutException e)
                {
                    return;
                }
                is_in_update = true;
                if (eUpdateModeChanging != null)
                {
                    eUpdateModeChanging.Invoke(this, new EventArgs());
                }
                if (eChangeDescription != null)
                {
                    eChangeDescription.Invoke(this);
                }
            }
        }
        public void gotoNormalMode()
        {
            try
            {
                if (true == updater.tryExit())
                {
                    checkNodeAccessable();
                    is_in_update = false;
                    if (eUpdateModeChanging != null)
                    {
                        eUpdateModeChanging.Invoke(this, new EventArgs());
                    }
                    if(eChangeDescription != null)
                    {
                        eChangeDescription.Invoke(this);
                    }
                }

            }
            catch (SrbUpdater.UpdateTimeoutException) { }
        }



        public void Dispose()
        {
            if (eDispossing != null)
            {
                eDispossing.Invoke(this);
            }
            if (this.node_form != null)
            {
                //this.nf.Close();
                this.node_form.close(this, null);
            }
        }

        #endregion

        public void ledAddr(BaseNode.AddressCluster.LedAddrType adt)
        {
            baseClu.ledAddr(adt);
        }


        #region 节点响应检查
        private bool is_node_exist = false;
        private int access_fail_counter = 0;

        public bool Is_hareware_exist => is_node_exist;

    
        public void active()
        {
            is_node_exist = true;
            access_fail_counter = 0;
        }
        public void lose()
        {
            access_fail_counter++;
            if (access_fail_counter >= 3)
            {
                is_node_exist = false;
            }

        }
        #endregion

        #region access


        public event EventHandler<AccessEventArgs> eDataAccessRecv;

        public class AccessEventArgs : EventArgs
        {
            public bool Handled = false;
            public Access ac;
            public AccessEventArgs(Access access) : base()
            {
                ac = access;
            }
        }



        private Mapping[] mappings;
        public void bankInit(byte[][] raw)
        {
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
                pd[i] = bank[mapping.downMapping(i)];
            }
            return new Access(this, this, (Access.PortEnum)port, pd);
        }

        public void accessDone(Access ac)
        {
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
                    if (e.Handled == false)
                    {
                        OnDataAccessDone(ac);
                    }
                    break;
                case Access.PortEnum.Udp:

                    break;

                default:
                    throw new Exception("Receive not data");
            }
        }

        public event EventHandler eBankChangeByAccess;
        protected virtual void OnDataAccessDone(Access ac)
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
                bank[mapping.upMapping(i)] = ac.Recv_data[i];
            }
            if (recv_len != 0)
            {
                if (eBankChangeByAccess != null)
                {
                    eBankChangeByAccess.Invoke(this, new EventArgs());
                }
            }
            return;
        }


        #endregion
    }
    public partial class BaseNode //窗体显示
    {
        public abstract class INodeControlOwner
        {
            public bool is_have_control = true;
            public System.Windows.Forms.Control control;
            protected abstract System.Windows.Forms.Control createControl(); 
            public virtual System.Windows.Forms.Control getControl()
            {
                if (control == null)
                {
                    control = createControl();
                    control.Disposed += onControlDisposed;
                }
                return control;
            }

            protected virtual void onControlDisposed(object sender, EventArgs e)
            {
                control = null;
            }
        }

        private Node_form node_form;
        public Node_form getForm()
        {
            if ((node_form == null)|| (node_form.IsDisposed))
            {
                node_form = new Node_form(this);
            }
            return node_form;
        }
        public void closeNodeForm()
        {
            if (node_form != null)
            {
                if (!(node_form.IsDisposed))
                {
                    node_form.close(this, new EventArgs());
                    node_form.Dispose();
                    node_form = null;
                }
            }
        }

        public INodeControlOwner getClusters(int i) => clusters[i];
        
        public virtual string Describe => datas.Describe;

        public virtual string GetToolTip()
        {
            if (Is_in_update)
            {
                return ToString();
            }
            else
            {
                return ToString() + "\n" + Describe;
            }
        }


    }

}
