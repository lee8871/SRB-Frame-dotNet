
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
        private Node.SrbUpdater.Broadcast update_all;
        public bool Is_calculation_running => main_brain.Is_running;
        public Node.SrbUpdater.Broadcast Update_all => update_all;
        SRB_Record record = new SRB_Record();
        public Node.SyncCluster.Broadcast sync_bc;
        public Node.AddressCluster.Broadcast address_bc;
        public SrbOnelineMaster()
        {
            ///TODO
            ///add master select 
            bus = new UsbToSrb();
            main_brain = new nsBrain.Brain_Test2(this);
            update_all = new Node.SrbUpdater.Broadcast(bus);
            _nodes_form = new mainForm(this);
            _nodes_form.Disposed += _nodes_form_Disposed;
            bus.Record = record;
            sync_bc = new Node.SyncCluster.Broadcast(Bus);
            address_bc = new Node.AddressCluster.Broadcast(Bus);
            bus.eNodeAdd += Bus_eNodeAdd;  
        }

        private void Bus_eNodeAdd(IBus bus, Node n)
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
        internal void ledAddrAll(SRB.Frame.Node.AddressCluster.LedAddrType type)
        {
            SRB.Frame.Node.AddressCluster.ledAddrBroadcast(type, bus);
        }

        internal void resetAllAddress()
        {
            SRB.Frame.Node.AddressCluster.randomAddrAll(bus);
        }
        internal void resetNewNodeAddress()
        {
            SRB.Frame.Node.AddressCluster.randomAddrNewNode(bus);
        }





        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    bus.closeAllUser();
                    endRecord();
                    Log_Writer.No_exit_flag = false;
                }
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~SrbOnelineMaster()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
