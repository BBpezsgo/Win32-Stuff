using System.Runtime.InteropServices;

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

namespace Win32.Gdi32.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static class Gdiplus
    {
        [DllImport("Gdiplus.dll", SetLastError = true)]
        public static extern void GdiplusShutdown(
          ULONG_PTR token
        );

        [DllImport("Gdiplus.dll", SetLastError = true)]
        unsafe public static extern Status GdiplusStartup(
          ULONG_PTR* token,
          GdiplusStartupInput* input,
          GdiplusStartupOutput* output
        );
    }
}
