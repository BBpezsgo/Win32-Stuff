namespace Win32.Console;

[SupportedOSPlatform("windows")]
public static class Terminal
{
    static uint SavedMode;
    static HWND stdinHandle = Kernel32.InvalidHandle;
    static HWND stdoutHandle = Kernel32.InvalidHandle;

    /// <exception cref="WindowsException"/>
    public static HWND InputHandle
    {
        get
        {
            if (stdinHandle == Kernel32.InvalidHandle)
            { stdinHandle = Kernel32.GetStdHandle(StdHandle.Input); }

            if (stdinHandle == Kernel32.InvalidHandle)
            { throw WindowsException.Get(); }

            return stdinHandle;
        }
    }
    /// <exception cref="WindowsException"/>
    public static HWND OutputHandle
    {
        get
        {
            if (stdoutHandle == Kernel32.InvalidHandle)
            { stdoutHandle = Kernel32.GetStdHandle(StdHandle.Output); }

            if (stdoutHandle == Kernel32.InvalidHandle)
            { throw WindowsException.Get(); }

            return stdoutHandle;
        }
    }

    /// <exception cref="WindowsException"/>
    public static uint InputFlags
    {
        get
        {
            if (Kernel32.GetConsoleMode(InputHandle, out uint mode) == FALSE)
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
            if (Kernel32.GetConsoleMode(OutputHandle, out uint mode) == FALSE)
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
        if (Kernel32.GetConsoleMode(InputHandle, out uint mode) == 0)
        { throw WindowsException.Get(); }

        SavedMode = mode;

        mode &= ~InputMode.EnableQuickEditMode;
        mode |= InputMode.EnableWindowInput;
        mode |= InputMode.EnableMouseInput;

        if (Kernel32.SetConsoleMode(InputHandle, mode) == 0)
        { throw WindowsException.Get(); }

        System.Console.CursorVisible = false;
    }

    /// <exception cref="WindowsException"/>
    public static void Restore()
    {
        System.Console.CursorVisible = true;

        if (InputHandle == Kernel32.InvalidHandle)
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
            if (Kernel32.SetCurrentConsoleFontEx(OutputHandle, FALSE, in value) == 0)
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
    public static unsafe void SetFont(string font, short fontSize)
    {
        fixed (WCHAR* fontPtr = font)
        {
            ConsoleFontInfoEx fontInfo = Terminal.Font;
            Marshal.Copy(font.ToCharArray(), 0, (nint)fontInfo.FaceName, Math.Min(32, font.Length));
            fontInfo.FontSize = fontSize;
            Terminal.Font = fontInfo;
        }
    }

    public static unsafe ConsoleFontInfoEx DefaultFont
    {
        get
        {
            fixed (WCHAR* faceNamePtr = "Consolas")
            {
                ConsoleFontInfoEx result = ConsoleFontInfoEx.Create();
                result.FontIndex = 0;
                result.FontFamily = FixedWidthTrueType;
                Marshal.Copy("Consolas".ToCharArray(), 0, (nint)result.FaceName, Math.Min(32, "Consolas".Length));
                result.FontWeight = 400;
                result.FontSize = 16;
                result.FontWidth = 8;
                return result;
            }
        }
    }

    /// <exception cref="WindowsException"/>
    public static unsafe ConsoleScreenBufferInfo ScreenBufferInfo
    {
        get
        {
            if (Kernel32.GetConsoleScreenBufferInfo(OutputHandle, out ConsoleScreenBufferInfo result) == FALSE)
            { throw WindowsException.Get(); }
            return result;
        }
    }

    /// <exception cref="WindowsException"/>
    public static short WindowLeft => ScreenBufferInfo.Window.Left;
    /// <exception cref="WindowsException"/>
    public static short WindowTop => ScreenBufferInfo.Window.Top;
    /// <exception cref="WindowsException"/>
    public static short WindowWidth => ScreenBufferInfo.Window.Width;
    /// <exception cref="WindowsException"/>
    public static short WindowHeight => ScreenBufferInfo.Window.Height;
}
