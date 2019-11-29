using System;
namespace SRB_CTR.nsBrain
{
    internal class Brain_Test : IBrain
    {
        private SRB.NodeType.Du_motor.Node[] motors = new SRB.NodeType.Du_motor.Node[4];
        private Random rnd = new Random();
        public Brain_Test(SRB_oneline_master f) : base(f)
        {
            period_in_ms = 1;
        }


        protected override void nodesBuildUp()
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

        }
        protected override void setup()
        {

        }

        private int[] from = new int[8];
        private int[] to = new int[8];
        protected override void loop()
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
        protected override void termination()
        {

        }


    }
}
