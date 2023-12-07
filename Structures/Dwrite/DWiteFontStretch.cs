namespace Win32.DWrite
{
    /// <summary>
    /// The font stretch enumeration describes relative change from the normal aspect ratio
    /// as specified by a font designer for the glyphs in a font.
    /// Values less than 1 or greater than 9 are considered to be invalid, and they are rejected by font API functions.
    /// </summary>
    public enum DWiteFontStretch
    {
        /// <summary>
        /// Predefined font stretch : Not known (0).
        /// </summary>
        UNDEFINED = 0,

        /// <summary>
        /// Predefined font stretch : Ultra-condensed (1).
        /// </summary>
        ULTRA_CONDENSED = 1,

        /// <summary>
        /// Predefined font stretch : Extra-condensed (2).
        /// </summary>
        EXTRA_CONDENSED = 2,

        /// <summary>
        /// Predefined font stretch : Condensed (3).
        /// </summary>
        CONDENSED = 3,

        /// <summary>
        /// Predefined font stretch : Semi-condensed (4).
        /// </summary>
        SEMI_CONDENSED = 4,

        /// <summary>
        /// Predefined font stretch : Normal (5).
        /// </summary>
        NORMAL = 5,

        /// <summary>
        /// Predefined font stretch : Medium (5).
        /// </summary>
        MEDIUM = 5,

        /// <summary>
        /// Predefined font stretch : Semi-expanded (6).
        /// </summary>
        SEMI_EXPANDED = 6,

        /// <summary>
        /// Predefined font stretch : Expanded (7).
        /// </summary>
        EXPANDED = 7,

        /// <summary>
        /// Predefined font stretch : Extra-expanded (8).
        /// </summary>
        EXTRA_EXPANDED = 8,

        /// <summary>
        /// Predefined font stretch : Ultra-expanded (9).
        /// </summary>
        ULTRA_EXPANDED = 9
    }
}
