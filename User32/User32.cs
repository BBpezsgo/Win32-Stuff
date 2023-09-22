using System.Runtime.InteropServices;

namespace Win32
{
    public static class User32
    {
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
        /// <para>
        /// Retrieves a handle to the specified window's parent or owner.
        /// </para>
        /// <para>
        /// To retrieve a handle to a specified ancestor, use the <c>GetAncestor</c> function.
        /// </para>
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose parent window handle is to be retrieved.
        /// </param>
        /// <returns>
        /// <para>
        /// If the window is a child window, the return value is a handle to the parent window.
        /// If the window is a top-level window with the <c>WS_POPUP</c> style, the return value is a handle to the owner window.
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
        /// the <c>WS_POPUP</c> style.
        /// </item>
        /// <item>
        /// The owner window has <c>WS_POPUP</c> style.
        /// </item>
        /// </list>
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// To obtain a window's owner window, instead of using <c>GetParent</c>,
        /// use <c>GetWindow</c> with the <c>GW_OWNER</c> flag. To obtain the parent window and
        /// not the owner, instead of using <c>GetParent</c>, use <c>GetAncestor</c> with
        /// the <c>GA_PARENT</c> flag.
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
        /// Displays a modal dialog box that contains a system icon, a set of buttons, and a brief application-specific message, such as status or error information. The message box returns an integer value that indicates which button the user clicked.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the owner window of the message box to be created. If this parameter is NULL, the message box has no owner window.
        /// </param>
        /// <param name="text">
        /// The message to be displayed. If the string consists of more than one line, you can separate the lines using a carriage return and/or linefeed character between each line.
        /// </param>
        /// <param name="caption">
        /// The dialog box title. If this parameter is NULL, the default title is Error.
        /// </param>
        /// <param name="type">
        /// The contents and behavior of the dialog box. This parameter can be a combination of flags from the following groups of flags.
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
          [In] Rect* lpRect,
          [In] BOOL bErase
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern LRESULT DispatchMessageW(
            [In] Message* lpMsg
        );


        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL GetMessageW(
              [Out] Message* lpMsg,
              [In, Optional] HWND hWnd,
              [In] UINT wMsgFilterMin,
              [In] UINT wMsgFilterMax
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL PeekMessageW(
              [Out] Message* lpMsg,
              [In, Optional] HWND hWnd,
              [In] uint wMsgFilterMin,
              [In] uint wMsgFilterMax,
              [In] uint wRemoveMsg
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
