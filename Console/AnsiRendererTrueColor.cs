using System.Text;
using Win32.Gdi32;

namespace Win32.Console;

public class AnsiRendererTrueColor : BufferedRenderer<ColoredChar>, IOnlySetterRenderer<AnsiChar>, IOnlySetterRenderer<ConsoleChar>, IOnlySetterRenderer<GdiColor>
{
    public override int Width => BufferWidth;
    public override int Height => BufferHeight;
    public override Span<ColoredChar> Buffer => ConsoleBuffer.AsSpan();

    short BufferWidth;
    short BufferHeight;
    ColoredChar[] ConsoleBuffer;
    readonly StringBuilder Builder;

    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref ColoredChar this[int i] => ref ConsoleBuffer[i];

    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    /// <exception cref="System.Security.SecurityException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="PlatformNotSupportedException"/>
    /// <exception cref="WindowsException"/>
    public AnsiRendererTrueColor() : this((short)System.Console.WindowWidth, (short)System.Console.WindowHeight)
    { }

    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    /// <exception cref="System.Security.SecurityException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="PlatformNotSupportedException"/>
    /// <exception cref="WindowsException"/>
    public AnsiRendererTrueColor(short bufferWidth, short bufferHeight)
    {
        BufferWidth = bufferWidth;
        BufferHeight = bufferHeight;

        ConsoleBuffer = new ColoredChar[BufferWidth * BufferHeight];

        if (OperatingSystem.IsWindows())
        { Ansi.EnableVirtualTerminalSequences(); }
        System.Console.CursorVisible = false;

        Builder = new StringBuilder(BufferWidth * BufferHeight);
    }

    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    /// <exception cref="System.Security.SecurityException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="PlatformNotSupportedException"/>
    /// <exception cref="WindowsException"/>
    public override void Render()
    {
        Builder.Clear();

        GdiColor bg = 0;
        GdiColor fg = 0;

        for (int y = 0; y < BufferHeight; y++)
        {
            for (int x = 0; x < BufferWidth; x++)
            {
                ref ColoredChar c = ref this[x, y];

                if (bg != c.Background)
                {
                    Ansi.SetBackgroundColor(Builder, c.Background);
                    bg = c.Background;
                }

                if (fg != c.Foreground)
                {
                    Ansi.SetForegroundColor(Builder, c.Foreground);
                    fg = c.Foreground;
                }

                Builder.Append(c.Char is '\0' ? ' ' : c.Char);
            }
        }

        Builder.Append(Ansi.Reset);

        System.Console.CursorVisible = false;
        System.Console.SetCursorPosition(0, 0);
        System.Console.Out.Write(Builder);
        System.Console.SetCursorPosition(0, 0);
        System.Console.ResetColor();
    }

    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="PlatformNotSupportedException"/>
    public override void RefreshBufferSize() => RefreshBufferSize(System.Console.WindowWidth, System.Console.WindowHeight);

    public void RefreshBufferSize(int width, int height)
    {
        BufferWidth = (short)width;
        BufferHeight = (short)height;

        if (ConsoleBuffer.Length != BufferWidth * BufferHeight)
        { ConsoleBuffer = new ColoredChar[BufferWidth * BufferHeight]; }
    }

    void IOnlySetterRenderer<ConsoleChar>.Set(int i, ConsoleChar pixel) => ConsoleBuffer[i] = pixel;
    void IOnlySetterRenderer<AnsiChar>.Set(int i, AnsiChar pixel) => ConsoleBuffer[i] = pixel;
    void IOnlySetterRenderer<GdiColor>.Set(int i, GdiColor pixel) => ConsoleBuffer[i] = new ColoredChar(' ', 0, pixel);
}
