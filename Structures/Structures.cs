using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    public static class PM
    {
        /// <summary>
        /// Messages are not removed from the queue after processing by PeekMessage.
        /// </summary>
        public const uint PM_NOREMOVE = 0x0000;
        /// <summary>
        /// Messages are removed from the queue after processing by PeekMessage.
        /// </summary>
        public const uint PM_REMOVE = 0x0001;
        /// <summary>
        /// <para>
        /// Prevents the system from releasing any thread that is waiting for the caller to go idle (see WaitForInputIdle).
        /// </para>
        /// <para>
        /// Combine this value with either PM_NOREMOVE or PM_REMOVE.
        /// </para>
        /// </summary>
        public const uint PM_NOYIELD = 0x0002;
    }

    public enum MessageBoxButton : uint
    {
        /// <summary>
        /// The message box contains three push buttons: Abort, Retry, and Ignore.
        /// </summary>
        MB_ABORTRETRYIGNORE = 0x00000002,

        /// <summary>
        /// The message box contains three push buttons: Cancel, Try Again, Continue. Use this message box type instead of MB_ABORTRETRYIGNORE.
        /// </summary>
        MB_CANCELTRYCONTINUE = 0x00000006,

        /// <summary>
        /// Adds a Help button to the message box. When the user clicks the Help button or presses F1, the system sends a WM_HELP message to the owner.
        /// </summary>
        MB_HELP = 0x00004000,

        /// <summary>
        /// The message box contains one push button: OK. This is the default.
        /// </summary>
        MB_OK = 0x00000000,

        /// <summary>
        /// The message box contains two push buttons: OK and Cancel.
        /// </summary>
        MB_OKCANCEL = 0x00000001,

        /// <summary>
        /// The message box contains two push buttons: Retry and Cancel.
        /// </summary>
        MB_RETRYCANCEL = 0x00000005,

        /// <summary>
        /// The message box contains two push buttons: Yes and No.
        /// </summary>
        MB_YESNO = 0x00000004,

        /// <summary>
        /// The message box contains three push buttons: Yes, No, and Cancel.
        /// </summary>
        MB_YESNOCANCEL = 0x00000003,
    }

    public enum MessageBoxIcon : uint
    {
        /// <summary>
        /// An exclamation-point icon appears in the message box.
        /// </summary>
        MB_ICONEXCLAMATION = 0x00000030,

        /// <summary>
        /// An exclamation-point icon appears in the message box.
        /// </summary>
        MB_ICONWARNING = 0x00000030,

        /// <summary>
        /// An icon consisting of a lowercase letter i in a circle appears in the message box.
        /// </summary>
        MB_ICONINFORMATION = 0x00000040,

        /// <summary>
        /// An icon consisting of a lowercase letter i in a circle appears in the message box.
        /// </summary>
        MB_ICONASTERISK = 0x00000040,

        /// <summary>
        /// A question-mark icon appears in the message box. The question-mark message icon is no longer recommended because it does not clearly represent a specific type of message and because the phrasing of a message as a question could apply to any message type. In addition, users can confuse the message symbol question mark with Help information. Therefore, do not use this question mark message symbol in your message boxes. The system continues to support its inclusion only for backward compatibility.
        /// </summary>
        MB_ICONQUESTION = 0x00000020,

        /// <summary>
        /// A stop-sign icon appears in the message box.
        /// </summary>
        MB_ICONSTOP = 0x00000010,

        /// <summary>
        /// A stop-sign icon appears in the message box.
        /// </summary>
        MB_ICONERROR = 0x00000010,

        /// <summary>
        /// A stop-sign icon appears in the message box.
        /// </summary>
        MB_ICONHAND = 0x00000010,
    }

    public enum MessageBoxDefaultButton : uint
    {
        /// <summary>
        /// The first button is the default button.
        /// MB_DEFBUTTON1 is the default unless MB_DEFBUTTON2, MB_DEFBUTTON3, or MB_DEFBUTTON4 is specified.
        /// </summary>
        MB_DEFBUTTON1 = 0x00000000,

        /// <summary>
        /// The second button is the default button.
        /// </summary>
        MB_DEFBUTTON2 = 0x00000100,

        /// <summary>
        /// The third button is the default button.
        /// </summary>
        MB_DEFBUTTON3 = 0x00000200,

        /// <summary>
        /// The fourth button is the default button.
        /// </summary>
        MB_DEFBUTTON4 = 0x00000300,
    }

    public enum MessageBoxModality : uint
    {
        /// <summary>
        /// <para>
        /// The user must respond to the message box before continuing work in the window identified by the hWnd parameter. However, the user can move to the windows of other threads and work in those windows.
        /// </para>
        /// <para>
        /// Depending on the hierarchy of windows in the application, the user may be able to move to other windows within the thread. All child windows of the parent of the message box are automatically disabled, but pop-up windows are not.
        /// </para>
        /// <para>
        /// MB_APPLMODAL is the default if neither MB_SYSTEMMODAL nor MB_TASKMODAL is specified.
        /// </para>
        /// </summary>
        MB_APPLMODAL = 0x00000000,

        /// <summary>
        /// Same as MB_APPLMODAL except that the message box has the WS_EX_TOPMOST style. Use system-modal message boxes to notify the user of serious, potentially damaging errors that require immediate attention (for example, running out of memory). This flag has no effect on the user's ability to interact with windows other than those associated with hWnd.
        /// </summary>
        MB_SYSTEMMODAL = 0x00001000,
        /// <summary>
        /// Same as MB_APPLMODAL except that all the top-level windows belonging to the current thread are disabled if the hWnd parameter is NULL.Use this flag when the calling application or library does not have a window handle available but still needs to prevent input to other windows in the calling thread without suspending other threads.
        /// </summary>
        MB_TASKMODAL = 0x00002000,
    }

    public enum MessageBoxResult : int
    {
        /// <summary>
        /// The Abort button was selected.
        /// </summary>
        IDABORT = 3,

        /// <summary>
        /// The Cancel button was selected.
        /// </summary>
        IDCANCEL = 2,

        /// <summary>
        /// The Continue button was selected.
        /// </summary>
        IDCONTINUE = 11,

        /// <summary>
        /// The Ignore button was selected.
        /// </summary>
        IDIGNORE = 5,

        /// <summary>
        /// The No button was selected.
        /// </summary>
        IDNO = 7,

        /// <summary>
        /// The OK button was selected.
        /// </summary>
        IDOK = 1,

        /// <summary>
        /// The Retry button was selected.
        /// </summary>
        IDRETRY = 4,

        /// <summary>
        /// The Try Again button was selected.
        /// </summary>
        IDTRYAGAIN = 10,

        /// <summary>
        /// The Yes button was selected.
        /// </summary>
        IDYES = 6,
    }

    public struct FormatMessageAttributes
    {
        /// <remarks>
        /// <para>
        /// The function allocates a buffer large enough to hold the formatted message, and places a pointer to the allocated buffer at the address specified by <c>lpBuffer</c>. The <c>lpBuffer</c> parameter is a pointer to an <c>LPTSTR</c>; you must cast the pointer to an <c>LPTSTR</c>. The nSize parameter specifies the minimum number of <c>TCHAR</c>s to allocate for an output message buffer. The caller should use the <c>LocalFree</c> function to free the buffer when it is no longer needed.
        /// </para>
        /// <para>
        /// If the length of the formatted message exceeds 128K bytes, then <see cref="FormatMessage"/> will fail and a subsequent call to <see cref="GetLastError"/> will return <c>ERROR_MORE_DATA</c>.
        /// </para>
        /// <para>
        /// In previous versions of Windows, this value was not available for use when compiling Windows Store apps. As of Windows 10 this value can be used.
        /// </para>
        /// <para>
        /// <b>Windows Server 2003 and Windows XP:</b><br/>
        /// If the length of the formatted message exceeds 128K bytes, then <see cref="FormatMessage"/> will not automatically fail with an error of <c>ERROR_MORE_DATA</c>.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;
        public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
        /// <remarks>
        /// <para>
        /// The <c>lpSource</c> parameter is a pointer to a null-terminated string that contains a message definition. The message definition may contain insert sequences, just as the message text in a message table resource may. This flag cannot be used with <see cref="FORMAT_MESSAGE_FROM_HMODULE"/> or <see cref="FORMAT_MESSAGE_FROM_SYSTEM"/>.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_FROM_STRING = 1024;
        /// <remarks>
        /// <para>
        /// The <c>lpSource</c> parameter is a module handle containing the message-table resource(s) to search. If this <c>lpSource</c> handle is <see langword="null"/>, the current process's application image file will be searched. This flag cannot be used with <see cref="FORMAT_MESSAGE_FROM_STRING"/>.
        /// </para>
        /// <para>
        /// If the module has no message table resource, the function fails with <c>ERROR_RESOURCE_TYPE_NOT_FOUND</c>.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_FROM_HMODULE = 2048;
        /// <remarks>
        /// <para>
        /// The function should search the system message-table resource(s) for the requested message. If this flag is specified with <see cref="FORMAT_MESSAGE_FROM_HMODULE"/>, the function searches the system message table if the message is not found in the module specified by <c>lpSource</c>. This flag cannot be used with <see cref="FORMAT_MESSAGE_FROM_STRING"/>.
        /// </para>
        /// <para>
        /// If this flag is specified, an application can pass the result of the <see cref="GetLastError"/> function to retrieve the message text for a system-defined error.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
        /// <remarks>
        /// <para>
        /// The <i>Arguments</i> parameter is not a <c>va_list</c> structure, but is a pointer to an array of values that represent the arguments.
        /// </para>
        /// <para>
        /// This flag cannot be used with 64-bit integer values. If you are using a 64-bit integer, you must use the <c>va_list</c> structure.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
        public const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 255;
    }

    public struct PaintStruct
    {
        public HDC hdc;
        public bool fErase;
        public Rect rcPaint;
        public bool fRestore;
        public bool fIncUpdate;
#pragma warning disable IDE0051 // Remove unused private members
        readonly int rgbReserved;
#pragma warning restore IDE0051 // Remove unused private members
    }

    public struct WNDCLASSEXW
    {
        public uint cbSize;
        public uint style;
        unsafe public delegate*<HWND, uint, WPARAM, LPARAM, LRESULT> lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public HINSTANCE hInstance;
        public HICON hIcon;
        public HCURSOR hCursor;
        public HBRUSH hbrBackground;
        unsafe public char* lpszMenuName;
        unsafe public char* lpszClassName;
        public HICON hIconSm;
    }

    public struct Point
    {
        public int X;
        public int Y;

        public static Point Empty => new() { X = 0, Y = 0, };
    }

    public struct Color
    {
        public const DWORD Red = 0x000000FF;
        public const DWORD Green = 0x0000FF00;
        public const DWORD Blue = 0x00FF0000;
        public const DWORD Black = 0x00000000;
        public const DWORD White = 0x00FFFFFF;
    }

    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public static class WS
    {
        public const uint BORDER = 0x00800000;
        public const uint CAPTION = 0x00C00000;
        public const uint CHILD = 0x40000000;
        public const uint CHILDWINDOW = 0x40000000;
        public const uint CLIPCHILDREN = 0x02000000;
        public const uint CLIPSIBLINGS = 0x04000000;
        public const uint DISABLED = 0x08000000;
        public const uint DLGFRAME = 0x00400000;
        public const uint GROUP = 0x00020000;
        public const uint HSCROLL = 0x00100000;
        public const uint ICONIC = 0x20000000;
        public const uint MAXIMIZE = 0x01000000;
        public const uint MAXIMIZEBOX = 0x00010000;
        public const uint MINIMIZE = 0x20000000;
        public const uint MINIMIZEBOX = 0x00020000;
        public const uint OVERLAPPED = 0x00000000;
        public const uint OVERLAPPEDWINDOW = (OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX);
        public const uint POPUP = 0x80000000;
        public const uint POPUPWINDOW = (POPUP | BORDER | SYSMENU);
        public const uint SIZEBOX = 0x00040000;
        public const uint SYSMENU = 0x00080000;
        public const uint TABSTOP = 0x00010000;
        public const uint THICKFRAME = 0x00040000;
        public const uint TILED = 0x00000000;
        public const uint TILEDWINDOW = (OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX);
        public const uint VISIBLE = 0x10000000;
        public const uint VSCROLL = 0x00200000;
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

    [Flags]
    public enum CharInfoAttributes : WORD
    {
        FOREGROUND_BLUE = 0x0001,           // File color contains blue.
        FOREGROUND_GREEN = 0x0002,          // File color contains green.
        FOREGROUND_RED = 0x0004,            // File color contains red.
        FOREGROUND_BRIGHT = 0x0008,         // File color is intensified.

        BACKGROUND_BLUE = 0x0010,           // Background color contains blue.
        BACKGROUND_GREEN = 0x0020,          // Background color contains green.
        BACKGROUND_RED = 0x0040,            // Background color contains red.
        BACKGROUND_BRIGHT = 0x0080,         // Background color is intensified.

        COMMON_LVB_LEADING_BYTE = 0x0100,   // Leading byte.
        COMMON_LVB_TRAILING_BYTE = 0x0200,  // Trailing byte.
        COMMON_LVB_GRID_HORIZONTAL = 0x0400,// Top horizontal.
        COMMON_LVB_GRID_LVERTICAL = 0x0800, // Left vertical.
        COMMON_LVB_GRID_RVERTICAL = 0x1000, // Right vertical.
        COMMON_LVB_REVERSE_VIDEO = 0x4000,  // Reverse foreground and background attribute.
        COMMON_LVB_UNDERSCORE = 0x8000,     // Underscore.
    }

    /// <summary>
    /// Defines the coordinates of a character cell in a console screen buffer. The origin of the coordinate system (0,0) is at the top, left cell of the buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        /// <summary>
        /// The horizontal coordinate or column value. The units depend on the function call.
        /// </summary>
        public short X;
        /// <summary>
        /// The vertical coordinate or row value. The units depend on the function call.
        /// </summary>
        public short Y;

        public Coord(short x, short y)
        {
            this.X = x;
            this.Y = y;
        }
        public Coord(int x, int y) : this((short)x, (short)y)
        { }
        public Coord(System.Drawing.Point p) : this((short)p.X, (short)p.Y)
        { }
        public Coord(System.Drawing.PointF p) : this((short)p.X, (short)p.Y)
        { }

        public override readonly bool Equals(object? obj) =>
            obj is Coord coord &&
            Equals(coord);
        public readonly bool Equals(Coord other) =>
            this.X == other.X &&
            this.Y == other.Y;

        public override readonly int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(Coord a, Coord b) => a.Equals(b);
        public static bool operator !=(Coord a, Coord b) => !(a == b);

        public override readonly string ToString()
            => $"{{ {X} ; {Y} }}";
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct CharUnion
    {
        [FieldOffset(0)] public char UnicodeChar;
        // [FieldOffset(0)] public byte AsciiChar;

        public CharUnion(char @char)
        {
            UnicodeChar = @char;
            // AsciiChar = 0;
        }

        public override readonly bool Equals(object? obj) => obj is CharUnion charUnion && Equals(charUnion);
        public readonly bool Equals(CharUnion other) =>
            this.UnicodeChar == other.UnicodeChar;

        public override readonly int GetHashCode() => HashCode.Combine(UnicodeChar);

        public static bool operator ==(CharUnion a, CharUnion b) => a.Equals(b);
        public static bool operator !=(CharUnion a, CharUnion b) => !(a == b);

        public override readonly string ToString()
            => $"{{ UnicodeChar: '{UnicodeChar}' }}";
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)] public CharUnion Char;
        [FieldOffset(2)] public WORD Attributes;

        public CharInfo(CharUnion @char, WORD attributes)
        {
            this.Char = @char;
            this.Attributes = attributes;
        }
        public CharInfo(char @char, WORD attributes) : this(new CharUnion(@char), attributes)
        { }

        public CharInfo(CharUnion @char) : this(@char, 0)
        { }
        public CharInfo(char @char) : this(new CharUnion(@char), 0)
        { }

        public CharInfo(CharUnion @char, ForegroundColor fg, BackgroundColor bg) : this(@char, (WORD)((int)fg | (int)bg))
        { }
        public CharInfo(char @char, ForegroundColor fg, BackgroundColor bg) : this(new CharUnion(@char), fg, bg)
        { }

        public ForegroundColor ForegroundColor
        {
            readonly get => (ForegroundColor)(Attributes & (0x0001 | 0x0002 | 0x0004 | 0x0008));
            set => Attributes = (WORD)((int)BackgroundColor & (int)value);
        }

        public BackgroundColor BackgroundColor
        {
            readonly get => (BackgroundColor)(Attributes & (0x0010 | 0x0020 | 0x0040 | 0x0080));
            set => Attributes = (WORD)((int)ForegroundColor & (int)value);
        }

        public override readonly bool Equals(object? obj) => obj is CharInfo charInfo && Equals(charInfo);
        public readonly bool Equals(CharInfo other) =>
            this.Attributes == other.Attributes &&
            this.Char == other.Char;

        public override readonly int GetHashCode() => HashCode.Combine(Attributes, Char);

        public static bool operator ==(CharInfo a, CharInfo b) => a.Equals(b);
        public static bool operator !=(CharInfo a, CharInfo b) => !(a == b);

        public override readonly string ToString()
            => $"{{ Attributes: {Attributes} Char: {Char} }}";
    }

    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct SmallRect : IEquatable<SmallRect>
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;

        public readonly short Width => (short)(Right - Left + 1);
        public readonly short Height => (short)(Bottom - Top + 1);

        public override readonly bool Equals(object? obj) =>
            obj is SmallRect rect
            && Equals(rect);
        public readonly bool Equals(SmallRect other) =>
            Left == other.Left &&
            Top == other.Top &&
            Right == other.Right &&
            Bottom == other.Bottom;

        public override readonly int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);

        public static bool operator ==(SmallRect a, SmallRect b) => a.Equals(b);
        public static bool operator !=(SmallRect a, SmallRect b) => !(a == b);

        public override readonly string ToString()
            => $"{{ Left: {Left} Top: {Top} Bottom: {Bottom} Right: {Right} }}";
    }

    public struct InputMode
    {
        internal const uint
            ENABLE_MOUSE_INPUT = 0x0010,
            ENABLE_QUICK_EDIT_MODE = 0x0040,
            ENABLE_EXTENDED_FLAGS = 0x0080,
            ENABLE_ECHO_INPUT = 0x0004,
            ENABLE_WINDOW_INPUT = 0x0008;

        public static void Default(ref uint mode)
        {
            mode &= ~InputMode.ENABLE_QUICK_EDIT_MODE;
            mode |= InputMode.ENABLE_WINDOW_INPUT;
            mode |= InputMode.ENABLE_MOUSE_INPUT;
        }
    }
}
