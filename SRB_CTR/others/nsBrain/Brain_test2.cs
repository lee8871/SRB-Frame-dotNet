using SRB.Frame;
namespace SRB_CTR.nsBrain
{
    internal class Brain_Test2 : IBrain
    {
        public Brain_Test2(SrbOnelineMaster f) : base(f)
        {
            period_in_ms = 2;
        }

        private SRB.NodeType.Du_motor.Interpreter left;
        private SRB.NodeType.Du_motor.Interpreter right;
        private SRB.NodeType.Du_motor.Interpreter key_control;
        private SRB.NodeType.Du_motor.Interpreter key_control2;
        private SRB.NodeType.Joystick.Interpreter handle;
        private SRB.NodeType.Charger.Interpreter charger;
        protected override void nodesBuildUp()
        {
            foreach (BaseNode n in frame.Bus)
            {
                if (n != null)
                {
                    if (n.Datas is SRB.NodeType.Du_motor.Interpreter)
                    {
                        if (n.Name == "Left")
                        {
                            left = n.Datas as SRB.NodeType.Du_motor.Interpreter;
                        }
                        if (n.Name == "Right")
                        {
                            right = n.Datas as SRB.NodeType.Du_motor.Interpreter;
                        }
                        if (n.Name == "Key1")
                        {
                            key_control = n.Datas as SRB.NodeType.Du_motor.Interpreter;
                        }
                        if (n.Name == "Key2")
                        {
                            key_control2 = n.Datas as SRB.NodeType.Du_motor.Interpreter;
                        }
                    }
                    else if (n.Datas is SRB.NodeType.Joystick.Interpreter)
                    {
                        if (n.Name == "Handle")
                        {
                            handle = n.Datas as SRB.NodeType.Joystick.Interpreter;
                        }
                    }
                    else if (n.Datas is SRB.NodeType.Charger.Interpreter)
                    {
                        charger = n.Datas as SRB.NodeType.Charger.Interpreter;
                    }
                }
            }
        }

        private bool last_up;
        private bool last_left;
        private bool last_right;
        private bool last_down;
        protected override void setup()
        {
            last_up = false;
            last_left = false;
            last_right = false;
            last_down = false;
            try
            {
                handle.addDataAccess(3);
            }
            catch { }
        }
        protected override void loop()
        {
            try
            {
                //about buzzer beep;
                if (handle.up != last_up)
                {
                    last_up = handle.up;
                    if (last_up == true)
                    {
                        charger.buzzer_commend = 0x40;//0100 0000
                    }
                    try
                    {
                        charger.addDataAccess(1);
                    }
                    catch { }
                    charger.buzzer_commend = 0x80;
                }
                else if (handle.left != last_left)
                {
                    last_left = handle.left;
                    if (last_left == true)
                    {
                        charger.buzzer_commend = 0x20;//0100 0000
                    }
                    try
                    {
                        charger.addDataAccess(1);
                    }
                    catch { }
                    charger.buzzer_commend = 0x80;

                }
                else if (handle.right != last_right)
                {
                    last_right = handle.right;
                    if (last_right == true)
                    {
                        charger.buzzer_commend = 0x10;//0100 0000
                    }
                    try
                    {
                        charger.addDataAccess(1);
                    }
                    catch { }
                    charger.buzzer_commend = 0x80;

                }
                else if (handle.down != last_down)
                {
                    last_down = handle.down;
                    if (last_down == true)
                    {
                        charger.buzzer_commend = 0x08;//0100 0000
                    }
                    try
                    {
                        charger.addDataAccess(1);
                    }
                    catch { }
                    charger.buzzer_commend = 0x80;
                }
            }
            catch { }

            try
            {//left and right motor
                if (handle.trag == true)
                {
                    left.Brake_a = 0x4fff;
                }
                else
                {
                    left.Speed_a = handle.joy_lx + handle.joy_ly;
                }
                if (handle.circle == true)
                {
                    left.Brake_b = 0x4fff;
                }
                else
                {
                    left.Speed_b = handle.joy_lx - handle.joy_ly;
                }
                left.addDataAccess(1);
            }
            catch { }

            try
            {
                if (handle.cross == true)
                {
                    right.Brake_b = 0x4fff;
                }
                else
                {
                    right.Speed_b = handle.joy_rx + handle.joy_ry;
                }
                if (handle.square == true)
                {
                    right.Brake_a = 0x4fff;
                }
                else
                {
                    right.Speed_a = handle.joy_rx - handle.joy_ry;
                }
                right.addDataAccess(1);
            }
            catch { }

            try
            {
                key_control.Speed_a = handle.pressure_l1 - handle.pressure_l2;
                key_control.Speed_b = handle.pressure_r1 - handle.pressure_r2;
                key_control.addDataAccess(1);
            }
            catch { }

            try
            {
                key_control2.Speed_a = handle.pressure_trag - handle.pressure_cros;
                key_control2.Speed_b = handle.pressure_squ - handle.pressure_cir;
                key_control2.addDataAccess(1);
            }
            catch { }

            try
            {
                handle.addDataAccess(3);
            }
            catch { }
        }

        protected override void termination()
        {
            try
            {//left and right motor
                left.Speed_a = 0;
                left.Speed_b = 0;
                left.addDataAccess(1);
            }
            catch { }

            try
            {
                right.Speed_b = 0;
                right.Speed_a = 0;
                right.addDataAccess(1);
            }
            catch { }

            try
            {
                key_control.Speed_a = 0;
                key_control.Speed_b = 0;
                key_control.addDataAccess(1);
            }
            catch { }

            try
            {
                key_control2.Speed_a = 0;
                key_control2.Speed_b = 0;
                key_control2.addDataAccess(1);
            }
            catch { }
        }

    }
}
