namespace Win32.LowLevel
{
    public static class IPAddressControlNotification
    {
        public const uint FIRST = unchecked(0U - 860U);
        public const uint LAST = unchecked(0U - 879U);

        public const ushort FIELDCHANGED = unchecked((ushort)(FIRST - 0));
    }
}
