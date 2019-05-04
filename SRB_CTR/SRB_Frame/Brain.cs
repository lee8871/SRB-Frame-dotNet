using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using SRB.Frame;

namespace SRB_CTR
{
    abstract class IBrain
    {
        protected SrbFrame frame;
        Log_Writer log;
        public IBrain(SrbFrame f)
        {
            frame = f;
            log = new Log_Writer("Brain");
            log.add("new Brain log!");
            log.autoFlushRun();
        }


        protected abstract void nodesBuildUp();  
        protected abstract void setup();   
        protected abstract void loop();    
        protected abstract void termination();   
        protected Thread calculation_thread;


        public  bool Is_running
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

        delegate void dRunStep();
        Stopwatch sw = new Stopwatch();
        double calculate_time, all_time;

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


        void nextRealTimeLoop(long num)
        {
            calculate_time = sw.getElapsedMs();
            frame.sendAccess();
            all_time = sw.getElapsedMs();
#if DEBUG
            log.add(string.Format("{0},{1:###0.0000},{2:###0.0000}", num, calculate_time, all_time));
#else
            if (all_time > period_in_ms)
            {
                log.add(string.Format("Not_real_time = ('{3}',{0},{1:###0.0000},{2:###0.0000},)",
                    num, calculate_time,
                    all_time, DateTime.Now.ToString("hh:mm:ss.fff")));
            }
#endif
            while (sw.getElapsedMs() < period_in_ms) ;
            sw.Restart();
        }









        #endregion

    }
}
