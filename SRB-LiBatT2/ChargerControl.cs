using SRB.Frame;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SRB.NodeType.Charger
{
    internal partial class ChangerControl : INodeControl
    {
        private Interpreter datas;

        public ChangerControl(Node n) :
            base(n)
        {
            datas = (Interpreter)n.Datas;
            InitializeComponent();
            MorseTB.KeyPress += MorseTB_KeyPress;
            n.eBankChangeByAccess += Node_eBankChangeByAccess;
            n.eDataAccessRecv += Node_eDataAccessRecv;
            datas.addDataAccess(2,false, 0);
            this.ToolTips.SetToolTip(PlayBTN, "Click to play morse.");
            this.ToolTips.SetToolTip(BatteryPowerLedBTN, "Click to Toggle Voltage LED On PCB.");
            this.ToolTips.SetToolTip(MuteBTN, "Click to Enable or disable buzzer alram.");
            this.ToolTips.SetToolTip(ChangeEnableBTN, "Click to enable or disable charge.");
        }

        private void Node_eDataAccessRecv(object sender, AccessEventArgs e)
        {
            this.datas.buzzer_commend = 0x80;
        }

        private void Node_eBankChangeByAccess(object sender, EventArgs e)
        {
            this.BatteryValueLAB.Text = (((double)datas.battery_voltage) / 1000.0).ToString("0.000") + "V";
            this.ChangeVottageBar.Value = datas.battery_voltage.enterRound(6000, 8400); ;
            this.ChargeTimerLAB.Text = datas.charge_second.ToString() + "S";
            this.CapacityLAB.Text = ((datas.capacity * 100.0) / 1024).ToString("0.0") + "%";
            this.statusLAB.Text = datas.getStatues();
            if (datas.cmd_charge_enable)
            {
                this.ChangeEnableBTN.BackgroundImage = global::SRB.NodeType.LiBatT2.Properties.Resources._1175709;
            }
            else
            {
                this.ChangeEnableBTN.BackgroundImage = global::SRB.NodeType.LiBatT2.Properties.Resources._1175309;
            }
            if (datas.is_Mute)
            {
                this.MuteBTN.BackgroundImage = global::SRB.NodeType.LiBatT2.Properties.Resources._1175310;
            }
            else
            {
                this.MuteBTN.BackgroundImage = global::SRB.NodeType.LiBatT2.Properties.Resources._1175710;
            }
            if (datas.is_PowerLEDRun)
            {
                this.BatteryPowerLedBTN.BackgroundImage = global::SRB.NodeType.LiBatT2.Properties.Resources._1175695;
            }
            else
            {
                this.BatteryPowerLedBTN.BackgroundImage = global::SRB.NodeType.LiBatT2.Properties.Resources._1175295;
            }
        }


        private void MorseTB_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Decimal) && (!e.Shift)) return;
            if ((e.KeyData == Keys.Subtract) && (!e.Shift)) return;
            if (e.KeyData == Keys.Back) return;
            if (e.KeyData == Keys.Delete) return;
            if (e.Control) return;
            if (e.Alt) return;
            e.Handled = true;
            return;
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
                MorseCharTB.Text = new string(MorseEnter.morseToChar(MorseTB.Text), 1);
            }
            catch
            {

            }
        }
        private void MorseCharTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MorseTB.Text = MorseEnter.charToMorse(MorseCharTB.Text.ToUpper().ToArray()[0]);
            }
            catch
            {

            }
        }

        private void PlayBTN_Click(object sender, EventArgs e)
        {
            datas.play(MorseTB.Text);
            if (is_running == false)
            {
                datas.addDataAccess(1);
            }

        }


        private void sendTimer_Tick(object sender, EventArgs e)
        {
            datas.addDataAccess(1);
        }

        private void BatteryPowerLedBTN_Click(object sender, EventArgs e)
        {
            datas.is_PowerLEDRun = !datas.is_PowerLEDRun;
            if (is_running == false)
            {
                datas.addDataAccess(1);
            }
        }

        private void ChangeEnableBTN_Click(object sender, EventArgs e)
        {
            datas.cmd_charge_enable = !datas.cmd_charge_enable;
            if (is_running == false)
            {
                datas.addDataAccess(1);
            }
        }

        private void MuteBTN_Click(object sender, EventArgs e)
        {
            datas.is_Mute = !datas.is_Mute;
            if (is_running == false)
            {
                datas.addDataAccess(1);
            }
        }
    }
}
