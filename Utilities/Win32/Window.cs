using System.Collections;
using System.Diagnostics;
using System.Globalization;
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
            HWND result = User32.SetParent(_handle, newParent);
            if (result == HWND.Zero)
            { throw WindowsException.Get(); }
            return result;
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public HWND ParentOrOwner
        {
            get
            {
                HWND result = User32.GetParent(_handle);
                if (result == HWND.Zero)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public HWND Parent
        {
            set => SetParent(value);
            get => User32.GetAncestor(_handle, GA.GA_PARENT);
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public HWND Owner
        {
            get
            {
                HWND result = User32.GetWindow(_handle, GW.GW_OWNER);
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

        static BOOL EnumChildWindowsProc(HWND hwnd, LPARAM lParam)
        {
            GCHandle handle = GCHandle.FromIntPtr(lParam);
            if (!handle.IsAllocated)
            { return FALSE; }
            object? obj = handle.Target;
            if (obj == null)
            { return FALSE; }

            List<HWND> childList = (List<HWND>)obj;
            childList.Add(hwnd);

            return TRUE;
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public HWND[] Childs
        {
            get
            {
                List<HWND> result = new();
                GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
                _ = User32.EnumChildWindows(_handle, &EnumChildWindowsProc, GCHandle.ToIntPtr(handle));
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
                RECT rect;
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

        string GetDebuggerDisplay() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        /// <exception cref="WindowsException"/>
        public void Animate(uint time, uint flags)
        {
            if (User32.AnimateWindow(_handle, time, flags) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public static void ArrangeIconicWindows(Window window)
        {
            if (User32.ArrangeIconicWindows(window._handle) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public static RECT CalculatePopupWindowPosition(POINT anchorPoint, SIZE windowSize, uint flags)
        {
            RECT result;
            if (User32.CalculatePopupWindowPosition(&anchorPoint, &windowSize, flags, null, &result) != TRUE)
            { throw WindowsException.Get(); }
            return result;
        }

        /// <exception cref="WindowsException"/>
        unsafe public static RECT CalculatePopupWindowPosition(POINT anchorPoint, SIZE windowSize, uint flags, RECT excludeRect)
        {
            RECT result;
            if (User32.CalculatePopupWindowPosition(&anchorPoint, &windowSize, flags, &excludeRect, &result) != TRUE)
            { throw WindowsException.Get(); }
            return result;
        }

        static BOOL EnumThreadsProc(HWND hwnd, LPARAM lParam)
        {
            GCHandle handle = GCHandle.FromIntPtr(lParam);
            if (!handle.IsAllocated)
            { return FALSE; }
            object? obj = handle.Target;
            if (obj == null)
            { return FALSE; }

            List<HWND> list = (List<HWND>)obj;
            list.Add(hwnd);

            return TRUE;
        }
        unsafe public static HWND[] GetThreadWindows(uint threadId)
        {
            List<HWND> result = new();
            GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
            _ = User32.EnumThreadWindows(threadId, &EnumThreadsProc, GCHandle.ToIntPtr(handle));
            HWND[] _result = result.ToArray();
            handle.Free();
            return _result;
        }

        static readonly Dictionary<uint, string> EnumWindowsProcErrors = new()
        {
            { 1, $"{nameof(GCHandle)} of {nameof(List<HWND>)} is deallocated" },
            { 2, $"Value of {nameof(List<HWND>)} is null" },
        };
        static BOOL EnumWindowsProc(HWND hwnd, LPARAM lParam)
        {
            GCHandle handle = GCHandle.FromIntPtr(lParam);
            if (!handle.IsAllocated)
            {
                Kernel32.SetLastError(WindowsException.APP_CODE_MASK | 1);
                return FALSE;
            }
            object? obj = handle.Target;
            if (obj == null)
            {
                Kernel32.SetLastError(WindowsException.APP_CODE_MASK | 2);
                return FALSE;
            }

            List<HWND> list = (List<HWND>)obj;
            list.Add(hwnd);

            return TRUE;
        }
        /// <exception cref="WindowsException"/>
        unsafe public static HWND[] GetWindows()
        {
            List<HWND> result = new();
            GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
            if (User32.EnumWindows(&EnumWindowsProc, GCHandle.ToIntPtr(handle)) == 0)
            {
                handle.Free();
                throw WindowsException.Get(EnumWindowsProcErrors);
            }
            HWND[] _result = result.ToArray();
            handle.Free();
            return _result;
        }

        public static HWND GetDesktop() => User32.GetDesktopWindow();

        public static HWND GetActive() => User32.GetForegroundWindow();

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public TITLEBARINFO TitleBarInfo
        {
            get
            {
                TITLEBARINFO result = TITLEBARINFO.Default;
                if (User32.GetTitleBarInfo(_handle, &result) == 0)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsMinimized => User32.IsIconic(_handle) != 0;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsMaximized => User32.IsZoomed(_handle) != 0;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsVisible => User32.IsWindowVisible(_handle) != 0;

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public unsafe string Text
        {
            get
            {
                const int MaxWidth = 64;
                fixed (WCHAR* buffer = new string('\0', MaxWidth))
                {
                    int length = User32.GetWindowTextW(_handle, buffer, MaxWidth);
                    return new string(buffer, 0, length);
                }
            }
            set
            {
                fixed (WCHAR* buffer = value)
                {
                    if (User32.SetWindowTextW(_handle, buffer) == 0)
                    { throw WindowsException.Get(); }
                }
            }
        }

        /// <exception cref="WindowsException"/>
        public void OpenIcon()
        {
            if (User32.OpenIcon(_handle) == 0)
            { throw WindowsException.Get(); }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public HWND TopChild => User32.GetTopWindow(_handle);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static HWND TopWindow => User32.GetTopWindow(HWND.Zero);

        /// <exception cref="WindowsException"/>
        public void BringToTop()
        {
            if (User32.BringWindowToTop(_handle) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public static void BringToTop(HWND handle)
        {
            if (User32.BringWindowToTop(handle) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public DWORD ThreadId
        {
            get
            {
                DWORD result = User32.GetWindowThreadProcessId(_handle, null);
                if (result == 0)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public (DWORD ThreadId, DWORD ProcessId) ThreadProcessId
        {
            get
            {
                DWORD processId = default;
                DWORD threadId = User32.GetWindowThreadProcessId(_handle, &processId);
                if (threadId == 0)
                { throw WindowsException.Get(); }
                return (threadId, processId);
            }
        }

        /// <exception cref="WindowsException"/>
        public void Move(int x, int y, int width, int height, bool repaint = true)
        {
            if (User32.MoveWindow(_handle, x, y, width, height, repaint ? TRUE : FALSE) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public void SetPos(int x, int y, int width, int height, uint flags = 0)
        {
            if (User32.SetWindowPos(_handle, IntPtr.Zero, x, y, width, height, flags | SWP.SWP_NOZORDER) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public RECT Rect
        {
            get
            {
                RECT result = default;
                if (User32.GetWindowRect(_handle, &result) == 0)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        unsafe public POINT Position
        {
            get
            {
                RECT result = default;
                if (User32.GetWindowRect(_handle, &result) == 0)
                { throw WindowsException.Get(); }
                return result.Position;
            }
            set
            {
                if (User32.SetWindowPos(_handle, IntPtr.Zero, value.X, value.Y, 0, 0, SWP.SWP_NOZORDER | SWP.SWP_NOSIZE) == 0)
                { throw WindowsException.Get(); }
            }
        }

        unsafe public SIZE Size
        {
            get
            {
                RECT result = default;
                if (User32.GetWindowRect(_handle, &result) == 0)
                { throw WindowsException.Get(); }
                return new Size() { cx = result.Width, cy = result.Height };
            }
            set
            {
                if (User32.SetWindowPos(_handle, IntPtr.Zero, 0, 0, value.cx, value.cy, SWP.SWP_NOZORDER | SWP.SWP_NOMOVE) == 0)
                { throw WindowsException.Get(); }
            }
        }

        unsafe public string ModuleFileName
        {
            get
            {
                const uint MaxLength = 128;
                fixed (WCHAR* buffer = new string('\0', (int)MaxLength))
                {
                    uint length = User32.GetWindowModuleFileNameW(_handle, buffer, MaxLength);
                    return new string(buffer, 0, (int)length);
                }
            }
        }
    }
}
