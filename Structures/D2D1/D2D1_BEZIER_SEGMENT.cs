using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Describes a cubic bezier in a path.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_BEZIER_SEGMENT
    {
        public D2D1_POINT_2F point1;
        public D2D1_POINT_2F point2;
        public D2D1_POINT_2F point3;

        public D2D1_BEZIER_SEGMENT(D2D1_POINT_2F point1, D2D1_POINT_2F point2, D2D1_POINT_2F point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
        }
    }
}
