namespace Win32.DWrite
{
    /// <summary>
    /// Direction for how reading progresses.
    /// </summary>
    public enum DWRITE_READING_DIRECTION
    {
        /// <summary>
        /// Reading progresses from left to right.
        /// </summary>
        LEFT_TO_RIGHT = 0,

        /// <summary>
        /// Reading progresses from right to left.
        /// </summary>
        RIGHT_TO_LEFT = 1,

        /// <summary>
        /// Reading progresses from top to bottom.
        /// </summary>
        TOP_TO_BOTTOM = 2,

        /// <summary>
        /// Reading progresses from bottom to top.
        /// </summary>
        BOTTOM_TO_TOP = 3,
    }
}
