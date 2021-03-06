﻿using LibUsbDotNet;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.Main;
using SRB.Frame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SRB.port
{
    public partial class UsbToSrb : IBus, IDisposable
    {
        private const int idVendor = 0x16c0;
        private const int idProduct = 0x05dc;
        public IDeviceNotifier UsbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
        private Queue<object> oldDevice = new Queue<object>();
        private Dictionary<string, UsbRegistry> devicesDIC = new Dictionary<string, UsbRegistry>();
        private UsbToSrb_uc config_form;
        private UsbDevice selected_device;
        private UsbEndpointReader srb_reader;
        private UsbEndpointWriter srb_writer;

        private string last_device_name = null;
        private object lock_access = new object();
        public UsbToSrb()
        {
            UsbDeviceNotifier.OnDeviceNotify += UsbDeviceNotifier_OnDeviceNotify;
            scanDevice();
            if (devicesDIC.Count == 1)
            {
                openPort(devicesDIC.Keys.ToArray()[0]);
            }
        }

        private void UsbDeviceNotifier_OnDeviceNotify(object sender, DeviceNotifyEventArgs e)
        {
            if(e.Device == null)
            {
                return;
            }
            if (e.Device.SerialNumber == last_device_name)
            {
                if (e.EventType == EventType.DeviceRemoveComplete)
                {
                    this.ClosePort();
                }
                if (e.EventType == EventType.DeviceArrival)
                {
                    this.reopenPort();
                }
            }
        }

        public string Port_name
        {
            get
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

        }
        public string Selected_device_name
        {
            get
            {
                string device_name;
                try
                {
                    if (selected_device != null)
                    {
                        selected_device.GetString(out device_name, 0x0409, 4);
                    }
                    else
                    {
                        return null;
                    }                   
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
                config_form = new UsbToSrb_uc(this);
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
                    throw new DriveNotFoundException(string.Format("open USB device(port) {0} fail", portName));
                }
                IUsbDevice wholeUsbDevice = selected_device as IUsbDevice;
                if (wholeUsbDevice is object)
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
                    throw new DriveNotFoundException(string.Format("selected_device can not as IUsbDevice"));
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
                    throw new DriveNotFoundException(string.Format("open USB (port)device {0} fail", Selected_device_name));
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
                    throw new DriveNotFoundException(string.Format("selected_device can not as IUsbDevice"));
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
                last_device_name = text;
            }
            else
            {
                throw new DriveNotFoundException("The USB-SRB is not oppend.");
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
            if (mRegDevices != null)
            {
                foreach (UsbRegistry regDevice in mRegDevices)
                {
                    if (regDevice.Device != null)
                    {
                        if ((regDevice.Vid != idVendor) || (regDevice.Pid != idProduct))
                        {
                            continue;
                        }
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

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (srb_reader!=null)
                    {
                        srb_reader.Dispose();
                    }
                    if (srb_writer != null)
                    {
                        srb_writer.Dispose();
                    }

                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~UsbToSrb()
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

    public partial class UsbToSrb : IBus
    {
        private const int access_bank_length = 256;
        private Access[] acs = new Access[access_bank_length];
        private LoopQueuePointer out_point = new LoopQueuePointer(access_bank_length);
        private LoopQueuePointer in_point = new LoopQueuePointer(access_bank_length);

        protected override bool doAccess(Access ac)
        {
            lock (lock_access)
            {
                acs[in_point] = ac;
                in_point++;
                return accessLoop();
            }

        }

        private int SEND_FAIL_ERROR = 1;
        private int USB_TIMEOUT = 2;
        protected override bool doAccess(Access[] acs, int acs_num = -1)
        {
            lock (lock_access)
            {
                if (acs_num == -1)
                {
                    acs_num = acs.Length;
                }
                if (acs_num > access_bank_length)
                {
                    throw new ArgumentException(string.Format("Max num of accesses to send is 128"));
                }
                if (acs_num == 0)
                {
                    return false;
                }
                for (int i = 0; i < acs_num; i++)
                {
                    this.acs[in_point] = acs[i];
                    in_point++;
                }
                return accessLoop();
            }
        }
        private bool accessLoop()
        {
            lock (lock_access)
            {
                if (this.Is_opened == false)
                {
                    //if (reopenPort() != true)
                    //{
                    LoopQueuePointer send_fail_point = new LoopQueuePointer(out_point);
                    while (in_point != send_fail_point)
                    {
                        acs[send_fail_point].portCloseSendFail();
                        send_fail_point++;
                    }
                    out_point.jumpTo(in_point);
                    return false;
                    //}
                }
                int access_error_counter = 0;

                LoopQueuePointer send_point = new LoopQueuePointer(out_point);

                while (sendAccess(send_point) == false)
                {
                    if (access_error_counter++ >= SEND_FAIL_ERROR) goto accessLoop_sendfail;
                }
                send_point++;

                while (true)
                {
                    if (in_point != send_point)
                    {
                        while (sendAccess(send_point) == false)
                        {
                            if (access_error_counter++ >= SEND_FAIL_ERROR) goto accessLoop_sendfail;
                        }
                        send_point++;
                    }
                    else
                    {
                        if (isPkgWaitRecv() == false)
                        {
                            out_point.jumpTo(in_point);
                            if (access_error_counter != 0)
                            {
                                Console.WriteLine("access error counter is " + access_error_counter);
                            }
                            return true;
                        }
                    }
                    while (recvAccess() == false)
                    {
                        if (access_error_counter++ >= SEND_FAIL_ERROR) goto accessLoop_sendfail;
                    }
                }
            accessLoop_sendfail:
                Console.WriteLine("USB: send fail" + DateTime.Now);
                USB_TIMEOUT++;
                Console.WriteLine("USB_TIMEOUT is setted to " + USB_TIMEOUT);
                //reopenPort();
                LoopQueuePointer sent_fail_point = new LoopQueuePointer(out_point);
                while (in_point != sent_fail_point)
                {
                    if (acs[sent_fail_point].Status == AccessStatus.SendWaitRecv)
                    {
                        acs[sent_fail_point].sendDoneRecvFail();
                    }
                    sent_fail_point++;
                }
                out_point.jumpTo(in_point);
                return false;
            }
        }
        private bool isPkgWaitRecv()
        {
            int i = out_point;
            while( i!= in_point) 
            {
                Access a = acs[i];
                i++;
                i %= access_bank_length;
                if (a != null)
                {
                    if (a.Status == AccessStatus.SendWaitRecv)
                    {
                        return true;
                    }
                    if (a.Status == AccessStatus.NoSend)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private byte[] send_to_usb_buf = new byte[64];
        private bool sendAccess(int point)
        {
            Access access = acs[point];
            int i = 0;
            send_to_usb_buf[i++] = (byte)point;
            send_to_usb_buf[i++] = access.Addr;
            send_to_usb_buf[i++] = access.Send_bfc;

            for(int j = 0;j<access.Send_data.Length;j++)
            {
                send_to_usb_buf[i++] = access.Send_data[j];
            }
            int send_done_len;
            int send_len = i;
            access.sendBegin();
            ErrorCode ec = srb_writer.Write(send_to_usb_buf, 0, send_len, USB_TIMEOUT, out send_done_len);
            switch (ec)
            {
                case ErrorCode.None:
                    access.sendDone();
                    return true;
                default:
                    return false;
            }
        }

        private byte[] recv_from_usb_buf = new byte[64];
        private bool recvAccess()
        {
            int recv_num;
            ErrorCode ec = srb_reader.Read(recv_from_usb_buf, USB_TIMEOUT, out recv_num);
            switch (ec)
            {
                case ErrorCode.None:
                    int recv_sno = recv_from_usb_buf[0];
                    if (acs[recv_sno] != null)
                    {
                        byte recv_error = recv_from_usb_buf[1];
                        if (recv_error < 0x0f)//thus recv_error is retry times
                        {
                            acs[recv_sno].receiveAccess(recv_error,recv_from_usb_buf[2], recv_from_usb_buf, 3);
                        }
                        else
                        {
                            switch (recv_error)
                            {
                                case 0xff:
                                    acs[recv_sno].receiveAccessBroadcast();
                                    break;
                                case 0xfe:
                                    acs[recv_sno].receiveAccessTimeout();
                                    break;
                                default:
                                    throw new AccessRecvBadValue("unknow receicve error code", acs[recv_sno]);
                            }
                        }
                    }
                    return true;
                default:
                    return false;
            }
        }
    }
}
