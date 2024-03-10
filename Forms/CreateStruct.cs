global using CREATESTRUCT = Win32.Forms.CreateStruct;

namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
public struct CreateStruct
{
    public unsafe void* CreateParams;
    public HINSTANCE Instance;
    public HMENU Menu;
    public HWND Parent;
    public int WindowHeightPx;
    public int WindowWidthPx;
    public int PositionY;
    public int PositionX;
    public LONG Style;
    public unsafe WCHAR* Name;
    public unsafe WCHAR* Class;
    public DWORD StyleEx;
}
