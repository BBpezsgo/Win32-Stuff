using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static partial class User32
    {
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL SetLayeredWindowAttributes(
          [In] HWND hwnd,
          [In] COLORREF crKey,
          [In] BYTE bAlpha,
          [In] DWORD dwFlags
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe BOOL UpdateLayeredWindow(
          [In] HWND hWnd,
          [In, Optional] HDC hdcDst,
          [In, Optional] POINT* pptDst,
          [In, Optional] SIZE* psize,
          [In, Optional] HDC hdcSrc,
          [In, Optional] POINT* pptSrc,
          [In] COLORREF crKey,
          [In, Optional] BLENDFUNCTION* pblend,
          [In] DWORD dwFlags
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe UINT RealGetWindowClassW(
          [In] HWND hwnd,
          [Out] WCHAR* ptszClassName,
          [In] UINT cchClassNameMax
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL EndTask(
          [In] HWND hWnd,
          [In] BOOL fShutDown,
          [In] BOOL fForce
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL SetForegroundWindow(
          [In] HWND hWnd
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND GetFocus();

        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND SetFocus(
          [In, Optional] HWND hWnd
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND SetActiveWindow(
          [In] HWND hWnd
        );

        /// <summary>
        /// Retrieves a handle to a window whose class name and window name match
        /// the specified strings. The function searches child windows,
        /// beginning with the one following the specified child window.
        /// This function does not perform a case-sensitive search.
        /// </summary>
        /// <param name="hWndParent">
        /// <para>
        /// A handle to the parent window whose child windows are to be searched.
        /// </para>
        /// <para>
        /// If <paramref name="hWndParent"/> is <c>NULL</c>, the function
        /// uses the desktop window as the parent window.
        /// The function searches among windows that are child windows of the desktop.
        /// </para>
        /// <para>
        /// If <paramref name="hWndParent"/> is <see cref="HWND_MESSAGE"/>,
        /// the function searches all message-only windows.
        /// </para>
        /// </param>
        /// <param name="hWndChildAfter">
        /// <para>
        /// A handle to a child window. The search begins with the next
        /// child window in the Z order. The child window must be a
        /// direct child window of <paramref name="hWndParent"/>, not
        /// just a descendant window.
        /// </para>
        /// <para>
        /// If <paramref name="hWndChildAfter"/> is <c>NULL</c>, the search begins with
        /// the first child window of <paramref name="hWndParent"/>.
        /// </para>
        /// <para>
        /// Note that if both <paramref name="hWndParent"/> and <paramref name="hWndChildAfter"/> are <c>NULL</c>,
        /// the function searches all top-level and message-only windows.
        /// </para>
        /// </param>
        /// <param name="lpszClass">
        /// <para>
        /// The class name or a class atom created by a previous call to the
        /// <see cref="RegisterClassW"/> or <see cref="RegisterClassExW"/> function. The atom must be placed
        /// in the low-order word of <paramref name="lpszClass"/>; the high-order word must be zero.
        /// </para>
        /// <para>
        /// If <paramref name="lpszClass"/> is a string, it specifies the window class name.
        /// The class name can be any name registered with <see cref="RegisterClassW"/>
        /// or <see cref="RegisterClassExW"/>, or any of the predefined control-class
        /// names, or it can be <see cref="Macros.MAKEINTATOM"/>(<c>0x8000</c>). In this latter case,
        /// 0x8000 is the atom for a menu class. For more information,
        /// see the Remarks section of this topic.
        /// </para>
        /// </param>
        /// <param name="lpszWindow">
        /// The window name (the window's title). If this parameter is <c>NULL</c>, all window names match.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is a handle to
        /// the window that has the specified class and window names.
        /// </para>
        /// <para>
        /// If the function fails, the return value is <c>NULL</c>.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe HWND FindWindowExA(
          [In, Optional] HWND hWndParent,
          [In, Optional] HWND hWndChildAfter,
          [In, Optional] WCHAR* lpszClass,
          [In, Optional] WCHAR* lpszWindow
        );

        /// <summary>
        /// <para>
        /// Retrieves a handle to the top-level window whose class name
        /// and window name match the specified strings. This function
        /// does not search child windows. This function does not perform
        /// a case-sensitive search.
        /// </para>
        /// <para>
        /// To search child windows, beginning with a specified
        /// child window, use the FindWindowEx function.
        /// </para>
        /// </summary>
        /// <param name="lpClassName">
        /// <para>
        /// The class name or a class atom created by a previous call to the <see cref="RegisterClassW"/> or
        /// <see cref="RegisterClassExW"/> function. The atom must be in the low-order word of
        /// <paramref name="lpClassName"/>; the high-order word must be zero.
        /// </para>
        /// <para>
        /// If <paramref name="lpClassName"/> points to a string, it specifies the window class name.
        /// The class name can be any name registered with <see cref="RegisterClassW"/> or
        /// <see cref="RegisterClassExW"/>, or any of the predefined control-class names.
        /// </para>
        /// <para>
        /// If <paramref name="lpClassName"/> is <c>NULL</c>, it finds any window whose title
        /// matches the <paramref name="lpWindowName"/> parameter.
        /// </para>
        /// </param>
        /// <param name="lpWindowName">
        /// The window name (the window's title). If this parameter is <c>NULL</c>, all window names match.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is a handle to the
        /// window that has the specified class name and window name.
        /// </para>
        /// <para>
        /// If the function fails, the return value is <c>NULL</c>.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// If the <paramref name="lpWindowName"/> parameter is not <c>NULL</c>,
        /// <c>FindWindow</c> calls the <see cref="GetWindowTextW"/> function to
        /// retrieve the window name for comparison. For a
        /// description of a potential problem that can arise,
        /// see the Remarks for <see cref="GetWindowTextW"/>.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe HWND FindWindowW(
          [In, Optional] WCHAR* lpClassName,
          [In, Optional] WCHAR* lpWindowName
        );

        /// <summary>
        /// Retrieves information about the specified window.
        /// </summary>
        /// <param name="hwnd">
        /// A handle to the window whose information is to be retrieved.
        /// </param>
        /// <param name="pwi">
        /// A pointer to a <see cref="WindowInfo"/> structure to receive the information.
        /// Note that you must set the <c>cbSize</c> member to sizeof(WINDOWINFO) before calling this function.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero. 
        /// </para>
        /// <para>
        /// To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe BOOL GetWindowInfo(
          [In] HWND hwnd,
          [In, Out] WindowInfo* pwi
        );

        /// <summary>
        /// <para>
        /// Changes an attribute of the specified window. The function also
        /// sets a value at the specified offset in the extra window memory.
        /// </para>
        /// <para>
        /// <b>Note:</b>
        /// To write code that is compatible with both 32-bit and 64-bit
        /// versions of Windows, use <c>SetWindowLongPtr</c>. When compiling for
        /// 32-bit Windows, <c>SetWindowLongPtr</c> is defined as a call to
        /// the <c>SetWindowLong</c> function.
        /// </para>
        /// </summary>
        /// <param name="hWnd">
        /// <para>
        /// A handle to the window and, indirectly, the class to which
        /// the window belongs. The <c>SetWindowLongPtr</c> function fails
        /// if the process that owns the window specified by the <paramref name="hWnd"/>
        /// parameter is at a higher process privilege in the UIPI
        /// hierarchy than the process the calling thread resides in.
        /// </para>
        /// <para>
        /// <b>Windows XP/2000:</b>
        /// The <c>SetWindowLongPtr</c> function fails if the window specified
        /// by the <paramref name="hWnd"/> parameter does not belong to
        /// the same process as the calling thread.
        /// </para>
        /// </param>
        /// <param name="nIndex">
        /// The zero-based offset to the value to be set.
        /// Valid values are in the range zero through the number
        /// of bytes of extra window memory, minus the size of a <see cref="LONG_PTR"/>.
        /// </param>
        /// <param name="dwNewLong">
        /// The replacement value.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is the previous value of the specified offset.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// <para>
        /// If the previous value is zero and the function succeeds,
        /// the return value is zero, but the function does not clear
        /// the last error information. To determine success or failure,
        /// clear the last error information by calling <see cref="Kernel32.SetLastError"/> with
        /// 0, then call <c>SetWindowLongPtr</c>. Function failure will be indicated
        /// by a return value of zero and a <see cref="Kernel32.GetLastError"/> result that is nonzero.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern LONG_PTR SetWindowLongPtrW(
          [In] HWND hWnd,
          [In] int nIndex,
          [In] LONG_PTR dwNewLong
        );

        /// <summary>
        /// <para>
        /// Retrieves information about the specified window.
        /// The function also retrieves the value at a
        /// specified offset into the extra window memory.
        /// </para>
        /// <para>
        /// <b>Note:</b>
        /// To write code that is compatible with both 32-bit
        /// and 64-bit versions of Windows, use <c>GetWindowLongPtr</c>.
        /// When compiling for 32-bit Windows, <c>GetWindowLongPtr</c>
        /// is defined as a call to the <c>GetWindowLong</c> function.
        /// </para>
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window and, indirectly, the class to which the window belongs.
        /// </param>
        /// <param name="nIndex">
        /// The zero-based offset to the value to be retrieved.
        /// Valid values are in the range zero through the number of bytes
        /// of extra window memory, minus the size of a <see cref="LONG_PTR"/>.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is the requested value.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// <para>
        /// If <see cref="SetWindowLongW"/> or <see cref="SetWindowLongPtrW"/> has not been called previously,
        /// <c>GetWindowLongPtrW</c> returns zero for values in the extra window or class memory.
        /// </para>
        /// </returns>
        /// <remarks>
        /// Reserve extra window memory by specifying a nonzero value in the <c>cbWndExtra</c> member
        /// of the <see cref="WNDCLASSEXW"/> structure used with the <see cref="RegisterClassExW"/> function.
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern LONG_PTR GetWindowLongPtrW(
          [In] HWND hWnd,
          [In] int nIndex
        );

        /// <summary>
        /// The <c>ScreenToClient</c> function converts the screen coordinates
        /// of a specified point on the screen to client-area coordinates.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose client area will be used for the conversion.
        /// </param>
        /// <param name="lpPoint">
        /// A pointer to a <see cref="POINT"/> structure that specifies the screen coordinates to be converted.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// The function uses the window identified by the <paramref name="hWnd"/> parameter and the
        /// screen coordinates given in the <see cref="POINT"/> structure to compute client coordinates.
        /// It then replaces the screen coordinates with the client coordinates.
        /// The new coordinates are relative to the upper-left corner of the
        /// specified window's client area.
        /// </para>
        /// <para>
        /// The <c>ScreenToClient</c> function assumes the specified point is in screen coordinates.
        /// </para>
        /// <para>
        /// All coordinates are in device units.
        /// </para>
        /// <para>
        /// Do not use <c>ScreenToClient</c> when in a mirroring situation, that is, when changing from
        /// left-to-right layout to right-to-left layout. Instead, use <see cref="MapWindowPoints"/>.
        /// For more information, see "Window Layout and Mirroring" in
        /// <see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/window-features">Window Features</see>.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe BOOL ScreenToClient(
          [In] HWND hWnd,
               POINT* lpPoint
        );

        /// <summary>
        /// The <c>ClientToScreen</c> function converts the client-area
        /// coordinates of a specified point to screen coordinates.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose client area is used for the conversion.
        /// </param>
        /// <param name="lpPoint">
        /// A pointer to a <see cref="POINT"/> structure that contains the client coordinates to be converted.
        /// The new screen coordinates are copied into this structure if the function succeeds.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// The <c>ClientToScreen</c> function replaces the client-area coordinates
        /// in the <see cref="POINT"/> structure with the screen coordinates. The screen
        /// coordinates are relative to the upper-left corner of the screen.
        /// Note, a screen-coordinate point that is above the window's client
        /// area has a negative y-coordinate. Similarly, a screen coordinate
        /// to the left of a client area has a negative x-coordinate.
        /// </para>
        /// <para>
        /// All coordinates are device coordinates.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe BOOL ClientToScreen(
          [In] HWND hWnd,
          [In, Out] POINT* lpPoint
        );

        /// <summary>
        /// Retrieves a handle to the child window at the specified point.
        /// The search is restricted to immediate child windows;
        /// grandchildren and deeper descendant windows are not searched.
        /// </summary>
        /// <param name="hwndParent">
        /// A handle to the window whose child is to be retrieved.
        /// </param>
        /// <param name="ptParentClientCoords">
        /// A <see cref="POINT"/> structure that defines the client
        /// coordinates of the point to be checked.
        /// </param>
        /// <returns>
        /// The return value is a handle to the child window that contains the specified point.
        /// </returns>
        /// <remarks>
        /// <c>RealChildWindowFromPoint</c> treats <c>HTTRANSPARENT</c> areas of a standard
        /// control differently from other areas of the control; it returns
        /// the child window behind a transparent part of a control.
        /// In contrast, <see cref="ChildWindowFromPoint"/> treats <c>HTTRANSPARENT</c> areas
        /// of a control the same as other areas. For example, if the point
        /// is in a transparent area of a groupbox, <c>RealChildWindowFromPoint</c>
        /// returns the child window behind a groupbox, whereas <see cref="ChildWindowFromPoint"/>
        /// returns the groupbox. However, both APIs return a static field, even
        /// though it, too, returns <c>HTTRANSPARENT</c>.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND RealChildWindowFromPoint(
          [In] HWND hwndParent,
          [In] POINT ptParentClientCoords
        );

        /// <summary>
        /// Determines which, if any, of the child windows belonging to the specified
        /// parent window contains the specified point. The function can ignore invisible,
        /// disabled, and transparent child windows. The search is restricted to
        /// immediate child windows. Grandchildren and deeper descendants are not searched.
        /// </summary>
        /// <param name="hwnd">
        /// A handle to the parent window.
        /// </param>
        /// <param name="pt">
        /// A structure that defines the client coordinates
        /// (relative to <paramref name="hwnd"/>) of the point to be checked.
        /// </param>
        /// <param name="flags">
        /// The child windows to be skipped.
        /// </param>
        /// <returns>
        /// The return value is a handle to the first child window that
        /// contains the point and meets the criteria specified by <paramref name="flags"/>.
        /// If the point is within the parent window but not within any
        /// child window that meets the criteria, the return value is a
        /// handle to the parent window. If the point lies outside the
        /// parent window or if the function fails, the return value is <c>NULL</c>.
        /// </returns>
        /// <remarks>
        /// The system maintains an internal list that contains the handles of the
        /// child windows associated with a parent window. The order of the
        /// handles in the list depends on the Z order of the child windows.
        /// If more than one child window contains the specified point, the
        /// system returns a handle to the first window in the list that contains
        /// the point and meets the criteria specified by <paramref name="flags"/>.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND ChildWindowFromPointEx(
          [In] HWND hwnd,
          [In] POINT pt,
          [In] ChildWindowFromPointFlags flags
        );

        /// <summary>
        /// <para>
        /// Determines which, if any, of the child windows belonging to a parent
        /// window contains the specified point. The search is restricted to
        /// immediate child windows. Grandchildren, and
        /// deeper descendant windows are not searched.
        /// </para>
        /// <para>
        /// To skip certain child windows, use the <see cref="ChildWindowFromPointEx"/> function.
        /// </para>
        /// </summary>
        /// <param name="hWndParent">
        /// A handle to the parent window.
        /// </param>
        /// <param name="Point">
        /// A structure that defines the client coordinates,
        /// relative to <paramref name="hWndParent"/>, of the point to be checked.
        /// </param>
        /// <returns>
        /// The return value is a handle to the child window that contains the point,
        /// even if the child window is hidden or disabled.
        /// If the point lies outside the parent window, the return value is <c>NULL</c>.
        /// If the point is within the parent window but not within any child window,
        /// the return value is a handle to the parent window.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The system maintains an internal list, containing the handles of the
        /// child windows associated with a parent window. The order of the handles
        /// in the list depends on the Z order of the child windows. If more than
        /// one child window contains the specified point, the system returns a
        /// handle to the first window in the list that contains the point.
        /// </para>
        /// <para>
        /// <c>ChildWindowFromPoint</c> treats an <c>HTTRANSPARENT</c> area of a standard control
        /// the same as other parts of the control. In contrast,
        /// <see cref="RealChildWindowFromPoint"/> treats an <c>HTTRANSPARENT</c> area differently;
        /// it returns the child window behind a transparent area of a control.
        /// For example, if the point is in a transparent area of a groupbox,
        /// <c>ChildWindowFromPoint</c> returns the groupbox while <see cref="RealChildWindowFromPoint"/>
        /// returns the child window behind the groupbox. However, both APIs return a
        /// static field, even though it, too, returns <c>HTTRANSPARENT</c>.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND ChildWindowFromPoint(
          [In] HWND hWndParent,
          [In] POINT Point
        );

        /// <summary>
        /// Shows or hides all pop-up windows owned by the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window that owns the pop-up windows to be shown or hidden.
        /// </param>
        /// <param name="fShow">
        /// If this parameter is <c>TRUE</c>, all hidden pop-up windows are shown.
        /// If this parameter is <c>FALSE</c>, all visible pop-up windows are hidden.
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
        /// <c>ShowOwnedPopups</c> shows only windows hidden by a previous call to <c>ShowOwnedPopups</c>.
        /// For example, if a pop-up window is hidden by using the <see cref="ShowWindow"/> function,
        /// subsequently calling <c>ShowOwnedPopups</c> with the <paramref name="fShow"/> parameter set to
        /// <c>TRUE</c> does not cause the window to be shown.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL ShowOwnedPopups(
          [In] HWND hWnd,
          [In] BOOL fShow
        );

        /// <summary>
        /// Determines which pop-up window owned by the specified window was most recently active.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the owner window.
        /// </param>
        /// <returns>
        /// The return value identifies the most recently active pop-up window.
        /// The return value is the same as the <paramref name="hWnd"/> parameter,
        /// if any of the following conditions are met:
        /// <list type="bullet">
        /// <item>
        /// The window identified by <paramref name="hWnd"/> was most recently active.
        /// </item>
        /// <item>
        /// The window identified by <paramref name="hWnd"/> does not own any pop-up windows.
        /// </item>
        /// <item>
        /// The window identifies by <paramref name="hWnd"/> is not a top-level window,
        /// or it is owned by another window.
        /// </item>
        /// </list>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND GetLastActivePopup(
          [In] HWND hWnd
        );

        /// <summary>
        /// Retrieves a handle to the menu assigned to the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose menu handle is to be retrieved.
        /// </param>
        /// <returns>
        /// The return value is a handle to the menu. If the specified window has no menu,
        /// the return value is <c>NULL</c>. If the window is a child window,
        /// the return value is undefined.
        /// </returns>
        /// <remarks>
        /// <c>GetMenu</c> does not work on floating menu bars.
        /// Floating menu bars are custom controls that mimic standard menus;
        /// they are not menus. To get the handle on a floating menu bar,
        /// use the <see href="https://learn.microsoft.com/en-us/previous-versions/ms971350(v=msdn.10)">Active Accessibility</see> APIs.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HMENU GetMenu(
          [In] HWND hWnd
        );

        /// <summary>
        /// Assigns a new menu to the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to which the menu is to be assigned.
        /// </param>
        /// <param name="hMenu">
        /// A handle to the new menu. If this parameter is <c>NULL</c>, the window's current menu is removed.
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
        /// The window is redrawn to reflect the menu change.
        /// A menu can be assigned to any window that is not a child window.
        /// </para>
        /// <para>
        /// The SetMenu function replaces the previous menu, if any, but it does not destroy it.
        /// An application should call the <see cref="DestroyMenu"/> function to accomplish this task.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL SetMenu(
          [In] HWND hWnd,
          [In, Optional] HMENU hMenu
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND SetCapture(
          [In] HWND hWnd
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL ReleaseCapture();

        /// <summary>
        /// Retrieves a handle to the window (if any) that has captured the mouse.
        /// Only one window at a time can capture the mouse; this window
        /// receives mouse input whether or not the cursor is within its borders.
        /// </summary>
        /// <returns>
        /// The return value is a handle to the capture window associated with the current thread.
        /// If no window in the thread has captured the mouse, the return value is <c>NULL</c>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// A <c>NULL</c> return value means the current thread has not captured the mouse.
        /// However, it is possible that another thread or process has captured the mouse.
        /// </para>
        /// <para>
        /// To get a handle to the capture window on another thread, use the <see cref="GetGUIThreadInfo"/> function.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND GetCapture();

        /// <summary>
        /// Retrieves the window handle to the active window
        /// attached to the calling thread's message queue.
        /// </summary>
        /// <returns>
        /// The return value is the handle to the active window attached to the
        /// calling thread's message queue. Otherwise, the return value is <c>NULL</c>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// To get the handle to the foreground window, you can use <see cref="GetForegroundWindow"/>.
        /// </para>
        /// <para>
        /// To get the window handle to the active window in the
        /// message queue for another thread, use <see cref="GetGUIThreadInfo"/>.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND GetActiveWindow();

        /// <summary>
        /// Retrieves the full path and file name of the module associated with the specified window handle.
        /// </summary>
        /// <param name="hwnd">
        /// A handle to the window whose module file name is to be retrieved.
        /// </param>
        /// <param name="pszFileName">
        /// The path and file name.
        /// </param>
        /// <param name="cchFileNameMax">
        /// The maximum number of characters that can be copied into the <c>lpszFileName</c> buffer.
        /// </param>
        /// <returns>
        /// The return value is the total number of characters copied into the buffer.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This function is intended for use with applications that allow
        /// the user to customize the environment.
        /// </para>
        /// <para>
        /// A keyboard layout name should be derived from the hexadecimal
        /// value of the language identifier corresponding to the layout.
        /// For example, U.S. English has a language identifier of 0x0409,
        /// so the primary U.S. English layout is named "00000409". Variants of U.S. English layout,
        /// such as the Dvorak layout, are named "00010409", "00020409" and so on.For a list of the
        /// primary language identifiers and sublanguage identifiers that make
        /// up a language identifier, see the <see cref="LanguageMacros.MAKELANGID"/> macro.
        /// </para>
        /// <para>
        /// There is a difference between the High Contrast color scheme and
        /// the High Contrast Mode. The High Contrast color scheme changes the
        /// system colors to colors that have obvious contrast; you switch to
        /// this color scheme by using the Display Options in the control panel.
        /// The High Contrast Mode, which uses <c>SPI_GETHIGHCONTRAST</c> and <c>SPI_SETHIGHCONTRAST</c>,
        /// advises applications to modify their appearance for visually-impaired users.
        /// It involves such things as audible warning to users and customized
        /// color scheme (using the Accessibility Options in the control panel).
        /// For more information, see <see href="https://learn.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-highcontrasta">HIGHCONTRAST</see>.
        /// For more information on general accessibility features, see <see href="https://learn.microsoft.com/en-us/windows/desktop/accessibility">Accessibility</see>.
        /// </para>
        /// <para>
        /// During the time that the primary button is held down to activate the Mouse ClickLock feature,
        /// the user can move the mouse. After the primary button is locked down,
        /// releasing the primary button does not result in a <see cref="WindowMessage.WM_LBUTTONUP"/> message.
        /// Thus, it will appear to an application that the primary button is still down.
        /// Any subsequent button message releases the primary button, sending a <see cref="WindowMessage.WM_LBUTTONUP"/> message
        /// to the application, thus the button can be unlocked programmatically or
        /// through the user clicking any button.
        /// </para>
        /// <para>
        /// This API is not DPI aware, and should not be used if the calling
        /// thread is per-monitor DPI aware.For the DPI-aware version of this API,
        /// see <see href="https://learn.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-systemparametersinfofordpi">SystemParametersInfoForDPI</see>. For more information on DPI awareness,
        /// see <see href="https://learn.microsoft.com/en-us/windows/desktop/hidpi/high-dpi-desktop-application-development-on-windows">the Windows High DPI documentation</see>.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe UINT GetWindowModuleFileNameW(
          [In] HWND hwnd,
          [Out] WCHAR* pszFileName,
          [In] UINT cchFileNameMax
        );

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window.
        /// The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpRect">
        /// A pointer to a <see cref="RECT"/> structure that receives the screen coordinates
        /// of the upper-left and lower-right corners of the window.
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
        public static extern unsafe BOOL GetWindowRect(
          [In] HWND hWnd,
          [Out] RECT* lpRect
        );

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window.
        /// These windows are ordered according to their appearance on the screen.
        /// The topmost window receives the highest rank and is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="hWndInsertAfter">
        /// A handle to the window to precede the positioned window in the Z order.
        /// This parameter must be a window handle or one of the following values.
        /// For more information about how this parameter is used, see the following Remarks section.
        /// </param>
        /// <param name="X">
        /// The new position of the left side of the window, in client coordinates.
        /// </param>
        /// <param name="Y">
        /// The new position of the top of the window, in client coordinates.
        /// </param>
        /// <param name="cx">
        /// The new width of the window, in pixels.
        /// </param>
        /// <param name="cy">
        /// The new height of the window, in pixels.
        /// </param>
        /// <param name="uFlags">
        /// The window sizing and positioning flags.
        /// See <see cref="SetWindowPosFlags"/>
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
        public static extern BOOL SetWindowPos(
          [In] HWND hWnd,
          [In, Optional] HWND hWndInsertAfter,
          [In] int X,
          [In] int Y,
          [In] int cx,
          [In] int cy,
          [In] SetWindowPosFlags uFlags
        );

        /// <summary>
        /// Determines the visibility state of the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be tested.
        /// </param>
        /// <returns>
        /// <para>
        /// If the specified window, its parent window, its parent's parent window,
        /// and so forth, have the <see cref="WindowStyles.VISIBLE"/> style, the return value is nonzero.
        /// Otherwise, the return value is zero.
        /// </para>
        /// <para>
        /// Because the return value specifies whether the window has the <see cref="WindowStyles.VISIBLE"/> style,
        /// it may be nonzero even if the window is totally obscured by other windows.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// The visibility state of a window is indicated by the <see cref="WindowStyles.VISIBLE"/> style bit.
        /// When <see cref="WindowStyles.VISIBLE"/> is set, the window is displayed and subsequent drawing into
        /// it is displayed as long as the window has the <see cref="WindowStyles.VISIBLE"/> style.
        /// </para>
        /// <para>
        /// Any drawing to a window with the <see cref="WindowStyles.VISIBLE"/> style will not be displayed
        /// if the window is obscured by other windows or is clipped by its parent window.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL IsWindowVisible(
          [In] HWND hWnd
        );

        /// <summary>
        /// Determines whether a window is maximized.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be tested.
        /// </param>
        /// <returns>
        /// <para>
        /// If the window is zoomed, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the window is not zoomed, the return value is zero.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL IsZoomed(
          [In] HWND hWnd
        );

        /// <summary>
        /// Restores a minimized (iconic) window to its previous size and position; it then activates the window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be restored and activated.
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
        /// <c>OpenIcon</c> sends a <see cref="WindowMessage.WM_QUERYOPEN"/> message to the given window.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL OpenIcon(
          [In] HWND hWnd
        );

        /// <summary>
        /// Changes the text of the specified window's title bar (if it has one).
        /// If the specified window is a control, the text of the control
        /// is changed. However, <c>SetWindowText</c> cannot change the text
        /// of a control in another application.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window or control whose text is to be changed.
        /// </param>
        /// <param name="lpString">
        /// The new title or control text.
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
        /// If the target window is owned by the current process,
        /// <c>SetWindowText</c> causes a <see cref="WindowMessage.WM_SETTEXT"/> message to be sent to the
        /// specified window or control. If the control is a list box control
        /// created with the <see cref="WindowStyles.CAPTION"/> style, however, <c>SetWindowText</c> sets the
        /// text for the control, not for the list box entries.
        /// </para>
        /// <para>
        /// To set the text of a control in another process, send the
        /// <see cref="WindowMessage.WM_SETTEXT"/> message directly instead of calling <c>SetWindowText</c>.
        /// </para>
        /// <para>
        /// The <c>SetWindowText</c> function does not expand tab characters
        /// (ASCII code 0x09). Tab characters are displayed as vertical bar (|) characters.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe BOOL SetWindowTextW(
          [In] HWND hWnd,
          [In, Optional] WCHAR* lpString
        );

        /// <summary>
        /// Tiles the specified child windows of the specified parent window.
        /// </summary>
        /// <param name="hwndParent">
        /// A handle to the parent window. If this parameter is <c>NULL</c>, the desktop window is assumed.
        /// </param>
        /// <param name="wHow">
        /// The tiling flags. This parameter can be one of the following values—optionally
        /// combined with <c>MDITILE_SKIPDISABLED</c> (0x0002) to prevent disabled MDI child windows from being tiled.
        /// <list type="table">
        /// <item>
        /// <term>
        /// MDITILE_HORIZONTAL 0x0001
        /// </term>
        /// <description>
        /// Tiles windows horizontally.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// MDITILE_VERTICAL 0x0000
        /// </term>
        /// <description>
        /// Tiles windows vertically.
        /// </description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="lpRect">
        /// A pointer to a structure that specifies the rectangular area, in client coordinates,
        /// within which the windows are arranged. If this parameter is <c>NULL</c>,
        /// the client area of the parent window is used.
        /// </param>
        /// <param name="cKids">
        /// The number of elements in the array specified by the <c>lpKids</c> parameter.
        /// This parameter is ignored if <c>lpKids</c> is <c>NULL</c>.
        /// </param>
        /// <param name="lpKids">
        /// An array of handles to the child windows to arrange.
        /// If a specified child window is a top-level window
        /// with the style <see cref="WindowStyles.EX_TOPMOST"/> or <see cref="WindowStyles.EX_TOOLWINDOW"/>,
        /// the child window is not arranged. If this parameter
        /// is <c>NULL</c>, all child windows of the specified parent
        /// window (or of the desktop window) are arranged.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is the number of windows arranged.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// Calling <c>TileWindows</c> causes all maximized windows to be restored to their previous size.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe WORD TileWindows(
          [In, Optional] HWND hwndParent,
          [In] UINT wHow,
          [In, Optional] RECT* lpRect,
          [In] UINT cKids,
          [In, Optional] HWND* lpKids
        );

        /// <summary>
        /// Retrieves a handle to the window that contains the specified point.
        /// </summary>
        /// <param name="Point">
        /// The point to be checked.
        /// </param>
        /// <returns>
        /// The return value is a handle to the window that contains the point.
        /// If no window exists at the given point, the return value is <c>NULL</c>.
        /// If the point is over a static text control, the return value
        /// is a handle to the window under the static text control.
        /// </returns>
        /// <remarks>
        /// The <c>WindowFromPoint</c> function does not retrieve a handle to a hidden
        /// or disabled window, even if the point is within the window.
        /// An application should use the <see cref="ChildWindowFromPoint"/> function
        /// for a nonrestrictive search.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND WindowFromPoint(
          [In] POINT Point
        );

        /// <summary>
        /// Copies the text of the specified window's title bar (if it has one) into a buffer.
        /// If the specified window is a control, the text of the control is copied.
        /// However, <c>GetWindowText</c> cannot retrieve the text of a control in another application.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window or control containing the text.
        /// </param>
        /// <param name="lpString">
        /// The buffer that will receive the text. If the string is as long or longer than the buffer,
        /// the string is truncated and terminated with a null character.
        /// </param>
        /// <param name="nMaxCount">
        /// The maximum number of characters to copy to the buffer, including the null character.
        /// If the text exceeds this limit, it is truncated.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is the length, in characters, of the copied string,
        /// not including the terminating null character. If the window
        /// has no title bar or text, if the title bar is empty, or if the window
        /// or control handle is invalid, the return value is zero. To get extended
        /// error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// <para>
        /// This function cannot retrieve the text of an edit control in another application.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If the target window is owned by the current process,
        /// <c>GetWindowText</c> causes a <see cref="WindowMessage.WM_GETTEXT"/> message to be sent
        /// to the specified window or control. If the target window
        /// is owned by another process and has a caption, <c>GetWindowText</c>
        /// retrieves the window caption text. If the window does not have
        /// a caption, the return value is a null string.
        /// This behavior is by design. It allows applications to call
        /// <c>GetWindowText</c> without becoming unresponsive if the process
        /// that owns the target window is not responding. However, if the
        /// target window is not responding and it belongs to the calling application,
        /// <c>GetWindowText</c> will cause the calling application to become unresponsive.
        /// </para>
        /// <para>
        /// To retrieve the text of a control in another process,
        /// send a <see cref="WindowMessage.WM_GETTEXT"/> message directly instead of calling <c>GetWindowText</c>.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe int GetWindowTextW(
          [In] HWND hWnd,
          [Out] WCHAR* lpString,
          [In] int nMaxCount
        );

        /// <summary>
        /// Determines whether a window is arranged.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be tested.
        /// </param>
        /// <returns>
        /// A nonzero value if the window is arranged; otherwise, zero.
        /// </returns>
        /// <remarks>
        /// A snapped window (see <see href="https://support.microsoft.com/windows/snap-your-windows-885a9b1e-a983-a3b1-16cd-c531795e6241">Snap your windows</see>) is considered to be <i>arranged</i>.
        /// You should treat <i>arranged</i> as a window state similar to
        /// <i>maximized</i>. Arranged, maximized, and minimized are mutually
        /// exclusive states. An arranged window can be restored to
        /// its original size and position. Restoring a window from
        /// minimized can make a window arranged if the window was
        /// arranged before it was minimized. When calling <see cref="GetWindowPlacement"/>,
        /// keep in mind that the <c>showCmd</c> member on the returned <c>WINDOWPLACEMENT</c>
        /// can have a value of <c>SW_SHOWNORMAL</c> even if the window is arranged.
        /// <br/>
        /// <br/>
        /// Example:
        /// <code>
        /// // Check whether the window is in the restored state.
        /// BOOL IsRestored(HWND hwnd)
        /// {
        ///     if (IsIconic(hwnd) || IsZoomed(hwnd) || IsWindowArranged(hwnd))
        ///     {
        ///        return false;
        ///     }
        ///     return true;
        /// }
        /// </code>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL IsWindowArranged(
          HWND hwnd
        );

        /// <summary>
        /// Determines whether the specified window handle identifies an existing window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be tested.
        /// </param>
        /// <returns>
        /// <para>
        /// If the window handle identifies an existing window, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the window handle does not identify an existing window, the return value is zero.
        /// </para>
        /// </returns>
        /// <remarks>
        /// A thread should not use <c>IsWindow</c> for a window that it did not
        /// create because the window could be destroyed after this function was called.
        /// Further, because window handles are recycled the handle could even point to a different window.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL IsWindow(
          [In, Optional] HWND hWnd
        );

        /// <summary>
        /// Determines whether the specified window is minimized (iconic).
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be tested.
        /// </param>
        /// <returns>
        /// <para>
        /// If the window is iconic, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the window is not iconic, the return value is zero.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL IsIconic(
          [In] HWND hWnd
        );

        /// <summary>
        /// Examines the Z order of the child windows associated
        /// with the specified parent window and retrieves a
        /// handle to the child window at the top of the Z order.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the parent window whose child windows are to be examined.
        /// If this parameter is <c>NULL</c>, the function returns a handle
        /// to the window at the top of the Z order.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the child window at the top of the Z order.
        /// If the specified window has no child windows, the return value is <c>NULL</c>.
        /// To get extended error information, use the <see cref="Kernel32.GetLastError"/> function.
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND GetTopWindow(
          [In, Optional] HWND hWnd
        );

        /// <summary>
        /// Retrieves information about the specified title bar.
        /// </summary>
        /// <param name="hwnd">
        /// A handle to the title bar whose information is to be retrieved.
        /// </param>
        /// <param name="pti">
        /// A pointer to a <see cref="TITLEBARINFO"/> structure to receive the information.
        /// Note that you must set the cbSize member to <c><see langword="sizeof"/>(<see cref="TITLEBARINFO"/>)</c> before calling this function.
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
        public static extern unsafe BOOL GetTitleBarInfo(
          [In] HWND hwnd,
          [In, Out] TITLEBARINFO* pti
        );

        /// <summary>
        /// Retrieves a handle to the foreground window
        /// (the window with which the user is currently working).
        /// The system assigns a slightly higher priority to the
        /// thread that creates the foreground window than it does to other threads.
        /// </summary>
        /// <returns>
        /// The return value is a handle to the foreground window.
        /// The foreground window can be <c>NULL</c> in certain circumstances,
        /// such as when a window is losing activation.
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND GetForegroundWindow();

        /// <summary>
        /// Calculates an appropriate pop-up window position using the specified
        /// anchor point, pop-up window size, flags, and the optional exclude
        /// rectangle. When the specified pop-up window size is smaller than
        /// the desktop window size, use the <c>CalculatePopupWindowPosition</c>
        /// function to ensure that the pop-up window is fully visible on
        /// the desktop window, regardless of the specified anchor point.
        /// </summary>
        /// <param name="anchorPoint">
        /// The specified anchor point.
        /// </param>
        /// <param name="windowSize">
        /// The specified window size.
        /// </param>
        /// <param name="flags">
        /// <para>
        /// Use one of the following flags to specify how the function positions
        /// the pop-up window horizontally and vertically. The flags are
        /// the same as the vertical and horizontal positioning flags of
        /// the <see cref="TrackPopupMenuEx"/> function.
        /// </para>
        /// <para>
        /// Use one of the following flags to specify how the function positions the pop-up window horizontally.
        /// <list type="bullet">
        /// <item><see cref="TrackPopupMenuFlags.CENTERALIGN"/></item>
        /// <item><see cref="TrackPopupMenuFlags.LEFTALIGN"/></item>
        /// <item><see cref="TrackPopupMenuFlags.RIGHTALIGN"/></item>
        /// </list>
        /// </para>
        /// <para>
        /// Uses one of the following flags to specify how the function positions the pop-up window vertically.
        /// <list type="bullet">
        /// <item><see cref="TrackPopupMenuFlags.BOTTOMALIGN"/></item>
        /// <item><see cref="TrackPopupMenuFlags.TOPALIGN"/></item>
        /// <item><see cref="TrackPopupMenuFlags.VCENTERALIGN"/></item>
        /// </list>
        /// </para>
        /// <para>
        /// Use one of the following flags to specify whether to accommodate horizontal or vertical alignment.
        /// <list type="bullet">
        /// <item><see cref="TrackPopupMenuFlags.HORIZONTAL"/></item>
        /// <item><see cref="TrackPopupMenuFlags.VERTICAL"/></item>
        /// </list>
        /// </para>
        /// <para>
        /// The following flag is available starting with Windows 7.
        /// <list type="bullet">
        /// <item><see cref="TrackPopupMenuFlags.WORKAREA"/></item>
        /// </list>
        /// </para>
        /// </param>
        /// <param name="excludeRect">
        /// A pointer to a structure that specifies the exclude rectangle. It can be <c>NULL</c>.
        /// </param>
        /// <param name="popupWindowPosition">
        /// A pointer to a structure that specifies the pop-up window position.
        /// </param>
        /// <returns>
        /// If the function succeeds, it returns <c>TRUE</c>; otherwise, it returns <c>FALSE</c>.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </returns>
        /// <remarks>
        /// <see cref="TrackPopupMenuFlags.WORKAREA"/> is supported for the <see cref="TrackPopupMenu"/> and <see cref="TrackPopupMenuEx"/> functions.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern unsafe BOOL CalculatePopupWindowPosition(
          [In] POINT* anchorPoint,
          [In] SIZE* windowSize,
          [In] UINT flags,
          [In, Optional] RECT* excludeRect,
          [Out] RECT* popupWindowPosition
        );

        /// <summary>
        /// Brings the specified window to the top of the Z order.
        /// If the window is a top-level window, it is activated.
        /// If the window is a child window, the top-level parent window
        /// associated with the child window is activated.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to bring to the top of the Z order.
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
        /// Use the <c>BringWindowToTop</c> function to uncover any window that
        /// is partially or completely obscured by other windows.
        /// </para>
        /// <para>
        /// Calling this function is similar to calling the <see cref="SetWindowPos"/>
        /// function to change a window's position in the Z order.
        /// <c>BringWindowToTop</c> does not make a window a top-level window.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL BringWindowToTop(
          [In] HWND hWnd
        );

        /// <summary>
        /// Arranges all the minimized (iconic) child windows of the specified parent window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the parent window.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is the height of one row of icons.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern UINT ArrangeIconicWindows(
          [In] HWND hWnd
        );

        /// <summary>
        /// Enables you to produce special effects when showing or hiding windows.
        /// There are four types of animation: roll, slide, collapse
        /// or expand, and alpha-blended fade.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to animate.
        /// The calling thread must own this window.
        /// </param>
        /// <param name="dwTime">
        /// The time it takes to play the animation, in milliseconds.
        /// Typically, an animation takes 200 milliseconds to play.
        /// </param>
        /// <param name="dwFlags">
        /// The type of animation. This parameter can be one or more of the
        /// following values. Note that, by default, these flags
        /// take effect when showing a window. To take effect when
        /// hiding a window, use <see cref="AnimateWindowFlags.HIDE"/> and a logical OR operator
        /// with the appropriate flags.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero. The function will fail in the following situations:
        /// <list type="bullet">
        /// <item>
        /// If the window is already visible and you are trying to show the window.
        /// </item>
        /// <item>
        /// If the window is already hidden and you are trying to hide the window.
        /// </item>
        /// <item>
        /// If there is no direction specified for the slide or roll animation.
        /// </item>
        /// <item>
        /// When trying to animate a child window with <see cref="AnimateWindowFlags.BLEND"/>.
        /// </item>
        /// <item>
        /// If the thread does not own the window.
        /// Note that, in this case, <see cref="AnimateWindow"/> fails but <see cref="Kernel32.GetLastError"/> returns <c>ERROR_SUCCESS</c>.
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// To get extended error information, call the <see cref="Kernel32.GetLastError"/> function.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// To show or hide a window without special effects, use <see cref="ShowWindow"/>.
        /// </para>
        /// <para>
        /// When using slide or roll animation, you must specify the direction.
        /// It can be either <see cref="AnimateWindowFlags.HOR_POSITIVE"/>, <see cref="AnimateWindowFlags.HOR_NEGATIVE"/>,
        /// <see cref="AnimateWindowFlags.VER_POSITIVE"/>, or <see cref="AnimateWindowFlags.VER_NEGATIVE"/>.
        /// </para>
        /// <para>
        /// You can combine <see cref="AnimateWindowFlags.HOR_POSITIVE"/> or <see cref="AnimateWindowFlags.HOR_NEGATIVE"/> with
        /// <see cref="AnimateWindowFlags.VER_POSITIVE"/> or <see cref="AnimateWindowFlags.VER_NEGATIVE"/> to animate a window diagonally.
        /// </para>
        /// <para>
        /// The window procedures for the window and its child windows should
        /// handle any <see cref="WindowMessage.PRINT"/> or <see cref="WindowMessage.PRINTCLIENT"/> messages. Dialog boxes, controls,
        /// and common controls already handle <see cref="WindowMessage.PRINTCLIENT"/>.
        /// The default window procedure already handles <see cref="WindowMessage.PRINT"/>.
        /// </para>
        /// <para>
        /// If a child window is displayed partially clipped,
        /// when it is animated it will have holes where it is clipped.
        /// </para>
        /// <para>
        /// <c>AnimateWindow</c> supports RTL windows.
        /// </para>
        /// <para>
        /// Avoid animating a window that has a drop shadow because
        /// it produces visually distracting, jerky animations.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL AnimateWindow(
          [In] HWND hWnd,
          [In] DWORD dwTime,
          [In] AnimateWindowFlags dwFlags
        );

        /// <summary>
        /// Retrieves the identifier of the thread that created the
        /// specified window and, optionally, the identifier of the process that created the window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpdwProcessId">
        /// A pointer to a variable that receives the process identifier.
        /// If this parameter is not <c>NULL</c>, <c>GetWindowThreadProcessId</c> copies
        /// the identifier of the process to the variable; otherwise,
        /// it does not. If the function fails, the value of the variable is unchanged.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the identifier
        /// of the thread that created the window. If the window handle
        /// is invalid, the return value is zero. To get extended error
        /// information, call <see cref="Kernel32.GetLastError"/>.
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe DWORD GetWindowThreadProcessId(
          [In] HWND hWnd,
          [Out, Optional] DWORD* lpdwProcessId
        );

        /// <summary>
        /// Retrieves a handle to the desktop window. The desktop window covers the entire screen.
        /// The desktop window is the area on top of which other windows are painted.
        /// </summary>
        /// <returns>
        /// The return value is a handle to the desktop window.
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern HWND GetDesktopWindow();

        /// <summary>
        /// Retrieves the handle to the ancestor of the specified window.
        /// </summary>
        /// <param name="hwnd">
        /// A handle to the window whose ancestor is to be retrieved.
        /// If this parameter is the desktop window, the function returns <c>NULL</c>.
        /// </param>
        /// <param name="gaFlags">
        /// The ancestor to be retrieved.
        /// </param>
        /// <returns>
        /// The return value is the handle to the ancestor window.
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern HWND GetAncestor(
          [In] HWND hwnd,
          [In] GetAncestorFlags gaFlags
        );

        /// <summary>
        /// Minimizes (but does not destroy) the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be minimized.
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
        /// To destroy a window, an application must use the <see cref="DestroyWindow"/> function.
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL CloseWindow(
          [In] HWND hWnd
        );

        /// <summary>
        /// Determines whether the specified window is enabled for mouse and keyboard input.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be tested.
        /// </param>
        /// <returns>
        /// <para>
        /// If the window is enabled, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the window is not enabled, the return value is zero.
        /// </para>
        /// </returns>
        /// <remarks>
        /// A child window receives input only if it is both enabled and visible.
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL IsWindowEnabled(
          [In] HWND hWnd
        );

        /// <summary>
        /// Enables or disables mouse and keyboard input to the specified window or control.
        /// When input is disabled, the window does not receive input such as
        /// mouse clicks and key presses. When input is enabled, the window receives all input.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be enabled or disabled.
        /// </param>
        /// <param name="bEnable">
        /// Indicates whether to enable or disable the window.
        /// If this parameter is <c>TRUE</c>, the
        /// window is enabled. If the parameter is <c>FALSE</c>, the window is disabled.
        /// </param>
        /// <returns>
        /// <para>
        /// If the window was previously disabled, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the window was not previously disabled, the return value is zero.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL EnableWindow(
          [In] HWND hWnd,
          [In] BOOL bEnable
        );

        /// <summary>
        /// Enumerates all nonchild windows associated with a thread
        /// by passing the handle to each window, in turn, to
        /// an application-defined callback function. <c>EnumThreadWindows</c>
        /// continues until the last window is enumerated or the callback
        /// function returns <c>FALSE</c>. To enumerate child windows of a
        /// particular window, use the <see cref="EnumChildWindows"/> function.
        /// </summary>
        /// <param name="dwThreadId">
        /// The identifier of the thread whose windows are to be enumerated.
        /// </param>
        /// <param name="lpfn">
        /// A pointer to an application-defined callback function.
        /// </param>
        /// <param name="lParam">
        /// An application-defined value to be passed to the callback function.
        /// </param>
        /// <returns>
        /// If the callback function returns <c>TRUE</c> for all windows in the
        /// thread specified by <paramref name="dwThreadId"/>, the return
        /// value is <c>TRUE</c>. If the callback function returns
        /// <c>FALSE</c> on any enumerated window, or if there are
        /// no windows found in the thread specified by <paramref name="dwThreadId"/>,
        /// the return value is <c>FALSE</c>.
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe BOOL EnumThreadWindows(
          [In] DWORD dwThreadId,
          [In] delegate*<HWND, LPARAM, BOOL> lpfn,
          [In] LPARAM lParam
        );

        /// <summary>
        /// Enumerates all top-level windows on the screen by passing
        /// the handle to each window, in turn, to an application-defined
        /// callback function. <c>EnumWindows</c> continues until the last top-level
        /// window is enumerated or the callback function returns <c>FALSE</c>.
        /// </summary>
        /// <param name="lpEnumFunc">
        /// A pointer to an application-defined callback function.
        /// For more information, see <see href="https://learn.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms633498(v=vs.85)">EnumWindowsProc</see>.
        /// </param>
        /// <param name="lParam">
        /// An application-defined value to be passed to the callback function.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// <para>
        /// If <c>EnumWindowsProc</c> returns zero, the return value is
        /// also zero. In this case, the callback function should call
        /// <see cref="Kernel32.SetLastError"/> to obtain a meaningful error code to be returned
        /// to the caller of <c>EnumWindows</c>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// The <c>EnumWindows</c> function does not enumerate child windows,
        /// with the exception of a few top-level windows owned by the
        /// system that have the <see cref="WindowStyles.CHILD"/> style.
        /// </para>
        /// <para>
        /// This function is more reliable than calling the <see cref="GetWindow"/>
        /// function in a loop. An application that calls <see cref="GetWindow"/>
        /// to perform this task risks being caught in an infinite
        /// loop or referencing a handle to a window that has been destroyed.
        /// </para>
        /// <para>
        /// <b>Note</b>: For Windows 8 and later, <c>EnumWindows</c> enumerates only top-level windows of desktop apps.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe BOOL EnumWindows(
          [In] delegate*<HWND, LPARAM, BOOL> lpEnumFunc,
          [In] LPARAM lParam
        );

        /// <summary>
        /// Retrieves a handle to a window that has the
        /// specified relationship
        /// (<see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/window-features">Z-Order</see>
        /// or owner) to the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to a window. The window handle retrieved is
        /// relative to this window, based on the value of the <paramref name="uCmd"/> parameter.
        /// </param>
        /// <param name="uCmd">
        /// The relationship between the specified window and the window whose handle is to be retrieved.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a window handle.
        /// If no window exists with the specified relationship to the
        /// specified window, the return value is <c>NULL</c>. To get extended
        /// error information, call <see cref="Kernel32.GetLastError"/>.
        /// </returns>
        /// <remarks>
        /// The <c>EnumChildWindows</c> function is more reliable than calling <c>GetWindow</c>
        /// in a loop. An application that calls <c>GetWindow</c> to perform this
        /// task risks being caught in an infinite loop or referencing a handle
        /// to a window that has been destroyed.
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern HWND GetWindow(
          [In] HWND hWnd,
          [In] GetWindowFlags uCmd
        );

        /// <summary>
        /// Changes the position and dimensions of the specified window.
        /// For a top-level window, the position and dimensions are
        /// relative to the upper-left corner of the screen. For a child
        /// window, they are relative to the upper-left corner of the parent window's client area.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="bRepaint">
        /// Indicates whether the window is to be repainted.
        /// If this parameter is <c>TRUE</c>, the window receives a
        /// message. If the parameter is <c>FALSE</c>, no repainting of
        /// any kind occurs. This applies to the client area, the
        /// nonclient area (including the title bar and scroll bars),
        /// and any part of the parent window uncovered as a result of
        /// moving a child window.
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
        /// If the <paramref name="bRepaint"/> parameter is <c>TRUE</c>,
        /// the system sends the <see cref="WindowMessage.WM_PAINT"/> message to the window procedure
        /// immediately after moving the window (that is, the MoveWindow function calls
        /// the <see cref="UpdateWindow"/> function). If <paramref name="bRepaint"/> is <c>FALSE</c>,
        /// the application must explicitly invalidate or redraw any parts of the window
        /// and parent window that need redrawing.
        /// </para>
        /// <para>
        /// <c>MoveWindow</c> sends the <c>WM_WINDOWPOSCHANGING</c>,
        /// <c>WM_WINDOWPOSCHANGED</c>, <c>WM_MOVE</c>, <c>WM_SIZE</c>,
        /// and <c>WM_NCCALCSIZE</c> messages to the window.
        /// </para>
        /// <para>
        /// For an example, see
        /// <see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/using-windows">Creating,
        /// Enumerating, and Sizing Child Windows</see>.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL MoveWindow(
          [In] HWND hWnd,
          [In] int X,
          [In] int Y,
          [In] int nWidth,
          [In] int nHeight,
          [In] BOOL bRepaint
        );

        /// <summary>
        /// Retrieves the coordinates of a window's client area.
        /// The client coordinates specify the upper-left and lower-right
        /// corners of the client area. Because client coordinates are
        /// relative to the upper-left corner of a window's client area,
        /// the coordinates of the upper-left corner are (0,0).
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose client coordinates are to be retrieved.
        /// </param>
        /// <param name="lpRect">
        /// A pointer to a <see cref="RECT"/> structure that receives the client coordinates.
        /// The left and top members are zero. The right and bottom members
        /// contain the width and height of the window.
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
        /// In conformance with conventions for the <see cref="RECT"/> structure,
        /// the bottom-right coordinates of the returned rectangle
        /// are exclusive. In other words, the pixel at (right, bottom)
        /// lies immediately outside the rectangle.
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe BOOL GetClientRect(
          [In] HWND hWnd,
          [Out] RECT* lpRect
        );

        /// <summary>
        /// The <c>UpdateWindow</c> function updates the client area of
        /// the specified window by sending a <see cref="WindowMessage.WM_PAINT"/> message to
        /// the window if the window's update region is not empty.
        /// The function sends a <see cref="WindowMessage.WM_PAINT"/> message directly to the
        /// window procedure of the specified window, bypassing the
        /// application queue. If the update region is empty, no message is sent.
        /// </summary>
        /// <param name="hWnd">
        /// Handle to the window to be updated.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL UpdateWindow(
          [In] HWND hWnd
        );

        /// <summary>
        /// Sets the specified window's show state.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="nCmdShow">
        /// Controls how the window is to be shown.
        /// This parameter is ignored the first time an application calls
        /// <c>ShowWindow</c>, if the program that launched the application provides
        /// a <see cref="STARTUPINFOW"/> structure. Otherwise, the first time <c>ShowWindow</c> is
        /// called, the value should be the value obtained by the <c>WinMain</c>
        /// function in its <paramref name="nCmdShow"/> parameter.
        /// </param>
        /// <returns>
        /// <para>
        /// If the window was previously visible, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the window was previously hidden, the return value is zero.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL ShowWindow(
          [In] HWND hWnd,
          [In] int nCmdShow
        );

        /// <summary>
        /// Enumerates the child windows that belong to the specified
        /// parent window by passing the handle to each child window,
        /// in turn, to an application-defined callback function.
        /// <c>EnumChildWindows</c> continues until the last child window
        /// is enumerated or the callback function returns <c>FALSE</c>.
        /// </summary>
        /// <param name="hWndParent">
        /// A handle to the parent window whose child windows are to be enumerated. If this parameter is <c>NULL</c>, this function is equivalent to <see href="https://learn.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-enumwindows">EnumWindows</see>.
        /// </param>
        /// <param name="lpEnumFunc">
        /// A pointer to an application-defined callback function. For more information, see <see href="https://learn.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms633493(v=vs.85)">EnumChildProc</see>.
        /// </param>
        /// <param name="lParam">
        /// An application-defined value to be passed to the callback function.
        /// </param>
        /// <returns>
        /// The return value is not used.
        /// </returns>
        /// <remarks>
        /// <para>
        /// If a child window has created child windows of its own, <c>EnumChildWindows</c> enumerates those windows as well.
        /// </para>
        /// <para>
        /// A child window that is moved or repositioned in the Z order during
        /// the enumeration process will be properly enumerated.
        /// The function does not enumerate a child window that is destroyed
        /// before being enumerated or that is created during the enumeration process.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe BOOL EnumChildWindows(
          [In, Optional] HWND hWndParent,
          [In] delegate*<HWND, LPARAM, BOOL> lpEnumFunc,
          [In] LPARAM lParam
        );

        /// <summary>
        /// Determines whether a window is a child window or descendant window of a
        /// specified parent window. A child window is the direct descendant of a
        /// specified parent window if that parent window is in the chain of parent
        /// windows; the chain of parent windows leads from the original overlapped
        /// or pop-up window to the child window.
        /// </summary>
        /// <param name="hWndParent">
        /// A handle to the parent window.
        /// </param>
        /// <param name="hWnd">
        /// A handle to the window to be tested.
        /// </param>
        /// <returns>
        /// <para>
        /// If the window is a child or descendant window of the specified parent window, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the window is not a child or descendant window of the specified parent window, the return value is zero.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL IsChild(
          [In] HWND hWndParent,
          [In] HWND hWnd
        );

        /// <summary>
        /// Changes the parent window of the specified child window.
        /// </summary>
        /// <param name="hWndChild">
        /// A handle to the child window.
        /// </param>
        /// <param name="hWndNewParent">
        /// A handle to the new parent window.
        /// If this parameter is <c>NULL</c>, the desktop window becomes the new
        /// parent window. If this parameter is <c>HWND_MESSAGE</c>,
        /// the child window becomes a <see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/window-features">message-only window</see>.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is a handle to the previous parent window.
        /// </para>
        /// <para>
        /// If the function fails, the return value is <c>NULL</c>. To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern HWND SetParent(
          [In] HWND hWndChild,
          [In, Optional] HWND hWndNewParent
        );

        /// <summary>
        /// <para>
        /// Retrieves a handle to the specified window's parent or owner.
        /// </para>
        /// <para>
        /// To retrieve a handle to a specified ancestor, use the <see cref="GetAncestor"/> function.
        /// </para>
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose parent window handle is to be retrieved.
        /// </param>
        /// <returns>
        /// <para>
        /// If the window is a child window, the return value is a handle to the parent window.
        /// If the window is a top-level window with the <see cref="WindowStyles.POPUP"/> style,
        /// the return value is a handle to the owner window.
        /// </para>
        /// <para>
        /// If the function fails, the return value is <c>NULL</c>.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// <para>
        /// This function typically fails for one of the following reasons:
        /// <list type="bullet">
        /// <item>
        /// The window is a top-level window that is unowned or does not have
        /// the <see cref="WindowStyles.POPUP"/> style.
        /// </item>
        /// <item>
        /// The owner window has <see cref="WindowStyles.POPUP"/> style.
        /// </item>
        /// </list>
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// To obtain a window's owner window, instead of using <c>GetParent</c>,
        /// use <see cref="GetWindow"/> with the <c>GW_OWNER</c> flag. To obtain the parent window and
        /// not the owner, instead of using <c>GetParent</c>, use <see cref="GetAncestor"/> with
        /// the <see cref="GetAncestorFlags.PARENT"/> flag.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern HWND GetParent(
          [In] HWND hWnd
        );

        /// <summary>
        /// <para>
        /// Destroys the specified window. The function sends <see cref="WindowMessage.WM_DESTROY"/> and
        /// <see cref="WindowMessage.WM_NCDESTROY"/> messages to the window to deactivate it and remove
        /// the keyboard focus from it. The function also destroys the window's menu,
        /// flushes the thread message queue, destroys timers, removes clipboard
        /// ownership, and breaks the clipboard viewer chain (if the window is at
        /// the top of the viewer chain).
        /// </para>
        /// <para>
        /// If the specified window is a parent or owner window, <c>DestroyWindow</c>
        /// automatically destroys the associated child or owned windows when it
        /// destroys the parent or owner window. The function first destroys
        /// child or owned windows, and then it destroys the parent or owner window.
        /// </para>
        /// <para>
        /// <c>DestroyWindow</c> also destroys modeless dialog boxes created
        /// by the <see cref="CreateDialog"/> function.
        /// </para>
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be destroyed.
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
        /// A thread cannot use <c>DestroyWindow</c> to destroy a window created by a different thread.
        /// </para>
        /// <para>
        /// If the window being destroyed is a child window that does not have
        /// the <see cref="WindowStyles.EX_NOPARENTNOTIFY"/> style, a <see cref="WindowMessage.WM_PARENTNOTIFY"/> message is sent to the parent.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL DestroyWindow(
          [In] HWND hWnd
        );

        /// <summary>
        /// Creates an overlapped, pop-up, or child window with an extended
        /// window style; otherwise, this function is identical to the
        /// <see cref="CreateWindowW"/> function. For more information about creating
        /// a window and for full descriptions of the other parameters
        /// of <c>CreateWindowExW</c>, see <see cref="CreateWindowW"/>.
        /// </summary>
        /// <param name="dwExStyle">
        /// The extended window style of the window being created.
        /// For a list of possible values,
        /// see <see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/extended-window-styles">Extended Window Styles</see>.
        /// </param>
        /// <param name="lpClassName">
        /// A null-terminated string or a class atom created by a previous
        /// call to the <see cref="RegisterClassW"/> or <see cref="RegisterClassExW"/> function.
        /// The atom must be in the low-order word of <paramref name="lpClassName"/>;
        /// the high-order word must be zero. If <paramref name="lpClassName"/> is a string,
        /// it specifies the window class name. The class name can
        /// be any name registered with <see cref="RegisterClassW"/> or <see cref="RegisterClassExW"/>,
        /// provided that the module that registers the class is also
        /// the module that creates the window. The class name can also
        /// be any of the predefined system class names.
        /// </param>
        /// <param name="lpWindowName">
        /// The window name. If the window style specifies a title bar, the window title
        /// pointed to by <paramref name="lpWindowName"/> is displayed in the title bar. When using
        /// <see cref="CreateWindowW"/> to create controls, such as buttons, check boxes, and
        /// static controls, use <paramref name="lpWindowName"/> to specify the text of the control.
        /// When creating a static control with the <c>SS_ICON</c> style, use <paramref name="lpWindowName"/>
        /// to specify the icon name or identifier. To specify an identifier,
        /// use the syntax <c>"#num"</c>.
        /// </param>
        /// <param name="dwStyle">
        /// The style of the window being created. This parameter can be a combination
        /// of the
        /// <see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/window-styles"> window style values</see>,
        /// plus the control styles indicated in the Remarks section.
        /// </param>
        /// <param name="X">
        /// The initial horizontal position of the window.
        /// For an overlapped or pop-up window, the <paramref name="X"/> parameter is the initial
        /// x-coordinate of the window's upper-left corner, in screen coordinates.
        /// For a child window, <paramref name="X"/> is the x-coordinate of the upper-left corner
        /// of the window relative to the upper-left corner of the parent window's
        /// client area. If <paramref name="X"/> is set to <c>CW_USEDEFAULT</c>, the system selects the default
        /// position for the window's upper-left corner and ignores the <paramref name="Y"/> parameter.
        /// <c>CW_USEDEFAULT</c> is valid only for overlapped windows; if it is specified for
        /// a pop-up or child window, the <paramref name="X"/> and <paramref name="Y"/> parameters are set to zero.
        /// </param>
        /// <param name="Y">
        /// <para>
        /// The initial vertical position of the window.
        /// For an overlapped or pop-up window, the <paramref name="Y"/> parameter is the initial
        /// y-coordinate of the window's upper-left corner, in screen coordinates.
        /// For a child window, <paramref name="Y"/> is the initial y-coordinate of the upper-left corner
        /// of the child window relative to the upper-left corner of the parent window's
        /// client area. For a list box y is the initial y-coordinate of the upper-left
        /// corner of the list box's client area relative to the upper-left corner of
        /// the parent window's client area.
        /// </para>
        /// <para>
        /// If an overlapped window is created with the <see cref="WindowStyles.VISIBLE"/> style bit set and
        /// the <paramref name="X"/> parameter is set to <c>CW_USEDEFAULT</c>,
        /// then the <paramref name="Y"/> parameter determines
        /// how the window is shown. If the <paramref name="Y"/> parameter is <c>CW_USEDEFAULT</c>, then the
        /// window manager calls <see cref="ShowWindow"/> with the <see cref="ShowWindowCommand.SHOW"/> flag after the window
        /// has been created. If the <paramref name="Y"/> parameter is some other value, then the window
        /// manager calls <see cref="ShowWindow"/> with that value as the <c>nCmdShow</c> parameter.
        /// </para>
        /// </param>
        /// <param name="nWidth">
        /// The width, in device units, of the window.
        /// For overlapped windows, <paramref name="nWidth"/> is the window's width, in screen coordinates,
        /// or <c>CW_USEDEFAULT</c>. If <paramref name="nWidth"/> is <c>CW_USEDEFAULT</c>, the system selects a default
        /// width and height for the window; the default width extends from the initial
        /// x-coordinates to the right edge of the screen; the default height extends
        /// from the initial y-coordinate to the top of the icon area. <c>CW_USEDEFAULT</c>
        /// is valid only for overlapped windows; if <c>CW_USEDEFAULT</c> is specified for a
        /// pop-up or child window, the <paramref name="nWidth"/> and <paramref name="nHeight"/> parameter are set to zero.
        /// </param>
        /// <param name="nHeight">
        /// The height, in device units, of the window. For overlapped windows, <paramref name="nHeight"/> is the window's height,
        /// in screen coordinates. If the <paramref name="nWidth"/> parameter is set
        /// to <c>CW_USEDEFAULT</c>, the system ignores <paramref name="nHeight"/>.
        /// </param>
        /// <param name="hWndParent">
        /// <para>
        /// A handle to the parent or owner window of the window being created.
        /// To create a child window or an owned window, supply a valid window
        /// handle. This parameter is optional for pop-up windows.
        /// </para>
        /// <para>
        /// To create a
        /// <see href="https://learn.microsoft.com/en-us/windows/desktop/winmsg/window-features">message-only window</see>,
        /// supply <c>HWND_MESSAGE</c> or a
        /// handle to an existing message-only window.
        /// </para>
        /// </param>
        /// <param name="hMenu">
        /// A handle to a menu, or specifies a child-window identifier,
        /// depending on the window style. For an overlapped or pop-up window,
        /// <paramref name="hMenu"/> identifies the menu to be used with the window; it can be
        /// <c>NULL</c> if the class menu is to be used. For a child window, <paramref name="hMenu"/>
        /// specifies the child-window identifier, an integer value used by
        /// a dialog box control to notify its parent about events.
        /// The application determines the child-window identifier;
        /// it must be unique for all child windows with the same parent window.
        /// </param>
        /// <param name="hInstance">
        /// A handle to the instance of the module to be associated with the window.
        /// </param>
        /// <param name="lpParam">
        /// <para>
        /// Pointer to a value to be passed to the window through the <see cref="CREATESTRUCT"/>
        /// structure (<c>lpCreateParams</c> member) pointed to by the <c>lParam</c> param of
        /// the <see cref="WindowMessage.WM_CREATE"/> message. This message is sent to the created window by
        /// this function before it returns.
        /// </para>
        /// <para>
        /// If an application calls <see cref="CreateWindowW"/> to create a MDI client window,
        /// <c>lpParam</c> should point to a <see cref="CLIENTCREATESTRUCT"/> structure.
        /// If an MDI client window calls <see cref="CreateWindowW"/> to create an MDI
        /// child window, <c>lpParam</c> should point to a <see cref="MDICREATESTRUCT"/> structure.
        /// <c>lpParam</c> may be <c>NULL</c> if no additional data is needed.
        /// </para>
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is a handle to the new window.
        /// </para>
        /// <para>
        /// If the function fails, the return value is <c>NULL</c>.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe HWND CreateWindowExW(
          [In] DWORD dwExStyle,
          [In, Optional] char* lpClassName,
          [In, Optional] char* lpWindowName,
          [In] DWORD dwStyle,
          [In] int X,
          [In] int Y,
          [In] int nWidth,
          [In] int nHeight,
          [In, Optional] HWND hWndParent,
          [In, Optional] HMENU hMenu,
          [In, Optional] HINSTANCE hInstance,
          [In, Optional] void* lpParam
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern LRESULT DefWindowProcW(
            [In] HWND hWnd,
            [In] UINT Msg,
            [In] WPARAM wParam,
            [In] LPARAM lParam
        );
    }
}
