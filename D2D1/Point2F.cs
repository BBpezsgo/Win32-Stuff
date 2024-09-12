namespace Win32.D2D1;

/// <summary>
/// Represents an x-coordinate and y-coordinate pair in two-dimensional space.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Point2F
{
    public FLOAT X;
    public FLOAT Y;

    public Point2F(FLOAT x, FLOAT y)
    {
        this.X = x;
        this.Y = y;
    }
}
