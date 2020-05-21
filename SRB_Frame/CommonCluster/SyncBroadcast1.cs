using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
这个的问题是同步效果不好，本身系统时钟到总线时钟是存在10 SrbTick左右误差的，
这种误差可能来自USB发送设备的周期，也很可能来自电脑
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
                public Broadcast(IBus bus)
                {
                    this.bus = bus;
                    Sync_public_sno = (byte)support.random.Next(127);
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
                    { BaseNode.SyncCluster.FIX_CID, Sync_public_sno});
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


                    int clock = getSrbClock(sync_elapsed_ticks);
                    ushort ms;
                    byte us4;
                    SyncCluster.intToClock(out ms, out us4, clock);
                    syncToBroadcast(ms, us4, sno);
                    if (details != null)
                    {
                        write(details, "#### Sync all by broadcast!\n");
                    }
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
                        clk_base = tickToDoubleSrbClock(tick_totle) * 1.0 / count;
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

                public void getSyncStatuc(dInfoOut info_out)
                {
                    string info="";
                    long sync_elapsed_ticks;
                    byte sno = recordTimeBroadcast(out sync_elapsed_ticks);

                    info+="#### Get all time statuc  \n";
                    int base_clock = getSrbClock(sync_elapsed_ticks);
                    ushort ms;
                    byte us4;
                    SyncCluster.intToClock(out ms, out us4, base_clock);
                    double diff_sum = 0;
                    info += "\n";
                    info += string.Format("Addr | ms1.024 | us4 | Totel int|Diff\n");
                    info += string.Format("----|----|----|----|----\n");

                    info += string.Format(" {0} | {1} | {2} | {3} | {4}\n", "PC",
                        (ms*1.0 / 1000).ToString("F3"), us4, base_clock,"--");
                    int success_count = 0;
                    foreach (BaseNode node in bus)
                    {
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
                        if (diff_avariage >= 1)
                        {
                            info += "+ Diff avariage out of range!\n";
                        }
                        info += string.Format("+ Diff avariage is {0}\n", (diff_sum / success_count).ToString("F3"));
                    }
                    else
                    {
                        info += "+ No node accessed !  \n";
                    }
                    write(info_out, info);
                    return;
                }




            }
        }
    }
}
