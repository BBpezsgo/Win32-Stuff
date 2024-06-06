﻿using System.Text;

namespace Win32.Console;

public class AnsiRenderer : BufferedRenderer<AnsiChar>, IOnlySetterRenderer<ConsoleChar>
{
    public override int Width => BufferWidth;
    public override int Height => BufferHeight;
    public override Span<AnsiChar> Buffer => ConsoleBuffer.AsSpan();

    short BufferWidth;
    short BufferHeight;
    AnsiChar[] ConsoleBuffer;
    readonly StringBuilder Builder;

    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref AnsiChar this[int i] => ref ConsoleBuffer[i];

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

        ConsoleBuffer = new AnsiChar[BufferWidth * BufferHeight];

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
        { ConsoleBuffer = new AnsiChar[BufferWidth * BufferHeight]; }
    }

    public void Set(int i, ConsoleChar pixel) => ConsoleBuffer[i] = pixel;
}
