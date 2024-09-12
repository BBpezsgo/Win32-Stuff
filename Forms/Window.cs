using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Win32.Forms;

public delegate void WindowEvent<TWindow, TArgs>(TWindow sender, TArgs args) where TWindow : Window;
public delegate void ControlNotification<TControl>(TControl sender) where TControl : Window;

[SupportedOSPlatform("windows")]
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class Window :
    IEquatable<Window?>,
    IEquatable<HWND>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public HWND Handle { get; protected set; }

    protected Window() => Handle = HWND.Zero;

    protected Window(HWND handle) => Handle = handle;

    public unsafe Window(
        string className,
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
        fixed (char* classNamePtr = className)
        {
            Handle = User32.CreateWindowExW(
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
    public static implicit operator HWND(Window window) => window.Handle;

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public Window ParentOrOwner
    {
        get
        {
            HWND result = User32.GetParent(Handle);
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
            if (User32.SetParent(Handle, value.Handle) == HWND.Zero)
            { throw WindowsException.Get(); }
        }
        get => new(User32.GetAncestor(Handle, GetAncestorFlags.Parent));
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public Window Root => new(User32.GetAncestor(Handle, GetAncestorFlags.Root));

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public Window Owner
    {
        get
        {
            HWND result = User32.GetWindow(Handle, GetWindowFlags.Owner);
            if (result == HWND.Zero)
            { throw WindowsException.Get(); }
            return new Window(result);
        }
    }

    public bool IsChildOf(HWND parent)
    {
        BOOL result = User32.IsChild(parent, Handle);
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
    public unsafe ImmutableArray<Window> Children
    {
        get
        {
            List<HWND> result = new();
            GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
            _ = User32.EnumChildWindows(Handle, &EnumChildWindowsProc, GCHandle.ToIntPtr(handle));
            handle.Free();
            return result.Select(handle => new Window(handle)).ToImmutableArray();
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe RECT ClientRect
    {
        get
        {
            RECT rect;
            if (User32.GetClientRect(Handle, &rect) == 0)
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
        BOOL result = User32.UpdateWindow(Handle);
        return result != FALSE;
    }

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

    string GetDebuggerDisplay() => $"{ClassName} ({ToString()})";

    /// <exception cref="WindowsException"/>
    public void Animate(uint time, AnimateWindowFlags flags)
    {
        if (User32.AnimateWindow(Handle, time, flags) == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public void ArrangeIconicChildWindows()
    {
        if (User32.ArrangeIconicWindows(Handle) == 0)
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

    public static unsafe ImmutableArray<Window> GetThreadWindows(uint threadId)
    {
        List<HWND> result = new();
        GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
        _ = User32.EnumThreadWindows(threadId, &EnumThreadsProc, GCHandle.ToIntPtr(handle));
        handle.Free();
        return result.Select(handle => new Window(handle)).ToImmutableArray();
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
            Kernel32.SetLastError(WindowsException.AppCodeMask | 1);
            return FALSE;
        }
        object? obj = handle.Target;
        if (obj == null)
        {
            Kernel32.SetLastError(WindowsException.AppCodeMask | 2);
            return FALSE;
        }

        List<HWND> list = (List<HWND>)obj;
        list.Add(hwnd);

        return TRUE;
    }

    /// <exception cref="WindowsException"/>
    public static unsafe ImmutableArray<Window> Windows
    {
        get
        {
            List<HWND> result = new();
            GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
            if (User32.EnumWindows(&EnumWindowsProc, GCHandle.ToIntPtr(handle)) == 0)
            {
                handle.Free();
                throw WindowsException.Get(EnumWindowsProcErrors);
            }
            handle.Free();
            return result.Select(handle => new Window(handle)).ToImmutableArray();
        }
    }

    public static Window Desktop => new(User32.GetDesktopWindow());

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
            BOOL _ = User32.SetForegroundWindow(value?.Handle ?? HWND.Zero);
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe TITLEBARINFO TitleBarInfo
    {
        get
        {
            TITLEBARINFO result = TITLEBARINFO.Create();
            if (User32.GetTitleBarInfo(Handle, &result) == 0)
            { throw WindowsException.Get(); }
            return result;
        }
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public bool IsMinimized => User32.IsIconic(Handle) != FALSE;

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public bool IsMaximized => User32.IsZoomed(Handle) != FALSE;

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public bool IsVisible => User32.IsWindowVisible(Handle) != FALSE;

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe string Text
    {
        get
        {
            const int MaxWidth = 64;
            fixed (WCHAR* buffer = new string('\0', MaxWidth))
            {
                int length = User32.GetWindowTextW(Handle, buffer, MaxWidth);
                return new string(buffer, 0, length);
            }
        }
        set
        {
            fixed (WCHAR* buffer = value)
            {
                if (User32.SetWindowTextW(Handle, buffer) == 0)
                { throw WindowsException.Get(); }
            }
        }
    }

    /// <exception cref="WindowsException"/>
    public void OpenIcon()
    {
        if (User32.OpenIcon(Handle) == 0)
        { throw WindowsException.Get(); }
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public Window? TopChild
    {
        get
        {
            HWND result = User32.GetTopWindow(Handle);
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
        if (User32.BringWindowToTop(Handle) == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe DWORD ThreadId
    {
        get
        {
            DWORD result = User32.GetWindowThreadProcessId(Handle, null);
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
            DWORD threadId = User32.GetWindowThreadProcessId(Handle, &processId);
            if (threadId == 0)
            { throw WindowsException.Get(); }
            return (threadId, processId);
        }
    }

    /// <exception cref="WindowsException"/>
    public void Move(int x, int y, int width, int height, bool repaint = true)
    {
        if (User32.MoveWindow(Handle, x, y, width, height, repaint ? TRUE : FALSE) == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public void SetPos(int x, int y, int width, int height, SetWindowPosFlags flags = 0)
    {
        if (User32.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, flags | SetWindowPosFlags.NoZOrder) == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe RECT Rect
    {
        get
        {
            RECT result = default;
            if (User32.GetWindowRect(Handle, &result) == 0)
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
            if (User32.GetWindowRect(Handle, &result) == 0)
            { throw WindowsException.Get(); }
            return result.Position;
        }
        set
        {
            if (User32.SetWindowPos(Handle, IntPtr.Zero, value.X, value.Y, 0, 0, SetWindowPosFlags.NoZOrder | SetWindowPosFlags.NoSize) == 0)
            { throw WindowsException.Get(); }
        }
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe SIZE Size
    {
        get
        {
            RECT result = default;
            if (User32.GetWindowRect(Handle, &result) == 0)
            { throw WindowsException.Get(); }
            return new Size() { Width = result.Width, Height = result.Height };
        }
        set
        {
            if (User32.SetWindowPos(Handle, IntPtr.Zero, 0, 0, value.Width, value.Height, SetWindowPosFlags.NoZOrder | SetWindowPosFlags.NoMove) == 0)
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
                uint length = User32.GetWindowModuleFileNameW(Handle, buffer, MaxLength);
                return new string(buffer, 0, (int)length);
            }
        }
    }

    /// <exception cref="WindowsException"/>
    public void ShowOwnedPopups(bool show)
    {
        if (User32.ShowOwnedPopups(Handle, show ? TRUE : FALSE) == 0)
        { throw WindowsException.Get(); }
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public Window? LastActivePopup
    {
        get
        {
            HWND result = User32.GetLastActivePopup(Handle);
            if (result == Handle)
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
            if (User32.SetActiveWindow(value?.Handle ?? HWND.Zero) == HWND.Zero)
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
            if (User32.SetFocus(value?.Handle ?? HWND.Zero) == HWND.Zero)
            { throw WindowsException.Get(); }
        }
    }

    /// <exception cref="WindowsException"/>
    public unsafe void TileWindows(uint how, HWND[]? windows)
    {
        if (windows == null)
        {
            if (User32.TileWindows(Handle, how, null, 0, null) == 0)
            { throw WindowsException.Get(); }
        }
        else
        {
            fixed (HWND* windowsPtr = windows)
            {
                if (User32.TileWindows(Handle, how, null, (uint)windows.Length, windowsPtr) == 0)
                { throw WindowsException.Get(); }
            }
        }
    }

    /// <exception cref="WindowsException"/>
    public unsafe void TileWindows(uint how, RECT rect, HWND[]? windows)
    {
        if (windows == null)
        {
            if (User32.TileWindows(Handle, how, &rect, 0, null) == 0)
            { throw WindowsException.Get(); }
        }
        else
        {
            fixed (HWND* windowsPtr = windows)
            {
                if (User32.TileWindows(Handle, how, &rect, (uint)windows.Length, windowsPtr) == 0)
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
        HWND handle = User32.ChildWindowFromPoint(Handle, point);
        if (handle == HWND.Zero)
        { return null; }
        return new Window(handle);
    }

    public Window? ChildFromPoint(POINT point, ChildWindowFromPointFlags flags)
    {
        HWND handle = User32.ChildWindowFromPointEx(Handle, point, flags);
        if (handle == HWND.Zero)
        { return null; }
        return new Window(handle);
    }

    public unsafe bool ClientToScreen(ref POINT point)
    {
        int result = User32.ClientToScreen(Handle, (POINT*)Unsafe.AsPointer(ref point));
        return result != FALSE;
    }

    public unsafe bool ScreenToClient(ref POINT point)
    {
        int result = User32.ScreenToClient(Handle, (POINT*)Unsafe.AsPointer(ref point));
        return result != FALSE;
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public bool IsEnabled
    {
        get => User32.IsWindowEnabled(Handle) != FALSE;
        set => _ = User32.EnableWindow(Handle, value ? TRUE : FALSE);
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public WindowPropertiesContainer Properties => new(Handle);

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe string ClassName
    {
        get
        {
            const int BUFFER_SIZE = 64;
            fixed (WCHAR* buffer = new string('\0', BUFFER_SIZE))
            {
                int length = User32.GetClassNameW(Handle, buffer, BUFFER_SIZE);
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
                UINT n = User32.RealGetWindowClassW(Handle, bufferPtr, (UINT)BufferSize);
                if (n == 0)
                { throw WindowsException.Get(); }
                return new string(bufferPtr, 0, (int)n);
            }
        }
    }

    public static bool operator ==(Window? window, HWND handle)
    {
        if (window is null) return handle == 0;
        return window.Handle == handle;
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
    public bool Equals(Window? other) => other is not null && Handle == other.Handle;
    /// <inheritdoc/>
    public bool Equals(HWND other) => Handle == other;

    /// <inheritdoc/>
    public override int GetHashCode() => Handle.GetHashCode();

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe WindowInfo Info
    {
        get
        {
            WindowInfo info = WindowInfo.Create();
            if (User32.GetWindowInfo(Handle, &info) == 0)
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
            HWND window = User32.FindWindowExW(Handle, HWND.Zero, classNamePtr, windowNamePtr);
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
        _ = User32.EnumChildWindows(Handle, &EnumChildWindowsProc2, GCHandle.ToIntPtr(handle));
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
        _ = User32.EnumChildWindows(Handle, &EnumChildWindowsProc3, GCHandle.ToIntPtr(handle));
        handle.Free();
    }

    /// <exception cref="Gdi32.GdiException"/>
    public Gdi32.DisplayDC GetDC()
    {
        HDC dc = User32.GetWindowDC(Handle);
        if (dc == HDC.Zero)
        { throw new Gdi32.GdiException($"Failed to get DC ({nameof(User32.GetWindowDC)}) of window {this}"); }
        return new Gdi32.DisplayDC(dc, Handle);
    }

    /// <exception cref="Gdi32.GdiException"/>
    public Gdi32.DisplayDC GetClientDC()
    {
        HDC dc = User32.GetDC(Handle);
        if (dc == HDC.Zero)
        { throw new Gdi32.GdiException($"Failed to get DC ({nameof(User32.GetDC)}) of window {this}"); }
        return new Gdi32.DisplayDC(dc, Handle);
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
}
