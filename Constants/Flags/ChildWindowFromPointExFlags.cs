namespace Win32
{
    public static class CWP
    {
        /// <summary>
        /// Does not skip any child windows
        /// </summary>
        public const UINT ALL = 0x0000;
        /// <summary>
        /// Skips disabled child windows
        /// </summary>
        public const UINT SKIPDISABLED = 0x0002;
        /// <summary>
        /// Skips invisible child windows
        /// </summary>
        public const UINT SKIPINVISIBLE = 0x0001;
        /// <summary>
        /// Skips transparent child windows
        /// </summary>
        public const UINT SKIPTRANSPARENT = 0x0004;
    }
}
