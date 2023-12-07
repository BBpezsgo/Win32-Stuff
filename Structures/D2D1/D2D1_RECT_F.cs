using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Represents a rectangle defined by the coordinates of the upper-left corner
    /// (left, top) and the coordinates of the lower-right corner (right, bottom).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_RECT_F
    {
        public FLOAT left;
        public FLOAT top;
        public FLOAT right;
        public FLOAT bottom;
    }
}
