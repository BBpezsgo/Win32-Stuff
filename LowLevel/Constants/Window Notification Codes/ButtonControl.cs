namespace Win32.LowLevel
{
    public static class ButtonControlNotifications
    {
        public const ushort CLICKED = 0;
        public const ushort PAINT = 1;
        public const ushort HILITE = 2;
        public const ushort UNHILITE = 3;
        public const ushort DISABLE = 4;
        public const ushort DOUBLECLICKED = 5;

        public const ushort PUSHED = HILITE;
        public const ushort UNPUSHED = UNHILITE;
        public const ushort DBLCLK = DOUBLECLICKED;
        public const ushort SETFOCUS = 6;
        public const ushort KILLFOCUS = 7;
    }
}
