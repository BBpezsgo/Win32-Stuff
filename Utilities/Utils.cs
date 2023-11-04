using System.Diagnostics;

namespace Win32
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

    public struct MouseMetrics
    {
        public static int MouseButtonCount => User32.GetSystemMetrics(SM.CMOUSEBUTTONS);
        public static bool IsMousePresent => User32.GetSystemMetrics(SM.MOUSEPRESENT) != 0;
        public static bool IsMouseHWheelPresent => User32.GetSystemMetrics(SM.MOUSEHORIZONTALWHEELPRESENT) != 0;
        public static bool IsMouseVWheelPresent => User32.GetSystemMetrics(SM.MOUSEWHEELPRESENT) != 0;
    }

    public struct DisplayMetrics
    {
        public static int DisplayMonitorCount => User32.GetSystemMetrics(SM.CMONITORS);

        public static int Width => User32.GetSystemMetrics(SM.CXSCREEN);
        public static int Height => User32.GetSystemMetrics(SM.CYSCREEN);
        public static SIZE Size => new(Width, Height);
        public static RECT Rect => new(0, 0, Width, Height);
    }

    public struct WindowMetrics
    {
        public static int BorderWidth => User32.GetSystemMetrics(SM.CXBORDER);
        public static int BorderHeight => User32.GetSystemMetrics(SM.CYBORDER);
        public static SIZE BorderSize => new(BorderWidth, BorderHeight);

        public static int MinimumWidth => User32.GetSystemMetrics(SM.CXMIN);
        public static int MinimumHeight => User32.GetSystemMetrics(SM.CYMIN);
        public static SIZE MinimumSize => new(MinimumWidth, MinimumHeight);

        public static int TitleBarButtonWidth => User32.GetSystemMetrics(SM.CXSIZE);
        public static int TitleBarButtonHeight => User32.GetSystemMetrics(SM.CYSIZE);
        public static SIZE TitleBarButtonSize => new(TitleBarButtonWidth, TitleBarButtonHeight);


        public static int MaximizedWidth => User32.GetSystemMetrics(SM.CXMAXIMIZED);
        public static int MaximizedHeight => User32.GetSystemMetrics(SM.CYMAXIMIZED);
        public static SIZE MaximizedSize => new(MaximizedWidth, MaximizedHeight);

        public static int MinimizedWidth => User32.GetSystemMetrics(SM.CXMINIMIZED);
        public static int MinimizedHeight => User32.GetSystemMetrics(SM.CYMINIMIZED);
        public static SIZE MinimizedSize => new(MinimizedWidth, MinimizedHeight);
    }

    public struct SystemMetrics
    {
        public static int FullScreenWidth => User32.GetSystemMetrics(SM.CXFULLSCREEN);
        public static int FullScreenHeight => User32.GetSystemMetrics(SM.CYFULLSCREEN);
        public static SIZE FullScreenSize => new(FullScreenWidth, FullScreenHeight);

        public static int SystemBootMode => User32.GetSystemMetrics(SM.CLEANBOOT);

        public static int CursorWidth => User32.GetSystemMetrics(SM.CXCURSOR);
        public static int CursorHeight => User32.GetSystemMetrics(SM.CYCURSOR);
        public static SIZE CursorSize => new(CursorWidth, CursorHeight);

        public static bool IsDbcsEnabled => User32.GetSystemMetrics(SM.DBCSENABLED) != 0;
        public static bool IsDebug => User32.GetSystemMetrics(SM.DEBUG) != 0;
        public static bool IsNetworkPresent => (User32.GetSystemMetrics(SM.NETWORK) & 1) != 0;
        public static bool IsShuttingDown => User32.GetSystemMetrics(SM.SHUTTINGDOWN) != 0;
        public static bool IsSlowMachine => User32.GetSystemMetrics(SM.SLOWMACHINE) != 0;
    }

    public static class Utils
    {
        public const DebuggerBrowsableState GlobalDebuggerBrowsable = DebuggerBrowsableState.Collapsed;

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

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public static bool IsGuiThread => User32.IsGUIThread(FALSE) != 0;

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public static uint DoubleClickTime => User32.GetDoubleClickTime();

        public static void BlockInput(bool block) => _ = User32.BlockInput(block ? TRUE : FALSE);

        public static bool SoundSentry() => User32.SoundSentry() != FALSE;
    }
}
