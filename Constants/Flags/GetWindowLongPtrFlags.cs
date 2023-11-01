namespace Win32
{
    public static class GWL
    {
        /// <summary>
        /// Retrieves the extended window styles.
        /// </summary>
        public const int EXSTYLE = -20;
        /// <summary>
        /// Retrieves a handle to the application instance.
        /// </summary>
        public const int HINSTANCE = -6;
        /// <summary>
        /// Retrieves a handle to the parent window, if any.
        /// </summary>
        public const int HWNDPARENT = -8;
        /// <summary>
        /// Retrieves the identifier of the window.
        /// </summary>
        public const int ID = -12;
        /// <summary>
        /// Retrieves the window styles.
        /// </summary>
        public const int STYLE = -16;
        /// <summary>
        /// Retrieves the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
        /// </summary>
        public const int USERDATA = -21;
        /// <summary>
        /// Retrieves the address of the window procedure, or a handle representing the address of the window procedure. You must use the CallWindowProc function to call the window procedure.
        /// </summary>
        public const int WNDPROC = -4;
    }
}
