using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SRB_CTR
{
    class ComPort
    {
        ///
        ///端口名称(COM1,COM2...COM4...)
        ///
        public string Port = "COM1";
        ///
        ///波特率9600
        ///
        public int BaudRate = 9600;
        ///
        ///数据位4-8
        ///
        public byte ByteSize = 8; //4-8
        ///
        ///奇偶校验0-4=no,odd,even,mark,space
        ///
        public byte Parity = 0;   //0-4=no,odd,even,mark,space
        ///
        ///停止位
        ///
        public byte StopBits = 0;   //0,1,2 = 1, 1.5, 2
        ///
        ///超时长
        ///
        public int ReadTimeout = 200;
        ///
        ///串口是否已经打开
        ///
        public bool Opened = false;
        ///
        /// COM口句柄
        ///
        private int hComm = -1;

        #region "API相关定义"
        private const string DLLPATH = "kernel32.dll"; // "kernel32";

        ///
        /// WINAPI常量,写标志
        ///
        private const uint GENERIC_READ = 0x80000000;
        ///
        /// WINAPI常量,读标志
        ///
        private const uint GENERIC_WRITE = 0x40000000;
        ///
        /// WINAPI常量,打开已存在
        ///
        private const int OPEN_EXISTING = 3;
        ///
        /// WINAPI常量,无效句柄
        ///
        private const int INVALID_HANDLE_VALUE = -1;

        private const int PURGE_RXABORT = 0x2;
        private const int PURGE_RXCLEAR = 0x8;
        private const int PURGE_TXABORT = 0x1;
        private const int PURGE_TXCLEAR = 0x4;

        ///
        ///设备控制块结构体类型
        ///
        [StructLayout(LayoutKind.Sequential)]
        public struct DCB
        {
            ///
            /// DCB长度
            ///
            public int DCBlength;
            ///
            ///指定当前波特率
            ///
            public int BaudRate;
            ///
            ///标志位
            ///
            public uint flags;
            ///
            ///未使用,必须为0
            ///
            public ushort wReserved;
            ///
            ///指定在XON字符发送这前接收缓冲区中可允许的最小字节数
            ///
            public ushort XonLim;
            ///
            ///指定在XOFF字符发送这前接收缓冲区中可允许的最小字节数
            ///
            public ushort XoffLim;
            ///
            ///指定端口当前使用的数据位
            ///
            public byte ByteSize;
            ///
            ///指定端口当前使用的奇偶校验方法,可能为:EVENPARITY,MARKPARITY,NOPARITY,ODDPARITY 0-4=no,odd,even,mark,space
            ///
            public byte Parity;
            ///
            ///指定端口当前使用的停止位数,可能为:ONESTOPBIT,ONE5STOPBITS,TWOSTOPBITS 0,1,2 = 1, 1.5, 2
            ///
            public byte StopBits;
            ///
            ///指定用于发送和接收字符XON的值 Tx and Rx XON character
            ///
            public byte XonChar;
            ///
            ///指定用于发送和接收字符XOFF值 Tx and Rx XOFF character
            ///
            public byte XoffChar;
            ///
            ///本字符用来代替接收到的奇偶校验发生错误时的值
            ///
            public byte ErrorChar;
            ///
            ///当没有使用二进制模式时,本字符可用来指示数据的结束
            ///
            public byte EofChar;
            ///
            ///当接收到此字符时,会产生一个事件
            ///
            public byte EvtChar;
            ///
            ///未使用
            ///
            public ushort wReserved1;
        }
        ~ComPort()
        {
            this.Close();
        }

        ///
        ///串口超时时间结构体类型
        ///
        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }

        ///
        ///溢出缓冲区结构体类型
        ///
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }

        ///
        ///打开串口
        ///
        ///要打开的串口名称
        ///指定串口的访问方式，一般设置为可读可写方式
        ///指定串口的共享模式，串口不能共享，所以设置为0
        ///设置串口的安全属性，WIN9X下不支持，应设为NULL
        ///对于串口通信，创建方式只能为OPEN_EXISTING
        ///指定串口属性与标志，设置为FILE_FLAG_OVERLAPPED(重叠I/O操作)，指定串口以异步方式通信
        ///对于串口通信必须设置为NULL
        [DllImport(DLLPATH)]
        private static extern int CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode,
        int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        ///
        ///得到串口状态
        ///
        ///通信设备句柄
        ///设备控制块DCB
        [DllImport(DLLPATH)]
        private static extern bool GetCommState(int hFile, ref DCB lpDCB);

        ///
        ///建立串口设备控制块(嵌入版没有)
        ///
        ///设备控制字符串
        ///设备控制块
        //[DllImport(DLLPATH)]
        //private static extern bool BuildCommDCB(string lpDef, ref DCB lpDCB);

        ///
        ///设置串口状态
        ///
        ///通信设备句柄
        ///设备控制块
        [DllImport(DLLPATH)]
        private static extern bool SetCommState(int hFile, ref DCB lpDCB);

        ///
        ///读取串口超时时间
        ///
        ///通信设备句柄
        ///超时时间
        [DllImport(DLLPATH)]
        private static extern bool GetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        ///
        ///设置串口超时时间
        ///
        ///通信设备句柄
        ///超时时间
        [DllImport(DLLPATH)]
        private static extern bool SetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        ///
        ///读取串口数据
        ///
        ///通信设备句柄
        ///数据缓冲区
        ///多少字节等待读取
        ///读取多少字节
        ///溢出缓冲区
        [DllImport(DLLPATH)]
        private static extern bool ReadFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToRead,
        ref int lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);

        ///
        ///写串口数据
        ///
        ///通信设备句柄
        ///数据缓冲区
        ///多少字节等待写入
        ///已经写入多少字节
        ///溢出缓冲区
        [DllImport(DLLPATH)]
        private static extern bool WriteFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToWrite,
        ref int lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);

        [DllImport(DLLPATH, SetLastError = true)]
        private static extern bool FlushFileBuffers(int hFile);

        [DllImport(DLLPATH, SetLastError = true)]
        private static extern bool PurgeComm(int hFile, uint dwFlags);

        ///
        ///关闭串口
        ///
        ///通信设备句柄
        [DllImport(DLLPATH)]
        private static extern bool CloseHandle(int hObject);

        ///
        ///得到串口最后一次返回的错误
        ///
        [DllImport(DLLPATH)]
        private static extern uint GetLastError();
        #endregion

        ///
        ///设置DCB标志位
        ///
        ///
        ///
        ///
        internal void SetDcbFlag(int whichFlag, int setting, DCB dcb)
        {
            uint num;
            setting = setting << whichFlag;
            if ((whichFlag == 4) || (whichFlag == 12))
            {
                num = 3;
            }
            else if (whichFlag == 15)
            {
                num = 0x1ffff;
            }
            else
            {
                num = 1;
            }
            dcb.flags &= ~(num << whichFlag);
            dcb.flags |= (uint)setting;
        }

        ///
        ///建立与串口的连接
        ///
        public int Open()
        {
            if (Opened)
            {
                this.Close();
            }
            DCB dcb = new DCB();
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();

            // 打开串口
            hComm = CreateFile("\\\\.\\" + Port, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return (int)GetLastError();
                throw(new Exception( "打开文件错误，编号为"+GetLastError().ToString()));
            }

            // 设置通信超时时间
            GetCommTimeouts(hComm, ref ctoCommPort);
            ctoCommPort.ReadIntervalTimeout = -1;
            ctoCommPort.ReadTotalTimeoutConstant = 0;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutConstant = 0;
            SetCommTimeouts(hComm, ref ctoCommPort);

            //设置串口参数
            GetCommState(hComm, ref dcb);
            dcb.DCBlength = Marshal.SizeOf(dcb);
            dcb.BaudRate = BaudRate;
            dcb.flags = 0;
            dcb.ByteSize = (byte)ByteSize;
            dcb.StopBits = StopBits;
            dcb.Parity = (byte)Parity;

            //------------------------------
            SetDcbFlag(0, 1, dcb);            //二进制方式
            SetDcbFlag(1, (Parity == 0) ? 0 : 1, dcb);
            SetDcbFlag(2, 0, dcb);            //不用CTS检测发送流控制
            SetDcbFlag(3, 0, dcb);            //不用DSR检测发送流控制
            SetDcbFlag(4, 0, dcb);            //禁止DTR流量控制
            SetDcbFlag(6, 0, dcb);            //对DTR信号线不敏感
            SetDcbFlag(9, 1, dcb);            //检测接收缓冲区
            SetDcbFlag(8, 0, dcb);            //不做发送字符控制
            SetDcbFlag(10, 0, dcb);           //是否用指定字符替换校验错的字符
            SetDcbFlag(11, 0, dcb);           //保留NULL字符
            SetDcbFlag(12, 0, dcb);           //允许RTS流量控制
            SetDcbFlag(14, 0, dcb);           //发送错误后，继续进行下面的读写操作
            //--------------------------------
            dcb.wReserved = 0;                       //没有使用，必须为0      
            dcb.XonLim = 0;                          //指定在XOFF字符发送之前接收到缓冲区中可允许的最小字节数
            dcb.XoffLim = 0;                         //指定在XOFF字符发送之前缓冲区中可允许的最小可用字节数
            dcb.XonChar = 0;                         //发送和接收的XON字符
            dcb.XoffChar = 0;                        //发送和接收的XOFF字符
            dcb.ErrorChar = 0;                       //代替接收到奇偶校验错误的字符
            dcb.EofChar = 0;                         //用来表示数据的结束     
            dcb.EvtChar = 0;                         //事件字符，接收到此字符时，会产生一个事件       
            dcb.wReserved1 = 0;                      //没有使用

            if (!SetCommState(hComm, ref dcb))
            {
                Opened = false;
                return -2;
            }
            Opened = true;
            return 0;
        }

        ///
        ///关闭串口,结束通讯
        ///
        public void Close()
        {
            if (Opened)
            {
                if (hComm != INVALID_HANDLE_VALUE)
                {
                    CloseHandle(hComm);
                    Opened = false;
                }
            }
        }
        ///
        ///读取串口返回的数据
        ///
        ///数据长度
        public int Read(ref byte[] bytData, int NumBytes)
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesRead = 0;
                ReadFile(hComm, bytData, NumBytes, ref BytesRead, ref ovlCommPort);
                return BytesRead;
            }
            else
            {
                Opened = false;
                return -1;
            }
        }
        public long[] tickbase = new long[1000];
        public int tick_counter= 0;
        ///
        ///向串口写数据
        ///
        ///数据数组
        public int Write(ref byte[] WriteBytes, int intSize)
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesWritten = 0;
                //tickbase[tick_counter] = Stopwatch.GetTimestamp();
                if(WriteFile(hComm, WriteBytes, intSize, ref BytesWritten, ref ovlCommPort)==false)
                {
                    throw (new Exception("写入文件错误，编号为" + GetLastError().ToString()));
                }

                return BytesWritten;
            }
            else
            {
                Opened = false;
                return -1;
            }
        }

        ///
        ///清除接收缓冲区
        ///
        ///
        public void ClearReceiveBuf()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                PurgeComm(hComm, PURGE_RXABORT | PURGE_RXCLEAR);
            }
        }

        ///
        ///清除发送缓冲区
        ///
        public void ClearSendBuf()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                PurgeComm(hComm, PURGE_TXABORT | PURGE_TXCLEAR);
            }
        }
    }
}
