namespace Win32.LowLevel
{
    public static class PBM
    {
        public const uint SMOOTH = 0x01;
        public const uint VERTICAL = 0x04;

        public const uint SETRANGE = WM.WM_USER + 1;
        public const uint SETPOS = WM.WM_USER + 2;
        public const uint DELTAPOS = WM.WM_USER + 3;
        public const uint SETSTEP = WM.WM_USER + 4;
        public const uint STEPIT = WM.WM_USER + 5;
        public const uint SETRANGE32 = WM.WM_USER + 6; // lParam = high, wParam = low

        public const uint GETRANGE = WM.WM_USER + 7; // wParam = return (TRUE ? low : high). lParam = PPBRANGE or NULL
        public const uint GETPOS = WM.WM_USER + 8;
        public const uint SETBARCOLOR = WM.WM_USER + 9; // lParam = bar color
        public const uint SETBKCOLOR = CCM.CCM_SETBKCOLOR; // lParam = bkColor

        // if (NTDDI_VERSION >= NTDDI_WINXP)
        public const uint MARQUEE = 0x08;
        // endif

        // if (NTDDI_VERSION >= NTDDI_WINXP)
        public const uint SETMARQUEE = WM.WM_USER + 10;
        // endif

        // if (NTDDI_VERSION >= NTDDI_VISTA)
        public const uint SMOOTHREVERSE = 0x10;
        // endif

        // if (NTDDI_VERSION >= NTDDI_VISTA)
        public const uint GETSTEP = WM.WM_USER + 13;
        public const uint GETBKCOLOR = WM.WM_USER + 14;
        public const uint GETBARCOLOR = WM.WM_USER + 15;
        public const uint SETSTATE = WM.WM_USER + 16; // wParam = ST_[State] (NORMAL, ERROR, PAUSED)
        public const uint GETSTATE = WM.WM_USER + 17;

        public const uint ST_NORMAL = 0x0001;
        public const uint ST_ERROR = 0x0002;
        public const uint ST_PAUSED = 0x0003;
        // endif

        // endif

    }
}
