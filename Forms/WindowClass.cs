global using WNDCLASSW = Win32.Forms.WindowClass;

namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
public struct WindowClass
{
    public UINT Style;
    public unsafe delegate*<HWND, uint, WPARAM, LPARAM, LRESULT> WindowProcedure;
    public int ClsExtra;
    public int WndExtra;
    public HINSTANCE Instance;
    public HICON Icon;
    public HCURSOR Cursor;
    public HBRUSH BackgroundBrush;
    public unsafe WCHAR* MenuName;
    public unsafe WCHAR* ClassName;
}
