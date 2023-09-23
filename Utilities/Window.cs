using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32.Utilities
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class Window
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HWND _handle;

        public HWND Handle
        {
            get => _handle;
            protected set => _handle = value;
        }

        public Window() => _handle = HWND.Zero;

        public Window(HWND handle) => _handle = handle;

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

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public HWND Parent
        {
            set => SetParent(value);
            get
            {
                HWND result = User32.GetParent(Handle);
                if (result == HWND.Zero)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        public bool IsChildOf(HWND parent)
        {
            BOOL result = User32.IsChild(parent, _handle);
            return result != FALSE;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public HWND[] Childs
        {
            get
            {

                List<HWND> result = new();
                GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
                _ = User32.EnumChildWindows(_handle, &EnumWindowsProc, GCHandle.ToIntPtr(handle));
                HWND[] _result = result.ToArray();
                handle.Free();
                return _result;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public RECT ClientRect
        {
            get
            {
                RECT rect = new();
                if (User32.GetClientRect(_handle, &rect) == 0)
                { throw WindowsException.Get(); }
                return rect;
            }
        }

        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is <see langword="true"/>.
        /// </para>
        /// <para>
        /// If the function fails, the return value is <see langword="false"/>.
        /// </para>
        /// </returns>
        public bool UpdateWindow()
        {
            BOOL result = User32.UpdateWindow(_handle);
            return result != FALSE;
        }

        static BOOL EnumWindowsProc(IntPtr hwnd, IntPtr lParam)
        {
            GCHandle handle = GCHandle.FromIntPtr(lParam);
            if (!handle.IsAllocated) return FALSE;
            object? obj = handle.Target;
            if (obj == null) return FALSE;

            List<IntPtr> childList = (List<IntPtr>)obj;
            childList.Add(hwnd);

            return TRUE;
        }

        string GetDebuggerDisplay() => "0x" + _handle.ToString("x").PadLeft(16, '0');
    }
}
