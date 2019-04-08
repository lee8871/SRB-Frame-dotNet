using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SRB.Frame;
using SRB.Frame.Cluster;

namespace SRB.NodeType.Charger
{
    public class Node : BaseNode
    {
        public byte buzzer_now { get => getBankByte(0); }

        public bool is_charge_open { get => getBankBool(1, 0); }
        public bool is_charging { get => getBankBool(1, 1); }
        public bool is_charge_done { get => getBankBool(1, 2); }
        public bool is_jack_in { get => getBankBool(1, 3); }

        public int battery_voltage { get => (int)getBankUshort(2); }
        public ushort battery_ADC { get => getBankUshort(4); }
        public int charge_second { get => (int)getBankUshort(6); }
        public int capacity { get => (short)getBankUshort(8); }

        public byte buzzer_commend { set => setBankByte(value, 10); }

        public bool cmd_charge_enable {
            get => getBankBool(11,0);
            set => setBankBool(value, 11,0);
        }
        public bool is_Mute {
            get => getBankBool(11,1);
            set => setBankBool(value, 11, 1);
        }
        public bool is_PowerLEDRun {
            get => getBankBool(11, 2);
            set => setBankBool(value, 11, 2);
        }
        public string getStatues()
        {
            if(is_charging)
            {
                if((!is_charge_done)&&(is_jack_in)&&(is_charge_open))
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
                if(is_charge_done)
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
            cfg_clu = new BatteryCluster(this);
            clusters[cfg_clu.Clustr_ID] = cfg_clu;

            morse_clu = new MorseCluster(this);
            clusters[morse_clu.Clustr_ID] = morse_clu;

            inn_res_clu = new InnResCluster(this);
            clusters[inn_res_clu.Clustr_ID] = inn_res_clu;

            Mapping0_clu = new MappingCluster(3, this,"Mapping0");
            clusters[Mapping0_clu.Clustr_ID] = Mapping0_clu;

            Mapping0_clu.eDataChanged += updataMapping;
            Mapping0_clu.read();
            this.cmd_charge_enable = true;
            this.buzzer_commend = 0x80;
            this.is_Mute = true;
            this.is_PowerLEDRun = true;
        }
        private void updataMapping(object sender, EventArgs e)
        {
            bankInit(new byte[][]{
                Mapping0_clu.mapping                  ,
                new byte[]  {10,2, 0,1, 2,3, 4,5, 6,7, 8,9,10,11}       ,
                new byte[]  {1,0,11},
                new byte[]  {0,0}
            });
        }

        public Node(byte addr, ISRB_Master f = null)
            : base(addr, f)
        {
            init();
        }        
        public Node(BaseNode n)
            : base(n)
        {
            init();
        }
        public override System.Windows.Forms.Control getClusterControl()
        {
            return new ChangerControl(this);
        }
        public override string Describe()
        {
            return @"";
        }
    }
}
