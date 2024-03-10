namespace Win32.DWrite;

/// <summary>
/// Alignment of paragraph text along the reading direction axis relative to
/// the leading and trailing edge of the layout box.
/// </summary>
public enum TextAlignment
{
    /// <summary>
    /// The leading edge of the paragraph text is aligned to the layout box's leading edge.
    /// </summary>
    LEADING,

    /// <summary>
    /// The trailing edge of the paragraph text is aligned to the layout box's trailing edge.
    /// </summary>
    TRAILING,

    /// <summary>
    /// The center of the paragraph text is aligned to the center of the layout box.
    /// </summary>
    CENTER,

    /// <summary>
    /// Align text to the leading side, and also justify text to fill the lines.
    /// </summary>
    JUSTIFIED
}
