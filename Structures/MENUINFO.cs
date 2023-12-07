global using MENUINFO = Win32.MenuInfo;

using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Contains information about a menu.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MenuInfo
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// The caller must set this member to <see langword="sizeof"/>(<see cref="MENUINFO"/>).
        /// </summary>
        readonly DWORD cbSize;
        /// <summary>
        /// <para>
        /// Indicates the members to be retrieved or set (except for <see cref="MenuInfoMasks.APPLYTOSUBMENUS"/>).
        /// </para>
        /// <para>
        /// See <see cref="MenuInfoMasks"/>
        /// </para>
        /// </summary>
        public DWORD fMask;
        /// <summary>
        /// <para>
        /// The menu style.
        /// </para>
        /// <para>
        /// See <see cref="MenuStyles"/>
        /// </para>
        /// </summary>
        public DWORD dwStyle;
        /// <summary>
        /// The maximum height of the menu in pixels. When the menu items exceed the space available,
        /// scroll bars are automatically used. The default (0) is the screen height.
        /// </summary>
        public UINT cyMax;
        /// <summary>
        /// A handle to the brush to be used for the menu's background.
        /// </summary>
        public HBRUSH hbrBack;
        /// <summary>
        /// The context help identifier. This is the same value used
        /// in the <c>GetMenuContextHelpId</c> and <c>SetMenuContextHelpId</c> functions.
        /// </summary>
        public DWORD dwContextHelpID;
        /// <summary>
        /// An application-defined value.
        /// </summary>
        public ULONG_PTR dwMenuData;

        MenuInfo(uint cbSize) : this() => this.cbSize = cbSize;
        unsafe public static MENUINFO Create() => new((uint)sizeof(MENUINFO));
    }
}
