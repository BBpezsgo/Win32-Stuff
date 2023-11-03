using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Contains information about a menu item.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="MENUITEMINFOW"/> structure is used with
    /// the <see cref="User32.GetMenuItemInfoW"/>, <see cref="User32.InsertMenuItemW"/>,
    /// and <see cref="User32.SetMenuItemInfoW"/> functions.
    /// </para>
    /// <para>
    /// The menu can display items using text, bitmaps, or both.
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    unsafe public struct MenuItemInfo
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// The caller must set this member to <see langword="sizeof"/>(<see cref="MENUITEMINFOW"/>).
        /// </summary>
        readonly UINT cbSize;
        /// <summary>
        /// Indicates the members to be retrieved or set.
        /// See <see cref="MIIM"/>.
        /// </summary>
        public UINT fMask;
        /// <summary>
        /// <para>
        /// The menu item type.
        /// See <see cref="MFT"/>.
        /// </para>
        /// <para>
        /// The <see cref="MFT.BITMAP"/>, <see cref="MFT.SEPARATOR"/>,
        /// and <see cref="MFT.STRING"/> values cannot be combined with one another.
        /// Set fMask to <see cref="MIIM.TYPE"/> to use <c>fType</c>.
        /// </para>
        /// <para>
        /// <c>fType</c> is used only if <see cref="fMask"/> has a value of <see cref="MIIM.FTYPE"/>.
        /// </para>
        /// </summary>
        public UINT fType;
        /// <summary>
        /// The menu item state.
        /// See <see cref="MFS"/>
        /// Set <see cref="fMask"/> to <see cref="MIIM.STATE"/> to use <see cref="fState"/>.
        /// </summary>
        public UINT fState;
        /// <summary>
        /// An application-defined value that identifies the menu item.
        /// Set <see cref="fMask"/> to <see cref="MIIM.ID"/> to use <see cref="wID"/>.
        /// </summary>
        public UINT wID;
        /// <summary>
        /// A handle to the drop-down menu or submenu associated with the menu item.
        /// If the menu item is not an item that opens a drop-down menu or submenu,
        /// this member is <c>NULL</c>. Set <see cref="fMask"/> to <see cref="MIIM.SUBMENU"/> to use <c>hSubMenu</c>.
        /// </summary>
        public HMENU hSubMenu;
        /// <summary>
        /// A handle to the bitmap to display next to the item if it is selected.
        /// If this member is <c>NULL</c>, a default bitmap is used.
        /// If the <see cref="MFT.RADIOCHECK"/> type value is specified, the default bitmap is a bullet.
        /// Otherwise, it is a check mark. Set <see cref="fMask"/>
        /// to <see cref="MIIM.CHECKMARKS"/> to use <c>hbmpChecked</c>.
        /// </summary>
        public HBITMAP hbmpChecked;
        /// <summary>
        /// A handle to the bitmap to display next to the item if it is not selected.
        /// If this member is <c>NULL</c>, no bitmap is used.
        /// Set <see cref="fMask"/> to <see cref="MIIM.CHECKMARKS"/> to use <c>hbmpUnchecked</c>.
        /// </summary>
        public HBITMAP hbmpUnchecked;
        /// <summary>
        /// An application-defined value associated with the menu item.
        /// Set <see cref="fMask"/> to <see cref="MIIM.DATA"/> to use <c>dwItemData</c>.
        /// </summary>
        public ULONG_PTR dwItemData;
        /// <summary>
        /// <para>
        /// The contents of the menu item. The meaning of this member depends on the value
        /// of <c>fType</c> and is used only if the <see cref="MIIM.TYPE"/> flag
        /// is set in the <c>fMask</c> member.
        /// </para>
        /// <para>
        /// To retrieve a menu item of type <see cref="MFT.STRING"/>,
        /// first find the size of the string by setting the
        /// dwTypeData member of <see cref="MENUITEMINFOW"/> to <c>NULL</c> and then calling
        /// <see cref="User32.GetMenuItemInfo"/>. The value of cch+1 is the size needed.
        /// Then allocate a buffer of this size, place the pointer to the
        /// buffer in <c>dwTypeData</c>, increment cch, and call <see cref="User32.GetMenuItemInfo"/>
        /// once again to fill the buffer with the string. If the retrieved
        /// menu item is of some other type, then <see cref="User32.GetMenuItemInfo"/> sets the
        /// <c>dwTypeData</c> member to a value whose type is specified by the <c>fType</c> member.
        /// </para>
        /// <para>
        /// When using with the <see cref="User32.SetMenuItemInfoW"/> function,
        /// this member should contain a value whose type
        /// is specified by the <c>fType</c> member.
        /// </para>
        /// <para>
        /// <c>dwTypeData</c> is used only if the <see cref="MIIM.STRING"/> flag is set in the <c>fMask</c> member
        /// </para>
        /// </summary>
        public WCHAR* dwTypeData;
        /// <summary>
        /// <para>
        /// The length of the menu item text, in characters,
        /// when information is received about a menu item of
        /// the <see cref="MFT.STRING"/> type. However, <c>cch</c> is used only if
        /// the <see cref="MIIM.TYPE"/> flag is set in the <c>fMask</c> member and is
        /// zero otherwise. Also, <c>cch</c> is ignored when the content
        /// of a menu item is set by calling <see cref="User32.SetMenuItemInfoW"/>.
        /// </para>
        /// <para>
        /// Note that, before calling <see cref="User32.GetMenuItemInfo"/>, the application must
        /// set <c>cch</c> to the length of the buffer pointed to by the <c>dwTypeData</c> member.
        /// If the retrieved menu item is of type <see cref="MFT.STRING"/> (as indicated by
        /// the <c>fType</c> member), then <see cref="User32.GetMenuItemInfo"/> changes cch to the length
        /// of the menu item text. If the retrieved menu item is of some other
        /// type, <see cref="User32.GetMenuItemInfo"/> sets the <c>cch</c> field to zero.
        /// </para>
        /// <para>
        /// The <c>cch</c> member is used when the <see cref="MIIM.STRING"/> flag is set in the <c>fMask</c> member.
        /// </para>
        /// </summary>
        public UINT cch;
        /// <summary>
        /// A handle to the bitmap to be displayed.
        /// See <see cref="HBMMENU"/>.
        /// It is used when the <see cref="MIIM.BITMAP"/> flag is set in the <c>fMask</c> member.
        /// </summary>
        public HBITMAP hbmpItem;

        MenuItemInfo(DWORD cbSize) : this() => this.cbSize = cbSize;
        unsafe public static MenuItemInfo Create() => new((uint)sizeof(MenuItemInfo));
    }
}
