﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using SRB.Frame;

namespace SRB_CTR
{
    public partial class SrbFrame : ISRB_Master, IDisposable
    {

        private ISRB_Driver srb;
        public bool Is_port_opend
        {
            get { return srb.Is_opened; }
        }

        FrameForm _nodes_form;
        public FrameForm Nodes_form
        {
            get { return _nodes_form; }
        }
        IBrain main_brain;
        SRB_Record record;
        public bool Is_calculation_running
        {
            get => main_brain.Is_running;
        }
        public SrbFrame()
        {
            Nodes = new BaseNode[200];
            ///TODO
            ///add master select 
            srb = new SRB_Master_Uart();
            main_brain = new nsBrain.Brain_Test2(this);
            _nodes_form = new FrameForm(this);
            _nodes_form.Disposed += _nodes_form_Disposed;
            record = new SRB_Record();
        }
        public System.Windows.Forms.Control usbControlDisplay()
        {
            if (!(srb is SRB_Master_USB))
            {
                srb = new SRB_Master_USB();
            }
            return srb.getConfigControl();
        }
        public System.Windows.Forms.Control uartControlDisplay()
        {
            if (!(srb is SRB_Master_Uart))
            {
                srb = new SRB_Master_Uart();
            }
            return srb.getConfigControl();
        }
        public bool isHighSpeedSupporting()
        {
            if (srb is SRB_Master_USB)
            {
                return true;
            }
            else
            {
                return false;
            }


        }



        public void Dispose()
        {
            endScan();
            main_brain.stop();
            endRecord();
            Log_Writer.No_exit_flag = false;
            while (Is_scan_running) ;
            while (main_brain.Is_running) ;
        }


        public void beginRecord()
        {
            record.beginRecord();
        }
        public void endRecord()
        {
            record.endRecord();
        }



        private void _nodes_form_Disposed(object sender, EventArgs e)
        {
            main_brain.stop();
        }
        #region Node


        public delegate void dNodeChange(BaseNode n);

        public dNodeChange eNode_register;
        public dNodeChange eNode_unregister;
        public dNodeChange eNode_change;
        public override void nodeRegister(BaseNode n)
        {
            int a = n.Addr;
            if (a < Nodes.Length)
            {
                if (Nodes[a] != null)
                {
                    BaseNode n_remove = Nodes[a];
                    n_remove.unregister();
                    n_remove.Dispose();
                }
                Nodes[a] = n;
                if (eNode_register != null)
                {
                    eNode_register.Invoke(n);
                }
            }
            n.Parent = this;
        }

        public override void nodeAddrChange(BaseNode n)
        {
            //remove the node which addr is changed
            for (int i = 0; i < scan_max_addr; i++)
            {
                if (Nodes[i] == n)
                {
                    Nodes[i] = null;
                    break;
                }
            }
            //find the addr changed to
            int addr = n.Addr;
            if (Nodes[addr] != null)
            {//If there been a node on the node changed to, unregister it.
                if (eNode_unregister != null)
                {
                    eNode_unregister.Invoke(Nodes[addr]);
                }
                Nodes[addr].Parent = null;
                Nodes[addr] = null;
            }
            //add the node here
            n.Parent = this;
            Nodes[addr] = n;
            nodeDescriptionChange(n);
        }

        public override void nodeDescriptionChange(BaseNode n)
        {
            if (eNode_change != null)
            {
                eNode_change.Invoke(n);
            }
        }

        public override void nodeReplace(BaseNode from, BaseNode to)
        {
            int addr = from.Addr;
            if (Nodes[addr] != from)
            {
                throw new Exception("Old node is not in register.");
            }
            Nodes[addr] = to;

            from.Parent = null;
            to.Parent = this;

            if (eNode_change != null)
            {
                eNode_change.Invoke(to);
            }
        }
        public override void nodeUnregister(BaseNode n)
        {
            int a = n.Addr;
            if (Nodes[a] != n)
            {
                throw new Exception(
                    n.Describe() + @"
unregist and call this Frame,
but we do not have the node in table");
            }
            if (eNode_unregister != null)
            {
                eNode_unregister.Invoke(Nodes[a]);
            }
            Nodes[a] = null;
        }


        public void classificationNode(BaseNode n)
        {
            switch (n.NodeType)
            {
                case "Du_Motor":
                    SRB.NodeType.Du_motor.Node cn = new SRB.NodeType.Du_motor.Node(n);
                    //  Nodes_form.addNode(cn);
                    break;
                case "Ps2_Handle":
                    SRB.NodeType.PS2_Handle.Node cn2 = new SRB.NodeType.PS2_Handle.Node(n);
                    //  Nodes_form.addNode(cn);
                    break;
                case "Charger_2LiB":
                    SRB.NodeType.Charger.Node Charger = new SRB.NodeType.Charger.Node(n);
                    //  Nodes_form.addNode(cn);
                    break;
                default:
                    //   Nodes_form.addNode(n);
                    break;
            }
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

        internal void ledAddrAll(SRB.Frame.Cluster.AddressCluster.LedAddrType type)
        {
            SRB.Frame.Cluster.AddressCluster.ledAddrBroadcast(type, this);
        }

        public override void addAccess(Access ac)
        {
            lock (lock_access_queue)
            {
                access_queue.Enqueue(ac);
            }
        }
        public override void singleAccess(Access ac)
        {
            srb.doAccess(ac);
            record.add(ac);
            ac.onAccessDone();
        }
        public override void sendAccess()
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
                record.add(ac);
            }
        }
        #endregion

        internal void resetAllAddress()
        {
            SRB.Frame.Cluster.AddressCluster.randomAddrAll(this);
        }
        internal void resetNewNodeAddress()
        {
            SRB.Frame.Cluster.AddressCluster.randomAddrNewNode(this);
        }
    }


    partial class SrbFrame
    {
        bool Is_scan_running {
            get
            {
                if (scan_thread != null)
                {
                    return scan_thread.IsAlive;
                }
                else
                {
                    return false;
                }
            }
        }
        Thread scan_thread;
        private int scan_addr = -1;
        public int Scan_status
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
        private int scan_max_addr = 200;

        bool scan_stop = true;
        public void autoSetAddress()
        {
            if (Is_scan_running == false)
            {
                scan_thread = new Thread(new ThreadStart(autoSetAddressLoop));
                scan_stop = false;
                scan_thread.Start();
            }
            return;

        }
        //about scan node 
        int scan_begin;
        int scan_end;
        public void scanNodes(int begin = 0, int end = -1)
        {
            if (end < 0)
            {
                end = scan_max_addr;
            }
            scan_end = end;
            scan_begin = begin;
            if (Is_scan_running == false)
            {
                scan_thread = new Thread(new ThreadStart(scanNodeLoop));
                scan_stop = false;
                scan_thread.Start();
            }
            return;
        }
        public void endScan()
        {
            scan_stop = true;
        }
        public void scanNodeLoop()
        {
            for (int Scaning = scan_begin; Scaning < scan_end; Scaning++)
            {
                Scan_status = Scaning;
                Scan_progress = Scan_status * 1.0 / scan_max_addr;
                BaseNode n = new BaseNode((byte)Scaning, this);
                if (n.Is_hareware_exist)
                {
                    classificationNode(n);
                }
                else
                {
                    nodeUnregister(n);
                }
                if (scan_stop)
                {
                    Scan_status = -2;
                    return;
                }
            }
            Scan_status = -3;
        }
        public void autoSetAddressLoop()
        {
            int new_addr = 10;
            for (int i = 100; i < 164; i++)
            {
                Scan_status = i;
                Scan_progress = Scan_status * 1.0 / scan_max_addr;
                if (Nodes[i] != null)
                {
                    while (Nodes[new_addr] != null)
                    {
                        new_addr++;
                    }
                    if (new_addr >= 100)
                    {
                        throw new Exception("Auto set addr error, Addr is high than 100");
                    }
                    Nodes[i].changeAddr((byte)new_addr);
                    new_addr++;
                }
                if (scan_stop)
                {
                    Scan_status = -2;
                    return;
                }
            }
            Scan_status = -3;
        }
    }
}
