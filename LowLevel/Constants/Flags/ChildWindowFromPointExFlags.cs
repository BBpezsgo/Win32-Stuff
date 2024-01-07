namespace Win32.LowLevel
{
    /// <summary>
    /// Flags for <see cref="User32.ChildWindowFromPointEx"/>
    /// </summary>
    [Flags]
    public enum ChildWindowFromPointFlags : UINT
    {
        /// <summary>
        /// Does not skip any child windows
        /// </summary>
        ALL = 0x0000,
        /// <summary>
        /// Skips disabled child windows
        /// </summary>
        SKIPDISABLED = 0x0002,
        /// <summary>
        /// Skips invisible child windows
        /// </summary>
        SKIPINVISIBLE = 0x0001,
        /// <summary>
        /// Skips transparent child windows
        /// </summary>
        SKIPTRANSPARENT = 0x0004,
    }
}
