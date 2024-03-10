namespace Win32.D2D1;

/// <summary>
/// Indicates whether the given segment should be stroked, or, if the join between
/// this segment and the previous one should be smooth.
/// </summary>
public enum PathSegment : DWORD
{
    None = 0x00000000,
    ForceUnstroked = 0x00000001,
    ForceRoundLineJoin = 0x00000002,
}
