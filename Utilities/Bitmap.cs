using System.Diagnostics;
using System.Globalization;

namespace Win32.Gdi32
{
    using LowLevel;

    [SupportedOSPlatform("windows")]
    public readonly struct Bitmap :
        IDisposable,
        IEquatable<Bitmap>,
        System.Numerics.IEqualityOperators<Bitmap, Bitmap, bool>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HBITMAP Handle;

        Bitmap(HWND handle) => Handle = handle;

        /// <exception cref="GdiException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public BitmapObject Info => Objects.GetObject<BitmapObject>(Handle);

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

        public static int CalculateDataSize(int width, int height, int bitCount) => ((width * bitCount) + 31) / 32 * 4 * height;

        public static int CalculateDataSize(BitmapInfoHeader bitmap) => Bitmap.CalculateDataSize(bitmap.Width, bitmap.Height, bitmap.BitCount);

        /// <exception cref="GdiException"/>
        public void Dispose()
        {
            if (Gdi32.DeleteObject(Handle) == 0)
            { throw new GdiException($"Failed to delete object ({nameof(Bitmap)}) {this}"); }
        }

        /// <exception cref="WindowsException"/>
        /// <exception cref="GdiException"/>
        unsafe public void SaveToFile(HDC dc, string fileName)
        {
            BitmapObject bmpScreen = this.Info;

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

            int dwBmpSize = CalculateDataSize(bi);

            // Starting with 32-bit Windows, GlobalAlloc and LocalAlloc are implemented as wrapper functions that 
            // call HeapAlloc using a handle to the process's default heap. Therefore, GlobalAlloc and LocalAlloc 
            // have greater overhead than HeapAlloc.
            GlobalObject hDIB = GlobalMemory.Alloc((uint)dwBmpSize, GMEM.GHND);
            void* lpbitmap = hDIB.Lock();

            // Gets the "bits" from the bitmap, and copies them into a buffer 
            // that's pointed to by lpbitmap.
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
            int scanlinesCopied = Gdi32.GetDIBits(
                dc,
                this,
                0,
                (uint)bmpScreen.Height,
                lpbitmap,
                (BitmapInfo*)&bi,
                DIBitsUsage.RGB_COLORS);
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
            if (scanlinesCopied == 0)
            { throw new GdiException($"Failed to get the bits from the bitmap"); }

            FileHandle hFile = FileHandle.Create(
                fileName,
                AccessRight.GenericWrite,
                0,
                null,
                CreateFileFlags.CREATE_ALWAYS,
                Win32.LowLevel.FileAttributes.NORMAL,
                HICON.Zero);

            // Add the size of the headers to the size of the bitmap to get the total file size.
            int dwSizeofDIB = dwBmpSize + sizeof(BitmapFileHeader) + sizeof(BitmapInfoHeader);

            BitmapFileHeader bmfHeader = new()
            {
                // Offset to where the actual bitmap bits start.
                OffBits = (uint)(sizeof(BitmapFileHeader) + sizeof(BitmapInfoHeader)),
                // Size of the file.
                Size = (uint)dwSizeofDIB,
                // bfType must always be BM for Bitmaps.
                Type = 0x4D42,
            };
            
            hFile.Write(&bmfHeader);
            hFile.Write(&bi);
            hFile.Write(lpbitmap, dwBmpSize);

            // Unlock and Free the DIB from the heap.
            hDIB.Unlock();
            hDIB.Dispose();

            // Close the handle for the file that was created.
            hFile.Dispose();
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
