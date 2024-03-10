global using MENUBARINFO = Win32.Forms.MenuBarInfo;

namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
public readonly struct MenuBarInfo
{
    readonly DWORD StructSize;

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
    readonly uint Flags;
    /// <summary>
    /// If the menu bar or popup menu has the focus, this member is <see langword="true"/>. Otherwise, the member is <see langword="false"/>.
    /// </summary>
    public bool BarFocused => (Flags & 0b_10000000_00000000_00000000_00000000) != 0;
    /// <summary>
    /// If the menu item has the focus, this member is <see langword="true"/>. Otherwise, the member is <see langword="false"/>.
    /// </summary>
    public bool Focused => (Flags & 0b_01000000_00000000_00000000_00000000) != 0;

    MenuBarInfo(DWORD structSize) : this() => StructSize = structSize;

    public static unsafe MENUBARINFO Create() => new((DWORD)sizeof(MENUBARINFO));
}
