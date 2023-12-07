using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Contains the center point, x-radius, and y-radius of an ellipse.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_ELLIPSE
    {
        public D2D1_POINT_2F point;
        public FLOAT radiusX;
        public FLOAT radiusY;
    }
}
