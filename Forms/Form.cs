using System.Text;

namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public sealed class Form : FormUnmanaged, IDisposable
{
    public unsafe delegate void WindowClassSetter(ref WindowClassEx windowClass);

    static int ActiveForms;

    public static bool HasAllocatedForms => ActiveForms > 0;

    bool IsDisposed;
    readonly Win32Class? Class;
    readonly GCHandle GCHandle;

    public event WindowEvent<Form, ResizeEventArgs>? OnResize;
    public event WindowEvent<Form, MenuItemEventArgs>? OnMenuItem;
    public event WindowEvent<Form, ContextMenuEventArgs>? OnContextMenu;
    public event WindowEvent<Form, PaintEventArgs>? OnPaint;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseLeftDoubleClick;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseLeftDown;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseLeftUp;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseMiddleUp;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseMiddleDown;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseMiddleDoubleClick;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseRightDoubleClick;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseRightDown;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseRightUp;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseXDoubleClick;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseXDown;
    public event WindowEvent<Form, MouseButtonEventArgs>? OnMouseXUp;
    public event WindowEvent<Form, MouseEventArgs>? OnMouseHover;
    public event WindowEvent<Form, EmptyArgs>? OnMouseLeave;
    public event WindowEvent<Form, MouseEventArgs>? OnMouseMove;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseLeftDoubleClickNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseLeftDownNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseLeftUpNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseMiddleDoubleClickNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseMiddleDownNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseMiddleUpNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseRightDoubleClickNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseRightDownNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseRightUpNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseXDoubleClickNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseXDownNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseXUpNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseHoverNC;
    public event WindowEvent<Form, EmptyArgs>? OnMouseLeaveNC;
    public event WindowEvent<Form, MouseNCEventArgs>? OnMouseMoveNC;
    public event WindowEvent<Form, MouseWheelEventArgs>? OnMouseWheel;
    public event WindowEvent<Form, MouseWheelEventArgs>? OnMouseHWheel;

    static string GenerateClassName()
    {
        StringBuilder result = new("BruhWindow");
        for (int i = 0; i < 8; i++)
        {
            switch (Random.Shared.Next(0, 3))
            {
                case 0:
                    result.Append((char)Random.Shared.Next('a', 'z'));
                    break;
                case 1:
                    result.Append((char)Random.Shared.Next('A', 'Z'));
                    break;
                case 2:
                    result.Append((char)Random.Shared.Next('0', '9'));
                    break;
                default:
                    result.Append('_');
                    break;
            }
        }
        return result.ToString();
    }

    internal readonly Dictionary<ushort, Control> Controls;

    /// <exception cref="WindowsException"/>
    public unsafe Form(
        string title,
        int width = CreateWindowFlags.USEDEFAULT,
        int height = CreateWindowFlags.USEDEFAULT,
        Menu? menu = null,
        uint styles = DefaultStyles,
        uint exStyles = 0,
        WindowClassSetter? windowClassSetter = null)
    {
        HINSTANCE hInstance = System.Diagnostics.Process.GetCurrentProcess().Handle;

        WNDCLASSEXW windowClass = WNDCLASSEXW.Create();

        string className = GenerateClassName();

        GCHandle = GCHandle.Alloc(this, GCHandleType.Normal);

        try
        {
            fixed (char* classNamePtr = className)
            {
                windowClass.BackgroundBrush = SystemBrushes.WINDOW;
                windowClass.ClsExtra = 0;
                windowClass.WndExtra = 0;
                windowClass.Cursor = HCURSOR.Zero;
                windowClass.Icon = HICON.Zero;
                windowClass.IconSmall = HICON.Zero;
                windowClass.Instance = hInstance;
                windowClass.ClassName = classNamePtr;
                windowClass.MenuName = null;
                windowClass.Style = 0;
                windowClass.WindowProcedure = &WinProc;

                windowClassSetter?.Invoke(ref windowClass);

                Class = Win32Class.Register(&windowClass);

                fixed (char* windowNamePtr = title)
                {
                    Handle = User32.CreateWindowExW(
                        exStyles,
                        classNamePtr,
                        windowNamePtr,
                        styles,
                        CreateWindowFlags.USEDEFAULT, CreateWindowFlags.USEDEFAULT,
                        width, height,
                        HWND.Zero,
                        (menu == null) ? HMENU.Zero : menu.Handle,
                        hInstance,
                        (void*)GCHandle.ToIntPtr(GCHandle));
                }
            }

            if (Handle == HWND.Zero)
            { throw WindowsException.Get(); }

            ActiveForms++;
        }
        catch
        {
            GCHandle.Free();
            throw;
        }

        IsDisposed = false;
        Controls = new Dictionary<ushort, Control>();
    }

    /// <exception cref="WindowsException"/>
    public unsafe Form(Win32Class @class, string title, int width = CreateWindowFlags.USEDEFAULT, int height = CreateWindowFlags.USEDEFAULT, Menu? menu = null, uint styles = DefaultStyles, uint exStyles = 0) : base()
    {
        HINSTANCE hInstance = System.Diagnostics.Process.GetCurrentProcess().Handle;

        GCHandle = GCHandle.Alloc(this, GCHandleType.Normal);

        try
        {
            fixed (char* classNamePtr = @class.Name)
            fixed (char* windowNamePtr = title)
            {
                Handle = User32.CreateWindowExW(
                    exStyles,
                    classNamePtr,
                    windowNamePtr,
                    styles,
                    CreateWindowFlags.USEDEFAULT, CreateWindowFlags.USEDEFAULT,
                    width, height,
                    HWND.Zero,
                    (menu == null) ? HMENU.Zero : menu.Handle,
                    hInstance,
                    (void*)GCHandle.ToIntPtr(GCHandle));
            }

            if (Handle == HWND.Zero)
            { throw WindowsException.Get(); }

            ActiveForms++;
        }
        catch
        {
            GCHandle.Free();
            throw;
        }

        IsDisposed = false;
        Controls = new Dictionary<ushort, Control>();
    }

    public const uint DefaultStyles =
        WindowStyles.OVERLAPPEDWINDOW |
        WindowStyles.VISIBLE;

    /// <inheritdoc/>
    /// <exception cref="WindowsException"/>
    /// <exception cref="InvalidOperationException"/>
    public void Dispose()
    {
        ActualDispose();
        GC.SuppressFinalize(this);
    }
    /// <exception cref="WindowsException"/>
    /// <exception cref="InvalidOperationException"/>
    ~Form() { ActualDispose(); }
    /// <exception cref="WindowsException"/>
    /// <exception cref="InvalidOperationException"/>
    void ActualDispose()
    {
        if (IsDisposed) return;

        if (Handle != HWND.Zero && User32.IsWindow(Handle) != FALSE)
        { _ = User32.DestroyWindow(Handle); }
        Class?.Unregister();

        IsDisposed = true;
        Handle = HWND.Zero;
        if (GCHandle.IsAllocated) GCHandle.Free();
        ActiveForms--;
    }

    /// <exception cref="WindowsException"/>
    /// <exception cref="InvalidOperationException"/>
    static unsafe LRESULT WinProc(HWND hwnd, uint uMsg, WPARAM wParam, LPARAM lParam)
    {
        nint userDataPtr;
        if (uMsg == WindowMessage.WM_CREATE)
        {
            CREATESTRUCT* pCreate = (CREATESTRUCT*)lParam;
            userDataPtr = (nint)pCreate->CreateParams;
            Kernel32.SetLastError(0);
            LONG_PTR result = User32.SetWindowLongPtrW(hwnd, GWLP.USERDATA, (nint)userDataPtr);
            if (result == LONG_PTR.Zero)
            {
                DWORD errorCode = Kernel32.GetLastError();
                if (errorCode != 0)
                { throw WindowsException.Get(errorCode); }
            }
        }
        else
        {
            userDataPtr = User32.GetWindowLongPtrW(hwnd, GWLP.USERDATA);
        }

        if (userDataPtr != 0)
        {
            GCHandle handle = GCHandle.FromIntPtr(userDataPtr);
            if (handle.IsAllocated)
            {
                object? obj = handle.Target;
                if (obj != null)
                {
                    Form _form = (Form)obj;
                    return _form.HandleEvent(uMsg, wParam, lParam);
                }
            }
        }

        return User32.DefWindowProcW(hwnd, uMsg, wParam, lParam);
    }

    /// <exception cref="GeneralException"/>
    public ushort GenerateControlId()
    {
        ushort result = 1;
        int endlessSafe = ushort.MaxValue - 1;
        while (Controls.ContainsKey(result))
        {
            result++;
            if (--endlessSafe <= 0)
            { throw new GeneralException("Failed to generate control id"); }
        }
        if (result == 0)
        { throw new GeneralException("Failed to generate control id"); }
        return result;
    }

    /// <exception cref="GeneralException"/>
    public ushort GenerateControlId(out ushort result)
    {
        result = GenerateControlId();
        return result;
    }

    /// <exception cref="WindowsException"/>
    unsafe LRESULT HandleEvent(uint uMsg, WPARAM wParam, LPARAM lParam)
    {
        switch (uMsg)
        {
            #region Painting & Drawing Messages
            case WindowMessage.WM_DISPLAYCHANGE:
                _ = User32.InvalidateRect(Handle, null, FALSE);
                break;
            case WindowMessage.WM_ERASEBKGND: break;
            case WindowMessage.WM_NCPAINT: break;
            case WindowMessage.WM_PAINT: OnPaint?.Invoke(this, new PaintEventArgs()); break;
            case WindowMessage.WM_PRINT: break;
            case WindowMessage.WM_PRINTCLIENT: break;
            case WindowMessage.WM_SETREDRAW: break;
            case WindowMessage.WM_SYNCPAINT: break;
            #endregion

            #region Menu Notifications
            case WindowMessage.WM_COMMAND:
                if (lParam != 0)
                {   // This is a control
                    ushort controlId = BitUtils.LowWord(wParam);
                    ushort notificationCode = BitUtils.HighWord(wParam);
                    if (Controls.TryGetValue(controlId, out Control? control) &&
                        control.Handle == lParam)
                    {
                        control.HandleNotification(this, notificationCode);
                        return 0;
                    }
                }
                else
                {
                    ushort subMsg = BitUtils.HighWord(wParam);
                    if (subMsg == 0)
                    {   // This is a menu
                        ushort menuItemId = BitUtils.LowWord(wParam);
                        OnMenuItem?.Invoke(this, new MenuItemEventArgs()
                        {
                            MenuItemId = menuItemId,
                        });
                    }
                    else if (subMsg == 1)
                    {   // This is an accelerator
                    }
                }
                break;
            case WindowMessage.WM_CONTEXTMENU: OnContextMenu?.Invoke(this, new ContextMenuEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_ENTERMENULOOP: break;
            case WindowMessage.WM_EXITMENULOOP: break;
            case WindowMessage.WM_GETTITLEBARINFOEX: break;
            case WindowMessage.WM_MENUCOMMAND: break;
            case WindowMessage.WM_MENUDRAG: break;
            case WindowMessage.WM_MENUGETOBJECT: break;
            case WindowMessage.WM_MENURBUTTONUP: break;
            case WindowMessage.WM_NEXTMENU: break;
            case WindowMessage.WM_UNINITMENUPOPUP: break;

            #endregion

            #region Control Messages
            case WindowMessage.CCM_DPISCALE: break;
            case WindowMessage.CCM_GETUNICODEFORMAT: break;
            case WindowMessage.CCM_GETVERSION: break;
            case WindowMessage.CCM_SETUNICODEFORMAT: break;
            case WindowMessage.CCM_SETVERSION: break;
            case WindowMessage.CCM_SETWINDOWTHEME: break;
            case WindowMessage.WM_NOTIFY:
                // NotificationMessageDetails* details = (NotificationMessageDetails*)lParam;
                break;
            case WindowMessage.WM_NOTIFYFORMAT: break;
            #endregion

            #region Messages
            case WindowMessage.MN_GETHMENU: break;
            case WindowMessage.WM_GETFONT: break;
            case WindowMessage.WM_GETTEXT: break;
            case WindowMessage.WM_GETTEXTLENGTH: break;
            case WindowMessage.WM_SETFONT: break;
            case WindowMessage.WM_SETICON: break;
            case WindowMessage.WM_SETTEXT: break;
            #endregion

            #region Notifications
            case WindowMessage.WM_ACTIVATEAPP: break;
            case WindowMessage.WM_CANCELMODE: break;
            case WindowMessage.WM_CHILDACTIVATE: break;
            case WindowMessage.WM_CLOSE:
                Destroy();
                return 0;
            case WindowMessage.WM_COMPACTING: break;
            case WindowMessage.WM_CREATE: break;
            case WindowMessage.WM_DESTROY:
                User32.PostQuitMessage(0);
                return 0;
            case WindowMessage.WM_DPICHANGED: break;
            case WindowMessage.WM_ENABLE: break;
            case WindowMessage.WM_ENTERSIZEMOVE: break;
            case WindowMessage.WM_EXITSIZEMOVE: break;
            case WindowMessage.WM_GETICON: break;
            case WindowMessage.WM_GETMINMAXINFO: break;
            case WindowMessage.WM_INPUTLANGCHANGE: break;
            case WindowMessage.WM_INPUTLANGCHANGEREQUEST: break;
            case WindowMessage.WM_MOVE: break;
            case WindowMessage.WM_MOVING: break;
            case WindowMessage.WM_NCACTIVATE: break;
            case WindowMessage.WM_NCCALCSIZE: break;
            case WindowMessage.WM_NCCREATE: break;
            case WindowMessage.WM_NCDESTROY: break;
            case WindowMessage.WM_NULL: break;
            case WindowMessage.WM_QUERYDRAGICON: break;
            case WindowMessage.WM_QUERYOPEN: break;
            case WindowMessage.WM_QUIT: break;
            case WindowMessage.WM_SHOWWINDOW: break;
            case WindowMessage.WM_SIZE: break;
            case WindowMessage.WM_SIZING: OnResize?.Invoke(this, new ResizeEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_STYLECHANGED: break;
            case WindowMessage.WM_STYLECHANGING: break;
            case WindowMessage.WM_THEMECHANGED: break;
            case WindowMessage.WM_USERCHANGED: break;
            case WindowMessage.WM_WINDOWPOSCHANGED: break;
            case WindowMessage.WM_WINDOWPOSCHANGING: break;
            #endregion

            #region Mouse Input Notifications
            case WindowMessage.WM_CAPTURECHANGED: break;
            case WindowMessage.WM_MOUSEACTIVATE: break;
            case WindowMessage.WM_NCHITTEST: break;

            case WindowMessage.WM_MOUSELEAVE: OnMouseLeave?.Invoke(this, new EmptyArgs()); break;
            case WindowMessage.WM_NCMOUSELEAVE: OnMouseLeaveNC?.Invoke(this, new EmptyArgs()); break;
            case WindowMessage.WM_LBUTTONDBLCLK: OnMouseLeftDoubleClick?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_LBUTTONDOWN: OnMouseLeftDown?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_LBUTTONUP: OnMouseLeftUp?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_MBUTTONDBLCLK: OnMouseMiddleDoubleClick?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_MBUTTONDOWN: OnMouseMiddleDown?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_MBUTTONUP: OnMouseMiddleUp?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_MOUSEHOVER: OnMouseHover?.Invoke(this, new MouseEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_MOUSEHWHEEL: OnMouseHWheel?.Invoke(this, new MouseWheelEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_MOUSEMOVE: OnMouseMove?.Invoke(this, new MouseEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_MOUSEWHEEL: OnMouseWheel?.Invoke(this, new MouseWheelEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCLBUTTONDBLCLK: OnMouseLeftDoubleClickNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCLBUTTONDOWN: OnMouseLeftDownNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCLBUTTONUP: OnMouseLeftUpNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCMBUTTONDBLCLK: OnMouseMiddleDoubleClickNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCMBUTTONDOWN: OnMouseMiddleDownNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCMBUTTONUP: OnMouseMiddleUpNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCMOUSEHOVER: OnMouseHoverNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCMOUSEMOVE: OnMouseMoveNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCRBUTTONDBLCLK: OnMouseRightDoubleClickNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCRBUTTONDOWN: OnMouseRightDownNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCRBUTTONUP: OnMouseRightUpNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCXBUTTONDBLCLK: OnMouseXDoubleClickNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCXBUTTONDOWN: OnMouseXDownNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_NCXBUTTONUP: OnMouseXUpNC?.Invoke(this, new MouseNCEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_RBUTTONDBLCLK: OnMouseRightDoubleClick?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_RBUTTONDOWN: OnMouseRightDown?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_RBUTTONUP: OnMouseRightUp?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_XBUTTONDBLCLK: OnMouseXDoubleClick?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_XBUTTONDOWN: OnMouseXDown?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
            case WindowMessage.WM_XBUTTONUP: OnMouseXUp?.Invoke(this, new MouseButtonEventArgs(wParam, lParam)); break;
                #endregion
        }

        return User32.DefWindowProcW(Handle, uMsg, wParam, lParam);
    }

    /// <exception cref="WindowsException"/>
    public static unsafe void HandleEvents()
    {
        MSG msg;
        int res;

        if ((res = User32.PeekMessageW(&msg, HWND.Zero, 0, 0, PeekMessageFlags.REMOVE)) != 0)
        {
            if (res == -1)
            { throw WindowsException.Get(); }

            _ = User32.TranslateMessage(&msg);
            User32.DispatchMessageW(&msg);
        }
    }

    /// <exception cref="WindowsException"/>
    public static unsafe void HandleEventsBlocking()
    {
        MSG msg;
        int res;

        while (ActiveForms > 0 && (res = User32.GetMessageW(&msg, HWND.Zero, 0, 0)) != 0)
        {
            if (res == -1)
            { throw WindowsException.Get(); }

            _ = User32.TranslateMessage(&msg);
            User32.DispatchMessageW(&msg);
        }
    }

    /// <exception cref="WindowsException"/>
    public static unsafe void HandleNextEvent(Action<MSG>? before = null)
    {
        MSG msg;
        int res;

        while ((res = User32.PeekMessageW(&msg, HWND.Zero, 0, 0, PeekMessageFlags.REMOVE)) != 0)
        {
            if (res == -1)
            { throw WindowsException.Get(); }

            _ = User32.TranslateMessage(&msg);

            before?.Invoke(msg);

            User32.DispatchMessageW(&msg);
        }
    }
}
