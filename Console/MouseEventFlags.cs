namespace Win32.Console;

[Flags]
public enum MouseEventFlags : DWORD
{
    /// <summary>
    /// A change in mouse position occurred.
    /// </summary>
    MouseMoved = 0x0001,
    /// <summary>
    /// The second click(button press) of a double-click occurred.The first click is returned as a regular button-press event.
    /// </summary>
    DoubleClick = 0x0002,
    /// <summary>
    /// <para>
    /// The vertical mouse wheel was moved.
    /// </para>
    /// <para>
    ///
    /// If the high word of the dwButtonState member contains a positive value, the wheel was rotated forward, away from the user. Otherwise, the wheel was rotated backward, toward the user.</para>
    /// </summary>
    MouseWheeled = 0x0004,
    /// <summary>
    /// <para>
    /// The horizontal mouse wheel was moved.
    /// </para>
    /// <para>
    /// If the high word of the dwButtonState member contains a positive value, the wheel was rotated to the right. Otherwise, the wheel was rotated to the left.
    /// </para>
    /// </summary>
    MouseWheeledHorizontal = 0x0008,
}
