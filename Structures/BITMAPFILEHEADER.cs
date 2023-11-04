using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct BITMAPFILEHEADER
    {
        public WORD bfType;
        public DWORD bfSize;
        public WORD bfReserved1;
        public WORD bfReserved2;
        public DWORD bfOffBits;
    }
}
