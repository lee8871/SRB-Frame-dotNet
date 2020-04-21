using SRB.Frame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace SRB.NodeType.PhotoElecX6
{
    internal partial class Ctrl : INodeControl
    {
        private Interpreter datas;
        BaseNode node;
        public Ctrl(BaseNode n) :
            base(n)
        {
            datas = (Interpreter)n.Datas;
            InitializeComponent();
            n.eBankChangeByAccess += N_eBankChangeByAccess;
            node = n;
        }

        Queue<int> randomNum = new Queue<int>();
        private void N_eBankChangeByAccess(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                randomNum.Enqueue(datas.value(i));
            }
            countBTN.Text = randomNum.Count.ToString(); 
        }
        private void countBTN_Click(object sender, EventArgs e)
        {
            string rev = "";
            rev += string.Format("{0} random num is received\n", randomNum.Count);


            int[] TogNum = new int[16];
            for (int i = 0; i < 16; i++)
            {
                TogNum[i] = 0;
            }
            foreach (int rn in randomNum)
            {
                int a = getTog(rn);
                TogNum[a]++;
            }
            rev += string.Format("\nNum tog\n", randomNum.Count);
            for (int i = 0; i < 16; i++)
            {
                rev += string.Format("{0}\t{1}\n", i, TogNum[i]);
            }


            int[] count = new int[1024];
            for(int i = 0; i < 1024; i++)
            {
                count[i] = 0;
            }
            foreach(int rn in randomNum)
            {
                int a = rn % 1024;
                count[a]++;
            }
            rev += string.Format("\nNum Counter\n", randomNum.Count);
            for (int i = 0; i < 1024; i++)
            {
                rev += string.Format("{0}\t{1}\n", i, count[i]);              
            }




            System.IO.File.WriteAllText(@"F:\SRB-Random\" + node.Name + DateTime.Now.ToString("MM月dd HH时mm分ss秒") + ".txt", rev); ;
        }

        private int getTog(int rn)
        {
            bool[] b = new bool[16];
            for(int i = 0; i < 16; i++)
            {
                b[i] = (0 != (rn & (1 << i))); 
            }
            int tog_num=0;
            for (int i = 0; i < 15; i++)
            {
                if (b[i] != b[i + 1])
                {
                    tog_num++;
                }
            }
            return tog_num;
        }

        private int x;
        private int y;
        protected override void OnAccess()
        {
            base.OnAccess();
        }

        private void RAWBTN_Click(object sender, EventArgs e)
        {
            string rev = "";
            rev += string.Format("{0} get {1} random numbers \n", node.Name , randomNum.Count);

            foreach (int rn in randomNum)
            {
                rev += string.Format("{0}\n", rn);
            }

            System.IO.File.WriteAllText(@"F:\SRB-Random\" + node.Name + "RAW"+DateTime.Now.ToString("MM月dd HH时mm分ss秒") + ".csv", rev); ;

        }
    }

}
