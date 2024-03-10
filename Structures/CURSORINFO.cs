namespace Win32;

public enum CursorState : DWORD
{
    /// <summary>
    /// The cursor is hidden.
    /// </summary>
    Hidden = 0,
    /// <summary>
    /// The cursor is showing.
    /// </summary>
    Showing = 1,
    /// <summary>
    /// Windows 8: The cursor is suppressed.
    /// This flag indicates
    /// that the system is not drawing the cursor because
    /// the user is providing input through touch or pen
    /// instead of the mouse.
    /// </summary>
    Suppressed = 2,
}

/// <summary>
/// Contains global cursor information.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct CursorInfo
{
    readonly DWORD StructSize;

    /// <summary>
    /// The cursor state.
    /// </summary>
    public readonly CursorState Flags;
    /// <summary>
    /// A handle to the cursor.
    /// </summary>
    public readonly HCURSOR Cursor;
    /// <summary>
    /// A structure that receives the screen coordinates of the cursor.
    /// </summary>
    public readonly POINT ScreenPosition;

    CursorInfo(DWORD structSize) : this() => StructSize = structSize;
    public static unsafe CursorInfo Create() => new((DWORD)sizeof(CursorInfo));
}
