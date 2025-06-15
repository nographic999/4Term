using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace _4Term
{
    public class cFont
    {
        public static Font ParseFontString(string fontString, bool underline, bool strikeout, ref string iferror)
        {
            string[] components = fontString.Split('-');
            if (components.Length == 3)
            {
                string fontName = components[0];
                float size = float.Parse(components[1]);
                FontStyle style = ParseFontStyle(components[2], ref iferror);
                if (underline)
                    style |= FontStyle.Underline;

                if (strikeout)
                    style |= FontStyle.Strikeout;
                return new Font(fontName, size, style);
            }
            return null;
        }
        static FontStyle ParseFontStyle(string styleString, ref string iferror)
        {
            switch (styleString.ToLower())
            {
                case "regular":
                    return FontStyle.Regular;
                case "bold":
                    return FontStyle.Bold;
                case "italic":
                    return FontStyle.Italic;
                default:
                    iferror = "Unknown font style: " + styleString;
                    return FontStyle.Regular;
            }
        }
    }
}
