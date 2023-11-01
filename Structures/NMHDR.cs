using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct NMHDR
    {
        public readonly HWND hwndFrom;
        public readonly UINT idFrom;
        public readonly UINT code;
    }
}
