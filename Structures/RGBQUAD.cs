using System.Runtime.InteropServices;

namespace Win32.Gdi32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct RGBQuad
    {
        public BYTE Blue;
        public BYTE Green;
        public BYTE Red;
        readonly BYTE Reserved;
    }
}
