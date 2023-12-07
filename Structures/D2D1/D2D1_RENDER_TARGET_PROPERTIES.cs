using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Contains rendering options (hardware or software), pixel format, DPI
    /// information, remoting options, and Direct3D support requirements for a render
    /// target.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_RENDER_TARGET_PROPERTIES
    {
        public D2D1_RENDER_TARGET_TYPE type;
        public D2D1_PIXEL_FORMAT pixelFormat;
        public FLOAT dpiX;
        public FLOAT dpiY;
        public D2D1_RENDER_TARGET_USAGE usage;
        public D2D1_FEATURE_LEVEL minLevel;
    }
}
