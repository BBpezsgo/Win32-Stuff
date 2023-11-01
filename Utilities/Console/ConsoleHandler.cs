using System.Runtime.InteropServices;

namespace Win32.Utilities
{
    public static class ConsoleHandler
    {
        static uint SavedMode;
        static HWND stdinHandle = Kernel32.INVALID_HANDLE_VALUE;
        static HWND stdoutHandle = Kernel32.INVALID_HANDLE_VALUE;

        public static HWND InputHandle => stdinHandle;
        public static HWND OutputHandle => stdoutHandle;

        /// <exception cref="WindowsException"/>
        public static void Setup()
        {
            stdinHandle = Kernel32.GetStdHandle(StdHandle.STD_INPUT_HANDLE);
            if (stdinHandle == Kernel32.INVALID_HANDLE_VALUE)
            { throw WindowsException.Get(); }

            stdoutHandle = Kernel32.GetStdHandle(StdHandle.STD_OUTPUT_HANDLE);
            if (stdoutHandle == Kernel32.INVALID_HANDLE_VALUE)
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
            { throw WindowsException.Get(6); }

            if (Kernel32.SetConsoleMode(stdinHandle, SavedMode) == 0)
            { throw WindowsException.Get(); }
        }

        const int FixedWidthTrueType = 54;

        /// <exception cref="WindowsException"/>
        public static CONSOLE_FONT_INFOEX Font
        {
            set
            {
                if (Kernel32.SetCurrentConsoleFontEx(stdoutHandle, FALSE, ref value) == 0)
                { throw WindowsException.Get(); }
            }
            get
            {
                CONSOLE_FONT_INFOEX fontInfo = CONSOLE_FONT_INFOEX.Create();

                if (Kernel32.GetCurrentConsoleFontEx(stdoutHandle, FALSE, ref fontInfo) == 0)
                { throw WindowsException.Get(); }

                return fontInfo;
            }
        }

        /// <exception cref="WindowsException"/>
        public static void SetFont(string font, short fontSize)
        {
            CONSOLE_FONT_INFOEX fontInfo = ConsoleHandler.Font;

            fontInfo.FaceName = font;
            fontInfo.FontSize = fontSize;

            ConsoleHandler.Font = fontInfo;
        }

        public static CONSOLE_FONT_INFOEX DefaultFont
        {
            get
            {
                CONSOLE_FONT_INFOEX result = CONSOLE_FONT_INFOEX.Create();
                result.FontIndex = 0;
                result.FontFamily = FixedWidthTrueType;
                result.FaceName = "Consolas";
                result.FontWeight = 400;
                result.FontSize = 16;
                result.FontWidth = 8;
                return result;
            }
        }
    }
}
