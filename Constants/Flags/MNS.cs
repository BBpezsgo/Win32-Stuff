namespace Win32
{
    public static class MNS
    {
        /// <summary>
        /// Menu automatically ends when mouse is outside
        /// the menu for approximately 10 seconds.
        /// </summary>
        public const UINT AUTODISMISS = 0x10000000;
        /// <summary>
        /// The same space is reserved for the check mark and the bitmap.
        /// If the check mark is drawn, the bitmap is not. All checkmarks
        /// and bitmaps are aligned. Used for menus where some
        /// items use checkmarks and some use bitmaps.
        /// </summary>
        public const UINT CHECKORBMP = 0x04000000;
        /// <summary>
        /// Menu items are OLE drop targets or drag sources.
        /// Menu owner receives <see cref="WM.WM_MENUDRAG"/> and
        /// <see cref="WM.WM_MENUGETOBJECT"/> messages.
        /// </summary>
        public const UINT DRAGDROP = 0x20000000;
        /// <summary>
        /// Menu is modeless; that is, there is no menu modal
        /// message loop while the menu is active.
        /// </summary>
        public const UINT MODELESS = 0x40000000;
        /// <summary>
        /// No space is reserved to the left of an item for a check mark.
        /// The item can still be selected, but the check mark will not appear next to the item.
        /// </summary>
        public const UINT NOCHECK = 0x80000000;
        /// <summary>
        /// Menu owner receives a <see cref="WM.WM_MENUCOMMAND"/> message instead
        /// of a <see cref="WM.WM_COMMAND"/> message when the user makes a selection.
        /// <c>NOTIFYBYPOS</c> is a menu header style and has no
        /// effect when applied to individual sub menus.
        /// </summary>
        public const UINT NOTIFYBYPOS = 0x08000000;
    }
}
