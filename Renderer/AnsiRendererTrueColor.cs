using System.Text;
using Win32.Gdi32;

namespace Win32.Console;

public class AnsiRendererTrueColor : BufferedRenderer<ColoredChar>, IOnlySetterRenderer<AnsiChar>, IOnlySetterRenderer<ConsoleChar>, IOnlySetterRenderer<GdiColor>
{
    readonly StringBuilder Builder;

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
    public AnsiRendererTrueColor(short bufferWidth, short bufferHeight) : base(bufferWidth, bufferHeight)
    {
        if (OperatingSystem.IsWindows())
        { Ansi.EnableVirtualTerminalSequences(); }
        System.Console.CursorVisible = false;

        Builder = new StringBuilder(_width * _height);
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

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
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
        _width = (short)width;
        _height = (short)height;

        if (_buffer.Length != _width * _height)
        { _buffer = new ColoredChar[_width * _height]; }
    }

    void IOnlySetterRenderer<ConsoleChar>.Set(int i, ConsoleChar pixel) => _buffer[i] = pixel;
    void IOnlySetterRenderer<AnsiChar>.Set(int i, AnsiChar pixel) => _buffer[i] = pixel;
    void IOnlySetterRenderer<GdiColor>.Set(int i, GdiColor pixel) => _buffer[i] = new ColoredChar(' ', 0, pixel);
}
