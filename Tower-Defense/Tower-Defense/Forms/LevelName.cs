using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;

namespace Tower_Defense
{
    public partial class LevelName : Form
    {
        Vector2
            myStart,
            myGoal;
        char[,] myLevel;

        public LevelName()
        {
            InitializeComponent();
        }

        public void SetLevelInfo(char[,] aLevel, Vector2 aStart, Vector2 aGoal)
        {
            this.myLevel = aLevel;
            this.myStart = aStart;
            this.myGoal = aGoal;
        }

        private void LevelNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[A-Öa-ö]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void SaveLevelButton_Click(object sender, EventArgs e)
        {
            Level.SaveLevel(LevelNameBox.Text, myLevel, myStart, myGoal);

            this.Close();
        }
    }
}
