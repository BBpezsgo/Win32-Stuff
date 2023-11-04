using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct BITMAP
    {
        public LONG bmType;
        public LONG bmWidth;
        public LONG bmHeight;
        public LONG bmWidthBytes;
        public WORD bmPlanes;
        public WORD bmBitsPixel;
        unsafe public void* bmBits;
    }
}
