global using TITLEBARINFO = Win32.Forms.TitleBarInfo;

namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay($"{{{nameof(Flags)}}}")]
public readonly struct TitleBarElementState
{
    public readonly DWORD Flags;
    /// <summary>
    /// The element can accept the focus.
    /// </summary>
    public bool IsFocusable => (Flags & 0x00100000) != 0;
    /// <summary>
    /// The element is invisible.
    /// </summary>
    public bool IsInvisible => (Flags & 0x00008000) != 0;
    /// <summary>
    /// The element has no visible representation.
    /// </summary>
    public bool IsOffscreen => (Flags & 0x00010000) != 0;
    /// <summary>
    /// The element is unavailable.
    /// </summary>
    public bool IsUnavailable => (Flags & 0x00000001) != 0;
    /// <summary>
    /// The element is in the pressed state.
    /// </summary>
    public bool IsPressed => (Flags & 0x00000008) != 0;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct TitleBarElementsState
{
    public readonly TitleBarElementState TitleBar;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD Reserved;
    public readonly TitleBarElementState MinimizeButton;
    public readonly TitleBarElementState MaximizeButton;
    public readonly TitleBarElementState HelpButton;
    public readonly TitleBarElementState CloseButton;
}

/// <summary>
/// Contains title bar information.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct TitleBarInfo
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD StructSize;

    /// <summary>
    /// The coordinates of the title bar.
    /// These coordinates include all title-bar elements except the window menu.
    /// </summary>
    public readonly RECT Rect;

    /// <summary>
    /// An array that receives a value for each element of the title bar.
    /// The following are the title bar elements represented by the array.
    /// </summary>
    public readonly TitleBarElementsState ElementsState;

    TitleBarInfo(DWORD structSize) : this() => StructSize = structSize;

    public static unsafe TITLEBARINFO Create() => new((DWORD)sizeof(TITLEBARINFO));
}
