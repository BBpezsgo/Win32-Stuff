using System.Runtime.InteropServices;

namespace Win32.Gdi32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct BitmapInfoHeader
    {
        readonly DWORD StructSize;

        public LONG Width;
        public LONG Height;
        public WORD Planes;
        public WORD BitCount;
        public DWORD Compression;
        public DWORD SizeImage;
        public LONG PixelsPerMeterX;
        public LONG PixelsPerMeterY;
        public DWORD ClrUsed;
        public DWORD ClrImportant;

        BitmapInfoHeader(DWORD structSize) : this() => StructSize = structSize;
        unsafe public static BitmapInfoHeader Create() => new((uint)sizeof(BitmapInfoHeader));
    }
}
