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
            this.CRLFRadioButton = new System.Windows.Forms.GroupBox();
            this.HexOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.LocalEchoCheckBox = new System.Windows.Forms.CheckBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.LFRadioButton = new System.Windows.Forms.RadioButton();
            this.CRRadioButton = new System.Windows.Forms.RadioButton();
            this.NoneRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FormHeightTextBox = new System.Windows.Forms.TextBox();
            this.FormWidthTextBox = new System.Windows.Forms.TextBox();
            this.FormHeightLabel = new System.Windows.Forms.Label();
            this.FormWidthLabel = new System.Windows.Forms.Label();
            this.ReceiveColorButton = new System.Windows.Forms.Button();
            this.ReceiveColorLabel = new System.Windows.Forms.Label();
            this.TransmitColorButton = new System.Windows.Forms.Button();
            this.TransmitColorLabel = new System.Windows.Forms.Label();
            this.BackgroundColorButton = new System.Windows.Forms.Button();
            this.BackgroundColorLabel = new System.Windows.Forms.Label();
            this.FontButton = new System.Windows.Forms.Button();
            this.FontLabel = new System.Windows.Forms.Label();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.WordWrapCheckBox = new System.Windows.Forms.CheckBox();
            this.ToggleScrollingCheckBox = new System.Windows.Forms.CheckBox();
            this.CRLFRadioButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CRLFRadioButton
            // 
            this.CRLFRadioButton.Controls.Add(this.ToggleScrollingCheckBox);
            this.CRLFRadioButton.Controls.Add(this.WordWrapCheckBox);
            this.CRLFRadioButton.Controls.Add(this.HexOutputCheckBox);
            this.CRLFRadioButton.Controls.Add(this.LocalEchoCheckBox);
            this.CRLFRadioButton.Controls.Add(this.radioButton4);
            this.CRLFRadioButton.Controls.Add(this.LFRadioButton);
            this.CRLFRadioButton.Controls.Add(this.CRRadioButton);
            this.CRLFRadioButton.Controls.Add(this.NoneRadioButton);
            this.CRLFRadioButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CRLFRadioButton.Location = new System.Drawing.Point(191, 10);
            this.CRLFRadioButton.Name = "CRLFRadioButton";
            this.CRLFRadioButton.Size = new System.Drawing.Size(200, 195);
            this.CRLFRadioButton.TabIndex = 2;
            this.CRLFRadioButton.TabStop = false;
            this.CRLFRadioButton.Text = "Options";
            // 
            // HexOutputCheckBox
            // 
            this.HexOutputCheckBox.AutoSize = true;
            this.HexOutputCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HexOutputCheckBox.Location = new System.Drawing.Point(6, 125);
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
            this.LocalEchoCheckBox.Location = new System.Drawing.Point(6, 148);
            this.LocalEchoCheckBox.Name = "LocalEchoCheckBox";
            this.LocalEchoCheckBox.Size = new System.Drawing.Size(86, 17);
            this.LocalEchoCheckBox.TabIndex = 5;
            this.LocalEchoCheckBox.Text = "Local echo";
            this.LocalEchoCheckBox.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(61, 51);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(61, 17);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "+CR-LF";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // LFRadioButton
            // 
            this.LFRadioButton.AutoSize = true;
            this.LFRadioButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LFRadioButton.Location = new System.Drawing.Point(6, 51);
            this.LFRadioButton.Name = "LFRadioButton";
            this.LFRadioButton.Size = new System.Drawing.Size(43, 17);
            this.LFRadioButton.TabIndex = 2;
            this.LFRadioButton.TabStop = true;
            this.LFRadioButton.Text = "+LF";
            this.LFRadioButton.UseVisualStyleBackColor = true;
            // 
            // CRRadioButton
            // 
            this.CRRadioButton.AutoSize = true;
            this.CRRadioButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CRRadioButton.Location = new System.Drawing.Point(61, 22);
            this.CRRadioButton.Name = "CRRadioButton";
            this.CRRadioButton.Size = new System.Drawing.Size(43, 17);
            this.CRRadioButton.TabIndex = 1;
            this.CRRadioButton.TabStop = true;
            this.CRRadioButton.Text = "+CR";
            this.CRRadioButton.UseVisualStyleBackColor = true;
            // 
            // NoneRadioButton
            // 
            this.NoneRadioButton.AutoSize = true;
            this.NoneRadioButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoneRadioButton.Location = new System.Drawing.Point(6, 22);
            this.NoneRadioButton.Name = "NoneRadioButton";
            this.NoneRadioButton.Size = new System.Drawing.Size(49, 17);
            this.NoneRadioButton.TabIndex = 0;
            this.NoneRadioButton.TabStop = true;
            this.NoneRadioButton.Text = "none";
            this.NoneRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FormHeightTextBox);
            this.groupBox1.Controls.Add(this.ReceiveColorButton);
            this.groupBox1.Controls.Add(this.ReceiveColorLabel);
            this.groupBox1.Controls.Add(this.FormWidthTextBox);
            this.groupBox1.Controls.Add(this.TransmitColorButton);
            this.groupBox1.Controls.Add(this.FormHeightLabel);
            this.groupBox1.Controls.Add(this.TransmitColorLabel);
            this.groupBox1.Controls.Add(this.BackgroundColorButton);
            this.groupBox1.Controls.Add(this.FormWidthLabel);
            this.groupBox1.Controls.Add(this.BackgroundColorLabel);
            this.groupBox1.Controls.Add(this.FontButton);
            this.groupBox1.Controls.Add(this.FontLabel);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 195);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Style";
            // 
            // FormHeightTextBox
            // 
            this.FormHeightTextBox.Location = new System.Drawing.Point(115, 45);
            this.FormHeightTextBox.Name = "FormHeightTextBox";
            this.FormHeightTextBox.Size = new System.Drawing.Size(50, 20);
            this.FormHeightTextBox.TabIndex = 16;
            // 
            // FormWidthTextBox
            // 
            this.FormWidthTextBox.Location = new System.Drawing.Point(115, 19);
            this.FormWidthTextBox.Name = "FormWidthTextBox";
            this.FormWidthTextBox.Size = new System.Drawing.Size(50, 20);
            this.FormWidthTextBox.TabIndex = 15;
            // 
            // FormHeightLabel
            // 
            this.FormHeightLabel.AutoSize = true;
            this.FormHeightLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormHeightLabel.Location = new System.Drawing.Point(6, 50);
            this.FormHeightLabel.Name = "FormHeightLabel";
            this.FormHeightLabel.Size = new System.Drawing.Size(73, 13);
            this.FormHeightLabel.TabIndex = 14;
            this.FormHeightLabel.Text = "Form Height";
            // 
            // FormWidthLabel
            // 
            this.FormWidthLabel.AutoSize = true;
            this.FormWidthLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormWidthLabel.Location = new System.Drawing.Point(6, 22);
            this.FormWidthLabel.Name = "FormWidthLabel";
            this.FormWidthLabel.Size = new System.Drawing.Size(67, 13);
            this.FormWidthLabel.TabIndex = 13;
            this.FormWidthLabel.Text = "Form Width";
            // 
            // ReceiveColorButton
            // 
            this.ReceiveColorButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReceiveColorButton.Location = new System.Drawing.Point(115, 131);
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
            this.ReceiveColorLabel.Location = new System.Drawing.Point(6, 136);
            this.ReceiveColorLabel.Name = "ReceiveColorLabel";
            this.ReceiveColorLabel.Size = new System.Drawing.Size(85, 13);
            this.ReceiveColorLabel.TabIndex = 11;
            this.ReceiveColorLabel.Text = "Receive Color";
            // 
            // TransmitColorButton
            // 
            this.TransmitColorButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransmitColorButton.Location = new System.Drawing.Point(115, 102);
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
            this.TransmitColorLabel.Location = new System.Drawing.Point(6, 107);
            this.TransmitColorLabel.Name = "TransmitColorLabel";
            this.TransmitColorLabel.Size = new System.Drawing.Size(91, 13);
            this.TransmitColorLabel.TabIndex = 9;
            this.TransmitColorLabel.Text = "Transmit Color";
            // 
            // BackgroundColorButton
            // 
            this.BackgroundColorButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackgroundColorButton.Location = new System.Drawing.Point(115, 160);
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
            this.BackgroundColorLabel.Location = new System.Drawing.Point(6, 165);
            this.BackgroundColorLabel.Name = "BackgroundColorLabel";
            this.BackgroundColorLabel.Size = new System.Drawing.Size(103, 13);
            this.BackgroundColorLabel.TabIndex = 7;
            this.BackgroundColorLabel.Text = "Background Color";
            // 
            // FontButton
            // 
            this.FontButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FontButton.Location = new System.Drawing.Point(115, 73);
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
            this.FontLabel.Location = new System.Drawing.Point(6, 78);
            this.FontLabel.Name = "FontLabel";
            this.FontLabel.Size = new System.Drawing.Size(31, 13);
            this.FontLabel.TabIndex = 0;
            this.FontLabel.Text = "Font";
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveSettingsButton.Location = new System.Drawing.Point(238, 211);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(153, 23);
            this.SaveSettingsButton.TabIndex = 8;
            this.SaveSettingsButton.Text = "Save Settings";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // WordWrapCheckBox
            // 
            this.WordWrapCheckBox.AutoSize = true;
            this.WordWrapCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WordWrapCheckBox.Location = new System.Drawing.Point(6, 79);
            this.WordWrapCheckBox.Name = "WordWrapCheckBox";
            this.WordWrapCheckBox.Size = new System.Drawing.Size(80, 17);
            this.WordWrapCheckBox.TabIndex = 9;
            this.WordWrapCheckBox.Text = "Word wrap";
            this.WordWrapCheckBox.UseVisualStyleBackColor = true;
            // 
            // ToggleScrollingCheckBox
            // 
            this.ToggleScrollingCheckBox.AutoSize = true;
            this.ToggleScrollingCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToggleScrollingCheckBox.Location = new System.Drawing.Point(6, 102);
            this.ToggleScrollingCheckBox.Name = "ToggleScrollingCheckBox";
            this.ToggleScrollingCheckBox.Size = new System.Drawing.Size(122, 17);
            this.ToggleScrollingCheckBox.TabIndex = 10;
            this.ToggleScrollingCheckBox.Text = "Toggle scrolling";
            this.ToggleScrollingCheckBox.UseVisualStyleBackColor = true;
            // 
            // Settings_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 238);
            this.Controls.Add(this.SaveSettingsButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CRLFRadioButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings_Form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.CRLFRadioButton.ResumeLayout(false);
            this.CRLFRadioButton.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox CRLFRadioButton;
        private System.Windows.Forms.CheckBox HexOutputCheckBox;
        private System.Windows.Forms.CheckBox LocalEchoCheckBox;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton LFRadioButton;
        private System.Windows.Forms.RadioButton CRRadioButton;
        private System.Windows.Forms.RadioButton NoneRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.Label FormHeightLabel;
        private System.Windows.Forms.Label FormWidthLabel;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.CheckBox ToggleScrollingCheckBox;
        private System.Windows.Forms.CheckBox WordWrapCheckBox;
    }
}