using System.Text;
using Win32.Gdi32;

namespace Win32.Console;

public class AnsiRendererHD : BufferedRenderer<GdiColor>, IOnlySetterRenderer<ConsoleChar>
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
    public AnsiRendererHD() : this((short)System.Console.WindowWidth, (short)System.Console.WindowHeight)
    { }

    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    /// <exception cref="System.Security.SecurityException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="PlatformNotSupportedException"/>
    /// <exception cref="WindowsException"/>
    public AnsiRendererHD(short bufferWidth, short bufferHeight) : base(bufferWidth, bufferHeight)
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

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Ansi.SetBackgroundColor(
                    Builder,
                    this[x, y]);
                Builder.Append(' ');
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
        { _buffer = new GdiColor[_width * _height]; }
    }

    public void Set(int i, ConsoleChar pixel) => this[i] = CharColor.FromCharacter(pixel);
}
