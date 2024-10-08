﻿namespace Win32.DWrite;

/// <summary>
/// The measuring method used for text layout.
/// </summary>
public enum MeasuringMode
{
    /// <summary>
    /// Text is measured using glyph ideal metrics whose values are independent to the current display resolution.
    /// </summary>
    Natural,

    /// <summary>
    /// Text is measured using glyph display compatible metrics whose values tuned for the current display resolution.
    /// </summary>
    GDIClassic,

    /// <summary>
    /// Text is measured using the same glyph display metrics as text measured by GDI using a font
    /// created with <c>CLEARTYPE_NATURAL_QUALITY</c>.
    /// </summary>
    GDINatural,
}
