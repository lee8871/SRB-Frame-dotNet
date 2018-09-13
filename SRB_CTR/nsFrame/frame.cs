
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using SRB_CTR.nsBrain;

namespace SRB_CTR.nsFrame
{
    class frame
    {
        Node[] nodes;
        internal Node[] Nodes
        {
            get { return nodes; }
        }

        SRB_Master master;

        public bool Is_port_opend
        {
            get { return master.Is_opened(); }
        }

        FrameForm _nodes_form;
        public FrameForm Nodes_form
        {
            get{return _nodes_form;}
        }
        Brain main_brain;

        Thread calculation;



        public frame()
        {
            nodes = new Node[128];
            master = new SRB_Master_Uart();
            //main_brain = new nsBrain.brain_test(this);
            _nodes_form = new FrameForm(this);
        }

        #region Node

        public delegate void eNodeChange(Node n);
        public eNodeChange eNode_register;
        public eNodeChange eNode_unregister;
        public eNodeChange eNode_change;
        internal void nodeRegister(Node n)
        {
            int a = n.Addr;
            if (nodes[a] != null)
            {
                if (eNode_unregister != null)
                {
                    eNode_unregister.Invoke(nodes[a]);
                }
                nodes[a].Parent = null;
                nodes[a] = null;

            }
            n.Parent = this;
            nodes[a] = n;
            if (eNode_register != null)
            {
                eNode_register.Invoke(n);
            }

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
                            nsBrain.Node_dMotor.cn cn = new nsBrain.Node_dMotor.cn(n);
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
        
        #endregion 

        #region Access
        object lock_access_queue = new object();
        Queue<Access> access_queue = new Queue<Access>();
        internal void addAccess(Access ac)
        {
            if (this.Is_calculation_running)
            {
                lock (lock_access_queue)
                {
                    access_queue.Enqueue(ac);
                }
            }
            else
            {
                singleAccess(ac);
            }
        }
        public void singleAccess(Access ac)
        {
            if (this.Is_calculation_running)
            {
                throw new Exception("The Brain is running, so single access is disabled.");
            }
            master.doAccess(ac);
            ac.accessDone();
            if (ad != null)
            {
                ad.addAccess(ac);
            }

        }
        #endregion

        #region CalCulation Loop
        private int[] calculation_delay = new int[100];
        private int[] communication_delay = new int[100];
        private int delay_counter = 0;
        public void getDelayValue(ref double us_calculation ,ref double us_communation)
        {
            double temp;
            temp = 0;
            foreach (long l in calculation_delay)
            {
                temp += l;
            }
            us_calculation = temp * (1024.0 / 100 / 3000);
            temp = 0;
            foreach (long l in communication_delay)
            {
                temp += l;
            }
            us_communation = temp * (1024.0 / 100 / 3000);
            return;
        }


        private int cycle_in_ms;
        public int Cycle_in_ms
        {
            get { return cycle_in_ms; }
            set { cycle_in_ms = value; }
        }
        private bool is_calculation_running = false;
        public bool Is_calculation_running
        {
            get { return is_calculation_running; }
        }
        
        public bool stopCalculation()
        {
            if (is_calculation_running)
            {
                is_calculation_running = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool runCalculation()
        {
            if (is_calculation_running)
            {
                return false;
            }
            else
            {
                is_calculation_running = true;
                calculation = new Thread(new ThreadStart(run));
                calculation.Priority = ThreadPriority.Highest;
                calculation.Start();
                return true;
            }
        }

        private void run()
        {
            long begin;
            long calculation_done;
            long communication_done;
            long next_begin;
            while (is_calculation_running == true)
            {
                begin = Stopwatch.GetTimestamp();

                main_brain.calculate();
                calculation_done = Stopwatch.GetTimestamp();
                
                Access[] acs;
                lock (lock_access_queue)
                {
                    acs = access_queue.ToArray();
                    access_queue.Clear();
                }

                master.doAccess(acs, acs.Length);

                foreach (Access ac in acs)
                {
                    ac.accessDone();

                }

                communication_done = Stopwatch.GetTimestamp();

                calculation_delay[delay_counter] = (int)(calculation_done - begin);
                communication_delay[delay_counter] = (int)(communication_done - calculation_done);

                delay_counter++; delay_counter %= 100;
                next_begin = begin + (long)(cycle_in_ms * (3000000 / 1024.0));  

                if (delay_counter == 99)
                {
                    double temp;
                    temp = 0;
                    foreach (long l in calculation_delay)
                    {
                        temp += l;
                    }
                    _nodes_form.us_calculation = temp * (1024.0 / 100 / 3000);
                    temp = 0;
                    foreach (long l in communication_delay)
                    {
                        temp += l;
                    }
                    _nodes_form.us_communation = temp * (1024.0 / 100 / 3000);
                }
                if (_nodes_form.IsDisposed)
                {
                    return;
                }
                if (ad != null)
                {
                    ad.addAccess(acs, acs.Length);
                }
                while (Stopwatch.GetTimestamp() < next_begin) ;
            }
        }
        #endregion




        public System.Windows.Forms.Control getuartConfigContol()
        {
            return master.getConfigControl();
        }
        AccessDisplayer ad;
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
