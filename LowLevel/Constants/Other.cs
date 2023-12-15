namespace Win32
{
    public struct Color
    {
        public const DWORD Red = 0x000000FF;
        public const DWORD Green = 0x0000FF00;
        public const DWORD Blue = 0x00FF0000;
        public const DWORD Black = 0x00000000;
        public const DWORD White = 0x00FFFFFF;
    }

    public enum ConsoleForegroundColor : WORD
    {
        Black = 0,
        Gray = ConsoleCharAttributes.ForegroundBright,
        Silver = ConsoleCharAttributes.ForegroundRed | ConsoleCharAttributes.ForegroundGreen | ConsoleCharAttributes.ForegroundBlue,
        White = ConsoleCharAttributes.ForegroundRed | ConsoleCharAttributes.ForegroundGreen | ConsoleCharAttributes.ForegroundBlue | ConsoleCharAttributes.ForegroundBright,
        Default = Silver,

        DarkRed = ConsoleCharAttributes.ForegroundRed,
        DarkGreen = ConsoleCharAttributes.ForegroundGreen,
        DarkBlue = ConsoleCharAttributes.ForegroundBlue,
        DarkYellow = ConsoleCharAttributes.ForegroundRed | ConsoleCharAttributes.ForegroundGreen,
        DarkCyan = ConsoleCharAttributes.ForegroundBlue | ConsoleCharAttributes.ForegroundGreen,
        DarkMagenta = ConsoleCharAttributes.ForegroundRed | ConsoleCharAttributes.ForegroundBlue,

        Red = ConsoleCharAttributes.ForegroundRed | ConsoleCharAttributes.ForegroundBright,
        Green = ConsoleCharAttributes.ForegroundGreen | ConsoleCharAttributes.ForegroundBright,
        Blue = ConsoleCharAttributes.ForegroundBlue | ConsoleCharAttributes.ForegroundBright,
        Yellow = ConsoleCharAttributes.ForegroundRed | ConsoleCharAttributes.ForegroundGreen | ConsoleCharAttributes.ForegroundBright,
        Cyan = ConsoleCharAttributes.ForegroundBlue | ConsoleCharAttributes.ForegroundGreen | ConsoleCharAttributes.ForegroundBright,
        Magenta = ConsoleCharAttributes.ForegroundRed | ConsoleCharAttributes.ForegroundBlue | ConsoleCharAttributes.ForegroundBright,
    }

    public enum ConsoleBackgroundColor : WORD
    {
        Black = 0,
        Gray = ConsoleCharAttributes.BackgroundBright,
        Silver = ConsoleCharAttributes.BackgroundRed | ConsoleCharAttributes.BackgroundGreen | ConsoleCharAttributes.BackgroundBlue,
        White = ConsoleCharAttributes.BackgroundRed | ConsoleCharAttributes.BackgroundGreen | ConsoleCharAttributes.BackgroundBlue | ConsoleCharAttributes.BackgroundBright,
        Default = Black,

        DarkRed = ConsoleCharAttributes.BackgroundRed,
        DarkGreen = ConsoleCharAttributes.BackgroundGreen,
        DarkBlue = ConsoleCharAttributes.BackgroundBlue,
        DarkYellow = ConsoleCharAttributes.BackgroundRed | ConsoleCharAttributes.BackgroundGreen,
        DarkCyan = ConsoleCharAttributes.BackgroundBlue | ConsoleCharAttributes.BackgroundGreen,
        DarkMagenta = ConsoleCharAttributes.BackgroundRed | ConsoleCharAttributes.BackgroundBlue,

        Red = ConsoleCharAttributes.BackgroundRed | ConsoleCharAttributes.BackgroundBright,
        Green = ConsoleCharAttributes.BackgroundGreen | ConsoleCharAttributes.BackgroundBright,
        Blue = ConsoleCharAttributes.BackgroundBlue | ConsoleCharAttributes.BackgroundBright,
        Yellow = ConsoleCharAttributes.BackgroundRed | ConsoleCharAttributes.BackgroundGreen | ConsoleCharAttributes.BackgroundBright,
        Cyan = ConsoleCharAttributes.BackgroundBlue | ConsoleCharAttributes.BackgroundGreen | ConsoleCharAttributes.BackgroundBright,
        Magenta = ConsoleCharAttributes.BackgroundRed | ConsoleCharAttributes.BackgroundBlue | ConsoleCharAttributes.BackgroundBright,
    }

    public struct InputMode
    {
        public const uint ENABLE_MOUSE_INPUT = 0x0010;
        public const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        public const uint ENABLE_EXTENDED_FLAGS = 0x0080;
        public const uint ENABLE_ECHO_INPUT = 0x0004;
        public const uint ENABLE_WINDOW_INPUT = 0x0008;
    }
}
