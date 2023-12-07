namespace Win32.D2D1
{
    /// <summary>
    /// Enum which describes the manner in which we render edges of non-text primitives.
    /// </summary>
    public enum D2D1_ANTIALIAS_MODE : DWORD
    {

        /// <summary>
        /// The edges of each primitive are antialiased sequentially.
        /// </summary>
        PER_PRIMITIVE = 0,

        /// <summary>
        /// Each pixel is rendered if its pixel center is contained by the geometry.
        /// </summary>
        ALIASED = 1,
    }
}
