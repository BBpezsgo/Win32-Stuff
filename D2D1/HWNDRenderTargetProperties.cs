namespace Win32.D2D1;

/// <summary>
/// Contains the HWND, pixel size, and presentation options for an
/// ID2D1HwndRenderTarget.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HWNDRenderTargetProperties
{
    public HWND hwnd;
    public SizeU PixelSize;
    public PresentOptions PresentOptions;
}
