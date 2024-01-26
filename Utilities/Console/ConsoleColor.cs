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

        static readonly System.Drawing.Color[] ColorValues = new System.Drawing.Color[0b_1_0000]
        {
            System.Drawing.Color.FromArgb(0, 0, 0),          // 0b_0000
            System.Drawing.Color.FromArgb(0, 0, 128),        // 0b_0001
            System.Drawing.Color.FromArgb(0, 128, 0),        // 0b_0010
            System.Drawing.Color.FromArgb(0, 128, 128),      // 0b_0011
            System.Drawing.Color.FromArgb(128, 0, 0),        // 0b_0100
            System.Drawing.Color.FromArgb(128, 0, 128),      // 0b_0101
            System.Drawing.Color.FromArgb(128, 128, 0),      // 0b_0110
            System.Drawing.Color.FromArgb(192, 192, 192),    // 0b_0111
            System.Drawing.Color.FromArgb(128, 128, 128),    // 0b_1000
            System.Drawing.Color.FromArgb(0, 0, 255),        // 0b_1001
            System.Drawing.Color.FromArgb(0, 255, 0),        // 0b_1010
            System.Drawing.Color.FromArgb(0, 255, 255),      // 0b_1011
            System.Drawing.Color.FromArgb(255, 0, 0),        // 0b_1100
            System.Drawing.Color.FromArgb(255, 0, 255),      // 0b_1101
            System.Drawing.Color.FromArgb(255, 255, 0),      // 0b_1110
            System.Drawing.Color.FromArgb(255, 255, 255),    // 0b_1111
        };

        static readonly int[] ColorAnsiValues = new int[0b_1_0000]
        {
            Ansi.ForegroundBlack,           // 0b_0000
            Ansi.ForegroundBlue,            // 0b_0001
            Ansi.ForegroundGreen,           // 0b_0010
            Ansi.ForegroundCyan,            // 0b_0011
            Ansi.ForegroundRed,             // 0b_0100
            Ansi.ForegroundMagenta,         // 0b_0101
            Ansi.ForegroundYellow,          // 0b_0110
            Ansi.ForegroundWhite,           // 0b_0111
            Ansi.BrightForegroundBlack,     // 0b_1000
            Ansi.BrightForegroundBlue,      // 0b_1001
            Ansi.BrightForegroundGreen,     // 0b_1010
            Ansi.BrightForegroundCyan,      // 0b_1011
            Ansi.BrightForegroundRed,       // 0b_1100
            Ansi.BrightForegroundMagenta,   // 0b_1101
            Ansi.BrightForegroundYellow,    // 0b_1110
            Ansi.BrightForegroundWhite,     // 0b_1111
        };

        public static int GetAnsiForegroundColor(byte color) => ColorAnsiValues[color];
        public static int GetAnsiBackgroundColor(byte color) => ColorAnsiValues[color] + 10;

        public static System.Drawing.Color GetColor(byte color) => ColorValues[color];

        public static byte GetRandomColor()
        {
            Span<byte> result = stackalloc byte[1];
            Random.Shared.NextBytes(result);
            return (byte)(result[0] % 0b_1_0000);
        }
    }
}
