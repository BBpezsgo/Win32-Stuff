namespace Win32.LowLevel
{
    public static class FileAttributes
    {
        public const int READONLY = 0x00000001;
        public const int HIDDEN = 0x00000002;
        public const int SYSTEM = 0x00000004;
        public const int DIRECTORY = 0x00000010;
        public const int ARCHIVE = 0x00000020;
        public const int DEVICE = 0x00000040;
        public const int NORMAL = 0x00000080;
        public const int TEMPORARY = 0x00000100;
        public const int SPARSE_FILE = 0x00000200;
        public const int REPARSE_POINT = 0x00000400;
        public const int COMPRESSED = 0x00000800;
        public const int OFFLINE = 0x00001000;
        public const int NOT_CONTENT_INDEXED = 0x00002000;
        public const int ENCRYPTED = 0x00004000;
        public const int INTEGRITY_STREAM = 0x00008000;
        public const int VIRTUAL = 0x00010000;
        public const int NO_SCRUB_DATA = 0x00020000;
        public const int EA = 0x00040000;
        public const int PINNED = 0x00080000;
        public const int UNPINNED = 0x00100000;
        public const int RECALL_ON_OPEN = 0x00040000;
        public const int RECALL_ON_DATA_ACCESS = 0x00400000;
    }
}
