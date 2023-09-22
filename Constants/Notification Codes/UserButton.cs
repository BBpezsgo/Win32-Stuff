namespace Win32
{
    // User Button Notification Codes
    public static class BN
    {
        public const ushort BN_CLICKED = 0;
        public const ushort BN_PAINT = 1;
        public const ushort BN_HILITE = 2;
        public const ushort BN_UNHILITE = 3;
        public const ushort BN_DISABLE = 4;
        public const ushort BN_DOUBLECLICKED = 5;

        public const ushort BN_PUSHED = BN_HILITE;
        public const ushort BN_UNPUSHED = BN_UNHILITE;
        public const ushort BN_DBLCLK = BN_DOUBLECLICKED;
        public const ushort BN_SETFOCUS = 6;
        public const ushort BN_KILLFOCUS = 7;
    }
}
