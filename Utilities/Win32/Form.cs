using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Win32.Utilities
{
    public delegate void MenuItemEventHandler(Form sender, ushort menuItemId);
    public delegate void ContextMenuEventHandler(Form sender, Window context, POINT position);

    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    readonly struct PaintHandle : IDisposable
    {
        readonly HWND windowHandle;
        readonly HDC dcHandle;
        unsafe readonly PaintStruct* paint;

        unsafe PaintHandle(HDC dcHandle, HWND windowHandle, PaintStruct* paint)
        {
            this.dcHandle = dcHandle;
            this.windowHandle = windowHandle;
            this.paint = paint;
        }

        public static implicit operator HDC(PaintHandle paintHandle) => paintHandle.dcHandle;

        public override string ToString() => "0x" + dcHandle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        unsafe public static PaintHandle Begin(HWND window, out PaintStruct paint)
        {
            paint = default;
            PaintStruct* paintPtr = (PaintStruct*)Unsafe.AsPointer(ref paint);
            HDC dcHandle = User32.BeginPaint(window, paintPtr);
            return new PaintHandle(dcHandle, window, paintPtr);
        }

        unsafe public void Dispose() => _ = User32.EndPaint(windowHandle, paint);
    }

    public class Form : Window, IDisposable
    {
        public const string ClassName = "BruhWindow";

        unsafe public delegate void ResizeEventHandler(Form sender, RECT* rect);

        static readonly Dictionary<HWND, Form> FormInstances = new();

        bool IsDisposed;
        readonly Win32Class? Class;

        public event ResizeEventHandler? OnResize;
        public event MenuItemEventHandler? OnMenuItem;
        public event ContextMenuEventHandler? OnContextMenu;

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

        public readonly Dictionary<ushort, Control> Controls;

        /// <exception cref="WindowsException"/>
        unsafe public Form(string title, int width, int height, Menu? menu = null) : base()
        {
            HINSTANCE hInstance = Process.GetCurrentProcess().Handle;

            uint exStyles = 0;

            WNDCLASSEXW windowClass = WNDCLASSEXW.Create();

            fixed (char* classNamePtr = ClassName)
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
                        DefaultStyles,
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

        const uint DefaultStyles =
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

        /// <exception cref="NotWindowsException"/>
        public ushort MakeId()
        {
            ushort result = 1;
            int endlessSafe = ushort.MaxValue - 1;
            while (Controls.ContainsKey(result))
            {
                result++;
                if (--endlessSafe <= 0)
                { throw new NotWindowsException($"Failed to generate control id"); }
            }
            if (result == 0)
            { throw new NotWindowsException($"Failed to generate control id"); }
            return result;
        }

        /// <exception cref="NotWindowsException"/>
        public ushort MakeId(out ushort result)
        {
            result = MakeId();
            return result;
        }

        /// <exception cref="WindowsException"/>
        unsafe public LRESULT HandleEvent(uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            switch (uMsg)
            {
                case WM.WM_CONTEXTMENU:
                    {
                        ushort x = Macros.LOWORD(lParam);
                        ushort y = Macros.HIWORD(lParam);
                        if (OnContextMenu != null)
                        {
                            OnContextMenu.Invoke(this, new Window((HWND)(void*)wParam), new POINT(x, y));
                            return LRESULT.Zero;
                        }
                        break;
                    }
                case WM.WM_SIZING:
                    {
                        RECT* rect = (RECT*)lParam.ToPointer();
                        OnResize?.Invoke(this, rect);

                        return (LRESULT)TRUE;
                    }
                case WM.WM_COMMAND:
                    if (lParam != LPARAM.Zero)
                    { // This is a control
                        ushort controlId = Macros.LOWORD(wParam);
                        if (Controls.TryGetValue(controlId, out Control? control) &&
                            control.Handle == lParam)
                        {
                            ushort notificationCode = Macros.HIWORD(wParam);
                            control.HandleNotification(this, notificationCode);
                            return LRESULT.Zero;
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
                case WM.WM_NOTIFY:
                    NMHDR* info = (NMHDR*)lParam.ToPointer();
                    break;
                case WM.WM_CLOSE:
                    Destroy();
                    return LRESULT.Zero;
                case WM.WM_DESTROY:
                    User32.PostQuitMessage(0);
                    // Dispose();
                    return LRESULT.Zero;
                case WM.WM_PAINT:
                    {
                        using PaintHandle hdc = PaintHandle.Begin(Handle, out PaintStruct paint);

                        _ = User32.FillRect(hdc, &paint.rcPaint, (HBRUSH)(COLOR.COLOR_WINDOW + 1));

                        break;
                    }
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
    }
}
