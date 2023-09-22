namespace Win32.Utilities
{
    public delegate LRESULT FormEventHandler(Form sender, uint msg, WPARAM wParam, LPARAM lParam);

    public class Form : Window, IDisposable
    {
        static readonly Dictionary<HWND, Form> Handlers = new();

        bool IsDisposed;
        readonly FormEventHandler? EventHandler;
        public readonly Dictionary<ushort, Control> Controls;

        unsafe public Form(string title, int width, int height, DWORD style = WS.WS_OVERLAPPEDWINDOW | WS.WS_VISIBLE, FormEventHandler? eventHandler = null) : base()
        {
            Handle = Create(
                title,
                width,
                height,
                &WinProc,
                style);
            Handlers.Add(Handle, this);

            IsDisposed = false;
            EventHandler = eventHandler;
            Controls = new Dictionary<ushort, Control>();
        }

        unsafe public static HWND Create(string title, int width, int height, delegate*<HWND, uint, WPARAM, LPARAM, LRESULT> windProc, DWORD style = WS.WS_OVERLAPPEDWINDOW | WS.WS_VISIBLE, void* lpParam = null, string className = "windowClass")
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

                return User32.CreateWindowExW(exStyles,
                    classNamePtr,
                    windowNamePtr,
                    style,
                    0, 0,
                    width, height,
                    HWND.Zero,
                    HMENU.Zero,
                    HINSTANCE.Zero,
                    lpParam);
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

            if (!disposing) return;

            Handlers.Remove(Handle);
        }

        static LRESULT WinProc(HWND hwnd, uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            if (Handlers.TryGetValue(hwnd, out Form? window) && window.Handle == hwnd)
            { return window.HandleEventInternal(uMsg, wParam, lParam); }

            return User32.DefWindowProcW(hwnd, uMsg, wParam, lParam);
        }

        LRESULT HandleEventInternal(uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            if (EventHandler != null) return EventHandler.Invoke(this, uMsg, wParam, lParam);
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

        public unsafe void HandleEvents()
        {
            Message msg;
            int res;

            if (!IsDisposed && (res = User32.PeekMessageW(&msg, Handle, 0, 0, PM.PM_REMOVE)) != 0)
            {
                if (res == -1)
                { throw WindowsException.Get(); }

                User32.DispatchMessageW(&msg);
            }
        }

        public unsafe void HandleEventsBlocking()
        {
            Message msg;
            int res;

            while (!IsDisposed && (res = User32.GetMessageW(&msg, Handle, 0, 0)) != 0)
            {
                if (res == -1)
                { throw WindowsException.Get(); }

                User32.DispatchMessageW(&msg);
            }
        }

        public void Close()
        {
            if (User32.PostMessageW(Handle, WM.WM_CLOSE, WPARAM.Zero, LPARAM.Zero) == 0)
            { throw WindowsException.Get(); }
        }
    }
}
