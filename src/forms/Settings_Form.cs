using System;
using System.Windows.Forms;
using System.Drawing;

namespace _4Term
{
    public partial class Settings_Form : Form
    {
        public Settings_Form()
        {
            InitializeComponent();
            switch (Settings.Port.AppendToSend)
            {
                case Settings.Port.AppendType.AppendNothing:
                    NoneRadioButton.Checked = true;
                    break;
                case Settings.Port.AppendType.AppendCR:
                    CRRadioButton.Checked = true;
                    break;
                case Settings.Port.AppendType.AppendLF:
                    LFRadioButton.Checked = true;
                    break;
                case Settings.Port.AppendType.AppendCRLF:
                    radioButton4.Checked = true;
                    break;
            }
            FormWidthTextBox.Text = Settings.Form.Width.ToString();
            FormHeightTextBox.Text = Settings.Form.Height.ToString();
            WordWrapCheckBox.Checked = Settings.RichTextBox.WordWrap;
            ToggleScrollingCheckBox.Checked = Settings.RichTextBox.Scroll;
            HexOutputCheckBox.Checked = Settings.RichTextBox.HexOutput;
            LocalEchoCheckBox.Checked = Settings.RichTextBox.LocalEcho;                 
        }
        
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (CRRadioButton.Checked)
                Settings.Port.AppendToSend = Settings.Port.AppendType.AppendCR;
            else if (LFRadioButton.Checked)
                Settings.Port.AppendToSend = Settings.Port.AppendType.AppendLF;
            else if (radioButton4.Checked)
                Settings.Port.AppendToSend = Settings.Port.AppendType.AppendCRLF;
            else
                Settings.Port.AppendToSend = Settings.Port.AppendType.AppendNothing;

            Settings.Form.Width = int.Parse(FormWidthTextBox.Text);
            Settings.Form.Height = int.Parse(FormHeightTextBox.Text);
            Settings.RichTextBox.WordWrap = WordWrapCheckBox.Checked;
            Settings.RichTextBox.Scroll = ToggleScrollingCheckBox.Checked;
            Settings.RichTextBox.HexOutput = HexOutputCheckBox.Checked;
            Settings.RichTextBox.LocalEcho = LocalEchoCheckBox.Checked;
      
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
                    Settings.BackColor.R,
                    Settings.BackColor.G,
                    Settings.BackColor.B
                );
                DialogResult result = colorDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Settings.BackColor.R = colorDialog.Color.R;
                    Settings.BackColor.G = colorDialog.Color.G;
                    Settings.BackColor.B = colorDialog.Color.B;
                    Settings.WriteXml();
                }
            }
        }
        private void TransmitColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = Color.FromArgb(
                    Settings.TransmitColor.R,
                    Settings.TransmitColor.G,
                    Settings.TransmitColor.B
                );
                DialogResult result = colorDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Settings.TransmitColor.R = colorDialog.Color.R;
                    Settings.TransmitColor.G = colorDialog.Color.G;
                    Settings.TransmitColor.B = colorDialog.Color.B;
                    Settings.WriteXml();
                }
            }
        }
        private void ReceiveColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = Color.FromArgb(
                    Settings.ReceiveColor.R,
                    Settings.ReceiveColor.G,
                    Settings.ReceiveColor.B
                );
                DialogResult result = colorDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Settings.ReceiveColor.R = colorDialog.Color.R;
                    Settings.ReceiveColor.G = colorDialog.Color.G;
                    Settings.ReceiveColor.B = colorDialog.Color.B;
                    Settings.WriteXml();
                }
            }
        }
        
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
