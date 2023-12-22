namespace Win32
{
#pragma warning disable CS1574
    public static class InputMode
    {
        /// <summary>
        /// Characters read by the <see cref="Kernel32.ReadFile"/> or
        /// <see cref="Kernel32.ReadConsole"/> function are written
        /// to the active screen buffer as they are typed into the console.
        /// This mode can be used only if the
        /// <see cref="ENABLE_LINE_INPUT"/> mode is also enabled.
        /// </summary>
        public const DWORD ENABLE_ECHO_INPUT = 0x0004;

        /// <summary>
        ///  When enabled, text entered in a console window will be
        ///  inserted at the current cursor location and all text
        ///  following that location will not be overwritten. When
        ///  disabled, all following text will be overwritten.
        /// </summary>
        public const DWORD ENABLE_INSERT_MODE = 0x0020;

        /// <summary>
        /// The <see cref="Kernel32.ReadFile"/> or <see cref="Kernel32.ReadConsole"/>
        /// function returns only when a carriage
        /// return character is read. If this mode is disabled, the functions
        /// return when one or more characters are available.
        /// </summary>
        public const DWORD ENABLE_LINE_INPUT = 0x0002;

        /// <summary>
        /// If the mouse pointer is within the borders of the console window
        /// and the window has the keyboard focus, mouse events generated
        /// by mouse movement and button presses are placed in the input
        /// buffer. These events are discarded by <see cref="Kernel32.ReadFile"/>
        /// or <see cref="Kernel32.ReadConsole"/>, even when this mode
        /// is enabled. The <see cref="Kernel32.ReadConsoleInput"/> function
        /// can be used to read <see cref="MouseEvent"/> input records
        /// from the input buffer.
        /// </summary>
        public const DWORD ENABLE_MOUSE_INPUT = 0x0010;

        /// <summary>
        /// <c>CTRL+C</c> is processed by the system and is not placed
        /// in the input buffer. If the input buffer is being read by
        /// <see cref="Kernel32.ReadFile"/> or <see cref="Kernel32.ReadConsole"/>,
        /// other control keys are processed by the system and are not returned
        /// in the <see cref="Kernel32.ReadFile"/> or <see cref="Kernel32.ReadConsole"/>
        /// buffer. If the <see cref="ENABLE_LINE_INPUT"/> mode is also
        /// enabled, backspace, carriage return, and line feed characters
        /// are handled by the system.
        /// </summary>
        public const DWORD ENABLE_PROCESSED_INPUT = 0x0001;

        /// <summary>
        /// This flag enables the user to use the mouse to select and edit text.
        /// To enable this mode, use
        /// <see cref="ENABLE_QUICK_EDIT_MODE"/> | <see cref="ENABLE_EXTENDED_FLAGS"/>.
        /// To disable this mode, use <see cref="ENABLE_EXTENDED_FLAGS"/> without this flag.
        /// </summary>
        public const DWORD ENABLE_QUICK_EDIT_MODE = 0x0040;

        /// <summary>
        /// User interactions that change the size of the console screen buffer
        /// are reported in the console's input buffer. Information about these
        /// events can be read from the input buffer by applications using the
        /// <see cref="Kernel32.ReadConsoleInput"/> function, but not by those
        /// using <see cref="Kernel32.ReadFile"/> or
        /// <see cref="Kernel32.ReadConsole"/>.
        /// </summary>
        public const DWORD ENABLE_WINDOW_INPUT = 0x0008;

        /// <summary>
        /// 
        /// </summary>
        public const DWORD ENABLE_EXTENDED_FLAGS = 0x0080;

        /// <summary>
        /// <para>
        /// Setting this flag directs the Virtual Terminal processing engine
        /// to convert user input received by the console window into
        /// <see href="https://learn.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences">Console Virtual Terminal Sequences</see>
        /// that can be retrieved by a supporting application through
        /// <see cref="Kernel32.ReadFile"/> or <see cref="Kernel32.ReadConsole"/> functions.
        /// </para>
        /// <para>
        /// The typical usage of this flag is intended in conjunction with
        /// <see cref="ENABLE_VIRTUAL_TERMINAL_PROCESSING"/> on the output
        /// handle to connect to an application that communicates
        /// exclusively via virtual terminal sequences.
        /// </para>
        /// </summary>
        public const DWORD ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200;


















        /// <summary>
        /// Characters written by the <see cref="Kernel32.WriteFile"/>
        /// or <see cref="Kernel32.WriteConsole"/> function or echoed
        /// by the <see cref="Kernel32.ReadFile"/> or
        /// <see cref="Kernel32.ReadConsole"/> function are parsed for
        /// ASCII control sequences, and the correct action is performed.
        /// Backspace, tab, bell, carriage return, and line feed
        /// characters are processed. It should be enabled when using
        /// control sequences or when <see cref="ENABLE_VIRTUAL_TERMINAL_PROCESSING"/>
        /// is set.
        /// </summary>
        public const DWORD ENABLE_PROCESSED_OUTPUT = 0x0001;

        /// <summary>
        /// When writing with <see cref="Kernel32.WriteFile"/> or
        /// <see cref="Kernel32.WriteConsole"/> or echoing with
        /// <see cref="Kernel32.ReadFile"/> or <see cref="Kernel32.ReadConsole"/>,
        /// the cursor moves to the beginning of the next row when it
        /// reaches the end of the current row. This causes the rows
        /// displayed in the console window to scroll up automatically
        /// when the cursor advances beyond the last row in the window.
        /// It also causes the contents of the console screen buffer to
        /// scroll up (../discarding the top row of the console screen
        /// buffer) when the cursor advances beyond the last row in the
        /// console screen buffer. If this mode is disabled, the last
        /// character in the row is overwritten with any subsequent characters.
        /// </summary>
        public const DWORD ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002;

        /// <summary>
        /// <para>
        /// When writing with <see cref="Kernel32.WriteFile"/> or
        /// <see cref="Kernel32.WriteConsole"/>, characters are parsed
        /// for VT100 and similar control character sequences that
        /// control cursor movement, color/font mode, and other
        /// operations that can also be performed via the existing
        /// Console APIs. For more information, see
        /// <see href="https://learn.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences">Console Virtual Terminal Sequences</see>.
        /// </para>
        /// <para>
        /// Ensure <see cref="ENABLE_PROCESSED_OUTPUT"/> is set
        /// when using this flag.
        /// </para>
        /// </summary>
        public const DWORD ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        /// <summary>
        /// <para>
        /// When writing with <see cref="Kernel32.WriteFile"/> or
        /// <see cref="Kernel32.WriteConsole"/>, this adds an additional
        /// state to end-of-line wrapping that can delay the cursor move
        /// and buffer scroll operations.
        /// </para>
        /// <para>
        /// Normally when <see cref="ENABLE_WRAP_AT_EOL_OUTPUT"/> is
        /// set and text reaches the end of the line, the cursor will
        /// immediately move to the next line and the contents of the
        /// buffer will scroll up by one line. In contrast with this
        /// flag set, the cursor does not move to the next line, and the
        /// scroll operation is not performed. The written character will
        /// be printed in the final position on the line and the cursor
        /// will remain above this character as if <see cref="ENABLE_WRAP_AT_EOL_OUTPUT"/>
        /// was off, but the next printable character will be printed as if
        /// <see cref="ENABLE_WRAP_AT_EOL_OUTPUT"/> is on. No overwrite will
        /// occur. Specifically, the cursor quickly advances down to the
        /// following line, a scroll is performed if necessary, the
        /// character is printed, and the cursor advances one more position.
        /// </para>
        /// <para>
        /// The typical usage of this flag is intended in conjunction
        /// with setting <see cref="ENABLE_VIRTUAL_TERMINAL_PROCESSING"/>
        /// to better emulate a terminal emulator where writing the final
        /// character on the screen (../in the bottom right corner) without
        /// triggering an immediate scroll is the desired behavior.
        /// </para>
        /// </summary>
        public const DWORD DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        /// <summary>
        /// <para>
        /// The APIs for writing character attributes including
        /// <see cref="Kernel32.WriteConsoleOutput"/> and
        /// <see cref="Kernel32.WriteConsoleOutputAttribute"/> allow
        /// the usage of flags from character attributes to adjust the
        /// color of the foreground and background of text. Additionally,
        /// a range of DBCS flags was specified with the COMMON_LVB prefix.
        /// Historically, these flags only functioned in DBCS code pages
        /// for Chinese, Japanese, and Korean languages.
        /// </para>
        /// <para>
        /// With exception of the leading byte and trailing byte
        /// flags, the remaining flags describing line drawing and reverse
        /// video (../swap foreground and background colors) can be useful
        /// for other languages to emphasize portions of output.
        /// </para>
        /// <para>
        /// Setting this console mode flag will allow these attributes to
        /// be used in every code page on every language.
        /// </para>
        /// <para>
        /// It is off by default to maintain compatibility with known
        /// applications that have historically taken advantage of the
        /// console ignoring these flags on non-CJK machines to store
        /// bits in these fields for their own purposes or by accident.
        /// </para>
        /// <para>
        /// Note that using the <see cref="ENABLE_VIRTUAL_TERMINAL_PROCESSING"/>
        /// mode can result in LVB grid and reverse video flags being set while
        /// this flag is still off if the attached application requests
        /// underlining or inverse video via
        /// <see href="https://learn.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences">Console Virtual Terminal Sequences</see>.
        /// </para>
        /// </summary>
        public const DWORD ENABLE_LVB_GRID_WORLDWIDE = 0x0010;
    }
}
