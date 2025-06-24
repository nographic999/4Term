using System;
using System.IO.Ports;

namespace _4Term
{
    /*----------------------------------------------------------
     * Class variables
     * -------------------------------------------------------*/
    public class Settings
    {
        public class Form
        {
            public static int Width = 718;
            public static int Height = 525;
            public static bool AdvancedMode = false;
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

        public class Color
        {
            public static (int R, int G, int B) Back = (0, 0, 0);
            public static (int R, int G, int B) Transmit = (255, 165, 0);
            public static (int R, int G, int B) Receive = (0, 250, 154);
        }      

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

            public static AppendType Append = AppendType.AppendCR;
        }

        public class Page
        {
            public static string[] Text = new string[]
            {
                /* pages 0-7 */
                "page0", "page1", "page2", "page3", "page4", "page5", "page6", "page7",

                /* pages 8-15 */
                "page8", "page9", "page10","page11", "page12", "page13", "page14", "page15",

                /* pages 16-23 */
                "page16", "page17", "page18", "page19", "page20", "page21", "page22", "page23",

                /* pages 24-31 */
                "page24", "page25", "page26", "page27", "page28", "page29", "page30", "page31"
            };
        }

        public class MacroText
        {
            public static string[] Text = new string[]
            {
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page0  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page1  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page2  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page3  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page4  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page5  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page6  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page7  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page8  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page9  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page10 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page11 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page12 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page13 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page14 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page15 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page16 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page17 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page18 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page19 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page20 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page21 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page22 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page23 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page24 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page25 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page26 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page27 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page28 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page29 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page30 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""      /* page31 */
            };
        }

        public class MacroCommand
        {
            public static string[] Text = new string[]
            {
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page0  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page1  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page2  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page3  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page4  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page5  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page6  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page7  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page8  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page9  */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page10 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page11 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page12 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page13 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page14 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page15 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page16 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page17 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page18 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page19 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page20 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page21 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page22 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page23 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page24 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page25 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page26 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page27 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page28 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page29 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",     /* page30 */
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""      /* page31 */
            };
        }

        /*----------------------------------------------------------
         * Class Functions
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         * 
         * [✓] ReadXml
         * [✓] WriteXml
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ReadXml
         * 
         * Loads application settings from the XML configuration file.
         * Initializes UI dimensions, font styles, serial port settings,
         * color themes, and macro definitions.
         * Returns immediately if the file cannot be loaded.
         *---------------------------------------------------------*/
        public static void ReadXml()
        {
            /* Load xml file */
            if (!Xml.LoadFileQ())
            {
                if (!Xml.CreateNewQ())
                {
                    Environment.Exit(1);
                }
                WriteXml();
                Xml.LoadFileQ();
            }

            /* Form section */
            Form.Width = Xml.ReadIntParmQ("Form/Width");
            Form.Height = Xml.ReadIntParmQ("Form/Height");
            Form.AdvancedMode = Xml.ReadBoolParmQ("Form/AdvancedMode");

            /* RichTextBox section */
            RichTextBox.Font = Xml.ReadStringParmQ("RichTextBox/Font");
            RichTextBox.FontStyle = Xml.ReadStringParmQ("RichTextBox/FontStyle");
            RichTextBox.Size = Xml.ReadIntParmQ("RichTextBox/Size");
            RichTextBox.Strikeout = Xml.ReadBoolParmQ("RichTextBox/Strikeout");
            RichTextBox.Underline = Xml.ReadBoolParmQ("RichTextBox/Underline");
            RichTextBox.WordWrap = Xml.ReadBoolParmQ("RichTextBox/WordWrap");
            RichTextBox.Scroll = Xml.ReadBoolParmQ("RichTextBox/Scroll");
            RichTextBox.HexOutput = Xml.ReadBoolParmQ("RichTextBox/HexOutput");
            RichTextBox.LocalEcho = Xml.ReadBoolParmQ("RichTextBox/LocalEcho");            

            /* Color section */
            Color.Back.R = Xml.ReadIntParmQ("Back/R");
            Color.Back.G = Xml.ReadIntParmQ("Back/G");
            Color.Back.B = Xml.ReadIntParmQ("Back/B");
            Color.Transmit.R = Xml.ReadIntParmQ("Transmit/R");
            Color.Transmit.G = Xml.ReadIntParmQ("Transmit/G");
            Color.Transmit.B = Xml.ReadIntParmQ("Transmit/B");
            Color.Receive.R = Xml.ReadIntParmQ("Receive/R");
            Color.Receive.G = Xml.ReadIntParmQ("Receive/G");
            Color.Receive.B = Xml.ReadIntParmQ("Receive/B");

            /* Port section */
            Port.PortName = Xml.ReadStringParmQ("Port/PortName");
            Port.BaudRate = Xml.ReadIntParmQ("Port/BaudRate");
            Port.DataBits = Xml.ReadIntParmQ("Port/DataBits");
            Port.Parity = (Parity)Enum.Parse(typeof(Parity), Xml.ReadStringParmQ("Port/Parity"));
            Port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Xml.ReadStringParmQ("Port/StopBits"));
            Port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Xml.ReadStringParmQ("Port/Handshake"));
            Port.Append = (Port.AppendType)Enum.Parse(typeof(Port.AppendType), Xml.ReadStringParmQ("Port/AppendToSend"));
            Port.Dtr = Xml.ReadBoolParmQ("Port/Dtr");
            Port.Rts = Xml.ReadBoolParmQ("Port/Rts");

            /* Page section */
            for (int i = 0; i < Page.Text.Length; i++)
            {
                Page.Text[i] = Xml.ReadStringParmQ($"Page/Item{i}");
            }

            /* MacroText section */
            for (int i = 0; i < MacroText.Text.Length; i++)
            {
                MacroText.Text[i] = Xml.ReadStringParmQ($"MacroText/Item{i}");
            }

            /* MacroCommand section */
            for (int i = 0; i < MacroCommand.Text.Length; i++)
            {
                MacroCommand.Text[i] = Xml.ReadStringParmQ($"MacroCommand/Item{i}");
            }
        }

        /*----------------------------------------------------------
         * WriteXml
         * 
         * Saves current application settings to the XML configuration file.
         * Persists UI dimensions, font styles, color settings, port configurations,
         * and all macro-related text and commands.
         * Returns immediately if the file cannot be loaded.
         *---------------------------------------------------------*/
        public static void WriteXml()
        {
            /* Load xml file */
            if (!Xml.LoadFileQ())
                return;

            /* Form section */
            Xml.WriteIntParmQ("Form/Width", Form.Width);
            Xml.WriteIntParmQ("Form/Height", Form.Height);
            Xml.WriteBoolParmQ("Form/AdvancedMode", Form.AdvancedMode);

            /* RichTextBox section */
            Xml.WriteStringParmQ("RichTextBox/Font", RichTextBox.Font);
            Xml.WriteStringParmQ("RichTextBox/FontStyle", RichTextBox.FontStyle);
            Xml.WriteIntParmQ("RichTextBox/Size", RichTextBox.Size);
            Xml.WriteBoolParmQ("RichTextBox/Strikeout", RichTextBox.Strikeout);
            Xml.WriteBoolParmQ("RichTextBox/Underline", RichTextBox.Underline);
            Xml.WriteBoolParmQ("RichTextBox/WordWrap", RichTextBox.WordWrap);
            Xml.WriteBoolParmQ("RichTextBox/Scroll", RichTextBox.Scroll);
            Xml.WriteBoolParmQ("RichTextBox/HexOutput", RichTextBox.HexOutput);
            Xml.WriteBoolParmQ("RichTextBox/LocalEcho", RichTextBox.LocalEcho);
            
            /* Color section */
            Xml.WriteIntParmQ("Back/R", Color.Back.R);
            Xml.WriteIntParmQ("Back/G", Color.Back.G);
            Xml.WriteIntParmQ("Back/B", Color.Back.B);
            Xml.WriteIntParmQ("Transmit/R", Color.Transmit.R);
            Xml.WriteIntParmQ("Transmit/G", Color.Transmit.G);
            Xml.WriteIntParmQ("Transmit/B", Color.Transmit.B);
            Xml.WriteIntParmQ("Receive/R", Color.Receive.R);
            Xml.WriteIntParmQ("Receive/G", Color.Receive.G);
            Xml.WriteIntParmQ("Receive/B", Color.Receive.B);

            /* Port section */
            Xml.WriteStringParmQ("Port/PortName", Port.PortName);
            Xml.WriteIntParmQ("Port/BaudRate", Port.BaudRate);
            Xml.WriteIntParmQ("Port/DataBits", Port.DataBits);
            Xml.WriteStringParmQ("Port/Parity", Port.Parity.ToString());
            Xml.WriteStringParmQ("Port/StopBits", Port.StopBits.ToString());
            Xml.WriteStringParmQ("Port/Handshake", Port.Handshake.ToString());
            Xml.WriteStringParmQ("Port/AppendToSend", Port.Append.ToString());
            Xml.WriteBoolParmQ("Port/Dtr", Port.Dtr);
            Xml.WriteBoolParmQ("Port/Rts", Port.Rts);

            /* Page section */
            for (int i = 0; i < Page.Text.Length; i++)
            {
                Xml.WriteStringParmQ($"Page/Item{i}", Page.Text[i]);
            }

            /* MacroText section */
            for (int i = 0; i < MacroText.Text.Length; i++)
            {
                Xml.WriteStringParmQ($"MacroText/Item{i}", MacroText.Text[i]);
            }

            /* MacroCommand section */
            for (int i = 0; i < MacroCommand.Text.Length; i++)
            {
                Xml.WriteStringParmQ($"MacroCommand/Item{i}", MacroCommand.Text[i]);
            }

            /* Save and close */
            Xml.CloseFileQ();
        }
    }
}
