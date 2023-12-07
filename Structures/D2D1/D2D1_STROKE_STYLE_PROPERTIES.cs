using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Properties, aside from the width, that allow geometric penning to be specified.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_STROKE_STYLE_PROPERTIES
    {
        public D2D1_CAP_STYLE startCap;
        public D2D1_CAP_STYLE endCap;
        public D2D1_CAP_STYLE dashCap;
        public D2D1_LINE_JOIN lineJoin;
        public FLOAT miterLimit;
        public D2D1_DASH_STYLE dashStyle;
        public FLOAT dashOffset;
    }
}
