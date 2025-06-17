using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace _4Term
{
    public partial class Settings_Form : Form
    {
        /*----------------------------------------------------------
         * Class Functions
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] Settings_Form (Constructor)
         * [✓] SaveSettingsButton_Click
         * [✓]
         * [✓] WndProc
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * SerialPortOptionsForm (Constructor)
         * 
         * Initializes serial port configuration form components:
         *---------------------------------------------------------*/
        public Settings_Form()
        {
            InitializeComponent();

                        FormWidthTextBox.Text = Settings.Form.Width.ToString();
            FormHeightTextBox.Text = Settings.Form.Height.ToString();
            WordWrapCheckBox.Checked = Settings.RichTextBox.WordWrap;
            ToggleScrollingCheckBox.Checked = Settings.RichTextBox.Scroll;
            HexOutputCheckBox.Checked = Settings.RichTextBox.HexOutput;
            LocalEchoCheckBox.Checked = Settings.RichTextBox.LocalEcho;
            AdvancedModeCheckBox.Checked = Settings.RichTextBox.AdvancedMode;
        }
        
        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            Settings.Form.Width = int.Parse(FormWidthTextBox.Text);
            Settings.Form.Height = int.Parse(FormHeightTextBox.Text);

                        
            Settings.RichTextBox.WordWrap = WordWrapCheckBox.Checked;
            Settings.RichTextBox.Scroll = ToggleScrollingCheckBox.Checked;
            Settings.RichTextBox.HexOutput = HexOutputCheckBox.Checked;
            Settings.RichTextBox.LocalEcho = LocalEchoCheckBox.Checked;
            Settings.RichTextBox.AdvancedMode = AdvancedModeCheckBox.Checked;

            Settings.WriteXml();
            Close();
        }
        private void FontButton_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.Font = new System.Drawing.Font(
                    new FontFamily(Settings.RichTextBox.Font),
                   Settings.RichTextBox.Size,
                    GetFontStyle(Settings.RichTextBox.FontStyle),
                    GraphicsUnit.Point,
                    (byte)(Settings.RichTextBox.Underline ? 1 : 0),
                    Settings.RichTextBox.Strikeout);
                fontDialog.FontMustExist = true;
                DialogResult result = fontDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Settings.RichTextBox.Font = fontDialog.Font.Name;
                    Settings.RichTextBox.Size = (int)fontDialog.Font.Size;
                    Settings.RichTextBox.FontStyle = GetSimplifiedFontStyle(fontDialog.Font.Style);
                    Settings.RichTextBox.Strikeout = fontDialog.Font.Strikeout;
                    Settings.RichTextBox.Underline = fontDialog.Font.Underline;
                    Settings.WriteXml();
                }
            }
        }
        private FontStyle GetFontStyle(string styleString)
        {
            if (Enum.TryParse<FontStyle>(styleString, out FontStyle result))
            {
                return result;
            }
            return FontStyle.Regular;
        }
        private string GetSimplifiedFontStyle(FontStyle fontStyle)
        {
            if (fontStyle == FontStyle.Regular)
                return "Regular";
            else if (fontStyle == FontStyle.Italic)
                return "Italic";
            else if (fontStyle == FontStyle.Bold)
                return "Bold";
            return "Regular";
        }

        private void BackgroundColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = Color.FromArgb(
                    Settings.Color.Back.R,
                    Settings.Color.Back.G,
                    Settings.Color.Back.B
                );
                DialogResult result = colorDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Settings.Color.Back.R = colorDialog.Color.R;
                    Settings.Color.Back.G = colorDialog.Color.G;
                    Settings.Color.Back.B = colorDialog.Color.B;
                    Settings.WriteXml();
                }
            }
        }
        private void TransmitColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = Color.FromArgb(
                    Settings.Color.Transmit.R,
                    Settings.Color.Transmit.G,
                    Settings.Color.Transmit.B
                );
                DialogResult result = colorDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Settings.Color.Transmit.R = colorDialog.Color.R;
                    Settings.Color.Transmit.G = colorDialog.Color.G;
                    Settings.Color.Transmit.B = colorDialog.Color.B;
                    Settings.WriteXml();
                }
            }
        }
        private void ReceiveColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = Color.FromArgb(
                    Settings.Color.Receive.R,
                    Settings.Color.Receive.G,
                    Settings.Color.Receive.B
                );
                DialogResult result = colorDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Settings.Color.Receive.R = colorDialog.Color.R;
                    Settings.Color.Receive.G = colorDialog.Color.G;
                    Settings.Color.Receive.B = colorDialog.Color.B;
                    Settings.WriteXml();
                }
            }
        }

        /*----------------------------------------------------------
         * WndProc
         * 
         * Blocks right-clicks on title bar/borders.
        *---------------------------------------------------------*/
        protected override void WndProc(ref Message buffer)
        {
            if (buffer.Msg == 0xA3)
            {
                return;
            }
            base.WndProc(ref buffer);
        }
    }
}
