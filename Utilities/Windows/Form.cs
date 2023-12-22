using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Win32
{
    public delegate void MenuItemEventHandler(Form sender, ushort menuItemId);
    public delegate void ContextMenuEventHandler(Form sender, Window context, POINT position);

    [SupportedOSPlatform("windows")]
    public class Form : Window, IDisposable
    {
        public unsafe delegate void ResizeEventHandler(Form sender, RECT* rect);
        public unsafe delegate void PaintEventHandler(Form sender);
        public unsafe delegate void MouseEventHandler(Form sender, ushort x, ushort y, uint flags);
        public unsafe delegate void WindowClassSetter(ref WindowClassEx windowClass);

        static int ActiveForms;

        public static bool HasAllocatedForms => ActiveForms > 0;

        bool IsDisposed;
        readonly Win32Class? Class;
        readonly GCHandle GCHandle;

        public event ResizeEventHandler? OnResize;
        public event MenuItemEventHandler? OnMenuItem;
        public event ContextMenuEventHandler? OnContextMenu;
        public event PaintEventHandler? OnPaint;
        public event MouseEventHandler? OnMouseDown;
        public event MouseEventHandler? OnMouseUp;
        public event MouseEventHandler? OnMouseMove;

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public Menu? Menu
        {
            get
            {
                HMENU menuHandle = User32.GetMenu(Handle);
                if (menuHandle == HMENU.Zero)
                { return null; }
                return new Menu(menuHandle);
            }
            set
            {
                if (User32.SetMenu(Handle, value?.Handle ?? HMENU.Zero) == FALSE)
                { throw WindowsException.Get(); }

                if (User32.DrawMenuBar(Handle) == FALSE)
                { throw WindowsException.Get(); }
            }
        }

        static string GenerateClassName()
        {
            StringBuilder result = new("BruhWindow");
            for (int i = 0; i < 8; i++)
            {
                int yes = Random.Shared.Next(0, 3);
                switch (yes)
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

        public static unsafe delegate*<HWND, uint, WPARAM, LPARAM, LRESULT> DefaultWindowProcess => &WinProc;

        /// <exception cref="WindowsException"/>
        public unsafe Form(
            string title,
            int width = CreateWindowFlags.USEDEFAULT,
            int height = CreateWindowFlags.USEDEFAULT,
            Menu? menu = null,
            uint styles = DefaultStyles,
            uint exStyles = 0,
            WindowClassSetter? windowClassSetter = null) : base()
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
                    windowClass.IconSm = HICON.Zero;
                    windowClass.Instance = hInstance;
                    windowClass.ClassName = classNamePtr;
                    windowClass.MenuName = null;
                    windowClass.Style = 0;
                    windowClass.WindowProcedure = DefaultWindowProcess;

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
        public unsafe Form(Win32Class @class, string title, int width = CreateWindowFlags.USEDEFAULT, int height = CreateWindowFlags.USEDEFAULT, Menu? menu = null, uint styles = DefaultStyles) : base()
        {
            HINSTANCE hInstance = System.Diagnostics.Process.GetCurrentProcess().Handle;

            uint exStyles = 0;

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
            (WindowStyles.OVERLAPPEDWINDOW ^ WindowStyles.THICKFRAME ^ WindowStyles.MAXIMIZEBOX) |
            WindowStyles.SYSMENU |
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
            void* userDataPtr;
            if (uMsg == WindowMessage.WM_CREATE)
            {
                CREATESTRUCT* pCreate = (CREATESTRUCT*)lParam;
                userDataPtr = pCreate->CreateParams;
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
                userDataPtr = GetUserData(hwnd);
            }

            if (userDataPtr != null)
            {
                GCHandle handle = GCHandle.FromIntPtr((nint)userDataPtr);
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
                { throw new GeneralException($"Failed to generate control id"); }
            }
            if (result == 0)
            { throw new GeneralException($"Failed to generate control id"); }
            return result;
        }

        /// <exception cref="GeneralException"/>
        public ushort GenerateControlId(out ushort result)
        {
            result = GenerateControlId();
            return result;
        }

        /// <exception cref="WindowsException"/>
        public unsafe LRESULT HandleEvent(uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            switch (uMsg)
            {
                #region Painting & Drawing Messages
                case WindowMessage.WM_DISPLAYCHANGE: break;
                case WindowMessage.WM_ERASEBKGND: break;
                case WindowMessage.WM_NCPAINT: break;
                case WindowMessage.WM_PAINT:
                {
                    OnPaint?.Invoke(this);
                    break;
                }
                case WindowMessage.WM_PRINT: break;
                case WindowMessage.WM_PRINTCLIENT: break;
                case WindowMessage.WM_SETREDRAW: break;
                case WindowMessage.WM_SYNCPAINT: break;
                #endregion

                #region Menu Notifications
                case WindowMessage.WM_COMMAND:
                    if (lParam != LPARAM.Zero)
                    { // This is a control
                        ushort controlId = Macros.LOWORD(wParam);
                        ushort notificationCode = Macros.HIWORD(wParam);
                        if (Controls.TryGetValue(controlId, out Control? control) &&
                            control.Handle == lParam)
                        {
                            control.HandleNotification(this, notificationCode);
                            return 0;
                        }
                    }
                    else
                    {
                        ushort subMsg = Macros.HIWORD(wParam);
                        if (subMsg == 0)
                        { // This is a menu
                            ushort menuItemId = Macros.LOWORD(wParam);
                            OnMenuItem?.Invoke(this, menuItemId);
                        }
                        else if (subMsg == 1)
                        { // This is an accelerator

                        }
                    }
                    break;
                case WindowMessage.WM_CONTEXTMENU:
                {
                    ushort x = Macros.LOWORD(lParam);
                    ushort y = Macros.HIWORD(lParam);
                    if (OnContextMenu != null)
                    {
                        OnContextMenu.Invoke(this, (Window)(HWND)wParam, new POINT(x, y));
                        return 0;
                    }
                    break;
                }
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
                    NotificationMessageDetails* details = (NotificationMessageDetails*)lParam.ToPointer();
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
                case WindowMessage.WM_SIZING:
                {
                    RECT* rect = (RECT*)lParam.ToPointer();
                    OnResize?.Invoke(this, rect);
                    return (LRESULT)1;
                }
                case WindowMessage.WM_STYLECHANGED: break;
                case WindowMessage.WM_STYLECHANGING: break;
                case WindowMessage.WM_THEMECHANGED: break;
                case WindowMessage.WM_USERCHANGED: break;
                case WindowMessage.WM_WINDOWPOSCHANGED: break;
                case WindowMessage.WM_WINDOWPOSCHANGING: break;
                #endregion

                #region Mouse Input Notifications
                case WindowMessage.WM_CAPTURECHANGED: break;
                case WindowMessage.WM_LBUTTONDBLCLK: break;
                case WindowMessage.WM_LBUTTONDOWN:
                    if (OnMouseDown != null)
                    {
                        ushort x = Macros.LOWORD(lParam);
                        ushort y = Macros.HIWORD(lParam);
                        uint flags = wParam.ToUInt32();
                        OnMouseDown.Invoke(this, x, y, flags);
                        return 0;
                    }
                    break;
                case WindowMessage.WM_LBUTTONUP:
                    if (OnMouseUp != null)
                    {
                        ushort x = Macros.LOWORD(lParam);
                        ushort y = Macros.HIWORD(lParam);
                        uint flags = wParam.ToUInt32();
                        OnMouseUp.Invoke(this, x, y, flags);
                        return 0;
                    }
                    break;
                case WindowMessage.WM_MBUTTONDBLCLK: break;
                case WindowMessage.WM_MBUTTONDOWN: break;
                case WindowMessage.WM_MBUTTONUP: break;
                case WindowMessage.WM_MOUSEACTIVATE: break;
                case WindowMessage.WM_MOUSEHOVER: break;
                case WindowMessage.WM_MOUSEHWHEEL: break;
                case WindowMessage.WM_MOUSELEAVE: break;
                case WindowMessage.WM_MOUSEMOVE:
                    if (OnMouseMove != null)
                    {
                        ushort x = Macros.LOWORD(lParam);
                        ushort y = Macros.HIWORD(lParam);
                        uint flags = wParam.ToUInt32();
                        OnMouseMove.Invoke(this, x, y, flags);
                        return 0;
                    }
                    break;
                case WindowMessage.WM_MOUSEWHEEL: break;
                case WindowMessage.WM_NCHITTEST: break;
                case WindowMessage.WM_NCLBUTTONDBLCLK: break;
                case WindowMessage.WM_NCLBUTTONDOWN: break;
                case WindowMessage.WM_NCLBUTTONUP: break;
                case WindowMessage.WM_NCMBUTTONDBLCLK: break;
                case WindowMessage.WM_NCMBUTTONDOWN: break;
                case WindowMessage.WM_NCMBUTTONUP: break;
                case WindowMessage.WM_NCMOUSEHOVER: break;
                case WindowMessage.WM_NCMOUSELEAVE: break;
                case WindowMessage.WM_NCMOUSEMOVE: break;
                case WindowMessage.WM_NCRBUTTONDBLCLK: break;
                case WindowMessage.WM_NCRBUTTONDOWN: break;
                case WindowMessage.WM_NCRBUTTONUP: break;
                case WindowMessage.WM_NCXBUTTONDBLCLK: break;
                case WindowMessage.WM_NCXBUTTONDOWN: break;
                case WindowMessage.WM_NCXBUTTONUP: break;
                case WindowMessage.WM_RBUTTONDBLCLK: break;
                case WindowMessage.WM_RBUTTONDOWN: break;
                case WindowMessage.WM_RBUTTONUP: break;
                case WindowMessage.WM_XBUTTONDBLCLK: break;
                case WindowMessage.WM_XBUTTONDOWN: break;
                case WindowMessage.WM_XBUTTONUP: break;
                #endregion

                default:
                    break;
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
        public void Close()
        {
            if (User32.PostMessageW(Handle, WindowMessage.WM_CLOSE, WPARAM.Zero, LPARAM.Zero) == FALSE)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public void Destroy()
        {
            if (Handle == HWND.Zero) return;
            if (User32.DestroyWindow(Handle) == FALSE)
            { throw WindowsException.Get(); }
            Handle = HWND.Zero;
        }

        /// <exception cref="WindowsException"/>
        public void Minimize()
        {
            if (User32.CloseWindow(Handle) == FALSE)
            { throw WindowsException.Get(); }
        }

        public void Show(int cmdShow) => _ = User32.ShowWindow(Handle, cmdShow);

        public static unsafe void* GetUserData(HWND hwnd)
        {
            LONG_PTR ptr = User32.GetWindowLongPtrW(hwnd, GWLP.USERDATA);
            return (void*)ptr;
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public bool IsValid => User32.IsWindow(Handle) != FALSE;

        /// <exception cref="WindowsException"/>
        public void SetLayeredWindowAttributes(COLORREF key, byte alpha, LWA flags = LWA.ALPHA | LWA.COLORKEY)
        {
            if (User32.SetLayeredWindowAttributes(Handle, key, alpha, (DWORD)flags) == FALSE)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public void SetLayeredWindowAttributes(ValueTuple<byte, byte, byte> key, byte alpha, LWA flags = LWA.ALPHA | LWA.COLORKEY)
            => this.SetLayeredWindowAttributes(Macros.RGB(key.Item1, key.Item2, key.Item3), alpha, flags);
    }
}
