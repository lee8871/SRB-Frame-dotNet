
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
using SRB.Frame;

namespace SRB.port
{
     partial class SRB_Master_USB : IDriver
    {
        private UsbDevice selected_device;
        private UsbEndpointReader srb_reader;
        private UsbEndpointWriter srb_writer;
        private Dictionary<string, UsbRegistry> devicesDIC = new Dictionary<string, UsbRegistry>();
        SRB_Master_USB_Uc config_form;
        private Queue<object> oldDevice = new Queue<object>();
        object lock_access = new object();
        public SRB_Master_USB()
        {
            scanDevice();
            if (devicesDIC.Count == 1)
            {
                openPort(devicesDIC.Keys.ToArray()[0]);
            }
        }


        public string getPortName()
        {
            if (Is_opened)
            {
                return Selected_device_name;
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

        public string Selected_device_name
        {
            get
            {
                string device_name;
                try
                {
                    selected_device.GetString(out device_name, 0x0409, 4);
                }
                catch
                {
                    return null;

                }
                return device_name;
            }
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


        public override bool Is_opened
        {
            get
            {
                if (selected_device == null)
                {
                    return false;
                }
                return selected_device.IsOpen;
            }
        }
        string last_device_name = null;
        internal bool openPort(string portName)
        {
            if (portName == Selected_device_name)
            {
                if (selected_device.IsOpen)
                {
                    return true;
                }
            }
            lock (lock_access)
            {
                ClosePort();
                UsbRegistry usb_reg;
                if (devicesDIC.TryGetValue(portName, out usb_reg) == false)
                {
                    return false;
                }
                selected_device = usb_reg.Device;
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
                last_device_name = portName;

                return true;
            }
        }

        internal bool reopenPort()
        {
            lock (lock_access)
            {
                ClosePort();
                scanDevice();
                UsbRegistry usb_reg;
                if (devicesDIC.TryGetValue(last_device_name, out usb_reg) == false)
                {
                    return false;
                }
                selected_device = usb_reg.Device;
                if (!(selected_device.Open()))
                {
                    throw new Exception(string.Format("open USB (port)device {0} fail", Selected_device_name));
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
                return true;
            }
        }
        public override void checkPort()
        {
        }

        internal void ClosePort()
        {
            lock (lock_access)
            {
                if (Is_opened)
                {
                    oldDevice.Enqueue(srb_reader);
                    srb_reader.Flush();
                    srb_reader.Dispose();
                    srb_reader = null;
                    oldDevice.Enqueue(srb_writer);
                    srb_writer.Flush();
                    srb_writer.Dispose();
                    srb_writer = null;
                    IUsbDevice wholeUsbDevice = selected_device as IUsbDevice;
                    if (!ReferenceEquals(wholeUsbDevice, null))
                    {
                        // Release interface #0.
                        wholeUsbDevice.ReleaseInterface(0);
                        wholeUsbDevice.ReleaseInterface((1 | 0x80));
                        wholeUsbDevice.ReleaseInterface(2);
                        wholeUsbDevice.Close();
                        oldDevice.Enqueue(selected_device);
                        selected_device.Close();
                        selected_device = null;
                    }

                }
            }
        }

        internal void changeName(string text)
        {
            if (Is_opened)
            {
                int len;
                byte[] data = new byte[64];
                int str_length = Encoding.Unicode.GetBytes(text, 0, text.Length, data, 2);
                data[0] = (byte)(str_length + 2);
                data[1] = 0x03;
                UsbSetupPacket setup = new UsbSetupPacket(0, 7, 0x0304, 0x0409, (short)(data.Length));
                selected_device.ControlTransfer(ref setup, data, data.Length, out len);
            }
            else
            {
                throw new Exception("The USB-SRB is not oppend.");
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
                if (regDevice.Device != null)
                {
                    regDevice.Device.GetString(out product_name, 0x0409, 2);
                    if (product_name == "SRB-USB")
                    {
                        regDevice.Device.GetString(out device_name, 0x0409, 4);
                        devicesDIC[device_name] = regDevice;
                    }
                }
            }
        }

    }
    partial class SRB_Master_USB : IDriver
    {
        Stopwatch stopwatch = new Stopwatch();



        Access[] accesses;


        int send_access_counter;
        int recv_access_counter;
        int access_num;
        int access_error_counter;

        public override bool doAccess(Access ac)
        {
            Access[] acs = new Access[1];
            acs[0] = ac;
            return doAccess(acs, 1);
        }
        public override bool doAccess(Access[] acs, int acs_num = -1)
        {
            lock (lock_access)
            {
                if (acs_num == -1)
                {
                    acs_num = acs.Length;
                }
                if (this.Is_opened == false)
                {
                    if (reopenPort() != true)
                    {
                        for (int acs_counter = 0; acs_counter < acs_num; acs_counter++)
                        {
                            acs[acs_counter].sendFail();
                        }
                        return false;
                    }
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
                access_error_counter = 0;
                stopwatch.Restart();
                recv_access_counter = send_access_counter = 0;
                sendAccess();
                while (recv_access_counter != access_num)
                {
                    sendAccess();
                    recvAccess();
                    if (access_error_counter >2)
                    {
                        if (reopenPort() == true)
                        {
                            access_error_counter = 0;
                        }
                        else
                        {
                            for (int acs_counter = recv_access_counter; acs_counter < acs_num; acs_counter++)
                            {
                                acs[acs_counter].sendFail();
                            }
                            stopwatch.Stop();
                            return false;
                        }
                    }
                }
                stopwatch.Stop();
              //  stopwatch.getElapsedMs;
                return true;
            }
        }

        byte[] send_to_usb_buf = new byte[64];
        private void sendAccess()
        {
            if (send_access_counter >= access_num)
            {
                return;
            }

            Access access = accesses[send_access_counter];

            int i = 0;
            send_to_usb_buf[i++] = (byte)send_access_counter;
            send_to_usb_buf[i++] = access.Addr;
            send_to_usb_buf[i++] = access.Send_bfc;

            foreach (byte b in access.Send_data)
            {
                send_to_usb_buf[i++] = b;
            }
            int send_done_len;
            int send_len = i;
            ErrorCode ec = srb_writer.Write(send_to_usb_buf, 0, send_len, 2, out send_done_len);
            switch (ec)
            {
                case ErrorCode.None:
                    DateTime t = DateTime.Now;
                    access.sendTime = t;
                    access.sendDone();
                    send_access_counter++;//发送成功了,转向下一个
                    break;
                default:
                    access_error_counter++;
                    //throw new Exception(ec.ToString());
                    break;
            }
        }


        byte[] recv_from_usb_buf = new byte[64];
        private bool recvAccess()
        {
            int recv_num;
            ErrorCode ec = srb_reader.Read(recv_from_usb_buf, 200, out recv_num);
            switch (ec)
            {
                case ErrorCode.None:
                    recv_access_counter++;
                    int recv_sno = recv_from_usb_buf[0];
                    byte recv_error = recv_from_usb_buf[1];
                    if (recv_error < 0x0f)//thus recv is retry times
                    {
                        accesses[recv_sno].receiveAccess(recv_from_usb_buf[2], recv_from_usb_buf, 3);
                    }                                            
                    else
                    {
                        switch (recv_error)
                        {
                            case 0xff:
                                accesses[recv_sno].receiveAccessBroadcast();
                                break;
                            case 0xfe:
                                accesses[recv_sno].receiveAccessTimeout();
                                break;
                            default:
                                throw new Exception("unknow receicve error code" + recv_error);
                        }
                    }
                    break;
                default:
                    //throw new Exception(ec.ToString());
                    access_error_counter++;
                    break;
            }
            return false;
        }
    }
}
