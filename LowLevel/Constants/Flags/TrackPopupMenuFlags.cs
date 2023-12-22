namespace Win32.LowLevel
{
#pragma warning disable CS1574
    public enum TrackPopupMenuFlags : UINT
    {
        /// <summary>
        /// Positions the pop-up window so that its left
        /// edge is aligned with the coordinate specified by the anchorPoint->x parameter.
        /// </summary>
        LEFTALIGN = 0x0000,
        /// <summary>
        /// Centers pop-up window horizontally relative to
        /// the coordinate specified by the anchorPoint->x parameter.
        /// </summary>
        CENTERALIGN = 0x0004,
        /// <summary>
        /// Positions the pop-up window so that its right
        /// edge is aligned with the coordinate specified by the anchorPoint->x parameter.
        /// </summary>
        RIGHTALIGN = 0x0008,

        /// <summary>
        /// Positions the pop-up window so that its top edge
        /// is aligned with the coordinate specified by the <c>anchorPoint->y</c> parameter.
        /// </summary>
        TOPALIGN = 0x0000,
        /// <summary>
        /// Centers the pop-up window vertically relative to the
        /// coordinate specified by the <c>anchorPoint->y</c> parameter.
        /// </summary>
        VCENTERALIGN = 0x0010,
        /// <summary>
        /// Positions the pop-up window so that its bottom
        /// edge is aligned with the coordinate specified by the <c>anchorPoint->y</c> parameter.
        /// </summary>
        BOTTOMALIGN = 0x0020,

        /// <summary>
        /// If the pop-up window cannot be shown at the specified
        /// location without overlapping the excluded rectangle,
        /// the system tries to accommodate the requested horizontal
        /// alignment before the requested vertical alignment.
        /// </summary>
        /* Horizontal alignment matters more */
        HORIZONTAL = 0x0000,
        /// <summary>
        /// If the pop-up window cannot be shown at the specified
        /// location without overlapping the excluded rectangle,
        /// the system tries to accommodate the requested vertical
        /// alignment before the requested horizontal alignment.
        /// </summary>
        /* Vert alignment matters more */
        VERTICAL = 0x0040,

        /// <summary>
        /// Restricts the pop-up window to within the work area.
        /// If this flag is not set, the pop-up window is restricted to
        /// the work area only if the input point is within the work area.
        /// For more information, see the <c>rcWork</c> and <c>rcMonitor</c>
        /// members of the <see cref="MONITORINFO"/> structure.
        /// </summary>
        WORKAREA = 0x10000,

        LEFTBUTTON = 0x0000,
        RIGHTBUTTON = 0x0002,

        // #if(WINVER >= 0x0400)

        /* Don't send any notification messages */
        NONOTIFY = 0x0080,
        RETURNCMD = 0x0100,
        // #endif /* WINVER >= 0x0400 */

        // #if(WINVER >= 0x0500)
        RECURSE = 0x0001,
        HORPOSANIMATION = 0x0400,
        HORNEGANIMATION = 0x0800,
        VERPOSANIMATION = 0x1000,
        VERNEGANIMATION = 0x2000,

        // #if(_WIN32_WINNT >= 0x0500)
        NOANIMATION = 0x4000,
        // #endif /* _WIN32_WINNT >= 0x0500 */

        // #if(_WIN32_WINNT >= 0x0501)
        LAYOUTRTL = 0x8000,
        // #endif /* _WIN32_WINNT >= 0x0501 */

        // #endif /* WINVER >= 0x0500 */
    }
}
