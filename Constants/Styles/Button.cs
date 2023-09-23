namespace Win32
{
    public static class BS
    {
        public const DWORD BS_SPLITBUTTON          =0x0000000C;
        public const DWORD BS_DEFSPLITBUTTON       =0x0000000D;
        public const DWORD BS_COMMANDLINK          =0x0000000E;
        public const DWORD BS_DEFCOMMANDLINK = 0x0000000F;

        public const DWORD BS_PUSHBUTTON = 0x00000000;
        public const DWORD BS_DEFPUSHBUTTON = 0x00000001;
        public const DWORD BS_CHECKBOX = 0x00000002;
        public const DWORD BS_AUTOCHECKBOX = 0x00000003;
        public const DWORD BS_RADIOBUTTON = 0x00000004;
        public const DWORD BS_3STATE = 0x00000005;
        public const DWORD BS_AUTO3STATE = 0x00000006;
        public const DWORD BS_GROUPBOX = 0x00000007;
        public const DWORD BS_USERBUTTON = 0x00000008;
        public const DWORD BS_AUTORADIOBUTTON = 0x00000009;
        public const DWORD BS_PUSHBOX = 0x0000000A;
        public const DWORD BS_OWNERDRAW = 0x0000000B;
        public const DWORD BS_TYPEMASK = 0x0000000F;
        public const DWORD BS_LEFTTEXT = 0x00000020;

        public const DWORD BS_TEXT = 0x00000000;
        public const DWORD BS_ICON = 0x00000040;
        public const DWORD BS_BITMAP = 0x00000080;
        public const DWORD BS_LEFT = 0x00000100;
        public const DWORD BS_RIGHT = 0x00000200;
        public const DWORD BS_CENTER = 0x00000300;
        public const DWORD BS_TOP = 0x00000400;
        public const DWORD BS_BOTTOM = 0x00000800;
        public const DWORD BS_VCENTER = 0x00000C00;
        public const DWORD BS_PUSHLIKE = 0x00001000;
        public const DWORD BS_MULTILINE = 0x00002000;
        public const DWORD BS_NOTIFY = 0x00004000;
        public const DWORD BS_FLAT = 0x00008000;
        public const DWORD BS_RIGHTBUTTON = BS_LEFTTEXT;
    }
}
