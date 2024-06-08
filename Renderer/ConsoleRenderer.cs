using System.Runtime.CompilerServices;

namespace Win32.Console;

public class ConsoleRenderer : BufferedRenderer<ConsoleChar>
{
    readonly HANDLE Handle;
    SMALL_RECT ConsoleRect;

    /// <exception cref="WindowsException"/>
    /// <exception cref="GeneralException"/>
    [SupportedOSPlatform("windows")]
    public ConsoleRenderer() : this(Terminal.WindowWidth, Terminal.WindowHeight)
    { }

    /// <exception cref="WindowsException"/>
    /// <exception cref="GeneralException"/>
    [SupportedOSPlatform("windows")]
    public ConsoleRenderer(short bufferWidth, short bufferHeight) : base(bufferWidth, bufferHeight)
    {
        Handle = Kernel32.GetStdHandle(StdHandle.Output);

        if (Handle == Kernel32.InvalidHandle)
        { throw WindowsException.Get(); }

        if (Handle == 0)
        { throw new GeneralException("Application does not have a standard output"); }

        ConsoleRect = new SMALL_RECT(0, 0, _width, _height);
    }

    /// <exception cref="WindowsException"/>
    [SupportedOSPlatform("windows")]
    public override unsafe void Render()
    {
        if (Kernel32.WriteConsoleOutputW(
            Handle,
            (ConsoleChar*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(_buffer.AsSpan())),
            new SmallSize(_width, _height),
            default,
            ref ConsoleRect) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    [SupportedOSPlatform("windows")]
    public override void RefreshBufferSize()
    {
        ConsoleScreenBufferInfo info = Terminal.ScreenBufferInfo;
        _width = info.Window.Width;
        _height = info.Window.Height;

        if (_buffer.Length != _width * _height)
        { _buffer = new ConsoleChar[_width * _height]; }
        ConsoleRect = new SMALL_RECT(0, 0, _width, _height);
    }
}
