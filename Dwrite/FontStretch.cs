﻿namespace Win32.DWrite;

/// <summary>
/// The font stretch enumeration describes relative change from the normal aspect ratio
/// as specified by a font designer for the glyphs in a font.
/// Values less than 1 or greater than 9 are considered to be invalid, and they are rejected by font API functions.
/// </summary>
public enum FontStretch
{
    /// <summary>
    /// Predefined font stretch : Not known (0).
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Predefined font stretch : Ultra-condensed (1).
    /// </summary>
    UltraCondensed = 1,

    /// <summary>
    /// Predefined font stretch : Extra-condensed (2).
    /// </summary>
    ExtraCondensed = 2,

    /// <summary>
    /// Predefined font stretch : Condensed (3).
    /// </summary>
    Condensed = 3,

    /// <summary>
    /// Predefined font stretch : Semi-condensed (4).
    /// </summary>
    SemiCondensed = 4,

    /// <summary>
    /// Predefined font stretch : Normal (5).
    /// </summary>
    Normal = 5,

    /// <summary>
    /// Predefined font stretch : Semi-expanded (6).
    /// </summary>
    SemiExpanded = 6,

    /// <summary>
    /// Predefined font stretch : Expanded (7).
    /// </summary>
    Expanded = 7,

    /// <summary>
    /// Predefined font stretch : Extra-expanded (8).
    /// </summary>
    ExtraExpanded = 8,

    /// <summary>
    /// Predefined font stretch : Ultra-expanded (9).
    /// </summary>
    UltraExpanded = 9
}
