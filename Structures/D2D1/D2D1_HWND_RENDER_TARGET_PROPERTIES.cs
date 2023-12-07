using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Contains the HWND, pixel size, and presentation options for an
    /// ID2D1HwndRenderTarget.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_HWND_RENDER_TARGET_PROPERTIES
    {
        public HWND hwnd;
        public D2D1_SIZE_U pixelSize;
        public D2D1_PRESENT_OPTIONS presentOptions;
    }
}
