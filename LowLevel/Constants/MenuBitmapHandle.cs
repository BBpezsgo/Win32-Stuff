namespace Win32.LowLevel
{
    public static class MenuBitmapHandle
    {
        /// <summary>
        /// A bitmap that is drawn by the window that owns the menu.
        /// The application must process the <see cref="WindowMessage.WM_MEASUREITEM"/> and <see cref="WindowMessage.WM_DRAWITEM"/> messages.
        /// </summary>
        public static readonly HBITMAP CALLBACK = -1;
        /// <summary>
        /// Close button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_CLOSE = 5;
        /// <summary>
        /// Disabled close button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_CLOSE_D = 6;
        /// <summary>
        /// Minimize button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_MINIMIZE = 3;
        /// <summary>
        /// Disabled minimize button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_MINIMIZE_D = 7;
        /// <summary>
        /// Restore button for the menu bar.
        /// </summary>
        public static readonly HBITMAP MBAR_RESTORE = 2;
        /// <summary>
        /// Close button for the submenu.
        /// </summary>
        public static readonly HBITMAP POPUP_CLOSE = 8;
        /// <summary>
        /// Maximize button for the submenu.
        /// </summary>
        public static readonly HBITMAP POPUP_MAXIMIZE = 10;
        /// <summary>
        /// Minimize button for the submenu.
        /// </summary>
        public static readonly HBITMAP POPUP_MINIMIZE = 11;
        /// <summary>
        /// Restore button for the submenu.
        /// </summary>
        public static readonly HBITMAP POPUP_RESTORE = 9;
        /// <summary>
        /// Windows icon or the icon of the window specified in dwItemData.
        /// </summary>
        public static readonly HBITMAP SYSTEM = 1;
    }
}
