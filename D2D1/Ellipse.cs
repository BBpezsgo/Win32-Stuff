namespace Win32.D2D1;

/// <summary>
/// Contains the center point, x-radius, and y-radius of an ellipse.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Ellipse
{
    public Point2F Point;
    public FLOAT RadiusX;
    public FLOAT RadiusY;
}
