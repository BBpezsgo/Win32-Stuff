namespace Win32.Gdi32;

[StructLayout(LayoutKind.Sequential)]
public struct BitmapFileHeader
{
    public WORD Type;
    public DWORD Size;
    readonly WORD Reserved1;
    readonly WORD Reserved2;
    public DWORD OffBits;
}
