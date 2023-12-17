namespace Win32
{
    public static class GMEM
    {
        /// <summary>
        /// <list type="table">
        /// 
        /// <item>
        /// <term> <see cref="Kernel32.GlobalAlloc"/> </term>
        /// <description>
        /// Allocates fixed memory. The return value is a pointer.
        /// </description>
        /// </item>
        /// 
        /// </list>
        /// </summary>
        public const int Fixed = 0x0000;

        /// <summary>
        /// <list type="table">
        /// 
        /// <item>
        /// <term> <see cref="Kernel32.GlobalAlloc"/> </term>
        /// <description>
        /// Allocates movable memory.
        /// Memory blocks are never moved in physical memory,
        /// but they can be moved within the default heap.
        /// <br/>
        /// The return value is a handle to the memory object.
        /// To translate the handle into a pointer, use the <see cref="Kernel32.GlobalLock"/> function.
        /// <br/>
        /// This value cannot be combined with <see cref="Fixed"/>.
        /// </description>
        /// </item>
        /// 
        /// <item>
        /// <term> <see cref="Kernel32.GlobalReAlloc"/> </term>
        /// <description>
        /// If the memory is a locked <see cref="Moveable"/> memory block or
        /// a <see cref="Fixed"/> memory block and this flag is not specified,
        /// the memory can only be reallocated in place.
        /// </description>
        /// </item>
        /// 
        /// </list>
        /// </summary>
        public const int Moveable = 0x0002;

        /// <summary>
        /// <list type="table">
        /// 
        /// <item>
        /// <term> <see cref="Kernel32.GlobalAlloc"/> </term>
        /// <description>
        /// Initializes memory contents to zero.
        /// </description>
        /// </item>
        /// 
        /// <item>
        /// <term> <see cref="Kernel32.GlobalReAlloc"/> </term>
        /// <description>
        /// Causes the additional memory contents to be initialized
        /// to zero if the memory object is growing in size.
        /// </description>
        /// </item>
        /// 
        /// </list>
        /// </summary>
        public const int ZeroInit = 0x0040;

        public const int Modify = 0x0080;

        public const int ValidFlags = 0x7F72;

        public const int InvalidHandle = 0x8000;

        /// <summary>
        /// Combines <see cref="Moveable"/> and <see cref="ZeroInit"/>.
        /// </summary>
        public const int GHND = Moveable | ZeroInit;
        /// <summary>
        /// Combines <see cref="Fixed"/> and <see cref="ZeroInit"/>.
        /// </summary>
        public const int GPTR = Fixed | ZeroInit;

        /* Flags returned by GlobalFlags (in addition to DISCARDABLE) */
        public const int Discarded = 0x4000;

        public const int LockCount = 0x00FF;

        const string ObsoleteText = "This obsolete, but provided for compatibility with 16-bit Windows. This is ignored.";

        [Obsolete(ObsoleteText)]
        public const int DDeShare = 0x2000;
        [Obsolete(ObsoleteText)]
        public const int Discardable = 0x0100;
        [Obsolete(ObsoleteText)]
        public const int Lower = NotBanked;
        [Obsolete(ObsoleteText)]
        public const int NoCompact = 0x0010;
        [Obsolete(ObsoleteText)]
        public const int NoDiscard = 0x0020;
        [Obsolete(ObsoleteText)]
        public const int NotBanked = 0x1000;
        [Obsolete(ObsoleteText)]
        public const int Notify = 0x4000;
        [Obsolete(ObsoleteText)]
        public const int Share = 0x2000;
    }
}
