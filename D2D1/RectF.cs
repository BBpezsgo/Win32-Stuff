namespace Win32.D2D1;

/// <summary>
/// Represents a rectangle defined by the coordinates of the upper-left corner
/// (left, top) and the coordinates of the lower-right corner (right, bottom).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct RectF
{
    public FLOAT Left;
    public FLOAT Top;
    public FLOAT Right;
    public FLOAT Bottom;
}
