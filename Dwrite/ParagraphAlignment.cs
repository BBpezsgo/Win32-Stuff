﻿namespace Win32.DWrite;

/// <summary>
/// Alignment of paragraph text along the flow direction axis relative to the
/// flow's beginning and ending edge of the layout box.
/// </summary>
public enum ParagraphAlignment
{
    /// <summary>
    /// The first line of paragraph is aligned to the flow's beginning edge of the layout box.
    /// </summary>
    Near,

    /// <summary>
    /// The last line of paragraph is aligned to the flow's ending edge of the layout box.
    /// </summary>
    Far,

    /// <summary>
    /// The center of the paragraph is aligned to the center of the flow of the layout box.
    /// </summary>
    Center,
}
