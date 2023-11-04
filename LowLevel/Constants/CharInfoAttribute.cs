namespace Win32.LowLevel
{
    public static class CharInfoAttribute
    {
        public static ushort Make(int background, int foreground)
        {
            background &= 0b_1111;
            foreground &= 0b_1111;
            return unchecked((ushort)((background << 4) | foreground));
        }

        public const ushort NONE = 0x0000;

        /// <summary>
        /// Text color contains blue.
        /// </summary>
        public const ushort FOREGROUND_BLUE = 0x0001;

        /// <summary>
        /// Text color contains green.
        /// </summary>
        public const ushort FOREGROUND_GREEN = 0x0002;

        /// <summary>
        /// Text color contains red.
        /// </summary>
        public const ushort FOREGROUND_RED = 0x0004;

        /// <summary>
        /// Text color is intensified.
        /// </summary>
        public const ushort FOREGROUND_INTENSITY = 0x0008;

        /// <summary>
        /// Background color contains blue.
        /// </summary>
        public const ushort BACKGROUND_BLUE = 0x0010;

        /// <summary>
        /// Background color contains green.
        /// </summary>
        public const ushort BACKGROUND_GREEN = 0x0020;

        /// <summary>
        /// Background color contains red.
        /// </summary>
        public const ushort BACKGROUND_RED = 0x0040;

        /// <summary>
        /// Background color is intensified.
        /// </summary>
        public const ushort BACKGROUND_INTENSITY = 0x0080;

        /// <summary>
        /// Leading byte.
        /// </summary>
        public const ushort COMMON_LVB_LEADING_BYTE = 0x0100;

        /// <summary>
        /// Trailing byte.
        /// </summary>
        public const ushort COMMON_LVB_TRAILING_BYTE = 0x0200;

        /// <summary>
        /// Top horizontal.
        /// </summary>
        public const ushort COMMON_LVB_GRID_HORIZONTAL = 0x0400;

        /// <summary>
        /// Left vertical.
        /// </summary>
        public const ushort COMMON_LVB_GRID_LVERTICAL = 0x0800;

        /// <summary>
        /// Right vertical.
        /// </summary>
        public const ushort COMMON_LVB_GRID_RVERTICAL = 0x1000;

        /// <summary>
        /// Reverse foreground and background attribute.
        /// </summary>
        public const ushort COMMON_LVB_REVERSE_VIDEO = 0x4000;

        /// <summary>
        /// Underscore.
        /// </summary>
        public const ushort COMMON_LVB_UNDERSCORE = 0x8000;
    }
}
