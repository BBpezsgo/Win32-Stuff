namespace Win32.Utilities
{
    public class Window
    {
        HWND _handle;

        public HWND Handle
        {
            get => _handle;
            protected set => _handle = value;
        }

        public Window()
        {
            _handle = HWND.Zero;
        }

        public Window(HWND handle)
        {
            _handle = handle;
        }

        unsafe public Window(
            string @class,
            string name,
            DWORD style,
            int x,
            int y,
            int width,
            int height,
            HWND parent,
            HMENU menu,
            HINSTANCE instance,
            void* @param = null)
        {
            fixed (char* windowNamePtr = name)
            fixed (char* classNamePtr = @class)
            {
                _handle = User32.CreateWindowExW(
                    0,
                    classNamePtr,
                    windowNamePtr,
                    style,
                    x,
                    y,
                    width,
                    height,
                    parent,
                    menu,
                    instance,
                    @param);
            }
        }

        /// <param name="newParent">
        /// Handle to the new parent window
        /// </param>
        /// <returns>
        /// Handle to the previous parent window
        /// </returns>
        /// <exception cref="WindowsException"/>
        public HWND SetParent(HWND newParent)
        {
            HWND result = User32.SetParent(Handle, newParent);
            if (result == HWND.Zero)
            { throw WindowsException.Get(); }
            return result;
        }

        public bool IsChildOf(HWND parent)
        {

        }
    }
}
