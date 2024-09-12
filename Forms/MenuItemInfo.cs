namespace Win32.Forms;

/// <summary>
/// Contains information about a menu item.
/// </summary>
/// <remarks>
/// <para>
/// The <see cref="MenuItemInfo"/> structure is used with
/// the <see cref="User32.GetMenuItemInfoW"/>, <see cref="User32.InsertMenuItemW"/>,
/// and <see cref="User32.SetMenuItemInfoW"/> functions.
/// </para>
/// <para>
/// The menu can display items using text, bitmaps, or both.
/// </para>
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct MenuItemInfo
{
    /// <summary>
    /// The size of the structure, in bytes.
    /// The caller must set this member to <see langword="sizeof"/>(<see cref="MenuItemInfo"/>).
    /// </summary>
    readonly UINT StructSize;
    /// <summary>
    /// Indicates the members to be retrieved or set.
    /// See <see cref="MenuItemInfoMasks"/>.
    /// </summary>
    public UINT Mask;
    /// <summary>
    /// <para>
    /// The menu item type.
    /// See <see cref="MFT"/>.
    /// </para>
    /// <para>
    /// The <see cref="MFT.Bitmap"/>, <see cref="MFT.Separator"/>,
    /// and <see cref="MFT.String"/> values cannot be combined with one another.
    /// Set <c>Mask</c> to <see cref="MenuItemInfoMasks.Type"/> to use <c>Type</c>.
    /// </para>
    /// <para>
    /// <c>Type</c> is used only if <see cref="Mask"/> has a value of <see cref="MenuItemInfoMasks.FType"/>.
    /// </para>
    /// </summary>
    public UINT Type;
    /// <summary>
    /// The menu item state.
    /// See <see cref="MFS"/>
    /// Set <see cref="Mask"/> to <see cref="MenuItemInfoMasks.State"/> to use <see cref="State"/>.
    /// </summary>
    public UINT State;
    /// <summary>
    /// An application-defined value that identifies the menu item.
    /// Set <see cref="Mask"/> to <see cref="MenuItemInfoMasks.Id"/> to use <see cref="wID"/>.
    /// </summary>
    public UINT wID;
    /// <summary>
    /// A handle to the drop-down menu or submenu associated with the menu item.
    /// If the menu item is not an item that opens a drop-down menu or submenu,
    /// this member is <c>NULL</c>. Set <see cref="Mask"/> to
    /// <see cref="MenuItemInfoMasks.SubMenu"/> to use <c>SubMenuHandle</c>.
    /// </summary>
    public HMENU SubMenuHandle;
    /// <summary>
    /// A handle to the bitmap to display next to the item if it is selected.
    /// If this member is <c>NULL</c>, a default bitmap is used.
    /// If the <see cref="MFT.RadioCheck"/> type value is specified, the default bitmap is a bullet.
    /// Otherwise, it is a check mark. Set <see cref="Mask"/>
    /// to <see cref="MenuItemInfoMasks.CheckMarks"/> to use <c>CheckedBitmapHandle</c>.
    /// </summary>
    public HBITMAP CheckedBitmapHandle;
    /// <summary>
    /// A handle to the bitmap to display next to the item if it is not selected.
    /// If this member is <c>NULL</c>, no bitmap is used.
    /// Set <see cref="Mask"/> to <see cref="MenuItemInfoMasks.CheckMarks"/> to use <c>UncheckedBitmapHandle</c>.
    /// </summary>
    public HBITMAP UncheckedBitmapHandle;
    /// <summary>
    /// An application-defined value associated with the menu item.
    /// Set <see cref="Mask"/> to <see cref="MenuItemInfoMasks.Data"/> to use <c>ItemData</c>.
    /// </summary>
    public ULONG_PTR ItemData;
    /// <summary>
    /// <para>
    /// The contents of the menu item. The meaning of this member depends on the value
    /// of <c>Type</c> and is used only if the <see cref="MenuItemInfoMasks.Type"/> flag
    /// is set in the <c>Mask</c> member.
    /// </para>
    /// <para>
    /// To retrieve a menu item of type <see cref="MFT.String"/>,
    /// first find the size of the string by setting the
    /// dwTypeData member of <see cref="MenuItemInfo"/> to <c>NULL</c> and then calling
    /// <see cref="User32.GetMenuItemInfoW"/>. The value of cch+1 is the size needed.
    /// Then allocate a buffer of this size, place the pointer to the
    /// buffer in <c>TypeData</c>, increment cch, and call <see cref="User32.GetMenuItemInfoW"/>
    /// once again to fill the buffer with the string. If the retrieved
    /// menu item is of some other type, then <see cref="User32.GetMenuItemInfoW"/> sets the
    /// <c>TypeData</c> member to a value whose type is specified by the <c>Type</c> member.
    /// </para>
    /// <para>
    /// When using with the <see cref="User32.SetMenuItemInfoW"/> function,
    /// this member should contain a value whose type
    /// is specified by the <c>Type</c> member.
    /// </para>
    /// <para>
    /// <c>TypeData</c> is used only if the <see cref="MenuItemInfoMasks.String"/> flag
    /// is set in the <c>Mask</c> member
    /// </para>
    /// </summary>
    public unsafe WCHAR* TypeData;
    /// <summary>
    /// <para>
    /// The length of the menu item text, in characters,
    /// when information is received about a menu item of
    /// the <see cref="MFT.String"/> type. However, <c>cch</c> is used only if
    /// the <see cref="MenuItemInfoMasks.Type"/> flag is set in the <c>Mask</c> member and is
    /// zero otherwise. Also, <c>cch</c> is ignored when the content
    /// of a menu item is set by calling <see cref="User32.SetMenuItemInfoW"/>.
    /// </para>
    /// <para>
    /// Note that, before calling <see cref="User32.GetMenuItemInfoW"/>, the application must
    /// set <c>cch</c> to the length of the buffer pointed to by the <c>TypeData</c> member.
    /// If the retrieved menu item is of type <see cref="MFT.String"/> (as indicated by
    /// the <c>Type</c> member), then <see cref="User32.GetMenuItemInfoW"/> changes cch to the length
    /// of the menu item text. If the retrieved menu item is of some other
    /// type, <see cref="User32.GetMenuItemInfoW"/> sets the <c>cch</c> field to zero.
    /// </para>
    /// <para>
    /// The <c>cch</c> member is used when the <see cref="MenuItemInfoMasks.String"/>
    /// flag is set in the <c>Mask</c> member.
    /// </para>
    /// </summary>
    public UINT cch;
    /// <summary>
    /// A handle to the bitmap to be displayed.
    /// See <see cref="MenuBitmapHandle"/>.
    /// It is used when the <see cref="MenuItemInfoMasks.Bitmap"/> flag is set in the <c>Mask</c> member.
    /// </summary>
    public HBITMAP ItemBitmapHandle;

    MenuItemInfo(DWORD structSize) : this() => this.StructSize = structSize;
    public static unsafe MenuItemInfo Create() => new((DWORD)sizeof(MenuItemInfo));
}
