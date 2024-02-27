namespace Win32.LowLevel
{
    public static class ButtonControlStyles
    {
        public const DWORD SPLITBUTTON = 0x0000000C;
        public const DWORD DEFSPLITBUTTON = 0x0000000D;
        public const DWORD COMMANDLINK = 0x0000000E;
        public const DWORD DEFCOMMANDLINK = 0x0000000F;

        public const DWORD PUSHBUTTON = 0x00000000;
        public const DWORD DEFPUSHBUTTON = 0x00000001;
        public const DWORD CHECKBOX = 0x00000002;
        public const DWORD AUTOCHECKBOX = 0x00000003;
        public const DWORD RADIOBUTTON = 0x00000004;
        public const DWORD _3STATE = 0x00000005;
        public const DWORD AUTO3STATE = 0x00000006;
        public const DWORD GROUPBOX = 0x00000007;
        public const DWORD USERBUTTON = 0x00000008;
        public const DWORD AUTORADIOBUTTON = 0x00000009;
        public const DWORD PUSHBOX = 0x0000000A;
        public const DWORD OWNERDRAW = 0x0000000B;
        public const DWORD TYPEMASK = 0x0000000F;
        public const DWORD LEFTTEXT = 0x00000020;

        public const DWORD TEXT = 0x00000000;
        public const DWORD ICON = 0x00000040;
        public const DWORD BITMAP = 0x00000080;
        public const DWORD LEFT = 0x00000100;
        public const DWORD RIGHT = 0x00000200;
        public const DWORD CENTER = 0x00000300;
        public const DWORD TOP = 0x00000400;
        public const DWORD BOTTOM = 0x00000800;
        public const DWORD VCENTER = 0x00000C00;
        public const DWORD PUSHLIKE = 0x00001000;
        public const DWORD MULTILINE = 0x00002000;
        public const DWORD NOTIFY = 0x00004000;
        public const DWORD FLAT = 0x00008000;
        public const DWORD RIGHTBUTTON = LEFTTEXT;
    }
}
