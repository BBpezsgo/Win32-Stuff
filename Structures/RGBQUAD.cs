using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct RGBQUAD
    {
        public BYTE rgbBlue;
        public BYTE rgbGreen;
        public BYTE rgbRed;
        public BYTE rgbReserved;
    }
}
