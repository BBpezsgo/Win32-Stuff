namespace Win32.LowLevel
{
    public static class TD
    {
        public static readonly unsafe WCHAR* WARNING_ICON = Macros.MAKEINTRESOURCEW(unchecked((WORD)(-1)));
        public static readonly unsafe WCHAR* ERROR_ICON = Macros.MAKEINTRESOURCEW(unchecked((WORD)(-2)));
        public static readonly unsafe WCHAR* INFORMATION_ICON = Macros.MAKEINTRESOURCEW(unchecked((WORD)(-3)));
        public static readonly unsafe WCHAR* SHIELD_ICON = Macros.MAKEINTRESOURCEW(unchecked((WORD)(-4)));
    }
}
