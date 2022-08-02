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

            WaveTextBox.Text = GameInfo.TotalWaves.ToString();

            XSizeTextBox.Text = Level.GetTiles.GetLength(0).ToString();
            YSizeTextBox.Text = Level.GetTiles.GetLength(1).ToString();
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

        private void WaveTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void SaveInfoButton_Click(object sender, EventArgs e)
        {
            if (XSizeTextBox.Text != "" && YSizeTextBox.Text != "")
            {
                int[,] tempSize;
                try
                {
                    tempSize = new int[
                        Int32.Parse(XSizeTextBox.Text),
                        Int32.Parse(YSizeTextBox.Text)];
                    GameInfo.TotalWaves = Int32.Parse(WaveTextBox.Text);
                }
                catch (Exception anException) 
                {
                    MessageBox.Show(anException.Message);
                    return; 
                }

                if (tempSize.GetLength(0) != 0 && tempSize.GetLength(1) != 0)
                {
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
