using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Timers;
using SRB.Frame.updater;

namespace SRB.Frame{
    public partial class Node
    {
        public partial class SrbUpdater : INodeControlOwner, IAccesser
        {
            Node node;

            int srb_build_id;
            string App_type;

            public Version bootloaderVER = new Version("Bootloader");
            public SRB.Frame.Version srbVER = new SRB.Frame.Version("SRB");
            public SRB.Frame.Version nodeVER = new SRB.Frame.Version("App");
            public SrbUpdater(Node node)
            {
                this.node = node;
            }

            public void gotoUpdateMode()
            {
                node.infoClu.resetNode();
                hold(200);
                sendInfoPkg();
                sendAppInfoPkg();
            }


            const byte UDT_CMD_WRITE0 = 0;
            const byte UDT_CMD_WRITE1 = 1;
            const byte UDT_CMD_WRITE2 = 2;
            const byte UDT_CMD_RUN = 3;
            const byte UDT_CMD_ERASE = 4;
            const byte UDT_CMD_HOLD = 5;
            const byte UDT_CMD_CHECK = 6;
            const byte UDT_CMD_INFO = 7;
            const byte UDP_CMD_APP_INFO = 8;
            const byte UDT_CMD_END = 9;

            string hardware_code;
            int hardware_time_stamp;
            public string File_information => sup_file.Dscription;
            public string Hardware_code => hardware_code;
            public int Hardware_time_stamp => hardware_time_stamp;

            SupFile sup_file;
            public SupFile Sup_file=>sup_file;
            public void loadFile(string path)
            {
                sup_file = new SupFile(path);
            }
            public void loadFile(SupFile sup)
            {
                sup_file = sup;
            }

            public class UpdateTimeoutException : SrbException
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
                        hardware_time_stamp = (int)support.byteToUint32(ac.Recv_data, 12);
                        bootloaderVER.read(ac.Recv_data, 9);
                        this.hardware_code = new string(hardware_code).Substring(0,8);
                    }
                    if (ac.Send_data[0] == UDP_CMD_APP_INFO)
                    {
                        int point = 0;
                        nodeVER.read(ac.Recv_data, point); point += 3;
                        srbVER.read(ac.Recv_data, point); point += 3;
                        point += 2;
                        char[] App_type_ca = new char[17];
                        for (int i = 0; i < App_type_ca.Length; i++)
                        {
                            App_type_ca[i] = (char)ac.Recv_data[point++];
                        }
                        this.App_type = new string(App_type_ca);
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
            public void sendAppInfoPkg(int retry_ms = 50)
            {
                node.bus.singleAccess(new Access(this, node, Access.PortEnum.Udp, new byte[] { UDP_CMD_APP_INFO }));
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




            public class Broadcast
            {
                IBus bus;
                SrbThread burning_st;
                SrbThread goto_update_from_poweron_st;
                public Broadcast(IBus bus)
                {
                    this.bus = bus;
                    goto_update_from_poweron_st = new SrbThread(gotoUpdateModeAllFromPowerOnSTH); 
                    burning_st = new SrbThread(burnAllSTH);
                }
                public void gotoUpdateModeAll()
                {
                    foreach (Node n in bus)
                    {
                        if (n.Is_in_update == false)
                        {
                            n.gotoUpdateMode();
                        }
                    }
                }
                public void gotoUpdateAllFromPowerOn_start()
                {
                    goto_update_from_poweron_st.run(bus);
                }
                public void gotoUpdateAllFromPowerOn_stop()
                {
                    goto_update_from_poweron_st.stop();
                }
                void gotoUpdateModeAllFromPowerOnSTH(SrbThread.dIsThreadStoping IsStoping)
                {
                    var stopwatch = new System.Diagnostics.Stopwatch();
                    stopwatch.Restart();
                    while (true)
                    {
                        bus.singleAccess(new Access(null, null, Access.PortEnum.Udp, new byte[] { SrbUpdater.UDT_CMD_HOLD }));
                        if (stopwatch.ElapsedMilliseconds > 10000)
                        {
                            return;
                        }
                        if (IsStoping())
                        {
                            return;
                        }
                    }
                }


                SupLoader sup_loader;
                public SupLoader Sup_loader => sup_loader;
                public void loadFiles(string sup_files_path)
                {
                    sup_loader = new SupLoader();
                    sup_loader.LoadFiles(sup_files_path);
                }


                public delegate void dAppendInfo(string st);
                private dAppendInfo appendInfo;
                public void burnAll(dAppendInfo delegateInfo)
                {
                    if (burning_st.Is_running == true)
                    {
                        return;
                    }
                    appendInfo = delegateInfo;
                    if (sup_loader.Is_file_loaded)
                    {
                        burning_st.run(bus);
                    }
                    else
                    {
                        appendInfo(null);
                        appendInfo("Update all fail. No sup file.\n");
                    }
                }
                private void burnAllSTH(SrbThread.dIsThreadStoping IsStoping)
                {
                    System.Collections.Generic.Queue<Node> node_to_update = new System.Collections.Generic.Queue<Node>();
                    appendInfo(null);
                    foreach (Node n in bus)
                    {
                        if (n.Is_in_update == true)
                        {
                            node_to_update.Enqueue(n);
                        }
                    }
                    if (node_to_update.Count == 0)
                    {
                        appendInfo("No Node in update mode. " +
                            "You may connect new nodes or set some nodes to update mode, than try burn all.\n");
                    }
                    else
                    {
                        appendInfo(string.Format(
                            "{0} node(s) waiting to be burn.\n\n", node_to_update.Count));
                        int node_counter = 0;
                        foreach (Node n in node_to_update)
                        {
                            if (IsStoping())
                            {
                                appendInfo("Update all canceled.");
                                return;
                            }
                            node_counter++;
                            string hc = n.Updater.Hardware_code;
                            appendInfo(string.Format(
                                "node {0}/{1} burning. hardware code is {2}\n",
                                node_counter, node_to_update.Count, hc));

                            var sup_file = sup_loader.findByHardwareCode(hc);
                            if (sup_file != null)
                            {
                                n.Updater.loadFile(sup_file);
                                n.Updater.update();
                                n.gotoNormalMode();
                                appendInfo(string.Format(
                                    "\tburning done\n", hc));
                            }
                            else
                            {
                                appendInfo(string.Format(
                                    "\t.sup file not found for {0}, burning cancled", hc));
                            }
                        }
                    }
                }
            }
        }
    }
}
