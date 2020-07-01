using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace SRB.Frame
{
    public partial class Node
    {
        public partial class SyncCluster : Node.ICluster
        {
            public class Broadcast
            {
                public delegate void dInfoOut(string st);
                public dInfoOut debug;
                
                Node clock_base_node = null;
                byte Sync_public_sno;
                IBus bus;
                private SrbThread synchronizeST;
                private SrbThread calibratST;
                public Broadcast(IBus bus)
                {
                    this.bus = bus;
                    Sync_public_sno = (byte)support.random.Next(127);
                    synchronizeST = new SrbThread(synchronizeThread);
                    calibratST = new SrbThread(calibratThread);
                }
                public void setBaseClock(Node cbn)
                {
                    if (bus[cbn.Addr] == cbn)
                    {
                        clock_base_node = cbn;
                    }
                    else
                    {
                        throw new NodeNotContainException(bus, cbn);
                    }
                }
                public void findBaseClock()
                {
                    foreach(Node n in bus)
                    {
                        if (n.syncClu.calibrationClu.calibration_value == 0)
                        {
                            clock_base_node = n;
                            break;
                        }
                    }
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
                double tickToDoubleSrbClock(long tick)
                {
                    double rev = tick;
                    rev *= 250000;
                    rev /= Stopwatch.Frequency;
                    return rev;

                }
                long tickToSrbClock(long tick)
                {
                    double rev = tick;
                    rev *= 250000;
                    rev /= Stopwatch.Frequency;
                    return (long)Math.Round(rev);
                }

                private byte recordTimeBroadcast(out long elapsed_ticks)
                {
                    Access ac = new Access(null, null, Access.PortEnum.Cgf, new byte[]
                    { Node.SyncCluster.FIX_CID, Sync_public_sno});
                    byte rev = Sync_public_sno;
                    Sync_public_sno++;
                    if (Sync_public_sno == 128)
                    {
                        Sync_public_sno = 0;
                    }
                    bus.singleAccess(ac);
                    elapsed_ticks = ac.Send_tick;
                    return rev;
                }
                private byte recordTimeBroadcast()
                {
                    Access ac = new Access(null, null, Access.PortEnum.Cgf, new byte[]
                    { Node.SyncCluster.FIX_CID, Sync_public_sno});
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
                    Access ac = new Access(null, null, Access.PortEnum.Cgf, new byte[]
                    { Node.SyncCluster.FIX_CID, sno,us4, ms.ByteLow(),ms.ByteHigh()});
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
                    if(Is_calibrat_running)
                    {
                        write(debug, "Synchronization is deny. Calibrat is running");
                        return;
                    }
                    if (clock_base_node == null)
                    {
                        findBaseClock();
                    }
                    int clock;
                    byte sno;
                    for (int i = 0; i < 5; i++)
                    {
                        sno = recordTimeBroadcast();
                        clock_base_node.syncClu.read();
                        clock = clock_base_node.syncClu.getClockInt(sno);
                        if (clock != -1)
                        {
                            ushort ms;
                            byte us4;
                            SyncCluster.intToClock(out ms, out us4, clock);
                            syncToBroadcast(ms, us4, sno);
                            write(debug, "Synchronize done");
                            writeSyncMarkCsv(clock,sno);
                            return;
                        }
                    }
                    write(debug, "Synchronize fail. Base clock node can not be accessed.");
                    clock_base_node = null;
                    return;
                }

                public void calibratClean(dInfoOut details)
                {
                    write(details, "All calibration values clear to 0.\n");
                    foreach (Node node in bus)
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
                public void calibrat(dInfoOut cinfo=null)
                {
                    if (clock_base_node == null)
                    {
                        findBaseClock();
                    }
                    calibrat_info = cinfo;
                    calibratST.run(bus);
                    return;
                }
                public void calibratAbandon()
                {
                    calibratST.stop();
                }
                private void calibratThread(SrbThread.dIsThreadStoping IsStoping)
                {
                    calibrat_info(null);
                    int record_count = 512;
                    Queue<Node> sync_node_queue = new Queue<Node>();
                    sync_node_queue.Enqueue(clock_base_node);
                    foreach (Node node in bus)
                    {
                        if (clock_base_node != node) 
                        {                         
                            sync_node_queue.Enqueue(node);
                        }
                    }
                    Node[] sync_nodes = sync_node_queue.ToArray();
                    if (sync_nodes.Length < 2)
                    {
                        write(calibrat_info, string.Format("No node to be calibrated\n"));
                        return;
                    }
                    var last_Sync_value = new long[sync_nodes.Length];
                    for (int i = 0; i < last_Sync_value.Length; i++)
                    {
                        last_Sync_value[i] = 0;
                    }
                    write(calibrat_info, string.Format("Calibrat {0} Nodes. Clock base is {1}.\n", sync_nodes.Length, clock_base_node.ToString())); ;
                    void readGroup(out int[,] sync_values_t2)
                    {
                        sync_values_t2 = new int[sync_nodes.Length, record_count];
                        for (int i = 0; i < record_count; i++)
                        {
                            byte sno = recordTimeBroadcast();
                            for (int j = 0; j < sync_nodes.Length; j++)
                            {
                                Node node = sync_nodes[j];
                                node.syncClu.read();
                                int clk_value = node.syncClu.getClockInt(sno);
                                if (clk_value != -1)
                                {
                                    while (last_Sync_value[j] > clk_value)
                                    {
                                        clk_value += 0x01000000;
                                    }
                                    last_Sync_value[j] = clk_value;
                                }
                                sync_values_t2[j, i] = clk_value;
                            }
                            if (99 == i % 100)
                            {
                                write(calibrat_info, ".");
                            }
                        }
                        write(calibrat_info, " Done.\nRecording clock for BEGIN."+
                             "Wait 30 seconds:\n" + "______________________________\n");
                    }
                    int[,] sync_bgn_values_t2;
                    readGroup(out sync_bgn_values_t2);
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
                    int[,] sync_end_values_t2;
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
                            if((-1 != sync_values_t2[b, i])&& (-1 != sync_values_t2[n, i]))
                            {
                                count++;
                                base_totle += sync_values_t2[b, i];
                                node_totle += sync_values_t2[n, i];
                            }
                        }
                        clk_base = base_totle * 1.0 / count; 
                        clk_node = node_totle * 1.0 / count;
                    }
                    for (int j = 1; j < sync_nodes.Length; j++)
                    {
                        Node node = sync_nodes[j];
                        double bgn_b, bgn_n, end_b, end_n;
                        calculate(0,j, in sync_bgn_values_t2, out bgn_b, out bgn_n);
                        calculate(0,j, in sync_end_values_t2, out end_b, out end_n);
                        double calibration_increase_totle = ((end_b - bgn_b) - (end_n - bgn_n)) * 0x4000;
                        double increase_count = (end_n - bgn_n) / (64 * 256 * 1.0);
                        double increase_for_each_times = calibration_increase_totle / increase_count;
                        node.syncClu.calibrationClu.read();
                        int from = node.syncClu.calibrationClu.calibration_value;
                        int add = (int)(Math.Round(increase_for_each_times));
                        node.syncClu.calibrationClu.calibration_value = node.syncClu.calibrationClu.calibration_value+add;
                        node.syncClu.calibrationClu.write();
                        int to = node.syncClu.calibrationClu.calibration_value;
                        write(calibrat_info, string.Format("Node{0} calibration value {1} -> {2}\n", node.Addr, from, to));
                    }
                    return;
                }
                int report_num = 0;

                public void getSyncStatuc(dInfoOut info_out)
                {
                    write(info_out,"\n## Report" + (report_num++) + "  " + System.DateTime.Now.ToLongTimeString() + "\n");
                    if (clock_base_node == null)
                    {
                        findBaseClock();
                    }


                    string info="";
                    long sync_elapsed_ticks;

                    byte sno = recordTimeBroadcast(out sync_elapsed_ticks);
                    info+="#### Get all time statuc  \n";
                    clock_base_node.syncClu.read();
                    int base_clock = clock_base_node.syncClu.getClockInt(sno);
                    if(base_clock == -1)
                    {
                        info += "+ Get clock base fail.\n";
                        return;

                    }
                    ushort ms;
                    byte us4;
                    SyncCluster.intToClock(out ms, out us4, base_clock);
                    double diff_sum = 0;
                    info += "\n";
                    info += string.Format("Addr | ms1.024 | us4 | Totel int|Diff\n");
                    info += string.Format("----|----|----|----|----\n");
                    info += string.Format(" {0} | {1} | {2} | {3} | {4}\n", "BC", 
                        (ms*1.0 / 1000).ToString("F3"), us4, base_clock,"--");
                    int success_count = 0;
                    foreach (Node node in bus)
                    {
                        if(node == clock_base_node)
                        {
                            continue;
                        }
                        node.syncClu.read();
                        if (node.syncClu.sno != sno)
                        {
                            info += string.Format("Addr|not receive|x|x|x\n", node.Addr);
                        }
                        else if ((node.syncClu.is_sync_miss != false))
                        {
                            info += string.Format("Addr|sync miss|x|x|x\n", node.Addr);
                        }
                        else
                        {
                            double c = node.syncClu.getClockInt();
                            double diff = c - base_clock;
                            diff_sum += System.Math.Abs(diff);
                            info += string.Format(" {0} | {1} | {2} | {3} | {4}\n", node.Addr,
                                    (node.syncClu.ms * 1.0 / 1000).ToString("F3"), node.syncClu.us4, c, diff.ToString("F3"));
                            success_count++;
                        }
                    }
                    if (success_count != 0)
                    {
                        info += "\n";
                        info += string.Format("+ {0}/{1} node accessed for sync!\n", success_count, bus.Count);
                        double diff_avariage = diff_sum / success_count;
                        info += string.Format("+ Diff avariage is {0}\n", (diff_sum / success_count).ToString("F3"));
                        write(info_out, info);
                        writeCsv(diff_avariage, sno);
                    }
                    else
                    {
                        info += "+ No node accessed !  \n";
                        write(info_out, info);
                    }
                    return;
                }
                object csv_out_look = new object();
                string csv_out;
                public string getSyncTableCsv()
                {
                    string rev;
                    lock (csv_out_look)
                    {
                        rev = csv_out;
                        csv_out = null;
                    }
                    return rev;
                }
                byte[] addrs;
                void writeCsv(double diff_avariage, byte sno)
                {
                    lock (csv_out_look)
                    {
                        if (csv_out == null)
                        {
                            addrs = new byte[bus.Count];
                            csv_out = "时间,同步序号,平均误差,";
                            csv_out += string.Format("基准节点{0},误差,",clock_base_node.addr);
                            int i = 0;
                            addrs[i++] = clock_base_node.addr;
                            foreach (Node node in bus)
                            {
                                if (node == clock_base_node)
                                {
                                    continue;
                                }
                                addrs[i++] = node.addr;
                                csv_out += string.Format("节点{0},误差,", node.addr);
                            }
                        }
                        csv_out += "\n";
                        csv_out += System.DateTime.Now.ToLongTimeString() + ",";
                        csv_out += (int)sno + ",";
                        csv_out += diff_avariage.ToString("F3") + ",";
                        int base_node_clock = clock_base_node.syncClu.getClockInt(); ;
                        foreach (byte a in addrs)
                        {
                            Node node = bus[a];
                            if (node.syncClu.sno != sno)
                            {
                                csv_out += "序号错误,,";
                            }
                            else if ((node.syncClu.is_sync_miss != false))
                            {
                                csv_out += "同步失败,,";
                            }
                            else
                            {
                                int c = node.syncClu.getClockInt();
                                int diff = c - base_node_clock;
                                csv_out += string.Format(" {0},{1},", c, diff);
                            }
                        }
                    }
                }
                void writeSyncMarkCsv(int clock, byte sno)
                {
                    lock (csv_out_look)
                    {
                        if (csv_out != null)
                        {
                            csv_out += "\n";
                            csv_out += System.DateTime.Now.ToLongTimeString() + ",";
                            csv_out += (int)sno + ",";
                            csv_out += "-1,";
                            int base_node_clock = clock_base_node.syncClu.getClockInt(); ;
                            csv_out += string.Format(" {0},", clock);
                        }
                    }
                }
            }
        }
    }
}
