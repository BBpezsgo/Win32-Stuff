namespace Win32.D2D1;

/// <summary>
/// Enum which describes the drawing of the ends of a line.
/// </summary>
public enum CapStyle : DWORD
{
    /// <summary>
    /// Flat line cap.
    /// </summary>
    Flat = 0,

    /// <summary>
    /// Square line cap.
    /// </summary>
    Square = 1,

    /// <summary>
    /// Round line cap.
    /// </summary>
    Round = 2,

    /// <summary>
    /// Triangle line cap.
    /// </summary>
    Triangle = 3,
}
