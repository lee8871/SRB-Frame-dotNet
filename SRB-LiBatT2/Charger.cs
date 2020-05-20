using SRB.Frame;
using System;

namespace SRB.NodeType.Charger
{
    public class Interpreter : BaseNode.INodeInterpreter
    {
        public byte buzzer_now => bank.getBankByte(0);

        public bool is_charge_open => bank.getBankBool(1, 0);
        public bool is_charging => bank.getBankBool(1, 1);
        public bool is_charge_done => bank.getBankBool(1, 2);
        public bool is_jack_in => bank.getBankBool(1, 3);

        public int battery_voltage => (int)bank.getBankUshort(2);
        public ushort battery_ADC => bank.getBankUshort(4);
        public int charge_second => (int)bank.getBankUshort(6);
        public int capacity => (short)bank.getBankUshort(8);

        public byte buzzer_commend { set => bank.setBankByte(value, 10); }

        public bool cmd_charge_enable
        {
            get => bank.getBankBool(11, 0);
            set => bank.setBankBool(value, 11, 0);
        }
        public bool is_Mute
        {
            get => bank.getBankBool(11, 1);
            set => bank.setBankBool(value, 11, 1);
        }
        public bool is_PowerLEDRun
        {
            get => bank.getBankBool(11, 2);
            set => bank.setBankBool(value, 11, 2);
        }
        public string getStatues()
        {
            if (is_charging)
            {
                if ((!is_charge_done) && (is_jack_in) && (is_charge_open))
                {
                    return "Charging";
                }
                else
                {
                    return "Error Status";
                }
            }
            else
            {
                if (is_charge_done)
                {
                    if ((is_jack_in) && (is_charge_open))
                    {
                        return "Charge Done";
                    }
                    else
                    {
                        return "Error Status";
                    }
                }
                else
                {
                    if (!(is_charge_open))
                    {
                        if ((is_jack_in))
                        {
                            return "Charge is Closed";
                        }
                        else
                        {
                            return "Discharging";
                        }
                    }
                    else
                    {
                        if ((is_jack_in))
                        {
                            return "Low Power Support";
                        }
                        else
                        {
                            return "Discharging";
                        }
                    }

                }

            }
        }


        public override string Help_net_work =>
            "https://github.com/lee8871/SRB-Introduction/blob/master/SRB%E4%B8%A4%E8%8A%82%E9%94%82%E7%94%B5%E6%B1%A0%E5%85%85%E7%94%B5%E8%8A%82%E7%82%B9.md";

        public void play(string s)
        {
            buzzer_commend = MorseEnter.morseToByte(s);
        }

        public void play(char c)
        {
            play(MorseEnter.charToMorse(c));
        }

        public void play(byte bc)
        {
            buzzer_commend = bc;
        }

        internal BatteryCluster cfg_clu;
        internal MappingCluster Mapping0_clu;
        internal MorseCluster morse_clu;
        internal InnResCluster inn_res_clu;
        public void init()
        {
            cfg_clu = new BatteryCluster(Node);

            morse_clu = new MorseCluster(Node);

            inn_res_clu = new InnResCluster(Node);

            Mapping0_clu = new MappingCluster(3, Node, "Mapping0");

            Mapping0_clu.eDataChanged += updataMapping;
            Mapping0_clu.read();
            this.cmd_charge_enable = true;
            this.buzzer_commend = 0x80;
            this.is_Mute = true;
            this.is_PowerLEDRun = true;
        }
        private void updataMapping(object sender, EventArgs e)
        {
            Node.bankInit(new byte[][]{
                Mapping0_clu.mapping                  ,
                new byte[]  {10,2, 0,1, 2,3, 4,5, 6,7, 8,9,10,11}       ,
                new byte[]  {1,0,11},
                new byte[]  {0,0}
            });
        }

        public Interpreter(BaseNode n)
            : base(n)
        {
            init();
        }
        protected override System.Windows.Forms.Control createControl()
        {
            return new ChangerControl(this.Node);
        }
        public override string Describe => @"";
    }
}
