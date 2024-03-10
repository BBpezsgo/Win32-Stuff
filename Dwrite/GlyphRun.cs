namespace Win32.DWrite;

/// <summary>
/// The <see cref="GlyphRun"/> structure contains the information needed by renderers
/// to draw glyph runs. All coordinates are in device independent pixels (DIPs).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct GlyphRun
{
    /// <summary>
    /// The physical font face to draw with.
    /// </summary>
    [NotNull, MarshalAs(UnmanagedType.IUnknown)] public object? FontFace;

    /// <summary>
    /// Logical size of the font in DIPs, not points (equals 1/96 inch).
    /// </summary>
    public FLOAT FontEmSize;

    /// <summary>
    /// The number of glyphs.
    /// </summary>
    public UINT32 GlyphCount;

    /// <summary>
    /// The indices to render.
    /// </summary>
    public unsafe UINT16* GlyphIndices;

    /// <summary>
    /// Glyph advance widths.
    /// </summary>
    public unsafe FLOAT* GlyphAdvances;

    /// <summary>
    /// Glyph offsets.
    /// </summary>
    public unsafe GlyphOffset* GlyphOffsets;

    /// <summary>
    /// If true, specifies that glyphs are rotated 90 degrees to the left and
    /// vertical metrics are used. Vertical writing is achieved by specifying
    /// isSideways = true and rotating the entire run 90 degrees to the right
    /// via a rotate transform.
    /// </summary>
    public BOOL IsSideways;

    /// <summary>
    /// The implicit resolved bidi level of the run. Odd levels indicate
    /// right-to-left languages like Hebrew and Arabic, while even levels
    /// indicate left-to-right languages like English and Japanese (when
    /// written horizontally). For right-to-left languages, the text origin
    /// is on the right, and text should be drawn to the left.
    /// </summary>
    public UINT32 BidiLevel;
}
