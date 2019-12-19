using System;
using System.Diagnostics;
using System.Threading;

namespace SRB_CTR
{
    internal abstract class IBrain
    {
        protected SrbOnelineMaster frame;
        public IBrain(SrbOnelineMaster f)
        {
            frame = f;
        }
        protected abstract void nodesBuildUp();
        protected abstract void setup();
        protected abstract void loop();
        protected abstract void termination();
        protected Thread calculation_thread;


        public bool Is_running
        {
            get
            {
                if (calculation_thread != null)
                {
                    return calculation_thread.IsAlive;
                }
                else
                {
                    return false;
                }
            }
        }
        protected bool stop_running_flag = false;
        public bool stop()
        {
            if (Is_running)
            {

                stop_running_flag = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool run()
        {
            if (Is_running)
            {
                return false;
            }
            else
            {
                stop_running_flag = false;
                nodesBuildUp();
                calculation_thread = new Thread(new ThreadStart(thLoop));
                calculation_thread.Priority = ThreadPriority.Highest;
                calculation_thread.Start();
                return true;
            }
        }
        #region about thread
        protected double period_in_ms = 2.5;
        protected long loop_num = 0;

        private delegate void dRunStep();

        private Stopwatch sw = new Stopwatch();
        private double calculate_time, all_time;

        protected virtual void thLoop()
        {
            sw.Restart();
            setup();
            nextRealTimeLoop(-1);
            while (stop_running_flag == false)
            {
                loop();
                nextRealTimeLoop(loop_num++);
            }
            termination();
            nextRealTimeLoop(-2);
        }

        private void nextRealTimeLoop(long num)
        {
            calculate_time = sw.getElapsedMs();
            frame.Bus.sendAccess();
            all_time = sw.getElapsedMs();
            if (all_time > period_in_ms)
            {
                Console.WriteLine(string.Format("'{3}' Not_real_time = (,num = {0},calculate_time = {1:###0.0000},usb_time = {2:###0.0000},)",
                    num, calculate_time,
                    (all_time - calculate_time), DateTime.Now.ToString("hh:mm:ss.fff")));
            }
            while (sw.getElapsedMs() < period_in_ms) ;
            sw.Restart();
        }









        #endregion

    }
}
