using System.Diagnostics;

namespace Win32
{
    public class WindowPropertiesContainer
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HWND _handle;
        static readonly Dictionary<HWND, Dictionary<string, HANDLE>> propEnumRequests = new();

        public WindowPropertiesContainer(HWND handle) => _handle = handle;

        /// <exception cref="WindowsException"/>
        unsafe public HANDLE this[string property]
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

        unsafe public bool Remove(string property)
        {
            HANDLE result;
            fixed (WCHAR* propertyPtr = property)
            { result = User32.RemovePropW(_handle, propertyPtr); }
            return result != HANDLE.Zero;
        }

        unsafe static BOOL EnumPropsProc(HWND hwnd, WCHAR* propName, HANDLE propVal)
        {
            if (!propEnumRequests.TryGetValue(hwnd, out Dictionary<string, HANDLE>? list))
            { return FALSE; }

            list.Add(new string(propName), propVal);
            return TRUE;
        }

        unsafe public Dictionary<string, HANDLE> ToDictionary()
        {
            Dictionary<string, HANDLE> result = new();
            propEnumRequests.Add(_handle, result);
            _ = User32.EnumPropsW(_handle, &EnumPropsProc);
            propEnumRequests.Remove(_handle);
            return result;
        }
    }
}
