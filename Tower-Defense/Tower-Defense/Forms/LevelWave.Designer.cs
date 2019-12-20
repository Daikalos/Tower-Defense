namespace Tower_Defense
{
    partial class LevelWave
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
            this.WaveFormatLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EasyWaveButton = new System.Windows.Forms.RadioButton();
            this.NormalWaveButton = new System.Windows.Forms.RadioButton();
            this.HardWaveButton = new System.Windows.Forms.RadioButton();
            this.CustomWaveButton = new System.Windows.Forms.RadioButton();
            this.SaveButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // WaveFormatLabel
            // 
            this.WaveFormatLabel.AutoSize = true;
            this.WaveFormatLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WaveFormatLabel.Location = new System.Drawing.Point(9, 9);
            this.WaveFormatLabel.Name = "WaveFormatLabel";
            this.WaveFormatLabel.Size = new System.Drawing.Size(118, 18);
            this.WaveFormatLabel.TabIndex = 0;
            this.WaveFormatLabel.Text = "Wave Format";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CustomWaveButton);
            this.panel1.Controls.Add(this.HardWaveButton);
            this.panel1.Controls.Add(this.NormalWaveButton);
            this.panel1.Controls.Add(this.EasyWaveButton);
            this.panel1.Location = new System.Drawing.Point(12, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 100);
            this.panel1.TabIndex = 2;
            // 
            // EasyWaveButton
            // 
            this.EasyWaveButton.AutoSize = true;
            this.EasyWaveButton.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EasyWaveButton.Location = new System.Drawing.Point(4, 4);
            this.EasyWaveButton.Name = "EasyWaveButton";
            this.EasyWaveButton.Size = new System.Drawing.Size(54, 20);
            this.EasyWaveButton.TabIndex = 0;
            this.EasyWaveButton.Text = "Easy";
            this.EasyWaveButton.UseVisualStyleBackColor = true;
            // 
            // NormalWaveButton
            // 
            this.NormalWaveButton.AutoSize = true;
            this.NormalWaveButton.Checked = true;
            this.NormalWaveButton.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NormalWaveButton.Location = new System.Drawing.Point(4, 28);
            this.NormalWaveButton.Name = "NormalWaveButton";
            this.NormalWaveButton.Size = new System.Drawing.Size(68, 20);
            this.NormalWaveButton.TabIndex = 1;
            this.NormalWaveButton.TabStop = true;
            this.NormalWaveButton.Text = "Normal";
            this.NormalWaveButton.UseVisualStyleBackColor = true;
            // 
            // HardWaveButton
            // 
            this.HardWaveButton.AutoSize = true;
            this.HardWaveButton.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HardWaveButton.Location = new System.Drawing.Point(4, 52);
            this.HardWaveButton.Name = "HardWaveButton";
            this.HardWaveButton.Size = new System.Drawing.Size(54, 20);
            this.HardWaveButton.TabIndex = 2;
            this.HardWaveButton.Text = "Hard";
            this.HardWaveButton.UseVisualStyleBackColor = true;
            // 
            // CustomWaveButton
            // 
            this.CustomWaveButton.AutoSize = true;
            this.CustomWaveButton.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomWaveButton.Location = new System.Drawing.Point(4, 76);
            this.CustomWaveButton.Name = "CustomWaveButton";
            this.CustomWaveButton.Size = new System.Drawing.Size(68, 20);
            this.CustomWaveButton.TabIndex = 3;
            this.CustomWaveButton.Text = "Custom";
            this.CustomWaveButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(277, 132);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // LevelWave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 167);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.WaveFormatLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LevelWave";
            this.Text = "Level Wave";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WaveFormatLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton CustomWaveButton;
        private System.Windows.Forms.RadioButton HardWaveButton;
        private System.Windows.Forms.RadioButton NormalWaveButton;
        private System.Windows.Forms.RadioButton EasyWaveButton;
        private System.Windows.Forms.Button SaveButton;
    }
}