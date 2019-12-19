namespace Tower_Defense
{
    partial class LevelName
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LevelNameLabel = new System.Windows.Forms.Label();
            this.LevelNameBox = new System.Windows.Forms.TextBox();
            this.SaveLevelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LevelNameLabel
            // 
            this.LevelNameLabel.AutoSize = true;
            this.LevelNameLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelNameLabel.Location = new System.Drawing.Point(12, 9);
            this.LevelNameLabel.Name = "LevelNameLabel";
            this.LevelNameLabel.Size = new System.Drawing.Size(278, 18);
            this.LevelNameLabel.TabIndex = 0;
            this.LevelNameLabel.Text = "Type the name of your level";
            this.LevelNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LevelNameBox
            // 
            this.LevelNameBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelNameBox.Location = new System.Drawing.Point(26, 46);
            this.LevelNameBox.Name = "LevelNameBox";
            this.LevelNameBox.Size = new System.Drawing.Size(272, 26);
            this.LevelNameBox.TabIndex = 1;
            this.LevelNameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LevelNameBox_KeyPress);
            // 
            // SaveLevelButton
            // 
            this.SaveLevelButton.Location = new System.Drawing.Point(320, 77);
            this.SaveLevelButton.Name = "SaveLevelButton";
            this.SaveLevelButton.Size = new System.Drawing.Size(75, 23);
            this.SaveLevelButton.TabIndex = 2;
            this.SaveLevelButton.Text = "Save";
            this.SaveLevelButton.UseVisualStyleBackColor = true;
            this.SaveLevelButton.Click += new System.EventHandler(this.SaveLevelButton_Click);
            // 
            // LevelName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 112);
            this.Controls.Add(this.SaveLevelButton);
            this.Controls.Add(this.LevelNameBox);
            this.Controls.Add(this.LevelNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LevelName";
            this.Text = "Level Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LevelNameLabel;
        private System.Windows.Forms.TextBox LevelNameBox;
        private System.Windows.Forms.Button SaveLevelButton;
    }
}