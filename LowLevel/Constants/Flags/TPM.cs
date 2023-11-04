namespace Win32.LowLevel
{
    public static partial class TPM
    {
        /// <summary>
        /// Positions the pop-up window so that its left
        /// edge is aligned with the coordinate specified by the anchorPoint->x parameter.
        /// </summary>
        public const UINT LEFTALIGN = 0x0000;
        /// <summary>
        /// Centers pop-up window horizontally relative to
        /// the coordinate specified by the anchorPoint->x parameter.
        /// </summary>
        public const UINT CENTERALIGN = 0x0004;
        /// <summary>
        /// Positions the pop-up window so that its right
        /// edge is aligned with the coordinate specified by the anchorPoint->x parameter.
        /// </summary>
        public const UINT RIGHTALIGN = 0x0008;

        /// <summary>
        /// Positions the pop-up window so that its top edge
        /// is aligned with the coordinate specified by the <c>anchorPoint->y</c> parameter.
        /// </summary>
        public const UINT TOPALIGN = 0x0000;
        /// <summary>
        /// Centers the pop-up window vertically relative to the
        /// coordinate specified by the <c>anchorPoint->y</c> parameter.
        /// </summary>
        public const UINT VCENTERALIGN = 0x0010;
        /// <summary>
        /// Positions the pop-up window so that its bottom
        /// edge is aligned with the coordinate specified by the <c>anchorPoint->y</c> parameter.
        /// </summary>
        public const UINT BOTTOMALIGN = 0x0020;

        /// <summary>
        /// If the pop-up window cannot be shown at the specified
        /// location without overlapping the excluded rectangle,
        /// the system tries to accommodate the requested horizontal
        /// alignment before the requested vertical alignment.
        /// </summary>
        /* Horizontal alignment matters more */
        public const UINT HORIZONTAL = 0x0000;
        /// <summary>
        /// If the pop-up window cannot be shown at the specified
        /// location without overlapping the excluded rectangle,
        /// the system tries to accommodate the requested vertical
        /// alignment before the requested horizontal alignment.
        /// </summary>
        /* Vert alignment matters more */
        public const UINT VERTICAL = 0x0040;

        /// <summary>
        /// Restricts the pop-up window to within the work area.
        /// If this flag is not set, the pop-up window is restricted to
        /// the work area only if the input point is within the work area.
        /// For more information, see the <c>rcWork</c> and <c>rcMonitor</c>
        /// members of the <see cref="MONITORINFO"/> structure.
        /// </summary>
        public const UINT WORKAREA = 0x10000;
    }
}
