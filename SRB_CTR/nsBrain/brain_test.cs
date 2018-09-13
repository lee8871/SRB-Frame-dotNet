using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB_CTR.nsFrame;
namespace SRB_CTR.nsBrain
{
    class brain_test:Brain
    {
        ControPanel mainCP;
        ControPanel secondCP;
        Node_dMotor.cn motor_0;
        Node_dMotor.cn motor_1;
        Node_Test001.cn t6_node;
        public brain_test(frame f):base(f)
        {
            motor_0 = new Node_dMotor.cn(8, parent);
            motor_1 = new Node_dMotor.cn(7, parent);
            t6_node = new Node_Test001.cn(1, parent);
            mainCP = new ControPanel(this);
            secondCP = new ControPanel(this);
            secondCP.Text = "motor1";
            mainCP.Show();
            secondCP.Show();
        }


        double h0 = 0;
        double h1 = 90;
        override public void calculate()
        {
            ColorHSV rc = new ColorHSV((int)(h0 + 0.5), 255, 255);
            t6_node.color_set = ColorHelper.HsvToRgb(rc).GetColor();
            t6_node.bulidUpD0();

            rc = new ColorHSV((int)(h1 + 0.5), 255, 255);

            motor_0.speed_a = mainCP.Motor_x;
            motor_0.speed_b = mainCP.Motor_y;
            motor_0.bulidUpD0();
            motor_1.speed_a = secondCP.Motor_x;
            motor_1.speed_b = secondCP.Motor_y;
            motor_1.bulidUpD0();

            h0 += 0.5;
            if (h0 > 360)
            {
                h0 -= 360;
            }
            h1 += 1;
            if (h1 > 360)
            {
                h1 -= 360;
            }
        }


    }
}
