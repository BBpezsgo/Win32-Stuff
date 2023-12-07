using System.Diagnostics;
using System.Globalization;

namespace Win32.Gdi32
{
    using LowLevel;

    public readonly struct Bitmap :
        IDisposable,
        IEquatable<Bitmap>,
        System.Numerics.IEqualityOperators<Bitmap, Bitmap, bool>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HBITMAP Handle;

        public Bitmap(HWND handle) => Handle = handle;

        /// <exception cref="GdiException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public BITMAP Info => Objects.GetObject<BITMAP>(Handle);

        /// <exception cref="GdiException"/>
        public static Bitmap CreateCompatibleBitmap(HDC hdc, int width, int height)
        {
            HANDLE handle = Gdi32.CreateCompatibleBitmap(hdc, width, height);
            if (handle == HANDLE.Zero)
            { throw new GdiException($"{nameof(Gdi32.CreateCompatibleBitmap)} failed"); }
            return new Bitmap(handle);
        }

        /// <exception cref="GdiException"/>
        unsafe public static Bitmap Create(int width, int height, uint planes, uint bitCount, void* bits)
        {
            HBITMAP handle = Gdi32.CreateBitmap(width, height, planes, bitCount, bits);
            if (handle == HANDLE.Zero)
            { throw new GdiException($"{nameof(Gdi32.CreateBitmap)} failed"); }
            return new Bitmap(handle);
        }

        /// <exception cref="WindowsException"/>
        unsafe public static Bitmap LoadFromFile(string file, int width = 0, int height = 0)
        {
            string fullPath;

            if (!Path.IsPathFullyQualified(file))
            { fullPath = Path.Combine(Directory.GetCurrentDirectory(), file); }
            else
            { fullPath = file; }

            fixed (WCHAR* filePtr = fullPath)
            {
                HANDLE handle = User32.LoadImageW(HINSTANCE.Zero, filePtr, 0, width, height, 0x00000010);
                if (handle == HANDLE.Zero)
                { throw WindowsException.Get(); }
                return new Bitmap(handle);
            }
        }

        /// <exception cref="GdiException"/>
        public void Dispose()
        {
            if (Gdi32.DeleteObject(Handle) == 0)
            { throw new GdiException($"Failed to delete object ({nameof(Bitmap)}) {this}"); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public void SaveToFile(HDC dc, string fileName)
        {
            BITMAP bmpScreen = this.Info;

            BitmapFileHeader bmfHeader;
            BitmapInfoHeader bi = BitmapInfoHeader.Create();

            bi.Width = bmpScreen.Width;
            bi.Height = bmpScreen.Height;
            bi.Planes = 1;
            bi.BitCount = 32;
            bi.Compression = 0; // 0 = BI_RGB
            bi.SizeImage = 0;
            bi.PixelsPerMeterX = 0;
            bi.PixelsPerMeterY = 0;
            bi.ClrUsed = 0;
            bi.ClrImportant = 0;

            int dwBmpSize = ((bmpScreen.Width * bi.BitCount) + 31) / 32 * 4 * bmpScreen.Height;

            // Starting with 32-bit Windows, GlobalAlloc and LocalAlloc are implemented as wrapper functions that 
            // call HeapAlloc using a handle to the process's default heap. Therefore, GlobalAlloc and LocalAlloc 
            // have greater overhead than HeapAlloc.
            GlobalObject hDIB = Memory.GlobalAlloc(GlobalMemory.GHND, (uint)dwBmpSize);
            void* lpbitmap = hDIB.Lock();

            // Gets the "bits" from the bitmap, and copies them into a buffer 
            // that's pointed to by lpbitmap.
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
            int scanlinesCopied = Gdi32.GetDIBits(dc, this, 0,
                  (uint)bmpScreen.Height,
                  lpbitmap,
                  (BitmapInfo*)&bi, DIBitsUsage.RGB_COLORS);
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type

            HANDLE hFile;

            fixed (char* fileNamePtr = fileName)
            {
                hFile = Kernel32.CreateFile(fileNamePtr,
                    AccessRight.GenericWrite,
                    0,
                    null,
                    CreateFileFlags.CREATE_ALWAYS,
                    Win32.LowLevel.FileAttributes.NORMAL,
                    HICON.Zero);
            }

            if (hFile == Kernel32.INVALID_HANDLE_VALUE)
            { throw WindowsException.Get(); }

            // Add the size of the headers to the size of the bitmap to get the total file size.
            int dwSizeofDIB = dwBmpSize + sizeof(BitmapFileHeader) + sizeof(BitmapInfoHeader);

            // Offset to where the actual bitmap bits start.
            bmfHeader.OffBits = (uint)(sizeof(BitmapFileHeader) + sizeof(BitmapInfoHeader));

            // Size of the file.
            bmfHeader.Size = (uint)dwSizeofDIB;

            // bfType must always be BM for Bitmaps.
            bmfHeader.Type = 0x4D42;

            uint dwBytesWritten = 0;

            int res = Kernel32.WriteFile(hFile, &bmfHeader, (uint)sizeof(BitmapFileHeader), &dwBytesWritten, null);
            if (res == 0)
            {
                uint error = Kernel32.GetLastError();
                if (error != 0x000003E5)
                { throw WindowsException.Get(); }
            }
            res = Kernel32.WriteFile(hFile, &bi, (uint)sizeof(BitmapInfoHeader), &dwBytesWritten, null);
            if (res == 0)
            {
                uint error = Kernel32.GetLastError();
                if (error != 0x000003E5)
                { throw WindowsException.Get(); }
            }
            res = Kernel32.WriteFile(hFile, lpbitmap, (uint)dwBmpSize, &dwBytesWritten, null);
            if (res == 0)
            {
                uint error = Kernel32.GetLastError();
                if (error != 0x000003E5)
                { throw WindowsException.Get(); }
            }

            // Unlock and Free the DIB from the heap.
            hDIB.Unlock();
            hDIB.Dispose();

            // Close the handle for the file that was created.
            if (Kernel32.CloseHandle(hFile) == 0)
            { throw WindowsException.Get(); }
        }

        public static implicit operator HBITMAP(Bitmap bitmap) => bitmap.Handle;
        public static explicit operator Bitmap(HBITMAP handle) => new(handle);

        public static bool operator ==(Bitmap left, Bitmap right) => left.Equals(right);
        public static bool operator !=(Bitmap left, Bitmap right) => !left.Equals(right);

        public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        public override bool Equals(object? obj) => obj is Bitmap bitmap && Equals(bitmap);
        public bool Equals(Bitmap other) => Handle == other.Handle;
        public override int GetHashCode() => Handle.GetHashCode();
    }
}
