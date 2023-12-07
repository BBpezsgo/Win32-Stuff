namespace Win32.DWrite
{
    /// <summary>
    /// Text granularity used to trim text overflowing the layout box.
    /// </summary>
    public enum DWRITE_TRIMMING_GRANULARITY
    {
        /// <summary>
        /// No trimming occurs. Text flows beyond the layout width.
        /// </summary>
        NONE,

        /// <summary>
        /// Trimming occurs at character cluster boundary.
        /// </summary>
        CHARACTER,

        /// <summary>
        /// Trimming occurs at word boundary.
        /// </summary>
        WORD
    }
}
