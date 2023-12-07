using System.Runtime.InteropServices;

namespace Win32.Gdi32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct BITMAP
    {
        public LONG Type;
        public LONG Width;
        public LONG Height;
        public LONG WidthBytes;
        public WORD Planes;
        public WORD BitsPixel;
        unsafe public void* Bits;
    }
}
