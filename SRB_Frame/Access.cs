using System;
using System.Diagnostics;

namespace SRB.Frame
{
    public interface IAccesser
    {
        void accessDone(Access acs);
    }

    public class AccessEventArgs : EventArgs
    {
        public bool Handled = false;
        public Access ac;
        public AccessEventArgs(Access access) : base()
        {
            ac = access;
        }
    }

    public enum AccessStatus
    {
        NoSend, PortColsed,
        SendWaitRecv, DeviceTimeOut,
        RecvedDone, SrbTimeOut, BroadcasePkg, RecvedBadPkg,
        NoInited
    };
    public enum AccessNodeError
    {
        RE_CFG_NO_CLUSTER_ID = 0xe0,
        RE_CFG_EMPTY_CLUSTER = 0xe1,
        RE_CFG_LEN_NO_MATCH = 0xe2,
        RE_CFG_WRITE_ONLY = 0xe3,
    };
    public enum AccessPort { D0, D1, D2, D3, Udp, Cgf, Rpt, Res };


    public interface IReadAsByteArray {
        byte this[int i] { get; }
        string ToHexSt();
        int Length { get; }
    }



    public class AccessData: IReadAsByteArray
    {
        int length;
        byte[] data;
        public AccessData(int max_length)
        {
            length = 0;
            data = new byte[max_length];
        }
        public void clean()
        {
            length = 0;
        }
        public int Length => length;
        public byte this[int i]
        {
            get 
            {
                if (i >= length)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return data[i];
                }
            }
            set
            {
                if (i >= length)
                {
                    length = i + 1;
                }
                data[i] = value;
            }
        }
        public void load(byte[] ba, int diff = 0)
        {
            int i = 0;
            while (diff < ba.Length)
            {
                data[i++] = ba[diff++];
            }
            if(length < i)
            {
                length = i;
            }
        }
        public void load(byte[] ba, int diff, int count)
        {
            int i = 0;
            while (i < count)
            {
                data[i++] = ba[diff++];
            }
            if (length < i)
            {
                length = i;
            }
        }
        public void load(byte[] ba, int i, int diff, int count)
        {
            count += i;
            while (i < count)
            {
                data[i++] = ba[diff++];
            }
            if (length < i)
            {
                length = i;
            }
        }

        public string ToHexSt()
        {
            if (length == 0)
            {
                return "<empty>";
            }
            string s = "";
            for (int i = 0; i < length; i++)
            {
                s += data[i].ToHexSt() + ' ';
            }
            return s;
        }
    }




    public class Access
    {
        private AccessPool pool;
        private long begin_send_tick;
        private long send_done_tick;
        private long recv_done_tick;
        private string description = "";
        public string Description { get => description; set => description = value; }

        private Node sender_node;
        private IAccesser accesser;

        private int retry;
        private AccessStatus _status;
        private AccessPort _port;

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
        public AccessStatus Status => _status;
        public AccessPort Port => _port;

        //_send_data is set by constructor
        private AccessData _send_data;
        public AccessData Send_data => _send_data;
        //_recv_data is set by recv
        private AccessData _recv_data;
        public IReadAsByteArray Recv_data => _recv_data;
        public byte Send_bfc => (byte)((int)_port * 32 + Send_data.Length);

        private byte _recv_bfc;
        public bool Recv_error => (_recv_bfc & (1 << 7)) != 0;
        public bool Recv_busy => (_recv_bfc & (1 << 6)) != 0;
        public bool Recv_event => (_recv_bfc & (1 << 5)) != 0;
        public int Recv_data_len => (int)(_recv_bfc & 0x1f);

        public long Send_tick => send_done_tick;
        public long Begin_send_tick => begin_send_tick;
        public long Recv_done_tick => recv_done_tick;

        public Access(AccessPool pool)
        {
            _send_data = new AccessData(31);
            _recv_data = new AccessData(31);
            init();
            this.pool = pool;
        }

        void init()
        {
            begin_send_tick = -1;
            send_done_tick = -1;
            recv_done_tick = -1;
            Description = "";
            retry = -1;
            _status = AccessStatus.NoInited;
            _send_data.clean();
            _recv_data.clean();

        }
        public void free()
        {
            init();
            pool.free(this);
        }

        public AccessData loadAccess(IAccesser a, Node n, AccessPort p)
        {
            accesser = a;
            sender_node = n;
            _port = p;
            _status = AccessStatus.NoSend;
            return _send_data;
        }


        public void onAccessDone()
        {//todo add Exception and log record
            switch (_status)
            {
                case AccessStatus.BroadcasePkg:                    
                    accesser?.accessDone(this);
                    break;
                case AccessStatus.RecvedDone:
                    sender_node.active(this.retry);
                    accesser?.accessDone(this);
                    break;
                case AccessStatus.PortColsed:
                    //TODO: Bus Master device is not open or port is not selected
                    break;
                case AccessStatus.DeviceTimeOut:
                    break;
                case AccessStatus.RecvedBadPkg:
                case AccessStatus.SrbTimeOut:
                    if (sender_node != null)
                    {//TODO: may something wrong on bus,  is not my error so log it.
                        sender_node.lose();
                    }
                    break;
                case AccessStatus.SendWaitRecv:
                case AccessStatus.NoSend:
                    throw new Exception("An access DONE called when " + _status.ToString() + "You should check SRB_MASTR.cs");
            }
        }
        public bool receiveAccess(int retry, byte bfc, byte[] data, int offset)
        {
            recv_done_tick = Stopwatch.GetTimestamp();
            if (Status == AccessStatus.SendWaitRecv)
            {
                this._recv_bfc = bfc;
                if ((data.Length - offset) > this.Recv_data_len)
                {
                    _recv_data.load(data, offset, this.Recv_data_len);
                    this.retry = retry;
                    _status = AccessStatus.RecvedDone;
                }
                else
                {
                    _status = AccessStatus.RecvedBadPkg;
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
            recv_done_tick = Stopwatch.GetTimestamp();
            this._recv_bfc = bfc;
            this._recv_data.load(data);
            if ((data.Length) != this.Recv_data_len)
            {
                _status = AccessStatus.RecvedBadPkg;
            }
            else
            {
                _status = AccessStatus.RecvedDone;
            }
        }
        public void receiveAccessBroadcast()
        {
            recv_done_tick = Stopwatch.GetTimestamp();
            if (this.Addr == 0xff)
            {
                _status = AccessStatus.BroadcasePkg;
            }
            else
            {
                _status = AccessStatus.RecvedBadPkg;
            }
        }
        public void receiveAccessTimeout()
        {
            recv_done_tick = Stopwatch.GetTimestamp();
            if (this.Addr == 0xff)
            {
                _status = AccessStatus.RecvedBadPkg;
            }
            else
            {
                _status = AccessStatus.SrbTimeOut;
            }
        }
        public void sendBegin()
        {
            begin_send_tick = Stopwatch.GetTimestamp();
        }
        public void sendDone()
        {
            _status = AccessStatus.SendWaitRecv;
            send_done_tick = Stopwatch.GetTimestamp();
        }
        public void portCloseSendFail()
        {
            if (_status != AccessStatus.NoSend)
            {
                throw new Exception("Send is done but call portCloseSendFail");
            }
            _status = AccessStatus.PortColsed;
        }
        public void sendDoneRecvFail()
        {
            if (_status != AccessStatus.SendWaitRecv)
            {
                throw new Exception("sendDoneRecvFail status is not SendWaitRecv");
            }
            recv_done_tick = Stopwatch.GetTimestamp();
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
            string st = "{" +
               "\"Ts\":" +
               '"' + send_done_tick.ToString() + "\", " +
               "\"ss\":" +
               '"' + (begin_send_tick - send_done_tick).ToString() + "\", "+
               "\"rs\":" +
               '"' + (send_done_tick - recv_done_tick).ToString() + "\", " + '\n' +
              "\"Addr\":" +
               '"' + Addr.ToString() + "\", " +
               "\"Dsc\":" +
               '"' + Description + "\", " +
               "\"Sta\":" +
               '"' + Status.ToString() + "\", " +'\n'+
               "\"Send\":{" +
                   "\"bfc\":" +
                   '"' + Send_bfc.ToHexSt() + "\", " +
                   "\"data\":" +
                   '"' + Send_data.ToHexSt() + "\"" +
               "}," + '\n' +

              "\"Recv\":{" +
                   "\"bfc\":" +
                   '"' + _recv_bfc.ToHexSt() + "\", " +
                   "\"data\":" +
                   '"' + Recv_data.ToHexSt() + "\"" +
               "}" +
            "}";
            return st;
        }
    }
}

