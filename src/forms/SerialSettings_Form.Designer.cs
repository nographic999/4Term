namespace _4Term
{
    partial class SerialPortOptionsForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RtsCheckBox = new System.Windows.Forms.CheckBox();
            this.DtrCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.HandshakingLabel = new System.Windows.Forms.Label();
            this.HandshakingComboBox = new System.Windows.Forms.ComboBox();
            this.StopBitsComboBox = new System.Windows.Forms.ComboBox();
            this.ParityComboBox = new System.Windows.Forms.ComboBox();
            this.DataBitsComboBox = new System.Windows.Forms.ComboBox();
            this.BaudRateComboBox = new System.Windows.Forms.ComboBox();
            this.ParityLabel = new System.Windows.Forms.Label();
            this.StopBitsLabel = new System.Windows.Forms.Label();
            this.DataBitsLabel = new System.Windows.Forms.Label();
            this.BaudRateLabel = new System.Windows.Forms.Label();
            this.COMPortLabel = new System.Windows.Forms.Label();
            this.COMPortComboBox = new System.Windows.Forms.ComboBox();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RtsCheckBox);
            this.groupBox2.Controls.Add(this.DtrCheckBox);
            this.groupBox2.Controls.Add(this.SaveSettingsButton);
            this.groupBox2.Controls.Add(this.HandshakingLabel);
            this.groupBox2.Controls.Add(this.HandshakingComboBox);
            this.groupBox2.Controls.Add(this.StopBitsComboBox);
            this.groupBox2.Controls.Add(this.ParityComboBox);
            this.groupBox2.Controls.Add(this.DataBitsComboBox);
            this.groupBox2.Controls.Add(this.BaudRateComboBox);
            this.groupBox2.Controls.Add(this.ParityLabel);
            this.groupBox2.Controls.Add(this.StopBitsLabel);
            this.groupBox2.Controls.Add(this.DataBitsLabel);
            this.groupBox2.Controls.Add(this.BaudRateLabel);
            this.groupBox2.Controls.Add(this.COMPortLabel);
            this.groupBox2.Controls.Add(this.COMPortComboBox);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(9, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(354, 133);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "COM Port Configuration";
            // 
            // RtsCheckBox
            // 
            this.RtsCheckBox.AutoSize = true;
            this.RtsCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RtsCheckBox.Location = new System.Drawing.Point(85, 106);
            this.RtsCheckBox.Name = "RtsCheckBox";
            this.RtsCheckBox.Size = new System.Drawing.Size(44, 17);
            this.RtsCheckBox.TabIndex = 13;
            this.RtsCheckBox.Text = "RTS";
            this.RtsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DtrCheckBox
            // 
            this.DtrCheckBox.AutoSize = true;
            this.DtrCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtrCheckBox.Location = new System.Drawing.Point(9, 105);
            this.DtrCheckBox.Name = "DtrCheckBox";
            this.DtrCheckBox.Size = new System.Drawing.Size(44, 17);
            this.DtrCheckBox.TabIndex = 12;
            this.DtrCheckBox.Text = "DTR";
            this.DtrCheckBox.UseVisualStyleBackColor = true;
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveSettingsButton.Location = new System.Drawing.Point(181, 100);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(153, 23);
            this.SaveSettingsButton.TabIndex = 4;
            this.SaveSettingsButton.Text = "Save Settings";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // HandshakingLabel
            // 
            this.HandshakingLabel.AutoSize = true;
            this.HandshakingLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HandshakingLabel.Location = new System.Drawing.Point(178, 76);
            this.HandshakingLabel.Name = "HandshakingLabel";
            this.HandshakingLabel.Size = new System.Drawing.Size(73, 13);
            this.HandshakingLabel.TabIndex = 6;
            this.HandshakingLabel.Text = "Handshaking";
            // 
            // HandshakingComboBox
            // 
            this.HandshakingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HandshakingComboBox.DropDownWidth = 125;
            this.HandshakingComboBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HandshakingComboBox.FormattingEnabled = true;
            this.HandshakingComboBox.Location = new System.Drawing.Point(257, 73);
            this.HandshakingComboBox.Name = "HandshakingComboBox";
            this.HandshakingComboBox.Size = new System.Drawing.Size(87, 21);
            this.HandshakingComboBox.TabIndex = 11;
            // 
            // StopBitsComboBox
            // 
            this.StopBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StopBitsComboBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopBitsComboBox.FormattingEnabled = true;
            this.StopBitsComboBox.Location = new System.Drawing.Point(257, 46);
            this.StopBitsComboBox.Name = "StopBitsComboBox";
            this.StopBitsComboBox.Size = new System.Drawing.Size(87, 21);
            this.StopBitsComboBox.TabIndex = 10;
            // 
            // ParityComboBox
            // 
            this.ParityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ParityComboBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParityComboBox.FormattingEnabled = true;
            this.ParityComboBox.Location = new System.Drawing.Point(257, 19);
            this.ParityComboBox.Name = "ParityComboBox";
            this.ParityComboBox.Size = new System.Drawing.Size(87, 21);
            this.ParityComboBox.TabIndex = 9;
            // 
            // DataBitsComboBox
            // 
            this.DataBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DataBitsComboBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataBitsComboBox.FormattingEnabled = true;
            this.DataBitsComboBox.Location = new System.Drawing.Point(85, 73);
            this.DataBitsComboBox.Name = "DataBitsComboBox";
            this.DataBitsComboBox.Size = new System.Drawing.Size(87, 21);
            this.DataBitsComboBox.TabIndex = 8;
            // 
            // BaudRateComboBox
            // 
            this.BaudRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BaudRateComboBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BaudRateComboBox.FormattingEnabled = true;
            this.BaudRateComboBox.Location = new System.Drawing.Point(85, 46);
            this.BaudRateComboBox.Name = "BaudRateComboBox";
            this.BaudRateComboBox.Size = new System.Drawing.Size(87, 21);
            this.BaudRateComboBox.TabIndex = 7;
            // 
            // ParityLabel
            // 
            this.ParityLabel.AutoSize = true;
            this.ParityLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParityLabel.Location = new System.Drawing.Point(178, 22);
            this.ParityLabel.Name = "ParityLabel";
            this.ParityLabel.Size = new System.Drawing.Size(43, 13);
            this.ParityLabel.TabIndex = 5;
            this.ParityLabel.Text = "Parity";
            // 
            // StopBitsLabel
            // 
            this.StopBitsLabel.AutoSize = true;
            this.StopBitsLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopBitsLabel.Location = new System.Drawing.Point(178, 49);
            this.StopBitsLabel.Name = "StopBitsLabel";
            this.StopBitsLabel.Size = new System.Drawing.Size(61, 13);
            this.StopBitsLabel.TabIndex = 4;
            this.StopBitsLabel.Text = "Stop Bits";
            // 
            // DataBitsLabel
            // 
            this.DataBitsLabel.AutoSize = true;
            this.DataBitsLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataBitsLabel.Location = new System.Drawing.Point(6, 76);
            this.DataBitsLabel.Name = "DataBitsLabel";
            this.DataBitsLabel.Size = new System.Drawing.Size(61, 13);
            this.DataBitsLabel.TabIndex = 3;
            this.DataBitsLabel.Text = "Data Bits";
            // 
            // BaudRateLabel
            // 
            this.BaudRateLabel.AutoSize = true;
            this.BaudRateLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BaudRateLabel.Location = new System.Drawing.Point(6, 49);
            this.BaudRateLabel.Name = "BaudRateLabel";
            this.BaudRateLabel.Size = new System.Drawing.Size(61, 13);
            this.BaudRateLabel.TabIndex = 2;
            this.BaudRateLabel.Text = "Baud Rate";
            // 
            // COMPortLabel
            // 
            this.COMPortLabel.AutoSize = true;
            this.COMPortLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.COMPortLabel.Location = new System.Drawing.Point(6, 22);
            this.COMPortLabel.Name = "COMPortLabel";
            this.COMPortLabel.Size = new System.Drawing.Size(55, 13);
            this.COMPortLabel.TabIndex = 1;
            this.COMPortLabel.Text = "COM Port";
            // 
            // COMPortComboBox
            // 
            this.COMPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMPortComboBox.DropDownWidth = 175;
            this.COMPortComboBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.COMPortComboBox.FormattingEnabled = true;
            this.COMPortComboBox.IntegralHeight = false;
            this.COMPortComboBox.Location = new System.Drawing.Point(85, 19);
            this.COMPortComboBox.Name = "COMPortComboBox";
            this.COMPortComboBox.Size = new System.Drawing.Size(87, 21);
            this.COMPortComboBox.TabIndex = 0;
            this.COMPortComboBox.Click += new System.EventHandler(this.COMPortComboBox_Click);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(129, 57);
            // 
            // SerialPortOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 147);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SerialPortOptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Serial port settings";
            this.TopMost = true;
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox HandshakingComboBox;
        private System.Windows.Forms.ComboBox StopBitsComboBox;
        private System.Windows.Forms.ComboBox ParityComboBox;
        private System.Windows.Forms.ComboBox DataBitsComboBox;
        private System.Windows.Forms.ComboBox BaudRateComboBox;
        private System.Windows.Forms.Label HandshakingLabel;
        private System.Windows.Forms.Label ParityLabel;
        private System.Windows.Forms.Label StopBitsLabel;
        private System.Windows.Forms.Label DataBitsLabel;
        private System.Windows.Forms.Label BaudRateLabel;
        private System.Windows.Forms.Label COMPortLabel;
        private System.Windows.Forms.ComboBox COMPortComboBox;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.CheckBox DtrCheckBox;
        private System.Windows.Forms.CheckBox RtsCheckBox;
    }
}