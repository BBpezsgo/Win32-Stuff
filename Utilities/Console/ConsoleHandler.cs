namespace Win32.Utilities
{
    public static class ConsoleHandler
    {
        static uint SavedMode;
        static HWND stdinHandle = Kernel32.INVALID_HANDLE_VALUE;

        public static HWND Handle => stdinHandle;

        /// <exception cref="WindowsException"/>
        public static void Setup()
        {
            stdinHandle = Kernel32.GetStdHandle(StdHandle.STD_INPUT_HANDLE);

            if (stdinHandle == Kernel32.INVALID_HANDLE_VALUE)
            { throw WindowsException.Get(); }

            uint mode = 0;
            if (Kernel32.GetConsoleMode(stdinHandle, ref mode) == 0)
            { throw WindowsException.Get(); }

            SavedMode = mode;

            mode &= ~InputMode.ENABLE_QUICK_EDIT_MODE;
            mode |= InputMode.ENABLE_WINDOW_INPUT;
            mode |= InputMode.ENABLE_MOUSE_INPUT;

            if (Kernel32.SetConsoleMode(stdinHandle, mode) == 0)
            { throw WindowsException.Get(); }

            Console.CursorVisible = false;
        }

        /// <exception cref="WindowsException"/>
        public static void Restore()
        {
            Console.CursorVisible = true;

            if (stdinHandle == Kernel32.INVALID_HANDLE_VALUE)
            { throw new WindowsException(6); }

            if (Kernel32.SetConsoleMode(stdinHandle, SavedMode) == 0)
            { throw WindowsException.Get(); }
        }
    }
}
