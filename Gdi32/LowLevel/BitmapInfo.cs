namespace Win32.Gdi32;

[StructLayout(LayoutKind.Sequential)]
public struct BitmapInfo
{
    public BitmapInfoHeader Header;
    public RGBQuad Colors;
}
