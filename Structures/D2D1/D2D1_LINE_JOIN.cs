namespace Win32.D2D1
{
    /// <summary>
    /// Enum which describes the drawing of the corners on the line.
    /// </summary>
    public enum D2D1_LINE_JOIN : DWORD
    {
        /// <summary>
        /// Miter join.
        /// </summary>
        MITER = 0,

        /// <summary>
        /// Bevel join.
        /// </summary>
        BEVEL = 1,

        /// <summary>
        /// Round join.
        /// </summary>
        ROUND = 2,

        /// <summary>
        /// Miter/Bevel join.
        /// </summary>
        MITER_OR_BEVEL = 3,
    }
}
