using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SRB.Frame
{
    public partial class Node
    {
        public partial class SyncCluster : Node.ICluster 
        {
            public class SyncNodeGroup
            {
                private IBus bus;
                public SyncNodeGroup(IBus bus)
                {
                    this.bus = bus;
                }
                private Node[] all_nodes=null;

                public Node[] All_nodes => getNodeTable();
                int nodes_change_flag = 100;
                private Node[] getNodeTable()
                {
                    if (all_nodes == null)
                    {
                        init();
                    }
                    return all_nodes;
                }

                public void init()
                {                   
                    foreach(Node n in bus)
                    {
                        if (n.is_sync_node)
                        {
                            Queue<Node> sync_node_queue = new Queue<Node>();
                            foreach (Node node in bus)
                            {
                                if (node.is_sync_node)
                                {
                                    sync_node_queue.Enqueue(node);
                                    node.eDispossing += Node_eDispossing;
                                }
                            }
                            if (sync_node_queue.Count != 0) { 
                                all_nodes = sync_node_queue.ToArray();
                                nodes_change_flag++;
                            }
                            else
                            {
                                if (all_nodes != null)
                                {
                                    nodes_change_flag++;
                                    all_nodes = null;
                                }
                            }
                        }
                    }
                }
                private void Node_eDispossing(Node n)
                {
                    nodes_change_flag++;
                    all_nodes = null;
                }
            }
            public class Broadcast: IAccesser
            {
                public delegate void dInfoOut(string st);
                public dInfoOut debug;
                byte Sync_public_sno;
                IBus bus;
                public IBus Bus => bus;
                private SrbThread synchronizeST;
                private SrbThread calibratST;
                private long tick_base;
                private SyncNodeGroup sync_nodes;
                public Broadcast(IBus bus)
                {
                    this.bus = bus;
                    Sync_public_sno = (byte)support.random.Next(127);
                    synchronizeST = new SrbThread(synchronizeThread);
                    calibratST = new SrbThread(calibratThread);
                    recordTimeBroadcast(out tick_base);
                    sync_nodes = new SyncNodeGroup(bus);
                }
                private void write(dInfoOut info, string st)
                {
                    if (info != null)
                    {
                        info(st);
                    }
                }
                public int getSrbClock(long elapsed_ticks)
                {
                    return (int)(tickToSrbClock(elapsed_ticks) & 0xffffff);
                }
                public long getTotolTicks(long elapsed_ticks)
                {
                    return tickToSrbClock(elapsed_ticks) >> 8;
                }
                /// <summary>
                /// 根据系统时间求出SRB时钟值
                /// </summary>
                /// <param name="tick">Stopwatch获得的系统时间</param>
                /// <returns></returns>
                int tickToSrbClock(long tick)
                {
                    long rev = tick;
                    rev -= tick_base;
                    rev %= ((Stopwatch.Frequency * 0x1000000)/ 250000);
                    rev *= 250000;
                    rev += (Stopwatch.Frequency/2);
                    rev /= Stopwatch.Frequency;
                    return (int)rev;
                }
                /// <summary>
                /// 让总线对某个SRB时钟同步
                /// </summary>
                /// <param name="tick">记录到SRB时钟的时刻</param>
                /// <param name="srb_clock">SRB时钟的值</param>
                void  masterSyncTo(long tick, int srb_clock)
                {
                    tick_base = tick;
                    tick_base -= (srb_clock* Stopwatch.Frequency +  (250000/2) ) / 250000;
                }

                
                long elapsed_ticks;
                public void accessDone(Access acs)
                {
                    elapsed_ticks = acs.Send_tick;
                }

                private byte recordTimeBroadcast(out long elapsed_ticks)
                {
                    Access ac = Bus.accessRequest(this, null, AccessPort.Cgf);
                    ac.Send_data[0] = Node.SyncCluster.FIX_CID;
                    ac.Send_data[1] = Sync_public_sno;
                    byte rev = Sync_public_sno;
                    Sync_public_sno++;
                    if (Sync_public_sno == 128)
                    {
                        Sync_public_sno = 0;
                    }
                    bus.singleAccess(ac);
                    elapsed_ticks = this.elapsed_ticks;
                    return rev;
                }
                private byte recordTimeBroadcast()
                {
                    Access ac = Bus.accessRequest(null, null, AccessPort.Cgf);
                    ac.Send_data[0] = Node.SyncCluster.FIX_CID;
                    ac.Send_data[1] = Sync_public_sno;
                    byte rev = Sync_public_sno;
                    Sync_public_sno++;
                    if (Sync_public_sno == 128)
                    {
                        Sync_public_sno = 0;
                    }
                    bus.singleAccess(ac);
                    return rev;
                }

                private void syncToBroadcast(ushort ms, byte us4, byte sno)
                {
                    Access ac = Bus.accessRequest(null, null, AccessPort.Cgf);
                    ac.Send_data[0] = Node.SyncCluster.FIX_CID;
                    ac.Send_data[1] = sno;
                    ac.Send_data[2] = us4;
                    ac.Send_data[3] = ms.ByteLow();
                    ac.Send_data[4] = ms.ByteHigh();
                    bus.singleAccess(ac);
                    return;
                }

                
                public bool Is_synchronize_running
                {
                    get => synchronizeST.Is_running;
                }
                int sync_timer_value;
                public int Sync_timer_value
                {
                    get => sync_timer_value;
                } 
                int sync_timer_top = 60;
                public void syncStart(int sync_timer_top = 60)
                {
                    this.sync_timer_top = sync_timer_top;
                    synchronizeST.run(bus);
                }
                public void syncStop()
                {
                    synchronizeST.stop();
                }
                private void synchronizeThread(SrbThread.dIsThreadStoping IsStoping)
                {
                    while (true)
                    {
                        syncAll();
                        for (int i = 0; i < sync_timer_top; i++)
                        {
                            sync_timer_value = i;
                            if (IsStoping())
                            {
                                return;
                            }
                            Thread.Sleep(1000);
                        }
                    }
                }



                public void syncAll()
                {
                    if (Is_calibrat_running)
                    {
                        return;
                    }
                    int clock;
                    byte sno;
                    for (int i = 0; i < 5; i++)
                    {
                        long etick;
                        sno = recordTimeBroadcast(out etick);
                        if(sync_nodes.All_nodes == null)
                        {
                            return;
                        }
                        sync_nodes.All_nodes[0].syncClu.read();
                        clock = sync_nodes.All_nodes[0].syncClu.getClockInt(sno);
                        if (clock != -1)
                        {
                            masterSyncTo(etick, clock);
                            ushort ms;
                            byte us4;
                            SyncCluster.intToClock(out ms, out us4, clock);
                            syncToBroadcast(ms, us4, sno);
                            write(debug, "Synchronize done");
                            return;
                        }
                    }
                    sync_nodes.init() ;
                    return;
                }

                public void calibratClean(dInfoOut details)
                {
                    write(details, "All calibration values clear to 0.\n");
                    if (sync_nodes.All_nodes == null)
                    {
                        return;
                    }
                    foreach (Node node in sync_nodes.All_nodes)
                    {
                        node.syncClu.calibrationClu.calibration_value = 0;
                        node.syncClu.calibrationClu.write();
                    }
                    return;
                }

                public bool Is_calibrat_running
                {
                    get => calibratST.Is_running;
                }

                dInfoOut calibrat_info = null;
                const int TICKS_EACH_ADJ = 32;
                Node[] cal_nodes;
                public void calibrat(dInfoOut cinfo=null)
                {
                    calibrat_info = cinfo;
                    calibrat_info(null);
                    if (sync_nodes.All_nodes == null)
                    {
                        write(calibrat_info, string.Format("No node to be calibrated\n"));
                        return;
                    }
                    cal_nodes = sync_nodes.All_nodes;
                    calibratST.run(bus);
                    return;
                }
                public void calibratAbandon()
                {
                    calibratST.stop();
                }
                private void calibratThread(SrbThread.dIsThreadStoping IsStoping)
                {
                    int record_count = 512;
                    var last_Sync_value = new long[1 + cal_nodes.Length];
                    for (int i = 0; i < last_Sync_value.Length; i++)
                    {
                        last_Sync_value[i] = 0;
                    }
                    void recordNodeTime(int[,] sync_values_t2, int node_num, int record_num, int clk_value)
                    {
                        if (clk_value != -1)
                        {
                            while (last_Sync_value[node_num] > clk_value)
                            {
                                clk_value += 0x01000000;
                            }
                            last_Sync_value[node_num] = clk_value;
                        }
                        sync_values_t2[node_num, record_num] = clk_value;
                    }
                    void readGroup(out int[,] sync_values_t2)
                    {
                        sync_values_t2 = new int[1 + cal_nodes.Length, record_count];
                        for (int i = 0; i < record_count; i++)
                        {
                            long access_tick;
                            byte sno = recordTimeBroadcast(out access_tick);
                            recordNodeTime(sync_values_t2, 0, i, tickToSrbClock(access_tick));
                            for (int j = 0; j < cal_nodes.Length; j++)
                            {
                                Node node = cal_nodes[j];
                                node.syncClu.read();
                                recordNodeTime(sync_values_t2, j + 1, i, node.syncClu.getClockInt(sno));
                            }
                            if (99 == i % 100)
                            {
                                write(calibrat_info, ".");
                            }
                        }
                        write(calibrat_info, "!\n");
                    }

                    write(calibrat_info, string.Format("Calibrat {0} Nodes. Clock base is SRB-master (PC).\n", cal_nodes.Length)); ;
                    int[,] sync_bgn_values_t2;
                    int[,] sync_end_values_t2;

                    write(calibrat_info, " Recording clock for BEGIN.");
                    readGroup(out sync_bgn_values_t2);

                    write(calibrat_info,"Wait 30 seconds:\n" + 
                        "0    5    10   15   20   25   30\n" + 
                        "|----|----|----|----|----|----|\n=");
                    for (int i = 0; i < 30; i++)
                    {
                        Thread.Sleep(1000);
                        if (IsStoping())
                        {
                            write(calibrat_info, string.Format("\n"));
                            write(calibrat_info, string.Format("Calebrat abandoned!\n"));
                            return;
                        }
                        write(calibrat_info, "=");
                    }
                    write(calibrat_info, string.Format("\n"));

                    write(calibrat_info, "Recording clock for END.");
                    readGroup(out sync_end_values_t2);

                    void calculate(int b, int n, in int[,] sync_values_t2, out double clk_base, out double clk_node)
                    {
                        int count = 0;
                        clk_base = 0;
                        clk_node = 0;
                        long base_totle = 0;
                        long node_totle = 0;
                        for (int i = 0; i < record_count; i++)
                        {
                            if ((-1 != sync_values_t2[b, i]) && (-1 != sync_values_t2[n, i]))
                            {
                                count++;
                                base_totle += sync_values_t2[b, i];
                                node_totle += sync_values_t2[n, i];
                            }
                        }
                        clk_base = base_totle * 1.0 / count;
                        clk_node = node_totle * 1.0 / count;
                    }
                    if (true)
                    {
                        int common_add;
                        {
                            int j = 0;
                            Node node = cal_nodes[j];
                            double bgn_b, bgn_n, end_b, end_n;
                            calculate(0, j + 1, in sync_bgn_values_t2, out bgn_b, out bgn_n);
                            calculate(0, j + 1, in sync_end_values_t2, out end_b, out end_n);
                            double calibration_increase_totle = ((end_b - bgn_b) - (end_n - bgn_n)) * 0x4000;
                            double increase_count = (end_n - bgn_n) / (TICKS_EACH_ADJ * 256 * 1.0);
                            double increase_for_each_times = calibration_increase_totle / increase_count;
                            node.syncClu.calibrationClu.read();
                            int from = node.syncClu.calibrationClu.calibration_value;
                            common_add = (int)(Math.Round(increase_for_each_times));
                            node.syncClu.calibrationClu.calibration_value = node.syncClu.calibrationClu.calibration_value + common_add;
                            node.syncClu.calibrationClu.write();
                            int to = node.syncClu.calibrationClu.calibration_value;
                            write(calibrat_info, $"Base Node{node.Addr} calibration value {from} -> {to},sync to system\n");
                        }

                        for (int j = 1; j < cal_nodes.Length; j++)
                        {
                            Node node = cal_nodes[j];
                            double bgn_b, bgn_n, end_b, end_n;
                            calculate(1, j+1, in sync_bgn_values_t2, out bgn_b, out bgn_n);
                            calculate(1, j+1, in sync_end_values_t2, out end_b, out end_n);
                            double calibration_increase_totle = ((end_b - bgn_b) - (end_n - bgn_n)) * 0x4000;
                            double increase_count = (end_n - bgn_n) / (TICKS_EACH_ADJ * 256 * 1.0);
                            double increase_for_each_times = calibration_increase_totle / increase_count;
                            node.syncClu.calibrationClu.read();
                            int from = node.syncClu.calibrationClu.calibration_value;
                            int add = (int)(Math.Round(increase_for_each_times));
                            node.syncClu.calibrationClu.calibration_value = node.syncClu.calibrationClu.calibration_value + add+ common_add;
                            node.syncClu.calibrationClu.write();
                            int to = node.syncClu.calibrationClu.calibration_value;
                            write(calibrat_info, $"Node{node.Addr} calibration value {from} -> {to}\n");
                        }
                    }
                    else
                    {
                        for (int j = 0; j < cal_nodes.Length; j++)
                        {
                            Node node = cal_nodes[j];
                            double bgn_b, bgn_n, end_b, end_n;
                            calculate(0, j+1 , in sync_bgn_values_t2, out bgn_b, out bgn_n);
                            calculate(0, j+1, in sync_end_values_t2, out end_b, out end_n);
                            double calibration_increase_totle = ((end_b - bgn_b) - (end_n - bgn_n)) * 0x4000;
                            double increase_count = (end_n - bgn_n) / (TICKS_EACH_ADJ * 256 * 1.0);
                            double increase_for_each_times = calibration_increase_totle / increase_count;
                            node.syncClu.calibrationClu.read();
                            int from = node.syncClu.calibrationClu.calibration_value;
                            int add = (int)(Math.Round(increase_for_each_times));
                            node.syncClu.calibrationClu.calibration_value = node.syncClu.calibrationClu.calibration_value + add;
                            node.syncClu.calibrationClu.write();
                            int to = node.syncClu.calibrationClu.calibration_value;
                            write(calibrat_info, string.Format("Node{0} calibration value {1} -> {2}\n", node.Addr, from, to));
                        }

                    }
                    return;

                }




                public void getSyncStatus(ref string csv_str, ref Node[] nodes)
                {
                    if(nodes == null)
                    {
                        nodes = sync_nodes.All_nodes;
                    }
                    if(nodes == null)
                    {
                        return;
                    }
                    if (csv_str == null)
                    {
                        csv_str = "access_ET";
                        foreach (Node node in nodes)
                        {
                            csv_str += ",node";
                            csv_str += node.Addr;
                        }
                        csv_str += "\n";
                    }
                    long sync_elapsed_ticks;
                    byte sno = recordTimeBroadcast(out sync_elapsed_ticks);
                    csv_str += tickToSrbClock(sync_elapsed_ticks).ToString();
                    Exception first_exp = null ;
                    foreach (Node node in nodes)
                    {
                        int t = -2;
                        try { 
                            node.syncClu.read();
                            t = node.syncClu.getClockInt(sno);
                        }
                        catch(Exception e)
                        {
                            if (first_exp == null)
                            {
                                first_exp = e;
                            }
                        }
                        csv_str += ",";
                        csv_str += t.ToString();
                    }
                    csv_str += "\n";
                    if (first_exp != null)
                    {
                        throw first_exp;
                    }
                }
            }
        }
    }
}
