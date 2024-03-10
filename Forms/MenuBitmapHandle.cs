namespace Win32.Forms;

public static class MenuBitmapHandle
{
    /// <summary>
    /// A bitmap that is drawn by the window that owns the menu.
    /// The application must process the <see cref="WindowMessage.WM_MEASUREITEM"/>
    /// and <see cref="WindowMessage.WM_DRAWITEM"/> messages.
    /// </summary>
    public static readonly HBITMAP Callback = -1;
    /// <summary>
    /// Close button for the menu bar.
    /// </summary>
    public static readonly HBITMAP MBarClose = 5;
    /// <summary>
    /// Disabled close button for the menu bar.
    /// </summary>
    public static readonly HBITMAP MBARCloseD = 6;
    /// <summary>
    /// Minimize button for the menu bar.
    /// </summary>
    public static readonly HBITMAP MBARMinimize = 3;
    /// <summary>
    /// Disabled minimize button for the menu bar.
    /// </summary>
    public static readonly HBITMAP MBarMinimizeD = 7;
    /// <summary>
    /// Restore button for the menu bar.
    /// </summary>
    public static readonly HBITMAP MBarRestore = 2;
    /// <summary>
    /// Close button for the submenu.
    /// </summary>
    public static readonly HBITMAP PopupClose = 8;
    /// <summary>
    /// Maximize button for the submenu.
    /// </summary>
    public static readonly HBITMAP PopupMaximize = 10;
    /// <summary>
    /// Minimize button for the submenu.
    /// </summary>
    public static readonly HBITMAP PopupMinimize = 11;
    /// <summary>
    /// Restore button for the submenu.
    /// </summary>
    public static readonly HBITMAP PopupRestore = 9;
    /// <summary>
    /// Windows icon or the icon of the window specified in <c>dwItemData</c>.
    /// </summary>
    public static readonly HBITMAP System = 1;
}
