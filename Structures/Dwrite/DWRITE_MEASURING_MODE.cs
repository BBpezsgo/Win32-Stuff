namespace Win32.DWrite
{
    /// <summary>
    /// The measuring method used for text layout.
    /// </summary>
    public enum DWRITE_MEASURING_MODE
    {
        /// <summary>
        /// Text is measured using glyph ideal metrics whose values are independent to the current display resolution.
        /// </summary>
        NATURAL,

        /// <summary>
        /// Text is measured using glyph display compatible metrics whose values tuned for the current display resolution.
        /// </summary>
        GDI_CLASSIC,

        /// <summary>
        /// Text is measured using the same glyph display metrics as text measured by GDI using a font
        /// created with CLEARTYPE_NATURAL_QUALITY.
        /// </summary>
        GDI_NATURAL
    }
}
