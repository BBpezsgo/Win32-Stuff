namespace Win32
{
    public static class ConsoleHandler
    {
        static uint SavedMode;
        static HWND stdinHandle = Kernel32.INVALID_HANDLE_VALUE;
        static HWND stdoutHandle = Kernel32.INVALID_HANDLE_VALUE;

        /// <exception cref="WindowsException"/>
        public static HWND InputHandle 
        {
            get
            {
                if (stdinHandle == Kernel32.INVALID_HANDLE_VALUE)
                { stdinHandle = Kernel32.GetStdHandle(StdHandle.STD_INPUT_HANDLE); }

                if (stdinHandle == Kernel32.INVALID_HANDLE_VALUE)
                { throw WindowsException.Get(); }
                
                return stdinHandle;
            }
        }
        /// <exception cref="WindowsException"/>
        public static HWND OutputHandle
        {
            get
            {
                if (stdoutHandle == Kernel32.INVALID_HANDLE_VALUE)
                { stdoutHandle = Kernel32.GetStdHandle(StdHandle.STD_OUTPUT_HANDLE); }

                if (stdoutHandle == Kernel32.INVALID_HANDLE_VALUE)
                { throw WindowsException.Get(); }

                return stdoutHandle;
            }
        }

        /// <exception cref="WindowsException"/>
        public static uint InputFlags
        {
            get
            {
                uint mode = default;

                if (Kernel32.GetConsoleMode(InputHandle, ref mode) == FALSE)
                { throw WindowsException.Get(); }

                return mode;
            }
            set
            {
                if (Kernel32.SetConsoleMode(InputHandle, value) == FALSE)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        public static uint OutputFlags
        {
            get
            {
                uint mode = default;

                if (Kernel32.GetConsoleMode(OutputHandle, ref mode) == FALSE)
                { throw WindowsException.Get(); }

                return mode;
            }
            set
            {
                if (Kernel32.SetConsoleMode(OutputHandle, value) == FALSE)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        public static void Setup()
        {
            uint mode = 0;
            if (Kernel32.GetConsoleMode(InputHandle, ref mode) == 0)
            { throw WindowsException.Get(); }

            SavedMode = mode;

            mode &= ~InputMode.ENABLE_QUICK_EDIT_MODE;
            mode |= InputMode.ENABLE_WINDOW_INPUT;
            mode |= InputMode.ENABLE_MOUSE_INPUT;

            if (Kernel32.SetConsoleMode(InputHandle, mode) == 0)
            { throw WindowsException.Get(); }

            Console.CursorVisible = false;
        }

        /// <exception cref="WindowsException"/>
        public static void Restore()
        {
            Console.CursorVisible = true;

            if (InputHandle == Kernel32.INVALID_HANDLE_VALUE)
            { throw WindowsException.Get(6); }

            if (Kernel32.SetConsoleMode(InputHandle, SavedMode) == 0)
            { throw WindowsException.Get(); }
        }

        const int FixedWidthTrueType = 54;

        /// <exception cref="WindowsException"/>
        public static ConsoleFontInfoEx Font
        {
            set
            {
                if (Kernel32.SetCurrentConsoleFontEx(OutputHandle, FALSE, ref value) == 0)
                { throw WindowsException.Get(); }
            }
            get
            {
                ConsoleFontInfoEx fontInfo = ConsoleFontInfoEx.Create();

                if (Kernel32.GetCurrentConsoleFontEx(OutputHandle, FALSE, ref fontInfo) == 0)
                { throw WindowsException.Get(); }

                return fontInfo;
            }
        }

        /// <exception cref="WindowsException"/>
        public static void SetFont(string font, short fontSize)
        {
            ConsoleFontInfoEx fontInfo = ConsoleHandler.Font;

            fontInfo.FaceName = font;
            fontInfo.FontSize = fontSize;

            ConsoleHandler.Font = fontInfo;
        }

        public static ConsoleFontInfoEx DefaultFont
        {
            get
            {
                ConsoleFontInfoEx result = ConsoleFontInfoEx.Create();
                result.FontIndex = 0;
                result.FontFamily = FixedWidthTrueType;
                result.FaceName = "Consolas";
                result.FontWeight = 400;
                result.FontSize = 16;
                result.FontWidth = 8;
                return result;
            }
        }

        /// <exception cref="WindowsException"/>
        unsafe public static ConsoleScreenBufferInfo ScreenBufferInfo
        {
            get
            {
                ConsoleScreenBufferInfo result = default;
                if (Kernel32.GetConsoleScreenBufferInfo(OutputHandle, &result) == FALSE)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        public static short WindowLeft => ScreenBufferInfo.Window.Left;
        public static short WindowTop => ScreenBufferInfo.Window.Top;
        public static short WindowWidth => (short)(ScreenBufferInfo.Window.Right - ScreenBufferInfo.Window.Left + 1);
        public static short WindowHeight => (short)(ScreenBufferInfo.Window.Bottom - ScreenBufferInfo.Window.Top + 1);
    }
}
