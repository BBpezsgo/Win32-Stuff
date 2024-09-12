namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
public readonly struct WindowInfo
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD StructSize;

    /// <summary>
    /// The coordinates of the window.
    /// </summary>
    public readonly RECT WindowRect;
    /// <summary>
    /// The coordinates of the client area.
    /// </summary>
    public readonly RECT ClientRect;
    /// <summary>
    /// The window styles.
    /// </summary>
    public readonly DWORD Styles;
    /// <summary>
    /// The extended window styles.
    /// </summary>
    public readonly DWORD StylesEx;
    /// <summary>
    /// The window status.
    /// If this member is <see cref="WindowStyles.ACTIVECAPTION"/> (0x0001),
    /// the window is active. Otherwise, this member is zero.
    /// </summary>
    public readonly DWORD WindowStatus;
    /// <summary>
    /// The width of the window border, in pixels.
    /// </summary>
    public readonly UINT WindowBorderWidth;
    /// <summary>
    /// The height of the window border, in pixels.
    /// </summary>
    public readonly UINT WindowBorderHeight;
    /// <summary>
    /// The window class atom (see <see cref="User32.RegisterClassExW"/>).
    /// </summary>
    public readonly ATOM WindowClassAtom;
    /// <summary>
    /// The Windows version of the application that created the window.
    /// </summary>
    public readonly WORD CreatorVersion;

    WindowInfo(DWORD structSize) : this() => this.StructSize = structSize;

    public static unsafe WindowInfo Create() => new((DWORD)sizeof(WindowInfo));
}
