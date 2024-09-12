namespace Win32.Forms;

[Flags]
public enum MouseEventFlags : uint
{
    None = 0x0000,
    /// <summary>
    /// The left mouse button is down.
    /// </summary>
    LeftButton = 0x0001,
    /// <summary>
    /// The right mouse key is down.
    /// </summary>
    RightButton = 0x0002,
    /// <summary>
    /// The SHIFT key is down.
    /// </summary>
    Shift = 0x0004,
    /// <summary>
    /// The CTRL key is down.
    /// </summary>
    Control = 0x0008,
    /// <summary>
    /// The middle mouse button is down.
    /// </summary>
    MiddleButton = 0x0010,
    /// <summary>
    /// The first X button is down.
    /// </summary>
    XButton1 = 0x0020,
    /// <summary>
    /// The second X button is down.
    /// </summary>
    XButton2 = 0x0040,
}
