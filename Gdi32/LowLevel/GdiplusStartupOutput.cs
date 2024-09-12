namespace Win32.Gdi32;

/// <summary>
/// Output structure for <see cref="Gdiplus.GdiplusStartup"/>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct GdiplusStartupOutput
{
    // The following 2 fields are NULL if SuppressBackgroundThread is FALSE.
    // Otherwise, they are functions which must be called appropriately to
    // replace the background thread.
    //
    // These should be called on the application's main message loop - i.e.
    // a message loop which is active for the lifetime of GDI+.
    // "NotificationHook" should be called before starting the loop,
    // and "NotificationUnhook" should be called after the loop ends.

    public delegate*<ULONG_PTR*, Status> NotificationHook;
    public delegate*<ULONG_PTR, void> NotificationUnhook;
}
