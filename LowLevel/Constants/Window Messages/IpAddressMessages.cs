namespace Win32.LowLevel
{
    public static class IPM
    {
        /// <summary>
        /// no parameters
        /// </summary>
        public const uint CLEARADDRESS = WM.WM_USER + 100;
        /// <summary>
        /// lparam = TCP/IP address
        /// </summary>
        public const uint SETADDRESS = WM.WM_USER + 101;
        /// <summary>
        /// lresult = # of non black fields.  lparam = LPDWORD for TCP/IP address
        /// </summary>
        public const uint GETADDRESS = WM.WM_USER + 102;
        /// <summary>
        /// wparam = field, lparam = range
        /// </summary>
        public const uint SETRANGE = WM.WM_USER + 103;
        /// <summary>
        /// wparam = field
        /// </summary>
        public const uint SETFOCUS = WM.WM_USER + 104;
        /// <summary>
        /// no parameters
        /// </summary>
        public const uint ISBLANK = WM.WM_USER + 105;
    }
}
