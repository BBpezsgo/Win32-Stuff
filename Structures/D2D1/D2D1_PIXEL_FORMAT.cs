using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Description of a pixel format.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_PIXEL_FORMAT
    {
        public DXGI_FORMAT format;
        public D2D1_ALPHA_MODE alphaMode;
    }
}
