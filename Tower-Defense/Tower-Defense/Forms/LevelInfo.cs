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

        private void xSizeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void ySizeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void WavesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void SaveInfoButton_Click(object sender, EventArgs e)
        {
            RadioButton tempCheckedButton = TerrainPanel.Controls.OfType<RadioButton>()
                          .FirstOrDefault(r => r.Checked); //Linq get button

            if (tempCheckedButton != null && XSizeTextBox.Text != "" && YSizeTextBox.Text != "")
            {
                int[,] tempSize;
                try
                {
                    tempSize = new int[
                        Int32.Parse(XSizeTextBox.Text),
                        Int32.Parse(YSizeTextBox.Text)];
                }
                catch (Exception anException) 
                {
                    MessageBox.Show(anException.ToString());
                    return; 
                }

                if (tempSize.GetLength(0) != 0 && tempSize.GetLength(1) != 0)
                {
                    GameInfo.TerrainType = tempCheckedButton.Text;

                    if (Level.CreateLevel(tempSize))
                    {
                        Level.LoadContentEditor();
                        this.Close();
                    }
                }
            }
        }
    }
}
