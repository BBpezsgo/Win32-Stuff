using System.Runtime.InteropServices;

namespace Win32.Gdi32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct BitmapInfo
    {
        public BitmapInfoHeader Header;
        public RGBQuad Colors;
    }
}
