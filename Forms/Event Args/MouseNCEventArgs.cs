namespace Win32.Forms;

public enum Hit : uint
{
    /// <summary>
    /// On the screen background or on a dividing line between windows.
    /// </summary>
    Nowhere = 0,
    /// <summary>
    /// In a client area.
    /// </summary>
    Client = 1,
    /// <summary>
    /// In a title bar.
    /// </summary>
    Caption = 2,
    /// <summary>
    /// In a window menu or in a Close button in a child window.
    /// </summary>
    SystemMenu = 3,
    /// <summary>
    /// In a size box.
    /// </summary>
    Size = 4,
    /// <summary>
    /// In a menu.
    /// </summary>
    Menu = 5,
    /// <summary>
    /// In a horizontal scroll bar.
    /// </summary>
    HScroll = 6,
    /// <summary>
    /// In the vertical scroll bar.
    /// </summary>
    VScroll = 7,
    /// <summary>
    /// In a Minimize button.
    /// </summary>
    MinimizeButton = 8,
    /// <summary>
    /// In a Maximize button.
    /// </summary>
    MaximizeButton = 9,
    /// <summary>
    /// In the left border of a resizable window (the user
    /// can click the mouse to resize the window horizontally).
    /// </summary>
    Left = 10,
    /// <summary>
    /// In the right border of a resizable window (the user
    /// can click the mouse to resize the window horizontally).
    /// </summary>
    Right = 11,
    /// <summary>
    /// In the upper-horizontal border of a window.
    /// </summary>
    Top = 12,
    /// <summary>
    /// In the upper-left corner of a window border.
    /// </summary>
    TopLeft = 13,
    /// <summary>
    /// In the upper-right corner of a window border.
    /// </summary>
    TopRight = 14,
    /// <summary>
    /// In the lower-horizontal border of a resizable window
    /// (the user can click the mouse to resize the window vertically).
    /// </summary>
    Bottom = 15,
    /// <summary>
    /// In the lower-left corner of a border of a resizable
    /// window (the user can click the mouse to resize the window diagonally).
    /// </summary>
    BottomLeft = 16,
    /// <summary>
    /// In the lower-right corner of a border of a resizable
    /// window (the user can click the mouse to resize the window diagonally).
    /// </summary>
    BottomRight = 17,
    /// <summary>
    /// In the border of a window that does not have a sizing border.
    /// </summary>
    Border = 18,
    /// <summary>
    /// In a Close button.
    /// </summary>
    Close = 20,
    /// <summary>
    /// In a Help button.
    /// </summary>
    Help = 21,
    /// <summary>
    /// On the screen background or on a dividing line between
    /// windows (same as <see cref="Nowhere"/>, except that the <see cref="User32.DefWindowProcW"/>
    /// function produces a system beep to indicate an error).
    /// </summary>
    Error = unchecked((uint)-2),
    /// <summary>
    /// In a window currently covered by another window in the
    /// same thread (the message will be sent to underlying
    /// windows in the same thread until one of them returns
    /// a code that is not <see cref="Transparent"/>).
    /// </summary>
    Transparent = unchecked((uint)-1),
}

public readonly struct MouseNCEventArgs
{
    public Hit Hit { get; }
    public short X { get; }
    public short Y { get; }

    public MouseNCEventArgs(nuint wParam, nint lParam)
    {
        Hit = (Hit)wParam;
        X = unchecked((short)BitUtils.LowWord(lParam));
        Y = unchecked((short)BitUtils.HighWord(lParam));
    }

    public override string ToString() => $"({X} {Y} ; {Hit})";
}
