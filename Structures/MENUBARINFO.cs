using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly struct MenuBarInfo
    {
        /// <summary>
        /// The size of the structure, in bytes. The caller must set this to sizeof(MENUBARINFO).
        /// </summary>
        readonly DWORD cbSize;
        /// <summary>
        /// The coordinates of the menu bar, popup menu, or menu item.
        /// </summary>
        public readonly RECT BarRect;
        /// <summary>
        /// A handle to the menu bar or popup menu.
        /// </summary>
        public readonly HMENU Menu;
        /// <summary>
        /// A handle to the submenu.
        /// </summary>
        public readonly HWND Submenu;
        readonly uint flags;
        /// <summary>
        /// If the menu bar or popup menu has the focus, this member is <see langword="true"/>. Otherwise, the member is <see langword="false"/>.
        /// </summary>
        public bool BarFocused => (flags & 0b_10000000_00000000_00000000_00000000) != 0;
        /// <summary>
        /// If the menu item has the focus, this member is <see langword="true"/>. Otherwise, the member is <see langword="false"/>.
        /// </summary>
        public bool Focused => (flags & 0b_01000000_00000000_00000000_00000000) != 0;

        MenuBarInfo(DWORD cbSize) : this() => this.cbSize = cbSize;
        unsafe public static MenuBarInfo Create() => new((uint)sizeof(MenuBarInfo));
    }
}
