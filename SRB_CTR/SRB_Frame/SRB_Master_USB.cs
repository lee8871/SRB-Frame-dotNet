using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
    

using LibUsbDotNet;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;
using LibUsbDotNet.LudnMonoLibUsb;
using EC = LibUsbDotNet.Main.ErrorCode;


namespace SRB_CTR.SRB_Frame
{
    class SRB_Master_USB : ISRB_Driver
    {
        private UsbDevice selected_device;
        private string selected_device_name;
        private UsbEndpointReader srb_reader;
        private UsbEndpointWriter srb_writer;
        private Dictionary<string, UsbRegistry> devicesDIC = new Dictionary<string,UsbRegistry>();
        SRB_Master_USB_Uc config_form;

        Stopwatch stopwatch;
        Log_Writer log;
        public SRB_Master_USB()
        {
            scanDevice();
            if (devicesDIC.Count == 1)
            {
                OpenPort(devicesDIC.Keys.ToArray()[0]);
            }
            log = new Log_Writer("USB");
            log.add("# new usb log!");
            log.add(string.Format("Date = '{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            log.add(string.Format("Time = '{0}'", DateTime.Now.ToString("HH:mm:ss")));
            log.autoFlushRun();
            stopwatch = new Stopwatch();
        }


        public string getPortName()
        {
            if (Is_opened())
            {
                return selected_device_name;
            }
            else
            {
                return "---";
            }

        }
        bool record_port_data = true;
        public bool Record_port_data
        {
            get { return record_port_data; }
            set { record_port_data = value; }
        }
        public override System.Windows.Forms.Control getConfigControl()
        {
            if (config_form == null)
            {
                config_form = new SRB_Master_USB_Uc(this);
            }
            return config_form;
        }

        internal void configFormClosed()
        {
            config_form = null;
        }


        public override bool Is_opened()
        {
            if (selected_device == null)
            {
                return false;
            }
            return selected_device.IsOpen;
        }

        internal void OpenPort(string portName)
        {
            lock (lock_access)
            {
                ClosePort();
                selected_device = devicesDIC[portName].Device;
                selected_device_name = portName;
                if (!(selected_device.Open()))
                {
                    throw new Exception(string.Format("open USB (port)device {0} fail", portName));
                }
                IUsbDevice wholeUsbDevice = selected_device as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null))
                {
                    // This is a "whole" USB device. Before it can be used, 
                    // the desired configuration and interface must be selected.
                    // Select config #1
                    wholeUsbDevice.SetConfiguration(1);
                    // Claim interface #0.
                    wholeUsbDevice.ClaimInterface(0);
                }
                else
                {
                    throw new Exception(string.Format("selected_device can not as IUsbDevice"));
                }
                srb_reader = selected_device.OpenEndpointReader((ReadEndpointID)(1 | 0x80), 1000, EndpointType.Interrupt);
                srb_writer = selected_device.OpenEndpointWriter((WriteEndpointID)2);
                // srb_reader.DataReceived += mEp_DataReceived;
                srb_reader.Flush();
            }
        }

        internal void ClosePort()
        {
            lock (lock_access)
            {
                if (Is_opened())
                {

                    srb_reader.DataReceivedEnabled = false;
                    // srb_reader.DataReceived -= mEp_DataReceived;
                    srb_reader.Dispose();
                    srb_reader = null;
                    srb_writer.Abort();
                    srb_writer.Dispose();
                    srb_writer = null;

                    IUsbDevice wholeUsbDevice = selected_device as IUsbDevice;
                    if (!ReferenceEquals(wholeUsbDevice, null))
                    {
                        // Release interface #0.
                        wholeUsbDevice.ReleaseInterface(0);
                    }

                    selected_device.Close();
                }
            }
        }



        internal string[] getPortTable()
        {
            scanDevice();
            return devicesDIC.Keys.ToArray();

        }
        private void scanDevice()
        {
            devicesDIC.Clear();
            UsbRegDeviceList mRegDevices;
            mRegDevices = UsbDevice.AllLibUsbDevices;
            string product_name;
            string device_name;
            foreach (UsbRegistry regDevice in mRegDevices)
            {
                regDevice.Device.GetString(out product_name, 0x0409, 2);
                if (product_name == "SRB-USB")
                {
                    regDevice.Device.GetString(out device_name, 0x0409, 3);
                    devicesDIC[device_name] = regDevice;
                }
            }
        }

        public override bool doAccess(Access ac)
        {
            Access[] acs = new Access[1];
            acs[0] = ac;
            return doAccess(acs, 1);
        }


        #region Access

        Access[] accesses;
        int send_access_counter;
        int recv_access_counter;
        int access_num;

        object lock_access = new object();
        public override bool doAccess(Access[] acs, int acs_num = -1)
        {
            lock (lock_access)
            {
                if (acs_num == -1)
                {
                    acs_num = acs.Length;
                }
                if (this.Is_opened() == false)
                {
                    for (int acs_counter = 0; acs_counter < acs_num; acs_counter++)
                    {
                        acs[acs_counter].sendFail();
                    }
                    return false;
                }
                if (acs_num > 128)
                {
                    throw new Exception(string.Format("Max num of accesses to send is 128"));
                }
                if (acs_num == 0)
                {
                    return false;
                }

                accesses = acs;
                access_num = acs_num;
                log.add("# new access");

                stopwatch.Restart();
                recv_access_counter = send_access_counter = 0;
                sendAccess();
                while (recv_access_counter != access_num)
                {
                    sendAccess();
                    recvAccess();
                }
                stopwatch.Stop();
                log.add(string.Format("Num_spend = ({0},{1:###0.0000})", acs_num, stopwatch.getElapsedMs()));
                
                return true;
            }
        }

        byte[] send_to_usb_buf = new byte[64];
        private void sendAccess()
        {
            if(send_access_counter >= access_num)
            {
                return;
            }

            Access access = accesses[send_access_counter];

            int i = 0;
            send_to_usb_buf[i++] = (byte)send_access_counter;
            send_to_usb_buf[i++] = access.Addr;
            send_to_usb_buf[i++] = access.Send_bfc;

            foreach(byte b in access.Send_data)
            {
                send_to_usb_buf[i++] = b;
            }
            int send_done_len;
            int send_len = i;
            if (srb_writer.Write(send_to_usb_buf, 0, send_len, 10, out send_done_len) == ErrorCode.None)
            {
                log.add(string.Format("Send_pkg= ({0},{1})",send_len,send_to_usb_buf.ToPythonTuple(send_len)));
                access.sendDone();
            }
            else
            {
                log.add("Send_pkg = ('fail',)");
                access.sendFail();
            }
            send_access_counter++;
        }
        byte[] recv_from_usb_buf = new byte[64];
        private bool recvAccess()
        {
            int recv_num;
            if (srb_reader.Read(recv_from_usb_buf, 10, out recv_num) == ErrorCode.None)
            {
                recv_access_counter++;
                log.add(string.Format("Recv_pkg =  ({0},{1})", recv_num, recv_from_usb_buf.ToPythonTuple(recv_num)));
                int recv_sno = recv_from_usb_buf[0];
                byte recv_error = recv_from_usb_buf[1];
                if(recv_error==0)
                {
                    byte[] recv_access_data = new byte[recv_num - 3];
                    accesses[recv_sno].receiveAccess(recv_from_usb_buf[2], recv_from_usb_buf,3);
                }
            }
            else
            {
                recv_access_counter = access_num;
                log.add("Recv_pkg = ('fail',)");
            }

            return false;
        }

        #endregion

    }
}
