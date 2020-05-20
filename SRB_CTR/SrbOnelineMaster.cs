
using SRB.Frame;
using SRB.port;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace SRB_CTR
{
    public partial class SrbOnelineMaster : IDisposable
    {
        public static int scan_max_addr = 227;
        private IBus bus;
        public IBus Bus => bus;

        private mainForm _nodes_form;
        public mainForm Nodes_form => _nodes_form;

        private IBrain main_brain;

        private BaseNode.SrbUpdater.Broadcast update_all;
        public bool Is_calculation_running => main_brain.Is_running;

        public BaseNode.SrbUpdater.Broadcast Update_all => update_all;

        SRB_Record record = new SRB_Record();

        public BaseNode.SyncCluster.Broadcast sync_bc;
        public BaseNode.AddressCluster.Broadcast address_bc;
        public SrbOnelineMaster()
        {
            ///TODO
            ///add master select 
            bus = new UsbToSrb();
            main_brain = new nsBrain.Brain_Test2(this);
            update_all = new BaseNode.SrbUpdater.Broadcast(bus);
            _nodes_form = new mainForm(this);
            _nodes_form.Disposed += _nodes_form_Disposed;
            bus.Record = record;
            sync_bc = new BaseNode.SyncCluster.Broadcast(Bus);
            address_bc = new BaseNode.AddressCluster.Broadcast(Bus);
            bus.eNodeAdd += Bus_eNodeAdd;  
        }

        private void Bus_eNodeAdd(IBus bus, BaseNode n)
        {
            Nodes_form.addNode(n);
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
            address_bc.Dispose();
            main_brain.stop();
            endRecord();
            Log_Writer.No_exit_flag = false;
            address_bc.Dispose();
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

    }
}
