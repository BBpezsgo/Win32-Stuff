namespace Win32
{
    public static class StdHandle
    {
        public const DWORD STD_INPUT_HANDLE = unchecked((DWORD)(-10));
        public const DWORD STD_OUTPUT_HANDLE = unchecked((DWORD)(-11));
        public const DWORD STD_ERROR_HANDLE = unchecked((DWORD)(-12));
    }
}
