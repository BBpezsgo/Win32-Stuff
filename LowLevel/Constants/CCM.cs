namespace Win32.LowLevel
{
    public static class CCM
    {
        public const uint LVM_FIRST = 0x1000;      // ListView messages
        public const uint TV_FIRST = 0x1100;       // TreeView messages
        public const uint HDM_FIRST = 0x1200;      // Header messages
        public const uint TCM_FIRST = 0x1300;      // Tab control messages

        public const uint PGM_FIRST = 0x1400;      // Pager control messages

        // if (NTDDI_VERSION >= NTDDI_WINXP)
        public const uint ECM_FIRST = 0x1500;      // Edit control messages
        public const uint BCM_FIRST = 0x1600;      // Button control messages
        public const uint CBM_FIRST = 0x1700;      // Combobox control messages
        // endif

        public const uint CCM_FIRST = 0x2000;      // Common control shared messages
        public const uint CCM_LAST = CCM_FIRST + 0x200;


        public const uint CCM_SETBKCOLOR = CCM_FIRST + 1; // lParam is bkColor

        public const uint CCM_SETCOLORSCHEME = CCM_FIRST + 2; // lParam is color scheme
        public const uint CCM_GETCOLORSCHEME = CCM_FIRST + 3; // fills in COLORSCHEME pointed to by lParam
        public const uint CCM_GETDROPTARGET = CCM_FIRST + 4;
        public const uint CCM_SETUNICODEFORMAT = CCM_FIRST + 5;
        public const uint CCM_GETUNICODEFORMAT = CCM_FIRST + 6;

        public const uint CCM_SETVERSION = CCM_FIRST + 0x7;
        public const uint CCM_GETVERSION = CCM_FIRST + 0x8;
        public const uint CCM_SETNOTIFYWINDOW = CCM_FIRST + 0x9; // wParam == hwndParent
        // if (NTDDI_VERSION >= NTDDI_WINXP)        
        public const uint CCM_SETWINDOWTHEME = CCM_FIRST + 0xb;
        public const uint CCM_DPISCALE = CCM_FIRST + 0xc; // wParam == Awareness
        // endif

        // for tooltips
        public const uint INFOTIPSIZE = 1024;
    }
}
