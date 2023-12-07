using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Represents an x-coordinate and y-coordinate pair in two-dimensional space.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_POINT_2F
    {
        public FLOAT x;
        public FLOAT y;

        public D2D1_POINT_2F(FLOAT x, FLOAT y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
