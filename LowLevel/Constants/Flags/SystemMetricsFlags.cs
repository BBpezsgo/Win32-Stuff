﻿namespace Win32;

public enum SystemMetricsFlags : int
{
    /// <summary>
    /// The width of the screen of the primary display monitor, in pixels.
    /// This is the same value obtained by calling
    /// GetDeviceCaps as follows: GetDeviceCaps(hdcPrimaryMonitor, HORZRES).
    /// </summary>
    CXSCREEN = 0,
    /// <summary>
    /// The height of the screen of the primary display monitor, in pixels.
    /// This is the same value obtained by calling
    /// GetDeviceCaps as follows: GetDeviceCaps(hdcPrimaryMonitor, VERTRES).
    /// </summary>
    CYSCREEN = 1,
    /// <summary>
    /// The width of a vertical scroll bar, in pixels.
    /// </summary>
    CXVSCROLL = 2,
    /// <summary>
    /// The height of a horizontal scroll bar, in pixels.
    /// </summary>
    CYHSCROLL = 3,
    /// <summary>
    /// The height of a caption area, in pixels.
    /// </summary>
    CYCAPTION = 4,
    /// <summary>
    /// The width of a window border, in pixels. This is equivalent to the
    /// SM_CXEDGE value for windows with the 3-D look.
    /// </summary>
    CXBORDER = 5,
    /// <summary>
    /// The height of a window border, in pixels. This is equivalent to the SM_CYEDGE value for
    /// windows with the 3-D look.
    /// </summary>
    CYBORDER = 6,
    /// <summary>
    /// The thickness of the frame around the perimeter of a window that has
    /// a caption but is not sizable, in pixels. SM_CXFIXEDFRAME is the
    /// height of the horizontal border, and SM_CYFIXEDFRAME is the width
    /// of the vertical border. This value is the same as SM_CXDLGFRAME.
    /// </summary>
    CXFIXEDFRAME = 7,
    /// <summary>
    /// The thickness of the frame around the perimeter of a window that
    /// has a caption but is not sizable, in pixels. SM_CXFIXEDFRAME is the height of the horizontal border, and SM_CYFIXEDFRAME is the width of the vertical border. This value is the same as SM_CYDLGFRAME.
    /// </summary>
    CYFIXEDFRAME = 8,
    /// <summary>
    /// The height of the thumb box in a vertical scroll bar, in pixels.
    /// </summary>
    CYVTHUMB = 9,
    /// <summary>
    /// The width of the thumb box in a horizontal scroll bar, in pixels.
    /// </summary>
    CXHTHUMB = 10,
    /// <summary>
    /// The system large width of an icon, in pixels. The LoadIcon
    /// function can load only icons with the dimensions that SM_CXICON and SM_CYICON specifies.
    /// See <see href="https://learn.microsoft.com/en-us/windows/win32/menurc/about-icons#icon-sizes">Icon Sizes</see> for more info.
    /// </summary>
    CXICON = 11,
    /// <summary>
    /// The system large height of an icon, in pixels. The LoadIcon
    /// function can load only icons with the dimensions that SM_CXICON
    /// and SM_CYICON specifies. See <see href="https://learn.microsoft.com/en-us/windows/win32/menurc/about-icons#icon-sizes">Icon Sizes</see> for more info.
    /// </summary>
    CYICON = 12,
    /// <summary>
    /// The nominal width of a cursor, in pixels.
    /// </summary>
    CXCURSOR = 13,
    /// <summary>
    /// The nominal height of a cursor, in pixels.
    /// </summary>
    CYCURSOR = 14,
    /// <summary>
    /// The height of a single-line menu bar, in pixels.
    /// </summary>
    CYMENU = 15,
    /// <summary>
    /// The width of the client area for a full-screen window on the
    /// primary display monitor, in pixels. To get the coordinates of
    /// the portion of the screen that is not obscured by the system
    /// taskbar or by application desktop
    /// toolbars, call the SystemParametersInfo function with the SPI_GETWORKAREA value.
    /// </summary>
    CXFULLSCREEN = 16,
    /// <summary>
    /// The height of the client area for a full-screen window on the primary display monitor, in pixels. To get the coordinates of the portion of the screen not obscured by the system taskbar or by application desktop toolbars, call the SystemParametersInfo function with the SPI_GETWORKAREA value.
    /// </summary>
    CYFULLSCREEN = 17,
    /// <summary>
    /// For double byte character set versions of the system, this
    /// is the height of the Kanji window at the bottom of the screen, in pixels.
    /// </summary>
    CYKANJIWINDOW = 18,
    /// <summary>
    /// Nonzero if a mouse is installed; otherwise, 0. This value is rarely zero,
    /// because of support for virtual mice and because some systems detect the
    /// presence of the port instead of the presence of a mouse.
    /// </summary>
    MOUSEPRESENT = 19,
    /// <summary>
    /// The height of the arrow bitmap on a vertical scroll bar, in pixels.
    /// </summary>
    CYVSCROLL = 20,
    /// <summary>
    /// The width of the arrow bitmap on a horizontal scroll bar, in pixels.
    /// </summary>
    CXHSCROLL = 21,
    /// <summary>
    /// Nonzero if the debug version of User. exe is installed, otherwise, 0.
    /// </summary>
    DEBUG = 22,
    /// <summary>
    /// Nonzero if the meanings of the left and right mouse buttons are swapped;
    /// otherwise, 0.
    /// </summary>
    SWAPBUTTON = 23,
    /// <summary>
    /// The minimum width of a window, in pixels.
    /// </summary>
    CXMIN = 28,
    /// <summary>
    /// The minimum height of a window, in pixels.
    /// </summary>
    CYMIN = 29,
    /// <summary>
    /// The width of a button in a window caption or title bar, in pixels.
    /// </summary>
    CXSIZE = 30,
    /// <summary>
    /// The height of a button in a window caption or title bar, in pixels.
    /// </summary>
    CYSIZE = 31,
    /// <summary>
    /// The thickness of the sizing border around the perimeter of a window that can be resized,
    /// in pixels. SM_CXSIZEFRAME is the width of the horizontal border,
    /// and SM_CYSIZEFRAME is the height of the vertical border.
    /// This value is the same as SM_CXFRAME.
    /// </summary>
    CXSIZEFRAME = 32,
    /// <summary>
    /// The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
    /// SM_CXSIZEFRAME is the width of the horizontal border,
    /// and SM_CYSIZEFRAME is the height of the vertical border.
    /// This value is the same as SM_CYFRAME.
    /// </summary>
    CYSIZEFRAME = 33,
    /// <summary>
    /// The minimum tracking width of a window, in pixels.
    /// The user cannot drag the window frame to a size smaller than these dimensions. A window can override this value by processing the WM_GETMINMAXINFO message.
    /// </summary>
    CXMINTRACK = 34,
    /// <summary>
    /// The minimum tracking height of a window, in pixels.
    /// The user cannot drag the window frame to a size smaller than these dimensions.
    /// A window can override this value by processing the WM_GETMINMAXINFO message.
    /// </summary>
    CYMINTRACK = 35,
    /// <summary>
    /// The width of the rectangle around the location of a first click in
    /// a double-click sequence, in pixels. The second click must
    /// occur within the rectangle that is defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK for the system to consider the two clicks a double-click. The two clicks must also occur within a specified time. To set the width of the double-click rectangle, call SystemParametersInfo with SPI_SETDOUBLECLKWIDTH.
    /// </summary>
    CXDOUBLECLK = 36,
    /// <summary>
    /// The height of the rectangle around the location of a first click in
    /// a double-click sequence, in pixels. The second click must
    /// occur within the rectangle defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK
    /// for the system to consider the two clicks a double-click.
    /// The two clicks must also occur within a specified time. To set the
    /// height of the double-click rectangle, call SystemParametersInfo with
    /// SPI_SETDOUBLECLKHEIGHT.
    /// </summary>
    CYDOUBLECLK = 37,
    /// <summary>
    /// The width of a grid cell for items in large icon view, in pixels.
    /// Each item fits into a rectangle of size SM_CXICONSPACING by
    /// SM_CYICONSPACING when arranged. This value is always greater than or equal to SM_CXICON.
    /// </summary>
    CXICONSPACING = 38,
    /// <summary>
    /// The height of a grid cell for items in large icon view, in
    /// pixels. Each item fits into a rectangle of size SM_CXICONSPACING
    /// by SM_CYICONSPACING when arranged. This value is always greater than or equal to SM_CYICON.
    /// </summary>
    CYICONSPACING = 39,
    /// <summary>
    /// Nonzero if drop-down menus are right-aligned with the corresponding
    /// menu-bar item, 0 if the menus are left-aligned.
    /// </summary>
    MENUDROPALIGNMENT = 40,
    /// <summary>
    /// Nonzero if the Microsoft Windows for Pen computing extensions are installed;
    /// zero otherwise.
    /// </summary>
    PENWINDOWS = 41,
    /// <summary>
    /// Nonzero if User32. dll supports DBCS, otherwise, 0.
    /// </summary>
    DBCSENABLED = 42,
    /// <summary>
    /// The number of buttons on a mouse, or zero if no mouse is installed.
    /// </summary>
    CMOUSEBUTTONS = 43,
    /// <summary>
    /// This system metric should be ignored; it always returns 0.
    /// </summary>
    SECURE = 44,
    /// <summary>
    /// The width of a 3-D border, in pixels. This metric is the 3-D counterpart of SM_CXBORDER.
    /// </summary>
    CXEDGE = 45,
    /// <summary>
    /// The height of a 3-D border, in pixels. This is the 3-D counterpart
    /// of SM_CYBORDER.
    /// </summary>
    CYEDGE = 46,
    /// <summary>
    /// The width of a grid cell for a minimized window, in pixels.
    /// Each minimized window fits into a rectangle this size when arranged.
    /// This value is always greater than or equal to SM_CXMINIMIZED.
    /// </summary>
    CXMINSPACING = 47,
    /// <summary>
    /// The height of a grid cell for a minimized window, in pixels.
    /// Each minimized window fits into a rectangle this size when arranged.
    /// This value is always greater than or equal to SM_CYMINIMIZED.
    /// </summary>
    CYMINSPACING = 48,
    /// <summary>
    /// The system small width of an icon, in pixels.
    /// Small icons typically appear in window captions and in small icon view.
    /// See <see href="https://learn.microsoft.com/en-us/windows/win32/menurc/about-icons#icon-sizes">Icon Sizes</see> for more info.
    /// </summary>
    CXSMICON = 49,
    /// <summary>
    /// The system small height of an icon, in pixels.
    /// Small icons typically appear in window captions and in small icon view.
    /// See <see href="https://learn.microsoft.com/en-us/windows/win32/menurc/about-icons#icon-sizes">Icon Sizes</see> for more info.
    /// </summary>
    CYSMICON = 50,
    /// <summary>
    /// The height of a small caption, in pixels.
    /// </summary>
    CYSMCAPTION = 51,
    /// <summary>
    /// The width of small caption buttons, in pixels.
    /// </summary>
    CXSMSIZE = 52,
    /// <summary>
    /// The height of small caption buttons, in pixels.
    /// </summary>
    CYSMSIZE = 53,
    /// <summary>
    /// The width of menu bar buttons, such as the child window close button
    /// that is used in the multiple document interface, in pixels.
    /// </summary>
    CXMENUSIZE = 54,
    /// <summary>
    /// The height of menu bar buttons, such as the child window close
    /// button that is used in the multiple document interface, in pixels.
    /// </summary>
    CYMENUSIZE = 55,
    /// <summary>
    /// The flags that specify how the system arranged minimized windows.
    /// For more information, see the Remarks section in this topic.
    /// </summary>
    ARRANGE = 56,
    /// <summary>
    /// The width of a minimized window, in pixels.
    /// </summary>
    CXMINIMIZED = 57,
    /// <summary>
    /// The height of a minimized window, in pixels.
    /// </summary>
    CYMINIMIZED = 58,
    /// <summary>
    /// The default maximum width of a window that has a caption and sizing
    /// borders, in pixels. This metric refers to the entire desktop.
    /// The user cannot drag the window frame to a size larger than these dimensions.
    /// A window can override this value by processing the WM_GETMINMAXINFO message.
    /// </summary>
    CXMAXTRACK = 59,
    /// <summary>
    /// The default maximum height of a window that has a caption and
    /// sizing borders, in pixels. This metric refers to the
    /// entire desktop. The user cannot drag the window frame to a size
    /// larger than these dimensions.
    /// A window can override this value by processing the
    /// WM_GETMINMAXINFO message.
    /// </summary>
    CYMAXTRACK = 60,
    /// <summary>
    /// The default width, in pixels, of a maximized top-level window
    /// on the primary display monitor.
    /// </summary>
    CXMAXIMIZED = 61,
    /// <summary>
    /// The default height, in pixels, of a maximized top-level window
    /// on the primary display monitor.
    /// </summary>
    CYMAXIMIZED = 62,
    /// <summary>
    /// The least significant bit is set if a network is present; otherwise, it is cleared.
    /// The other bits are reserved for future use.
    /// </summary>
    NETWORK = 63,
    /// <summary>
    /// The value that specifies how the system is started:
    /// <list type="bullet">
    /// <item>
    /// 0 Normal boot
    /// </item>
    /// <item>
    /// 1 Fail-safe boot
    /// </item>
    /// <item>
    /// 2 Fail-safe with network boot
    /// </item>
    /// </list>
    /// A fail-safe boot(also called SafeBoot, Safe Mode, or Clean Boot)
    /// bypasses the user startup files.
    /// </summary>
    CLEANBOOT = 67,
    /// <summary>
    /// The number of pixels on either side of a mouse-down point that the
    /// mouse pointer can move before a drag operation begins. This allows
    /// the user to click and release the mouse button easily without unintentionally starting a drag operation. If this value is negative, it is subtracted from the left of the mouse-down point and added to the right of it.
    /// </summary>
    CXDRAG = 68,
    /// <summary>
    /// The number of pixels above and below a mouse-down point that the mouse pointer can move before a drag operation begins. This allows the user to click and release the mouse button easily without unintentionally starting a drag operation. If this value is negative, it is subtracted from above the mouse-down point and added below it.
    /// </summary>
    CYDRAG = 69,
    /// <summary>
    /// Nonzero if the user requires an application to present information visually
    /// in situations where it would otherwise present the information only in audible
    /// form; otherwise, 0.
    /// </summary>
    SHOWSOUNDS = 70,
    /// <summary>
    /// The width of the default menu check-mark bitmap, in pixels.
    /// </summary>
    CXMENUCHECK = 71,
    /// <summary>
    /// The height of the default menu check-mark bitmap, in pixels.
    /// </summary>
    CYMENUCHECK = 72,
    /// <summary>
    /// Nonzero if the computer has a low-end(slow) processor; otherwise, 0.
    /// </summary>
    SLOWMACHINE = 73,
    /// <summary>
    /// Nonzero if the system is enabled for Hebrew and Arabic languages, 0 if not.
    /// </summary>
    MIDEASTENABLED = 74,
    /// <summary>
    /// Nonzero if a mouse with a vertical scroll wheel is installed; otherwise 0.
    /// </summary>
    MOUSEWHEELPRESENT = 75,
    /// <summary>
    /// The coordinates for the left side of the virtual screen. The virtual screen is the bounding
    /// rectangle of all display monitors. The SM_CXVIRTUALSCREEN metric is the width
    /// of the virtual screen.
    /// </summary>
    XVIRTUALSCREEN = 76,
    /// <summary>
    /// The coordinates for the top of the virtual screen. The virtual screen is the bounding
    /// rectangle of all display monitors. The SM_CYVIRTUALSCREEN metric is the height
    /// of the virtual screen.
    /// </summary>
    YVIRTUALSCREEN = 77,
    /// <summary>
    /// The width of the virtual screen, in pixels.
    /// The virtual screen is the bounding rectangle of all display monitors.
    /// The SM_XVIRTUALSCREEN metric is the coordinates for the left side of the virtual screen.
    /// </summary>
    CXVIRTUALSCREEN = 78,
    /// <summary>
    /// The height of the virtual screen, in pixels.
    /// The virtual screen is the bounding rectangle of all display monitors.
    /// The SM_YVIRTUALSCREEN metric is the coordinates for the top of the virtual screen.
    /// </summary>
    CYVIRTUALSCREEN = 79,
    /// <summary>
    /// The number of display monitors on a desktop. For more information,
    /// see the Remarks section in this topic.
    /// </summary>
    CMONITORS = 80,
    /// <summary>
    /// Nonzero if all the display monitors have the same color format, otherwise, 0.
    /// Two displays can have the same bit depth, but different color formats.
    /// For example, the red, green, and blue pixels can be encoded with different numbers of bits,
    /// or those bits can be located in different places in a pixel color value.
    /// </summary>
    SAMEDISPLAYFORMAT = 81,
    /// <summary>
    /// Nonzero if Input Method Manager/Input Method Editor
    /// features are enabled; otherwise, 0. SM_IMMENABLED indicates
    /// whether the system is ready to use a Unicode-based IME on a Unicode application.
    /// To ensure that a language-dependent IME works, check SM_DBCSENABLED and the system
    /// ANSI code page. Otherwise the ANSI-to-Unicode conversion may not be performed
    /// correctly, or some components like fonts or registry settings may not be present.
    /// </summary>
    IMMENABLED = 82,
    /// <summary>
    /// The width of the left and right edges of the focus rectangle that
    /// the DrawFocusRect draws. This value is in pixels.
    /// Windows 2000:  This value is not supported.
    /// </summary>
    CXFOCUSBORDER = 83,
    /// <summary>
    /// The height of the top and bottom edges of the focus rectangle drawn by
    /// DrawFocusRect. This value is in pixels. Windows 2000:  This value is not supported.
    /// </summary>
    CYFOCUSBORDER = 84,
    /// <summary>
    /// Nonzero if the current operating system is the Windows XP Tablet PC edition or if the current
    /// operating system is Windows Vista or Windows 7 and the Tablet PC Input service is started;
    /// otherwise, 0. The SM_DIGITIZER setting indicates the type of digitizer input supported by a device running Windows 7 or Windows Server 2008 R2. For more information, see Remarks.
    /// </summary>
    TABLETPC = 86,
    /// <summary>
    /// Nonzero if the current operating system is the Windows XP, Media Center Edition, 0 if not.
    /// </summary>
    MEDIACENTER = 87,
    /// <summary>
    /// Nonzero if the current operating system is Windows 7 Starter Edition,
    /// Windows Vista Starter, or Windows XP Starter Edition; otherwise, 0.
    /// </summary>
    STARTER = 88,
    /// <summary>
    /// The build number if the system is Windows Server 2003 R2; otherwise, 0.
    /// </summary>
    SERVERR2 = 89,
    /// <summary>
    /// Nonzero if a mouse with a horizontal scroll wheel is installed; otherwise 0.
    /// </summary>
    MOUSEHORIZONTALWHEELPRESENT = 91,
    /// <summary>
    /// The amount of border padding for captioned windows, in pixels.
    /// Windows XP/2000:  This value is not supported.
    /// </summary>
    CXPADDEDBORDER = 92,
    /// <summary>
    /// Nonzero if the current operating system is
    /// Windows 7 or Windows Server 2008 R2 and the Tablet PC Input
    /// service is started; otherwise, 0. The return value is a bitmask
    /// that specifies the type of digitizer input supported by the device.
    /// For more information, see Remarks.
    /// Windows Server 2008, Windows Vista and Windows XP/2000:  This value is not supported.
    /// </summary>
    DIGITIZER = 94,
    /// <summary>
    /// Nonzero if there are digitizers in the system; otherwise, 0. SM_MAXIMUMTOUCHES
    /// returns the aggregate maximum of the maximum number of contacts supported by every
    /// digitizer in the system. If the system has only single-touch digitizers,
    /// the return value is 1. If the system has multi-touch digitizers,
    /// the return value is the number of simultaneous contacts the hardware can provide.
    /// Windows Server 2008, Windows Vista and Windows XP/2000:  This value is not supported.
    /// </summary>
    MAXIMUMTOUCHES = 95,
    /// <summary>
    /// This system metric is used in a Terminal Services environment. If the calling
    /// process is associated with a Terminal Services client session, the return value is nonzero.
    /// If the calling process is associated with the Terminal Services console session,
    /// the return value is 0. Windows Server 2003 and Windows XP:  The console session
    /// is not necessarily the physical console. For more information, see WTSGetActiveConsoleSessionId.
    /// </summary>
    REMOTESESSION = 0x1000,
    /// <summary>
    /// Nonzero if the current session is shutting down; otherwise, 0.
    /// Windows 2000:  This value is not supported.
    /// </summary>
    SHUTTINGDOWN = 0x2000,
    /// <summary>
    /// This system metric is used in a Terminal Services environment to determine if
    /// the current Terminal Server session is being remotely controlled. Its value is nonzero if
    /// the current session is remotely controlled; otherwise, 0. You can use terminal
    /// services management tools such as Terminal Services Manager(tsadmin. msc) and shadow.
    /// exe to control a remote session. When a session is being remotely controlled, another
    /// user can view the contents of that session and potentially interact with it.
    /// </summary>
    REMOTECONTROL = 0x2001,
    /// <summary>
    /// Reflects the state of the laptop or slate mode, 0 for Slate Mode
    /// and non-zero otherwise. When this system metric changes, the system sends a broadcast message via WM_SETTINGCHANGE with ""ConvertibleSlateMode"" in the LPARAM. Note that this system metric doesn't apply to desktop PCs. In that case, use GetAutoRotationState.
    /// </summary>
    CONVERTIBLESLATEMODE = 0x2003,
    /// <summary>
    /// Reflects the state of the docking mode, 0 for Undocked Mode and non-zero otherwise.
    /// When this system metric changes, the system sends a broadcast message via <see cref="WM_SETTINGCHANGE"/> with ""SystemDockMode"" in the LPARAM.
    /// </summary>
    SYSTEMDOCKED = 0x2004,
}
