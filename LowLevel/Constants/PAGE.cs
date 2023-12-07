namespace Win32
{
    public static class PAGE
    {
        public const DWORD EXECUTE = 0x10;
        public const DWORD EXECUTE_READ = 0x20;
        public const DWORD EXECUTE_READWRITE = 0x40;
        public const DWORD EXECUTE_WRITECOPY = 0x80;
        public const DWORD NOACCESS = 0x01;
        public const DWORD READONLY = 0x02;
        public const DWORD READWRITE = 0x04;
        public const DWORD WRITECOPY = 0x08;
        public const DWORD TARGETS_INVALID = 0x40000000;
    }
}
