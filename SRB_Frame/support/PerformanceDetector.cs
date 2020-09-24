using System;
using System.Diagnostics;

namespace SRB.Frame.PerformanceDetector
{
    public class PerformanceDetector
    {
        int check_times = 0;
        int record_times = 0;
        long[] bank;
        long[] temp_bank;
        int grain_size;
        bool enabled = false;

        int current_page_base;
        public long this[int i]
        {
            get
            {
                if (i >= grain_size)
                {
                    throw new OverflowException();
                }
                return bank[current_page_base + i];
            }
        }
        public void initPage()
        {
            current_page_base = 0;
        }
        public bool nextPage()
        {
            current_page_base += grain_size;
            return (current_page_base / grain_size < record_times);
        }


        public int Check_times => check_times;
        public int Record_times => record_times;
        public int Grain_size => grain_size;
        public bool Enabled => enabled;

        public PerformanceDetector(int grain_size)
        {
            this.grain_size = grain_size;
            temp_bank = new long[grain_size];
            bank = new long[grain_size * 4096];
        }
        public void beginCheck()
        {
            check_times++;
            enabled = true;
            for (int i = 0; i < grain_size; i++)
            {
                temp_bank[i] = -1;
            }
            temp_bank[0] = Stopwatch.GetTimestamp();
        }
        public void checkPoint(int i)
        {
            if (enabled)
            {
                temp_bank[i] = Stopwatch.GetTimestamp();
            }
        }
        public void checkPoint(int i, long tick)
        {
            if (enabled)
            {
                temp_bank[i] = tick;
            }
        }
        public delegate bool delegate_RecordCheck(long[] temp_bank);
        public void endCheckPoint(delegate_RecordCheck check)
        {
            if (enabled)
            {
                enabled = false;
                if (check(temp_bank))
                {
                    int offset = record_times * grain_size;
                    for (int i = 0; i < grain_size; i++)
                    {
                        bank[i + offset] = temp_bank[i];
                    }
                    record_times++;
                }
            }
        }
    }
}
