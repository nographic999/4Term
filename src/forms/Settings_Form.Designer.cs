namespace _4Term
{
    partial class Settings_Form
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
            this.AdvancedModeCheckBox = new System.Windows.Forms.CheckBox();
            this.FormGroupBox = new System.Windows.Forms.GroupBox();
            this.FormHeightTextBox = new System.Windows.Forms.TextBox();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.FormWidthTextBox = new System.Windows.Forms.TextBox();
            this.ReceiveColorButton = new System.Windows.Forms.Button();
            this.ReceiveColorLabel = new System.Windows.Forms.Label();
            this.TransmitColorButton = new System.Windows.Forms.Button();
            this.TransmitColorLabel = new System.Windows.Forms.Label();
            this.BackgroundColorButton = new System.Windows.Forms.Button();
            this.BackgroundColorLabel = new System.Windows.Forms.Label();
            this.FontButton = new System.Windows.Forms.Button();
            this.FontLabel = new System.Windows.Forms.Label();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.RichTextBoxGroupBox = new System.Windows.Forms.GroupBox();
            this.ToggleScrollingCheckBox = new System.Windows.Forms.CheckBox();
            this.WordWrapCheckBox = new System.Windows.Forms.CheckBox();
            this.HexOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.LocalEchoCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FormGroupBox.SuspendLayout();
            this.RichTextBoxGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AdvancedModeCheckBox
            // 
            this.AdvancedModeCheckBox.AutoSize = true;
            this.AdvancedModeCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvancedModeCheckBox.Location = new System.Drawing.Point(9, 67);
            this.AdvancedModeCheckBox.Name = "AdvancedModeCheckBox";
            this.AdvancedModeCheckBox.Size = new System.Drawing.Size(104, 17);
            this.AdvancedModeCheckBox.TabIndex = 11;
            this.AdvancedModeCheckBox.Text = "Advanced mode";
            this.AdvancedModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // FormGroupBox
            // 
            this.FormGroupBox.Controls.Add(this.FormHeightTextBox);
            this.FormGroupBox.Controls.Add(this.AdvancedModeCheckBox);
            this.FormGroupBox.Controls.Add(this.WidthLabel);
            this.FormGroupBox.Controls.Add(this.HeightLabel);
            this.FormGroupBox.Controls.Add(this.FormWidthTextBox);
            this.FormGroupBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormGroupBox.Location = new System.Drawing.Point(10, 10);
            this.FormGroupBox.Name = "FormGroupBox";
            this.FormGroupBox.Size = new System.Drawing.Size(175, 87);
            this.FormGroupBox.TabIndex = 6;
            this.FormGroupBox.TabStop = false;
            this.FormGroupBox.Text = "Form";
            // 
            // FormHeightTextBox
            // 
            this.FormHeightTextBox.Location = new System.Drawing.Point(115, 39);
            this.FormHeightTextBox.Name = "FormHeightTextBox";
            this.FormHeightTextBox.Size = new System.Drawing.Size(50, 20);
            this.FormHeightTextBox.TabIndex = 16;
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WidthLabel.Location = new System.Drawing.Point(6, 16);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(37, 13);
            this.WidthLabel.TabIndex = 13;
            this.WidthLabel.Text = "Width";
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeightLabel.Location = new System.Drawing.Point(6, 44);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(43, 13);
            this.HeightLabel.TabIndex = 14;
            this.HeightLabel.Text = "Height";
            // 
            // FormWidthTextBox
            // 
            this.FormWidthTextBox.Location = new System.Drawing.Point(115, 13);
            this.FormWidthTextBox.Name = "FormWidthTextBox";
            this.FormWidthTextBox.Size = new System.Drawing.Size(50, 20);
            this.FormWidthTextBox.TabIndex = 15;
            // 
            // ReceiveColorButton
            // 
            this.ReceiveColorButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReceiveColorButton.Location = new System.Drawing.Point(116, 68);
            this.ReceiveColorButton.Name = "ReceiveColorButton";
            this.ReceiveColorButton.Size = new System.Drawing.Size(50, 23);
            this.ReceiveColorButton.TabIndex = 12;
            this.ReceiveColorButton.Text = "Set";
            this.ReceiveColorButton.UseVisualStyleBackColor = true;
            this.ReceiveColorButton.Click += new System.EventHandler(this.ReceiveColorButton_Click);
            // 
            // ReceiveColorLabel
            // 
            this.ReceiveColorLabel.AutoSize = true;
            this.ReceiveColorLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReceiveColorLabel.Location = new System.Drawing.Point(7, 73);
            this.ReceiveColorLabel.Name = "ReceiveColorLabel";
            this.ReceiveColorLabel.Size = new System.Drawing.Size(85, 13);
            this.ReceiveColorLabel.TabIndex = 11;
            this.ReceiveColorLabel.Text = "Receive Color";
            // 
            // TransmitColorButton
            // 
            this.TransmitColorButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransmitColorButton.Location = new System.Drawing.Point(116, 39);
            this.TransmitColorButton.Name = "TransmitColorButton";
            this.TransmitColorButton.Size = new System.Drawing.Size(50, 23);
            this.TransmitColorButton.TabIndex = 10;
            this.TransmitColorButton.Text = "Set";
            this.TransmitColorButton.UseVisualStyleBackColor = true;
            this.TransmitColorButton.Click += new System.EventHandler(this.TransmitColorButton_Click);
            // 
            // TransmitColorLabel
            // 
            this.TransmitColorLabel.AutoSize = true;
            this.TransmitColorLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransmitColorLabel.Location = new System.Drawing.Point(7, 44);
            this.TransmitColorLabel.Name = "TransmitColorLabel";
            this.TransmitColorLabel.Size = new System.Drawing.Size(91, 13);
            this.TransmitColorLabel.TabIndex = 9;
            this.TransmitColorLabel.Text = "Transmit Color";
            // 
            // BackgroundColorButton
            // 
            this.BackgroundColorButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackgroundColorButton.Location = new System.Drawing.Point(116, 11);
            this.BackgroundColorButton.Name = "BackgroundColorButton";
            this.BackgroundColorButton.Size = new System.Drawing.Size(50, 23);
            this.BackgroundColorButton.TabIndex = 8;
            this.BackgroundColorButton.Text = "Set";
            this.BackgroundColorButton.UseVisualStyleBackColor = true;
            this.BackgroundColorButton.Click += new System.EventHandler(this.BackgroundColorButton_Click);
            // 
            // BackgroundColorLabel
            // 
            this.BackgroundColorLabel.AutoSize = true;
            this.BackgroundColorLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackgroundColorLabel.Location = new System.Drawing.Point(7, 16);
            this.BackgroundColorLabel.Name = "BackgroundColorLabel";
            this.BackgroundColorLabel.Size = new System.Drawing.Size(103, 13);
            this.BackgroundColorLabel.TabIndex = 7;
            this.BackgroundColorLabel.Text = "Background Color";
            // 
            // FontButton
            // 
            this.FontButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FontButton.Location = new System.Drawing.Point(115, 16);
            this.FontButton.Name = "FontButton";
            this.FontButton.Size = new System.Drawing.Size(50, 23);
            this.FontButton.TabIndex = 4;
            this.FontButton.Text = "Set";
            this.FontButton.UseVisualStyleBackColor = true;
            this.FontButton.Click += new System.EventHandler(this.FontButton_Click);
            // 
            // FontLabel
            // 
            this.FontLabel.AutoSize = true;
            this.FontLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FontLabel.Location = new System.Drawing.Point(6, 21);
            this.FontLabel.Name = "FontLabel";
            this.FontLabel.Size = new System.Drawing.Size(31, 13);
            this.FontLabel.TabIndex = 0;
            this.FontLabel.Text = "Font";
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveSettingsButton.Location = new System.Drawing.Point(198, 211);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(175, 23);
            this.SaveSettingsButton.TabIndex = 8;
            this.SaveSettingsButton.Text = "Save Settings";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // RichTextBoxGroupBox
            // 
            this.RichTextBoxGroupBox.Controls.Add(this.FontButton);
            this.RichTextBoxGroupBox.Controls.Add(this.ToggleScrollingCheckBox);
            this.RichTextBoxGroupBox.Controls.Add(this.FontLabel);
            this.RichTextBoxGroupBox.Controls.Add(this.WordWrapCheckBox);
            this.RichTextBoxGroupBox.Controls.Add(this.HexOutputCheckBox);
            this.RichTextBoxGroupBox.Controls.Add(this.LocalEchoCheckBox);
            this.RichTextBoxGroupBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichTextBoxGroupBox.Location = new System.Drawing.Point(10, 103);
            this.RichTextBoxGroupBox.Name = "RichTextBoxGroupBox";
            this.RichTextBoxGroupBox.Size = new System.Drawing.Size(175, 131);
            this.RichTextBoxGroupBox.TabIndex = 13;
            this.RichTextBoxGroupBox.TabStop = false;
            this.RichTextBoxGroupBox.Text = "RichTextBox";
            // 
            // ToggleScrollingCheckBox
            // 
            this.ToggleScrollingCheckBox.AutoSize = true;
            this.ToggleScrollingCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToggleScrollingCheckBox.Location = new System.Drawing.Point(9, 60);
            this.ToggleScrollingCheckBox.Name = "ToggleScrollingCheckBox";
            this.ToggleScrollingCheckBox.Size = new System.Drawing.Size(122, 17);
            this.ToggleScrollingCheckBox.TabIndex = 10;
            this.ToggleScrollingCheckBox.Text = "Toggle scrolling";
            this.ToggleScrollingCheckBox.UseVisualStyleBackColor = true;
            // 
            // WordWrapCheckBox
            // 
            this.WordWrapCheckBox.AutoSize = true;
            this.WordWrapCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WordWrapCheckBox.Location = new System.Drawing.Point(9, 37);
            this.WordWrapCheckBox.Name = "WordWrapCheckBox";
            this.WordWrapCheckBox.Size = new System.Drawing.Size(80, 17);
            this.WordWrapCheckBox.TabIndex = 9;
            this.WordWrapCheckBox.Text = "Word wrap";
            this.WordWrapCheckBox.UseVisualStyleBackColor = true;
            // 
            // HexOutputCheckBox
            // 
            this.HexOutputCheckBox.AutoSize = true;
            this.HexOutputCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HexOutputCheckBox.Location = new System.Drawing.Point(9, 83);
            this.HexOutputCheckBox.Name = "HexOutputCheckBox";
            this.HexOutputCheckBox.Size = new System.Drawing.Size(86, 17);
            this.HexOutputCheckBox.TabIndex = 8;
            this.HexOutputCheckBox.Text = "Hex output";
            this.HexOutputCheckBox.UseVisualStyleBackColor = true;
            // 
            // LocalEchoCheckBox
            // 
            this.LocalEchoCheckBox.AutoSize = true;
            this.LocalEchoCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalEchoCheckBox.Location = new System.Drawing.Point(9, 106);
            this.LocalEchoCheckBox.Name = "LocalEchoCheckBox";
            this.LocalEchoCheckBox.Size = new System.Drawing.Size(86, 17);
            this.LocalEchoCheckBox.TabIndex = 5;
            this.LocalEchoCheckBox.Text = "Local echo";
            this.LocalEchoCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TransmitColorLabel);
            this.groupBox1.Controls.Add(this.BackgroundColorButton);
            this.groupBox1.Controls.Add(this.BackgroundColorLabel);
            this.groupBox1.Controls.Add(this.ReceiveColorButton);
            this.groupBox1.Controls.Add(this.TransmitColorButton);
            this.groupBox1.Controls.Add(this.ReceiveColorLabel);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(191, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 99);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color";
            // 
            // Settings_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 245);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RichTextBoxGroupBox);
            this.Controls.Add(this.SaveSettingsButton);
            this.Controls.Add(this.FormGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings_Form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormGroupBox.ResumeLayout(false);
            this.FormGroupBox.PerformLayout();
            this.RichTextBoxGroupBox.ResumeLayout(false);
            this.RichTextBoxGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox FormGroupBox;
        private System.Windows.Forms.Label FontLabel;
        private System.Windows.Forms.Button ReceiveColorButton;
        private System.Windows.Forms.Label ReceiveColorLabel;
        private System.Windows.Forms.Button TransmitColorButton;
        private System.Windows.Forms.Label TransmitColorLabel;
        private System.Windows.Forms.Button BackgroundColorButton;
        private System.Windows.Forms.Label BackgroundColorLabel;
        private System.Windows.Forms.Button FontButton;
        private System.Windows.Forms.TextBox FormHeightTextBox;
        private System.Windows.Forms.TextBox FormWidthTextBox;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.CheckBox AdvancedModeCheckBox;
        private System.Windows.Forms.GroupBox RichTextBoxGroupBox;
        private System.Windows.Forms.CheckBox ToggleScrollingCheckBox;
        private System.Windows.Forms.CheckBox WordWrapCheckBox;
        private System.Windows.Forms.CheckBox HexOutputCheckBox;
        private System.Windows.Forms.CheckBox LocalEchoCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}