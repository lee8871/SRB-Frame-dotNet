using System;

namespace SRB.Frame
{
    public delegate void dNodeUpdateEvent(Node n);
    public partial class Node : IAccesser, IDisposable
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
        public SyncCluster syncClu;
        private DebugInfoCluster debugClu;

        public event dNodeUpdateEvent eChangeDescription;
        public event dNodeUpdateEvent eDispossing;
        public event dNodeUpdateEvent eUpdateModeChanging;
        private void onAddressChanged(byte new_addr)
        {
            addr = new_addr;
        }
        private void onDescriptionChanged()
        {
            node_form?.descriptioChange();
            eChangeDescription?.Invoke(this);
        }
        private void onUpdateModeChanged()
        {
            node_form?.updateModeChanging();
            eUpdateModeChanging?.Invoke(this);
            onDescriptionChanged();
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
                return $"^{Addr}\n{updater.Hardware_code}";
            }
            else
            {
                return $"{Addr}\n{Name}";
            }
        }
        public override string ToString()
        {
            if (is_in_update)
            {
                return $"Updating(Addr:{ Addr.ToString()} Hardware:{updater.Hardware_code})";
            }
            else
            {
                return $"{Name}(Addr:{ Addr.ToString()} Type:{NodeType})";
            }
        }
        public Node(byte addr, IBus bus)
        {
            this.addr = addr;
            this.bus = bus;
            bank = new ByteBank(256);
        }
        bool is_in_update = false;
        public bool Is_in_update => is_in_update;
        public void checkNodeUpdateable()
        {
            Access ac = new Access(this, this, AccessPort.Udp, new byte[] { 5 });
            bus.singleAccess(ac);
            if (ac.Status == AccessStatus.RecvedDone)
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
            for (int i = 0; i < clusters.Length; i++)
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
                debugClu = new DebugInfoCluster(this);
                infoClu.readAll();
                specializer.specializeNode(this);
            }
        }

        bool is_sync_node => (syncClu != null);
        public void initSyncClu()
        {
            syncClu = new SyncCluster(this);
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
                onUpdateModeChanged();
            }
        }
        public void gotoNormalMode()
        {
            try
            {
                if (true == updater.tryExit())
                {
                    Action action = () =>
                    {
                        System.Threading.Thread.Sleep(200);
                        checkNodeAccessable();
                        is_in_update = false;
                        onUpdateModeChanged();
                    };
                    var t = new System.Threading.Tasks.Task(action);
                    t.Start();
                }
            }
            catch (SrbUpdater.UpdateTimeoutException) { }
        }


        private bool isDisposed = false;
        public bool IsDisposed => isDisposed;
        public void Dispose()
        {
            eDispossing?.Invoke(this);
            closeNodeForm();
            isDisposed = true;
        }

        #endregion

        public void ledAddr(Node.AddressCluster.LedAddrType adt)
        {
            baseClu.ledAddr(adt);
        }


        #region 节点响应检查
        private bool is_node_exist = false;
        private int node_exist_check_counter = 0;
        public bool Is_hareware_exist => is_node_exist;

        private int access_counter = 0;
        private int access_retry_counter = 0;
        private int access_fail_counter = 0;
        public int Access_counter { get => access_counter; }
        public int Access_retry_counter { get => access_retry_counter; }
        public int Access_fail_counter { get => access_fail_counter; }

        public void active(int retry)
        {
            access_counter++;
            access_retry_counter += retry;

            is_node_exist = true;
            node_exist_check_counter = 0;
        }
        public void lose()
        {
            access_fail_counter++;
            node_exist_check_counter++;
            if (node_exist_check_counter >= 3)
            {
                is_node_exist = false;
            }

        }
        #endregion

        #region 节点数据访问
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
            return new Access(this, this, (AccessPort)port, pd);
        }

        public event EventHandler<AccessEventArgs> eDataAccessRecv;
        public void accessDone(Access ac)
        {
            switch (ac.Port)
            {
                case AccessPort.D0:
                case AccessPort.D1:
                case AccessPort.D2:
                case AccessPort.D3:
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
               // default:
                   // throw new Exception("Receive not data");
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
                eBankChangeByAccess?.Invoke(this, new EventArgs());
            }
            return;
        }


        #endregion
    }
    public partial class Node //窗体显示
    {
        public abstract class INodeControlOwner
        {
            public bool is_follower = true;
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
        private NodeForm node_form;
        public NodeForm getForm()
        {
            if (node_form == null)
            {
                node_form = new NodeForm(this);
                node_form.Disposed += Node_form_Disposed;
            }

            return node_form;
        }

        private void Node_form_Disposed(object sender, EventArgs e)
        {
            node_form = null;
        }

        public void closeNodeForm()
        {
           node_form?.close(this, new EventArgs());
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
