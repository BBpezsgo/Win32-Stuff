using System.Runtime.CompilerServices;

namespace Win32.Console;

public class ConsoleRenderer : BufferedRenderer<ConsoleChar>
{
    public override int Width => BufferWidth;
    public override int Height => BufferHeight;

    public override Span<ConsoleChar> Buffer => ConsoleBuffer;

    readonly HANDLE Handle;
    short BufferWidth;
    short BufferHeight;
    ConsoleChar[] ConsoleBuffer;
    SMALL_RECT ConsoleRect;

    /// <exception cref="ArgumentOutOfRangeException"/>
    public override ref ConsoleChar this[int i] => ref ConsoleBuffer[i];

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
        Handle = Kernel32.GetStdHandle(StdHandle.Output);

        if (Handle == Kernel32.InvalidHandle)
        { throw WindowsException.Get(); }

        if (Handle == 0)
        { throw new GeneralException("Application does not have a standard output"); }

        BufferWidth = bufferWidth;
        BufferHeight = bufferHeight;

        ConsoleBuffer = new ConsoleChar[Width * Height];
        ConsoleRect = new SMALL_RECT((SHORT)0, (SHORT)0, Width, Height);
    }

    /// <exception cref="WindowsException"/>
    [SupportedOSPlatform("windows")]
    public override unsafe void Render()
    {
        if (Kernel32.WriteConsoleOutputW(
            Handle,
            (ConsoleChar*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ConsoleBuffer.AsSpan())),
            new SmallSize(Width, Height),
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
