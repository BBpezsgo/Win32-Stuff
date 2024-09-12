namespace Win32.Forms;

public static class MFT
{
    /// <summary>
    /// Displays the menu item using a bitmap. The low-order word
    /// of the <c>dwTypeData</c> member is the bitmap handle, and the cch
    /// member is ignored.
    /// <see cref="MFT.Bitmap"/> is replaced by <see cref="MenuItemInfoMasks.Bitmap"/> and <c>hbmpItem</c>.
    /// </summary>
    public const int Bitmap = 0x00000004;
    /// <summary>
    /// Places the menu item on a new line(for a menu bar) or in a
    /// new column(for a drop-down menu, submenu, or shortcut menu).
    /// For a drop-down menu, submenu, or shortcut menu, a vertical
    /// line separates the new column from the old.
    /// </summary>
    public const int MenuBarBreak = 0x00000020;
    /// <summary>
    /// Places the menu item on a new line(for a menu bar) or in a new
    /// column(for a drop-down menu, submenu, or shortcut menu).
    /// For a drop-down menu, submenu, or shortcut menu, the columns
    /// are not separated by a vertical line.
    /// </summary>
    public const int MenuBreak = 0x00000040;
    /// <summary>
    /// Assigns responsibility for drawing the menu item to the window that
    /// owns the menu. The window receives a <see cref="WindowMessage.WM_MEASUREITEM"/> message
    /// before the menu is displayed for the first time, and
    /// a <see cref="WindowMessage.WM_DRAWITEM"/> message whenever the appearance of the menu
    /// item must be updated. If this value is specified, the <c>dwTypeData</c>
    /// member contains an application-defined value.
    /// </summary>
    public const int OwnerDraw = 0x00000100;
    /// <summary>
    /// Displays selected menu items using a radio-button mark
    /// instead of a check mark if the <c>hbmpChecked</c> member is <c>NULL</c>.
    /// </summary>
    public const int RadioCheck = 0x00000200;
    /// <summary>
    /// Right-justifies the menu item and any subsequent items.
    /// This value is valid only if the menu item is in a menu bar.
    /// </summary>
    public const int RightJustify = 0x00004000;
    /// <summary>
    /// Specifies that menus cascade right-to-left(the default is left-to-right).
    /// This is used to support right-to-left languages, such as Arabic and Hebrew.
    /// </summary>
    public const int RightOrder = 0x00002000;
    /// <summary>
    /// Specifies that the menu item is a separator.
    /// A menu item separator appears as a horizontal dividing line.
    /// The <c>dwTypeData</c> and <c>cch</c> members are ignored.This value is valid only in a
    /// drop-down menu, submenu, or shortcut menu.
    /// </summary>
    public const int Separator = 0x00000800;
    /// <summary>
    /// Displays the menu item using a text string.
    /// The <c>dwTypeData</c> member is the pointer to a null-terminated string, and the
    /// cch member is the length of the string.
    /// <see cref="MFT.String"/> is replaced by <see cref="MenuItemInfoMasks.String"/>.
    /// </summary>
    public const int String = 0x00000000;
}
