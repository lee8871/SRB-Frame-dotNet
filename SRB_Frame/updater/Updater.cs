using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SRB.Frame.updater;

namespace SRB.Frame{
    public partial class BaseNode
    {
        public class SrbUpdater : INodeControlOwner, IAccesser
        {
            BaseNode node;
            public SrbUpdater(BaseNode node)
            {
                this.node = node;
            }
            const byte UDT_CMD_WRITE0 = 0;
            const byte UDT_CMD_WRITE1 = 1;
            const byte UDT_CMD_WRITE2 = 2;
            const byte UDT_CMD_RUN = 3;
            const byte UDT_CMD_ERASE = 4;
            const byte UDT_CMD_HOLD = 5;
            const byte UDT_CMD_CHECK = 6;
            const byte UDT_CMD_INFO = 7;
            const byte UDT_CMD_END = 8;

            string file_info;
            string hardware_code;
            public string File_information => file_info;
            public string Hardware_code => hardware_code;
            SupFile sup_file;
            public void loadFile(string path)
            {
                sup_file = new SupFile(path);
            }
            public void loadFile(SupFile sup)
            {
                sup_file = sup;
            }

            public class UpdateTimeoutException : Exception
            {
                string msg;
                public override string Message=> msg;
                public int time_out_ms;
                public UpdateTimeoutException(string message = null, int time_out_ms = 0) : base(null)
                {
                    msg = message;
                    this.time_out_ms = time_out_ms;
                }
                public override string ToString()
                {
                    return string.Format("在{0}操作中超时，超时时间：{1}ms", Message, time_out_ms);
                }
                public void setMessage(string st){
                    msg = st;
                }
            }

            bool is_hold = false;
            bool is_code_good = false;
            bool is_busy = false;
            public void accessDone(Access ac)
            {
                if (ac.Port != Access.PortEnum.Udp)
                {
                    throw new Exception("Update type should Udp,but get " + ac.Port.ToString());
                }
                if (ac.Recv_data == null)
                {
                    throw new Exception("cfg_receive a null recv_data");
                }
                if (!((ac.Recv_error) || (ac.Recv_busy)))
                {
                    if(ac.Send_data[0] == UDT_CMD_INFO)
                    {
                        char[] hardware_code = new char[ac.Recv_data_len];
                        for(int i = 0;i< hardware_code.Length; i++)
                        {
                            hardware_code[i] = (char)ac.Recv_data[i];
                        }

                        this.hardware_code = new string(hardware_code).Substring(0,8);
                    }
                    else
                    {
                        byte bl_flag = ac.Recv_data[0];
                        is_hold = ((bl_flag & (1 << 0)) != 0);
                        is_code_good = ((bl_flag & (1 << 1)) != 0);
                        is_busy = ((bl_flag & (1 << 2)) != 0);
                        return;
                    }
                }
            }
            public void sendInfoPkg(int retry_ms = 50)
            {
                node.bus.singleAccess(new Access(this, node, Access.PortEnum.Udp, new byte[] { UDT_CMD_INFO }));
            }
            public void hold(int retry_ms = 50)
            {
                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Restart();
                is_hold = false;
                while (is_hold == false)
                {
                    node.bus.singleAccess(new Access(this, node, Access.PortEnum.Udp, new byte[] { UDT_CMD_HOLD }));
                    if (stopwatch.ElapsedMilliseconds > retry_ms)
                    {
                        throw new UpdateTimeoutException("切换下载状态", retry_ms);
                    }
                }
            }
            double UpdataRate;
            public void update()
            {
                Stopwatch Update_time_out = new Stopwatch();
                Update_time_out.Restart();
                int totle_time_out_ms = 2000;
                string step = null;
                try
                {
                    step = "update::preparatory wait idle";
                    waitToIdle(Update_time_out, totle_time_out_ms);
                    step = "update::erase";
                    node.bus.singleAccess(new Access(this, node, Access.PortEnum.Udp, new byte[] { UDT_CMD_ERASE }));

                    int counter = 0;
                    foreach (var ba in sup_file)
                    {
                        waitToIdle(Update_time_out, totle_time_out_ms);
                        node.bus.singleAccess(new Access(this, node, Access.PortEnum.Udp, ba));

                        step = "update::write" + counter;
                        counter++;
                        UpdataRate = counter / sup_file.Length;
                    }
                }
                catch (UpdateTimeoutException e)
                {
                    e.setMessage(step);
                    throw e; 
                }
            }

            private void waitToIdle(Stopwatch time_out_watch, int time_out_ms)
            {
                is_busy = true;
                while (is_busy == true)
                {
                    node.bus.singleAccess(new Access(this, node, Access.PortEnum.Udp, new byte[] { UDT_CMD_HOLD }));
                    if (time_out_watch.ElapsedMilliseconds > time_out_ms)
                    {
                        throw new UpdateTimeoutException(null, time_out_ms);
                    }
                }
            }

            public void gotoNormalMode()
            {
                node.gotoNormalMode();
            }


            public bool tryExit()
            {
                Stopwatch Update_time_out = new Stopwatch();
                Update_time_out.Restart();
                int totle_time_out_ms = 200;
                string step = null;
                try
                {
                    step = "update::preparatory wait idle";
                    waitToIdle(Update_time_out, totle_time_out_ms);
                    node.bus.singleAccess(new Access(this, node, Access.PortEnum.Udp, new byte[] { UDT_CMD_CHECK }));
                    step = "update::wait check done";
                    waitToIdle(Update_time_out, totle_time_out_ms);
                }
                catch(UpdateTimeoutException e)
                {
                    e.setMessage(step);
                    throw e;
                }
                if (is_code_good == true)
                {
                    node.bus.singleAccess(new Access(this, node, Access.PortEnum.Udp, new byte[] { UDT_CMD_RUN }));
                    return true;
                }
                else
                {
                    return false;
                }
            }

            protected override System.Windows.Forms.Control createControl()
            {
                return new UpdateControl(this);
            }

            public override string ToString()
            {
                return "Updater[HC:"+Hardware_code+"]";
            }



            public static void holdAll(IBus bus, int duration_ms=5000 )
            {
                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Restart();
                while (true)
                {
                    bus.singleAccess(new Access(null, null, Access.PortEnum.Udp, new byte[] { UDT_CMD_HOLD }));
                    if (stopwatch.ElapsedMilliseconds > duration_ms)
                    {
                        break;
                    }
                }

            }

        }
    }
}
