using System.Linq;

namespace Text_parser
{
    public static class CharExtension
    {
        private static ConnectingLine[] s_wordConnectors = new ConnectingLine[] 
        {   ConnectingLine.MinusFromKeyboard, 
            ConnectingLine.Minus, 
            ConnectingLine.Dash, 
            ConnectingLine.ShortDash 
        };

        public static bool IsDash(this char c) => s_wordConnectors.Any(s =>
        {
            char line = (char)s;
            return line.Equals(c);
        });

    }
}
