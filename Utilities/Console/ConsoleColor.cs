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

        static readonly byte[] AnsiForegroundColorValues = new byte[0b_1_0000]
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

        static readonly byte[] AnsiBackgroundColorValues = new byte[0b_1_0000]
        {
            Ansi.BackgroundBlack,           // 0b_0000
            Ansi.BackgroundBlue,            // 0b_0001
            Ansi.BackgroundGreen,           // 0b_0010
            Ansi.BackgroundCyan,            // 0b_0011
            Ansi.BackgroundRed,             // 0b_0100
            Ansi.BackgroundMagenta,         // 0b_0101
            Ansi.BackgroundYellow,          // 0b_0110
            Ansi.BackgroundWhite,           // 0b_0111
            Ansi.BrightBackgroundBlack,     // 0b_1000
            Ansi.BrightBackgroundBlue,      // 0b_1001
            Ansi.BrightBackgroundGreen,     // 0b_1010
            Ansi.BrightBackgroundCyan,      // 0b_1011
            Ansi.BrightBackgroundRed,       // 0b_1100
            Ansi.BrightBackgroundMagenta,   // 0b_1101
            Ansi.BrightBackgroundYellow,    // 0b_1110
            Ansi.BrightBackgroundWhite,     // 0b_1111
        };

        public static byte GetAnsiForegroundColor(byte color) => AnsiForegroundColorValues[color];
        public static byte GetAnsiBackgroundColor(byte color) => AnsiBackgroundColorValues[color];

        public static System.Drawing.Color GetColor(byte color) => ColorValues[color];

        public static byte GetRandomColor()
        {
            Span<byte> result = stackalloc byte[1];
            Random.Shared.NextBytes(result);
            return (byte)(result[0] % 0b_1_0000);
        }


        #region 4bit IRGB

        public static readonly System.Drawing.Color[] Irgb4bitColors = new System.Drawing.Color[0b_1_0000]
        {
            System.Drawing.Color.FromArgb(0, 0, 0), // 0b_0000
            System.Drawing.Color.FromArgb(0, 0, 128), // 0b_0001
            System.Drawing.Color.FromArgb(0, 128, 0), // 0b_0010
            System.Drawing.Color.FromArgb(0, 128, 128), // 0b_0011
            System.Drawing.Color.FromArgb(128, 0, 0), // 0b_0100
            System.Drawing.Color.FromArgb(128, 0, 128), // 0b_0101
            System.Drawing.Color.FromArgb(128, 128, 0), // 0b_0110
            System.Drawing.Color.FromArgb(192, 192, 192), // 0b_0111
            System.Drawing.Color.FromArgb(128, 128, 128), // 0b_1000
            System.Drawing.Color.FromArgb(0, 0, 255), // 0b_1001
            System.Drawing.Color.FromArgb(0, 255, 0), // 0b_1010
            System.Drawing.Color.FromArgb(0, 255, 255), // 0b_1011
            System.Drawing.Color.FromArgb(255, 0, 0), // 0b_1100
            System.Drawing.Color.FromArgb(255, 0, 255), // 0b_1101
            System.Drawing.Color.FromArgb(255, 255, 0), // 0b_1110
            System.Drawing.Color.FromArgb(255, 255, 255), // 0b_1111
        };

        public static System.Drawing.Color From4bitIRGB(byte irgb) => Irgb4bitColors[irgb];

        public static System.Drawing.Color From4bitIRGB(byte r, byte g, byte b, byte i)
            => From4bitIRGB((byte)((i << 3) | (r << 2) | (g << 1) | (b)));

        static int ColorDistance(System.Drawing.Color a, System.Drawing.Color b)
        {
            (int R, int G, int B) d = (a.R - b.R, a.G - b.G, a.B - b.B);
            return (d.R * d.R) + (d.G * d.G) + (d.B * d.B);
        }

        /// <summary>
        /// <para>
        /// Find the closest 4-bit RGBI approximation (by Euclidean distance) to a 24-bit RGB color
        /// </para>
        /// <para>
        /// Source: <see href="https://stackoverflow.com/questions/41644778/convert-24-bit-color-to-4-bit-rgbi"/>
        /// </para>
        /// </summary>
        public static byte To4bitIRGB(System.Drawing.Color color)
        {
            /// <summary>
            /// Find the closest RGBx approximation of a 24-bit RGB color, for x = 0 or 1
            /// </summary>
            static (byte R, byte G, byte B) RgbxApprox(System.Drawing.Color color, byte x)
            {
                int threshold = ((x + 1) * byte.MaxValue) / 3;
                byte r = color.R > threshold ? (byte)1 : (byte)0;
                byte g = color.G > threshold ? (byte)1 : (byte)0;
                byte b = color.B > threshold ? (byte)1 : (byte)0;
                return (r, g, b);
            }

            // find best RGB0 and RGB1 approximations:
            (byte r0, byte g0, byte b0) = RgbxApprox(color, 0);
            (byte r1, byte g1, byte b1) = RgbxApprox(color, 1);

            // convert them back to 24-bit RGB:
            System.Drawing.Color color1 = From4bitIRGB(r0, g0, b0, 0);
            System.Drawing.Color color2 = From4bitIRGB(r1, g1, b1, 1);

            // return the color closer to the original:
            int d0 = ColorDistance(color, color1);
            int d1 = ColorDistance(color, color2);

            byte result = 0b_0000;

            if (d0 <= d1)
            {
                result |= 0b_0000;
                if (r0 != 0)
                { result |= 0b_0100; }
                if (g0 != 0)
                { result |= 0b_0010; }
                if (b0 != 0)
                { result |= 0b_0001; }
            }
            else
            {
                result |= 0b_1000;
                if (r1 != 0)
                { result |= 0b_0100; }
                if (g1 != 0)
                { result |= 0b_0010; }
                if (b1 != 0)
                { result |= 0b_0001; }
            }

            return result;
        }

        #endregion
    }
}
