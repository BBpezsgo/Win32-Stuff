namespace Win32.Gdi32
{
    using LowLevel;

    /// <summary>Display Device Context</summary>
    public class DisplayDC : DC
    {
        readonly HWND Window;

        internal DisplayDC(HDC handle, HWND window) : base(handle)
        { Window = window; }

        /// <exception cref="GdiException"/>
        protected override void DisposeDC()
        {
            if (User32.ReleaseDC(Window, Handle) == FALSE)
            { throw new GdiException($"Failed to release DC ({nameof(User32.ReleaseDC)}) {this}"); }
        }

        /// <exception cref="GdiException"/>
        public static DisplayDC Get(HWND window)
        {
            HDC handle = User32.GetDC(window);
            if (handle == HDC.Zero)
            { throw new GdiException($"Failed to get DC ({nameof(User32.GetDC)}) of window {window}"); }
            return new DisplayDC(handle, window);
        }

        /// <exception cref="GdiException"/>
        public MemoryDC CreateMemoryDC()
        {
            HDC handle = Gdi32.CreateCompatibleDC(Handle);
            if (handle == HDC.Zero)
            { throw new GdiException($"Failed to create memory DC ({nameof(Gdi32.CreateCompatibleDC)}) from DC {this}"); }
            return new MemoryDC(handle);
        }

        public Window? GetWindow()
        {
            HWND window = User32.WindowFromDC(Handle);
            if (window == HWND.Zero)
            { return null; }
            return new Window(window);
        }
    }
}
