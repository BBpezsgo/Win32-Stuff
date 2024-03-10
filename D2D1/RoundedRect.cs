namespace Win32.D2D1;

/// <summary>
/// Contains the dimensions and corner radii of a rounded rectangle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct RoundedRect
{
    public RectF Rect;
    public FLOAT RadiusX;
    public FLOAT RadiusY;
}
