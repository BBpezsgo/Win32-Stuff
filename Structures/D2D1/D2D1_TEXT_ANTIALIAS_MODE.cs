namespace Win32.D2D1
{
    /// <summary>
    /// Describes the antialiasing mode used for drawing text.
    /// </summary>
    public enum D2D1_TEXT_ANTIALIAS_MODE : DWORD
    {
        /// <summary>
        /// Render text using the current system setting.
        /// </summary>
        DEFAULT = 0,

        /// <summary>
        /// Render text using ClearType.
        /// </summary>
        CLEARTYPE = 1,

        /// <summary>
        /// Render text using gray-scale.
        /// </summary>
        GRAYSCALE = 2,

        /// <summary>
        /// Render text aliased.
        /// </summary>
        ALIASED = 3,
    }
}
