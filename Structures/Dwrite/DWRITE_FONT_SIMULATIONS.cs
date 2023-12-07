namespace Win32.DWrite
{
    /// <summary>
    /// Specifies algorithmic style simulations to be applied to the font face.
    /// Bold and oblique simulations can be combined via bitwise OR operation.
    /// </summary>
    public enum DWRITE_FONT_SIMULATIONS
    {
        /// <summary>
        /// No simulations are performed.
        /// </summary>
        NONE = 0x0000,

        /// <summary>
        /// Algorithmic emboldening is performed.
        /// </summary>
        BOLD = 0x0001,

        /// <summary>
        /// Algorithmic italicization is performed.
        /// </summary>
        OBLIQUE = 0x0002
    }
}
