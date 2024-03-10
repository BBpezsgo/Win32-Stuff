namespace Win32.D2D1;

/// <summary>
/// Contains rendering options (hardware or software), pixel format, DPI
/// information, remoting options, and Direct3D support requirements for a render
/// target.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct RenderTargetProperties
{
    public RenderTargetType Type;
    public PixelFormat PixelFormat;
    public FLOAT DpiX;
    public FLOAT DpiY;
    public RenderTargetUsage Usage;
    public FeatureLevel MinLevel;
}
