namespace Win32.Utilities
{
    public class Win32Class
    {
        readonly string _className;
        readonly HINSTANCE _moduleHandle;

        public string Name => _className;

        Win32Class(string name, HWND moduleHandle)
        {
            _className = name;
            _moduleHandle = moduleHandle;
        }

        /// <exception cref="WindowsException"/>
        unsafe public static Win32Class Register(WNDCLASSEXW* info) => Win32Class.Register(info, out _);
        /// <exception cref="WindowsException"/>
        unsafe public static Win32Class Register(WNDCLASSEXW* info, out ushort id)
        {
            id = User32.RegisterClassExW(info);
            if (id == 0)
            { throw WindowsException.Get(); }
            return new Win32Class(new string(info->lpszClassName), info->hInstance);
        }

        /// <exception cref="WindowsException"/>
        unsafe public void Unregister()
        {
            fixed (WCHAR* classNamePtr = _className)
            {
                if (User32.UnregisterClassW(classNamePtr, _moduleHandle) == 0)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        unsafe public WNDCLASSEXW GetInfo()
        {
            WNDCLASSEXW result = WNDCLASSEXW.Create();
            fixed (WCHAR* namePtr = _className)
            {
                if (User32.GetClassInfoExW(_moduleHandle, namePtr, &result) == 0)
                { throw WindowsException.Get(); }
            }
            return result;
        }
    }
}
