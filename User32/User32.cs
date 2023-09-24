using System.Runtime.InteropServices;

namespace Win32
{
    public static class User32
    {
        /// <summary>
        /// Retrieves information about the global cursor.
        /// </summary>
        /// <param name="pci">
        /// A pointer to a <see cref="CURSORINFO"/> structure that receives the information.
        /// Note that you must set the <c>cbSize</c> member to <see langword="sizeof"/>(<see cref="CURSORINFO"/>) before calling this function.
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
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL GetCursorInfo(
          [In, Out] CURSORINFO* pci
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
        unsafe public static extern DWORD GetWindowThreadProcessId(
          [In] HWND hWnd,
          [Out, Optional] DWORD* lpdwProcessId
        );

        /// <summary>
        /// Retrieves information about the active window or a specified GUI thread.
        /// </summary>
        /// <param name="idThread">
        /// The identifier for the thread for which information is to be retrieved.
        /// To retrieve this value, use the <see cref="GetWindowThreadProcessId"/> function.
        /// If this parameter is <c>NULL</c>, the function returns information for the foreground thread.
        /// </param>
        /// <param name="pgui">
        /// A pointer to a <see cref="GUITHREADINFO"/> structure that receives information describing the thread.
        /// Note that you must set the cbSize member to <see langword="sizeof"/>(<see cref="GUITHREADINFO"/>) before calling
        /// this function.
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
        /// This function succeeds even if the active window is not owned by the calling process.
        /// If the specified thread does not exist or have an input queue, the function will fail.
        /// </para>
        /// <para>
        /// This function is useful for retrieving out-of-context information about a thread.
        /// The information retrieved is the same as if an application retrieved the information about itself.
        /// </para>
        /// <para>
        /// For an edit control, the returned rcCaret rectangle contains the caret plus information on
        /// text direction and padding. Thus, it may not give the correct position of the cursor.
        /// The Sans Serif font uses four characters for the cursor:
        /// <list type="table">
        /// <item>
        /// <term>
        /// CURSOR_LTR
        /// </term>
        /// <description>
        /// 0xf00c
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// CURSOR_RTL
        /// </term>
        /// <description>
        /// 0xf00d
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// CURSOR_THAI
        /// </term>
        /// <description>
        /// 0xf00e
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// CURSOR_USA
        /// </term>
        /// <description>
        /// 0xfff (this is a marker value with no associated glyph)
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// To get the actual insertion point in the <c>rcCaret</c> rectangle, perform the following steps.
        /// <list type="number">
        /// <item>
        /// Call <see cref="GetKeyboardLayout"/> to retrieve the current input language.
        /// </item>
        /// <item>
        /// Determine the character used for the cursor, based on the current input language.
        /// </item>
        /// <item>
        /// Call <see cref="CreateFont"/> using Sans Serif for the font,
        /// the height given by <c>rcCaret</c>, and a width of <c>zero</c>.
        /// For <c>fnWeight</c>, call <c>SystemParametersInfo(SPI_GETCARETWIDTH, 0, pvParam, 0)</c>.
        /// If <c>pvParam</c> is greater than 1, set <c>fnWeight</c> to 700, otherwise set <c>fnWeight</c> to 400.
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// The function may not return valid window handles in the
        /// <see cref="GUITHREADINFO"/> structure when called to retrieve information
        /// for the foreground thread, such as when a window is losing activation.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL GetGUIThreadInfo(
          [In] DWORD idThread,
          [In, Out] GUITHREADINFO* pgui
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
        /// The ancestor to be retrieved. See <see cref="GA"/> for values.
        /// </param>
        /// <returns>
        /// The return value is the handle to the ancestor window.
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern HWND GetAncestor(
          [In] HWND hwnd,
          [In] UINT gaFlags
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
        /// Enables you to produce special effects when showing or hiding windows.
        /// There are four types of animation: roll, slide, collapse or expand, and alpha-blended fade.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to animate. The calling thread must own this window.
        /// </param>
        /// <param name="dwTime">
        /// The time it takes to play the animation, in milliseconds.
        /// Typically, an animation takes 200 milliseconds to play.
        /// </param>
        /// <param name="dwFlags">
        /// The type of animation. This parameter can be one or more of the
        /// following values. Note that, by default, these
        /// flags take effect when showing a window. To take
        /// effect when hiding a window, use <c>AW_HIDE</c> and a
        /// logical <c>OR</c> operator with the appropriate flags.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// The function will fail in the following situations:
        /// <list type="bullet">
        /// <item>If the window is already visible and you are trying to show the window.</item>
        /// <item>If the window is already hidden and you are trying to hide the window.</item>
        /// <item>If there is no direction specified for the slide or roll animation.</item>
        /// <item>When trying to animate a child window with <c>AW_BLEND</c>.</item>
        /// <item>If the thread does not own the window. Note that, in this case, <c>AnimateWindow</c> fails but <see cref="Kernel32.GetLastError"/> returns <c>ERROR_SUCCESS</c>.</item>
        /// </list>
        /// </para>
        /// <para>
        /// To get extended error information, call the <see cref="Kernel32.GetLastError"/> function.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL AnimateWindow(
          [In] HWND hWnd,
          [In] DWORD dwTime,
          [In] DWORD dwFlags
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
        unsafe public static extern BOOL EnumThreadWindows(
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
        /// system that have the <see cref="WS.WS_CHILD"/> style.
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
        unsafe public static extern BOOL EnumWindows(
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
          [In] UINT uCmd
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
        /// the system sends the <see cref="WM.WM_PAINT"/> message to the window procedure
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
        unsafe public static extern BOOL GetClientRect(
          [In] HWND hWnd,
          [Out] RECT* lpRect
        );

        /// <summary>
        /// The <c>UpdateWindow</c> function updates the client area of
        /// the specified window by sending a <see cref="WM.WM_PAINT"/> message to
        /// the window if the window's update region is not empty.
        /// The function sends a <see cref="WM.WM_PAINT"/> message directly to the
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
        unsafe public static extern BOOL EnumChildWindows(
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
        /// If the window is a top-level window with the <see cref="WS.WS_POPUP"/> style,
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
        /// the <see cref="WS.WS_POPUP"/> style.
        /// </item>
        /// <item>
        /// The owner window has <see cref="WS.WS_POPUP"/> style.
        /// </item>
        /// </list>
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// To obtain a window's owner window, instead of using <c>GetParent</c>,
        /// use <see cref="GetWindow"/> with the <c>GW_OWNER</c> flag. To obtain the parent window and
        /// not the owner, instead of using <c>GetParent</c>, use <see cref="GetAncestor"/> with
        /// the <see cref="GA.GA_PARENT"/> flag.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern HWND GetParent(
              [In] HWND hWnd
            );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern LRESULT SendMessage(
              [In] HWND hWnd,
              [In] UINT Msg,
              [In] WPARAM wParam,
              [In] LPARAM lParam
            );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern LONG_PTR GetWindowLongPtrW(
          [In] HWND hWnd,
          [In] int nIndex
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL DestroyWindow(
          [In] HWND hWnd
        );

        /// <summary>
        /// Displays a modal dialog box that contains a system icon,
        /// a set of buttons, and a brief application-specific message,
        /// such as status or error information. The message box returns
        /// an integer value that indicates which button the user clicked.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the owner window of the message box to be created.
        /// If this parameter is <c>NULL</c>, the message box has no owner window.
        /// </param>
        /// <param name="text">
        /// The message to be displayed. If the string consists of more
        /// than one line, you can separate the lines using a carriage return
        /// and/or linefeed character between each line.
        /// </param>
        /// <param name="caption">
        /// The dialog box title. If this parameter is <c>NULL</c>, the default title is Error.
        /// </param>
        /// <param name="type">
        /// The contents and behavior of the dialog box. This parameter can be
        /// a combination of flags from the following groups of flags.
        /// </param>
        /// <returns></returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern MessageBoxResult MessageBox(
          [In, Optional] HWND hWnd,
          [In, Optional] char* lpText,
          [In, Optional] char* lpCaption,
          [In] UINT uType
        );

        unsafe public static MessageBoxResult MessageBox(
           HWND hWnd,
           string lbText,
           string lpCaption,
           UINT uType)
        {
            fixed (char* _lbText = lbText)
            {
                fixed (char* _lpCaption = lpCaption)
                {
                    return User32.MessageBox(hWnd, _lbText, _lpCaption, uType);
                }
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL PostMessageW(
          [In, Optional] HWND hWnd,
          [In] UINT Msg,
          [In] WPARAM wParam,
          [In] LPARAM lParam
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void PostQuitMessage(
          [In] int nExitCode
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL InvalidateRect(
          [In] HWND hWnd,
          [In] RECT* lpRect,
          [In] BOOL bErase
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern LRESULT DispatchMessageW(
            [In] MSG* lpMsg
        );


        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL GetMessageW(
              [Out] MSG* lpMsg,
              [In, Optional] HWND hWnd,
              [In] UINT wMsgFilterMin,
              [In] UINT wMsgFilterMax
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL PeekMessageW(
              [Out] MSG* lpMsg,
              [In, Optional] HWND hWnd,
              [In] uint wMsgFilterMin,
              [In] uint wMsgFilterMax,
              [In] uint wRemoveMsg
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL TranslateMessage(
          [In] MSG* lpMsg
        );


        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern HWND CreateWindowExW(
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
        unsafe public static extern BOOL EndPaint(
              [In] HWND hWnd,
              [In] PaintStruct* lpPaint
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern HDC BeginPaint(
              [In] HWND hWnd,
              [Out] PaintStruct* lpPaint
        );


        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern LRESULT DefWindowProcW(
            [In] HWND hWnd,
            [In] UINT Msg,
            [In] WPARAM wParam,
            [In] LPARAM lParam
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern ATOM RegisterClassExW(
            [In] WNDCLASSEXW* unnamedParam1);

    }
}
