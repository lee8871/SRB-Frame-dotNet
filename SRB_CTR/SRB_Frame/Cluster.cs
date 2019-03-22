using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.SRB_Frame
{
    public abstract class Cluster
    {
        protected byte clustr_ID;
        protected byte[] bank;
        protected byte[] bank_write;

        protected Node parent_node;

        public Node Parent_node { get => parent_node; }
        public byte Clustr_ID { get => clustr_ID; }


        public Cluster(byte ID, Node n, int banksize = -1)
        {
            this.clustr_ID = ID;
            this.parent_node = n;
            if(banksize !=-1)
            {
                bank = new byte[banksize];
            }
        }
        public void writeBankinit()
        {
            bank_write = new byte[bank.Length];
            for (int i = 0; i < bank.Length; i++)
            {
                bank_write[i] = bank[i];
            }
        }

        public virtual void write()
        {
            byte[] data = new byte[bank_write.Length + 1];
            data[0] = Clustr_ID;
            for (int i = 0; i < bank_write.Length; i++)
            {
                data[i + 1] = bank_write[i];
            }
            Access ac = new Access(parent_node, Access.PortEnum.Cgf , data);
            parent_node.singleAccess(ac);

        }
        public virtual void writeRecv(Access ac)
        {
            if (ac.Recv_error == false)
            {
                for (int i = 0; i < bank_write.Length; i++)
                {
                    bank[i] = bank_write[i];
                }
                OnDataChangded();
            }
        }

        public void read()
        {
            Access ac = new Access(parent_node, Access.PortEnum.Cgf, new byte[] { Clustr_ID });
            parent_node.singleAccess(ac);
        }
        public virtual void readRecv(Access ac)
        {

            //todo:
            //check datalen
            if (ac.Recv_data_len != 0)
            {
                bank = new byte[ac.Recv_data.Length];
                for (int i = 0; i < bank.Length; i++)
                {
                    bank[i] = ac.Recv_data[i];
                }
            }
            OnDataChangded();
        }

        public void changeParentNode(Node n)
        {
            parent_node = n;
        }


        public abstract UserControl createControl();
        public event EventHandler eDataChanged;

        public void OnDataChangded()
        {
            if(eDataChanged!=null)
            {
                eDataChanged.Invoke(this, new EventArgs());
            }
        }





        //bank controls
        protected string getBankString(int diff, int max_len = 31)
        {
            char[] cs = new char[max_len];
            int i;
            for (i = 0; i < max_len; i++)
            {
                if (bank[diff + i] == 0)
                {
                    break;
                }
                else
                {
                    cs[i] = (char)bank[diff + i];
                }
            }
            string rev = new string(cs, 0, i);
            return rev;
        }
        protected ushort getBankUshort(int diff)
        {
            ushort rev = 0;
            rev += bank[diff + 1];
            rev <<= 8;
            rev += bank[diff]; ;
            return rev;
        }

        protected bool getBankBool(int diff,int bit_diff=0)
        {
            return ((bank[diff]&(1<<bit_diff)) != 0);
        }

        protected void setBankUshort(ushort val, int diff)
        {
            bank_write[diff] = (byte)val;
            val >>= 8;
            bank_write[diff + 1] = (byte)val;
            return;
        }

        protected void setBankString(string str, int diff, int max_len = 31)
        {
            char[] ca = str.ToCharArray();
            if (ca.Length >= max_len)//there should a \0 in the end. So ca len shold small than max 
            {
                throw new Exception("string too long!");
            }
            //if (ca[ca.Length - 1] != '\0')
            //{
            //    throw new Exception("transform char array do not has\0 at they end.");

            //}
            int i;
            for (i = 0; i < ca.Length; i++)
            {
                bank_write[diff + i] = (byte)ca[i];
            }
            bank_write[diff + i] = (byte)'\0';
            return;
        }
        protected void setBankBool(bool b, int diff, int bit_diff = 0)
        {
            if(b)
            {
                bank_write[diff] |= (byte)( 1ul << bit_diff);
            }
            else
            {
                bank_write[diff] &= ((byte)(~(1ul << bit_diff)));
            }        
        }


        protected byte getBankByte(int diff)
        {
            return bank[diff];
        }
        protected void setBankByte(byte val, int diff)
        {
            bank_write[diff] = val;
            return;
        }



        protected byte[] getBankByteArray(int diff,int len)
        {
            byte[] ba = new byte[len];
            for (int i = 0; i < len; i++)
            {
                ba[i] = bank[diff + i];
            }
            return ba;
        }
        protected void setBankByteArray(byte[] ba, int diff, int len= -1)
        {
            if (len == -1)
            {
                len = ba.Length;
            }
            for (int i = 0; i < len; i++)
            {
                bank_write[diff + i] = ba[i];
            }
            return;
        }
    }
}
