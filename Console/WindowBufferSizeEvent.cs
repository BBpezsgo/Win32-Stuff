namespace Win32.Console;

/// <summary>
/// Describes a change in the size of the console screen buffer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct WindowBufferSizeEvent
{
    public readonly COORD Size;

    public WindowBufferSizeEvent(COORD size)
    {
        Size = size;
    }

    public readonly SHORT Width => Size.X;
    public readonly SHORT Height => Size.Y;
}
