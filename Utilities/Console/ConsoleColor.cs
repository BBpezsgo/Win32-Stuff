using System.Runtime.CompilerServices;

namespace Win32
{
    /// <summary>
    /// Representation:<br/>
    /// <c>BBBBFFFF</c>
    /// </summary>
    public struct CharColor
    {
        public const byte Red = 0b_0100;
        public const byte Green = 0b_0010;
        public const byte Blue = 0b_0001;
        public const byte Yellow = 0b_0110;
        public const byte Cyan = 0b_0011;
        public const byte Magenta = 0b_0101;

        public const byte BrightRed = 0b_1100;
        public const byte BrightGreen = 0b_1010;
        public const byte BrightBlue = 0b_1001;
        public const byte BrightYellow = 0b_1110;
        public const byte BrightCyan = 0b_1011;
        public const byte BrightMagenta = 0b_1101;

        public const byte Black = 0b_0000;
        public const byte Silver = 0b_0111;
        public const byte Gray = 0b_1000;
        public const byte White = 0b_1111;

        internal const WORD MASK_FG = 0b_0000_1111;
        internal const WORD MASK_BG = 0b_1111_0000;
        internal const WORD MASK_COLOR = 0b_1111_1111;

        public static WORD Make(byte background, byte foreground) => unchecked((WORD)((foreground & MASK_FG) | ((background << 4) & MASK_BG)));

        public static byte Invert(byte color) => color switch
        {
            CharColor.Red => CharColor.BrightCyan,
            CharColor.Green => CharColor.BrightMagenta,
            CharColor.Blue => CharColor.BrightYellow,
            CharColor.Yellow => CharColor.BrightBlue,
            CharColor.Cyan => CharColor.BrightRed,
            CharColor.Magenta => CharColor.BrightGreen,
            CharColor.BrightRed => CharColor.Cyan,
            CharColor.BrightGreen => CharColor.Magenta,
            CharColor.BrightBlue => CharColor.Yellow,
            CharColor.BrightYellow => CharColor.Blue,
            CharColor.BrightCyan => CharColor.Red,
            CharColor.BrightMagenta => CharColor.Green,
            CharColor.Black => CharColor.White,
            CharColor.Silver => CharColor.Gray,
            CharColor.Gray => CharColor.Silver,
            CharColor.White => CharColor.Black,
            _ => 0,
        };
    }
}
