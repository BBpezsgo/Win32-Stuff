namespace Win32.Utilities
{
    public partial class Control
    {
        unsafe public static Control Any(
            HWND parent,
            string name,
            string @class,
            DWORD style,
            int x,
            int y,
            int width,
            int height,
            ushort id)
        {
            fixed (char* windowNamePtr = name)
            fixed (char* classNamePtr = @class)
            {
                HWND handle = Control.AnyHandle(
                    parent,
                    name,
                    @class,
                    style,
                    x,
                    y,
                    width,
                    height,
                    id);

                return new Control(handle);
            }
        }

        unsafe protected static HWND AnyHandle(
            HWND parent,
            string name,
            string @class,
            DWORD style,
            int x,
            int y,
            int width,
            int height,
            ushort id)
        {
            fixed (char* windowNamePtr = name)
            fixed (char* classNamePtr = @class)
            {
                return User32.CreateWindowExW(
                    0,
                    classNamePtr,
                    windowNamePtr,
                    style,
                    x,
                    y,
                    width,
                    height,
                    parent,
                    new HMENU(id),
                    User32.GetWindowLongPtrW(parent, GWL.GWL_HINSTANCE));
            }
        }

    }
}
