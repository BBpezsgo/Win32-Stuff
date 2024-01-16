using System.Runtime.InteropServices;

namespace Win32.Gdi32
{
    /// <summary>
    /// The <see cref="BitmapInfoHeader"/> structure contains information about
    /// the dimensions and color format of a device-independent bitmap (DIB).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct BitmapInfoHeader
    {
        readonly DWORD StructSize;

        public LONG Width;
        public LONG Height;
        public WORD Planes;
        public WORD BitCount;
        public BitmapCompression Compression;
        public DWORD SizeImage;
        public LONG PixelsPerMeterX;
        public LONG PixelsPerMeterY;
        public DWORD ClrUsed;
        public DWORD ClrImportant;

        BitmapInfoHeader(DWORD structSize) : this() => StructSize = structSize;
        public static unsafe BitmapInfoHeader Create() => new((uint)sizeof(BitmapInfoHeader));
    }
}
