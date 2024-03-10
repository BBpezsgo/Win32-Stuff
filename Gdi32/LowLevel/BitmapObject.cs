namespace Win32.Gdi32;

[StructLayout(LayoutKind.Sequential)]
public struct BitmapObject
{
    public LONG Type;
    public LONG Width;
    public LONG Height;
    public LONG WidthBytes;
    public WORD Planes;
    public WORD BitsPixel;
    public unsafe void* Bits;
}
