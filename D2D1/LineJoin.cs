namespace Win32.D2D1;

/// <summary>
/// Enum which describes the drawing of the corners on the line.
/// </summary>
public enum LineJoin : DWORD
{
    /// <summary>
    /// Miter join.
    /// </summary>
    Miter = 0,

    /// <summary>
    /// Bevel join.
    /// </summary>
    Bevel = 1,

    /// <summary>
    /// Round join.
    /// </summary>
    Round = 2,

    /// <summary>
    /// Miter/Bevel join.
    /// </summary>
    MiterOrBevel = 3,
}
