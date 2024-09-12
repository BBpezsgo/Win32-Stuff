namespace Win32.D2D1;

/// <summary>
/// Specifies how the intersecting areas of geometries or figures are combined to
/// form the area of the composite geometry.
/// </summary>
public enum FillMode : DWORD
{
    Alternate = 0,
    Winding = 1,
}
