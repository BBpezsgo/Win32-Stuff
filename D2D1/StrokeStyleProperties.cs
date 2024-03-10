namespace Win32.D2D1;

/// <summary>
/// Properties, aside from the width, that allow geometric penning to be specified.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct StrokeStyleProperties
{
    public CapStyle StartCap;
    public CapStyle EndCap;
    public CapStyle DashCap;
    public LineJoin LineJoin;
    public FLOAT MiterLimit;
    public DashStyle DashStyle;
    public FLOAT DashOffset;
}
