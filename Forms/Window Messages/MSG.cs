namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
public struct MSG
{
    public HWND hwnd;
    public UINT message;
    public WPARAM wParam;
    public LPARAM lParam;
    public DWORD time;
    public POINT pt;
    public DWORD lPrivate;
}
