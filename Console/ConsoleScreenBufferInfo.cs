﻿namespace Win32.Console;

/// <summary>
/// Contains information about a console screen buffer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ConsoleScreenBufferInfo
{
    /// <summary>
    /// A <see cref="COORD"/> structure that contains the size of the
    /// console screen buffer, in character columns and rows.
    /// </summary>
    public COORD Size;
    /// <summary>
    /// A <see cref="COORD"/> structure that contains the column and
    /// row coordinates of the cursor in the console screen buffer.
    /// </summary>
    public COORD CursorPosition;
    /// <summary>
    /// The attributes of the characters written to a screen buffer
    /// by the <see cref="Kernel32.WriteFile"/> and <see cref="Kernel32.WriteConsole"/> functions, or echoed
    /// to a screen buffer by the <see cref="Kernel32.ReadFile"/> and <see cref="Kernel32.ReadConsole"/> functions.
    /// For more information, see
    /// <see href="https://learn.microsoft.com/en-us/windows/console/console-screen-buffers#character-attributes">Character Attributes</see>.
    /// </summary>
    public WORD Attributes;
    /// <summary>
    /// A <see cref="SMALL_RECT"/> structure that contains the console screen buffer
    /// coordinates of the upper-left and lower-right corners of the display window.
    /// </summary>
    public SMALL_RECT Window;
    /// <summary>
    /// A <see cref="COORD"/> structure that contains the maximum size of the console window,
    /// in character columns and rows, given the current screen
    /// buffer size and font and the screen size.
    /// </summary>
    public COORD MaximumWindowSize;
}
