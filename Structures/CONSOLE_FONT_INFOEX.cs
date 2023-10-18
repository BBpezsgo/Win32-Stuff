using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CONSOLE_FONT_INFOEX
    {
        public ULONG StructSize;
        public DWORD FontIndex;
        public SHORT FontWidth;
        public SHORT FontSize;
        public UINT FontFamily;
        public UINT FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;
    }
}
