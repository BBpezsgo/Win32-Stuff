using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Contains the dimensions and corner radii of a rounded rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_ROUNDED_RECT
    {
        public D2D1_RECT_F rect;
        public FLOAT radiusX;
        public FLOAT radiusY;
    }
}
