namespace Win32
{
    [SupportedOSPlatform("windows")]
    public class Win32Class : IEquatable<Win32Class?>
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
        public static unsafe Win32Class Register(WNDCLASSEXW* info) => Win32Class.Register(info, out _);

        /// <exception cref="WindowsException"/>
        public static unsafe Win32Class Register(WNDCLASSEXW* info, out ushort id)
        {
            id = User32.RegisterClassExW(info);
            if (id == 0)
            { throw WindowsException.Get(); }
            return new Win32Class(new string(info->ClassName), info->Instance);
        }

        /// <exception cref="WindowsException"/>
        public unsafe void Unregister()
        {
            fixed (WCHAR* classNamePtr = _className)
            {
                if (User32.UnregisterClassW(classNamePtr, _moduleHandle) == 0)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        public unsafe WNDCLASSEXW GetInfo()
        {
            WNDCLASSEXW result = WNDCLASSEXW.Create();
            fixed (WCHAR* namePtr = _className)
            {
                if (User32.GetClassInfoExW(_moduleHandle, namePtr, &result) == 0)
                { throw WindowsException.Get(); }
            }
            return result;
        }

        /// <inheritdoc/>
        public override string ToString() => _className;
        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as Win32Class);
        /// <inheritdoc/>
        public bool Equals(Win32Class? other) =>
            other is not null &&
            string.Equals(_className, other._className, StringComparison.Ordinal) &&
            _moduleHandle == other._moduleHandle;
        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(_className, _moduleHandle);
    }
}
