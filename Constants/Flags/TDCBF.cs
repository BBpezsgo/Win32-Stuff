namespace Win32
{
    public static class TDCBF
    {
        /// <summary>
        /// The task dialog contains the push button: OK.
        /// </summary>
        public const int OK_BUTTON            = 0x0001; // selected control return value IDOK
        /// <summary>
        /// The task dialog contains the push button: Yes.
        /// </summary>
        public const int YES_BUTTON           = 0x0002; // selected control return value IDYES
        /// <summary>
        /// The task dialog contains the push button: No.
        /// </summary>
        public const int NO_BUTTON            = 0x0004; // selected control return value IDNO
        /// <summary>
        /// The task dialog contains the push button: Cancel.
        /// This button must be specified for the dialog box to
        /// respond to typical cancel actions (Alt-F4 and Escape).
        /// </summary>
        public const int CANCEL_BUTTON        = 0x0008; // selected control return value IDCANCEL
        /// <summary>
        /// The task dialog contains the push button: Retry.
        /// </summary>
        public const int CLOSE_BUTTON = 0x0020;         // selected control return value IDCLOSE
        /// <summary>
        /// The task dialog contains the push button: Close.
        /// </summary>
        public const int RETRY_BUTTON         = 0x0010; // selected control return value IDRETRY
    }
}
