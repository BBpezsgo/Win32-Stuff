global using WNDCLASSW = Win32.WindowClass;

using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WindowClass
    {
        public UINT Style;
        public unsafe delegate*<HWND, uint, WPARAM, LPARAM, LRESULT> WindowProcedure;
        public int ClsExtra;
        public int WndExtra;
        public HINSTANCE hInstance;
        public HICON Icon;
        public HCURSOR Cursor;
        public HBRUSH BackgroundBrush;
        public unsafe WCHAR* MenuName;
        public unsafe WCHAR* ClassName;
    }
}
