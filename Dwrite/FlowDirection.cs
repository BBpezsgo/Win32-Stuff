﻿namespace Win32.DWrite;

/// <summary>
/// Direction for how lines of text are placed relative to one another.
/// </summary>
public enum FlowDirection
{
    /// <summary>
    /// Text lines are placed from top to bottom.
    /// </summary>
    TopToBottom = 0,

    /// <summary>
    /// Text lines are placed from bottom to top.
    /// </summary>
    BottomToTop = 1,

    /// <summary>
    /// Text lines are placed from left to right.
    /// </summary>
    LeftToRight = 2,

    /// <summary>
    /// Text lines are placed from right to left.
    /// </summary>
    RightToLeft = 3,
}
