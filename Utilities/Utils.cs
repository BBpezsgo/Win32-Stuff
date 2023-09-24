namespace Win32.Utilities
{
    public static class Cursor
    {
        /// <summary>
        /// Retrieves information about the global cursor.
        /// </summary>
        /// <exception cref="WindowsException"/>
        unsafe public static CURSORINFO GetCursorInfo()
        {
            CURSORINFO result = CURSORINFO.Create();
            if (User32.GetCursorInfo(&result) == FALSE)
            { throw WindowsException.Get(); }
            return result;
        }
    }

    public static class Utils
    {
        /// <summary>
        /// Retrieves the identifier of the thread that created the
        /// specified window.
        /// </summary>
        /// <param name="windowHandle">
        /// A handle to the window.
        /// </param>
        /// <exception cref="WindowsException"/>
        unsafe public static void GetWindowThreadId(this HWND windowHandle, out DWORD threadId)
        {
            threadId = User32.GetWindowThreadProcessId(windowHandle, null);

            if (threadId == 0)
            { throw WindowsException.Get(); }
        }

        /// <summary>
        /// Retrieves the identifier of the thread that created the
        /// specified window and the identifier of the process that created the window.
        /// </summary>
        /// <param name="windowHandle">
        /// A handle to the window.
        /// </param>
        /// <exception cref="WindowsException"/>
        unsafe public static void GetWindowThreadAndProcessId(this HWND windowHandle, out DWORD threadId, out DWORD processId)
        {
            DWORD processId_ = 0;

            threadId = User32.GetWindowThreadProcessId(windowHandle, &processId_);
            processId = processId_;

            if (threadId == 0)
            { throw WindowsException.Get(); }
        }

        /// <summary>
        /// Retrieves information about the active window or a specified GUI thread.
        /// </summary>
        /// <param name="threadId">
        /// The identifier for the thread for which information is to be retrieved.
        /// To retrieve this value, use the <see cref="GetWindowThreadProcessId"/> function.
        /// If this parameter is <c>NULL</c>, the function returns information for the foreground thread.
        /// </param>
        unsafe public static GUITHREADINFO GetGUIThreadInfo(DWORD threadId)
        {
            GUITHREADINFO result = GUITHREADINFO.Create();
            if (User32.GetGUIThreadInfo(threadId, &result) == FALSE)
            { throw WindowsException.Get(); }
            return result;
        }
    }
}
