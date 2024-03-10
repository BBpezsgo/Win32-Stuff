namespace Win32.Console;

/// <summary>
/// Describes a focus event in a console <see cref="InputEvent"/> structure.
/// These events are used internally and should be ignored.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct FocusEvent
{
    /// <summary>
    /// Reserved.
    /// </summary>
    public readonly BOOL SetFocus;
}
