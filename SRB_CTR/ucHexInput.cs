using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR
{
    public partial class ucHexInput : UserControl 
    {
        public ucHexInput()
        {
            InitializeComponent();
            mainRT.KeyPress+=new KeyPressEventHandler(mainRT_KeyPress);
        
        }

        void mainRT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            List<char> available = 
                new List<char>(new char[]{'1','2','3','4','5','6','7','8','9','0','A','B','C','D','E','F',' '});
            if (!(available.Exists(o => { return o == e.KeyChar; })))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar != ' ')
            {
                int privious_1 = (mainRT.SelectionStart - 1);
                int privious_2 = (mainRT.SelectionStart - 2);
                if (privious_2 < 0)
                {
                    return;
                }
                if( (' ' != mainRT.Text.ToCharArray()[privious_1])  && 
                    (' ' != mainRT.Text.ToCharArray()[privious_2])  )
                {
                    mainRT.SelectedText = " ";
                }
            }
        }
        public byte[] getBytes()
        {
            string st = mainRT.Text;
            string[] bytes_st = st.Split(new char[] { ' ' });
            List<byte> bytes_list = new List<byte>();
            foreach(string byte_st in bytes_st)
            {
                if (byte_st.Length >= 1)
                {
                    bytes_list.Add((byte)Convert.ToInt32(byte_st, 16));
                }
            }
            return bytes_list.ToArray();
        }
    }
}
