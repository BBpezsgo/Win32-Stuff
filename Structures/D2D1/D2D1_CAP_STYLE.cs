namespace Win32.D2D1
{
    /// <summary>
    /// Enum which describes the drawing of the ends of a line.
    /// </summary>
    public enum D2D1_CAP_STYLE : DWORD
    {
        /// <summary>
        /// Flat line cap.
        /// </summary>
        FLAT = 0,

        /// <summary>
        /// Square line cap.
        /// </summary>
        SQUARE = 1,

        /// <summary>
        /// Round line cap.
        /// </summary>
        ROUND = 2,

        /// <summary>
        /// Triangle line cap.
        /// </summary>
        TRIANGLE = 3,
    }
}
