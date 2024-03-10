namespace Win32.Forms;

/// <summary>
/// Contains extended parameters for the <see cref="User32.TrackPopupMenuEx"/> function.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct TrackPopupMenuParams
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD StructSize;

    /// <summary>
    /// The rectangle to be excluded when positioning the window, in screen coordinates.
    /// </summary>
    public RECT Exclude;

    TrackPopupMenuParams(DWORD structSize) : this() => StructSize = structSize;

    public static unsafe TrackPopupMenuParams Create() => new((DWORD)sizeof(TrackPopupMenuParams));
}
