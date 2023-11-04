using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Win32
{
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public class Window : IEquatable<Window?>
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

        public static explicit operator Window(HWND handle) => new(handle);

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
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
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
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public HWND Parent
        {
            set => SetParent(value);
            get => User32.GetAncestor(_handle, GA.PARENT);
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public HWND Root => User32.GetAncestor(_handle, GA.ROOT);

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public HWND Owner
        {
            get
            {
                HWND result = User32.GetWindow(_handle, GW.OWNER);
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
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public Window[] Children
        {
            get
            {
                List<HWND> result = new();
                GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
                _ = User32.EnumChildWindows(_handle, &EnumChildWindowsProc, GCHandle.ToIntPtr(handle));
                HWND[] _result = result.ToArray();
                handle.Free();
                Window[] _result2 = new Window[_result.Length];
                for (int i = 0; i < _result.Length; i++)
                {
                    _result2[i] = (Window)_result[i];
                }
                return _result2;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
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
        public bool Update()
        {
            BOOL result = User32.UpdateWindow(_handle);
            return result != FALSE;
        }

        public override string ToString() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        /// <exception cref="WindowsException"/>
        public void Animate(uint time, uint flags)
        {
            if (User32.AnimateWindow(_handle, time, flags) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public void ArrangeIconicChildWindows()
        {
            if (User32.ArrangeIconicWindows(_handle) == 0)
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
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public TITLEBARINFO TitleBarInfo
        {
            get
            {
                TITLEBARINFO result = TITLEBARINFO.Create();
                if (User32.GetTitleBarInfo(_handle, &result) == 0)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public bool IsMinimized => User32.IsIconic(_handle) != 0;

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public bool IsMaximized => User32.IsZoomed(_handle) != 0;

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public bool IsVisible => User32.IsWindowVisible(_handle) != 0;

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
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

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public HWND TopChild => User32.GetTopWindow(_handle);

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public static HWND TopWindow => User32.GetTopWindow(HWND.Zero);

        /// <exception cref="WindowsException"/>
        public void BringToTop()
        {
            if (User32.BringWindowToTop(_handle) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
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
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
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
            if (User32.SetWindowPos(_handle, IntPtr.Zero, x, y, width, height, flags | SWP.NOZORDER) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
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

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
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
                if (User32.SetWindowPos(_handle, IntPtr.Zero, value.X, value.Y, 0, 0, SWP.NOZORDER | SWP.NOSIZE) == 0)
                { throw WindowsException.Get(); }
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public SIZE Size
        {
            get
            {
                RECT result = default;
                if (User32.GetWindowRect(_handle, &result) == 0)
                { throw WindowsException.Get(); }
                return new Size() { Width = result.Width, Height = result.Height };
            }
            set
            {
                if (User32.SetWindowPos(_handle, IntPtr.Zero, 0, 0, value.Width, value.Height, SWP.NOZORDER | SWP.NOMOVE) == 0)
                { throw WindowsException.Get(); }
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
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

        /// <exception cref="WindowsException"/>
        public void ShowOwnedPopups(bool show)
        {
            if (User32.ShowOwnedPopups(_handle, show ? TRUE : FALSE) == 0)
            { throw WindowsException.Get(); }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public HWND? LastActivePopup
        {
            get
            {
                HWND result = User32.GetLastActivePopup(_handle);
                if (result == _handle)
                { return null; }
                return result;
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public static Window? MouseCaptureBy
        {
            get
            {
                HWND handle = User32.GetCapture();
                if (handle == HWND.Zero)
                { return null; }
                return new Window(handle);
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public static Window? ActiveWindow
        {
            get
            {
                HWND handle = User32.GetActiveWindow();
                if (handle == HWND.Zero)
                { return null; }
                return new Window(handle);
            }
        }

        /// <exception cref="WindowsException"/>
        unsafe public void TileWindows(uint how, HWND[]? windows)
        {
            if (windows == null)
            {
                if (User32.TileWindows(_handle, how, null, 0, null) == 0)
                { throw WindowsException.Get(); }
            }
            else
            {
                fixed (HWND* windowsPtr = windows)
                {
                    if (User32.TileWindows(_handle, how, null, (uint)windows.Length, windowsPtr) == 0)
                    { throw WindowsException.Get(); }
                }
            }
        }

        /// <exception cref="WindowsException"/>
        unsafe public void TileWindows(uint how, RECT rect, HWND[]? windows)
        {
            if (windows == null)
            {
                if (User32.TileWindows(_handle, how, &rect, 0, null) == 0)
                { throw WindowsException.Get(); }
            }
            else
            {
                fixed (HWND* windowsPtr = windows)
                {
                    if (User32.TileWindows(_handle, how, &rect, (uint)windows.Length, windowsPtr) == 0)
                    { throw WindowsException.Get(); }
                }
            }
        }

        public static Window? WindowFromPoint(POINT point)
        {
            HWND handle = User32.WindowFromPoint(point);
            if (handle == HWND.Zero)
            { return null; }
            return new Window(handle);
        }

        public Window? ChildFromPoint(POINT point)
        {
            HWND handle = User32.ChildWindowFromPoint(_handle, point);
            if (handle == HWND.Zero)
            { return null; }
            return new Window(handle);
        }

        public Window? ChildFromPoint(POINT point, uint flags)
        {
            HWND handle = User32.ChildWindowFromPointEx(_handle, point, flags);
            if (handle == HWND.Zero)
            { return null; }
            return new Window(handle);
        }

        unsafe public bool ClientToScreen(ref POINT point)
        {
            POINT* pointPtr = (POINT*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref point);
            int result = User32.ClientToScreen(_handle, pointPtr);
            return result != 0;
        }

        unsafe public bool ScreenToClient(ref POINT point)
        {
            POINT* pointPtr = (POINT*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref point);
            int result = User32.ScreenToClient(_handle, pointPtr);
            return result != 0;
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public bool Enabled
        {
            get => User32.IsWindowEnabled(Handle) != FALSE;
            set => _ = User32.EnableWindow(Handle, value ? TRUE : FALSE);
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public WindowPropertiesContainer Properties => new(_handle);

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public string Win32ClassName
        {
            get
            {
                const int BUFFER_SIZE = 64;
                fixed (WCHAR* buffer = new string('\0', BUFFER_SIZE))
                {
                    int length = User32.GetClassNameW(_handle, buffer, BUFFER_SIZE);
                    if (length == 0)
                    { throw WindowsException.Get(); }
                    return new string(buffer, 0, length);
                }
            }
        }

        public static bool operator ==(Window window, HWND handle) => window.Handle == handle;
        public static bool operator !=(Window window, HWND handle) => window.Handle != handle;
        public static bool operator ==(HWND handle, Window window) => window.Handle == handle;
        public static bool operator !=(HWND handle, Window window) => window.Handle != handle;

        public override bool Equals(object? obj) => obj is Window other && this.Equals(other);
        public bool Equals(Window? other) => other is not null && _handle.Equals(other._handle);

        public override int GetHashCode() => HashCode.Combine(_handle);

        /// <exception cref="WindowsException"/>
        unsafe public WINDOWINFO Info
        {
            get
            {
                WINDOWINFO info = WINDOWINFO.Create();
                if (User32.GetWindowInfo(_handle, &info) == 0)
                { throw WindowsException.Get(); }
                return info;
            }
        }

        public LRESULT SendMessage(uint msg, WPARAM wParam, LPARAM lParam) => User32.SendMessage(Handle, msg, wParam, lParam);

        unsafe public static Window? Find(string? className, string? windowName)
        {
            fixed (WCHAR* classNamePtr = className)
            fixed (WCHAR* windowNamePtr = windowName)
            {
                HWND window = User32.FindWindowW();
                if (window == HWND.Zero)
                { return null; }
                return new Window(window);
            }
        }

        unsafe public Window? FindChild(string? className, string? windowName)
        {
            fixed (WCHAR* classNamePtr = className)
            fixed (WCHAR* windowNamePtr = windowName)
            {
                HWND window = User32.FindWindowExA(_handle, HWND.Zero, classNamePtr, windowNamePtr);
                if (window == HWND.Zero)
                { return null; }
                return new Window(window);
            }
        }

        static BOOL EnumChildWindowsProc2(HWND hwnd, LPARAM lParam)
        {
            GCHandle handle = GCHandle.FromIntPtr(lParam);
            if (!handle.IsAllocated)
            { return FALSE; }
            object? obj = handle.Target;
            if (obj == null)
            { return FALSE; }
            if (obj is not Action<Window> callback)
            { return FALSE; }

            callback.Invoke(new Window(hwnd));

            return TRUE;
        }
        unsafe public void EnumChildren(Action<Window> callback)
        {
            GCHandle handle = GCHandle.Alloc(callback, GCHandleType.Weak);
            _ = User32.EnumChildWindows(_handle, &EnumChildWindowsProc2, GCHandle.ToIntPtr(handle));
            handle.Free();
        }

        static BOOL EnumChildWindowsProc3(HWND hwnd, LPARAM lParam)
        {
            GCHandle handle = GCHandle.FromIntPtr(lParam);
            if (!handle.IsAllocated)
            { return FALSE; }
            object? obj = handle.Target;
            if (obj == null)
            { return FALSE; }
            if (obj is not Func<Window, bool> callback)
            { return FALSE; }

            bool res = callback.Invoke(new Window(hwnd));
            return res ? TRUE : FALSE;
        }
        unsafe public void EnumChildren(Action<Window, bool> callback)
        {
            GCHandle handle = GCHandle.Alloc(callback, GCHandleType.Weak);
            _ = User32.EnumChildWindows(_handle, &EnumChildWindowsProc3, GCHandle.ToIntPtr(handle));
            handle.Free();
        }

        /// <exception cref="NotWindowsException"/>
        public DisplayDC GetDC()
        {
            HDC dc = User32.GetWindowDC(_handle);
            if (dc == HDC.Zero)
            { throw new NotWindowsException($"Failed to get DC ({nameof(User32.GetWindowDC)}) of window {this}"); }
            return new DisplayDC(dc, _handle);
        }

        /// <exception cref="NotWindowsException"/>
        public DisplayDC GetClientDC()
        {
            HDC dc = User32.GetDC(_handle);
            if (dc == HDC.Zero)
            { throw new NotWindowsException($"Failed to get DC ({nameof(User32.GetDC)}) of window {this}"); }
            return new DisplayDC(dc, _handle);
        }

        public static DisplayDC GetPrimaryDisplayDC()
        {
            HDC dc = User32.GetWindowDC(HWND.Zero);
            if (dc == HDC.Zero)
            { throw new NotWindowsException($"Failed to get DC ({nameof(User32.GetWindowDC)}) of the primary display"); }
            return new DisplayDC(dc, HWND.Zero);
        }
    }
}
