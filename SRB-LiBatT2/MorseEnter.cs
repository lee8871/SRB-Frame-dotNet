using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SRB.NodeType.Charger
{
    public partial class MorseEnter : UserControl
    {
        public byte Morse_code { get => morseToByte(MorseTB.Text); set => MorseTB.Text = byteToMorse(value); }
        public MorseEnter()
        {
            InitializeComponent();
            MorseTB.KeyPress += MorseTB_KeyPress;
            this.MorseCharTB.Text = "A";
        }

        private void MorseTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= ' ') && (e.KeyChar <= '~'))
            {
                if ((e.KeyChar == '.') || (e.KeyChar == '-'))
                {
                    return;
                }
                e.Handled = true;
            }
        }
        private void MorseTB_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in MorseTB.Text)
            {
                if ((c != '.') && (c != '-'))
                {
                    MorseTB.Text = MorseTB.Text.Replace(c.ToString(), "");
                }
            }
            try
            {
                MorseCharTB.Text = new string(morseToChar(MorseTB.Text), 1);
            }
            catch
            {

            }
        }
        private void MorseCharTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MorseTB.Text = charToMorse(MorseCharTB.Text.ToUpper().ToArray()[0]);
            }
            catch
            {

            }
        }

        private static Dictionary<char, string> Morse = new Dictionary<char, string>()
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

        static public char morseToChar(string morse)
        {
            foreach (char c in Morse.Keys)
            {
                if (Morse[c] == morse)
                {
                    return c;
                }
            }
            return '\0';
        }

        static public string charToMorse(char c)
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

        static public string byteToMorse(byte b)
        {
            char[] c = new char[8];
            int i = 0, counter = 0;
            for (; i < 8; i++)
            {
                if ((b & (1 << i)) != 0)
                {
                    i++;
                    break;
                }
            }
            for (; i < 8; i++)
            {
                if ((b & (1 << i)) != 0)
                {
                    c[counter] = '-'; counter++;
                }
                else
                {
                    c[counter] = '.'; counter++;
                }
            }
            return new string(c, 0, counter);
        }

        static public byte morseToByte(string ms)
        {
            char[] ticks = ms.ToArray();
            foreach (char c in ticks)
            {
                if ((c != '.') && (c != '-'))
                {
                    throw new ArgumentException("Morse string is constitute by '.'and '-'");
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
            return b;
        }


    }
}
