using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    /// <summary>
    /// Memory Device Context
    /// </summary>
    public readonly struct MemoryDC : IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HDC _handle;

        internal MemoryDC(HDC handle) => _handle = handle;

        public static implicit operator HDC(MemoryDC dc) => dc._handle;
        public static explicit operator MemoryDC(HDC dc) => new(dc);

        public void Dispose()
        {
            if (Gdi32.DeleteDC(_handle) == FALSE)
            { throw new NotWindowsException($"Failed to delete memory DC {this}"); }
        }

        /// <exception cref="NotWindowsException"/>
        public static MemoryDC Create(HDC hdc)
        {
            HDC handle = Gdi32.CreateCompatibleDC(hdc);
            if (handle == HDC.Zero)
            { throw new NotWindowsException($"{nameof(Gdi32.CreateCompatibleDC)} failed"); }
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
