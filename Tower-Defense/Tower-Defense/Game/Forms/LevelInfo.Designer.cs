namespace Tower_Defense
{
    partial class LevelInfo
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
            this.SaveInfoButton = new System.Windows.Forms.Button();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.YLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.YSizeTextBox = new System.Windows.Forms.TextBox();
            this.XSizeTextBox = new System.Windows.Forms.TextBox();
            this.WarningLabel = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveInfoButton
            // 
            this.SaveInfoButton.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveInfoButton.Location = new System.Drawing.Point(225, 115);
            this.SaveInfoButton.Name = "SaveInfoButton";
            this.SaveInfoButton.Size = new System.Drawing.Size(75, 23);
            this.SaveInfoButton.TabIndex = 2;
            this.SaveInfoButton.Text = "Save";
            this.SaveInfoButton.UseVisualStyleBackColor = true;
            this.SaveInfoButton.Click += new System.EventHandler(this.SaveInfoButton_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoLabel.Location = new System.Drawing.Point(12, 13);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(48, 18);
            this.InfoLabel.TabIndex = 6;
            this.InfoLabel.Text = "Info";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.YLabel);
            this.panel2.Controls.Add(this.XLabel);
            this.panel2.Controls.Add(this.YSizeTextBox);
            this.panel2.Controls.Add(this.XSizeTextBox);
            this.panel2.Location = new System.Drawing.Point(15, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 61);
            this.panel2.TabIndex = 7;
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YLabel.Location = new System.Drawing.Point(6, 30);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(18, 18);
            this.YLabel.TabIndex = 3;
            this.YLabel.Text = "Y";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XLabel.Location = new System.Drawing.Point(6, 4);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(18, 18);
            this.XLabel.TabIndex = 2;
            this.XLabel.Text = "X";
            // 
            // YSizeTextBox
            // 
            this.YSizeTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YSizeTextBox.Location = new System.Drawing.Point(30, 30);
            this.YSizeTextBox.Name = "YSizeTextBox";
            this.YSizeTextBox.Size = new System.Drawing.Size(68, 20);
            this.YSizeTextBox.TabIndex = 1;
            this.YSizeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ySizeTextBox_KeyPress);
            // 
            // XSizeTextBox
            // 
            this.XSizeTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XSizeTextBox.Location = new System.Drawing.Point(30, 3);
            this.XSizeTextBox.Name = "XSizeTextBox";
            this.XSizeTextBox.Size = new System.Drawing.Size(68, 20);
            this.XSizeTextBox.TabIndex = 0;
            this.XSizeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xSizeTextBox_KeyPress);
            // 
            // WarningLabel
            // 
            this.WarningLabel.AutoSize = true;
            this.WarningLabel.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WarningLabel.Location = new System.Drawing.Point(12, 118);
            this.WarningLabel.Name = "WarningLabel";
            this.WarningLabel.Size = new System.Drawing.Size(197, 16);
            this.WarningLabel.TabIndex = 8;
            this.WarningLabel.Text = "This will reset your level!";
            // 
            // LevelInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 150);
            this.Controls.Add(this.WarningLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.SaveInfoButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LevelInfo";
            this.Text = "Level Info";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SaveInfoButton;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.TextBox YSizeTextBox;
        private System.Windows.Forms.TextBox XSizeTextBox;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.Label WarningLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

