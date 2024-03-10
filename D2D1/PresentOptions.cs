namespace Win32.D2D1;

/// <summary>
/// Describes how present should behave.
/// </summary>
public enum PresentOptions : DWORD
{
    None = 0x00000000,

    /// <summary>
    /// Keep the target contents intact through present.
    /// </summary>
    RetainContents = 0x00000001,

    /// <summary>
    /// Do not wait for display refresh to commit changes to display.
    /// </summary>
    Immediately = 0x00000002,
}
