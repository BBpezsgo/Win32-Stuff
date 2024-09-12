using System.Text;
using Win32.Gdi32;

namespace Win32.Console;

public class AnsiRenderer : BufferedRenderer<AnsiChar>, IOnlySetterRenderer<ConsoleChar>
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
    public AnsiRenderer() : this((short)System.Console.WindowWidth, (short)System.Console.WindowHeight)
    { }

    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    /// <exception cref="System.Security.SecurityException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="PlatformNotSupportedException"/>
    /// <exception cref="WindowsException"/>
    public AnsiRenderer(short bufferWidth, short bufferHeight) : base(bufferWidth, bufferHeight)
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

        byte prevForegroundColor = default;
        byte prevBackgroundColor = default;

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Ansi.FromConsoleChar(
                    Builder,
                    this[x, y],
                    ref prevForegroundColor,
                    ref prevBackgroundColor,
                    x == 0 && y == 0);
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
        { _buffer = new AnsiChar[_width * _height]; }
    }

    public void Set(int i, ConsoleChar pixel) => _buffer[i] = pixel;

    public static void RenderExtended(ReadOnlySpan<GdiColor> buffer, int width, int height)
    {
        StringBuilder builder = new(width * height);
        byte prevColor = default;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int i = (y * width) + x;
                GdiColor color = buffer[i];
                byte bruh = Ansi.ToAnsi256(color.R, color.G, color.B);

                if ((x == 0 && y == 0) || prevColor != bruh)
                {
                    Ansi.SetBackgroundColor(builder, bruh);
                    prevColor = bruh;
                }

                builder.Append(' ');
            }
        }
        System.Console.Out.Write(builder);
        System.Console.SetCursorPosition(0, 0);
    }

    public static void RenderTrueColor(ReadOnlySpan<GdiColor> buffer, int width, int height)
    {
        StringBuilder builder = new(width * height);
        GdiColor prevColor = default;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int i = (y * width) + x;
                GdiColor color = buffer[i];

                if ((x == 0 && y == 0) || prevColor != color)
                {
                    Ansi.SetBackgroundColor(builder, color.R, color.G, color.B);
                    prevColor = color;
                }

                builder.Append(' ');
            }
        }
        System.Console.Out.Write(builder);
        System.Console.SetCursorPosition(0, 0);
    }
}
