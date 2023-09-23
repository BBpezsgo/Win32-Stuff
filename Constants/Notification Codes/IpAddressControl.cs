namespace Win32.Constants.Notification_Codes
{
    public static class IPN
    {
        public const uint IPN_FIRST = unchecked(0U - 860U);
        public const uint IPN_LAST = unchecked(0U - 879U);

        public const ushort IPN_FIELDCHANGED = unchecked((ushort)(IPN_FIRST - 0));
    }
}
