namespace Win32;

[Flags]
public enum ControlKeyState : DWORD
{
    /// <summary>
    /// The right ALT key is pressed.
    /// </summary>
    RightAlt = 0x0001,
    /// <summary>
    /// The left ALT key is pressed.
    /// </summary>
    LeftAlt = 0x0002,
    /// <summary>
    /// The right CTRL key is pressed.
    /// </summary>
    RightCtrl = 0x0004,
    /// <summary>
    /// The left CTRL key is pressed.
    /// </summary>
    LeftCtrl = 0x0008,
    /// <summary>
    /// The SHIFT key is pressed.
    /// </summary>
    Shift = 0x0010,
    /// <summary>
    /// The NUM LOCK light is on.
    /// </summary>
    NumLock = 0x0020,
    /// <summary>
    /// The SCROLL LOCK light is on.
    /// </summary>
    ScrollLock = 0x0040,
    /// <summary>
    /// The CAPS LOCK light is on.
    /// </summary>
    CapsLock = 0x0080,
    /// <summary>
    /// The key is enhanced.
    /// </summary>
    EnhancedKey = 0x0100,
}
