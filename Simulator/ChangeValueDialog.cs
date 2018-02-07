using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    public partial class ChangeValueDialog : Form
    {
        public int mode { get; private set; }
        public uint idx { get; private set; }
        public uint newValue { get; private set; }

        public ChangeValueDialog(int cvdMode, uint idx)
        {
            mode = cvdMode;
            InitializeComponent();
            switch (cvdMode)
            {
                case CvdModeConstVar.REGISTER_MODE:
                    Text = "Change Register Value";
                    locationLabel.Text = "Register";
                    if (idx < ConstVar.REGISTER_NUM)
                    {
                        registerSelectionComboBox.SelectedIndex = (int)idx;
                    }
                    else
                    {
                        registerSelectionComboBox.SelectedIndex = -1;
                    }
                    registerSelectionComboBox.Visible = true;
                    registerModePromptLabel.Visible = true;
                    break;
                case CvdModeConstVar.MEMORY_MODE:
                    Text = "Change Memory Value";
                    locationLabel.Text = "Address";
                    idx = (idx / 4) * 4;
                    addressTextBox.Text = "x" + idx.ToString("X8");
                    addressTextBox.Visible = true;
                    memoryModePromptLabel.Visible = true;
                    break;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool tryParseToUint(string numStr, out uint num)
        {
            for (int i = 0; i < numStr.Length; ++i)
            {
                if (!char.IsWhiteSpace(numStr, i))
                {
                    numStr = numStr.Substring(i);
                    break;
                }
            }
            if (numStr[0] == 'x' || numStr[0] == 'X')
            {
                return uint.TryParse(numStr.Substring(1), System.Globalization.NumberStyles.HexNumber, null, out num);
            }
            else
            {
                return uint.TryParse(numStr, System.Globalization.NumberStyles.AllowLeadingWhite | System.Globalization.NumberStyles.AllowTrailingWhite, null, out num);
            }
        }
    
        private void buttonOK_Click(object sender, EventArgs e)
        {
            uint newValue;
            if (!tryParseToUint(newValueTextBox.Text, out newValue))
            {
                MessageBox.Show("Illegal Value.");
                return;
            }
            switch (mode)
            {
                case CvdModeConstVar.REGISTER_MODE:
                    if (registerSelectionComboBox.SelectedIndex < 0 || registerSelectionComboBox.SelectedIndex >= ConstVar.REGISTER_NUM)
                    {
                        MessageBox.Show("Please select a register.");
                        return;
                    }
                    this.newValue = newValue / 4 * 4;
                    idx = (uint)registerSelectionComboBox.SelectedIndex;
                    break;
                case CvdModeConstVar.MEMORY_MODE:
                    uint address;
                    if (!tryParseToUint(addressTextBox.Text, out address))
                    {
                        MessageBox.Show("Illegal address.");
                        return;
                    }
                    if (address / 4 > ConstVar.MAX_WORDS)
                    {
                        MessageBox.Show("Inexistent address.");
                        return;
                    }
                    this.newValue = newValue;
                    idx = address / 4;
                    break;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    class CvdModeConstVar
    {
        public const int REGISTER_MODE = 0;
        public const int MEMORY_MODE = 1;
        public const int PC_SERNUM = 0;
    }

}
