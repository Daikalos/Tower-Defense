using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;
using System.Linq;

namespace Tower_Defense
{
    public partial class LevelInfo : Form
    {
        public LevelInfo()
        {
            InitializeComponent();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void xSizeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]"))
            {
                e.Handled = true;
            }
        }

        private void ySizeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]"))
            {
                e.Handled = true;
            }
        }

        private void WavesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]"))
            {
                e.Handled = true;
            }
        }

        private void SaveInfoButton_Click(object sender, EventArgs e)
        {
            RadioButton tempCheckedButton = TerrainPanel.Controls.OfType<RadioButton>()
                          .FirstOrDefault(r => r.Checked); //Linq get button

            if (tempCheckedButton != null)
            {
                int[,] tempSize = new int[
                    Int32.Parse(XSizeTextBox.Text),
                    Int32.Parse(YSizeTextBox.Text)];

                GameInfo.TerrainType = tempCheckedButton.Text;

                Level.CreateLevel(tempSize);
                Level.LoadContentEditor();

                this.Close();
            }
        }
    }
}
