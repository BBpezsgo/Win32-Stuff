namespace Win32.D2D1;

/// <summary>
/// Describes the sequence of dashes and gaps in a stroke.
/// </summary>
public enum DashStyle : DWORD
{
    Solid = 0,
    Dash = 1,
    Dot = 2,
    DashDot = 3,
    DashDotDot = 4,
    Custom = 5,
}
