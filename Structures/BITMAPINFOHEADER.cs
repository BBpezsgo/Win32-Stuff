using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct BITMAPINFOHEADER
    {
        public DWORD biSize;
        public LONG biWidth;
        public LONG biHeight;
        public WORD biPlanes;
        public WORD biBitCount;
        public DWORD biCompression;
        public DWORD biSizeImage;
        public LONG biXPelsPerMeter;
        public LONG biYPelsPerMeter;
        public DWORD biClrUsed;
        public DWORD biClrImportant;
    }
}
