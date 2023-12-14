global using WNDCLASSEXW = Win32.WindowClassEx;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WindowClassEx
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly uint StructSize;

        public uint Style;
        public unsafe delegate*<HWND, uint, WPARAM, LPARAM, LRESULT> WindowProcedure;
        public int ClsExtra;
        public int WndExtra;
        public HINSTANCE Instance;
        public HICON Icon;
        public HCURSOR Cursor;
        public HBRUSH BackgroundBrush;
        public unsafe char* MenuName;
        public unsafe char* ClassName;
        public HICON IconSm;

        WindowClassEx(DWORD structSize) : this() => this.StructSize = structSize;

        public static unsafe WNDCLASSEXW Create() => new((DWORD)sizeof(WNDCLASSEXW));
    }
}
