using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Contains global cursor information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct CursorInfo
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// The caller must set this to <see langword="sizeof"/>(<see cref="CURSORINFO"/>).
        /// </summary>
        public readonly DWORD cbSize;
        /// <summary>
        /// The cursor state. This parameter can be one of the following values.
        /// <list type="table">
        /// 
        /// <item>
        /// <term>
        /// 0
        /// </term>
        /// <description>
        /// The cursor is hidden.
        /// </description>
        /// </item>
        /// 
        /// <item>
        /// <term>
        /// CURSOR_SHOWING = 0x00000001
        /// </term>
        /// <description>
        /// The cursor is showing.
        /// </description>
        /// </item>
        /// 
        /// <item>
        /// <term>
        /// CURSOR_SUPPRESSED = 0x00000002
        /// </term>
        /// <description>
        /// <b>Windows 8:</b> The cursor is suppressed.
        /// This flag indicates that the system is
        /// not drawing the cursor because the user
        /// is providing input through touch or pen
        /// instead of the mouse.
        /// </description>
        /// </item>
        /// 
        /// </list>
        /// </summary>
        public readonly DWORD flags;
        /// <summary>
        /// A handle to the cursor.
        /// </summary>
        public readonly HCURSOR hCursor;
        /// <summary>
        /// A structure that receives the screen coordinates of the cursor.
        /// </summary>
        public readonly POINT ptScreenPos;

        CursorInfo(uint cbSize) : this() => this.cbSize = cbSize;

        unsafe public static CURSORINFO Create() => new((uint)sizeof(CURSORINFO));
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct GuiThreadInfo
    {
        public readonly DWORD cbSize;
        public readonly DWORD flags;
        public readonly HWND hwndActive;
        public readonly HWND hwndFocus;
        public readonly HWND hwndCapture;
        public readonly HWND hwndMenuOwner;
        public readonly HWND hwndMoveSize;
        public readonly HWND hwndCaret;
        public readonly RECT rcCaret;

        GuiThreadInfo(uint cbSize) : this() => this.cbSize = cbSize;

        unsafe public static GUITHREADINFO Create() => new((uint)sizeof(GUITHREADINFO));
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct NMHDR
    {
        public readonly HWND hwndFrom;
        public readonly UINT idFrom;
        public readonly UINT code;
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ProgressBarRange
    {
        public readonly int Low;
        public readonly int High;

        public ProgressBarRange(int low, int high)
        {
            Low = low;
            High = high;
        }

        public static implicit operator ValueTuple<int, int>(PBRANGE v) => (v.Low, v.High);
        public static implicit operator PBRANGE(ValueTuple<int, int> v) => (v.Item1, v.Item2);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    unsafe public struct StartupInfo
    {
        public DWORD cb;
        public WCHAR* lpReserved;
        public WCHAR* lpDesktop;
        public WCHAR* lpTitle;
        public DWORD dwX;
        public DWORD dwY;
        public DWORD dwXSize;
        public DWORD dwYSize;
        public DWORD dwXCountChars;
        public DWORD dwYCountChars;
        public DWORD dwFillAttribute;
        public DWORD dwFlags;
        public WORD wShowWindow;
        public WORD cbReserved2;
        public BYTE* lpReserved2;
        public HANDLE hStdInput;
        public HANDLE hStdOutput;
        public HANDLE hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct SECURITY_ATTRIBUTES
    {
        public DWORD nLength;
        public void* lpSecurityDescriptor;
        public BOOL bInheritHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct CreateStruct
    {
        public void* lpCreateParams;
        public HINSTANCE hInstance;
        public HMENU hMenu;
        public HWND hwndParent;
        public int cy;
        public int cx;
        public int y;
        public int x;
        public LONG style;
        public char* lpszName;
        public char* lpszClass;
        public DWORD dwExStyle;
    }

    public static class CB
    {
        public const int CB_OKAY = 0;
        public const int CB_ERR = -1;
        public const int CB_ERRSPACE = -2;

        public const uint CB_GETEDITSEL = 0x0140;
        public const uint CB_LIMITTEXT = 0x0141;
        public const uint CB_SETEDITSEL = 0x0142;
        public const uint CB_ADDSTRING = 0x0143;
        public const uint CB_DELETESTRING = 0x0144;
        public const uint CB_DIR = 0x0145;
        public const uint CB_GETCOUNT = 0x0146;
        public const uint CB_GETCURSEL = 0x0147;
        public const uint CB_GETLBTEXT = 0x0148;
        public const uint CB_GETLBTEXTLEN = 0x0149;
        public const uint CB_INSERTSTRING = 0x014A;
        public const uint CB_RESETCONTENT = 0x014B;
        public const uint CB_FINDSTRING = 0x014C;
        public const uint CB_SELECTSTRING = 0x014D;
        public const uint CB_SETCURSEL = 0x014E;
        public const uint CB_SHOWDROPDOWN = 0x014F;
        public const uint CB_GETITEMDATA = 0x0150;
        public const uint CB_SETITEMDATA = 0x0151;
        public const uint CB_GETDROPPEDCONTROLRECT = 0x0152;
        public const uint CB_SETITEMHEIGHT = 0x0153;
        public const uint CB_GETITEMHEIGHT = 0x0154;
        public const uint CB_SETEXTENDEDUI = 0x0155;
        public const uint CB_GETEXTENDEDUI = 0x0156;
        public const uint CB_GETDROPPEDSTATE = 0x0157;
        public const uint CB_FINDSTRINGEXACT = 0x0158;
        public const uint CB_SETLOCALE = 0x0159;
        public const uint CB_GETLOCALE = 0x015A;

        public const uint CB_GETTOPINDEX = 0x015b;
        public const uint CB_SETTOPINDEX = 0x015c;
        public const uint CB_GETHORIZONTALEXTENT = 0x015d;
        public const uint CB_SETHORIZONTALEXTENT = 0x015e;
        public const uint CB_GETDROPPEDWIDTH = 0x015f;
        public const uint CB_SETDROPPEDWIDTH = 0x0160;
        public const uint CB_INITSTORAGE = 0x0161;

        public const uint CB_MULTIPLEADDSTRING = 0x0163;
    }

    public static class CBS
    {
        public const DWORD CBS_SIMPLE = 0x0001;
        public const DWORD CBS_DROPDOWN = 0x0002;
        public const DWORD CBS_DROPDOWNLIST = 0x0003;
        public const DWORD CBS_OWNERDRAWFIXED = 0x0010;
        public const DWORD CBS_OWNERDRAWVARIABLE = 0x0020;
        public const DWORD CBS_AUTOHSCROLL = 0x0040;
        public const DWORD CBS_OEMCONVERT = 0x0080;
        public const DWORD CBS_SORT = 0x0100;
        public const DWORD CBS_HASSTRINGS = 0x0200;
        public const DWORD CBS_NOINTEGRALHEIGHT = 0x0400;
        public const DWORD CBS_DISABLENOSCROLL = 0x0800;
        public const DWORD CBS_UPPERCASE = 0x2000;
        public const DWORD CBS_LOWERCASE = 0x4000;
    }

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

    public static class GWL
    {
        /// <summary>
        /// Retrieves the extended window styles.
        /// </summary>
        public const int GWL_EXSTYLE = -20;
        /// <summary>
        /// Retrieves a handle to the application instance.
        /// </summary>
        public const int GWL_HINSTANCE = -6;
        /// <summary>
        /// Retrieves a handle to the parent window, if any.
        /// </summary>
        public const int GWL_HWNDPARENT = -8;
        /// <summary>
        /// Retrieves the identifier of the window.
        /// </summary>
        public const int GWL_ID = -12;
        /// <summary>
        /// Retrieves the window styles.
        /// </summary>
        public const int GWL_STYLE = -16;
        /// <summary>
        /// Retrieves the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
        /// </summary>
        public const int GWL_USERDATA = -21;
        /// <summary>
        /// Retrieves the address of the window procedure, or a handle representing the address of the window procedure. You must use the CallWindowProc function to call the window procedure.
        /// </summary>
        public const int GWL_WNDPROC = -4;
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

    [StructLayout(LayoutKind.Sequential)]
    public struct PaintStruct
    {
        public HDC hdc;
        public bool fErase;
        public RECT rcPaint;
        public bool fRestore;
        public bool fIncUpdate;
        public readonly int rgbReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowClassEx
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

    /// <summary>
    /// The <see cref="POINT"/> structure defines the x- and y-coordinates of a point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct Point
    {
        /// <summary>
        /// Specifies the x-coordinate of the point.
        /// </summary>
        public int X;
        /// <summary>
        /// Specifies the y-coordinate of the point.
        /// </summary>
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static explicit operator Coord(POINT p) => new(p.X, p.Y);
        public static explicit operator POINT(Coord p) => new(p.X, p.Y);

        public static POINT Empty => new(0, 0);

        public override readonly string ToString()
            => $"({X}, {Y})";
    }

    public struct Color
    {
        public const DWORD Red = 0x000000FF;
        public const DWORD Green = 0x0000FF00;
        public const DWORD Blue = 0x00FF0000;
        public const DWORD Black = 0x00000000;
        public const DWORD White = 0x00FFFFFF;
    }

    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct Rect
    {
        public LONG Left;
        public LONG Top;
        public LONG Right;
        public LONG Bottom;

        public readonly POINT Position => new(Math.Min(Left, Right), Math.Min(Top, Bottom));

        public readonly LONG Width => Math.Max(Left, Right) - Math.Min(Left, Right);
        public readonly LONG Height => Math.Max(Top, Bottom) - Math.Min(Top, Bottom);

        public Rect(LONG top, LONG left, LONG bottom, LONG right)
        {
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
        }

        public override readonly string ToString()
            => $"{{ Left: {Left} Top: {Top} Bottom: {Bottom} Right: {Right} }}";
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
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct Coord : IEquatable<Coord>
    {
        /// <summary>
        /// The horizontal coordinate or column value. The units depend on the function call.
        /// </summary>
        public SHORT X;
        /// <summary>
        /// The vertical coordinate or row value. The units depend on the function call.
        /// </summary>
        public SHORT Y;

        public Coord(SHORT x, SHORT y)
        {
            X = x;
            Y = y;
        }
        public Coord(int x, int y) : this((SHORT)x, (SHORT)y) { }
        public Coord(System.Drawing.Point p) : this((SHORT)p.X, (SHORT)p.Y) { }
        public Coord(System.Drawing.PointF p) : this((SHORT)p.X, (SHORT)p.Y) { }

        public override readonly bool Equals(object? obj) =>
            obj is Coord coord &&
            Equals(coord);

        public readonly bool Equals(Coord other) =>
            X == other.X &&
            Y == other.Y;

        public override readonly int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(Coord a, Coord b) => a.Equals(b);
        public static bool operator !=(Coord a, Coord b) => !(a == b);

        public override readonly string ToString()
            => $"({X}, {Y})";
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct CharInfo
    {
        [FieldOffset(0)] public char Char;
        [FieldOffset(2)] public WORD Attributes;

        const WORD MASK_FG = 0b_0000_1111;
        const WORD MASK_BG = 0b_1111_0000;

        public byte Foreground
        {
            readonly get => (byte)(Attributes & MASK_FG);
            set => Attributes = (ushort)((Attributes & MASK_BG) | (value & MASK_FG));
        }

        public byte Background
        {
            readonly get => (byte)(Attributes >> 4);
            set => Attributes = (ushort)((Attributes & MASK_FG) | ((value << 4) & MASK_BG));
        }

        public CharInfo(char @char, WORD attributes)
        {
            Char = @char;
            Attributes = attributes;
        }

        public CharInfo(char @char) : this(@char, 0)
        { }

        public CharInfo(char @char, byte foreground, byte background) : this(@char, (WORD)((foreground & MASK_FG) | ((background << 4) & MASK_BG)))
        { }

        public override readonly bool Equals(object? obj) => obj is CharInfo charInfo && Equals(charInfo);
        public readonly bool Equals(CharInfo other) =>
            Attributes == other.Attributes &&
            Char == other.Char;

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

        public short X
        {
            readonly get => Left;
            set => Left = value;
        }

        public short Y
        {
            readonly get => Top;
            set => Top = value;
        }

        public short Width
        {
            readonly get => (short)(Right - Left + 1);
            set => Right = (short)(Left + value);
        }
        public short Height
        {
            readonly get => (short)(Bottom - Top + 1);
            set => Bottom = (short)(Top + value);
        }

        public static SmallRect Zero => default;

        public readonly Point Location => new(Left, Top);

        public static SmallRect FromPosAndSize(int x, int y, int width, int height) => new()
        {
            Left = (short)x,
            Top = (short)y,
            Bottom = (short)(y + height),
            Right = (short)(x + width),
        };

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

        public readonly bool Contains(Point point) =>
            point.X >= Left &&
            point.Y >= Top &&
            point.X < Right &&
            point.Y < Height;

        public readonly bool Contains(int x, int y) =>
            x >= Left &&
            y >= Top &&
            x < Right &&
            y < Height;
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
