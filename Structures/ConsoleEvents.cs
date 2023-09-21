using System.Runtime.InteropServices;

namespace Win32
{
    public struct EventType
    {
        public const ushort KEY = 0x0001;
        public const ushort MOUSE = 0x0002;
        public const ushort WINDOW_BUFFER_SIZE = 0x0004;
    }

    public struct ControlKeyState
    {
        /// <summary>
        /// The CAPS LOCK light is on.
        /// </summary>
        public const uint CAPSLOCK_ON = 0x0080;
        /// <summary>
        /// The key is enhanced.See remarks.
        /// </summary>
        public const uint ENHANCED_KEY = 0x0100;
        /// <summary>
        /// The left ALT key is pressed.
        /// </summary>
        public const uint LEFT_ALT_PRESSED = 0x0002;
        /// <summary>
        /// The left CTRL key is pressed.
        /// </summary>
        public const uint LEFT_CTRL_PRESSED = 0x0008;
        /// <summary>
        /// The NUM LOCK light is on.
        /// </summary>
        public const uint NUMLOCK_ON = 0x0020;
        /// <summary>
        /// The right ALT key is pressed.
        /// </summary>
        public const uint RIGHT_ALT_PRESSED = 0x0001;
        /// <summary>
        /// The right CTRL key is pressed.
        /// </summary>
        public const uint RIGHT_CTRL_PRESSED = 0x0004;
        /// <summary>
        /// The SCROLL LOCK light is on.
        /// </summary>
        public const uint SCROLLLOCK_ON = 0x0040;
        /// <summary>
        /// The SHIFT key is pressed.
        /// </summary>
        public const uint SHIFT_PRESSED = 0x0010;
    }

    public enum MouseButtonState : ulong
    {
        Left = 0x0001,
        Right = 0x0002,
        Middle = 0x0004,
        Button3 = 0x0008,
        Button4 = 0x0010,
        ScrollUp = 7864320,
        ScrollDown = 4287102976,
    }

    public struct MouseEventFlags
    {
        /// <summary>
        /// The second click(button press) of a double-click occurred.The first click is returned as a regular button-press event.
        /// </summary>
        public const uint DOUBLE_CLICK = 0x0002;
        /// <summary>
        /// <para>
        /// The horizontal mouse wheel was moved.
        /// </para>
        /// <para>
        /// If the high word of the dwButtonState member contains a positive value, the wheel was rotated to the right. Otherwise, the wheel was rotated to the left.
        /// </para>
        /// </summary>
        public const uint MOUSE_HWHEELED = 0x0008;
        /// <summary>
        /// A change in mouse position occurred.
        /// </summary>
        public const uint MOUSE_MOVED = 0x0001;
        /// <summary>
        /// <para>
        /// The vertical mouse wheel was moved.
        /// </para>
        /// <para>
        /// 
        /// If the high word of the dwButtonState member contains a positive value, the wheel was rotated forward, away from the user. Otherwise, the wheel was rotated backward, toward the user.</para>
        /// </summary>
        public const uint MOUSE_WHEELED = 0x0004;
    }

    /// <summary>
    /// Describes an input event in the console input buffer. These records can be read from the input buffer by using the <see cref="Kernel32.ReadConsoleInput"/> or <see cref="Kernel32.PeekConsoleInput"/> function, or written to the input buffer by using the <see cref="Kernel32.WriteConsoleInput"/> function.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct InputEvent
    {
        /// <summary>
        ///  <list type="table">
        ///   <item>
        ///    <term>KEY_EVENT 0x0001</term>
        ///    <description>
        ///     The Event member contains a <see cref="KeyEvent"/> structure with information about a keyboard event.
        ///    </description>
        ///   </item>
        ///   <item>
        ///    <term>MOUSE_EVENT 0x0002</term>
        ///    <description>
        ///     The Event member contains a <see cref="MouseEvent"/> structure with information about a mouse movement or button press event.
        ///    </description>
        ///   </item>
        ///   <item>
        ///    <term>WINDOW_BUFFER_SIZE_EVENT 0x0004</term>
        ///    <description>
        ///     The Event member contains a <see cref="WindowBufferSizeEvent"/> structure with information about the new size of the console screen buffer.
        ///    </description>
        ///   </item>
        ///   <item>
        ///    <term>MENU_EVENT 0x0008</term>
        ///    <description>
        ///     The Event member contains a <c>MENU_EVENT_RECORD</c> structure. These events are used internally and should be ignored.
        ///    </description>
        ///   </item>
        ///   <item>
        ///    <term>FOCUS_EVENT 0x0010</term>
        ///    <description>
        ///     The Event member contains a <c>FOCUS_EVENT_RECORD</c> structure. These events are used internally and should be ignored. 
        ///    </description>
        ///   </item>
        ///  </list>
        /// </summary>
        [FieldOffset(0)]
        public readonly WORD EventType;

        [FieldOffset(4)]
        public readonly KeyEvent KeyEvent;

        [FieldOffset(4)]
        public readonly MouseEvent MouseEvent;

        [FieldOffset(4)]
        public readonly WindowBufferSizeEvent WindowBufferSizeEvent;

        /*
        [FieldOffset(4)]
        public readonly MENU_EVENT_RECORD MenuEvent;
        [FieldOffset(4)]
        public readonly FOCUS_EVENT_RECORD FocusEvent;
        */
    }

    /// <summary>
    /// Describes a mouse input event in a console <see cref="InputEvent"/> structure.
    /// </summary>
    public readonly struct MouseEvent
    {
        /// <summary>
        /// A <see cref="Coord"/> structure that contains the location of the cursor, in terms of the console screen buffer's character-cell coordinates.
        /// </summary>
        public readonly Coord MousePosition;

        /// <summary>
        /// The status of the mouse buttons. The least significant bit corresponds to the leftmost mouse button. The next least significant bit corresponds to the rightmost mouse button. The next bit indicates the next-to-leftmost mouse button. The bits then correspond left to right to the mouse buttons. A bit is 1 if the button was pressed.
        /// </summary>
        public readonly DWORD ButtonState;

        /// <summary>
        /// The state of the control keys.
        /// </summary>
        public readonly DWORD ControlKeyState;

        /// <summary>
        /// The type of mouse event.
        /// </summary>
        public readonly DWORD EventFlags;
    }

    /// <summary>
    /// Describes a keyboard input event in a console <see cref="InputEvent"/> structure.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public readonly struct KeyEvent
    {
        /// <summary>
        /// If the key is pressed, this member is <see langword="true"/>. Otherwise, this member is <see langword="false"/> (the key is released).
        /// </summary>
        [FieldOffset(0)]
        public readonly BOOL IsDown;

        /// <summary>
        /// The repeat count, which indicates that a key is being held down. For example, when a key is held down, you might get five events with this member equal to 1, one event with this member equal to 5, or multiple events with this member greater than or equal to 1.
        /// </summary>
        [FieldOffset(4)]
        public readonly WORD RepeatCount;

        /// <summary>
        /// A <see href="https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes">virtual-key code</see> that identifies the given key in a device-independent manner.
        /// </summary>
        [FieldOffset(6)]
        public readonly WORD VirtualKeyCode;

        [FieldOffset(8)]
        public readonly WORD wVirtualScanCode;

        [FieldOffset(10)]
        public readonly WCHAR UnicodeChar;

        [FieldOffset(10)]
        public readonly CHAR AsciiChar;

        /// <summary>
        /// The state of the control keys.
        /// </summary>
        [FieldOffset(12)]
        public readonly DWORD ControlKeyState;
    }

    /// <summary>
    /// Describes a change in the size of the console screen buffer.
    /// </summary>
    public readonly struct WindowBufferSizeEvent
    {
        public readonly Coord Size;

        public readonly SHORT Width => Size.X;
        public readonly SHORT Height => Size.Y;
    }
}
