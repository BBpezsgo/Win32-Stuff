namespace Win32.Console;

[StructLayout(LayoutKind.Sequential)]
public struct ConsoleSelectionInfo
{
    public DWORD Flags;
    public COORD SelectionAnchor;
    public SMALL_RECT Selection;
}

[Flags]
public enum ConsoleSelectionInfoFlags : DWORD
{
    /// <summary>
    /// No selection.
    /// </summary>
    NoSelection = 0x0000,
    /// <summary>
    /// Selection has begun.If a mouse selection, this will typically
    /// not occur without the <see cref="SelectionNotEmpty"/> flag.
    /// If a keyboard selection, this may occur when mark mode has been
    /// entered but the user is still navigating to the initial position.
    /// </summary>
    SelectionInProgress = 0x0001,
    /// <summary>
    /// Selection rectangle not empty. The payload
    /// of <see cref="ConsoleSelectionInfo.SelectionAnchor"/> and
    /// <see cref="ConsoleSelectionInfo.Selection"/> are valid.
    /// </summary>
    SelectionNotEmpty = 0x0002,
    /// <summary>
    /// Selecting with the mouse. If off, the user is operating <c>conhost.exe</c> mark
    /// mode selection with the keyboard.
    /// </summary>
    MouseSelection = 0x0004,
    /// <summary>
    /// Mouse is down.The user is actively adjusting the selection rectangle with a mouse.
    /// </summary>
    ConsoleMouseDown = 0x0008,
}
