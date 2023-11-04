namespace Win32.LowLevel
{
    public static class GlobalMemory
    {
        public const int GMEM_FIXED = 0x0000;
        public const int GMEM_MOVEABLE = 0x0002;
        public const int GMEM_NOCOMPACT = 0x0010;
        public const int GMEM_NODISCARD = 0x0020;
        public const int GMEM_ZEROINIT = 0x0040;
        public const int GMEM_MODIFY = 0x0080;
        public const int GMEM_DISCARDABLE = 0x0100;
        public const int GMEM_NOT_BANKED = 0x1000;
        public const int GMEM_SHARE = 0x2000;
        public const int GMEM_DDESHARE = 0x2000;
        public const int GMEM_NOTIFY = 0x4000;
        public const int GMEM_LOWER = GMEM_NOT_BANKED;
        public const int GMEM_VALID_FLAGS = 0x7F72;
        public const int GMEM_INVALID_HANDLE = 0x8000;

        public const int GHND = (GMEM_MOVEABLE | GMEM_ZEROINIT);
        public const int GPTR = (GMEM_FIXED | GMEM_ZEROINIT);

        /* Flags returned by GlobalFlags (in addition to GMEM_DISCARDABLE) */
        public const int GMEM_DISCARDED = 0x4000;
        public const int GMEM_LOCKCOUNT = 0x00FF;

        public static HANDLE GlobalLRUNewest(HANDLE h) => h;
        public static HANDLE GlobalLRUOldest(HANDLE h) => h;
        public static void GlobalDiscard(HGLOBAL h) => Kernel32.GlobalReAlloc(h, SIZE_T.Zero, GMEM_MOVEABLE);
    }
}
