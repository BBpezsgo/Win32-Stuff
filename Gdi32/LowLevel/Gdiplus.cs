namespace Win32.Gdi32;

[SupportedOSPlatform("windows")]
public static class Gdiplus
{
    [DllImport("Gdiplus.dll", SetLastError = true)]
    public static extern void GdiplusShutdown(
      ULONG_PTR token
    );

    [DllImport("Gdiplus.dll", SetLastError = true)]
    public static extern unsafe Status GdiplusStartup(
      ULONG_PTR* token,
      GdiplusStartupInput* input,
      GdiplusStartupOutput* output
    );
}
