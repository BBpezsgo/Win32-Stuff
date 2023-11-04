namespace Win32.LowLevel
{
    public static class SS
    {
        public const uint LEFT = 0x00000000;
        public const uint CENTER = 0x00000001;
        public const uint RIGHT = 0x00000002;
        public const uint ICON = 0x00000003;
        public const uint BLACKRECT = 0x00000004;
        public const uint GRAYRECT = 0x00000005;
        public const uint WHITERECT = 0x00000006;
        public const uint BLACKFRAME = 0x00000007;
        public const uint GRAYFRAME = 0x00000008;
        public const uint WHITEFRAME = 0x00000009;
        public const uint USERITEM = 0x0000000A;
        public const uint SIMPLE = 0x0000000B;
        public const uint LEFTNOWORDWRAP = 0x0000000C;
        public const uint OWNERDRAW = 0x0000000D;
        public const uint BITMAP = 0x0000000E;
        public const uint ENHMETAFILE = 0x0000000F;
        public const uint ETCHEDHORZ = 0x00000010;
        public const uint ETCHEDVERT = 0x00000011;
        public const uint ETCHEDFRAME = 0x00000012;
        public const uint TYPEMASK = 0x0000001F;
        public const uint REALSIZECONTROL = 0x00000040;
        /* Don't do "&" character translation */
        public const uint NOPREFIX = 0x00000080;
        public const uint NOTIFY = 0x00000100;
        public const uint CENTERIMAGE = 0x00000200;
        public const uint RIGHTJUST = 0x00000400;
        public const uint REALSIZEIMAGE = 0x00000800;
        public const uint SUNKEN = 0x00001000;
        public const uint EDITCONTROL = 0x00002000;
        public const uint ENDELLIPSIS = 0x00004000;
        public const uint PATHELLIPSIS = 0x00008000;
        public const uint WORDELLIPSIS = 0x0000C000;
        public const uint ELLIPSISMASK = 0x0000C000;
    }
}
