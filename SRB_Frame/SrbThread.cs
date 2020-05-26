using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SRB.Frame
{
    public class SrbThread: IBus.IbusUser
    {
        public delegate bool dIsThreadStoping();

        public delegate void dSrbThreadStard(dIsThreadStoping stopCheck);
        dSrbThreadStard srbThreadStard_delegate;
        public SrbThread(dSrbThreadStard srbThreadStard)
        {
            srbThreadStard_delegate = srbThreadStard;
        }

        Thread thread;
        public bool Is_running
        {
            get
            {
                if (thread != null)
                {
                    return thread.IsAlive;
                }
                else
                {
                    return false;
                }
            }
        }
        int stoping = 0;
        IBus bus;
        //todo: 理论上说，一个Srb线程可以使用多个总线，这里可以扩展总线数量。
        public Thread run(IBus b)
        {
            if (Is_running == false)
            {
                this.bus = b;
                bus.addUser(this);
                stoping = 0;
                thread = new Thread(new ThreadStart(work));
                thread.Start();
                return thread;
            }
            else
            {
                throw new PerformedException(string.Format("{0} thread is running",this));
            }
        }
        public void work()
        {
            bool dIsThreadStoping()
            {
                if(stoping != 0)
                {
                    return true;
                }
                return false;
            }
            srbThreadStard_delegate(dIsThreadStoping);
            bus.removeUser(this);

        }
        public void stop()
        {
            stoping = 1;

        }
        void IBus.IbusUser.stopUseBus(IBus bus)
        {
            stop();
        }
    }
}
