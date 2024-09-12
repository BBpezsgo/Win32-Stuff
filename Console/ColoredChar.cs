using Win32.Gdi32;

namespace Win32.Console;

public struct ColoredChar :
    IEquatable<ColoredChar>,
    IEquatable<char>
{
    public static ColoredChar Empty => new(' ', CharColor.Black, CharColor.Black);

    public char Char;
    public GdiColor Foreground;
    public GdiColor Background;

    public ColoredChar(char @char)
    {
        Char = @char;
        Foreground = CharColor.Irgb4bitColors[CharColor.Silver];
        Background = CharColor.Irgb4bitColors[CharColor.Black];
    }

    /// <param name="foreground"> See <see cref="CharColor"/> for values </param>
    /// <param name="background"> See <see cref="CharColor"/> for values </param>
    public ColoredChar(char @char, byte foreground, byte background = CharColor.Black)
    {
        Char = @char;
        Foreground = CharColor.Irgb4bitColors[foreground];
        Background = CharColor.Irgb4bitColors[background];
    }

    public ColoredChar(char @char, GdiColor foreground, GdiColor background = default)
    {
        Char = @char;
        Foreground = foreground;
        Background = background;
    }

    public override readonly bool Equals(object? obj) => obj is ColoredChar charInfo && Equals(charInfo);
    public readonly bool Equals(ColoredChar other) => Foreground == other.Foreground && Background == other.Background && Char == other.Char;
    public readonly bool Equals(char other) => Char == other;

    public override readonly int GetHashCode() => HashCode.Combine(Char, Foreground, Background);

    public static bool operator ==(ColoredChar a, ColoredChar b) => a.Equals(b);
    public static bool operator !=(ColoredChar a, ColoredChar b) => !a.Equals(b);

    public static bool operator ==(ColoredChar a, char b) => a.Char == b;
    public static bool operator !=(ColoredChar a, char b) => a.Char != b;

    /// <inheritdoc cref="op_Equality(ColoredChar, char)"/>
    public static bool operator ==(char a, ColoredChar b) => a == b.Char;
    /// <inheritdoc cref="op_Inequality(ColoredChar, char)"/>
    public static bool operator !=(char a, ColoredChar b) => a != b.Char;

    public static explicit operator ColoredChar(char c) => new(c);
    public static implicit operator char(ColoredChar c) => c.Char;

    public static implicit operator ColoredChar(ConsoleChar c) => new(c.Char, CharColor.Irgb4bitColors[c.Foreground], CharColor.Irgb4bitColors[c.Background]);
    public static implicit operator ColoredChar(AnsiChar c) => new(c.Char, Ansi.FromAnsi256(c.Foreground), Ansi.FromAnsi256(c.Background));

    public override readonly string ToString() => Char.ToString();
}
