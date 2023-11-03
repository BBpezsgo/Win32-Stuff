namespace Win32.Utilities
{
    public readonly struct Bitmap : IDisposable
    {
        readonly HBITMAP _handle;
        public Bitmap(HWND handle) => _handle = handle;

        /// <exception cref="WindowsException"/>
        unsafe public static Bitmap LoadFromFile(string file, int width = 0, int height = 0)
        {
            string fullPath;
            if (!Path.IsPathFullyQualified(file))
            {
                fullPath = Path.Combine(Directory.GetCurrentDirectory(), file);
            }
            else
            {
                fullPath = file;
            }
            fixed (WCHAR* filePtr = fullPath)
            {
                HANDLE handle = User32.LoadImageW(HINSTANCE.Zero, filePtr, 0, width, height, 0x00000010);
                if (handle == HANDLE.Zero)
                { throw WindowsException.Get(); }
                return new Bitmap(handle);
            }
        }

        public void Dispose()
        {
            _ = Gdi32.DeleteObject(_handle);
        }

        public static implicit operator HBITMAP(Bitmap bitmap) => bitmap._handle;
    }
}
