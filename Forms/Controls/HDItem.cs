namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
public struct HDItem
{
    public UINT Mask;
    public int cxy;
    public unsafe WCHAR* Text;
    public HBITMAP Bitmap;
    public int cchTextMax;
    public int fmt;
    public LPARAM lParam;
    public int Image;
    public int Order;
    public UINT Type;
    public unsafe void* Filter;
    public UINT State;
}
