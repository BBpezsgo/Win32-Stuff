namespace Win32.Gdi32;

/// <summary>
/// Input structure for <see cref="Gdiplus.GdiplusStartup"/>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct GdiplusStartupInput
{
    /// <summary>
    /// Must be 1  (or 2 for the Ex version)
    /// </summary>
    readonly UINT32 GdiplusVersion;

    /// <summary>
    /// Ignored on free builds
    /// </summary>
    readonly delegate*<DebugEventLevel, CHAR*, void> DebugEventCallback;

    /// <summary>
    /// FALSE unless you're prepared to call
    /// the hook/unhook functions properly
    /// </summary>
    readonly BOOL SuppressBackgroundThread;

    /// <summary>
    /// FALSE unless you want GDI+ only to use
    /// its internal image codecs.
    /// </summary>
    readonly BOOL SuppressExternalCodecs;

    public GdiplusStartupInput(
        delegate*<DebugEventLevel, CHAR*, void> debugEventCallback = null,
        BOOL suppressBackgroundThread = FALSE,
        BOOL suppressExternalCodecs = FALSE)
    {
        GdiplusVersion = 1;
        DebugEventCallback = debugEventCallback;
        SuppressBackgroundThread = suppressBackgroundThread;
        SuppressExternalCodecs = suppressExternalCodecs;
    }
}
