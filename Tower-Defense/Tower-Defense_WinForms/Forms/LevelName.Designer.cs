namespace Tower_Defense_WinForms
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
            this.levelNameLabel = new System.Windows.Forms.Label();
            this.levelNameBox = new System.Windows.Forms.TextBox();
            this.saveLevelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // levelNameLabel
            // 
            this.levelNameLabel.AutoSize = true;
            this.levelNameLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelNameLabel.Location = new System.Drawing.Point(12, 9);
            this.levelNameLabel.Name = "levelNameLabel";
            this.levelNameLabel.Size = new System.Drawing.Size(278, 18);
            this.levelNameLabel.TabIndex = 0;
            this.levelNameLabel.Text = "Type the name of your level";
            this.levelNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // levelNameBox
            // 
            this.levelNameBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelNameBox.Location = new System.Drawing.Point(26, 46);
            this.levelNameBox.Name = "levelNameBox";
            this.levelNameBox.Size = new System.Drawing.Size(272, 26);
            this.levelNameBox.TabIndex = 1;
            // 
            // saveLevelButton
            // 
            this.saveLevelButton.Location = new System.Drawing.Point(320, 77);
            this.saveLevelButton.Name = "saveLevelButton";
            this.saveLevelButton.Size = new System.Drawing.Size(75, 23);
            this.saveLevelButton.TabIndex = 2;
            this.saveLevelButton.Text = "Save";
            this.saveLevelButton.UseVisualStyleBackColor = true;
            // 
            // LevelName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 112);
            this.Controls.Add(this.saveLevelButton);
            this.Controls.Add(this.levelNameBox);
            this.Controls.Add(this.levelNameLabel);
            this.Name = "LevelName";
            this.Text = "Level Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label levelNameLabel;
        private System.Windows.Forms.TextBox levelNameBox;
        private System.Windows.Forms.Button saveLevelButton;
    }
}