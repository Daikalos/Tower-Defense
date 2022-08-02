using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Tower_Defense
{
    public partial class LevelWave : Form
    {
        List<TextBox> myEnemyStart_TextBoxes;
        List<TextBox> myEnemySpawnRate_TextBoxes;
        List<TextBox> myEnemyAmount_TextBoxes;
        List<RadioButton> myLevelDifficulty;

        public LevelWave()
        {
            InitializeComponent();

            myEnemyStart_TextBoxes = new List<TextBox>();
            myEnemyStart_TextBoxes.Add(Enemy_00_Start_TextBox);
            myEnemyStart_TextBoxes.Add(Enemy_01_Start_TextBox);
            myEnemyStart_TextBoxes.Add(Enemy_02_Start_TextBox);
            myEnemyStart_TextBoxes.Add(Enemy_03_Start_TextBox);

            myEnemySpawnRate_TextBoxes = new List<TextBox>();
            myEnemySpawnRate_TextBoxes.Add(Enemy_00_SpawnRate_TextBox);
            myEnemySpawnRate_TextBoxes.Add(Enemy_01_SpawnRate_TextBox);
            myEnemySpawnRate_TextBoxes.Add(Enemy_02_SpawnRate_TextBox);
            myEnemySpawnRate_TextBoxes.Add(Enemy_03_SpawnRate_TextBox);

            myEnemyAmount_TextBoxes = new List<TextBox>();
            myEnemyAmount_TextBoxes.Add(Enemy_00_Amount_TextBox);
            myEnemyAmount_TextBoxes.Add(Enemy_01_Amount_TextBox);
            myEnemyAmount_TextBoxes.Add(Enemy_02_Amount_TextBox);
            myEnemyAmount_TextBoxes.Add(Enemy_03_Amount_TextBox);

            myLevelDifficulty = new List<RadioButton>();
            myLevelDifficulty.Add(EasyWaveButton);
            myLevelDifficulty.Add(NormalWaveButton);
            myLevelDifficulty.Add(HardWaveButton);

            for (int i = 0; i < myEnemyStart_TextBoxes.Count; i++)
            {
                myEnemyStart_TextBoxes[i].Text = SpawnInfo.Enemy_Start[i].ToString();
            }

            for (int i = 0; i < myEnemySpawnRate_TextBoxes.Count; i++)
            {
                myEnemySpawnRate_TextBoxes[i].Text = SpawnInfo.Enemy_SpawnRate[i].ToString();
            }

            for (int i = 0; i < myEnemyAmount_TextBoxes.Count; i++)
            {
                myEnemyAmount_TextBoxes[i].Text = SpawnInfo.Enemy_Amount[i].ToString();
            }

            myLevelDifficulty[SpawnInfo.Difficulty].Checked = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < myEnemyStart_TextBoxes.Count; i++)
                {
                    SpawnInfo.Enemy_Start[i] = Int32.Parse(myEnemyStart_TextBoxes[i].Text);
                }

                for (int i = 0; i < myEnemySpawnRate_TextBoxes.Count; i++)
                {
                    SpawnInfo.Enemy_SpawnRate[i] = Int32.Parse(myEnemySpawnRate_TextBoxes[i].Text);
                }

                for (int i = 0; i < myEnemyAmount_TextBoxes.Count; i++)
                {
                    SpawnInfo.Enemy_Amount[i] = Int32.Parse(myEnemyAmount_TextBoxes[i].Text);
                }

                SpawnInfo.Difficulty = myLevelDifficulty.FindIndex(r => r.Checked);

                this.Close();
            } 
            catch (Exception anException)
            {
                MessageBox.Show(anException.Message);
            }
        }

        private void Enemy_00_Start_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_00_SpawnRate_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_00_Amount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_01_Start_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_01_SpawnRate_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_01_Amount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_02_Start_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_02_SpawnRate_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_02_Amount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_03_Start_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_03_SpawnRate_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Enemy_03_Amount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"[0-9]") && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
