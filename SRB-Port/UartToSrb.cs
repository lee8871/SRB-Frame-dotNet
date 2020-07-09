using SRB.Frame;
using System;
using System.Diagnostics;
using System.IO.Ports;

namespace SRB.port
{
    public class UartToSrb : IBus
    {
        private SerialPort mainComPort;

        //ComPort mainComPort;
        private UartToSrb_uc config_form;
        public string getPortName()
        {
            if (Is_opened)
            {
                return mainComPort.PortName;
            }
            else
            {
                return "---";
            }

        }

        private bool record_port_data = true;
        public bool Record_port_data
        {
            get { return record_port_data; }
            set { record_port_data = value; }
        }
        public UartToSrb()
        {
            mainComPort = new SerialPort();
            mainComPort.BaudRate = 2500000;
            mainComPort.RtsEnable = true;
            mainComPort.ReadTimeout = 2;
            string[] port_names = getPortTable();
            if (port_names.Length != 0)
            {
                switch (port_names[0])
                {
                    case "COM1":
                    case "COM2":
                        if (port_names.Length >= 2)
                        {
                            openPort(port_names[1]);
                        }
                        break;
                    default:
                        openPort(port_names[0]);
                        break;
                }
            }
        }
        public override System.Windows.Forms.Control getConfigControl()
        {
            if (config_form == null)
            {
                config_form = new UartToSrb_uc(this);
            }
            return config_form;
        }

        internal void configFormClosed()
        {
            config_form = null;
        }


        public override bool Is_opened => mainComPort.IsOpen;


        internal void openPort(string portName)
        {
            ClosePort();
            mainComPort.PortName = portName;
            try
            {
                mainComPort.Open();
            }
            catch { }
        }

        private void OpenPort()
        {
            ClosePort();
            try
            {
                mainComPort.Open();
            }
            catch { }
        }

        internal void ClosePort()
        {
            if (Is_opened)
            {
                mainComPort.Close();
            }
        }



        internal string[] getPortTable()
        {
            return System.IO.Ports.SerialPort.GetPortNames();
        }

        private byte[] all_bytes_buffer = new byte[128 * 74];
        private byte[] one_ac_bytes_buffer = new byte[74];
        private Access[] acs;
        private int acs_num;
        private int last_send_time_cost = 0;
        public int Last_send_time_cost => last_send_time_cost;

        private int last_recv_time_cost = 0;
        public int Last_recv_time_cost => last_recv_time_cost;

        private long time_record;
        protected override bool doAccess(Access ac)
        {
            Access[] acs = new Access[1];
            acs[0] = ac;
            return doAccess(acs, 1);
        }
        protected override bool doAccess(Access[] acs, int acs_num = -1)
        {
            if (acs_num == -1)
            {
                acs_num = acs.Length;
            }
            if (this.Is_opened == false)
            {
                OpenPort();
                if (this.Is_opened == false)
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
                throw new ArgumentException("Max num of accesses to send is 128", "acs_num");
            }
            //original_send_ba = null;
            //original_recv_ba = null;
            this.acs = acs;
            this.acs_num = acs_num;

            time_record = Stopwatch.GetTimestamp();

            if (uartSendAccess() == true)
            {
                last_send_time_cost = (int)(Stopwatch.GetTimestamp() - time_record);
                time_record += last_send_time_cost;
                if (recvAccess() == true)
                {
                    last_recv_time_cost = (int)(Stopwatch.GetTimestamp() - time_record);
                    time_record += last_recv_time_cost;
                    return true;
                }
            }
            OpenPort();
            for (int acs_counter = 0; acs_counter < acs_num; acs_counter++)
            {
                if (acs[acs_counter].Status == Access.StatusEnum.SendWaitRecv)
                {
                    acs[acs_counter].sendFail();
                }
            }
            return true;
        }









        //private byte[] original_send_ba;
        //private byte[] original_recv_ba;
        private int send_buffer_counter = 0;

        private bool uartSendAccess()
        {
            send_buffer_counter = 0;
            for (int acs_counter = 0; acs_counter < acs_num; acs_counter++)
            {
                Access ac = acs[acs_counter];

                long ET_send = Stopwatch.GetTimestamp();
                toUartByteArray(ac, (byte)acs_counter);
                ac.sendDone(ET_send);
            }
            //if (record_port_data)
            //{
            //    //UartRawData raw = new UartRawData();
            //    //original_send_ba = new byte[send_buffer_counter];
            //    //Array.Copy(all_bytes_buffer, original_send_ba, send_buffer_counter);
            //}
            try
            {
                this.mainComPort.Write(all_bytes_buffer, 0, send_buffer_counter);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private int toUartByteArray(Access ac, byte sno)
        {
            int i = 0;
            all_bytes_buffer[send_buffer_counter++] = 0xf5;
            all_bytes_buffer[send_buffer_counter++] = sno;
            all_bytes_buffer[send_buffer_counter++] = ac.Addr;
            if (0xf5 == (all_bytes_buffer[send_buffer_counter++] = ac.Send_bfc))
            {
                all_bytes_buffer[send_buffer_counter++] = 0xf3;
            }
            foreach (byte b in ac.Send_data)
            {
                if (0xf5 == (all_bytes_buffer[send_buffer_counter++] = b))
                {
                    all_bytes_buffer[send_buffer_counter++] = 0xf3;
                }
            }
            if (0xf5 == (all_bytes_buffer[send_buffer_counter++] = 0))
            {
                all_bytes_buffer[send_buffer_counter++] = 0xf3;
            }
            return i;
        }

        private int recv_acs_num;
        private long recv_begin_time;
        private byte[] recv_temp = new byte[100];
        private int recv_counter;
        private bool recvAccess()
        {
            // int recv_buffer_counter = 0;
            bool Escaping = false;

            current_sno = 0xf8;
            recv_ac_counter = -1;

            recv_ac_length = 0;
            recv_acs_num = 0;
            recv_begin_time = Stopwatch.GetTimestamp();

            while (true)
            {
                try
                {
                    recv_counter = mainComPort.BytesToRead;
                    this.mainComPort.Read(recv_temp, 0, recv_counter);
                }
                catch
                {
                    return false;
                }
                //if (record_port_data)
                //{
                //    for (int i = 0; i < recv_counter; i++)
                //    {
                //        all_bytes_buffer[recv_buffer_counter++] = recv_temp[i];
                //    }
                //}
                if (Stopwatch.GetTimestamp() > (recv_begin_time + 100000))
                {
                    if (recv_counter == 0)
                    {
                        return false;
                    }
                }
                for (int i = 0; i < recv_counter; i++)
                {
                    byte b = recv_temp[i];
                    if (b == 0xf5)
                    {
                        Escaping = true;
                        continue;
                    }
                    else
                    {
                        if (Escaping == true)
                        {
                            if (b == 0xf3)
                            {
                                recvData(0xf5);
                            }
                            else if (b == 0xf8)
                            {
                                //do nothing
                            }
                            else
                            {
                                recvSno(b);
                            }
                            Escaping = false;
                        }
                        else
                        {
                            recvData(b);
                        }
                    }
                }
                if (recv_acs_num == acs_num)
                {
                    //这是关于记录接收到的数据的代码,上面删掉了类似的if (record_port_data)
                    //{
                    //    original_recv_ba = new byte[recv_buffer_counter];
                    //    Array.Copy(all_bytes_buffer, original_recv_ba, recv_buffer_counter);
                    //}
                    return true;
                }
            }
        }

        private byte current_sno;
        private int recv_ac_counter;
        private int recv_ac_length;
        private void recvSno(byte sno)
        {
            recv_ac_counter = 0;
            current_sno = sno;
            // acs[current_sno].Status = Access.StatusEnum.RecvedBadPkg;
        }
        private void recvData(byte data)
        {
            if (recv_ac_counter == -1)
            {
                return;
            }
            if (recv_ac_counter == 0)
            {
                recv_ac_length = (data & 0x1f) + 2;
            }
            one_ac_bytes_buffer[recv_ac_counter++] = data;
            if (recv_ac_counter == recv_ac_length)
            {
                fromUartGetBytes(acs[current_sno], one_ac_bytes_buffer, recv_ac_counter);
                recv_ac_counter = -1;
                recv_acs_num++;
            }
        }

        public void fromUartGetBytes(Access ac, byte[] bytes, int length)
        {
            byte bfc = bytes[0];
            int len = (int)(bfc & 0x1f);
            if (len + 2 != length)
            {
                return;
            }
            byte[] data = new byte[len];
            for (int i = 0; i < len; i++)
            {
                data[i] = bytes[i + 1];
            }
            ac.receiveAccess(bfc, data);
        }

    }
}
