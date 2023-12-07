using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Stores an ordered pair of integers, typically the width and height of a
    /// rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_SIZE_U
    {
        public UINT32 width;
        public UINT32 height;
    }
}
