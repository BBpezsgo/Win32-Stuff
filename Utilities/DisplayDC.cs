using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    /// <summary>
    /// Display Device Context
    /// </summary>
    public readonly struct DisplayDC : IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HDC _handle;
        readonly HWND _window;

        internal DisplayDC(HDC handle, HWND window)
        {
            _handle = handle;
            _window = window;
        }

        public static implicit operator HDC(DisplayDC dc) => dc._handle;

        public void Dispose()
        {
            if (User32.ReleaseDC(_window, _handle) == FALSE)
            { throw new NotWindowsException($"Failed to release DC ({nameof(User32.ReleaseDC)}) {this}"); }
        }

        /// <exception cref="NotWindowsException"/>
        public static DisplayDC Get(HWND window)
        {
            HDC handle = User32.GetDC(window);
            if (handle == HDC.Zero)
            { throw new NotWindowsException($"Failed to get DC ({nameof(User32.GetDC)}) of window {window}"); }
            return new DisplayDC(handle, window);
        }

        /// <exception cref="NotWindowsException"/>
        public MemoryDC CreateMemoryDC()
        {
            HDC handle = Gdi32.CreateCompatibleDC(_handle);
            if (handle == HDC.Zero)
            { throw new NotWindowsException($"Failed to create memory DC ({nameof(Gdi32.CreateCompatibleDC)}) from DC {this}"); }
            return new MemoryDC(handle);
        }

        #region Same for all DCs

        public override string ToString() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        /// <exception cref="NotWindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public GdiColor BrushColor
        {
            readonly get => DC.GetBrushColor(_handle);
            set => DC.SetBrushColor(_handle, value);
        }

        /// <exception cref="NotWindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public GdiColor PenColor
        {
            readonly get => DC.GetPenColor(_handle);
            set => DC.SetPenColor(_handle, value);
        }

        /// <exception cref="NotWindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public GdiColor BackgroundColor
        {
            readonly get => DC.GetBgColor(_handle);
            set => DC.SetBgColor(_handle, value);
        }

        /// <exception cref="NotWindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public StretchMode StretchMode
        {
            readonly get => DC.GetStretchMode(_handle);
            set => DC.SetStretchMode(_handle, value);
        }

        public readonly GdiColor GetPixel(int x, int y) => DC.GetPixel(_handle, x, y);
        /// <exception cref="NotWindowsException"/>
        public void SetPixel(int x, int y, GdiColor color) => DC.SetPixel(_handle, x, y, color);
        /// <exception cref="NotWindowsException"/>
        public void SetPixelV(int x, int y, GdiColor color) => DC.SetPixelV(_handle, x, y, color);

        #endregion

    }
}
