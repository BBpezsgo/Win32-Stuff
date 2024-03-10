namespace Win32.DWrite;

/// <summary>
/// The font weight enumeration describes common values for degree of blackness or thickness of strokes of characters in a font.
/// Font weight values less than 1 or greater than 999 are considered to be invalid, and they are rejected by font API functions.
/// </summary>
public enum FontWeight
{
    /// <summary>
    /// Predefined font weight : Thin (100).
    /// </summary>
    Thin = 100,

    /// <summary>
    /// Predefined font weight : Extra-light (200).
    /// </summary>
    ExtraLight = 200,

    /// <summary>
    /// Predefined font weight : Light (300).
    /// </summary>
    Light = 300,

    /// <summary>
    /// Predefined font weight : Semi-light (350).
    /// </summary>
    SemiLight = 350,

    /// <summary>
    /// Predefined font weight : Normal (400).
    /// </summary>
    Normal = 400,

    /// <summary>
    /// Predefined font weight : Medium (500).
    /// </summary>
    Medium = 500,

    /// <summary>
    /// Predefined font weight : Semi-bold (600).
    /// </summary>
    SemiBold = 600,

    /// <summary>
    /// Predefined font weight : Bold (700).
    /// </summary>
    Bold = 700,

    /// <summary>
    /// Predefined font weight : Extra-bold (800).
    /// </summary>
    ExtraBold = 800,

    /// <summary>
    /// Predefined font weight : Black (900).
    /// </summary>
    Black = 900,

    /// <summary>
    /// Predefined font weight : Extra-black (950).
    /// </summary>
    ExtraBlack = 950,
}
