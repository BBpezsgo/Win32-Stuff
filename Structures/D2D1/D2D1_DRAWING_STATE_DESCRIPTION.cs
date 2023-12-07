using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Allows the drawing state to be atomically created. This also specifies the
    /// drawing state that is saved into an IDrawingStateBlock object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_DRAWING_STATE_DESCRIPTION
    {
        public D2D1_ANTIALIAS_MODE antialiasMode;
        public D2D1_TEXT_ANTIALIAS_MODE textAntialiasMode;
        public UINT64 tag1;
        public UINT64 tag2;
        public D2D1_MATRIX_3X2_F transform;
    }
}
