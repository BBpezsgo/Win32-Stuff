namespace Win32.D2D1
{
    /// <summary>
    /// Indicates whether the given segment should be stroked, or, if the join between
    /// this segment and the previous one should be smooth.
    /// </summary>
    public enum D2D1_PATH_SEGMENT : DWORD
    {
        NONE = 0x00000000,
        FORCE_UNSTROKED = 0x00000001,
        FORCE_ROUND_LINE_JOIN = 0x00000002,
    }
}
