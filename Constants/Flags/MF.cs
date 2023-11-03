using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32
{
    public static class MF
    {
        /// <summary>
        /// Uses a bitmap as the menu item. The <c>lpNewItem</c> parameter contains a handle to the bitmap.
        /// </summary>
        public const int BITMAP = 0x00000004;
        /// <summary>
        /// Places a check mark next to the menu item.If the application provides
        /// check-mark bitmaps(see <see cref="User32.SetMenuItemBitmaps"/>, this
        /// flag displays the check-mark bitmap next to the menu item.
        /// </summary>
        public const int CHECKED = 0x00000008;
        /// <summary>
        /// Disables the menu item so that it cannot be selected, but the flag does not gray it.
        /// </summary>
        public const int DISABLED = 0x00000002;
        /// <summary>
        /// Enables the menu item so that it can be selected, and restores it from its grayed state.
        /// </summary>
        public const int ENABLED = 0x00000000;
        /// <summary>
        /// Disables the menu item and grays it so that it cannot be selected.
        /// </summary>
        public const int GRAYED = 0x00000001;
        /// <summary>
        /// Functions the same as the MF_MENUBREAK flag for a menu bar.
        /// For a drop-down menu, submenu, or shortcut menu,
        /// the new column is separated from the old column by a vertical line.
        /// </summary>
        public const int MENUBARBREAK = 0x00000020;
        /// <summary>
        /// Places the item on a new line(for a menu bar) or in a
        /// new column(for a drop-down menu, submenu, or shortcut menu) without separating columns.
        /// </summary>
        public const int MENUBREAK = 0x00000040;
        /// <summary>
        /// Specifies that the item is an owner-drawn item.
        /// Before the menu is displayed for the first time, the window that owns the menu
        /// receives a <see cref="WM.WM_MEASUREITEM"/> message to retrieve the width and height of the menu item.
        /// The <see cref="WM.WM_DRAWITEM"/> message is then sent to the window procedure of the owner
        /// window whenever the appearance of the menu item must be updated.
        /// </summary>
        public const int OWNERDRAW = 0x00000100;
        /// <summary>
        /// Specifies that the menu item opens a drop-down menu or submenu.The <c>uIDNewItem</c> parameter
        /// specifies a handle to the drop-down menu or submenu.
        /// This flag is used to add a menu name to a menu bar,
        /// or a menu item that opens a submenu to a drop-down menu, submenu, or shortcut menu.
        /// </summary>
        public const int POPUP = 0x00000010;
        /// <summary>
        /// Draws a horizontal dividing line.This flag is used only in a drop-down menu, submenu, or shortcut menu.
        /// The line cannot be grayed, disabled, or highlighted. The <c>lpNewItem</c> and <c>uIDNewItem</c> parameters are ignored.
        /// </summary>
        public const int SEPARATOR = 0x00000800;
        /// <summary>
        /// Specifies that the menu item is a text string; the <c>lpNewItem</c> parameter is a pointer to the string.
        /// </summary>
        public const int STRING = 0x00000000;
        /// <summary>
        /// Does not place a check mark next to the item(default).
        /// If the application supplies check-mark bitmaps(see <see cref="User32.SetMenuItemBitmaps"/>),
        /// this flag displays the clear bitmap next to the menu item.
        /// </summary>
        public const int UNCHECKED = 0x00000000;

        public const int INSERT = 0x00000000;
        public const int CHANGE = 0x00000080;
        public const int APPEND = 0x00000100;
        public const int DELETE = 0x00000200;
        public const int REMOVE = 0x00001000;

        public const uint BYCOMMAND = 0x00000000;
        public const uint BYPOSITION = 0x00000400;

        /// <summary>
        /// Removes highlighting from the menu item.
        /// </summary>
        public const uint UNHILITE = 0x00000000;
        /// <summary>
        /// Highlights the menu item.
        /// If this flag is not specified, the highlighting is removed from the item.
        /// </summary>
        public const uint HILITE = 0x00000080;

        // #if (WINVER >= 0x0400)
        public const int DEFAULT = 0x00001000;
        // #endif /* WINVER >= 0x0400 */
        public const int SYSMENU = 0x00002000;
        public const int HELP = 0x00004000;
        // #if (WINVER >= 0x0400)
        public const int RIGHTJUSTIFY = 0x00004000;
        // #endif /* WINVER >= 0x0400 */

        public const int MOUSESELECT = 0x00008000;
    }
}
