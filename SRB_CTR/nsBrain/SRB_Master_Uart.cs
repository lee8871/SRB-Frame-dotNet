using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SRB_CTR.nsBrain
{
    public class SRB_Master_Uart : SRB_Master
    {

        ComPort mainComPort;
        SRB_master_uart_form config_form;
        public string getPortName()
        {
            if (Is_opened())
            {
                return mainComPort.Port;
            }
            else
            {
                return "---";
            }

        }

        bool record_port_data = false;
        public bool Record_port_data
        {
            get { return record_port_data; }
            set { record_port_data = value; }
        }
        public SRB_Master_Uart()
        {
            mainComPort = new ComPort();
            mainComPort.BaudRate = 115200;
            OpenPort("com5");
        }
        public override System.Windows.Forms.Form getConfigForm()
        {
            if(config_form == null)
            {
                config_form = new SRB_master_uart_form(this);
            }
            return config_form;
        }

        internal void configFormClosed()
        {
            config_form = null;
        }


        public override bool Is_opened()
        {
            return mainComPort.Opened; 
        }

        internal void OpenPort(string portName)
        {
            ClosePort();
            mainComPort.Port = portName;
            mainComPort.Open();
        }

        internal void ClosePort()
        {
            if (Is_opened())
            {
                mainComPort.Close();
                mainComPort.ClearReceiveBuf();
                mainComPort.ClearSendBuf();
            }
        }



        internal string[] getPortTable()
        {
            return System.IO.Ports.SerialPort.GetPortNames();
        }
        byte[] all_bytes_buffer = new byte[128 * 74];
        byte[] one_ac_bytes_buffer = new byte[74];
        Access[] acs;
        int acs_num;



        int last_send_time_cost = 0;
        public int Last_send_time_cost
        {
            get { return last_send_time_cost; }
        }

        int last_recv_time_cost = 0;
        public int Last_recv_time_cost
        {
            get { return last_recv_time_cost; }
        }

        long time_record;
        
        public override bool doAccess(Access[] acs,int acs_num)
        {
            if (this.Is_opened() == false)
            {
                for (int acs_counter = 0; acs_counter < acs_num; acs_counter++)
                {
                    acs[acs_counter].status = Access.Status.SendFail;
                }
                return false;
            }
            if(acs_num>128) 
            {
                throw new Exception(string.Format("Max num of accesses to send is 128"));
            }
            original_send_ba = null;
            original_recv_ba = null;
            this.acs = acs;
            this.acs_num = acs_num;
            time_record = Stopwatch.GetTimestamp();

            sendAccess();

            last_send_time_cost = (int)(Stopwatch.GetTimestamp() - time_record);
            time_record += last_send_time_cost;

            recvAccess();

            last_recv_time_cost = (int)(Stopwatch.GetTimestamp() - time_record);
            time_record += last_recv_time_cost;

            for (int acs_counter = 0; acs_counter < acs_num; acs_counter++)
            {
                if (acs[acs_counter].status == Access.Status.Sended)
                {
                    acs[acs_counter].status = Access.Status.NoRecv;
                }
                acs[acs_counter].original_SendByte = original_send_ba;
                acs[acs_counter].original_RecvByte = original_recv_ba;
            }
            return true;
        }



        private byte[] original_send_ba;
        private byte[] original_recv_ba;

        private void sendAccess()
        {
            int one_ac_buffer_len = 0;
            int send_buffer_counter = 0;
            for(int acs_counter = 0; acs_counter<acs_num; acs_counter++)
            {
                Access ac = acs[acs_counter];
                one_ac_buffer_len = ac.toUartByteArray(ref one_ac_bytes_buffer, (byte)acs_counter);
                for (int i = 0; i < one_ac_buffer_len; i++) 
                {
                    byte b = one_ac_bytes_buffer[i];
                    all_bytes_buffer[send_buffer_counter++] = b;
                }
                DateTime t = DateTime.Now;
                ac.sendTime = t;
                ac.status = Access.Status.Sended;
            }
            if (record_port_data)
            {
                original_send_ba = new byte[send_buffer_counter];
                Array.Copy(all_bytes_buffer, original_send_ba, send_buffer_counter);
            }
            this.mainComPort.Write(ref all_bytes_buffer, send_buffer_counter);
        }


        int recv_acs_num;
        long recv_begin_time;
        byte[] recv_temp = new byte[100];
        int recv_counter;
        private void recvAccess()
        {
            int recv_buffer_counter = 0;
            bool Escaping = false; 

            current_sno = 0xf8;
            recv_ac_counter = -1;

            recv_ac_length = 0;
            recv_acs_num = 0;
            recv_begin_time = Stopwatch.GetTimestamp();

            while (true)
            {
                recv_counter = this.mainComPort.Read(ref recv_temp, 100);

                if (record_port_data)
                {
                    for (int i = 0; i < recv_counter; i++)
                    {
                        all_bytes_buffer[recv_buffer_counter++] = recv_temp[i];
                    }
                }

                if (Stopwatch.GetTimestamp() > (recv_begin_time + 100000))
                {
                    if (recv_counter == 0)
                    {
                        break;
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
                    break ;
                }
            }
            if (record_port_data)
            {
                original_recv_ba = new byte[recv_buffer_counter];
                Array.Copy(all_bytes_buffer, original_recv_ba, recv_buffer_counter);
            }
        }
        byte current_sno ;
        int recv_ac_counter;
        int recv_ac_length;
        private void recvSno(byte sno)
        {
            recv_ac_counter = 0;
            current_sno = sno;
            acs[current_sno].receiveSno();
            acs[current_sno].status = Access.Status.RecvedMiss;
        }
        private void recvData(byte data)
        {
            if (recv_ac_counter == -1)
            {
                return;
            }
            if (recv_ac_counter == 0)
            {
                recv_ac_length = data & 0x1f;
            }
            one_ac_bytes_buffer[recv_ac_counter++] = data;
            if (recv_ac_counter == recv_ac_length)
            {
                acs[current_sno].fromUartGetBytes(one_ac_bytes_buffer, recv_ac_counter);
                acs[current_sno].status = Access.Status.RecvedDone;
                recv_ac_counter = -1;
                recv_acs_num++;
            }

        }
    }
}
