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
    public partial class HexTextBox :TextBox
    {
        public HexTextBox()
        {
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if((e.KeyChar >= 'a')&&(e.KeyChar <= 'f'))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
                base.OnKeyPress(e);
            }
            else if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                base.OnKeyPress(e);
            }
            else
            {
                e.Handled = true;
            }
        }
        public byte byte_value{
            get
            {
                if (this.Text == "") return 0;
                else return Convert.ToByte(this.Text,16); 
            }
            set { 
                this.Text = value.ToHexSt(); 
            }
        }
    }
}
