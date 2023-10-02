global using COLORREF = System.UInt32;

using System.Runtime.InteropServices;

namespace Win32
{
    public static class Gdi32
    {
        [DllImport("Gdi32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern COLORREF SetPixel(
          [In] HDC hdc,
          [In] int x,
          [In] int y,
          [In] COLORREF color
        );
    }
}
