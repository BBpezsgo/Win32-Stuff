namespace Win32.Utilities
{
    public delegate LRESULT? FormEventHandler(Form sender, uint msg, WPARAM wParam, LPARAM lParam);

    public class Form : Window, IDisposable
    {
        unsafe public delegate void ResizeEventHandler(Form sender, RECT* rect);

        static readonly Dictionary<HWND, Form> Handlers = new();

        bool IsDisposed;

        public event FormEventHandler? OnEvent;
        public event ResizeEventHandler? OnResize;

        public readonly Dictionary<ushort, Control> Controls;

        unsafe public Form(string title, int width, int height, DWORD style = DefaultStyles) : base()
        {
            Handle = Create(
                title,
                width,
                height,
                &WinProc,
                style);
            Handlers.Add(Handle, this);

            IsDisposed = false;
            Controls = new Dictionary<ushort, Control>();
        }

        const uint DefaultStyles =
            (WS.OVERLAPPEDWINDOW ^ WS.THICKFRAME ^ WS.MAXIMIZEBOX) |
            WS.SYSMENU |
            WS.VISIBLE;

        unsafe public static HWND Create(string title, int width, int height, delegate*<HWND, uint, WPARAM, LPARAM, LRESULT> windProc, DWORD style = DefaultStyles, void* lpParam = null, string className = "windowClass")
        {
            fixed (char* classNamePtr = className)
            {
                WNDCLASSEXW windowClass = new()
                {
                    cbSize = (uint)sizeof(WNDCLASSEXW),
                    hbrBackground = HBRUSH.Zero,
                    hCursor = HCURSOR.Zero,
                    hIcon = HICON.Zero,
                    hIconSm = HICON.Zero,
                    hInstance = HINSTANCE.Zero,
                    lpszClassName = classNamePtr,
                    lpszMenuName = null,
                    style = 0,
                    lpfnWndProc = windProc,
                };

                ushort classId = User32.RegisterClassExW(&windowClass);
            }

            fixed (char* windowNamePtr = title)
            fixed (char* classNamePtr = className)
            {
                uint exStyles = 0;

                HWND handle = User32.CreateWindowExW(exStyles,
                    classNamePtr,
                    windowNamePtr,
                    style,
                    0, 0,
                    width, height,
                    HWND.Zero,
                    HMENU.Zero,
                    HINSTANCE.Zero,
                    lpParam);

                // UxTheme.SetWindowTheme(handle, " ", " ");

                return handle;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        ~Form() { Dispose(disposing: false); }
        void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            IsDisposed = true;
            Handlers.Remove(Handle);

            if (!disposing) return;
        }

        static LRESULT WinProc(HWND hwnd, uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            if (Handlers.TryGetValue(hwnd, out Form? window) && window.Handle == hwnd)
            { return window.HandleEventInternal(uMsg, wParam, lParam); }

            return User32.DefWindowProcW(hwnd, uMsg, wParam, lParam);
        }

        LRESULT HandleEventInternal(uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            LRESULT? result = OnEvent?.Invoke(this, uMsg, wParam, lParam);

            if (result.HasValue) return result.Value;

            if (uMsg == WM.WM_SIZING)
            {
                unsafe
                {
                    RECT* rect = (RECT*)lParam.ToPointer();
                    OnResize?.Invoke(this, rect);
                }

                return (LRESULT)TRUE;
            }

            return HandleEventDefault(uMsg, wParam, lParam);
        }

        public ushort MakeId()
        {
            ushort result = 1;
            int endlessSafe = ushort.MaxValue - 1;
            while (Controls.ContainsKey(result))
            {
                result++;
                if (--endlessSafe <= 0) throw new Exception($"Failed to generate control id");
            }
            return result;
        }

        public ushort MakeId(out ushort result)
        {
            result = MakeId();
            return result;
        }

        public LRESULT HandleEventDefault(uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            switch (uMsg)
            {
                case WM.WM_COMMAND:
                    if (lParam != LPARAM.Zero)
                    {
                        ushort controlId = Macros.LOWORD(wParam);
                        if (Controls.TryGetValue(controlId, out Control? control))
                        {
                            control.DispatchEvent(Handle, uMsg, wParam, lParam);
                            return LRESULT.Zero;
                        }
                    }
                    break;
                case WM.WM_NOTIFY:
                    unsafe {
                        NMHDR* info = (NMHDR*)lParam.ToPointer();
                    }
                    break;
                case WM.WM_CLOSE:
                    if (User32.DestroyWindow(Handle) == 0)
                    { throw WindowsException.Get(); }
                    return LRESULT.Zero;
                case WM.WM_DESTROY:
                    Dispose();
                    User32.PostQuitMessage(0);
                    return LRESULT.Zero;
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

            if (Handlers.Count > 0 && (res = User32.PeekMessageW(&msg, HWND.Zero, 0, 0, PM.REMOVE)) != 0)
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

            while (Handlers.Count > 0 && (res = User32.GetMessageW(&msg, HWND.Zero, 0, 0)) != 0)
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

        /// <exception cref="WindowsException"/>
        public void Minimize()
        {
            if (User32.CloseWindow(Handle) == 0)
            { throw WindowsException.Get(); }
        }
    }
}
