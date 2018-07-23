
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;


namespace SRB_CTR.nsBrain
{
    internal class brain
    {
        Node[] nodes;
        internal Node[] Nodes
        {
            get { return nodes; }
        }
        SRB_Master master;
        public bool Is_port_opend
        {
            get { return master.Is_opened(); }
        }
        NodesForm _nodes_form;
        public NodesForm Nodes_form
        {
            get{return _nodes_form;}
        }
        ControPanel mainCP;
        ControPanel secondCP;

        Node_dMotor.cn motor_0;
        Node_dMotor.cn motor_1;
        Node_Test001.cn t6_node;
        Thread calculation;
        public brain()
        {
            nodes = new Node[128];
            nodes[0] = motor_0 = new Node_dMotor.cn(0);
            nodes[1] = motor_1 = new Node_dMotor.cn(1);
            nodes[6] = t6_node = new Node_Test001.cn(6);
     //       nodes[0x13] = new Node(0x13);
            master = new SRB_Master_Uart();
            _nodes_form = new NodesForm(this);
            mainCP = new ControPanel(this);
            secondCP = new ControPanel(this);
            secondCP.Text = "motor1";
            mainCP.Show();
            secondCP.Show();
        }

        private int[] calculation_delay = new int[100];
        private int[] communication_delay = new int[100];
        private int delay_counter = 0;
        public void getDelayValue(ref double us_calculation ,ref double us_communation)
        {
            double temp;
            temp = 0;
            foreach (long l in calculation_delay)
            {
                temp += l;
            }
            us_calculation = temp * (1024.0 / 100 / 3000);
            temp = 0;
            foreach (long l in communication_delay)
            {
                temp += l;
            }
            us_communation = temp * (1024.0 / 100 / 3000);
            return;
        }


        private int cycle_in_ms;
        public int Cycle_in_ms
        {
            get { return cycle_in_ms; }
            set { cycle_in_ms = value; }
        }
        private bool is_calculation_running = false;
        public bool Is_calculation_running
        {
            get { return is_calculation_running; }
        }
        
        public bool stopCalculation()
        {
            if (is_calculation_running)
            {
                is_calculation_running = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool runCalculation()
        {
            if (is_calculation_running)
            {
                return false;
            }
            else
            {
                is_calculation_running = true;
                calculation = new Thread(new ThreadStart(run));
                calculation.Priority = ThreadPriority.Highest;
                calculation.Start();
                return true;
            }
        }


        private Access[] acs = new Access[128];
        private int acs_counter = 0;

        private void run()
        {
            long begin;
            long calculation_done;
            long communication_done;
            long next_begin;
            while (is_calculation_running == true)
            {
                begin = Stopwatch.GetTimestamp();

                calculateColor();
                calculation_done = Stopwatch.GetTimestamp();

                acs_counter = 0;
                foreach (Node n in nodes)
                {
                    if (n != null)
                    {
                        foreach (Access ac in n.bulidUp())
                        {
                            acs[acs_counter++] = ac;
                        }
                    }
                }

                master.doAccess(acs, acs_counter);

                for (int i = 0; i < acs_counter; i++)
                {
                    Access ac = acs[i];
                    if (ac.Is_received)
                    {
                        nodes[(int)(ac.Addr)].accessDone(ac);
                    }
                    else
                    {

                    }
                }

                communication_done = Stopwatch.GetTimestamp();

                calculation_delay[delay_counter] = (int)(calculation_done - begin);
                communication_delay[delay_counter] = (int)(communication_done - calculation_done);

                delay_counter++; delay_counter %= 100;
                next_begin = begin + (long)(cycle_in_ms * (3000000 / 1024.0));  

                if (delay_counter == 99)
                {
                    double temp;
                    temp = 0;
                    foreach (long l in calculation_delay)
                    {
                        temp += l;
                    }
                    _nodes_form.us_calculation = temp * (1024.0 / 100 / 3000);
                    temp = 0;
                    foreach (long l in communication_delay)
                    {
                        temp += l;
                    }
                    _nodes_form.us_communation = temp * (1024.0 / 100 / 3000);
                }
                if (_nodes_form.IsDisposed)
                {
                    return;
                }
                if (ad != null)
                {
                    ad.addAccess(acs, acs_counter);
                }
                while (Stopwatch.GetTimestamp() < next_begin) ;
            }
        }
        double h0 = 0;
        double h1 = 90;
        private void calculateColor()
        {
            ColorHSV rc = new ColorHSV((int)(h0 + 0.5), 255, 255);
            t6_node.color_set = ColorHelper.HsvToRgb(rc).GetColor();


            rc = new ColorHSV((int)(h1 + 0.5), 255, 255);

            motor_0.speed_a = mainCP.Motor_x;
            motor_0.speed_b = mainCP.Motor_y;

            motor_1.speed_a = secondCP.Motor_x;
            motor_1.speed_b = secondCP.Motor_y;

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






        public System.Windows.Forms.Form getuartConfigForm()
        {
            return master.getConfigForm();
        }
        AccessDisplayer ad;
        private int motor0_x;

        public int Motor0_x
        {
            get { return motor0_x; }
            set { motor0_x = value; }
        }
        private int motor0_y;

        public int Motor0_y
        {
            get { return motor0_y; }
            set { motor0_y = value; }
        }
        public System.Windows.Forms.Form getAccessDisplayer()
        {
            if (ad == null)
            {
                ad = new AccessDisplayer(this);
            }
            return ad;
        }
        public void closeAccessDisplayer()
        {
            ad = null;
        }
    }
}
