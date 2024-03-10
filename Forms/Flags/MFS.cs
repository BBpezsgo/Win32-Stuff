namespace Win32.Forms;

public static class MFS
{
    /// <summary>
    /// Checks the menu item.
    /// For more information about selected menu items, see the <c>hbmpChecked</c> member.
    /// </summary>
    public const int CHECKED = 0x00000008;
    /// <summary>
    /// Specifies that the menu item is the default.
    /// A menu can contain only one default menu item, which is displayed in bold.
    /// </summary>
    public const int DEFAULT = 0x00001000;
    /// <summary>
    /// Disables the menu item and grays it so that it cannot be selected.
    /// This is equivalent to <see cref="MFS.GRAYED"/>.
    /// </summary>
    public const int DISABLED = 0x00000003;
    /// <summary>
    /// Enables the menu item so that it can be selected.This is the default state.
    /// </summary>
    public const int ENABLED = 0x00000000;
    /// <summary>
    /// Disables the menu item and grays it so that it cannot be selected.
    /// This is equivalent to <see cref="MFS.DISABLED"/>.
    /// </summary>
    public const int GRAYED = 0x00000003;
    /// <summary>
    /// Highlights the menu item.
    /// </summary>
    public const int HILITE = 0x00000080;
    /// <summary>
    /// Unchecks the menu item.
    /// For more information about clear menu items, see the <c>hbmpChecked</c> member.
    /// </summary>
    public const int UNCHECKED = 0x00000000;
    /// <summary>
    /// Removes the highlight from the menu item.
    /// This is the default state.
    /// </summary>
    public const int UNHILITE = 0x00000000;
}
