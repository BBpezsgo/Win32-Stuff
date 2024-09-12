namespace Win32.DWrite;

/// <summary>
/// Text granularity used to trim text overflowing the layout box.
/// </summary>
public enum TrimmingGranularity
{
    /// <summary>
    /// No trimming occurs. Text flows beyond the layout width.
    /// </summary>
    None,

    /// <summary>
    /// Trimming occurs at character cluster boundary.
    /// </summary>
    Character,

    /// <summary>
    /// Trimming occurs at word boundary.
    /// </summary>
    Word,
}
