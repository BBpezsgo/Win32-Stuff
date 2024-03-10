namespace Win32.Forms;

public static class CommonControlMessages
{
    // public const uint LVM_FIRST = 0x1000;      // ListView messages
    // public const uint TV_FIRST = 0x1100;       // TreeView messages
    // public const uint TCM_FIRST = 0x1300;      // Tab control messages
    // public const uint PGM_FIRST = 0x1400;      // Pager control messages

    // for tooltips
    public const uint INFOTIPSIZE = 1024;

    public const uint FIRST = 0x2000;
    public const uint LAST = FIRST + 0x200;

    public const uint SETBKCOLOR = FIRST + 1; // lParam is bkColor
    public const uint SETCOLORSCHEME = FIRST + 2; // lParam is color scheme
    public const uint GETCOLORSCHEME = FIRST + 3; // fills in COLORSCHEME pointed to by lParam
    public const uint GETDROPTARGET = FIRST + 4;
    public const uint SETUNICODEFORMAT = FIRST + 5;
    public const uint GETUNICODEFORMAT = FIRST + 6;
    public const uint SETVERSION = FIRST + 0x7;
    public const uint GETVERSION = FIRST + 0x8;
    public const uint SETNOTIFYWINDOW = FIRST + 0x9; // wParam == hwndParent
    public const uint SETWINDOWTHEME = FIRST + 0xb;
    public const uint DPISCALE = FIRST + 0xc; // wParam == Awareness
}
