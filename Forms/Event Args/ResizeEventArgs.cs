namespace Win32.Forms;

public enum WindowSizingEdge
{
    /// <summary>
    /// Left edge
    /// </summary>
    Left = 1,
    /// <summary>
    /// Right edge
    /// </summary>
    Right = 2,
    /// <summary>
    /// Top edge
    /// </summary>
    Top = 3,
    /// <summary>
    /// Top-left corner
    /// </summary>
    TopLeft = 4,
    /// <summary>
    /// Top-right
    /// </summary>
    TopRight = 5,
    /// <summary>
    /// Bottom edge
    /// </summary>
    Bottom = 6,
    /// <summary>
    /// Bottom-left corner
    /// </summary>
    BottomLeft = 7,
    /// <summary>
    /// Bottom-right corner
    /// </summary>
    BottomRight = 8,
}

public readonly struct ResizeEventArgs
{
    public WindowSizingEdge Edge { get; }
    public RECT Rect { get; }

    public unsafe ResizeEventArgs(nuint wParam, nint lParam)
    {
        Edge = (WindowSizingEdge)wParam;
        Rect = *(RECT*)lParam;
    }
}
