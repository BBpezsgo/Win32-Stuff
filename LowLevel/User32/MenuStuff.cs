namespace Win32;

[SupportedOSPlatform("windows")]
public static partial class User32
{
    [DllImport("User32.dll", SetLastError = true)]
    public static extern HMENU GetSystemMenu(
      [In] HWND hWnd,
      [In] BOOL bRevert
    );

    [DllImport("User32.dll", SetLastError = true)]
    public static extern BOOL SetMenuItemBitmaps(
      [In] HMENU hMenu,
      [In] UINT uPosition,
      [In] UINT uFlags,
      [In, Optional] HBITMAP hBitmapUnchecked,
      [In, Optional] HBITMAP hBitmapChecked
    );

    /// <summary>
    /// Retrieves a handle to the drop-down menu or submenu activated by the specified menu item.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the menu.
    /// </param>
    /// <param name="nPos">
    /// The zero-based relative position in the specified menu
    /// of an item that activates a drop-down menu or submenu.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is a handle to the
    /// drop-down menu or submenu activated by the menu item.
    /// If the menu item does not activate a drop-down menu or submenu, the return value is <c>NULL</c>.
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern HMENU GetSubMenu(
      [In] HMENU hMenu,
      [In] int nPos
    );

    /// <summary>
    /// Displays a shortcut menu at the specified location and tracks the
    /// selection of items on the shortcut menu.
    /// The shortcut menu can appear anywhere on the screen.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the shortcut menu to be displayed.
    /// This handle can be obtained by calling the <see cref="CreatePopupMenu"/>
    /// function to create a new shortcut menu or by calling
    /// the <see cref="GetSubMenu"/> function to retrieve a handle to a
    /// submenu associated with an existing menu item.
    /// </param>
    /// <param name="uFlags">
    /// <para>
    /// Specifies function options.
    /// </para>
    /// <para>
    /// For any animation to occur, the <see cref="SystemParametersInfoW"/> function must
    /// set <see cref="SPI.SETMENUANIMATION"/>. Also, all the <see cref="TrackPopupMenuFlags"/>*ANIMATION flags, except
    /// <see cref="TrackPopupMenuFlags.NoAnimation"/>, are ignored if menu fade animation is on.
    /// For more information, see the <see cref="SPI.GETMENUFADE"/> flag in <see cref="SystemParametersInfoW"/>.
    /// </para>
    /// <para>
    /// Use the <see cref="TrackPopupMenuFlags.Recurse"/> flag to display a menu when another menu is already displayed.
    /// This is intended to support context menus within a menu.
    /// </para>
    /// <para>
    /// The excluded rectangle is a portion of the screen that
    /// the menu should not overlap; it is specified by the <paramref name="lptpm"/> parameter.
    /// </para>
    /// <para>
    /// For right-to-left text layout, use <see cref="TrackPopupMenuFlags.LayoutRTL"/>. By default, the text layout is left-to-right.
    /// </para>
    /// </param>
    /// <param name="x">
    /// The horizontal location of the shortcut menu, in screen coordinates.
    /// </param>
    /// <param name="y">
    /// The vertical location of the shortcut menu, in screen coordinates.
    /// </param>
    /// <param name="hwnd">
    /// A handle to the window that owns the shortcut menu.
    /// This window receives all messages from the menu.
    /// The window does not receive a <see cref="WindowMessage.WM_COMMAND"/> message
    /// from the menu until the function returns.
    /// If you specify <see cref="TrackPopupMenuFlags.NoNotify"/> in the <paramref name="uFlags"/> parameter,
    /// the function does not send messages to the window
    /// identified by hwnd. However, you must still pass a
    /// window handle in hwnd. It can be any window handle
    /// from your application.
    /// </param>
    /// <param name="lptpm">
    /// A pointer to a <see cref="TrackPopupMenuParams"/> structure that specifies an area of
    /// the screen the menu should not overlap. This parameter can be <c>NULL</c>.
    /// </param>
    /// <returns>
    /// <para>
    /// If you specify <see cref="TrackPopupMenuFlags.ReturnCMD"/> in the <paramref name="uFlags"/> parameter,
    /// the return value is the menu-item identifier of the item
    /// that the user selected. If the user cancels the menu without
    /// making a selection, or if an error occurs, the return value is zero.
    /// </para>
    /// <para>
    /// If you do not specify <see cref="TrackPopupMenuFlags.ReturnCMD"/> in the <paramref name="uFlags"/> parameter,
    /// the return value is nonzero if the function succeeds and zero
    /// if it fails. To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// Call <see cref="GetSystemMetrics"/> with <see cref="SystemMetricsFlags.MENUDROPALIGNMENT"/> to determine
    /// the correct horizontal alignment flag (<see cref="TrackPopupMenuFlags.LeftAlign"/> or <see cref="TrackPopupMenuFlags.RightAlign"/>)
    /// and/or horizontal animation direction flag (<see cref="TrackPopupMenuFlags.HPosAnimation"/> or
    /// <see cref="TrackPopupMenuFlags.HNegativeAnimation"/>) to pass to <see cref="TrackPopupMenu"/>
    /// or <c>TrackPopupMenuEx</c>.
    /// This is essential for creating an optimal user experience, especially
    /// when developing Microsoft Tablet PC applications.
    /// </para>
    /// <para>
    /// To display a context menu for a notification icon, the current
    /// window must be the foreground window before the application calls
    /// <see cref="TrackPopupMenu"/> or <c>TrackPopupMenuEx</c>. Otherwise, the menu will not
    /// disappear when the user clicks outside of the menu or the window
    /// that created the menu (if it is visible). If the current window
    /// is a child window, you must set the (top-level) parent window as
    /// the foreground window.
    /// </para>
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL TrackPopupMenuEx(
      [In] HMENU hMenu,
      [In] Forms.TrackPopupMenuFlags uFlags,
      [In] int x,
      [In] int y,
      [In] HWND hwnd,
      [In, Optional] Forms.TrackPopupMenuParams* lptpm
    );

    /// <summary>
    /// Displays a shortcut menu at the specified location
    /// and tracks the selection of items on the menu.
    /// The shortcut menu can appear anywhere on the screen.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the shortcut menu to be displayed.
    /// The handle can be obtained by calling <see cref="CreatePopupMenu"/> to
    /// create a new shortcut menu, or by calling <see cref="GetSubMenu"/> to
    /// retrieve a handle to a submenu associated with an existing menu item.
    /// </param>
    /// <param name="uFlags">
    /// <para>
    /// Use zero of more of these flags to specify function options.
    /// See <see cref="TrackPopupMenuFlags"/>
    /// </para>
    /// <para>
    /// For any animation to occur, the <see cref="SystemParametersInfoW"/> function
    /// must set <see cref="SPI.SETMENUANIMATION"/>. Also, all the <see cref="TrackPopupMenuFlags"/>.*ANIMATION flags,
    /// except <see cref="TrackPopupMenuFlags.NoAnimation"/>, are ignored if menu fade animation is on.
    /// For more information, see the <see cref="SPI.GETMENUFADE"/> flag in <see cref="SystemParametersInfoW"/>.
    /// </para>
    /// <para>
    /// Use the <see cref="TrackPopupMenuFlags.Recurse"/> flag to display a menu when another menu is already displayed.
    /// This is intended to support context menus within a menu.
    /// </para>
    /// <para>
    /// For right-to-left text layout, use <see cref="TrackPopupMenuFlags.LayoutRTL"/>. By default, the text layout is left-to-right.
    /// </para>
    /// </param>
    /// <param name="x">
    /// The horizontal location of the shortcut menu, in screen coordinates.
    /// </param>
    /// <param name="y">
    /// The vertical location of the shortcut menu, in screen coordinates.
    /// </param>
    /// <param name="nReserved">
    /// Reserved; must be zero.
    /// </param>
    /// <param name="hWnd">
    /// A handle to the window that owns the shortcut menu.
    /// This window receives all messages from the menu.
    /// The window does not receive a <see cref="WindowMessage.WM_COMMAND"/> message from the
    /// menu until the function returns. If you specify <see cref="TrackPopupMenuFlags.NoNotify"/>
    /// in the <paramref name="uFlags"/> parameter, the function does not send messages
    /// to the window identified by <paramref name="hWnd"/>. However, you must still
    /// pass a window handle in <paramref name="hWnd"/>. It can be any window handle
    /// from your application.
    /// </param>
    /// <param name="prcRect">
    /// Ignored.
    /// </param>
    /// <returns>
    /// <para>
    /// If you specify <see cref="TrackPopupMenuFlags.ReturnCMD"/> in the <paramref name="uFlags"/> parameter,
    /// the return value is the menu-item identifier of the item that the user selected.
    /// If the user cancels the menu without making a selection,
    /// or if an error occurs, the return value is zero.
    /// </para>
    /// <para>
    /// If you do not specify <see cref="TrackPopupMenuFlags.ReturnCMD"/> in the <paramref name="uFlags"/> parameter, the return value is
    /// nonzero if the function succeeds and zero if it fails.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL TrackPopupMenu(
      [In] HMENU hMenu,
      [In] Forms.TrackPopupMenuFlags uFlags,
      [In] int x,
      [In] int y,
      [In] int nReserved,
      [In] HWND hWnd,
      [In, Optional] RECT* prcRect
    );

    /// <summary>
    /// Sets the default menu item for the specified menu.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the menu to set the default item for.
    /// </param>
    /// <param name="uItem">
    /// The identifier or position of the new default menu item
    /// or -1 for no default item. The meaning of this
    /// parameter depends on the value of <paramref name="fByPos"/>.
    /// </param>
    /// <param name="fByPos">
    /// The meaning of <paramref name="uItem"/>. If this parameter is <c>FALSE</c>,
    /// <paramref name="uItem"/> is a menu item identifier.
    /// Otherwise, it is a menu item position. See <see href="https://learn.microsoft.com/en-us/windows/desktop/menurc/about-menus">About Menus</see> for more information.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, use the <see cref="Kernel32.GetLastError"/> function.
    /// </para>
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern BOOL SetMenuDefaultItem(
      [In] HMENU hMenu,
      [In] UINT uItem,
      [In] UINT fByPos
    );

    /// <summary>
    /// Deletes a menu item or detaches a submenu from the specified menu.
    /// If the menu item opens a drop-down menu or submenu,
    /// <c>RemoveMenu</c> does not destroy the menu or its handle,
    /// allowing the menu to be reused. Before this function
    /// is called, the <see cref="GetSubMenu"/> function should retrieve a
    /// handle to the drop-down menu or submenu.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the menu to be changed.
    /// </param>
    /// <param name="uPosition">
    /// The menu item to be deleted, as determined by the <paramref name="uFlags"/> parameter.
    /// </param>
    /// <param name="uFlags">
    /// Indicates how the <paramref name="uPosition"/> parameter is interpreted.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// The application must call the <see cref="DrawMenuBar"/> function whenever a
    /// menu changes, whether the menu is in a displayed window.
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern BOOL RemoveMenu(
      [In] HMENU hMenu,
      [In] UINT uPosition,
      [In] UINT uFlags
    );

    /// <summary>
    /// Determines which menu item, if any, is at the specified location.
    /// </summary>
    /// <param name="hWnd">
    /// A handle to the window containing the menu. If this value is <c>NULL</c> and
    /// the <paramref name="hMenu"/> parameter represents a popup menu, the function will find the menu window.
    /// </param>
    /// <param name="hMenu">
    /// A handle to the menu containing the menu items to hit test.
    /// </param>
    /// <param name="ptScreen">
    /// A structure that specifies the location to test. If <paramref name="hMenu"/> specifies a menu bar,
    /// this parameter is in window coordinates. Otherwise, it is in client coordinates.
    /// </param>
    /// <returns>
    /// Returns the zero-based position of the menu item at the specified
    /// location or -1 if no menu item is at the specified location.
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern int MenuItemFromPoint(
      [In, Optional] HWND hWnd,
      [In] HMENU hMenu,
      [In] POINT ptScreen
    );

    /// <summary>
    /// <para>
    /// Changes an existing menu item. This function is used to specify
    /// the content, appearance, and behavior of the menu item.
    /// </para>
    /// <para>
    /// <b>Note:</b>
    /// The <c>ModifyMenuW</c> function has been superseded by the
    /// <see cref="SetMenuItemInfoW"/> function. You can still use <c>ModifyMenuW</c>, however,
    /// if you do not need any of the extended features of <see cref="SetMenuItemInfoW"/>.
    /// </para>
    /// </summary>
    /// <param name="hMnu">
    /// A handle to the menu to be changed.
    /// </param>
    /// <param name="uPosition">
    /// The menu item to be changed, as determined by the <paramref name="uFlags"/> parameter.
    /// </param>
    /// <param name="uFlags">
    /// Controls the interpretation of the <paramref name="uPosition"/> parameter and the content,
    /// appearance, and behavior of the menu item.
    /// </param>
    /// <param name="uIDNewItem">
    /// The identifier of the modified menu item or, if the <paramref name="uFlags"/> parameter has
    /// the <see cref="MenuFlags.Popup"/> flag set, a handle to the drop-down menu or submenu.
    /// </param>
    /// <param name="lpNewItem">
    /// The contents of the changed menu item. The interpretation of this parameter depends
    /// on whether the <paramref name="uFlags"/> parameter includes
    /// the <see cref="MenuFlags.Bitmap"/>, <see cref="MenuFlags.OwnerDraw"/>, or <see cref="MenuFlags.String"/> flag.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    [Obsolete($"Use {nameof(SetMenuItemInfoW)} instead")]
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL ModifyMenuW(
      [In] HMENU hMnu,
      [In] UINT uPosition,
      [In] UINT uFlags,
      [In] UINT_PTR uIDNewItem,
      [In, Optional] WCHAR* lpNewItem
    );

    /// <summary>
    /// <para>
    /// Inserts a new menu item into a menu, moving other items down the menu.
    /// </para>
    /// <para>
    /// <b>Note:</b>
    /// The <c>InsertMenuW</c> function has been superseded by the <see cref="InsertMenuItemW"/> function.
    /// You can still use <c>InsertMenuW</c>, however, if you do not need any
    /// of the extended features of <see cref="InsertMenuItemW"/>.
    /// </para>
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the menu to be changed.
    /// </param>
    /// <param name="uPosition">
    /// The menu item before which the new menu item is to be inserted,
    /// as determined by the <paramref name="uFlags"/> parameter.
    /// </param>
    /// <param name="uFlags">
    /// Controls the interpretation of the <paramref name="uPosition"/> parameter and the content,
    /// appearance, and behavior of the new menu item.
    /// </param>
    /// <param name="uIDNewItem">
    /// The identifier of the new menu item or, if the <paramref name="uFlags"/> parameter has
    /// the <c>MF.POPUP</c> flag set, a handle to the drop-down menu or submenu.
    /// </param>
    /// <param name="lpNewItem">
    /// The content of the new menu item. The interpretation of <paramref name="lpNewItem"/> depends
    /// on whether the <paramref name="uFlags"/>
    /// parameter includes the <see cref="MenuFlags.Bitmap"/>, <see cref="MenuFlags.OwnerDraw"/>, or <see cref="MenuFlags.String"/> flag.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// The application must call the <see cref="DrawMenuBar"/> function whenever a
    /// menu changes, whether the menu is in a displayed window.
    /// </para>
    /// <para>
    /// The following groups of flags cannot be used together:
    /// <list type="bullet">
    /// <item>
    /// <see cref="MenuFlags.ByCommand"/> and <see cref="MenuFlags.ByPosition"/>
    /// </item>
    /// <item>
    /// <see cref="MenuFlags.Disabled"/>, <see cref="MenuFlags.Enabled"/>, and <see cref="MenuFlags.Grayed"/>
    /// </item>
    /// <item>
    /// <see cref="MenuFlags.Bitmap"/>, <see cref="MenuFlags.String"/>,
    /// <see cref="MenuFlags.OwnerDraw"/>, and <see cref="MenuFlags.Separator"/>
    /// </item>
    /// <item>
    /// <see cref="MenuFlags.MenuBarBreak"/> and <see cref="MenuFlags.MenuBreak"/>
    /// </item>
    /// <item>
    /// <see cref="MenuFlags.Checked"/> and <see cref="MenuFlags.Unchecked"/>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    [Obsolete($"Use {nameof(InsertMenuItemW)} instead")]
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL InsertMenuW(
      [In] HMENU hMenu,
      [In] UINT uPosition,
      [In] UINT uFlags,
      [In] UINT_PTR uIDNewItem,
      [In, Optional] WCHAR* lpNewItem
    );

    /// <summary>
    /// Adds or removes highlighting from an item in a menu bar.
    /// </summary>
    /// <param name="hWnd">
    /// A handle to the window that contains the menu.
    /// </param>
    /// <param name="hMenu">
    /// A handle to the menu bar that contains the item.
    /// </param>
    /// <param name="uIDHiliteItem">
    /// The menu item. This parameter is either the identifier of the menu
    /// item or the offset of the menu item in the menu bar, depending
    /// on the value of the <paramref name="uHilite"/> parameter.
    /// </param>
    /// <param name="uHilite">
    /// Controls the interpretation of the <paramref name="uIDHiliteItem"/> parameter and indicates
    /// whether the menu item is highlighted. This parameter must be a
    /// combination of either <see cref="MenuFlags.ByCommand"/> or <see cref="MenuFlags.ByPosition"/>
    /// and <see cref="MenuFlags.Hilite"/> or <see cref="MenuFlags.Unhilite"/>.
    /// </param>
    /// <returns>
    /// <para>
    /// If the menu item is set to the specified highlight state, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the menu item is not set to the specified highlight state, the return value is zero.
    /// </para>
    /// </returns>
    /// <remarks>
    /// The <see cref="MenuFlags.Hilite"/> and <see cref="MenuFlags.Unhilite"/> flags can be used only with
    /// the <c>HiliteMenuItem</c> function; they cannot be used with the <see cref="ModifyMenuW"/> function.
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern BOOL HiliteMenuItem(
      [In] HWND hWnd,
      [In] HMENU hMenu,
      [In] UINT uIDHiliteItem,
      [In] UINT uHilite
    );

    /// <summary>
    /// Creates a drop-down menu, submenu, or shortcut menu.
    /// The menu is initially empty. You can insert or append
    /// menu items by using the <see cref="InsertMenuItemW"/> function.
    /// You can also use the <see cref="InsertMenuW"/> function to insert
    /// menu items and the <see cref="AppendMenuW"/> function to append menu items.
    /// </summary>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is a handle to the newly created menu.
    /// </para>
    /// <para>
    /// If the function fails, the return value is <c>NULL</c>.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// The application can add the new menu to an existing menu,
    /// or it can display a shortcut menu by calling
    /// the <see cref="TrackPopupMenuEx"/> or <see cref="TrackPopupMenu"/> functions.
    /// </para>
    /// <para>
    /// Resources associated with a menu that is assigned to a window are freed automatically.
    /// If the menu is not assigned to a window, an application must
    /// free system resources associated with the menu before closing.
    /// An application frees menu resources by calling the <see cref="DestroyMenu"/> function.
    /// </para>
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern HMENU CreatePopupMenu();

    /// <summary>
    /// Checks a specified menu item and makes it a radio item.
    /// At the same time, the function clears all other menu items
    /// in the associated group and clears the radio-item type flag for those items.
    /// </summary>
    /// <param name="hmenu">
    /// A handle to the menu that contains the group of menu items.
    /// </param>
    /// <param name="first">
    /// The identifier or position of the first menu item in the group.
    /// </param>
    /// <param name="last">
    /// The identifier or position of the last menu item in the group.
    /// </param>
    /// <param name="check">
    /// The identifier or position of the menu item to check.
    /// </param>
    /// <param name="flags">
    /// Indicates the meaning of <paramref name="first"/>, <paramref name="last"/>,
    /// and <paramref name="check"/>.
    /// If this parameter is <see cref="MenuFlags.ByCommand"/>, the other parameters
    /// specify menu item identifiers. If it is <see cref="MenuFlags.ByPosition"/>,
    /// the other parameters specify the menu item positions.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, use the <see cref="Kernel32.GetLastError"/> function.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// The <c>CheckMenuRadioItem</c> function sets the <see cref="MFT.RadioCheck"/> type flag and
    /// the <see cref="MFS.CHECKED"/> state for the item specified by <paramref name="check"/>
    /// and, at the same time,
    /// clears both flags for all other items in the group. The selected item is
    /// displayed using a bullet bitmap instead of a check-mark bitmap.
    /// </para>
    /// <para>
    /// For more information about menu item type and state flags, see the <see cref="MenuItemInfo"/> structure.
    /// </para>
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern BOOL CheckMenuRadioItem(
      [In] HMENU hmenu,
      [In] UINT first,
      [In] UINT last,
      [In] UINT check,
      [In] UINT flags
    );

    /// <summary>
    /// Determines whether a handle is a menu handle.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to be tested.
    /// </param>
    /// <returns>
    /// <para>
    /// If the handle is a menu handle, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the handle is not a menu handle, the return value is zero.
    /// </para>
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern BOOL IsMenu(
      [In] HMENU hMenu
    );

    /// <summary>
    /// Retrieves information about the specified menu bar.
    /// </summary>
    /// <param name="hwnd">
    /// A handle to the window (menu bar) whose information is to be retrieved.
    /// </param>
    /// <param name="idObject">
    /// The menu object. This parameter can be one of the following values.
    /// <list type="table">
    /// <item>
    /// <term>
    /// OBJID_CLIENT ((LONG)0xFFFFFFFC)
    /// </term>
    /// <description>
    /// The popup menu associated with the window.
    /// </description>
    /// </item>
    /// <item>
    /// <term>
    /// OBJID_MENU ((LONG)0xFFFFFFFD)
    /// </term>
    /// <description>
    /// The menu bar associated with the window (see the <see cref="GetMenu"/> function).
    /// </description>
    /// </item>
    /// <item>
    /// <term>
    /// OBJID_SYSMENU ((LONG)0xFFFFFFFF)
    /// </term>
    /// <description>
    /// The system menu associated with the window (see the <see cref="GetSystemMenu"/> function).
    /// </description>
    /// </item>
    /// </list>
    /// </param>
    /// <param name="idItem">
    /// The item for which to retrieve information.
    /// If this parameter is zero, the function retrieves information about the menu itself.
    /// If this parameter is 1, the function retrieves information about the
    /// first item on the menu, and so on.
    /// </param>
    /// <param name="pmbi">
    /// A pointer to a <see cref="MENUBARINFO"/> structure that receives the information.
    /// Note that you must set the cbSize member to <see langword="sizeof"/>(<see cref="MENUBARINFO"/>) before calling this function.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetMenuBarInfo(
      [In] HWND hwnd,
      [In] LONG idObject,
      [In] LONG idItem,
      [In, Out] MENUBARINFO* pmbi
    );

    /// <summary>
    /// Retrieves information about a menu item.
    /// </summary>
    /// <param name="hmenu">
    /// A handle to the menu that contains the menu item.
    /// </param>
    /// <param name="item">
    /// The identifier or position of the menu item to get information about.
    /// The meaning of this parameter depends on the value of <paramref name="fByPosition"/>.
    /// </param>
    /// <param name="fByPosition">
    /// The meaning of <paramref name="item"/>. If this parameter is <c>FALSE</c>,
    /// <paramref name="item"/> is a menu item identifier. Otherwise, it is a menu item position.
    /// </param>
    /// <param name="lpmii">
    /// A pointer to a <see cref="MenuItemInfo"/> structure that specifies the information to
    /// retrieve and receives information about the menu item.
    /// Note that you must set the <c>cbSize</c> member to <see langword="sizeof"/>(<see cref="MenuItemInfo"/>) before
    /// calling this function.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, use the <see cref="Kernel32.GetLastError"/> function.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// To retrieve a menu item of type <see cref="MFT.String"/>, first find
    /// the size of the string by setting the <c>dwTypeData</c> member
    /// of <see cref="MenuItemInfo"/> to <c>NULL</c> and then calling <c>GetMenuItemInfo</c>.
    /// The value of <c>cch</c>+1 is the size needed. Then allocate a
    /// buffer of this size, place the pointer to the buffer in
    /// <c>dwTypeData</c>, increment <c>cch</c> by one, and then call <c>GetMenuItemInfo</c>
    /// once again to fill the buffer with the string.
    /// </para>
    /// <para>
    /// If the retrieved menu item is of some other type, then <c>GetMenuItemInfo</c> sets the
    /// <c>dwTypeData</c> member to a value whose type is specified
    /// by the <c>fTypefType</c> member and sets cch to 0.
    /// </para>
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetMenuItemInfoW(
      [In] HMENU hmenu,
      [In] UINT item,
      [In] BOOL fByPosition,
      [In, Out] Forms.MenuItemInfo* lpmii
    );

    /// <summary>
    /// Determines the number of items in the specified menu.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the menu to be examined.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value specifies the number of items in the menu.
    /// </para>
    /// <para>
    /// If the function fails, the return value is -1.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern int GetMenuItemCount(
      [In, Optional] HMENU hMenu
    );

    /// <summary>
    /// Enables, disables, or grays the specified menu item.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the menu.
    /// </param>
    /// <param name="uIDEnableItem">
    /// The menu item to be enabled, disabled, or grayed, as determined by the uEnable parameter.
    /// This parameter specifies an item in a menu bar, menu, or submenu.
    /// </param>
    /// <param name="uEnable"></param>
    /// <returns>
    /// The return value specifies the previous state of the menu item
    /// (it is either MF_DISABLED, MF_ENABLED, or MF_GRAYED).
    /// If the menu item does not exist, the return value is -1.
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern BOOL EnableMenuItem(
      [In] HMENU hMenu,
      [In] UINT uIDEnableItem,
      [In] UINT uEnable
    );

    /// <summary>
    /// Retrieves the menu item identifier of a menu item located at the specified position in a menu.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the menu that contains the item whose identifier is to be retrieved.
    /// </param>
    /// <param name="nPos">
    /// The zero-based relative position of the menu item whose identifier is to be retrieved.
    /// </param>
    /// <returns>
    /// The return value is the identifier of the specified menu item.
    /// If the menu item identifier is <c>NULL</c> or if the specified item opens a submenu, the return value is -1.
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern UINT GetMenuItemID(
      [In] HMENU hMenu,
      [In] int nPos
    );

    /// <summary>
    /// Sets information for a specified menu.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to a menu.
    /// </param>
    /// <param name="lpmi">
    /// A pointer to a <see cref="MENUINFO"/> structure for the menu.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL SetMenuInfo(
      [In] HMENU hMenu,
      [In] MENUINFO* lpmi
    );

    /// <summary>
    /// Retrieves information about a specified menu.
    /// </summary>
    /// <param name="hMenu">
    /// A handle on a menu.
    /// </param>
    /// <param name="lpmi">
    /// A pointer to a <see cref="MENUINFO"/> structure containing information for the menu.
    /// Note that you must set the <c>cbSize</c> member to <see langword="sizeof"/>(<see cref="MENUINFO"/>) before calling this function.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetMenuInfo(
      [In] HMENU hMenu,
      [In, Out] MENUINFO* lpmi
    );

    /// <summary>
    /// Changes information about a menu item.
    /// </summary>
    /// <param name="hmenu">
    /// A handle to the menu that contains the menu item.
    /// </param>
    /// <param name="item">
    /// The identifier or position of the menu item to change.
    /// The meaning of this parameter depends on the value of <paramref name="fByPosition"/>.
    /// </param>
    /// <param name="lpmii">
    /// A pointer to a <see cref="MenuItemInfo"/> structure that contains information
    /// about the menu item and specifies which menu item attributes to change.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, use the <see cref="Kernel32.GetLastError"/> function.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// The application must call the <see cref="DrawMenuBar"/> function whenever
    /// a menu changes, whether the menu is in a displayed window.
    /// </para>
    /// <para>
    /// In order for keyboard accelerators to work with bitmap or
    /// owner-drawn menu items, the owner of the menu must process
    /// the <see cref="WindowMessage.WM_MENUCHAR"/> message. See
    /// <see href="https://learn.microsoft.com/en-us/windows/desktop/menurc/using-menus">Owner-Drawn Menus and the <c>WM_MENUCHAR</c> Message</see> for more information.
    /// </para>
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL SetMenuItemInfoW(
      [In] HMENU hmenu,
      [In] UINT item,
           BOOL fByPosition,
      [In] Forms.MenuItemInfo* lpmii
    );

    /// <summary>
    /// Redraws the menu bar of the specified window.
    /// If the menu bar changes after the system has created the window,
    /// this function must be called to draw the changed menu bar.
    /// </summary>
    /// <param name="hWnd">
    /// A handle to the window whose menu bar is to be redrawn.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern BOOL DrawMenuBar(
      [In] HWND hWnd
    );

    /// <summary>
    /// Appends a new item to the end of the specified menu bar, drop-down menu, submenu, or shortcut menu.
    /// You can use this function to specify the content, appearance, and behavior of the menu item.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the menu bar, drop-down menu, submenu, or shortcut menu to be changed.
    /// </param>
    /// <param name="uFlags">
    /// Controls the appearance and behavior of the new menu item.
    /// See <see cref="MenuFlags"/>
    /// </param>
    /// <param name="uIDNewItem">
    /// The identifier of the new menu item or, if the <paramref name="uFlags"/> parameter
    /// is set to <c>MF_POPUP</c>, a handle to the drop-down menu or submenu.
    /// </param>
    /// <param name="lpNewItem">
    /// The content of the new menu item.
    /// The interpretation of <paramref name="lpNewItem"/> depends on whether
    /// the <paramref name="uFlags"/> parameter includes the following values.
    /// <list type="table">
    /// <item>
    /// <term>
    /// <see cref="MenuFlags.Bitmap"/>
    /// </term>
    /// <description>
    /// Contains a bitmap handle.
    /// </description>
    /// </item>
    /// <item>
    /// <term>
    /// <see cref="MenuFlags.OwnerDraw"/>
    /// </term>
    /// <description>
    /// Contains an application-supplied value that can be used to
    /// maintain additional data related to the menu item.
    /// The value is in the <c>itemData</c> member of the structure pointed
    /// by the <c>lParam</c> parameter of the <see cref="WindowMessage.WM_MEASUREITEM"/> or <see cref="WindowMessage.WM_DRAWITEM"/>
    /// message sent when the menu is created or its appearance is updated.
    /// </description>
    /// </item>
    /// <item>
    /// <term>
    /// <see cref="MenuFlags.String"/>
    /// </term>
    /// <description>
    /// Contains a pointer to a null-terminated string.
    /// </description>
    /// </item>
    /// </list>
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// The application must call the <see cref="DrawMenuBar"/> function whenever a menu changes,
    /// whether the menu is in a displayed window.
    /// </para>
    /// <para>
    /// To get keyboard accelerators to work with bitmap or owner-drawn
    /// menu items, the owner of the menu must process the <see cref="WindowMessage.WM_MENUCHAR"/> message.
    /// For more information, see
    /// <see href="https://learn.microsoft.com/en-us/windows/desktop/menurc/using-menus">Owner-Drawn Menus and the <c>WM_MENUCHAR</c> Message</see>.
    /// </para>
    /// <para>
    /// The following groups of flags cannot be used together:
    /// <list type="bullet">
    /// <item>
    /// <see cref="MenuFlags.Bitmap"/>, <see cref="MenuFlags.String"/>, and <see cref="MenuFlags.OwnerDraw"/>
    /// </item>
    /// <item>
    /// <see cref="MenuFlags.Checked"/> and <see cref="MenuFlags.Unchecked"/>
    /// </item>
    /// <item>
    /// <see cref="MenuFlags.Disabled"/>, <see cref="MenuFlags.Enabled"/>, and <see cref="MenuFlags.Grayed"/>
    /// </item>
    /// <item>
    /// <see cref="MenuFlags.MenuBarBreak"/> and <see cref="MenuFlags.MenuBreak"/>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL AppendMenuW(
      [In] HMENU hMenu,
      [In] UINT uFlags,
      [In] UINT_PTR uIDNewItem,
      [In, Optional] WCHAR* lpNewItem
    );

    /// <summary>
    /// Inserts a new menu item at the specified position in a menu.
    /// </summary>
    /// <param name="hmenu">
    /// A handle to the menu in which the new menu item is inserted.
    /// </param>
    /// <param name="item">
    /// The identifier or position of the menu item before which to insert the new item.
    /// The meaning of this parameter depends on the value of <paramref name="fByPosition"/>.
    /// </param>
    /// <param name="fByPosition">
    /// Controls the meaning of item. If this parameter is <c>FALSE</c>, item is a menu item identifier.
    /// Otherwise, it is a menu item position.
    /// See <see href="https://learn.microsoft.com/en-us/windows/desktop/menurc/about-menus">Accessing Menu Items Programmatically</see> for more information.
    /// </param>
    /// <param name="lpmi">
    /// A pointer to a <see cref="MenuItemInfo"/> structure that contains information about the new menu item.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, use the <see cref="Kernel32.GetLastError"/> function.
    /// </para>
    /// </returns>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern unsafe BOOL InsertMenuItemW(
      [In] HMENU hmenu,
      [In] UINT item,
      [In] BOOL fByPosition,
      [In] Forms.MenuItemInfo* lpmi
    );

    /// <summary>
    /// Destroys the specified menu and frees any memory that the menu occupies.
    /// </summary>
    /// <param name="hMenu">
    /// A handle to the menu to be destroyed.
    /// </param>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is nonzero.
    /// </para>
    /// <para>
    /// If the function fails, the return value is zero.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// Before closing, an application must use the <c>DestroyMenu</c> function to destroy
    /// a menu not assigned to a window. A menu that is assigned to a window is
    /// automatically destroyed when the application closes.
    /// </para>
    /// <para>
    /// <c>DestroyMenu</c> is recursive, that is, it will destroy the menu and all its submenus.
    /// </para>
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern BOOL DestroyMenu(
      [In] HMENU hMenu
    );

    /// <summary>
    /// Creates a menu. The menu is initially empty,
    /// but it can be filled with menu items
    /// by using the <see cref="InsertMenuItemW"/>, <see cref="AppendMenuW"/>,
    /// and <see cref="InsertMenuW"/> functions.
    /// </summary>
    /// <returns>
    /// <para>
    /// If the function succeeds, the return value is a handle to the newly created menu.
    /// </para>
    /// <para>
    /// If the function fails, the return value is <c>NULL</c>.
    /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// Resources associated with a menu that is assigned to a window are freed automatically.
    /// If the menu is not assigned to a window, an application must
    /// free system resources associated with the menu before closing.
    /// An application frees menu resources by calling the <see cref="DestroyMenu"/> function.
    /// </remarks>
    [DllImport("User32.dll", SetLastError = true)]
    public static extern HMENU CreateMenu();
}
