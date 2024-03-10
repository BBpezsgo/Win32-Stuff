namespace Win32.DWrite;

/// <summary>
/// The file format of a complete font face.
/// Font formats that consist of multiple files, e.g. Type 1 .PFM and .PFB, have
/// a single enum entry.
/// </summary>
[SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute")]
public enum FontFaceType
{
    /// <summary>
    /// OpenType font face with CFF outlines.
    /// </summary>
    CFF,

    /// <summary>
    /// OpenType font face with TrueType outlines.
    /// </summary>
    TrueType,

    /// <summary>
    /// OpenType font face that is a part of a TrueType or CFF collection.
    /// </summary>
    OpenTypeCollection,

    /// <summary>
    /// A Type 1 font face.
    /// </summary>
    Type1,

    /// <summary>
    /// A vector .FON format font face.
    /// </summary>
    Vector,

    /// <summary>
    /// A bitmap .FON format font face.
    /// </summary>
    Bitmap,

    /// <summary>
    /// Font face type is not recognized by the DirectWrite font system.
    /// </summary>
    Unknown,

    /// <summary>
    /// The font data includes only the CFF table from an OpenType CFF font.
    /// This font face type can be used only for embedded fonts (i.e., custom
    /// font file loaders) and the resulting font face object supports only the
    /// minimum functionality necessary to render glyphs.
    /// </summary>
    RawCFF,

    [Obsolete("The following name is obsolete, but kept as an alias to avoid breaking existing code.")]
    TrueTypeCollection = OpenTypeCollection,
};
