namespace Win32.DWrite;

/// <summary>
/// The type of a font represented by a single font file.
/// Font formats that consist of multiple files, e.g. Type 1 .PFM and .PFB, have
/// separate enum values for each of the file type.
/// </summary>
[SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute")]
public enum FontFileType
{
    /// <summary>
    /// Font type is not recognized by the DirectWrite font system.
    /// </summary>
    Unknown,

    /// <summary>
    /// OpenType font with CFF outlines.
    /// </summary>
    CFF,

    /// <summary>
    /// OpenType font with TrueType outlines.
    /// </summary>
    TrueType,

    /// <summary>
    /// OpenType font that contains a TrueType collection.
    /// </summary>
    OpenTypeCollection,

    /// <summary>
    /// Type 1 PFM font.
    /// </summary>
    Type1PFM,

    /// <summary>
    /// Type 1 PFB font.
    /// </summary>
    Type1PFB,

    /// <summary>
    /// Vector .FON font.
    /// </summary>
    Vector,

    /// <summary>
    /// Bitmap .FON font.
    /// </summary>
    Bitmap,

    [Obsolete("The following name is obsolete, but kept as an alias to avoid breaking existing code.")]
    TrueTypeCollection = OpenTypeCollection,
};
