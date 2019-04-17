﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB_CTR;
namespace SRB_CTR.nsBrain
{
    internal class Brain_Test:IBrain
    {
        SRB.NodeType.Du_motor.Node[] motors = new SRB.NodeType.Du_motor.Node[4];
        Random rnd = new Random();
        public Brain_Test(SrbFrame f):base(f)
        {
            period_in_ms = 1;
        }


        protected override void onRun()
        {
            for (int i = 0; i < 4; i++)
            {
                if (motors[i] == null)
                {
                    motors[i] = frame.Nodes[i + 2] as SRB.NodeType.Du_motor.Node;
                }
                if (motors[i] == null)
                {
                    motors[i] = new SRB.NodeType.Du_motor.Node((byte)(i + 2), frame);
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

        int[] from = new int[8];
        int[] to = new int[8];
        public void calculate3()
        {
            motors[2].Speed_a = 0;
            motors[2].Speed_b = -500;
            motors[2].addAccess(0);
        }
        override public void calculate()
        {
            long phase = (long)(loop_num * period_in_ms) % 4000;
            if (phase == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    from[i] = to[i];
                    to[i] = rnd.Next(-1000, 1000);
                }
            }
            if (phase < 3000)
            {
                for (int i = 0; i < 8;)
                {
                    int motor_num = i / 2;
                    motors[motor_num].Speed_a = (int)(from[i] + ((to[i] - from[i]) * phase / 3000.0));
                    i++;
                    motors[motor_num].Speed_b = (int)(from[i] + ((to[i] - from[i]) * phase / 3000.0));
                    i++;
                    try
                    {
                        motors[motor_num].addAccess(0);
                    }
                    catch { }
                }
            }
            else
            {
                for (int i = 0; i < 8;)
                {
                    int motor_num = i / 2;
                    motors[motor_num].Speed_a = (to[i]);
                    i++;
                    motors[motor_num].Speed_b = (to[i]);
                    i++;
                    try
                    {
                        motors[motor_num].addAccess(0);
                    }
                    catch { }
                }
            }
        }


    }
}
