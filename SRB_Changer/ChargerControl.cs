using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRB.Frame;

namespace SRB.NodeType.Charger
{
    partial class ChangerControl : UserControl
    {
        Node node;

        public ChangerControl(Node n)
        {
            node = n;
            InitializeComponent();
            MorseTB.KeyPress += MorseTB_KeyPress;
            node.eBankChangeByAccess += Node_eBankChangeByAccess;
            node.eDataAccessRecv += Node_eDataAccessRecv;
            node.singleAccess(2,0);
            this.ToolTips.SetToolTip(PlayBTN, "Click to play morse.");
            this.ToolTips.SetToolTip(BatteryPowerLedBTN, "Click to Toggle Voltage LED On PCB.");
            this.ToolTips.SetToolTip(MuteBTN, "Click to Enable or disable buzzer alram.");
            this.ToolTips.SetToolTip(ChangeEnableBTN, "Click to enable or disable charge.");
        }

        private void Node_eDataAccessRecv(object sender, BaseNode.AccessEventArgs e)
        {
            this.node.buzzer_commend = 0x80;
        }

        private void Node_eBankChangeByAccess(object sender, EventArgs e)
        {
            this.BatteryValueLAB.Text =( ((double)node.battery_voltage) / 1000.0).ToString("0.000") + "V";
            this.ChangeVottageBar.Value = node.battery_voltage.enterRound(6000, 8400); ;
            this.ChargeTimerLAB.Text = node.charge_second.ToString() + "S";
            this.statusLAB.Text = node.getStatues(); 
            if (node.cmd_charge_enable)
            {
                this.ChangeEnableBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175709;
            }
            else
            {
                this.ChangeEnableBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175309;
            }
            if(node.is_Mute)
            {
                this.MuteBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175310;
            }
            else
            {
                this.MuteBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175710;
            }
            if(node.is_PowerLEDRun)
            {
                this.BatteryPowerLedBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175695;
            }
            else
            {
                this.BatteryPowerLedBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175295;
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
            try{
                MorseTB.Text = MorseEnter.charToMorse(MorseCharTB.Text.ToUpper().ToArray()[0]);
            }
            catch
            {

            }
        }

        private void PlayBTN_Click(object sender, EventArgs e)
        {
            node.play(MorseTB.Text);
            if (sendTimer.Enabled == false)
            {
                node.singleAccess(1);
            }

        }
        
        private void RunStopBTN_Click(object sender, EventArgs e)
        {
            if (sendTimer.Enabled)
            {
                this.RunStopBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175842;
                sendTimer.Stop() ;
            }
            else
            {
                this.RunStopBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175836;
                sendTimer.Start();
            }

        }

        private void sendTimer_Tick(object sender, EventArgs e)
        {
            node.singleAccess(1);
        }

        private void BatteryPowerLedBTN_Click(object sender, EventArgs e)
        {
            node.is_PowerLEDRun = !node.is_PowerLEDRun;
            if (sendTimer.Enabled == false)
            {
                node.singleAccess(1);
            }
        }

        private void ChangeEnableBTN_Click(object sender, EventArgs e)
        {
            node.cmd_charge_enable = !node.cmd_charge_enable;
            if (sendTimer.Enabled == false)
            {
                node.singleAccess(1);
            }
        }

        private void MuteBTN_Click(object sender, EventArgs e)
        {     
            node.is_Mute = !node.is_Mute;
            if (sendTimer.Enabled == false)
            {
                node.singleAccess(1);
            }
        }
    }
}
