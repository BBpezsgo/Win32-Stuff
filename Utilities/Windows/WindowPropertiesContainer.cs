using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    public class WindowPropertiesContainer
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HWND _handle;

        public WindowPropertiesContainer(HWND handle) => _handle = handle;

        /// <exception cref="WindowsException"/>
        public unsafe HANDLE this[string property]
        {
            get
            {
                HANDLE result;
                fixed (WCHAR* propertyPtr = property)
                { result = User32.GetPropW(_handle, propertyPtr); }
                return result;
            }
            set
            {
                BOOL result;
                fixed (WCHAR* propertyPtr = property)
                { result = User32.SetPropW(_handle, propertyPtr, value); }
                if (result == 0)
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

        static unsafe BOOL ToDictionaryProc(HWND hwnd, WCHAR* propName, HANDLE propVal, ULONG_PTR lParam)
        {
            GCHandle handle = GCHandle.FromIntPtr((nint)lParam);
            if (!handle.IsAllocated)
            { return FALSE; }
            object? obj = handle.Target;
            if (obj == null)
            { return FALSE; }

            Dictionary<string, HANDLE> childList = (Dictionary<string, HANDLE>)obj;
            childList.Add(new string(propName), propVal);

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
    }
}
