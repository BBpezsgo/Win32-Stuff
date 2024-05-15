using System.Text;
using Win32.Gdi32;

namespace Win32.Console;

public class AnsiRendererHD : Renderer<GdiColor>
{
    public override int Width => BufferWidth;
    public override int Height => BufferHeight;

    short BufferWidth;
    short BufferHeight;
    GdiColor[] ConsoleBuffer;
    readonly StringBuilder Builder;

    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref GdiColor this[int i] => ref ConsoleBuffer[i];

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
    public AnsiRendererHD(short bufferWidth, short bufferHeight)
    {
        BufferWidth = bufferWidth;
        BufferHeight = bufferHeight;

        ConsoleBuffer = new GdiColor[BufferWidth * BufferHeight];

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

        for (int y = 0; y < BufferHeight; y++)
        {
            for (int x = 0; x < BufferWidth; x++)
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
        BufferWidth = (short)width;
        BufferHeight = (short)height;

        if (ConsoleBuffer.Length != BufferWidth * BufferHeight)
        { ConsoleBuffer = new GdiColor[BufferWidth * BufferHeight]; }
    }
}
