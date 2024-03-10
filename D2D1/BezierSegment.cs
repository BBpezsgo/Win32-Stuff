namespace Win32.D2D1;

/// <summary>
/// Describes a cubic bezier in a path.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct BezierSegment
{
    public Point2F Point1;
    public Point2F Point2;
    public Point2F Point3;

    public BezierSegment(Point2F point1, Point2F point2, Point2F point3)
    {
        this.Point1 = point1;
        this.Point2 = point2;
        this.Point3 = point3;
    }
}
