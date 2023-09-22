namespace Win32
{
    // Scroll Bar Messages
    public static class SBM
    {
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint SBM_SETPOS = 0x00E0;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint SBM_GETPOS = 0x00E1;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint SBM_SETRANGE = 0x00E2;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint SBM_SETRANGEREDRAW = 0x00E6;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint SBM_GETRANGE = 0x00E3;
        /** <summary><b> not in win3.1 </b></summary> */
        public const uint SBM_ENABLE_ARROWS = 0x00E4;

        // if (WINVER >= 0x0400)
        public const uint SBM_SETSCROLLINFO = 0x00E9;
        public const uint SBM_GETSCROLLINFO = 0x00EA;

        // if (_WIN32_WINNT >= 0x0501)
        public const uint SBM_GETSCROLLBARINFO = 0x00EB;

        // if (WINVER >= 0x0400)
        public const uint SIF_RANGE = 0x0001;
        public const uint SIF_PAGE = 0x0002;
        public const uint SIF_POS = 0x0004;
        public const uint SIF_DISABLENOSCROLL = 0x0008;
        public const uint SIF_TRACKPOS = 0x0010;
        public const uint SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS;
    }
}
