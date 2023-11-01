using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CONSOLE_FONT_INFOEX
    {
        readonly ULONG cbSize;
        public DWORD FontIndex;
        public SHORT FontWidth;
        public SHORT FontSize;
        public UINT FontFamily;
        public UINT FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;

#pragma warning disable CS8618
        CONSOLE_FONT_INFOEX(DWORD cbSize) : this() => this.cbSize = cbSize;
#pragma warning restore CS8618

        public static CONSOLE_FONT_INFOEX Create() => new((DWORD)Marshal.SizeOf<CONSOLE_FONT_INFOEX>());
    }
}
