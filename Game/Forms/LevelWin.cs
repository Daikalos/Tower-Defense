using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Tower_Defense
{
    public partial class LevelWin : Form
    {
        public LevelWin()
        {
            InitializeComponent();
        }

        private void FreePlayButton_Click(object sender, EventArgs e)
        {
            GameInfo.IsFreePlay = true;
            this.Close();
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            GameInfo.ReturnToMenu = true;
            this.Close();
        }
    }
}
