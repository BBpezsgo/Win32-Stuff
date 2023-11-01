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

    public enum ForegroundColor : WORD
    {
        Black = 0,
        DarkGray = CharInfoAttributes.FOREGROUND_BRIGHT,
        Gray = CharInfoAttributes.FOREGROUND_RED | CharInfoAttributes.FOREGROUND_GREEN | CharInfoAttributes.FOREGROUND_BLUE,
        White = CharInfoAttributes.FOREGROUND_RED | CharInfoAttributes.FOREGROUND_GREEN | CharInfoAttributes.FOREGROUND_BLUE | CharInfoAttributes.FOREGROUND_BRIGHT,
        Default = Gray,

        DarkRed = CharInfoAttributes.FOREGROUND_RED,
        DarkGreen = CharInfoAttributes.FOREGROUND_GREEN,
        DarkBlue = CharInfoAttributes.FOREGROUND_BLUE,
        DarkYellow = CharInfoAttributes.FOREGROUND_RED | CharInfoAttributes.FOREGROUND_GREEN,
        DarkCyan = CharInfoAttributes.FOREGROUND_BLUE | CharInfoAttributes.FOREGROUND_GREEN,
        DarkMagenta = CharInfoAttributes.FOREGROUND_RED | CharInfoAttributes.FOREGROUND_BLUE,

        Red = CharInfoAttributes.FOREGROUND_RED | CharInfoAttributes.FOREGROUND_BRIGHT,
        Green = CharInfoAttributes.FOREGROUND_GREEN | CharInfoAttributes.FOREGROUND_BRIGHT,
        Blue = CharInfoAttributes.FOREGROUND_BLUE | CharInfoAttributes.FOREGROUND_BRIGHT,
        Yellow = CharInfoAttributes.FOREGROUND_RED | CharInfoAttributes.FOREGROUND_GREEN | CharInfoAttributes.FOREGROUND_BRIGHT,
        Cyan = CharInfoAttributes.FOREGROUND_BLUE | CharInfoAttributes.FOREGROUND_GREEN | CharInfoAttributes.FOREGROUND_BRIGHT,
        Magenta = CharInfoAttributes.FOREGROUND_RED | CharInfoAttributes.FOREGROUND_BLUE | CharInfoAttributes.FOREGROUND_BRIGHT,
    }

    public enum BackgroundColor : WORD
    {
        Black = 0,
        DarkGray = CharInfoAttributes.BACKGROUND_BRIGHT,
        Gray = CharInfoAttributes.BACKGROUND_RED | CharInfoAttributes.BACKGROUND_GREEN | CharInfoAttributes.BACKGROUND_BLUE,
        White = CharInfoAttributes.BACKGROUND_RED | CharInfoAttributes.BACKGROUND_GREEN | CharInfoAttributes.BACKGROUND_BLUE | CharInfoAttributes.BACKGROUND_BRIGHT,
        Default = Black,

        DarkRed = CharInfoAttributes.BACKGROUND_RED,
        DarkGreen = CharInfoAttributes.BACKGROUND_GREEN,
        DarkBlue = CharInfoAttributes.BACKGROUND_BLUE,
        DarkYellow = CharInfoAttributes.BACKGROUND_RED | CharInfoAttributes.BACKGROUND_GREEN,
        DarkCyan = CharInfoAttributes.BACKGROUND_BLUE | CharInfoAttributes.BACKGROUND_GREEN,
        DarkMagenta = CharInfoAttributes.BACKGROUND_RED | CharInfoAttributes.BACKGROUND_BLUE,

        Red = CharInfoAttributes.BACKGROUND_RED | CharInfoAttributes.BACKGROUND_BRIGHT,
        Green = CharInfoAttributes.BACKGROUND_GREEN | CharInfoAttributes.BACKGROUND_BRIGHT,
        Blue = CharInfoAttributes.BACKGROUND_BLUE | CharInfoAttributes.BACKGROUND_BRIGHT,
        Yellow = CharInfoAttributes.BACKGROUND_RED | CharInfoAttributes.BACKGROUND_GREEN | CharInfoAttributes.BACKGROUND_BRIGHT,
        Cyan = CharInfoAttributes.BACKGROUND_BLUE | CharInfoAttributes.BACKGROUND_GREEN | CharInfoAttributes.BACKGROUND_BRIGHT,
        Magenta = CharInfoAttributes.BACKGROUND_RED | CharInfoAttributes.BACKGROUND_BLUE | CharInfoAttributes.BACKGROUND_BRIGHT,
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
