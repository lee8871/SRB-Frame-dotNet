using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SRB_CTR
{
    abstract class IBrain
    {
        protected SrbFrame frame;
        public IBrain(SrbFrame f)
        {
            frame = f;

            log = new Log_Writer("Brain");
            log.add("new Brain log!");
            log.autoFlushRun();
        }
        public abstract void calculate();


        protected Thread calculation_thread;
        protected bool running_flag = false;
        public bool Running_flag
        {
            get { return running_flag; }
        }
        protected bool is_calculate_running = false;


        Log_Writer log;

        protected virtual void onRun()
        {
            running_flag = true;
            calculation_thread = new Thread(new ThreadStart(thLoop));
            calculation_thread.Priority = ThreadPriority.Highest;
            is_calculate_running = true;
            calculation_thread.Start();
        }
        protected virtual void onStop()
        {
            is_calculate_running = false;
        }
        public virtual bool stop()
        {
            if (is_calculate_running)
            {
                running_flag = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool run()
        {
            if (is_calculate_running)
            {
                return false;
            }
            else
            {
                onRun();
                return true;
            }
        }
        protected double period_in_ms = 2.5;
        protected long loop_num = 0;
        protected virtual void thLoop()
        {
            Stopwatch sw = new Stopwatch();
            double calculate_time, all_time;
            while (running_flag == true)
            {
                sw.Restart();
                calculate();
                calculate_time = sw.getElapsedMs();
                frame.sendAccess();
                all_time = sw.getElapsedMs();
#if DEBUG
                log.add(string.Format("{0},{1:###0.0000},{2:###0.0000}", loop_num, calculate_time, all_time));
#else
                if(all_time>period_in_ms)
                {
                    log.add(string.Format("Not_real_time = ('{3}',{0},{1:###0.0000},{2:###0.0000},)", 
                        loop_num,           calculate_time, 
                        all_time,           DateTime.Now.ToString("hh:mm:ss.fff")));
                }
#endif
                loop_num++;
                while (sw.getElapsedMs() < period_in_ms) ;
            }
            onStop();
        }
    }
}
