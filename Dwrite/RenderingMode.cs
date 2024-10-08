﻿namespace Win32.DWrite;

/// <summary>
/// Represents a method of rendering glyphs.
/// </summary>
public enum RenderingMode
{
    /// <summary>
    /// Specifies that the rendering mode is determined automatically based on the font and size.
    /// </summary>
    Default,

    /// <summary>
    /// Specifies that no antialiasing is performed. Each pixel is either set to the foreground
    /// color of the text or retains the color of the background.
    /// </summary>
    Aliased,

    /// <summary>
    /// Specifies that antialiasing is performed in the horizontal direction and the appearance
    /// of glyphs is layout-compatible with GDI using <c>CLEARTYPE_QUALITY</c>. Use <c>DWRITE_MEASURING_MODE_GDI_CLASSIC</c>
    /// to get glyph advances. The antialiasing may be either ClearType or grayscale depending on
    /// the text antialiasing mode.
    /// </summary>
    GDIClassic,

    /// <summary>
    /// Specifies that antialiasing is performed in the horizontal direction and the appearance
    /// of glyphs is layout-compatible with GDI using <c>CLEARTYPE_NATURAL_QUALITY</c>. Glyph advances
    /// are close to the font design advances, but are still rounded to whole pixels. Use
    /// <c>DWRITE_MEASURING_MODE_GDI_NATURAL</c> to get glyph advances. The antialiasing may be either
    /// ClearType or grayscale depending on the text antialiasing mode.
    /// </summary>
    GDINatural,

    /// <summary>
    /// Specifies that antialiasing is performed in the horizontal direction. This rendering
    /// mode allows glyphs to be positioned with subpixel precision and is therefore suitable
    /// for natural (i.e., resolution-independent) layout. The antialiasing may be either
    /// ClearType or grayscale depending on the text antialiasing mode.
    /// </summary>
    Natural,

    /// <summary>
    /// Similar to natural mode except that antialiasing is performed in both the horizontal
    /// and vertical directions. This is typically used at larger sizes to make curves and
    /// diagonal lines look smoother. The antialiasing may be either ClearType or grayscale
    /// depending on the text antialiasing mode.
    /// </summary>
    NaturalSymmetric,

    /// <summary>
    /// Specifies that rendering should bypass the rasterizer and use the outlines directly.
    /// This is typically used at very large sizes.
    /// </summary>
    Outline,
};
