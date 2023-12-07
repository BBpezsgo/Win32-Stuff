namespace Win32.DWrite
{
    /// <summary>
    /// The font style enumeration describes the slope style of a font face, such as Normal, Italic or Oblique.
    /// Values other than the ones defined in the enumeration are considered to be invalid, and they are rejected by font API functions.
    /// </summary>
    public enum DWRITE_FONT_STYLE
    {
        /// <summary>
        /// Font slope style : Normal.
        /// </summary>
        NORMAL,

        /// <summary>
        /// Font slope style : Oblique.
        /// </summary>
        OBLIQUE,

        /// <summary>
        /// Font slope style : Italic.
        /// </summary>
        ITALIC
    }
}
