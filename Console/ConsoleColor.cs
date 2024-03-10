using Win32.Gdi32;

namespace Win32.Console;

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

    static readonly GdiColor[] ColorValues = new GdiColor[0b_1_0000]
    {
        new(0, 0, 0),          // 0b_0000
        new(0, 0, 128),        // 0b_0001
        new(0, 128, 0),        // 0b_0010
        new(0, 128, 128),      // 0b_0011
        new(128, 0, 0),        // 0b_0100
        new(128, 0, 128),      // 0b_0101
        new(128, 128, 0),      // 0b_0110
        new(192, 192, 192),    // 0b_0111
        new(128, 128, 128),    // 0b_1000
        new(0, 0, 255),        // 0b_1001
        new(0, 255, 0),        // 0b_1010
        new(0, 255, 255),      // 0b_1011
        new(255, 0, 0),        // 0b_1100
        new(255, 0, 255),      // 0b_1101
        new(255, 255, 0),      // 0b_1110
        new(255, 255, 255),    // 0b_1111
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

    [SuppressMessage("Style", "IDE0230:Use UTF-8 string literal")]
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

    public static GdiColor GetColor(byte color) => ColorValues[color];

    [SuppressMessage("Security", "CA5394")]
    public static byte GetRandomColor()
    {
        Span<byte> result = stackalloc byte[1];
        Random.Shared.NextBytes(result);
        return (byte)(result[0] % 0b_1_0000);
    }

    #region 4bit IRGB

    public static readonly GdiColor[] Irgb4bitColors = new GdiColor[0b_1_0000]
    {
        new(0, 0, 0), // 0b_0000
        new(0, 0, 128), // 0b_0001
        new(0, 128, 0), // 0b_0010
        new(0, 128, 128), // 0b_0011
        new(128, 0, 0), // 0b_0100
        new(128, 0, 128), // 0b_0101
        new(128, 128, 0), // 0b_0110
        new(192, 192, 192), // 0b_0111
        new(128, 128, 128), // 0b_1000
        new(0, 0, 255), // 0b_1001
        new(0, 255, 0), // 0b_1010
        new(0, 255, 255), // 0b_1011
        new(255, 0, 0), // 0b_1100
        new(255, 0, 255), // 0b_1101
        new(255, 255, 0), // 0b_1110
        new(255, 255, 255), // 0b_1111
    };

    public static GdiColor To24bitColor(byte irgb) => Irgb4bitColors[irgb];

    static GdiColor To24bitColor(byte r, byte g, byte b, byte i)
        => Irgb4bitColors[(byte)((i << 3) | (r << 2) | (g << 1) | b)];

    /// <summary>
    /// <para>
    /// Find the closest RGBx approximation of a 24-bit RGB color, for x = 0 or 1
    /// </para>
    /// <para>
    /// Source: <see href="https://stackoverflow.com/questions/41644778/convert-24-bit-color-to-4-bit-rgbi"/>
    /// </para>
    /// </summary>
    static (byte R, byte G, byte B) RgbxApprox(GdiColor color, byte x)
    {
        int threshold = (x + 1) * (byte.MaxValue / 3);
        byte r = color.R > threshold ? (byte)1 : (byte)0;
        byte g = color.G > threshold ? (byte)1 : (byte)0;
        byte b = color.B > threshold ? (byte)1 : (byte)0;
        return (r, g, b);
    }

    /// <summary>
    /// <para>
    /// Find the closest 4-bit RGBI approximation (by Euclidean distance) to a 24-bit RGB color
    /// </para>
    /// <para>
    /// Source: <see href="https://stackoverflow.com/questions/41644778/convert-24-bit-color-to-4-bit-rgbi"/>
    /// </para>
    /// </summary>
    public static byte From24bitColor(ValueTuple<float, float, float> color) => From24bitColor(new GdiColor(color.Item1, color.Item2, color.Item3));

    /// <summary>
    /// <para>
    /// Find the closest 4-bit RGBI approximation (by Euclidean distance) to a 24-bit RGB color
    /// </para>
    /// <para>
    /// Source: <see href="https://stackoverflow.com/questions/41644778/convert-24-bit-color-to-4-bit-rgbi"/>
    /// </para>
    /// </summary>
    public static byte From24bitColor(ValueTuple<int, int, int> color) => From24bitColor(new GdiColor(color.Item1, color.Item2, color.Item3));

    /// <summary>
    /// <para>
    /// Find the closest 4-bit RGBI approximation (by Euclidean distance) to a 24-bit RGB color
    /// </para>
    /// <para>
    /// Source: <see href="https://stackoverflow.com/questions/41644778/convert-24-bit-color-to-4-bit-rgbi"/>
    /// </para>
    /// </summary>
    public static byte From24bitColor(int r, int g, int b) => From24bitColor(new GdiColor(r, g, b));

    /// <summary>
    /// <para>
    /// Find the closest 4-bit RGBI approximation (by Euclidean distance) to a 24-bit RGB color
    /// </para>
    /// <para>
    /// Source: <see href="https://stackoverflow.com/questions/41644778/convert-24-bit-color-to-4-bit-rgbi"/>
    /// </para>
    /// </summary>
    public static byte From24bitColor(GdiColor color)
    {
        // find best RGB0 and RGB1 approximations:
        (byte r0, byte g0, byte b0) = RgbxApprox(color, 0);
        (byte r1, byte g1, byte b1) = RgbxApprox(color, 1);

        // convert them back to 24-bit RGB:
        GdiColor color1 = To24bitColor(r0, g0, b0, 0);
        GdiColor color2 = To24bitColor(r1, g1, b1, 1);

        // return the color closer to the original:
        int d0 = GdiColor.Distance(color, color1);
        int d1 = GdiColor.Distance(color, color2);

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

    static readonly (char Character, float Intensity)[] ShadeCharacters = new (char Character, float Intensity)[]
    {
        ( '░', .25f ),
        ( '▒', .50f ),
        ( '▓', .75f ),
    };

    public static ConsoleChar ToCharacterShaded(GdiColor color)
    {
        const byte DarkValue = (byte)(byte.MaxValue * .125f);
        const byte BrightValue = (byte)(byte.MaxValue * .875f);

        byte shade = Math.Max(color.R, Math.Max(color.G, color.B));

        if (shade < DarkValue)
        { return ConsoleChar.Empty; }

        byte c = CharColor.From24bitColor(color);

        if (shade > BrightValue)
        { return new ConsoleChar(' ', (ushort)(c << 4)); }

        return new ConsoleChar(ShadeCharacters[shade * (ShadeCharacters.Length - 1) / byte.MaxValue].Character, c);
    }

    public static ConsoleChar ToCharacterColored(GdiColor color)
    {
        ConsoleChar result = ConsoleChar.Empty;
        int smallestDist = int.MaxValue;
        GdiColor fgC, bgC;
        int dist;
        float shade;
        byte fg, bg;

        for (fg = 0; fg <= CharColor.White; fg++)
        {
            fgC = CharColor.Irgb4bitColors[fg];

            {
                dist = GdiColor.Distance(fgC, color);
                if (smallestDist > dist)
                {
                    smallestDist = dist;
                    result = new ConsoleChar(' ', 0, fg);
                }
                if (dist <= float.Epsilon) return result;
            }

            for (bg = (byte)(fg + 1); bg <= CharColor.White; bg++)
            {
                bgC = CharColor.Irgb4bitColors[bg];

                for (int i = 0; i < ShadeCharacters.Length; i++)
                {
                    shade = ShadeCharacters[i].Intensity;
                    dist = GdiColor.Distance((fgC * shade) + (bgC * (1f - shade)), color);
                    if (smallestDist > dist)
                    {
                        smallestDist = dist;
                        result = new ConsoleChar(ShadeCharacters[i].Character, fg, bg);
                    }
                    if (dist <= float.Epsilon) return result;
                }
            }
        }

        return result;
    }

    public static GdiColor FromCharacter(ConsoleChar character)
    {
        byte shade = character.Char switch
        {
            '░' => 64,
            '▒' => 127,
            '▓' => 191,
            ' ' => 0,
            '\0' => 0,
            _ => 127,
        };

        GdiColor bg = CharColor.To24bitColor(character.Background);
        GdiColor fg = CharColor.To24bitColor(character.Foreground);

        return (bg * (byte.MaxValue - shade)) + (fg * shade);
    }

    [SuppressMessage("Naming", "CA1707")]
    public static byte To4bitIRGB_BruteForce(GdiColor color, int threshold = 1)
    {
        byte closest = 0b_0000;
        int closestDistance = int.MaxValue;

        for (byte irgb = 0b_0000; irgb <= 0b_1111; irgb++)
        {
            GdiColor rgb = CharColor.To24bitColor(irgb);
            int distance = GdiColor.Distance(rgb, color);

            if (distance <= threshold)
            { return irgb; }

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = irgb;
            }
        }

        return closest;
    }

    #endregion
}
