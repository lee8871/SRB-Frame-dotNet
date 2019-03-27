using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SRB.Frame;

namespace SRB.NodeType.Charger
{
    public class Node : BaseNode
    {
        public byte buzzer_now { get => getBankByte(0); }

        public bool is_charge_open { get => getBankBool(1, 0); }
        public bool is_charging { get => getBankBool(1, 1); }
        public bool is_charge_done { get => getBankBool(1, 2); }
        public bool is_jack_in { get => getBankBool(1, 3); }

        public int battery_voteage { get => (int)getBankUshort(2); }
        public ushort battery_ADC { get => getBankUshort(4); }
        public int charge_second { get => (int)getBankUshort(6); }

        public byte buzzer_commend { set => setBankByte(value, 8); }

        public bool cmd_charge_enable {
            get => getBankBool(9,0);
            set => setBankBool(value, 9,0);
        }
        public bool is_Mute {
            get => getBankBool(9,1);
            set => setBankBool(value, 9, 1);
        }
        public bool is_PowerLEDRun {
            get => getBankBool(9, 2);
            set => setBankBool(value, 9, 2);
        }

        internal ConfigCluster cfg_clu;
        internal MappingCluster Mapping0_clu;
        Dictionary<char, string> Morse = new Dictionary<char, string>()
        {
            {'A' , ".-"},
            {'B' , "-..."},
            {'C' , "-.-."},
            {'D' , "-.."},
            {'E' , "."},
            {'F' , "..-."},
            {'G' , "--."},
            {'H' , "...."},
            {'I' , ".."},
            {'J' , ".---"},
            {'K' , "-.-"},
            {'L' , ".-.."},
            {'M' , "--"},
            {'N' , "-."},
            {'O' , "---"},
            {'P' , ".--."},
            {'Q' , "--.-"},
            {'R' , ".-."},
            {'S' , "..."},
            {'T' , "-"},
            {'U' , "..-"},
            {'V' , "...-"},
            {'W' , ".--"},
            {'X' , "-..-"},
            {'Y' , "-.--"},
            {'Z' , "--.."},
            {'1' , ".----"},
            {'2' , "..---"},
            {'3' , "...--"},
            {'4' , "....-"},
            {'5' , "....."},
            {'6' , "-...."},
            {'7' , "--..."},
            {'8' , "---.."},
            {'9' , "----."},
            {'0' , "-----"},
            {'.' , ".-.-.-"},
            {':' , "---..."},
            {',' , "--..--"},
            {';' , "-.-.-."},
            {'?' , "..--.."},
            {'=' , "-...-"},
            {'\'' , ".----."},
            {'/' , "-..-."},
            {'!' , "-.-.--"},
            {'-' , "-....-"},
            {'_' , "..--.-"},
            {'(' , "-.--."},
            {')' , "-.--.-"},
            {'$' , "...-..-"},
            {'&' , ".-..."},
            {'@' , ".--.-."},
            {'+' , ".-.-."},
            {'"' , ".-..-."},

        };


        public char morseToChar(string morse)
        {
            foreach(char c in Morse.Keys)
            {
                if (Morse[c] == morse)
                {
                    return c;
                }
            }
            return '\0';
        }

        public string charToMorse(char c)
        { 
            try
            {
                return Morse[c];
            }
            catch
            {
                return "";
            }
        }

        public void play(string s)
        {
            char[] ticks = s.ToArray();

            foreach (char c in ticks)
            {
                if ((c != '.') &&( c != '-'))
                {
                    throw new Exception("Morse string is constitute by '.'and '-'");
                }
            }
            int i = 8 - 1 - ticks.Length;
            byte b = 0;
            b |= (byte)(1 << i);
            i++;
            foreach (char c in ticks)
            {
                if (c == '-')
                {
                    b |= (byte)(1 << i);
                }
                i++;
            }
            buzzer_commend = b;
        }

        public void play(char c)
        {
            play(Morse[c]);
        }


        public void init()
        {
            cfg_clu = new ConfigCluster(11, this);
            clusters[cfg_clu.Clustr_ID] = cfg_clu;

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
                new byte[] {8,2, 0,1, 2,3, 4,5, 6,7,  8,9}        ,
                new byte[] {1,0,9}  ,
                new byte[] {8,2, 0,1, 2,3, 4,5, 6,7,  8,9}
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
