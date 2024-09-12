namespace Win32.Gdi32;

public enum StretchMode
{
    /// <summary>
    /// Performs a Boolean AND operation using the color values
    /// for the eliminated and existing pixels. If the bitmap is a
    /// monochrome bitmap, this mode preserves black pixels
    /// at the expense of white pixels.
    /// </summary>
    BlackOnWhite = 1,

    /// <summary>
    /// Performs a Boolean OR operation using the color values
    /// for the eliminated and existing pixels. If the bitmap
    /// is a monochrome bitmap, this mode preserves white pixels
    /// at the expense of black pixels.
    /// </summary>
    WhiteOnBlack = 2,

    /// <summary>
    /// Deletes the pixels. This mode deletes all eliminated
    /// lines of pixels without trying to preserve their information.
    /// </summary>
    ColorOnColor = 3,

    /// <summary>
    /// Maps pixels from the source rectangle into blocks of
    /// pixels in the destination rectangle. The average color over
    /// the destination block of pixels approximates the color of the
    /// source pixels. After setting the <see cref="HALFTONE"/> stretching mode,
    /// an application must call the <see cref="Gdi32.SetBrushOrgEx"/> function to set
    /// the brush origin. If it fails to do so, brush misalignment occurs.
    /// </summary>
    Halftone = 4,
}
