namespace Win32.Gdi32;

/// <summary>Memory Device Context</summary>
[SupportedOSPlatform("windows")]
public sealed class MemoryDC : DC
{
    public MemoryDC(HDC handle) : base(handle)
    { }

    /// <exception cref="GdiException"/>
    protected override void Dispose(bool disposing)
    {
        if (Handle == HDC.Zero) return;

        if (Gdi32.DeleteDC(Handle) == FALSE)
        { throw new GdiException($"Failed to delete DC ({nameof(Gdi32.DeleteDC)}) {this}"); }

        Handle = HDC.Zero;
    }

    /// <exception cref="GdiException"/>
    public static MemoryDC Create(HDC hdc)
    {
        HDC handle = Gdi32.CreateCompatibleDC(hdc);
        if (handle == HDC.Zero)
        { throw new GdiException($"{nameof(Gdi32.CreateCompatibleDC)} failed"); }
        return new MemoryDC(handle);
    }
}
