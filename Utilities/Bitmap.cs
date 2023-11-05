using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    public readonly struct Bitmap : IDisposable
    {
        readonly HBITMAP _handle;
        public Bitmap(HWND handle) => _handle = handle;

        /// <exception cref="GdiException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public BITMAP Info => Objects.GetObject<BITMAP>(_handle);

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
            if (Gdi32.DeleteObject(_handle) == 0)
            { throw new GdiException($"Failed to delete object ({nameof(Bitmap)}) {this}"); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public void SaveToFile(HDC dc, string fileName)
        {
            BITMAP bmpScreen = this.Info;

            BITMAPFILEHEADER bmfHeader;
            BITMAPINFOHEADER bi;

            bi.biSize = (uint)sizeof(BITMAPINFOHEADER);
            bi.biWidth = bmpScreen.bmWidth;
            bi.biHeight = bmpScreen.bmHeight;
            bi.biPlanes = 1;
            bi.biBitCount = 32;
            bi.biCompression = 0; // 0 = BI_RGB
            bi.biSizeImage = 0;
            bi.biXPelsPerMeter = 0;
            bi.biYPelsPerMeter = 0;
            bi.biClrUsed = 0;
            bi.biClrImportant = 0;

            int dwBmpSize = ((bmpScreen.bmWidth * bi.biBitCount) + 31) / 32 * 4 * bmpScreen.bmHeight;

            // Starting with 32-bit Windows, GlobalAlloc and LocalAlloc are implemented as wrapper functions that 
            // call HeapAlloc using a handle to the process's default heap. Therefore, GlobalAlloc and LocalAlloc 
            // have greater overhead than HeapAlloc.
            GlobalObject hDIB = Memory.GlobalAlloc(GlobalMemory.GHND, (uint)dwBmpSize);
            void* lpbitmap = hDIB.Lock();

            // Gets the "bits" from the bitmap, and copies them into a buffer 
            // that's pointed to by lpbitmap.
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
            int scanlinesCopied = Gdi32.GetDIBits(dc, this, 0,
                  (uint)bmpScreen.bmHeight,
                  lpbitmap,
                  (BITMAPINFO*)&bi, DIB.RGB_COLORS);
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type

            HANDLE hFile;

            fixed (char* fileNamePtr = fileName)
            {
                hFile = Kernel32.CreateFile(fileNamePtr,
                    Rights.GENERIC_WRITE,
                    0,
                    null,
                    CFF.CREATE_ALWAYS,
                    FILE_ATTRIBUTE.NORMAL,
                    IntPtr.Zero);
            }

            if (hFile == Kernel32.INVALID_HANDLE_VALUE)
            { throw WindowsException.Get(); }

            // Add the size of the headers to the size of the bitmap to get the total file size.
            int dwSizeofDIB = dwBmpSize + sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER);

            // Offset to where the actual bitmap bits start.
            bmfHeader.bfOffBits = (uint)(sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER));

            // Size of the file.
            bmfHeader.bfSize = (uint)dwSizeofDIB;

            // bfType must always be BM for Bitmaps.
            bmfHeader.bfType = 0x4D42;

            uint dwBytesWritten = 0;

            int res = Kernel32.WriteFile(hFile, &bmfHeader, (uint)sizeof(BITMAPFILEHEADER), &dwBytesWritten, null);
            if (res == 0)
            {
                uint error = Kernel32.GetLastError();
                if (error != 0x000003E5)
                { throw WindowsException.Get(); }
            }
            res = Kernel32.WriteFile(hFile, &bi, (uint)sizeof(BITMAPINFOHEADER), &dwBytesWritten, null);
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

        public static implicit operator HBITMAP(Bitmap bitmap) => bitmap._handle;

        public override string ToString() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
    }
}
