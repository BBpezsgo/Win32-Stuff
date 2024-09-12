namespace Win32.Console;

/// <summary>
/// Describes a mouse input event in a console <see cref="InputEvent"/> structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseEvent
{
    /// <summary>
    /// A <see cref="COORD"/> structure that contains the location of the cursor,
    /// in terms of the console screen buffer's character-cell coordinates.
    /// </summary>
    public readonly COORD MousePosition;

    /// <summary>
    /// The status of the mouse buttons. The least significant bit corresponds to
    /// the leftmost mouse button. The next least significant bit corresponds
    /// to the rightmost mouse button. The next bit indicates the
    /// next-to-leftmost mouse button. The bits then correspond left
    /// to right to the mouse buttons. A bit is 1 if the button was pressed.
    /// </summary>
    public readonly DWORD ButtonState;

    /// <summary>
    /// The state of the control keys.
    /// </summary>
    public readonly ControlKeyState ControlKeyState;

    /// <summary>
    /// The type of mouse event.
    /// </summary>
    public readonly MouseEventFlags EventFlags;

    public short Scroll => unchecked((short)BitUtils.HighWord(ButtonState));

    public MouseEvent(COORD mousePosition, DWORD buttonState, ControlKeyState controlKeyState, MouseEventFlags eventFlags)
    {
        MousePosition = mousePosition;
        ButtonState = buttonState;
        ControlKeyState = controlKeyState;
        EventFlags = eventFlags;
    }

    public override string ToString() => $"{{ Pos: {MousePosition}, State: {Convert.ToString(ButtonState, 2).PadLeft(8, '0')}, Flags: {EventFlags}, Ctrl: {ControlKeyState} }}";
}
