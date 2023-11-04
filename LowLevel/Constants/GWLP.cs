namespace Win32.LowLevel
{
    public static class GWLP
    {
        /// <summary>
        /// Sets a new application instance handle.
        /// </summary>
        public const int HINSTANCE = -6;
        /// <summary>
        /// Sets a new identifier of the child window.
        /// The window cannot be a top-level window.
        /// </summary>
        public const int ID = -12;
        /// <summary>
        /// Sets the user data associated with the window.
        /// This data is intended for use by the application
        /// that created the window. Its value is initially zero.
        /// </summary>
        public const int USERDATA = -21;
        /// <summary>
        /// Sets a new address for the window procedure.
        /// </summary>
        public const int WNDPROC = -4;
    }

    public static class GWL
    {
        /// <summary>
        /// Sets a new extended window style.
        /// </summary>
        public const int EXSTYLE = -20;
        /// <summary>
        /// Sets a new window style.
        /// </summary>
        public const int STYLE = -16;
    }
}
