namespace Win32.Forms;

public static class MenuItemInfoMasks
{
    /// <summary>
    /// Retrieves or sets the <c>hbmpItem</c> member.
    /// </summary>
    public const int Bitmap = 0x00000080;
    /// <summary>
    /// Retrieves or sets the <c>hbmpChecked</c> and <c>hbmpUnchecked</c> members.
    /// </summary>
    public const int CheckMarks = 0x00000008;
    /// <summary>
    /// Retrieves or sets the <c>dwItemData</c> member.
    /// </summary>
    public const int Data = 0x00000020;
    /// <summary>
    /// Retrieves or sets the <see cref="MenuItemInfo.Type"/> member.
    /// </summary>
    public const int FType = 0x00000100;
    /// <summary>
    /// Retrieves or sets the <c>wID</c> member.
    /// </summary>
    public const int Id = 0x00000002;
    /// <summary>
    /// Retrieves or sets the <c>fState</c> member.
    /// </summary>
    public const int State = 0x00000001;
    /// <summary>
    /// Retrieves or sets the <see cref="MenuItemInfo.TypeData"/> member.
    /// </summary>
    public const int String = 0x00000040;
    /// <summary>
    /// Retrieves or sets the <see cref="MenuItemInfo.SubMenuHandle"/> member.
    /// </summary>
    public const int SubMenu = 0x00000004;
    /// <summary>
    /// Retrieves or sets the <see cref="MenuItemInfo.Type"/> and <see cref="MenuItemInfo.TypeData"/> members.
    /// <see cref="Type"/> is replaced by <see cref="Bitmap"/>, <see cref="FType"/>, and <see cref="String"/>.
    /// </summary>
    public const int Type = 0x00000010;
}
