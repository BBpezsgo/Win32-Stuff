namespace Win32.DWrite;

/// <summary>
/// The <see cref="Matrix"/> structure specifies the graphics transform to be applied
/// to rendered glyphs.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Matrix
{
    /// <summary>
    /// Horizontal scaling / cosine of rotation
    /// </summary>
    public FLOAT m11;

    /// <summary>
    /// Vertical shear / sine of rotation
    /// </summary>
    public FLOAT m12;

    /// <summary>
    /// Horizontal shear / negative sine of rotation
    /// </summary>
    public FLOAT m21;

    /// <summary>
    /// Vertical scaling / cosine of rotation
    /// </summary>
    public FLOAT m22;

    /// <summary>
    /// Horizontal shift (always orthogonal regardless of rotation)
    /// </summary>
    public FLOAT dx;

    /// <summary>
    /// Vertical shift (always orthogonal regardless of rotation)
    /// </summary>
    public FLOAT dy;
}
