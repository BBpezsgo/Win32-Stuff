namespace Win32
{
    public static class PBM
    {
        public const uint PBS_SMOOTH = 0x01;
        public const uint PBS_VERTICAL = 0x04;

        public const uint PBM_SETRANGE = WM.WM_USER + 1;
        public const uint PBM_SETPOS = WM.WM_USER + 2;
        public const uint PBM_DELTAPOS = WM.WM_USER + 3;
        public const uint PBM_SETSTEP = WM.WM_USER + 4;
        public const uint PBM_STEPIT = WM.WM_USER + 5;
        public const uint PBM_SETRANGE32 = WM.WM_USER + 6; // lParam = high, wParam = low

        public const uint PBM_GETRANGE = WM.WM_USER + 7; // wParam = return (TRUE ? low : high). lParam = PPBRANGE or NULL
        public const uint PBM_GETPOS = WM.WM_USER + 8;
        public const uint PBM_SETBARCOLOR = WM.WM_USER + 9; // lParam = bar color
        public const uint PBM_SETBKCOLOR = CCM.CCM_SETBKCOLOR; // lParam = bkColor

        // if (NTDDI_VERSION >= NTDDI_WINXP)
        public const uint PBS_MARQUEE = 0x08;
        // endif

        // if (NTDDI_VERSION >= NTDDI_WINXP)
        public const uint PBM_SETMARQUEE = WM.WM_USER + 10;
        // endif

        // if (NTDDI_VERSION >= NTDDI_VISTA)
        public const uint PBS_SMOOTHREVERSE = 0x10;
        // endif

        // if (NTDDI_VERSION >= NTDDI_VISTA)
        public const uint PBM_GETSTEP = WM.WM_USER + 13;
        public const uint PBM_GETBKCOLOR = WM.WM_USER + 14;
        public const uint PBM_GETBARCOLOR = WM.WM_USER + 15;
        public const uint PBM_SETSTATE = WM.WM_USER + 16; // wParam = PBST_[State] (NORMAL, ERROR, PAUSED)
        public const uint PBM_GETSTATE = WM.WM_USER + 17;

        public const uint PBST_NORMAL = 0x0001;
        public const uint PBST_ERROR = 0x0002;
        public const uint PBST_PAUSED = 0x0003;
        // endif

        // endif

    }
}
