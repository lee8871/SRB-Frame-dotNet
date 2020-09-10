using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame
{
    class ByteBankMember
    {
        private ByteBank bank;
        private int bit_diff;
        private int bit_len;
        public ByteBankMember(int bit_len, ByteBankMember last)
        {
            this.bank = last.bank;
            this.bit_diff = last.bit_diff + last.bit_len;
            this.bit_len = bit_len;
        }
        public ByteBankMember(int bit_len, ByteBank bank, int byte_diff=0)
        {
            this.bank = bank;
            this.bit_diff = 8 * byte_diff;
            this.bit_len = bit_len;
        }

        public static implicit operator byte(ByteBankMember mb)
        {
            return mb.bank.getBankByte( mb.bit_diff / 8);
        }



    };


};
