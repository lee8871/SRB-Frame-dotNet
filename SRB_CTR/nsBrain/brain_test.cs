using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB_CTR.SRB_Frame;
namespace SRB_CTR.nsBrain
{
    internal class Brain_Test:IBrain
    {
        Node_dMotor.Cn[] motors = new Node_dMotor.Cn[4];
        public Brain_Test(SrbFrame f):base(f)
        {
            period_in_ms = 10;
        }


        protected override void onRun()
        {
            for (int i = 0; i < 4; i++)
            {
                if (motors[i] == null)
                {
                    motors[i] = frame.Nodes[i + 2] as Node_dMotor.Cn;
                }
                if (motors[i] == null)
                {
                    motors[i] = new Node_dMotor.Cn((byte)(i + 2), frame);
                }
            }
            base.onRun();
        }
        protected override void onStop()
        {
            for (int i = 0; i < 4; i++)
            {
                motors[i] = null;
            }
            base.onStop();
        }

        override public void calculate()
        {
            int a, b;
            long phase = loop_num % 400;
            if(phase < 100)
            {
                a = b = 800;
            }
            else if (phase < 200)
            {
                a = b = 0;
            }
            else if (phase < 300)
            {
                a = b = -800;
            }
            else
            {
                a = b = 0;
            }
            for (int i = 0; i < 4; i++)
            {
                motors[i].speed_a = a;
                motors[i].speed_b = b;
                motors[i].bulidUpD0();
            }
        }


    }
}
