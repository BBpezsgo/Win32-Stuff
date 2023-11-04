namespace Win32.LowLevel
{
    public static class ControlKeyState
    {
        /// <summary>
        /// The CAPS LOCK light is on.
        /// </summary>
        public const uint CAPSLOCK_ON = 0x0080;
        /// <summary>
        /// The key is enhanced.See remarks.
        /// </summary>
        public const uint ENHANCED_KEY = 0x0100;
        /// <summary>
        /// The left ALT key is pressed.
        /// </summary>
        public const uint LEFT_ALT_PRESSED = 0x0002;
        /// <summary>
        /// The left CTRL key is pressed.
        /// </summary>
        public const uint LEFT_CTRL_PRESSED = 0x0008;
        /// <summary>
        /// The NUM LOCK light is on.
        /// </summary>
        public const uint NUMLOCK_ON = 0x0020;
        /// <summary>
        /// The right ALT key is pressed.
        /// </summary>
        public const uint RIGHT_ALT_PRESSED = 0x0001;
        /// <summary>
        /// The right CTRL key is pressed.
        /// </summary>
        public const uint RIGHT_CTRL_PRESSED = 0x0004;
        /// <summary>
        /// The SCROLL LOCK light is on.
        /// </summary>
        public const uint SCROLLLOCK_ON = 0x0040;
        /// <summary>
        /// The SHIFT key is pressed.
        /// </summary>
        public const uint SHIFT_PRESSED = 0x0010;
    }
}
