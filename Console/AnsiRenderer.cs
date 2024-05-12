using System.Numerics;
using System.Text;

namespace Win32.Console;

public class AnsiRenderer : Renderer<ConsoleChar>
{
    public override short Width => BufferWidth;
    public override short Height => BufferHeight;

    short BufferWidth;
    short BufferHeight;
    ConsoleChar[] ConsoleBuffer;
    readonly StringBuilder Builder;

    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref ConsoleChar this[int i] => ref ConsoleBuffer[i];

    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref ConsoleChar this[int x, int y] => ref ConsoleBuffer[(y * BufferWidth) + x];
    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref ConsoleChar this[float x, float y] => ref ConsoleBuffer[((int)MathF.Round(y) * BufferWidth) + (int)MathF.Round(x)];
    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref ConsoleChar this[COORD p] => ref ConsoleBuffer[(p.Y * BufferWidth) + p.X];
    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref ConsoleChar this[POINT p] => ref ConsoleBuffer[(p.Y * BufferWidth) + p.X];
    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref ConsoleChar this[Vector2 p] => ref ConsoleBuffer[((int)MathF.Round(p.Y) * BufferWidth) + (int)MathF.Round(p.X)];

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
    public AnsiRenderer(short bufferWidth, short bufferHeight)
    {
        BufferWidth = bufferWidth;
        BufferHeight = bufferHeight;

        ConsoleBuffer = new ConsoleChar[BufferWidth * BufferHeight];

        if (OperatingSystem.IsWindows())
        { Ansi.EnableVirtualTerminalSequences(); }
        System.Console.CursorVisible = false;

        Builder = new StringBuilder(BufferWidth * BufferHeight);
    }

    public override bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < BufferWidth && y < BufferHeight;
    public override bool IsVisible(float x, float y) => MathF.Round(x) >= 0 && MathF.Round(y) >= 0 && MathF.Round(x) < BufferWidth && MathF.Round(y) < BufferHeight;
    public override bool IsVisible(COORD position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
    public override bool IsVisible(POINT position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
    public override bool IsVisible(Vector2 position) => MathF.Round(position.X) >= 0 && MathF.Round(position.Y) >= 0 && MathF.Round(position.X) < BufferWidth && MathF.Round(position.Y) < BufferHeight;

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

        for (int y = 0; y < BufferHeight; y++)
        {
            for (int x = 0; x < BufferWidth; x++)
            {
                Ansi.FromConsoleChar(
                    Builder,
                    this[x, y],
                    ref prevForegroundColor,
                    ref prevBackgroundColor,
                    x == 0 && y == 0);
            }
        }

        System.Console.CursorVisible = false;
        System.Console.SetCursorPosition(0, 0);
        System.Console.Out.Write(Builder);
        System.Console.SetCursorPosition(0, 0);
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
        { ConsoleBuffer = new ConsoleChar[BufferWidth * BufferHeight]; }
    }
}
