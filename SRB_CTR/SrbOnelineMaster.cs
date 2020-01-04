
using SRB.Frame;
using SRB.port;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SRB_CTR
{
    //新版本的测试
    public partial class SrbOnelineMaster : IDisposable
    {

        private IBus bus;
        public IBus Bus => bus;

        private mainForm _nodes_form;
        public mainForm Nodes_form => _nodes_form;

        private IBrain main_brain;

        private UpdateAll update_all;
        public bool Is_calculation_running => main_brain.Is_running;

        public UpdateAll Update_all => update_all;

        SRB_Record record = new SRB_Record();

        public SrbOnelineMaster()
        {
            ///TODO
            ///add master select 
            bus = new UsbToSrb();
            main_brain = new nsBrain.Brain_Test2(this);
            update_all = new UpdateAll(this);
            _nodes_form = new mainForm(this);
            _nodes_form.Disposed += _nodes_form_Disposed;
            bus.Record = record;

        }
        public System.Windows.Forms.Control usbControlDisplay()
        {
            if (!(bus is UsbToSrb))
            {
                bus = new UsbToSrb();
            }
            return bus.getConfigControl();
        }
        public System.Windows.Forms.Control uartControlDisplay()
        {
            if (!(bus is UartToSrb))
            {
                bus = new UartToSrb();
            }
            return bus.getConfigControl();
        }
        public bool isHighSpeedSupporting()
        {
            if (bus is UsbToSrb)
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

        internal void runCalculation()
        {
            main_brain.run();
        }

        internal void stopCalculation()
        {
            main_brain.stop();
        }


        internal void ledAddrAll(SRB.Frame.BaseNode.AddressCluster.LedAddrType type)
        {
            SRB.Frame.BaseNode.AddressCluster.ledAddrBroadcast(type, bus);
        }

        internal void resetAllAddress()
        {
            SRB.Frame.BaseNode.AddressCluster.randomAddrAll(bus);
        }
        internal void resetNewNodeAddress()
        {
            SRB.Frame.BaseNode.AddressCluster.randomAddrNewNode(bus);
        }

        internal void testUpdate(byte address)
        {
            BaseNode n = bus.createNode(address);
            if (n == null)
            {
                n = new BaseNode(address, bus);
            }
           // UpdateForm uf = new UpdateForm(n);
           // uf.Show();
        }
    }

    public partial class SrbOnelineMaster
    {
        private bool Is_scan_running
        {
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

        private Thread scan_thread;
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
        private bool scan_stop = true;
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
        private int scan_begin;
        private int scan_end;
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
        private void scanNodeLoop()
        {
            bus.removeAllNode();
            for (int scan_num = scan_begin; scan_num < scan_end; scan_num++)
            {
                Scan_status = scan_num;
                Scan_progress = Scan_status * 1.0 / scan_max_addr;
                BaseNode n = bus.createTempNode((byte)scan_num);
                n.checkNodeAccessable();
                if (n.Is_hareware_exist)
                {
                    bus.addTempNode();
                    Nodes_form.addNode(n);
                }
                if (scan_stop)
                {
                    Scan_status = -2;
                    return;
                }
            }
            Scan_status = -3;
        }
        public void scanUpdateNodes(int begin = 0, int end = -1)
        {
            if (end < 0)
            {
                end = scan_max_addr;
            }
            scan_end = end;
            scan_begin = begin;
            if (Is_scan_running == false)
            {
                scan_thread = new Thread(new ThreadStart(scanUpdateNodeLoop));
                scan_stop = false;
                scan_thread.Start();
            }
            return;
        }

        private void scanUpdateNodeLoop()
        {
            for (int scan_num = scan_begin; scan_num < scan_end; scan_num++)
            {
                Scan_status = scan_num;
                Scan_progress = Scan_status * 1.0 / scan_max_addr;
                if (bus[scan_num] == null)
                {
                    BaseNode n = bus.createTempNode((byte)scan_num);
                    n.checkNodeUpdateable();
                    if (n.Is_in_update)
                    {
                        bus.addTempNode();
                        Nodes_form.addNode(n);
                    }
                    if (scan_stop)
                    {
                        Scan_status = -2;
                        return;
                    }
                }
            }
            Scan_status = -3;
        }

        public void autoSetAddressLoop()
        {
            byte new_addr = 10;
            for (byte i = 100; i < 164; i++)
            {
                Scan_status = i;
                Scan_progress = Scan_status * 1.0 / scan_max_addr;
                if (bus[i] != null)
                {
                    while (bus[new_addr] != null)
                    {
                        new_addr++;
                    }
                    if (new_addr >= 100)
                    {
                        throw new Exception("Auto set addr error, Addr is high than 100");
                    }
                    bus[i].changeAddr((byte)new_addr);
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
