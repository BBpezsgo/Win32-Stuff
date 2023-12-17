using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BLENDFUNCTION
    {
        public BYTE BlendOp;
        public BYTE BlendFlags;
        public BYTE SourceConstantAlpha;
        public BYTE AlphaFormat;
    }
}
