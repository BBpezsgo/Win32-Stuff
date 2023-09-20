using System.Runtime.InteropServices;

namespace Win32
{
    public static class User32
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern LRESULT SendMessage(
              [In] HWND hWnd,
              [In] uint Msg,
              [In] WPARAM wParam,
              [In] LPARAM lParam
            );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetWindowLongPtrW(
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
          [In] uint uType
        );

        unsafe public static MessageBoxResult MessageBox(
           HWND hWnd,
           string lbText,
           string lpCaption,
           uint uType)
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
          [In] uint Msg,
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
              [In] uint wMsgFilterMin,
              [In] uint wMsgFilterMax
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
            [In] uint Msg,
            [In] WPARAM wParam,
            [In] LPARAM lParam
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern LRESULT RegisterClassExW(
            [In] HWND hWnd,
            [In] uint Msg,
            [In] WPARAM wParam,
            [In] LPARAM lParam);

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern ATOM RegisterClassExW(
            [In] WNDCLASSEXW* unnamedParam1);

    }
}
