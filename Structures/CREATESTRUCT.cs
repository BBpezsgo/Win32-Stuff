using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    unsafe public struct CreateStruct
    {
        public void* lpCreateParams;
        public HINSTANCE hInstance;
        public HMENU hMenu;
        public HWND hwndParent;
        public int cy;
        public int cx;
        public int y;
        public int x;
        public LONG style;
        public char* lpszName;
        public char* lpszClass;
        public DWORD dwExStyle;
    }
}
