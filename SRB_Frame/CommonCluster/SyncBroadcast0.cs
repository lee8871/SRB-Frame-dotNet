using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SRB.Frame
{
    public partial class BaseNode
    {
        public partial class SyncCluster : BaseNode.ICluster
        {
            public class Broadcast
            {
                public delegate void dInfoOut(string st);
                public dInfoOut debug;

                private void write(dInfoOut info , string st)
                {
                    if (info != null)
                    {
                        info(st);
                    }
                }

                byte Sync_public_sno;
                IBus bus;
                Stopwatch sync_clock;
                public Broadcast(IBus bus)
                {
                    sync_clock = new Stopwatch();
                    sync_clock.Restart();
                    this.bus = bus;
                    Sync_public_sno = (byte)support.random.Next(127);
                }

                public int getClockInt()
                {
                    return (int)(((long)tickToClock(sync_clock.ElapsedTicks)) & 0xffffff);
                }
                public int getClockInt(long elapsed_ticks)
                {
                    return (int)(((long)tickToClock(elapsed_ticks)) & 0xffffff);
                }
                public long getTotolTicks()
                {
                    return ((long)tickToClock(sync_clock.ElapsedTicks)) >> 8;
                }
                public long getTotolTicks(long elapsed_ticks)
                {
                    return ((long)tickToClock(elapsed_ticks)) >> 8;
                }


                private byte recordTimeBroadcast(out long elapsed_ticks)
                {
                    Access ac = new Access(null, null, Access.PortEnum.Cgf, new byte[]
                    { BaseNode.SyncCluster.FIX_CID, Sync_public_sno});
                    byte rev = Sync_public_sno;
                    Sync_public_sno++;
                    if (Sync_public_sno == 128)
                    {
                        Sync_public_sno = 0;
                    }
                    elapsed_ticks = sync_clock.ElapsedTicks;
                    bus.singleAccess(ac);
                    return rev;
                }

                public void syncToBroadcast(ushort ms, byte us4, byte sno)
                {
                    Access ac = new Access(null, null, Access.PortEnum.Cgf, new byte[]
                    { BaseNode.SyncCluster.FIX_CID, sno,us4, ms.ByteLow(),ms.ByteHigh()});
                    bus.singleAccess(ac);
                    return;
                }
                public void syncAll(dInfoOut details)
                {
                    long sync_elapsed_ticks;
                    byte sno = recordTimeBroadcast(out sync_elapsed_ticks);


                    int clock = getClockInt(sync_elapsed_ticks);
                    ushort ms;
                    byte us4;
                    SyncCluster.intToClock(out ms, out us4, clock);


                    syncToBroadcast(ms, us4, sno);

                    if (details != null)
                    {
                        write(details, "#### Sync all by broadcast\n");
                    }
                }

                public int getAimNodeTime(byte aim_node_addr, out byte sno, dInfoOut details)
                {
                    long sync_elapsed_ticks;
                    sno = recordTimeBroadcast(out sync_elapsed_ticks);
                    Queue<BaseNode> sync_success_nodes = new Queue<BaseNode>();

                    foreach (BaseNode node in bus)
                    {
                        if (node != null)
                        {
                            if (node.Addr == aim_node_addr)
                            {
                                node.syncClu.read();//这个可以把所有节点的访问放在一起加快速度。
                                if ((node.syncClu.sno == sno) && (node.syncClu.is_sync_miss == false))
                                {
                                    return node.syncClu.getClockInt();
                                }
                            }
                        }
                    }
                    return -1;
                }
                public void calibratClean(dInfoOut details)
                {
                    write(details, "All calibration values clear to 0.\n");
                    foreach (BaseNode node in bus)
                    {
                        node.syncClu.calibrationClu.calibration_value = 0;
                        node.syncClu.calibrationClu.write();
                    }
                    return;
                }


                BaseNode clock_base_node = null;




                private Thread calibratT = null;
                public bool Is_calibrat_running
                {
                    get
                    {
                        if (calibratT != null)
                        {
                            return calibratT.IsAlive;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                dInfoOut calibrat_info = null;
                int abandon_calibrat = 0;
                public void calibrat(dInfoOut cinfo=null)
                {
                    if (Is_calibrat_running)
                    {
                        throw new ObjectDisposedException("Calibrat");
                    }
                    else
                    {
                        calibrat_info = cinfo;
                        abandon_calibrat = 0;
                        calibratT = new Thread(calibratThread);
                        calibratT.Start();
                        return;
                    }
                }
                public void calibratAbandon()
                {
                    if (Is_calibrat_running)
                    {
                        abandon_calibrat = 1;
                    }
                }

                private void calibratThread()
                {
                    calibrat_info(null);
                    int node_count = bus.Count;
                    if (node_count < 1)
                    {
                        write(calibrat_info, string.Format("No node to be calibrated\n"));
                        return;
                    }

                    int record_count = 512;


                    byte[] addrs = new byte[node_count];
                    {
                        int i = 0;
                        foreach (BaseNode node in bus)
                        {
                            addrs[i++] = node.Addr;
                        }
                    }
                    var last_Sync_value = new long[node_count];
                    for (int i = 0; i < node_count; i++)
                    {
                        last_Sync_value[i] = 0;
                    }


                    write(calibrat_info, string.Format("Calibrat {0} Nodes. Clock base is this PC.\n", node_count));

                    void readGroup(out long[,] sync_values_t2)
                    {
                        sync_values_t2 = new long[node_count + 1, record_count];
                        for (int i = 0; i < record_count; i++)
                        {
                            byte sno = recordTimeBroadcast(out sync_values_t2[node_count, i]);
                            for (int j = 0; j < node_count; j++)
                            {
                                BaseNode node = bus[addrs[j]];
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
                        }
                    }
                    long[,] sync_bgn_values_t2;
                    readGroup(out sync_bgn_values_t2);
                    write(calibrat_info, string.Format("Record clock Begin done.\n"));
                    write(calibrat_info, string.Format("Wait 30 seconds:\n"));
                    write(calibrat_info, string.Format("______________________________\n"));
                    for (int i = 0; i < 30; i++)
                    {
                        Thread.Sleep(1000);
                        write(calibrat_info, string.Format("="));
                        if (abandon_calibrat == 1)
                        {
                            write(calibrat_info, string.Format("\n"));
                            write(calibrat_info, string.Format("Calebrat abandoned!\n"));
                            return;
                        }
                    }
                    write(calibrat_info, string.Format("\n"));
                    long[,] sync_end_values_t2;
                    readGroup(out sync_end_values_t2);
                    write(calibrat_info, string.Format("Record clock End done.\n"));

                    void calculate(int j, in long[,] sync_values_t2, out double clk_base, out double clk_node)
                    {
                        int count = 0;
                        clk_base = 0;
                        clk_node = 0;
                        long tick_totle = 0;
                        long clock_totle = 0;
                        for (int i = 0; i < record_count; i++)
                        {
                            if(-1 != sync_values_t2[j, i])
                            {
                                count++;
                                tick_totle += sync_values_t2[node_count, i];
                                clock_totle += sync_values_t2[j, i];
                            }
                        }
                        clk_base = tickToClock(tick_totle) * 1.0 / count; 
                        clk_node = clock_totle * 1.0 / count;
                    }
                    for (int j = 0; j < node_count; j++)
                    {
                        BaseNode node = bus[addrs[j]];
                        double bgn_b, bgn_n, end_b, end_n;
                        calculate(j, in sync_bgn_values_t2, out bgn_b, out bgn_n);
                        calculate(j, in sync_end_values_t2, out end_b, out end_n);
                        double calibration_increase_totle = ((end_b - bgn_b) - (end_n - bgn_n)) * 0x4000;
                        double increase_count = (end_n - bgn_n) / (32 * 256 * 1.0);
                        double increase_for_each_times = calibration_increase_totle / increase_count;
                        node.syncClu.calibrationClu.read();
                        node.syncClu.calibrationClu.calibration_value += (int)(Math.Round(increase_for_each_times));
                        node.syncClu.calibrationClu.write();
                    }
                    write(calibrat_info, string.Format("Calculate calibration value and write.\n"));
                    return;
                }

                double tickToClock(long tick)
                {
                    double rev = tick;
                    rev /= Stopwatch.Frequency;
                    rev *= 250000;
                    return rev;

                }
                public int getAvariageTime(out BaseNode[] successed_node_array, out byte sno, dInfoOut details)
                {
                    long sync_elapsed_ticks;
                    sno = recordTimeBroadcast(out sync_elapsed_ticks);
                    double totle_clock = 0;
                    int avariage;
                    Queue<BaseNode> sync_success_nodes = new Queue<BaseNode>();

                    foreach (BaseNode node in bus)
                    {
                        if (node != null)
                        {
                            node.syncClu.read();//这个可以把所有节点的访问放在一起加快速度。
                            if ((node.syncClu.sno == sno) && (node.syncClu.is_sync_miss == false))
                            {
                                sync_success_nodes.Enqueue(node);
                                totle_clock += node.syncClu.getClockInt();
                            }
                        }
                    }
                    successed_node_array = sync_success_nodes.ToArray();
                    if (successed_node_array.Length != 0)
                    {
                        avariage = (int)Math.Round(totle_clock / successed_node_array.Length);
                    }
                    else
                    {
                        avariage = -1;
                    }
                    if (details != null)
                    {
                        write(details, "#### Get all recorded time\n");
                        write(details, string.Format("\nAddr | ms1.024 | us4 | Totel int\n"));
                        write(details, string.Format("----|----|----|----\n"));
                        if (sync_success_nodes.Count != 0)
                        {
                            ushort ms;
                            byte us4;
                            SyncCluster.intToClock(out ms, out us4, avariage);
                            write(details, string.Format("avariage |{0}.{1} | {2} | {3}\n",
                             ms / 1000, ms % 1000, us4, avariage));

                            foreach (BaseNode node in bus)
                            {
                                if (node != null)
                                {
                                    if (node.syncClu.sno != sno)
                                    {
                                        write(details, string.Format("Addr|not receive|x|x\n", node.Addr));
                                    }
                                    else if ((node.syncClu.is_sync_miss != false))
                                    {
                                        write(details, string.Format("Addr|sync miss|x|x\n", node.Addr));
                                    }
                                    else
                                    {
                                        int c = node.syncClu.getClockInt();
                                        write(details, string.Format(" {0} | {1}.{2} | {3} | {4}\n",
                                            node.Addr, node.syncClu.ms / 1000, node.syncClu.ms % 1000, node.syncClu.us4, c));
                                    }
                                }
                            }
                            write(details, "\n");
                        }
                    }
                    return avariage;
                }

                public void syncAllBroadcast(int time, byte sno, dInfoOut details)
                {
                    ushort ms;
                    byte us4;
                    SyncCluster.intToClock(out ms, out us4, time);
                    syncToBroadcast(ms, us4, sno);

                    if (details != null)
                    {
                        write(details, "#### Sync all by broadcast\n");

                    }
                }
                public void getSyncStatuc(dInfoOut details)
                {
                    if (details == null)
                    {
                        throw new Exception("report sync status should write info to string");
                    }
                    long sync_elapsed_ticks;
                    byte sno = recordTimeBroadcast(out sync_elapsed_ticks);
                    double totle_clock = 0;
                    int success_count = 0;
                    int fail_count = 0;
                    Queue<BaseNode> sync_success_nodes = new Queue<BaseNode>();
                    foreach (BaseNode node in bus)
                    {
                        if (node != null)
                        {
                            node.syncClu.read();//这个可以把所有节点的访问放在一起加快速度。
                            if ((node.syncClu.sno == sno) && (node.syncClu.is_sync_miss == false))
                            {
                                sync_success_nodes.Enqueue(node);
                                int c = node.syncClu.getClockInt();
                                totle_clock += c;
                                success_count++;
                            }
                            else
                            {

                                fail_count++;
                            }
                        }
                    }
                    if (success_count != 0)
                    {
                        write(details, "#### Get all time statuc  \n");

                        write(details, string.Format("+ {0}/{1} node accessed for sync!\n", success_count, success_count + fail_count));
                        double avariage = totle_clock / success_count;
                        ushort ms;
                        byte us4;
                        SyncCluster.intToClock(out ms, out us4, (int)Math.Round(totle_clock / success_count));

                        double diff_sum = 0;

                        write(details, "\n");
                        write(details, string.Format("Addr | ms1.024 | us4 | Totel int|Diff\n"));
                        write(details, string.Format("----|----|----|----|----\n"));
                        write(details, string.Format("avariage |{0}.{1} | {2} | {3} | -- \n",
                        ms / 1000, ms % 1000, us4, avariage.ToString("F3")));
                        foreach (BaseNode node in bus)
                        {
                            if (node != null)
                            {
                                if (node.syncClu.sno != sno)
                                {
                                    write(details, string.Format("Addr|not receive|x|x|x\n", node.Addr));
                                }
                                else if ((node.syncClu.is_sync_miss != false))
                                {
                                    write(details, string.Format("Addr|sync miss|x|x|x\n", node.Addr));
                                }
                                else
                                {
                                    double c = node.syncClu.getClockInt();
                                    double diff = c - avariage;
                                    diff_sum += System.Math.Abs(diff);
                                    write(details, string.Format(" {0} | {1}.{2} | {3} | {4} | {5}\n", node.Addr,
                                            node.syncClu.ms / 1000, node.syncClu.ms % 1000, node.syncClu.us4, c, diff.ToString("F3")));
                                }
                            }
                        }
                        write(details, "\n");
                        double diff_avariage = diff_sum / success_count;
                        if (diff_avariage >= 1)
                        {
                            write(details, "+ Diff avariage out of range!\n");
                        }
                        write(details, string.Format("+ Diff avariage is {0}\n", (diff_sum / success_count).ToString("F3")));
                    }
                    else
                    {
                        write(details, "+ No node accessed !  \n");
                    }
                    return;
                }






                private void calibratThread2()
                {
                    calibrat_info(null);
                    int node_count = bus.Count;
                    if (node_count < 2)
                    {
                        write(calibrat_info, string.Format("Calibrat {0} Node fall. Node should more than 2.\n", node_count));
                        return;
                    }


                    BaseNode clock_base_node;
                    if (this.clock_base_node == null)
                    {
                        //clock_base_node = findTimeBaseNode();
                    }
                    else
                    {
                        clock_base_node = this.clock_base_node;
                    }
                    int record_count = 512;
                    byte[] addrs = new byte[node_count];
                    {
                        int i = 0;
                        addrs[i++] = clock_base_node.Addr;
                        foreach (BaseNode node in bus)
                        {
                            if (node != clock_base_node)
                            {
                                addrs[i++] = node.Addr;
                            }
                        }
                    }
                    int[] last_Sync_value = new int[node_count];
                    for (int i = 0; i < node_count; i++)
                    {
                        last_Sync_value[i] = 0;
                    }
                    write(calibrat_info, string.Format("Calibrat {0} Nodes. Clock base is {1}.\n", node_count, clock_base_node.ToString()));

                    void readGroup(out int[,] sync_values_t2)
                    {
                        sync_values_t2 = new int[node_count, record_count];
                        for (int i = 0; i < record_count; i++)
                        {
                            long sync_elapsed_ticks;
                            byte sno = recordTimeBroadcast(out sync_elapsed_ticks);
                            clock_base_node.syncClu.read();
                            int clk_value = clock_base_node.syncClu.getClockInt(sno);
                            sync_values_t2[0, i] = clk_value;

                            if (clk_value != -1)
                            {
                                while (last_Sync_value[0] > sync_values_t2[0, i])
                                {
                                    sync_values_t2[0, i] += 0x01000000;
                                }
                                last_Sync_value[0] = sync_values_t2[0, i];
                                for (int j = 1; j < node_count; j++)
                                {
                                    BaseNode node = bus[addrs[j]];
                                    node.syncClu.read();
                                    clk_value = node.syncClu.getClockInt(sno);
                                    sync_values_t2[j, i] = clk_value;

                                    if (clk_value != -1)
                                    {
                                        while (last_Sync_value[j] > sync_values_t2[j, i])
                                        {
                                            sync_values_t2[j, i] += 0x01000000;
                                        }
                                        last_Sync_value[j] = sync_values_t2[j, i];
                                    }

                                }
                            }
                        }
                    }
                    int[,] sync_bgn_values_t2;
                    readGroup(out sync_bgn_values_t2);
                    write(calibrat_info, string.Format("Record clock Begin done.\n"));
                    write(calibrat_info, string.Format("Wait 30 seconds.\n"));
                    write(calibrat_info, string.Format("Waitting"));
                    for (int i = 0; i < 30; i++)
                    {
                        Thread.Sleep(1000);
                        write(calibrat_info, string.Format("▨"));
                        if (abandon_calibrat == 1)
                        {
                            write(calibrat_info, string.Format("\n"));
                            write(calibrat_info, string.Format("Calebrat abandoned!\n"));
                            return;
                        }
                    }
                    write(calibrat_info, string.Format("\n"));
                    int[,] sync_end_values_t2;
                    readGroup(out sync_end_values_t2);
                    write(calibrat_info, string.Format("Record clock End done.\n"));

                    void calculate(int j, in int[,] sync_values_t2, out double clk_base, out double clk_node)
                    {
                        int count = 0;
                        clk_base = 0;
                        clk_node = 0;
                        for (int i = 0; i < record_count; i++)
                        {
                            if ((-1 != sync_values_t2[0, i]) && (-1 != sync_values_t2[j, i]))
                            {
                                count++;
                                clk_base += sync_values_t2[0, i];
                                clk_node += sync_values_t2[j, i];
                            }
                        }
                        clk_base /= count;
                        clk_node /= count;
                    }
                    for (int j = 1; j < node_count; j++)
                    {
                        BaseNode node = bus[addrs[j]];
                        double bgn_b, bgn_n, end_b, end_n;
                        calculate(j, in sync_bgn_values_t2, out bgn_b, out bgn_n);
                        calculate(j, in sync_end_values_t2, out end_b, out end_n);
                        double calibration_increase_totle = ((end_b - bgn_b) - (end_n - bgn_n)) * 0x4000;
                        double increase_count = (end_n - bgn_n) / (64 * 256 * 1.0);
                        double increase_for_each_times = calibration_increase_totle / increase_count;
                        node.syncClu.calibrationClu.read();
                        node.syncClu.calibrationClu.calibration_value += (int)(Math.Round(increase_for_each_times));
                        node.syncClu.calibrationClu.write();
                    }
                    write(calibrat_info, string.Format("Calculate calibration value and write.\n"));
                    return;
                }
                private BaseNode findTimeBaseNode()
                {
                    foreach (BaseNode node in bus)
                    {
                        if (node != null)
                        {
                            node.syncClu.calibrationClu.read();
                            if (node.syncClu.calibrationClu.Is_read_done == true)
                            {
                                if (node.syncClu.calibrationClu.calibration_value == 0)
                                {
                                    write(debug, "#### Calibrat set base clock node \n");
                                    write(debug, string.Format("+ node {0} is base node. \n", node.Addr));
                                    write(debug, "\n");
                                    return node;
                                }
                            }
                        }
                    }
                    write(debug, "#### Calibrat set base clock node \n");
                    write(debug, "+ can not find base node. \n");
                    write(debug, "\n");
                    return null;
                }

                public void syncToAimNode2(byte aim_node_addr, dInfoOut details)
                {
                    byte sno;
                    int time = getAimNodeTime(aim_node_addr, out sno, details);
                    if (time != -1)
                    {
                        syncAllBroadcast(time, sno, details);
                    }
                    // syncDebugAll(nodes,ref details);
                    return;
                }
                public void syncDebugAll(BaseNode[] successed_node_array, dInfoOut details)
                {
                    if (details == null)
                    {
                        throw new Exception("report sync status should write info to string");
                    }
                    write(details, "#### debug check\n");
                    write(details, string.Format("\nAddr | Offset | OldTime | NewTime \n"));
                    write(details, string.Format("----|----|----|----\n"));
                    foreach (BaseNode node in successed_node_array)
                    {
                        node.debugClu.read();

                        write(details, string.Format(" {0} | {1} | {2} | {3} \n",
                            node.Addr,
                        (int)support.byteToUint32(node.debugClu.Bank, 0),
                        (int)support.byteToUint32(node.debugClu.Bank, 4),
                        (int)support.byteToUint32(node.debugClu.Bank, 8)));
                    }
                    write(details, "\n");
                }
                public int syncAllAvariageUnicast(dInfoOut details)
                {
                    long sync_elapsed_ticks;
                    byte sno = recordTimeBroadcast(out sync_elapsed_ticks);
                    double totle_clock = 0;
                    int success_count = 0;
                    int fail_count = 0;
                    Queue<BaseNode> sync_success_nodes = new Queue<BaseNode>();
                    write(details, "\n");
                    write(details, string.Format("Addr | ms1.024 | us4 | Totel int\n"));
                    write(details, string.Format("----|----|----|----\n"));
                    foreach (BaseNode node in bus)
                    {
                        if (node != null)
                        {
                            node.syncClu.read();//这个可以把所有节点的访问放在一起加快速度。

                            if (node.syncClu.sno != sno)
                            {
                                write(details, string.Format("Addr|not receive|x|x\n", node.Addr));
                            }
                            else if ((node.syncClu.is_sync_miss != false))
                            {
                                write(details, string.Format("Addr|sync miss|x|x\n", node.Addr));
                            }
                            else
                            {
                                sync_success_nodes.Enqueue(node);
                                int c = node.syncClu.getClockInt();
                                totle_clock += c;
                                write(details, string.Format(" {0} | {1}.{2} | {3} | {4}\n",
                                    node.Addr, node.syncClu.ms / 1000, node.syncClu.ms % 1000, node.syncClu.us4, c));
                                success_count++;
                            }
                        }
                    }
                    if (success_count != 0)
                    {
                        int avariage = (int)Math.Round(totle_clock / success_count);
                        ushort ms;
                        byte us4;
                        SyncCluster.intToClock(out ms, out us4, avariage);
                        write(details, string.Format("avariage |{0}.{1} | {2} | {3}\n",
                         ms / 1000, ms % 1000, us4, avariage));
                        write(details, "\n");
                        write(details, string.Format("+ {0}/{1} node accessed for sync!\n", success_count, success_count + fail_count));
                        foreach (BaseNode node in sync_success_nodes)
                        {
                            node.syncClu.syncTo(ms, us4, sno);
                        }
                        write(details, string.Format("\nOffset | OldTime | NewTime \n"));
                        write(details, string.Format("----|----|----\n"));
                        foreach (BaseNode node in sync_success_nodes)
                        {
                            node.debugClu.read();

                            write(details, string.Format(" {0} | {1} | {2} \n",
                            (int)support.byteToUint32(node.debugClu.Bank, 0),
                            (int)support.byteToUint32(node.debugClu.Bank, 4),
                            (int)support.byteToUint32(node.debugClu.Bank, 8)));
                        }
                        write(details, "\n");

                    }
                    return fail_count;
                }
                public int syncAllAvariageUnicastDebug(dInfoOut details)
                {
                    long sync_elapsed_ticks;
                    byte sno = recordTimeBroadcast(out sync_elapsed_ticks);
                    double totle_clock = 0;
                    int success_count = 0;
                    int fail_count = 0;
                    Queue<BaseNode> sync_success_nodes = new Queue<BaseNode>();
                    write(details, "\n");
                    write(details, string.Format("Addr | ms1.024 | us4 | Totel int\n"));
                    write(details, string.Format("----|----|----|----\n"));
                    foreach (BaseNode node in bus)
                    {
                        if (node != null)
                        {
                            node.syncClu.read();//这个可以把所有节点的访问放在一起加快速度。

                            if (node.syncClu.sno != sno)
                            {
                                write(details, string.Format("Addr|not receive|x|x\n", node.Addr));
                            }
                            else if ((node.syncClu.is_sync_miss != false))
                            {
                                write(details, string.Format("Addr|sync miss|x|x\n", node.Addr));
                            }
                            else
                            {
                                sync_success_nodes.Enqueue(node);
                                int c = node.syncClu.getClockInt();
                                totle_clock += c;
                                write(details, string.Format(" {0} | {1}.{2} | {3} | {4}\n",
                                    node.Addr, node.syncClu.ms / 1000, node.syncClu.ms % 1000, node.syncClu.us4, c));
                                success_count++;
                            }
                        }
                    }
                    if (success_count != 0)
                    {
                        int avariage = (int)Math.Round(totle_clock / success_count);
                        ushort ms;
                        byte us4;
                        SyncCluster.intToClock(out ms, out us4, avariage);
                        write(details, string.Format("avariage |{0}.{1} | {2} | {3}\n",
                         ms / 1000, ms % 1000, us4, avariage));
                        write(details, "\n");
                        write(details, string.Format("+ {0}/{1} node accessed for sync!\n", success_count, success_count + fail_count));
                        foreach (BaseNode node in sync_success_nodes)
                        {
                            node.syncClu.syncTo(ms, us4, sno);
                        }
                        write(details, string.Format("\nOffset | OldTime | NewTime \n"));
                        write(details, string.Format("----|----|----\n"));
                        foreach (BaseNode node in sync_success_nodes)
                        {
                            node.debugClu.read();

                            write(details, string.Format(" {0} | {1} | {2} \n",
                            (int)support.byteToUint32(node.debugClu.Bank, 0),
                            (int)support.byteToUint32(node.debugClu.Bank, 4),
                            (int)support.byteToUint32(node.debugClu.Bank, 8)));
                        }
                        write(details, "\n");

                    }
                    return fail_count;
                }
                public void syncAll2(dInfoOut details)
                {
                    BaseNode[] nodes;
                    byte sno;
                    int time = getAvariageTime(out nodes, out sno, details);
                    if (time != -1)
                    {
                        syncAllBroadcast(time, sno, details);
                    }
                    return;
                }
            }
        }
    }
}
