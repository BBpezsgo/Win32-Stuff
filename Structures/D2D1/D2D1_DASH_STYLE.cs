namespace Win32.D2D1
{
    /// <summary>
    /// Describes the sequence of dashes and gaps in a stroke.
    /// </summary>
    public enum D2D1_DASH_STYLE : DWORD
    {
        SOLID = 0,
        DASH = 1,
        DOT = 2,
        DASH_DOT = 3,
        DASH_DOT_DOT = 4,
        CUSTOM = 5,
    }
}
