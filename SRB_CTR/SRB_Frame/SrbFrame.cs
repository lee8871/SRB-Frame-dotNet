
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using SRB_CTR.nsBrain;
using SRB_CTR.SRB_Frame.Cluster_base;

namespace SRB_CTR.SRB_Frame
{
    class SrbFrame
    {
        Node[] nodes;
        internal Node[] Nodes
        {
            get { return nodes; }
        }
        ISRB_Driver srb;
        public bool Is_port_opend
        {
            get { return srb.Is_opened(); }
        }
        FrameForm _nodes_form;
        public FrameForm Nodes_form
        {
            get{return _nodes_form;}
        }
        IBrain main_brain;
        public SrbFrame()
        {
            nodes = new Node[128];
            ///TODO
            ///add master select 
            //master = new SRB_Master_Uart();
            srb = new SRB_Master_USB();
            main_brain = new nsBrain.Brain_Test(this);
            _nodes_form = new FrameForm(this);
            _nodes_form.Disposed += _nodes_form_Disposed;
        }

        private void _nodes_form_Disposed(object sender, EventArgs e)
        {
            main_brain.stop();
        }
        #region Node

        public delegate void dNodeChange(Node n);
        public dNodeChange eNode_register;
        public dNodeChange eNode_unregister;
        public dNodeChange eNode_change;
        internal void nodeRegister(Node n)
        {
            int a = n.Addr;
            if (a < nodes.Length)
            { 
                if (nodes[a] != null)
                {
                    if (eNode_unregister != null)
                    {
                        eNode_unregister.Invoke(nodes[a]);
                    }
                    nodes[a].Parent = null;
                    nodes[a] = null;
                }
            nodes[a] = n;
            if (eNode_register != null)
            {
                eNode_register.Invoke(n);
            }
            }
            n.Parent = this;
        }

        internal void nodeAddrChange(Node n)
        {
            //remove the node which addr is changed
            for (int i = 0;i<scan_max_addr;i++)
            {
                if (Nodes[i] == n)
                {
                    Nodes[i] = null;
                    break;
                }
            }
            //find the addr changed to
            int addr = n.Addr;
            if (nodes[addr] != null)
            {//If there been a node on the node changed to, unregister it.
                if (eNode_unregister != null)
                {
                    eNode_unregister.Invoke(nodes[addr]);
                }
                nodes[addr].Parent = null;
                nodes[addr] = null;
            }
            //add the node here
            n.Parent = this;
            nodes[addr] = n;
            nodeDescriptionChange(n);
        }

        internal void nodeDescriptionChange(Node n)
        {
            if (eNode_change != null)
            {
                eNode_change.Invoke(n);
            }
        }

        internal void nodeReplace(Node from, Node to)
        {
            int addr = from.Addr;
            if (nodes[addr] != from)
            {
                throw new Exception("Old node is not in register.");
            }
            nodes[addr] = to;

            from.Parent = null;            
            to.Parent = this;

            if (eNode_change != null)
            {
                eNode_change.Invoke(to);
            }
        }
        internal void nodeUnregister(Node n)
        {
            int a = n.Addr;
            if (nodes[a] == n)
            {
                if (eNode_unregister != null)
                {
                    eNode_unregister.Invoke(nodes[a]);
                }
                nodes[a].Parent = null;
                nodes[a] = null;
            }
        }

        public bool scan_stop = true;
        Thread scan_thread;
        private int scan_addr = -1;

        public int Scan_addr
        {
            get { return scan_addr; }
            set { scan_addr = value; }
        }
        private double scan_progress = 0;
        public double Scan_progress
        {
            get { return scan_progress; }
            set { scan_progress = value; }
        }
        private int scan_max_addr = 128;
        public void scanNodes()
        {
            if (scan_stop)
            {
                scan_thread = new Thread(new ThreadStart(scanNodeLoop));
                scan_stop = false;
                scan_thread.Start();
            }
            return;

        }
        public void scanNodeLoop()
        {
            for (int i = 0; i < scan_max_addr; i++)
            {
                Scan_addr = i;
                Scan_progress = Scan_addr *1.0 / scan_max_addr;
                Node n = new Node((byte)i, this);
                if (n.Is_hareware_exist)
                {
                    switch (n.NodeType)
                    {
                        case "Du_Motor":
                            nsBrain.Node_dMotor.Cn cn = new nsBrain.Node_dMotor.Cn(n);
                          //  Nodes_form.addNode(cn);
                            break;
                        default:
                         //   Nodes_form.addNode(n);
                            break;
                    }
                }             
                else
                {
                    nodeUnregister(n);
                }
                if (scan_stop)
                {
                    Scan_addr = -2;
                    return;
                }
            }
            scan_stop = true;
            Scan_addr = -3;
        }

        internal void ledAddrAll(Clu.LedAddrType adt)
        {
            Access ac;
            byte[] b = new byte[2];
            int i = 0;
            b[i++] = 0x0;
            switch (adt)
            {
                case Clu.LedAddrType.Close:
                    b[i++] = 0xf5; break;
                case Clu.LedAddrType.High:
                    b[i++] = 0xf4; break;
                case Clu.LedAddrType.Low:
                    b[i++] = 0xf3; break;
            }
            ac = new Access(null, Access.PortEnum.Cgf, b);
            singleAccess(ac);
        }

        internal void runCalculation()
        {
            main_brain.run();
        }

        internal void stopCalculation()
        {
            main_brain.stop();
        }

        #endregion

        #region Access
        object lock_access_queue = new object();
        Queue<Access> access_queue = new Queue<Access>();
        internal void addAccess(Access ac)
        {
            lock (lock_access_queue)
            {
                access_queue.Enqueue(ac);
            }
        }
        public void singleAccess(Access ac)
        {
            srb.doAccess(ac);
            ac.onAccessDone();
            if (ad != null)
            {
                ad.addAccess(ac);
            }
        }
        public void sendAccess()
        {
            Access[] acs;
            lock (lock_access_queue)
            {
                acs = access_queue.ToArray();
                access_queue.Clear();
            }
            srb.doAccess(acs, acs.Length);
            foreach (Access ac in acs)
            {
                ac.onAccessDone();
            }
            if (ad != null)
            {
                ad.addAccess(acs, acs.Length);
            }
        }
        #endregion
        

        public System.Windows.Forms.Control getuartConfigContol()
        {
            return srb.getConfigControl();
        }
        AccessDisplayer ad;
        internal bool Is_calculation_running
        {
            get => main_brain.Running_flag;
        }

        public System.Windows.Forms.Form getAccessDisplayer()
        {
            if (ad == null)
            {
                ad = new AccessDisplayer(this);
            }
            return ad;
        }
        public void closeAccessDisplayer()
        {
            ad = null;
        }


    }
}
