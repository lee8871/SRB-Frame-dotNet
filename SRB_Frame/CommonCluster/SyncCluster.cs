﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Windows.Forms;
namespace SRB.Frame
{
    public partial class Node
    {
        public partial class SyncCluster : Node.ICluster
        {

            private CalibrationCluster calibrationClu;
            public const byte FIX_CID = 8;
            public int us4 { get => bank.getBankByte(1); }
            public int ms { get => bank.getBankUshort(2); }
            public int sno { get => (int)bank[0, 7]; }
            public bool is_sync_miss { get => bank.getBankBool(0, 7); }
            public CalibrationCluster CalibrationClu { get => calibrationClu;}

            public SyncCluster(Node n)
                : base(n, FIX_CID, 4)
            {
                calibrationClu = new CalibrationCluster(n);
                following_clusters = new ICluster[] { calibrationClu };
            }
            public int getClockInt()
            {
                if (is_sync_miss)
                {
                    return -1;
                }
                else
                {
                    return (ms<<8 )+ us4;
                }
            }
            public int getClockInt(int check_sno)
            {
                if(check_sno != sno)
                {
                    return -1;
                }
                if (is_sync_miss)
                {
                    return -1;
                }
                else
                {
                    return (ms << 8) + us4;
                }
            }
            public void syncTo(ushort ms, byte us4, byte sno  )
            {
                Access ac = Bus.accessRequest(this, parent_node, AccessPort.Cgf);
                ac.Send_data[0] = CID;
                ac.Send_data[1] = sno;
                ac.Send_data[2] = us4;
                ac.Send_data[3] = ms.ByteLow();
                ac.Send_data[4] = ms.ByteHigh();
            }            
            public static void intToClock(out ushort ms, out byte us4,in int clock  )
            {
                if ((clock >0xffffff)||(clock<0) )
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    ms = (ushort)(clock>>8);
                    us4 = (byte)clock;
                }

            }
            public static ushort getMs(in int clock)
            {
                if ((clock > 0xffffff) || (clock < 0))
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    return (ushort)(clock >> 8);
                }

            }

            public static byte getUs(in int clock)
            {
                if ((clock > 0xffffff) || (clock < 0))
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    return (byte)clock;
                }

            }

            public override void write()
            {
                throw new Exception("read only cluster can not write.");
            }
            public override void writeRecv(Access ac)
            {
            }

            public override void readRecv(Access ac)
            {
                base.readRecv(ac);
            }
            protected override Control createControl()
            {
                return new SyncCC(this);
            }
            public override string ToString()
            {
                return string.Format("Sync Cluster{0}", CID.ToHexSt());
            }

        }
        public class CalibrationCluster : Node.ICluster
        {
            public const byte FIX_CID = 9;

            public int bgn = -1;

            public void captureCalibrationBegin(int sno)
            {
                if (sno == parent_node.syncClu.sno)
                {
                    bgn = parent_node.syncClu.getClockInt();
                }
                else
                {
                    bgn = -1;
                }
            }
            public void writeCalibration()
            {
               if(calibration_values_QUEUE.Count == 0)
                {
                    return;
                }
                double sum = 0;
                foreach(double v in calibration_values_QUEUE)
                {
                    sum += v;
                }
                double avariage = sum / calibration_values_QUEUE.Count;
                this.calibration_value = (int)(Math.Round(avariage));
                this.write();
            }
            public static int Calebrat_fail = Int32.MaxValue;
            public double newest_calibration_value = Calebrat_fail;
            public int end = -1;

            public int Avariage_Count { get => calibration_values_QUEUE.Count; }

            Queue<double> calibration_values_QUEUE = new Queue<double>();
            public void captureCalibrationEnd(int sno, int base_time)
            {
                int time;
                if (sno == parent_node.syncClu.sno)
                {
                    end = parent_node.syncClu.getClockInt();
                    if ((bgn != -1) && (end != -1))
                    {
                        time = end - bgn;
                        if (time < 0)
                        {
                            time += 0x01000000;
                        }
                        long calibration_increase_totle =( base_time - time )* 0x4000;
                        double increase_count = ((double)time) / (64*256*1.0);
                        double increase_for_each_times = calibration_increase_totle / increase_count;
                        newest_calibration_value = increase_for_each_times;
                        calibration_values_QUEUE.Enqueue(newest_calibration_value);
                        return;
                    }
                }
                newest_calibration_value = Calebrat_fail;
                return;
            }

            public int getTimeSpend(int sno)
            {
                int end;
                int time;
                if (sno == parent_node.syncClu.sno)
                {
                    end = parent_node.syncClu.getClockInt();
                    if ((bgn != -1) && (end != -1))
                    {
                        time = end - bgn;
                        if (time < 0)
                        {
                            time += 0x01000000;
                        }
                        return time;
                    }
                }
                 return Calebrat_fail;
            }
            public bool Is_read_done = false;

            public int calibration_value
            { 
                get => (short)bank.getBankUshort(0);
                set => bank.setBankUshort((ushort)value, 0);                    
            }
            public override void readRecv(Access ac)
            {
                Is_read_done = true;
                base.readRecv(ac);

            }
            public CalibrationCluster(Node n)
                : base(n, FIX_CID,2)
            {
                is_follower = false;
            }
            protected override Control createControl()
            {
                throw new Exception("This node do not has a control.");
            }
            public override string ToString()
            {
                return string.Format("Sync Cluster{0}", CID.ToHexSt());
            }
        }
    }
}
