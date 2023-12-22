using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    public class WindowPropertiesContainer : IEquatable<WindowPropertiesContainer?>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HWND _handle;

        public WindowPropertiesContainer(HWND handle) => _handle = handle;

        /// <exception cref="WindowsException"/>
        public unsafe HANDLE this[string property]
        {
            get
            {
                HANDLE value;
                fixed (WCHAR* propertyPtr = property)
                { value = User32.GetPropW(_handle, propertyPtr); }
                if (value == HANDLE.Zero)
                { throw new KeyNotFoundException($"The specified key (\"{property}\") not found in the collection"); }
                return value;
            }
            set
            {
                BOOL result;
                fixed (WCHAR* propertyPtr = property)
                { result = User32.SetPropW(_handle, propertyPtr, value); }
                if (result == FALSE)
                { throw WindowsException.Get(); }
            }
        }

        public unsafe bool Remove(string property)
        {
            HANDLE result;
            fixed (WCHAR* propertyPtr = property)
            { result = User32.RemovePropW(_handle, propertyPtr); }
            return result != HANDLE.Zero;
        }

        static unsafe BOOL ToDictionaryProc(HWND handle, WCHAR* property, HANDLE value, ULONG_PTR lParam)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr((nint)lParam);
            if (!gcHandle.IsAllocated)
            { return FALSE; }
            object? obj = gcHandle.Target;
            if (obj == null)
            { return FALSE; }

            Dictionary<string, HANDLE> childList = (Dictionary<string, HANDLE>)obj;
            childList.Add(new string(property), value);

            return TRUE;
        }

        public unsafe Dictionary<string, HANDLE> ToDictionary()
        {
            Dictionary<string, HANDLE> result = new();
            GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
            _ = User32.EnumPropsExW(_handle, &ToDictionaryProc, GCHandle.ToIntPtr(handle));
            handle.Free();
            return result;
        }

        public unsafe bool TryGetValue(string property, out HANDLE value)
        {
            HANDLE result;
            fixed (WCHAR* propertyPtr = property)
            { result = User32.GetPropW(_handle, propertyPtr); }
            value = result;
            return value != HANDLE.Zero;
        }

        public unsafe bool Contains(string property)
        {
            HANDLE result;
            fixed (WCHAR* propertyPtr = property)
            { result = User32.GetPropW(_handle, propertyPtr); }
            return result != HANDLE.Zero;
        }

        static unsafe BOOL DelPropProc(
            HWND handle,
            WCHAR* property,
            HANDLE value)
        {
            _ = User32.RemovePropW(handle, property);
            return TRUE;
        }
        public unsafe void Clear() => _ = User32.EnumPropsW(_handle, &DelPropProc);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as WindowPropertiesContainer);

        /// <inheritdoc/>
        public bool Equals(WindowPropertiesContainer? other) => other is not null && _handle == other._handle;

        /// <inheritdoc/>
        public override int GetHashCode() => _handle.GetHashCode();
    }
}
