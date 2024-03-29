﻿using System.Numerics;

namespace Win32.Console;

public class ConsoleRenderer : BufferedRenderer<ConsoleChar>
{
    protected HANDLE Handle;

    public override short Width => BufferWidth;
    public override short Height => BufferHeight;

    public override Span<ConsoleChar> Buffer => ConsoleBuffer;

    protected short BufferWidth;
    protected short BufferHeight;

    protected ConsoleChar[] ConsoleBuffer;
    protected SMALL_RECT ConsoleRect;

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

    /// <exception cref="WindowsException"/>
    /// <exception cref="GeneralException"/>
    [SupportedOSPlatform("windows")]
    public ConsoleRenderer() : this(Terminal.WindowWidth, Terminal.WindowHeight)
    { }

    /// <exception cref="WindowsException"/>
    /// <exception cref="GeneralException"/>
    [SupportedOSPlatform("windows")]
    public ConsoleRenderer(short bufferWidth, short bufferHeight)
    {
        /*
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        _ = Kernel32.SetConsoleOutputCP(65001);
        _ = Kernel32.SetConsoleCP(65001);
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        */

        Handle = Kernel32.GetStdHandle(StdHandle.Output);

        if (Handle == Kernel32.InvalidHandle)
        { throw WindowsException.Get(); }

        if (Handle == 0)
        { throw new GeneralException("Application does not have a standard output"); }

        BufferWidth = bufferWidth;
        BufferHeight = bufferHeight;

        ConsoleBuffer = new ConsoleChar[BufferWidth * BufferHeight];
        ConsoleRect = new SMALL_RECT((SHORT)0, (SHORT)0, BufferWidth, BufferHeight);
    }

    public override bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < BufferWidth && y < BufferHeight;
    public override bool IsVisible(float x, float y) => MathF.Round(x) >= 0 && MathF.Round(y) >= 0 && MathF.Round(x) < BufferWidth && MathF.Round(y) < BufferHeight;
    public override bool IsVisible(COORD position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
    public override bool IsVisible(POINT position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
    public override bool IsVisible(Vector2 position) => MathF.Round(position.X) >= 0 && MathF.Round(position.Y) >= 0 && MathF.Round(position.X) < BufferWidth && MathF.Round(position.Y) < BufferHeight;

    /// <exception cref="WindowsException"/>
    [SupportedOSPlatform("windows")]
    public override void Render()
    {
        if (Kernel32.WriteConsoleOutput(
            Handle,
            ConsoleBuffer,
            Size,
            default,
            ref ConsoleRect) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    [SupportedOSPlatform("windows")]
    public override void RefreshBufferSize()
    {
        ConsoleScreenBufferInfo info = Terminal.ScreenBufferInfo;
        BufferWidth = info.Window.Width;
        BufferHeight = info.Window.Height;

        if (ConsoleBuffer.Length != BufferWidth * BufferHeight)
        { ConsoleBuffer = new ConsoleChar[BufferWidth * BufferHeight]; }
        ConsoleRect = new SMALL_RECT((SHORT)0, (SHORT)0, BufferWidth, BufferHeight);
    }
}
