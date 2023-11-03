namespace Win32
{
    public static class TD
    {
        unsafe public static readonly WCHAR* WARNING_ICON = Macros.MAKEINTRESOURCEW(unchecked((WORD)(-1)));
        unsafe public static readonly WCHAR* ERROR_ICON = Macros.MAKEINTRESOURCEW(unchecked((WORD)(-2)));
        unsafe public static readonly WCHAR* INFORMATION_ICON = Macros.MAKEINTRESOURCEW(unchecked((WORD)(-3)));
        unsafe public static readonly WCHAR* SHIELD_ICON = Macros.MAKEINTRESOURCEW(unchecked((WORD)(-4)));
    }
}
