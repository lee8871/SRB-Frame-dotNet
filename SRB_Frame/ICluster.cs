using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB.Frame
{
    public abstract class ICluster : IByteBank
    {
        protected byte clustr_ID;

        protected BaseNode parent_node;

        public BaseNode Parent_node { get => parent_node; }
        public byte Clustr_ID { get => clustr_ID; }


        public ICluster(BaseNode n, byte ID, int banksize):base(banksize,true)
        {
            this.clustr_ID = ID;
            this.parent_node = n;
        }
        public void writeBankinit()
        {
            bank_write_temp = new byte[bank.Length];
            for (int i = 0; i < bank.Length; i++)
            {
                bank_write_temp[i] = bank[i];
            }
        }


        public virtual void write()
        {
            byte[] data = new byte[bank_write_temp.Length + 1];
            data[0] = Clustr_ID;
            for (int i = 0; i < bank_write_temp.Length; i++)
            {
                data[i + 1] = bank_write_temp[i];
            }
            Access ac = new Access(parent_node, Access.PortEnum.Cgf , data);
            parent_node.singleAccess(ac);

        }
        public virtual void writeRecv(Access ac)
        {
            if (ac.Recv_error == false)
            {
                for (int i = 0; i < bank_write_temp.Length; i++)
                {
                    bank[i] = bank_write_temp[i];
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

        public void changeParentNode(BaseNode n)
        {
            parent_node = n;
        }


        public event EventHandler eDataChanged;

        public virtual void OnDataChangded()
        {
            if(eDataChanged!=null)
            {
                eDataChanged.Invoke(this, new EventArgs());
            }
        }




        public abstract UserControl createControl();
        public abstract override string ToString();
    }
}
