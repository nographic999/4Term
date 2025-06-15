using System;
using System.IO.Ports;

namespace _4Term
{
    /*----------------------------------------------------------
     * Class variables
     * -------------------------------------------------------*/
    public class Settings
    {
        public static string XmlName = "config.xml";

        public class Form
        {
            public static int Width = 718;
            public static int Height = 525;
        }

        public class RichTextBox
        {
            public static string Font = "Consolas";
            public static string FontStyle = "Regular";
            public static int Size = 10;
            public static bool Strikeout = false;
            public static bool Underline = false;
            public static bool WordWrap = true;
            public static bool Scroll = true;
            public static bool HexOutput = false;
            public static bool LocalEcho = true;
        }

        public static (int R, int G, int B) BackColor = (0, 0, 0);
        public static (int R, int G, int B) TransmitColor = (255, 165, 0);
        public static (int R, int G, int B) ReceiveColor = (0, 250, 154);

        public class Port
        {      
            public static string PortName = "COM1";
            public static int BaudRate = 9600;
            public static int DataBits = 8;
            public static System.IO.Ports.Parity Parity = System.IO.Ports.Parity.None;
            public static System.IO.Ports.StopBits StopBits = System.IO.Ports.StopBits.One;
            public static System.IO.Ports.Handshake Handshake = System.IO.Ports.Handshake.None;
            public static bool Dtr = false;
            public static bool Rts = false;
            public enum AppendType
            {
                AppendNothing,
                AppendCR,
                AppendLF,
                AppendCRLF
            }
            public static AppendType AppendToSend = AppendType.AppendCR;
        }

        public class Page
        {
            /* RadioButton text */
            public static string[] Text = new string[]
            {
                /* RadioButton text group 0 (pages 0-7) */
                "page0", "page1", "page2", "page3", "page4", "page5", "page6", "page7",

                /* RadioButton text group 1 (pages 8-15) */
                "page8", "page9", "page10","page11", "page12", "page13", "page14", "page15",

                /* RadioButton text group 2 (pages 16-23) */
                "page16", "page17", "page18", "page19", "page20", "page21", "page22", "page23",

                /* RadioButton text group 3 (pages 24-31) */
                "page24", "page25", "page26", "page27", "page28", "page29", "page30", "page31"
            };
        }

        public class MacroText
        {
            public static string Section = "Text";
            public static string[] Text = new string[]
            {
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""
            };
        }
        public class MacroCommand
        {
            public static string Section = "Command";
            public static string[] Text = new string[]
            {
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""
            };
        }
        public static void ReadXml()
        {
            var path = "config.xml";
            if (!System.IO.File.Exists(path))
                return;

            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(path);

            var root = xmlDoc.DocumentElement; // <Settings>

            Form.Width = int.Parse(root.SelectSingleNode("Form/Width").InnerText);
            Form.Height = int.Parse(root.SelectSingleNode("Form/Height").InnerText);

            RichTextBox.Font = root.SelectSingleNode("RichTextBox/Font").InnerText;
            RichTextBox.FontStyle = root.SelectSingleNode("RichTextBox/FontStyle").InnerText;
            RichTextBox.Size = int.Parse(root.SelectSingleNode("RichTextBox/Size").InnerText);
            RichTextBox.Strikeout = bool.Parse(root.SelectSingleNode("RichTextBox/Strikeout").InnerText);
            RichTextBox.Underline = bool.Parse(root.SelectSingleNode("RichTextBox/Underline").InnerText);
            RichTextBox.WordWrap = bool.Parse(root.SelectSingleNode("RichTextBox/WordWrap").InnerText);
            RichTextBox.Scroll = bool.Parse(root.SelectSingleNode("RichTextBox/Scroll").InnerText);
            RichTextBox.HexOutput = bool.Parse(root.SelectSingleNode("RichTextBox/HexOutput").InnerText);
            RichTextBox.LocalEcho = bool.Parse(root.SelectSingleNode("RichTextBox/LocalEcho").InnerText);

            BackColor.R = int.Parse(root.SelectSingleNode("BackColor/R").InnerText);
            BackColor.G = int.Parse(root.SelectSingleNode("BackColor/G").InnerText);
            BackColor.B = int.Parse(root.SelectSingleNode("BackColor/B").InnerText);

            TransmitColor.R = int.Parse(root.SelectSingleNode("TransmitColor/R").InnerText);
            TransmitColor.G = int.Parse(root.SelectSingleNode("TransmitColor/G").InnerText);
            TransmitColor.B = int.Parse(root.SelectSingleNode("TransmitColor/B").InnerText);

            ReceiveColor.R = int.Parse(root.SelectSingleNode("ReceiveColor/R").InnerText);
            ReceiveColor.G = int.Parse(root.SelectSingleNode("ReceiveColor/G").InnerText);
            ReceiveColor.B = int.Parse(root.SelectSingleNode("ReceiveColor/B").InnerText);

            Port.PortName = root.SelectSingleNode("Port/PortName").InnerText;
            Port.BaudRate = int.Parse(root.SelectSingleNode("Port/BaudRate").InnerText);
            Port.DataBits = int.Parse(root.SelectSingleNode("Port/DataBits").InnerText);
            Port.Parity = (Parity)Enum.Parse(typeof(Parity), root.SelectSingleNode("Port/Parity").InnerText);
            Port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), root.SelectSingleNode("Port/StopBits").InnerText);
            Port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), root.SelectSingleNode("Port/Handshake").InnerText);
            Port.AppendToSend = (Port.AppendType)Enum.Parse(typeof(Port.AppendType), root.SelectSingleNode("Port/AppendToSend").InnerText);


            var pageNodes = root.SelectNodes("Page/*");
            if (pageNodes != null)
            {
                for (int i = 0; i < pageNodes.Count && i < Page.Text.Length; i++)
                {
                    Page.Text[i] = pageNodes[i].InnerText;
                }
            }

            var macroTextNodes = root.SelectNodes("MacroText/*");
            if (macroTextNodes != null)
            {
                for (int i = 0; i < macroTextNodes.Count && i < MacroText.Text.Length; i++)
                {
                    MacroText.Text[i] = macroTextNodes[i].InnerText;
                }
            }

            var macroCommandNodes = root.SelectNodes("MacroCommand/*");
            if (macroCommandNodes != null)
            {
                for (int i = 0; i < macroCommandNodes.Count && i < MacroCommand.Text.Length; i++)
                {
                    MacroCommand.Text[i] = macroCommandNodes[i].InnerText;
                }
            }
        }

        public static void WriteXml()
        {
            var xmlDoc = new System.Xml.XmlDocument();

            var declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(declaration);

            var root = xmlDoc.CreateElement("Settings");
            xmlDoc.AppendChild(root);

            // Helper method to create sections and add key/value
            void AddValue(string sectionName, string key, string value)
            {
                var section = root.SelectSingleNode(sectionName) ?? xmlDoc.CreateElement(sectionName);
                if (section.ParentNode == null)
                    root.AppendChild(section);

                var elem = xmlDoc.CreateElement(key);
                elem.InnerText = value;
                section.AppendChild(elem);
            }

            // Form section
            AddValue("Form", "Width", Form.Width.ToString());
            AddValue("Form", "Height", Form.Height.ToString());

            // RichTextBox section
            AddValue("RichTextBox", "Font", RichTextBox.Font);
            AddValue("RichTextBox", "FontStyle", RichTextBox.FontStyle);
            AddValue("RichTextBox", "Size", RichTextBox.Size.ToString());
            AddValue("RichTextBox", "Strikeout", RichTextBox.Strikeout.ToString());
            AddValue("RichTextBox", "Underline", RichTextBox.Underline.ToString());
            AddValue("RichTextBox", "WordWrap", RichTextBox.WordWrap.ToString());
            AddValue("RichTextBox", "Scroll", RichTextBox.Scroll.ToString());
            AddValue("RichTextBox", "HexOutput", RichTextBox.HexOutput.ToString());
            AddValue("RichTextBox", "LocalEcho", RichTextBox.LocalEcho.ToString());

            // BackColor
            AddValue("BackColor", "R", BackColor.R.ToString());
            AddValue("BackColor", "G", BackColor.G.ToString());
            AddValue("BackColor", "B", BackColor.B.ToString());

            // TransmitColor
            AddValue("TransmitColor", "R", TransmitColor.R.ToString());
            AddValue("TransmitColor", "G", TransmitColor.G.ToString());
            AddValue("TransmitColor", "B", TransmitColor.B.ToString());

            // ReceiveColor
            AddValue("ReceiveColor", "R", ReceiveColor.R.ToString());
            AddValue("ReceiveColor", "G", ReceiveColor.G.ToString());
            AddValue("ReceiveColor", "B", ReceiveColor.B.ToString());

            // Port section
            AddValue("Port", "PortName", Port.PortName);
            AddValue("Port", "BaudRate", Port.BaudRate.ToString());
            AddValue("Port", "DataBits", Port.DataBits.ToString());
            AddValue("Port", "Parity", Port.Parity.ToString());
            AddValue("Port", "StopBits", Port.StopBits.ToString());
            AddValue("Port", "Handshake", Port.Handshake.ToString());
            AddValue("Port", "AppendToSend", Port.AppendToSend.ToString());
            AddValue("Port", "Dtr", Port.Dtr.ToString());
            AddValue("Port", "Rts", Port.Rts.ToString());

            // Page section
            var pageElement = xmlDoc.CreateElement("Page");
            for (int i = 0; i <= 31; i++)
            {
                var textElem = xmlDoc.CreateElement($"Item{i}");
                textElem.InnerText = Page.Text[i];
                pageElement.AppendChild(textElem);
            }
            root.AppendChild(pageElement);

            // MacroText section
            var macroTextElement = xmlDoc.CreateElement("MacroText");
            for (int i = 0; i <= 511; i++)
            {
                var textElem = xmlDoc.CreateElement($"Item{i}");
                textElem.InnerText = MacroText.Text[i];
                macroTextElement.AppendChild(textElem);
            }
            root.AppendChild(macroTextElement);

            // MacroCommand section
            var macroCommandElement = xmlDoc.CreateElement("MacroCommand");
            for (int i = 0; i <= 511; i++)
            {
                var textElem = xmlDoc.CreateElement($"Item{i}");
                textElem.InnerText = MacroCommand.Text[i];
                macroCommandElement.AppendChild(textElem);
            }
            root.AppendChild(macroCommandElement);

            // Save to file
            xmlDoc.Save("config.xml");
        }
    }
}
