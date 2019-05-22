using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRB.Frame
{
    public class Access
    {
        //about Note
        public DateTime sendTime;
        public string description = "";

        private BaseNode sender_node;        
        public byte Addr
        {
            get
            {
                if (sender_node != null)
                {
                    return sender_node.Addr;
                }
                else
                {
                    return 0xff;
                }
            }
        }
        public enum StatusEnum {
            NoSend, PortColsed,
            SendWaitRecv, DeviceTimeOut,
            RecvedDone,  SrbTimeOut,BroadcasePkg ,RecvedBadPkg,
        };

        public void onAccessDone()
        {
            switch (_status)
            {
                case StatusEnum.BroadcasePkg:
                    break;
                case StatusEnum.RecvedDone:
                    sender_node.accessDone(this);
                    break;
                case StatusEnum.PortColsed:
                case StatusEnum.DeviceTimeOut:
                    //TODO: Bus Master device is not open or port is not selected
                    break;
                case StatusEnum.RecvedBadPkg:
                case StatusEnum.SrbTimeOut:
                    //TODO: may something wrong on bus,  is not my error so log it.
                    break;
                case StatusEnum.SendWaitRecv:
                case StatusEnum.NoSend:
                    throw new Exception("An access DONE called when " + _status.ToString() + "You should check SRB_MASTR.cs");
            }
        }


        private StatusEnum _status;
        public StatusEnum Status { get => _status; }


        public enum PortEnum { D0, D1, D2, D3, Cmd, Cgf, Rpt, Res };


        private PortEnum _port;
        public PortEnum Port { get => _port; }
    

        //_send_data is set by constructor
        private byte[] _send_data;
        public byte[] Send_data       {  get => _send_data; }

        //_recv_data is set by recv
        private byte[] _recv_data;

        public byte[] Recv_data       {  get => _recv_data; }
        
        public byte Send_bfc
        {
            get { return (byte)((int)_port * 32 + _send_data.Length); }
        }
        private byte _recv_bfc;

        public bool Recv_error
        {
            get { return (_recv_bfc & (1 << 7)) != 0; }
        }
        public bool Recv_busy
        {
            get { return (_recv_bfc & (1 << 6)) != 0; }
        }
        public bool Recv_event
        {
            get { return (_recv_bfc & (1 << 5)) != 0; }
        }
        public int Recv_data_len
        {
            get { return (int)(_recv_bfc & 0x1f); }
        }




        public Access(BaseNode n, PortEnum p, byte[] send_d)
        {
            sender_node = n;
            _send_data = send_d;
            _port = p;
            _status = StatusEnum.NoSend;
        }

        public bool receiveAccess(byte bfc, byte[] data, int offset)
        {
            if (Status == StatusEnum.SendWaitRecv)
            {
                this._recv_bfc = bfc;
                if ((data.Length - offset) > this.Recv_data_len)
                {
                    this._recv_data = new byte[this.Recv_data_len];
                    for (int i = 0; i < this.Recv_data_len; i++)
                    {
                        _recv_data[i] = data[i + offset];
                    }
                    _status = StatusEnum.RecvedDone;
                }
                else
                {
                    _status = StatusEnum.RecvedBadPkg;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void receiveAccess(byte bfc, byte[] data)
        {
            this._recv_bfc = bfc;
            this._recv_data = data;
            if ((data.Length) != this.Recv_data_len)
            {
                _status = StatusEnum.RecvedBadPkg;
            }
            else
            {
                _status = StatusEnum.RecvedDone;
            }
        }
        public void receiveAccessBroadcast()
        {
            if (this.Addr == 0xff)
            {
                _status = StatusEnum.BroadcasePkg;
            }
            else
            {
                _status = StatusEnum.RecvedBadPkg;
            }
        }
        public void receiveAccessTimeout()
        {
            if (this.Addr == 0xff)
            {
                _status = StatusEnum.RecvedBadPkg;
            }
            else
            {
                _status = StatusEnum.SrbTimeOut;
            }
        }



        public void sendDone()
        {
            _status = StatusEnum.SendWaitRecv;
        }

        public void sendFail()
        {
            switch(_status)
            {
                case StatusEnum.NoSend:
                    _status = StatusEnum.PortColsed;
                    break;
                case StatusEnum.SendWaitRecv:
                    _status = StatusEnum.DeviceTimeOut;
                    break;
                default:
                    break;
            }
            
        }


        
        public override string ToString()
        {
            string st_recv, st_send;
            st_recv = _recv_data.ToHexSt();
            st_send = _send_data.ToHexSt();
            string st = System.String.Format(
                 @"Addr:{0}.{1} Send:{2}Recv:{3}",
                 Addr.ToString(),
                 _port, st_send, st_recv);
            return st;
        }

        public string toHtml()
        {
            string st_recv, st_send;
            st_recv = _recv_data.ToHexSt();
            st_send = _send_data.ToHexSt();
            string st = System.String.Format(
                 @"
                <span class=note>Addr:</span>
                <span class=sno>{0}.{1} </span>
                <span class=note>Send:</span>
                <span class=send>{2}</span>
                <span class=note>Recv:</span>
                <span class=recv>{3}</span>
                <br>",
                 Addr.ToString(),
                 _port, st_send, st_recv);
            return st;
        }
        public string toJson()
        {
            string st = string.Format(
               "{7}\"Ts\":\"{0}\",\"Dsc\":\"{1}\",\"Addr\":{9},\"State\":\"{2}\",\r\n\"Send\":{7}\"bfc\":\"{3}\",\"data\":\"{4}\"{8},\r\n\"Recv\":{7}\"bfc\":\"{5}\",\"data\":\"{6}\"{8}\r\n{8}\r\n",
                sendTime.ToString("dd-HH:mm:ss:fffff"),
                this.description,
                this.Status.ToString(),
                this.Send_bfc.ToHexSt(),
                this.Send_data.ToHexSt(),
                this._recv_bfc.ToHexSt(),
                this.Recv_data.ToHexSt(),
                "{",
                "}",
                this.Addr.ToString()
            );
            return st;
        }





        private static byte[] Crc_table = 
		{
			0x00,0x31,0x62,0x53,0xc4,0xf5,0xa6,0x97,0xb9,0x88,0xdb,0xea,0x7d,0x4c,0x1f,0x2e,
			0x43,0x72,0x21,0x10,0x87,0xb6,0xe5,0xd4,0xfa,0xcb,0x98,0xa9,0x3e,0x0f,0x5c,0x6d,
			0x86,0xb7,0xe4,0xd5,0x42,0x73,0x20,0x11,0x3f,0x0e,0x5d,0x6c,0xfb,0xca,0x99,0xa8,
			0xc5,0xf4,0xa7,0x96,0x01,0x30,0x63,0x52,0x7c,0x4d,0x1e,0x2f,0xb8,0x89,0xda,0xeb,
			0x3d,0x0c,0x5f,0x6e,0xf9,0xc8,0x9b,0xaa,0x84,0xb5,0xe6,0xd7,0x40,0x71,0x22,0x13,
			0x7e,0x4f,0x1c,0x2d,0xba,0x8b,0xd8,0xe9,0xc7,0xf6,0xa5,0x94,0x03,0x32,0x61,0x50,
			0xbb,0x8a,0xd9,0xe8,0x7f,0x4e,0x1d,0x2c,0x02,0x33,0x60,0x51,0xc6,0xf7,0xa4,0x95,
			0xf8,0xc9,0x9a,0xab,0x3c,0x0d,0x5e,0x6f,0x41,0x70,0x23,0x12,0x85,0xb4,0xe7,0xd6,
			0x7a,0x4b,0x18,0x29,0xbe,0x8f,0xdc,0xed,0xc3,0xf2,0xa1,0x90,0x07,0x36,0x65,0x54,
			0x39,0x08,0x5b,0x6a,0xfd,0xcc,0x9f,0xae,0x80,0xb1,0xe2,0xd3,0x44,0x75,0x26,0x17,
			0xfc,0xcd,0x9e,0xaf,0x38,0x09,0x5a,0x6b,0x45,0x74,0x27,0x16,0x81,0xb0,0xe3,0xd2,
			0xbf,0x8e,0xdd,0xec,0x7b,0x4a,0x19,0x28,0x06,0x37,0x64,0x55,0xc2,0xf3,0xa0,0x91,
			0x47,0x76,0x25,0x14,0x83,0xb2,0xe1,0xd0,0xfe,0xcf,0x9c,0xad,0x3a,0x0b,0x58,0x69,
			0x04,0x35,0x66,0x57,0xc0,0xf1,0xa2,0x93,0xbd,0x8c,0xdf,0xee,0x79,0x48,0x1b,0x2a,
			0xc1,0xf0,0xa3,0x92,0x05,0x34,0x67,0x56,0x78,0x49,0x1a,0x2b,0xbc,0x8d,0xde,0xef,
			0x82,0xb3,0xe0,0xd1,0x46,0x77,0x24,0x15,0x3b,0x0a,0x59,0x68,0xff,0xce,0x9d,0xac
		};

    }
}

