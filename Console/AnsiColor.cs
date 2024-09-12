using Win32.Gdi32;

namespace Win32.Console;

public enum AnsiColor : int
{
    Black = 0,
    Red = 1,
    Green = 2,
    Yellow = 3,
    Blue = 4,
    Magenta = 5,
    Cyan = 6,
    White = 7,
    BrightBlack = 60,
    BrightRed = 61,
    BrightGreen = 62,
    BrightYellow = 63,
    BrightBlue = 64,
    BrightMagenta = 65,
    BrightCyan = 66,
    BrightWhite = 67,
}

public static class AnsiColorExtensions
{
    public static GdiColor ToGdiColor(this AnsiColor color) => color switch
    {
        AnsiColor.Black => new GdiColor(0, 0, 0),
        AnsiColor.Red => new GdiColor(170, 0, 0),
        AnsiColor.Green => new GdiColor(0, 170, 0),
        AnsiColor.Yellow => new GdiColor(170, 85, 0),
        AnsiColor.Blue => new GdiColor(0, 0, 170),
        AnsiColor.Magenta => new GdiColor(170, 0, 170),
        AnsiColor.Cyan => new GdiColor(0, 170, 170),
        AnsiColor.White => new GdiColor(170, 170, 170),
        AnsiColor.BrightBlack => new GdiColor(85, 85, 85),
        AnsiColor.BrightRed => new GdiColor(255, 85, 85),
        AnsiColor.BrightGreen => new GdiColor(85, 255, 85),
        AnsiColor.BrightYellow => new GdiColor(255, 255, 85),
        AnsiColor.BrightBlue => new GdiColor(85, 85, 255),
        AnsiColor.BrightMagenta => new GdiColor(255, 85, 255),
        AnsiColor.BrightCyan => new GdiColor(85, 255, 255),
        AnsiColor.BrightWhite => new GdiColor(255, 255, 255),
        _ => throw new UnreachableException(),
    };
}
