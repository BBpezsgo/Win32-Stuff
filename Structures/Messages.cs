namespace Win32
{
    public static class CBN
    {
        public const ushort CBN_ERRSPACE = ushort.MaxValue;
        public const ushort CBN_SELCHANGE = 1;
        public const ushort CBN_DBLCLK = 2;
        public const ushort CBN_SETFOCUS = 3;
        public const ushort CBN_KILLFOCUS = 4;
        public const ushort CBN_EDITCHANGE = 5;
        public const ushort CBN_EDITUPDATE = 6;
        public const ushort CBN_DROPDOWN = 7;
        public const ushort CBN_CLOSEUP = 8;
        public const ushort CBN_SELENDOK = 9;
        public const ushort CBN_SELENDCANCEL = 10;
    }

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
