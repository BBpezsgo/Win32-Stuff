namespace Win32.Gdi32
{
    using LowLevel;

    /// <summary>Memory Device Context</summary>
    public class MemoryDC : DC
    {
        internal MemoryDC(HDC handle) : base(handle)
        { }

        public static explicit operator MemoryDC(HDC dc) => new(dc);

        /// <exception cref="GdiException"/>
        protected override void DisposeDC()
        {
            if (Gdi32.DeleteDC(Handle) == FALSE)
            { throw new GdiException($"Failed to delete memory DC {this}"); }
        }

        /// <exception cref="GdiException"/>
        public static MemoryDC Create(HDC hdc)
        {
            HDC handle = Gdi32.CreateCompatibleDC(hdc);
            if (handle == HDC.Zero)
            { throw new GdiException($"{nameof(Gdi32.CreateCompatibleDC)} failed"); }
            return new MemoryDC(handle);
        }
    }
}
