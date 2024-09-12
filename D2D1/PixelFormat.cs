namespace Win32.D2D1;

/// <summary>
/// Description of a pixel format.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct PixelFormat
{
    public DXGIFormat Format;
    public AlphaMode AlphaMode;
}
