namespace Win32.Gdi32;

[SupportedOSPlatform("windows")]
public static partial class Gdiplus
{
    [LibraryImport("Gdiplus.dll", SetLastError = true)]
    public static partial void GdiplusShutdown(
      ULONG_PTR token
    );

    [LibraryImport("Gdiplus.dll", SetLastError = true)]
    public static unsafe partial Status GdiplusStartup(
      ULONG_PTR* token,
      GdiplusStartupInput* input,
      GdiplusStartupOutput* output
    );
}
