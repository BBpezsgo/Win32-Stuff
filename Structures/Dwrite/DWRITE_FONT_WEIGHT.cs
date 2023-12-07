namespace Win32.DWrite
{
    /// <summary>
    /// The font weight enumeration describes common values for degree of blackness or thickness of strokes of characters in a font.
    /// Font weight values less than 1 or greater than 999 are considered to be invalid, and they are rejected by font API functions.
    /// </summary>
    public enum DWRITE_FONT_WEIGHT
    {
        /// <summary>
        /// Predefined font weight : Thin (100).
        /// </summary>
        THIN = 100,

        /// <summary>
        /// Predefined font weight : Extra-light (200).
        /// </summary>
        EXTRA_LIGHT = 200,

        /// <summary>
        /// Predefined font weight : Ultra-light (200).
        /// </summary>
        ULTRA_LIGHT = 200,

        /// <summary>
        /// Predefined font weight : Light (300).
        /// </summary>
        LIGHT = 300,

        /// <summary>
        /// Predefined font weight : Semi-light (350).
        /// </summary>
        SEMI_LIGHT = 350,

        /// <summary>
        /// Predefined font weight : Normal (400).
        /// </summary>
        NORMAL = 400,

        /// <summary>
        /// Predefined font weight : Regular (400).
        /// </summary>
        REGULAR = 400,

        /// <summary>
        /// Predefined font weight : Medium (500).
        /// </summary>
        MEDIUM = 500,

        /// <summary>
        /// Predefined font weight : Demi-bold (600).
        /// </summary>
        DEMI_BOLD = 600,

        /// <summary>
        /// Predefined font weight : Semi-bold (600).
        /// </summary>
        SEMI_BOLD = 600,

        /// <summary>
        /// Predefined font weight : Bold (700).
        /// </summary>
        BOLD = 700,

        /// <summary>
        /// Predefined font weight : Extra-bold (800).
        /// </summary>
        EXTRA_BOLD = 800,

        /// <summary>
        /// Predefined font weight : Ultra-bold (800).
        /// </summary>
        ULTRA_BOLD = 800,

        /// <summary>
        /// Predefined font weight : Black (900).
        /// </summary>
        BLACK = 900,

        /// <summary>
        /// Predefined font weight : Heavy (900).
        /// </summary>
        HEAVY = 900,

        /// <summary>
        /// Predefined font weight : Extra-black (950).
        /// </summary>
        EXTRA_BLACK = 950,

        /// <summary>
        /// Predefined font weight : Ultra-black (950).
        /// </summary>
        ULTRA_BLACK = 950
    };

}
