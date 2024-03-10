namespace Win32.DWrite;

/// <summary>
/// Direction for how reading progresses.
/// </summary>
public enum ReadingDirection
{
    /// <summary>
    /// Reading progresses from left to right.
    /// </summary>
    LeftToRight = 0,

    /// <summary>
    /// Reading progresses from right to left.
    /// </summary>
    RightToLeft = 1,

    /// <summary>
    /// Reading progresses from top to bottom.
    /// </summary>
    TopToBottom = 2,

    /// <summary>
    /// Reading progresses from bottom to top.
    /// </summary>
    BottomToTop = 3,
}
