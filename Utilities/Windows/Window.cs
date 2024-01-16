using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class Window :
        IEquatable<Window?>,
        IEquatable<HWND>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HWND _handle;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public HWND Handle
        {
            get => _handle;
            protected set => _handle = value;
        }

        protected Window() => _handle = HWND.Zero;

        protected Window(HWND handle) => _handle = handle;

        public unsafe Window(
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
        public static implicit operator HWND(Window window) => window._handle;

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public Window ParentOrOwner
        {
            get
            {
                HWND result = User32.GetParent(_handle);
                if (result == HWND.Zero)
                { throw WindowsException.Get(); }
                return new Window(result);
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public Window Parent
        {
            set
            {
                if (User32.SetParent(_handle, value._handle) == HWND.Zero)
                { throw WindowsException.Get(); }
            }
            get => new(User32.GetAncestor(_handle, GetAncestorFlags.PARENT));
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public Window Root => new(User32.GetAncestor(_handle, GetAncestorFlags.ROOT));

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public Window Owner
        {
            get
            {
                HWND result = User32.GetWindow(_handle, GetWindowFlags.OWNER);
                if (result == HWND.Zero)
                { throw WindowsException.Get(); }
                return new Window(result);
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

        /// <exception cref="WindowsException"/>
        public void Destroy()
        {
            if (Handle == HWND.Zero) return;
            if (User32.DestroyWindow(Handle) == FALSE)
            { throw WindowsException.Get(); }
            Handle = HWND.Zero;
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe IReadOnlyCollection<Window> Children
        {
            get
            {
                List<HWND> result = new();
                GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
                _ = User32.EnumChildWindows(_handle, &EnumChildWindowsProc, GCHandle.ToIntPtr(handle));
                HWND[] _result = result.ToArray();
                handle.Free();
                return Window.ConvertArray(_result);
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe RECT ClientRect
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

        string GetDebuggerDisplay() => $"{ClassName} ({ToString()})";

        /// <exception cref="WindowsException"/>
        public void Animate(uint time, AnimateWindowFlags flags)
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
        public static unsafe RECT CalculatePopupWindowPosition(POINT anchorPoint, SIZE windowSize, uint flags)
        {
            RECT result;
            if (User32.CalculatePopupWindowPosition(&anchorPoint, &windowSize, flags, null, &result) != TRUE)
            { throw WindowsException.Get(); }
            return result;
        }

        /// <exception cref="WindowsException"/>
        public static unsafe RECT CalculatePopupWindowPosition(POINT anchorPoint, SIZE windowSize, uint flags, RECT excludeRect)
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

        public static unsafe Window[] GetThreadWindows(uint threadId)
        {
            List<HWND> result = new();
            GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
            _ = User32.EnumThreadWindows(threadId, &EnumThreadsProc, GCHandle.ToIntPtr(handle));
            HWND[] _result = result.ToArray();
            handle.Free();
            return Window.ConvertArray(_result);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
        public static unsafe Window[] GetWindows()
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
            return Window.ConvertArray(_result);
        }

        public static Window GetDesktop() => new(User32.GetDesktopWindow());

        public static Window? ForegroundWindow
        {
            get
            {
                HWND handle = User32.GetForegroundWindow();
                if (handle == HWND.Zero)
                { return null; }
                return new Window(handle);
            }
            set
            {
                BOOL _ = User32.SetForegroundWindow(value?._handle ?? HWND.Zero);
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe TITLEBARINFO TitleBarInfo
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
        public bool IsMinimized => User32.IsIconic(_handle) != FALSE;

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public bool IsMaximized => User32.IsZoomed(_handle) != FALSE;

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public bool IsVisible => User32.IsWindowVisible(_handle) != FALSE;

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
        public Window? TopChild
        {
            get
            {
                HWND result = User32.GetTopWindow(_handle);
                if (result == HWND.Zero) return null;
                return new Window(result);
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public static Window? TopWindow
        {
            get
            {
                HWND result = User32.GetTopWindow(HWND.Zero);
                if (result == HWND.Zero) return null;
                return new Window(result);
            }
        }

        /// <exception cref="WindowsException"/>
        public void BringToTop()
        {
            if (User32.BringWindowToTop(_handle) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe DWORD ThreadId
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
        public unsafe (DWORD ThreadId, DWORD ProcessId) ThreadProcessId
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
        public void SetPos(int x, int y, int width, int height, SetWindowPosFlags flags = 0)
        {
            if (User32.SetWindowPos(_handle, IntPtr.Zero, x, y, width, height, flags | SetWindowPosFlags.NOZORDER) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe RECT Rect
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
        public unsafe POINT Position
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
                if (User32.SetWindowPos(_handle, IntPtr.Zero, value.X, value.Y, 0, 0, SetWindowPosFlags.NOZORDER | SetWindowPosFlags.NOSIZE) == 0)
                { throw WindowsException.Get(); }
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe SIZE Size
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
                if (User32.SetWindowPos(_handle, IntPtr.Zero, 0, 0, value.Width, value.Height, SetWindowPosFlags.NOZORDER | SetWindowPosFlags.NOMOVE) == 0)
                { throw WindowsException.Get(); }
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe string ModuleFileName
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
        public Window? LastActivePopup
        {
            get
            {
                HWND result = User32.GetLastActivePopup(_handle);
                if (result == _handle)
                { return null; }
                return new Window(result);
            }
        }

        /// <exception cref="WindowsException"/>
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
            set
            {
                if (User32.SetActiveWindow(value?._handle ?? HWND.Zero) == HWND.Zero)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public static Window? FocusedWindow
        {
            get
            {
                HWND handle = User32.GetFocus();
                if (handle == HWND.Zero)
                { return null; }
                return new Window(handle);
            }
            set
            {
                if (User32.SetFocus(value?._handle ?? HWND.Zero) == HWND.Zero)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        public unsafe void TileWindows(uint how, HWND[]? windows)
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
        public unsafe void TileWindows(uint how, RECT rect, HWND[]? windows)
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

        public static Window? FromPoint(POINT point)
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

        public Window? ChildFromPoint(POINT point, ChildWindowFromPointFlags flags)
        {
            HWND handle = User32.ChildWindowFromPointEx(_handle, point, flags);
            if (handle == HWND.Zero)
            { return null; }
            return new Window(handle);
        }

        public unsafe bool ClientToScreen(ref POINT point)
        {
            int result = User32.ClientToScreen(_handle, (POINT*)Unsafe.AsPointer(ref point));
            return result != FALSE;
        }

        public unsafe bool ScreenToClient(ref POINT point)
        {
            int result = User32.ScreenToClient(_handle, (POINT*)Unsafe.AsPointer(ref point));
            return result != FALSE;
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public bool IsEnabled
        {
            get => User32.IsWindowEnabled(Handle) != FALSE;
            set => _ = User32.EnableWindow(Handle, value ? TRUE : FALSE);
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public WindowPropertiesContainer Properties => new(_handle);

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe string ClassName
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

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe string RealClassName
        {
            get
            {
                const int BufferSize = 64;
                fixed (WCHAR* bufferPtr = new string('\0', BufferSize))
                {
                    UINT n;
                    n = User32.RealGetWindowClassW(_handle, bufferPtr, (UINT)BufferSize);
                    if (n == 0)
                    { throw WindowsException.Get(); }
                    return new string(bufferPtr, 0, (int)n);
                }
            }
        }

        public static bool operator ==(Window? window, HWND handle)
        {
            if (window is null) return handle == 0;
            return window._handle == handle;
        }
        public static bool operator !=(Window? window, HWND handle) => !(window == handle);
        public static bool operator ==(HWND handle, Window? window) => window == handle;
        public static bool operator !=(HWND handle, Window? window) => !(window == handle);
        /// <inheritdoc cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Equality"/>
        public static bool operator ==(Window? a, Window? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }
        /// <inheritdoc cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Inequality"/>
        public static bool operator !=(Window? a, Window? b) => !(a == b);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Window other && this.Equals(other);
        /// <inheritdoc/>
        public bool Equals(Window? other) => other is not null && _handle == other._handle;
        /// <inheritdoc/>
        public bool Equals(HWND other) => _handle == other;

        /// <inheritdoc/>
        public override int GetHashCode() => _handle.GetHashCode();

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe WindowInfo Info
        {
            get
            {
                WindowInfo info = WindowInfo.Create();
                if (User32.GetWindowInfo(_handle, &info) == 0)
                { throw WindowsException.Get(); }
                return info;
            }
        }

        public LRESULT SendMessage(uint msg, WPARAM wParam, LPARAM lParam) => User32.SendMessage(Handle, msg, wParam, lParam);

        public static unsafe Window? Find(string? className, string? windowName)
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

        public unsafe Window? FindChild(string? className, string? windowName)
        {
            fixed (WCHAR* classNamePtr = className)
            fixed (WCHAR* windowNamePtr = windowName)
            {
                HWND window = User32.FindWindowExW(_handle, HWND.Zero, classNamePtr, windowNamePtr);
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

        public unsafe void EnumChildren(Action<Window> callback)
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

        public unsafe void EnumChildren(Action<Window, bool> callback)
        {
            GCHandle handle = GCHandle.Alloc(callback, GCHandleType.Weak);
            _ = User32.EnumChildWindows(_handle, &EnumChildWindowsProc3, GCHandle.ToIntPtr(handle));
            handle.Free();
        }

        /// <exception cref="Gdi32.GdiException"/>
        public Gdi32.DisplayDC GetDC()
        {
            HDC dc = User32.GetWindowDC(_handle);
            if (dc == HDC.Zero)
            { throw new Gdi32.GdiException($"Failed to get DC ({nameof(User32.GetWindowDC)}) of window {this}"); }
            return new Gdi32.DisplayDC(dc, _handle);
        }

        /// <exception cref="Gdi32.GdiException"/>
        public Gdi32.DisplayDC GetClientDC()
        {
            HDC dc = User32.GetDC(_handle);
            if (dc == HDC.Zero)
            { throw new Gdi32.GdiException($"Failed to get DC ({nameof(User32.GetDC)}) of window {this}"); }
            return new Gdi32.DisplayDC(dc, _handle);
        }

        /// <exception cref="Gdi32.GdiException"/>
        public static Gdi32.DisplayDC GetPrimaryDisplayDC()
        {
            HDC dc = User32.GetWindowDC(HWND.Zero);
            if (dc == HDC.Zero)
            { throw new Gdi32.GdiException($"Failed to get DC ({nameof(User32.GetWindowDC)}) of the primary display"); }
            return new Gdi32.DisplayDC(dc, HWND.Zero);
        }

        /// <exception cref="WindowsException"/>
        public static void EndTask(HWND window, bool force)
        {
            if (User32.EndTask(window, FALSE, force ? TRUE : FALSE) == FALSE)
            { throw WindowsException.Get(); }
        }

        public static Window[] ConvertArray(HWND[] handles)
        {
            Window[] result = new Window[handles.Length];
            for (int i = 0; i < handles.Length; i++)
            { result[i] = new Window(handles[i]); }
            return result;
        }
        public static HWND[] ConvertArray(Window?[] handles)
        {
            HWND[] result = new HWND[handles.Length];
            for (int i = 0; i < handles.Length; i++)
            { result[i] = handles[i]?._handle ?? HWND.Zero; }
            return result;
        }
    }
}
