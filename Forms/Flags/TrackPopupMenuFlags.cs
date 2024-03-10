namespace Win32.Forms;

[Flags]
[SuppressMessage("Roslynator", "RCS1234")]
public enum TrackPopupMenuFlags : UINT
{
    /// <summary>
    /// Positions the pop-up window so that its left
    /// edge is aligned with the coordinate specified by the anchorPoint->x parameter.
    /// </summary>
    LeftAlign = 0x0000,

    /// <summary>
    /// Positions the pop-up window so that its top edge
    /// is aligned with the coordinate specified by the <c>anchorPoint->y</c> parameter.
    /// </summary>
    TopAlign = 0x0000,

    /// <summary>
    /// If the pop-up window cannot be shown at the specified
    /// location without overlapping the excluded rectangle,
    /// the system tries to accommodate the requested horizontal
    /// alignment before the requested vertical alignment.
    /// </summary>
    /* Horizontal alignment matters more */
    Horizontal = 0x0000,

    LeftButton = 0x0000,

    Recurse = 0x0001,
    RightButton = 0x0002,
    /// <summary>
    /// Centers pop-up window horizontally relative to
    /// the coordinate specified by the anchorPoint->x parameter.
    /// </summary>
    CenterAlign = 0x0004,
    /// <summary>
    /// Positions the pop-up window so that its right
    /// edge is aligned with the coordinate specified by the anchorPoint->x parameter.
    /// </summary>
    RightAlign = 0x0008,
    /// <summary>
    /// Centers the pop-up window vertically relative to the
    /// coordinate specified by the <c>anchorPoint->y</c> parameter.
    /// </summary>
    VCenterAlign = 0x0010,
    /// <summary>
    /// Positions the pop-up window so that its bottom
    /// edge is aligned with the coordinate specified by the <c>anchorPoint->y</c> parameter.
    /// </summary>
    BottomAlign = 0x0020,
    /// <summary>
    /// If the pop-up window cannot be shown at the specified
    /// location without overlapping the excluded rectangle,
    /// the system tries to accommodate the requested vertical
    /// alignment before the requested horizontal alignment.
    /// </summary>
    /* Vert alignment matters more */
    Vertical = 0x0040,

    /// <summary>
    /// Don't send any notification messages
    /// </summary>
    NoNotify = 0x0080,
    ReturnCMD = 0x0100,
    HPosAnimation = 0x0400,
    HNegativeAnimation = 0x0800,
    VPositiveAnimation = 0x1000,
    VNegativeAnimation = 0x2000,

    NoAnimation = 0x4000,
    LayoutRTL = 0x8000,

    /// <summary>
    /// Restricts the pop-up window to within the work area.
    /// If this flag is not set, the pop-up window is restricted to
    /// the work area only if the input point is within the work area.
    /// For more information, see the <c>rcWork</c> and <c>rcMonitor</c>
    /// members of the <see cref="MONITORINFO"/> structure.
    /// </summary>
    WorkArea = 0x10000,
}
