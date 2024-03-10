global using MENUINFO = Win32.Forms.MenuInfo;

namespace Win32.Forms;

/// <summary>
/// Contains information about a menu.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MenuInfo
{
    /// <summary>
    /// The size of the structure, in bytes.
    /// The caller must set this member to <see langword="sizeof"/>(<see cref="MENUINFO"/>).
    /// </summary>
    readonly DWORD StructSize;
    /// <summary>
    /// <para>
    /// Indicates the members to be retrieved or set (except for <see cref="MenuInfoMasks.APPLYTOSUBMENUS"/>).
    /// </para>
    /// <para>
    /// See <see cref="MenuInfoMasks"/>
    /// </para>
    /// </summary>
    public DWORD Mask;
    /// <summary>
    /// <para>
    /// The menu style.
    /// </para>
    /// <para>
    /// See <see cref="MenuStyles"/>
    /// </para>
    /// </summary>
    public DWORD Style;
    /// <summary>
    /// The maximum height of the menu in pixels. When the menu items exceed the space available,
    /// scroll bars are automatically used. The default (0) is the screen height.
    /// </summary>
    public UINT MaxHeight;
    /// <summary>
    /// A handle to the brush to be used for the menu's background.
    /// </summary>
    public HBRUSH BackBrushHandle;
    /// <summary>
    /// The context help identifier. This is the same value used
    /// in the <c>GetMenuContextHelpId</c> and <c>SetMenuContextHelpId</c> functions.
    /// </summary>
    public DWORD ContextHelpId;
    /// <summary>
    /// An application-defined value.
    /// </summary>
    public ULONG_PTR MenuData;

    MenuInfo(DWORD structSize) : this() => this.StructSize = structSize;
    public static unsafe MENUINFO Create() => new((DWORD)sizeof(MENUINFO));
}
