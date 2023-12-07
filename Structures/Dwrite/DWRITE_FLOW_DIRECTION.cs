namespace Win32.DWrite
{
    /// <summary>
    /// Direction for how lines of text are placed relative to one another.
    /// </summary>
    public enum DWRITE_FLOW_DIRECTION
    {
        /// <summary>
        /// Text lines are placed from top to bottom.
        /// </summary>
        TOP_TO_BOTTOM = 0,

        /// <summary>
        /// Text lines are placed from bottom to top.
        /// </summary>
        BOTTOM_TO_TOP = 1,

        /// <summary>
        /// Text lines are placed from left to right.
        /// </summary>
        LEFT_TO_RIGHT = 2,

        /// <summary>
        /// Text lines are placed from right to left.
        /// </summary>
        RIGHT_TO_LEFT = 3,
    }
}
