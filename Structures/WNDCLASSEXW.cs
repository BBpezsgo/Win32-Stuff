using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WindowClassEx
    {
        readonly uint cbSize;
        public uint style;
        unsafe public delegate*<HWND, uint, WPARAM, LPARAM, LRESULT> lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public HINSTANCE hInstance;
        public HICON hIcon;
        public HCURSOR hCursor;
        public HBRUSH hbrBackground;
        unsafe public char* lpszMenuName;
        unsafe public char* lpszClassName;
        public HICON hIconSm;

        WindowClassEx(DWORD cbSize) : this() => this.cbSize = cbSize;
        unsafe public static WindowClassEx Create() => new((uint)sizeof(WindowClassEx));
    }
}
