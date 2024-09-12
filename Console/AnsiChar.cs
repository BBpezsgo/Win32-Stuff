namespace Win32.Console;

public struct AnsiChar :
    IEquatable<AnsiChar>,
    IEquatable<char>
{
    public static AnsiChar Empty => new(' ', CharColor.Black, CharColor.Black);

    public char Char;
    public byte Foreground;
    public byte Background;

    public AnsiChar(char @char)
    {
        Char = @char;
        Foreground = Ansi.ToAnsi256(CharColor.Irgb4bitColors[CharColor.Silver]);
        Background = Ansi.ToAnsi256(CharColor.Irgb4bitColors[CharColor.Black]);
    }

    /// <param name="foreground"> See <see cref="CharColor"/> for values </param>
    /// <param name="background"> See <see cref="CharColor"/> for values </param>
    public AnsiChar(char @char, byte foreground, byte background = CharColor.Black)
    {
        Char = @char;
        Foreground = foreground;
        Background = background;
    }

    public override readonly bool Equals(object? obj) => obj is AnsiChar charInfo && Equals(charInfo);
    public readonly bool Equals(AnsiChar other) => Foreground == other.Foreground && Background == other.Background && Char == other.Char;
    public readonly bool Equals(char other) => Char == other;

    public override readonly int GetHashCode() => HashCode.Combine(Char, Foreground, Background);

    public static bool operator ==(AnsiChar a, AnsiChar b) => a.Equals(b);
    public static bool operator !=(AnsiChar a, AnsiChar b) => !a.Equals(b);

    public static bool operator ==(AnsiChar a, char b) => a.Char == b;
    public static bool operator !=(AnsiChar a, char b) => a.Char != b;

    /// <inheritdoc cref="op_Equality(AnsiChar, char)"/>
    public static bool operator ==(char a, AnsiChar b) => a == b.Char;
    /// <inheritdoc cref="op_Inequality(AnsiChar, char)"/>
    public static bool operator !=(char a, AnsiChar b) => a != b.Char;

    public static explicit operator AnsiChar(char c) => new(c);
    public static implicit operator char(AnsiChar c) => c.Char;

    public static implicit operator AnsiChar(ConsoleChar c) => new(c.Char, CharColor.IrgbToAnsi[c.Foreground], CharColor.IrgbToAnsi[c.Background]);

    public override readonly string ToString() => Char.ToString();
}
