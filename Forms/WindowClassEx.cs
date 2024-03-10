global using WNDCLASSEXW = Win32.Forms.WindowClassEx;

namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
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
    public HICON IconSmall;

    WindowClassEx(DWORD structSize) : this() => this.StructSize = structSize;

    public static unsafe WindowClassEx Create() => new((DWORD)sizeof(WindowClassEx));
}
