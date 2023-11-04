namespace Win32.LowLevel
{
    public static class HBMMENU
    {
        /// <summary>
        /// A bitmap that is drawn by the window that owns the menu.
        /// The application must process the <see cref="WM.MEASUREITEM"/> and <see cref="WM.DRAWITEM"/> messages.
        /// </summary>
        public static readonly HBITMAP CALLBACK = (HBITMAP)(-1);
        /// <summary>
        /// Close button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_CLOSE = (HBITMAP)5;
        /// <summary>
        /// Disabled close button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_CLOSE_D = (HBITMAP)6;
        /// <summary>
        /// Minimize button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_MINIMIZE = (HBITMAP)3;
        /// <summary>
        /// Disabled minimize button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_MINIMIZE_D = (HBITMAP)7;
        /// <summary>
        /// Restore button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_RESTORE = (HBITMAP)2;
        /// <summary>
        /// Close button for the submenu.
        /// </summary>
        public static readonly HBITMAP POPUP_CLOSE = (HBITMAP)8;
        /// <summary>
        /// Maximize button for the submenu.
        /// </summary>
        public static readonly HBITMAP POPUP_MAXIMIZE = (HBITMAP)10;
        /// <summary>
        /// Minimize button for the submenu.
        /// </summary>
        public static readonly HBITMAP POPUP_MINIMIZE = (HBITMAP)11;
        /// <summary>
        /// Restore button for the submenu.
        /// </summary>
        public static readonly HBITMAP POPUP_RESTORE = (HBITMAP)9;
        /// <summary>
        /// Windows icon or the icon of the window specified in dwItemData.
        /// </summary>
        public static readonly HBITMAP SYSTEM = (HBITMAP)1;
    }
}
