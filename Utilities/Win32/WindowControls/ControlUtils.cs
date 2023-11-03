namespace Win32.Utilities
{
    public partial class Control
    {
        unsafe protected static HWND AnyHandle(
            HWND parent,
            string? name,
            string @class,
            DWORD style,
            int x,
            int y,
            int width,
            int height,
            ushort id)
        {
            fixed (WCHAR* windowNamePtr = name)
            fixed (WCHAR* classNamePtr = @class)
            {
                HWND handle = User32.CreateWindowExW(
                    0,
                    classNamePtr,
                    windowNamePtr,
                    style,
                    x,
                    y,
                    width,
                    height,
                    parent,
                    (HMENU)id,
                    User32.GetWindowLongPtrW(parent, GWLP.HINSTANCE));
                return handle;
            }
        }

    }
}
