namespace Win32.LowLevel
{
    public static class ScrollBarControlMessage
    {
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint SETPOS = 0x00E0;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint GETPOS = 0x00E1;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint SETRANGE = 0x00E2;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint SETRANGEREDRAW = 0x00E6;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint GETRANGE = 0x00E3;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint ENABLE_ARROWS = 0x00E4;

        // if (WINVER >= 0x0400)
        public const uint SETSCROLLINFO = 0x00E9;
        public const uint GETSCROLLINFO = 0x00EA;

        // if (_WIN32_WINNT >= 0x0501)
        public const uint GETSCROLLBARINFO = 0x00EB;
    }

    public static class SIF
    {
        // if (WINVER >= 0x0400)
        public const uint RANGE = 0x0001;
        public const uint PAGE = 0x0002;
        public const uint POS = 0x0004;
        public const uint DISABLENOSCROLL = 0x0008;
        public const uint TRACKPOS = 0x0010;
        public const uint ALL = RANGE | PAGE | POS | TRACKPOS;
    }
}
