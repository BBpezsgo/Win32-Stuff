using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CONSOLE_FONT_INFOEX
    {
        public const int Size =
            sizeof(ULONG) +
            sizeof(DWORD) +
            sizeof(SHORT) +
            sizeof(SHORT) +
            sizeof(UINT) +
            sizeof(UINT) +
            32;

        public ULONG cbSize;
        public DWORD nFont;
        public COORD dwFontSize;
        public UINT FontFamily;
        public UINT FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;
    }
}
