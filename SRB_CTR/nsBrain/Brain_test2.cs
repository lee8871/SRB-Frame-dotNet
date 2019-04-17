﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB.Frame;
using SRB_CTR;
namespace SRB_CTR.nsBrain
{
    internal class Brain_Test2:IBrain
    {
        SRB.NodeType.Du_motor.Node left;
        SRB.NodeType.Du_motor.Node right;
        SRB.NodeType.PS2_Handle.Node handle;
        SRB.NodeType.Charger.Node charger;
        Random rnd = new Random();
        public Brain_Test2(SrbFrame f):base(f)
        {
            period_in_ms = 1;
        }


        protected override void onRun()
        {
            foreach(BaseNode n in frame.Nodes)
            {
                if(n!=null)
                {
                    if (n is SRB.NodeType.Du_motor.Node)
                    {
                        if (n.Name == "left")
                        {
                            left = n as SRB.NodeType.Du_motor.Node;
                        }
                        if (n.Name == "right")
                        {
                            right = n as SRB.NodeType.Du_motor.Node;
                        }
                    }
                    else if(n is SRB.NodeType.PS2_Handle.Node)
                    {
                        if (n.Name == "brain test")
                        {
                            handle = n as SRB.NodeType.PS2_Handle.Node;
                        }
                    }
                    else if (n is SRB.NodeType.Charger.Node)
                    {
                        charger = n as SRB.NodeType.Charger.Node;
                    }

                    try
                    {
                        handle.singleAccess(0);
                        last_up = handle.up;
                        last_left = handle.left;
                        last_right = handle.right;
                        last_down = handle.down;
                    }
                    catch { }
                }
            }
            base.onRun();
        }
        protected override void onStop()
        {
            base.onStop();
        }
        bool last_up;
        bool last_left;
        bool last_right;
        bool last_down;
        override public void calculate()
        {
            if (handle.up != last_up)
            {
                last_up = handle.up;
                if(last_up == true)
                {
                    charger.buzzer_commend = 0x40;//0100 0000
                }
                try
                {
                    charger.addAccess(1);
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
                    charger.addAccess(1);
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
                    charger.addAccess(1);
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
                    charger.addAccess(1);
                }
                catch { }
                charger.buzzer_commend = 0x80;

            }


            if (handle.trag == true)
            {
                left.Brake_a = 0xffff;
            }
            else
            {
                left.Speed_a = handle.joy_lx + handle.joy_ly;
            }
            if (handle.circle == true)
            {
                left.Brake_b = 0xffff;
            }
            else
            {
                left.Speed_b = handle.joy_lx - handle.joy_ly;
            }

            if (handle.cross == true)
            {
                right.Brake_b = 0xffff;
            }
            else
            {
                right.Speed_b = handle.joy_rx + handle.joy_ry;
            }
            if (handle.square == true)
            {
                right.Brake_a = 0xffff;
            }
            else
            {
                right.Speed_a = handle.joy_rx - handle.joy_ry;
            }


            try
            {
                handle.addAccess(1);
                left.addAccess(1);
                right.addAccess(1);
            }
            catch { }
        }
    }
}
