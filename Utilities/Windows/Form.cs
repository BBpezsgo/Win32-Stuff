using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Win32
{
    public delegate void MenuItemEventHandler(Form sender, ushort menuItemId);
    public delegate void ContextMenuEventHandler(Form sender, Window context, POINT position);

    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public readonly struct PaintHandle : IDisposable
    {
        readonly DisplayDC dc;
        unsafe readonly PaintStruct* paint;

        public DisplayDC DeviceContext => dc;

        unsafe PaintHandle(DisplayDC dc, PaintStruct* paint)
        {
            this.dc = dc;
            this.paint = paint;
        }

        public static implicit operator HDC(PaintHandle paintHandle) => paintHandle.dc;
        public static implicit operator DisplayDC(PaintHandle paintHandle) => paintHandle.dc;

        public override string ToString() => dc.ToString();

        unsafe public static PaintHandle Begin(HWND window, out PaintStruct paint)
        {
            paint = default;
            PaintStruct* paintPtr = (PaintStruct*)Unsafe.AsPointer(ref paint);
            HDC dcHandle = User32.BeginPaint(window, paintPtr);
            return new PaintHandle(new DisplayDC(dcHandle, window), paintPtr);
        }

        unsafe public void Dispose() => _ = User32.EndPaint(dc, paint);
    }

    public class Form : Window, IDisposable
    {
        public const string ClassName = "BruhWindow";

        unsafe public delegate void ResizeEventHandler(Form sender, RECT* rect);
        unsafe public delegate void PaintEventHandler(Form sender);
        unsafe public delegate void MouseEventHandler(Form sender, ushort x, ushort y, uint flags);

        static readonly Dictionary<HWND, Form> FormInstances = new();

        bool IsDisposed;
        readonly Win32Class? Class;

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
                if (User32.SetMenu(Handle, value?.Handle ?? HMENU.Zero) == 0)
                { throw WindowsException.Get(); }

                if (User32.DrawMenuBar(Handle) == 0)
                { throw WindowsException.Get(); }
            }
        }

        static string GenerateClassName()
        {
            StringBuilder result = new(ClassName);
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

        public readonly Dictionary<ushort, Control> Controls;

        /// <exception cref="WindowsException"/>
        unsafe public Form(string title, int width, int height, Menu? menu = null, uint styles = DefaultStyles) : base()
        {
            HINSTANCE hInstance = Process.GetCurrentProcess().Handle;

            uint exStyles = 0;

            WNDCLASSEXW windowClass = WNDCLASSEXW.Create();

            string className = GenerateClassName();
            fixed (char* classNamePtr = className)
            {
                windowClass.hbrBackground = HBRUSH.Zero;
                windowClass.hCursor = HCURSOR.Zero;
                windowClass.hIcon = HICON.Zero;
                windowClass.hIconSm = HICON.Zero;
                windowClass.hInstance = hInstance;
                windowClass.lpszClassName = classNamePtr;
                windowClass.lpszMenuName = null;
                windowClass.style = 0;
                windowClass.lpfnWndProc = &WinProc;

                Class = Win32Class.Register(&windowClass);

                fixed (char* windowNamePtr = title)
                {
                    Handle = User32.CreateWindowExW(
                        exStyles,
                        classNamePtr,
                        windowNamePtr,
                        styles,
                        0, 0,
                        width, height,
                        HWND.Zero,
                        (menu == null) ? HMENU.Zero : menu.Handle,
                        hInstance,
                        null);
                }
            }

            if (Handle == HWND.Zero)
            { throw WindowsException.Get(); }

            FormInstances.Add(Handle, this);

            IsDisposed = false;
            Controls = new Dictionary<ushort, Control>();
        }

        unsafe public static delegate*<HWND, uint, WPARAM, LPARAM, LRESULT> DefaultWindowProcess => &WinProc;

        /// <exception cref="WindowsException"/>
        unsafe public Form(Win32Class @class, string title, int width, int height, Menu? menu = null, uint styles = DefaultStyles) : base()
        {
            HINSTANCE hInstance = Process.GetCurrentProcess().Handle;

            uint exStyles = 0;

            fixed (char* classNamePtr = @class.Name)
            fixed (char* windowNamePtr = title)
            {
                Handle = User32.CreateWindowExW(
                    exStyles,
                    classNamePtr,
                    windowNamePtr,
                    styles,
                    0, 0,
                    width, height,
                    HWND.Zero,
                    (menu == null) ? HMENU.Zero : menu.Handle,
                    hInstance,
                    null);
            }

            if (Handle == HWND.Zero)
            { throw WindowsException.Get(); }

            FormInstances.Add(Handle, this);

            IsDisposed = false;
            Controls = new Dictionary<ushort, Control>();
        }

        public const uint DefaultStyles =
            (WS.OVERLAPPEDWINDOW ^ WS.THICKFRAME ^ WS.MAXIMIZEBOX) |
            WS.SYSMENU |
            WS.VISIBLE;

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        ~Form() { Dispose(disposing: false); }
        void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (disposing)
            {
                Destroy();
                Class?.Unregister();
            }

            FormInstances.Remove(Handle);
            IsDisposed = true;
            Handle = HWND.Zero;
        }

        /// <exception cref="WindowsException"/>
        unsafe static LRESULT WinProc(HWND hwnd, uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            /*
            void* userDataPtr;
            if (uMsg == WM.WM_CREATE)
            {
                CREATESTRUCT* pCreate = (CREATESTRUCT*)lParam;
                userDataPtr = pCreate->lpCreateParams;
                Kernel32.SetLastError(0);
                LONG_PTR result = User32.SetWindowLongPtrW(hwnd, GWLP.USERDATA, (LONG_PTR)userDataPtr);
                if (result == LONG_PTR.Zero)
                {
                    uint errorCode = Kernel32.GetLastError();
                    if (errorCode != 0)
                    { throw WindowsException.Get(errorCode); }
                }
            }
            else
            {
                userDataPtr = GetUserData(hwnd);
            }
            */

            if (FormInstances.TryGetValue(hwnd, out Form? window) && window.Handle == hwnd)
            { return window.HandleEvent(uMsg, wParam, lParam); }

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
        unsafe public LRESULT HandleEvent(uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            switch (uMsg)
            {
                #region Painting & Drawing Messages
                case WM.WM_DISPLAYCHANGE: break;
                case WM.WM_ERASEBKGND: break;
                case WM.WM_NCPAINT: break;
                case WM.WM_PAINT:
                    {
                        if (OnPaint != null)
                        {
                            OnPaint.Invoke(this);
                        }
                        else
                        {
                            using PaintHandle hdc = PaintHandle.Begin(Handle, out PaintStruct paint);
                            _ = User32.FillRect(hdc, &paint.rcPaint, (HBRUSH)(COLOR.COLOR_WINDOW + 1));
                        }
                        break;
                    }
                case WM.WM_PRINT: break;
                case WM.WM_PRINTCLIENT: break;
                case WM.WM_SETREDRAW: break;
                case WM.WM_SYNCPAINT: break;
                #endregion

                #region Menu Notifications
                case WM.WM_COMMAND:
                    if (lParam != LPARAM.Zero)
                    { // This is a control
                        ushort controlId = Macros.LOWORD(wParam);
                        ushort notificationCode = Macros.HIWORD(wParam);
                        if (Controls.TryGetValue(controlId, out Control? control) &&
                            control.Handle == lParam)
                        {
                            control.HandleNotification(this, notificationCode);
                            return (LPARAM)0;
                        }

                        const int BUFFER_SIZE = 64;
                        string className;
                        fixed (WCHAR* buffer = new string('\0', BUFFER_SIZE))
                        {
                            int length = User32.GetClassNameW(lParam, buffer, BUFFER_SIZE);
                            if (length == 0)
                            { throw WindowsException.Get(); }
                            className = new string(buffer, 0, length);
                        }
                        className = className.ToUpperInvariant();
                        switch (className)
                        {
                            case LowLevel.ClassName.STATIC:
                                new StaticControl(lParam).HandleNotification(this, notificationCode);
                                return (LPARAM)0;
                            case LowLevel.ClassName.PROGRESS_BAR:
                                new ProgressBar(lParam).HandleNotification(this, notificationCode);
                                return (LPARAM)0;
                            case LowLevel.ClassName.IP_ADDRESS:
                                new IpAddress(lParam).HandleNotification(this, notificationCode);
                                return (LPARAM)0;
                            case LowLevel.ClassName.EDIT:
                                new EditControl(lParam).HandleNotification(this, notificationCode);
                                return (LPARAM)0;
                            case LowLevel.ClassName.BUTTON:
                                new Button(lParam).HandleNotification(this, notificationCode);
                                return (LPARAM)0;
                            default:
                                break;
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
                case WM.WM_CONTEXTMENU:
                    {
                        ushort x = Macros.LOWORD(lParam);
                        ushort y = Macros.HIWORD(lParam);
                        if (OnContextMenu != null)
                        {
                            OnContextMenu.Invoke(this, new Window((HWND)(void*)wParam), new POINT(x, y));
                            return (LPARAM)0;
                        }
                        break;
                    }
                case WM.WM_ENTERMENULOOP: break;
                case WM.WM_EXITMENULOOP: break;
                case WM.WM_GETTITLEBARINFOEX: break;
                case WM.WM_MENUCOMMAND: break;
                case WM.WM_MENUDRAG: break;
                case WM.WM_MENUGETOBJECT: break;
                case WM.WM_MENURBUTTONUP: break;
                case WM.WM_NEXTMENU: break;
                case WM.WM_UNINITMENUPOPUP: break;

                #endregion

                #region Control Messages
                case WM.CCM_DPISCALE: break;
                case WM.CCM_GETUNICODEFORMAT: break;
                case WM.CCM_GETVERSION: break;
                case WM.CCM_SETUNICODEFORMAT: break;
                case WM.CCM_SETVERSION: break;
                case WM.CCM_SETWINDOWTHEME: break;
                case WM.WM_NOTIFY:
                    NMHDR* info = (NMHDR*)lParam.ToPointer();
                    break;
                case WM.WM_NOTIFYFORMAT: break;
                #endregion

                #region Messages
                case WM.MN_GETHMENU: break;
                case WM.WM_GETFONT: break;
                case WM.WM_GETTEXT: break;
                case WM.WM_GETTEXTLENGTH: break;
                case WM.WM_SETFONT: break;
                case WM.WM_SETICON: break;
                case WM.WM_SETTEXT: break;
                #endregion

                #region Notifications
                case WM.WM_ACTIVATEAPP: break;
                case WM.WM_CANCELMODE: break;
                case WM.WM_CHILDACTIVATE: break;
                case WM.WM_CLOSE:
                    Destroy();
                    return (LRESULT)0;
                case WM.WM_COMPACTING: break;
                case WM.WM_CREATE: break;
                case WM.WM_DESTROY:
                    User32.PostQuitMessage(0);
                    return (LRESULT)0;
                case WM.WM_DPICHANGED: break;
                case WM.WM_ENABLE: break;
                case WM.WM_ENTERSIZEMOVE: break;
                case WM.WM_EXITSIZEMOVE: break;
                case WM.WM_GETICON: break;
                case WM.WM_GETMINMAXINFO: break;
                case WM.WM_INPUTLANGCHANGE: break;
                case WM.WM_INPUTLANGCHANGEREQUEST: break;
                case WM.WM_MOVE: break;
                case WM.WM_MOVING: break;
                case WM.WM_NCACTIVATE: break;
                case WM.WM_NCCALCSIZE: break;
                case WM.WM_NCCREATE: break;
                case WM.WM_NCDESTROY: break;
                case WM.WM_NULL: break;
                case WM.WM_QUERYDRAGICON: break;
                case WM.WM_QUERYOPEN: break;
                case WM.WM_QUIT: break;
                case WM.WM_SHOWWINDOW: break;
                case WM.WM_SIZE: break;
                case WM.WM_SIZING:
                    {
                        RECT* rect = (RECT*)lParam.ToPointer();
                        OnResize?.Invoke(this, rect);
                        return (LRESULT)1;
                    }
                case WM.WM_STYLECHANGED: break;
                case WM.WM_STYLECHANGING: break;
                case WM.WM_THEMECHANGED: break;
                case WM.WM_USERCHANGED: break;
                case WM.WM_WINDOWPOSCHANGED: break;
                case WM.WM_WINDOWPOSCHANGING: break;
                #endregion

                #region Mouse Input Notifications
                case WM.WM_CAPTURECHANGED: break;
                case WM.WM_LBUTTONDBLCLK: break;
                case WM.WM_LBUTTONDOWN:
                    if (OnMouseDown != null)
                    {
                        ushort x = Macros.LOWORD(lParam);
                        ushort y = Macros.HIWORD(lParam);
                        uint flags = wParam.ToUInt32();
                        OnMouseDown.Invoke(this, x, y, flags);
                        return (LPARAM)0;
                    }
                    break;
                case WM.WM_LBUTTONUP:
                    if (OnMouseUp != null)
                    {
                        ushort x = Macros.LOWORD(lParam);
                        ushort y = Macros.HIWORD(lParam);
                        uint flags = wParam.ToUInt32();
                        OnMouseUp.Invoke(this, x, y, flags);
                        return (LPARAM)0;
                    }
                    break;
                case WM.WM_MBUTTONDBLCLK: break;
                case WM.WM_MBUTTONDOWN: break;
                case WM.WM_MBUTTONUP: break;
                case WM.WM_MOUSEACTIVATE: break;
                case WM.WM_MOUSEHOVER: break;
                case WM.WM_MOUSEHWHEEL: break;
                case WM.WM_MOUSELEAVE: break;
                case WM.WM_MOUSEMOVE:
                    if (OnMouseMove != null)
                    {
                        ushort x = Macros.LOWORD(lParam);
                        ushort y = Macros.HIWORD(lParam);
                        uint flags = wParam.ToUInt32();
                        OnMouseMove.Invoke(this, x, y, flags);
                        return (LPARAM)0;
                    }
                    break;
                case WM.WM_MOUSEWHEEL: break;
                case WM.WM_NCHITTEST: break;
                case WM.WM_NCLBUTTONDBLCLK: break;
                case WM.WM_NCLBUTTONDOWN: break;
                case WM.WM_NCLBUTTONUP: break;
                case WM.WM_NCMBUTTONDBLCLK: break;
                case WM.WM_NCMBUTTONDOWN: break;
                case WM.WM_NCMBUTTONUP: break;
                case WM.WM_NCMOUSEHOVER: break;
                case WM.WM_NCMOUSELEAVE: break;
                case WM.WM_NCMOUSEMOVE: break;
                case WM.WM_NCRBUTTONDBLCLK: break;
                case WM.WM_NCRBUTTONDOWN: break;
                case WM.WM_NCRBUTTONUP: break;
                case WM.WM_NCXBUTTONDBLCLK: break;
                case WM.WM_NCXBUTTONDOWN: break;
                case WM.WM_NCXBUTTONUP: break;
                case WM.WM_RBUTTONDBLCLK: break;
                case WM.WM_RBUTTONDOWN: break;
                case WM.WM_RBUTTONUP: break;
                case WM.WM_XBUTTONDBLCLK: break;
                case WM.WM_XBUTTONDOWN: break;
                case WM.WM_XBUTTONUP: break;
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

            if (FormInstances.Count > 0 && (res = User32.PeekMessageW(&msg, HWND.Zero, 0, 0, PM.REMOVE)) != 0)
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

            while (FormInstances.Count > 0 && (res = User32.GetMessageW(&msg, HWND.Zero, 0, 0)) != 0)
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
            if (User32.PostMessageW(Handle, WM.WM_CLOSE, WPARAM.Zero, LPARAM.Zero) == 0)
            { throw WindowsException.Get(); }
        }

        public void Destroy()
        {
            if (User32.DestroyWindow(Handle) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public void Minimize()
        {
            if (User32.CloseWindow(Handle) == 0)
            { throw WindowsException.Get(); }
        }

        public void Show(int cmdShow) => _ = User32.ShowWindow(Handle, cmdShow);

        unsafe public static void* GetUserData(HWND hwnd)
        {
            LONG_PTR ptr = User32.GetWindowLongPtrW(hwnd, GWLP.USERDATA);
            return (void*)ptr;
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public bool IsValid => User32.IsWindow(Handle) != FALSE;
    }
}
