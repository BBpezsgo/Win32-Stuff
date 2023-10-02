using System.Runtime.InteropServices;

namespace Win32
{
    public struct EventType
    {
        public const ushort KEY = 0x0001;
        public const ushort MOUSE = 0x0002;
        public const ushort WINDOW_BUFFER_SIZE = 0x0004;
        public const ushort MENU = 0x0008;
        public const ushort FOCUS = 0x0010;
    }

    public struct MouseButton
    {
        public const DWORD Left = 0x0001;
        public const DWORD Right = 0x0002;
        public const DWORD Middle = 0x0004;
        public const DWORD Button3 = 0x0008;
        public const DWORD Button4 = 0x0010;
        public const DWORD ScrollUp = 7864320;
        public const DWORD ScrollDown = 4287102976;
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
        ///     The Event member contains a <see cref="Win32.KeyEvent"/> structure with information about a keyboard event.
        ///    </description>
        ///   </item>
        ///   <item>
        ///    <term>MOUSE_EVENT 0x0002</term>
        ///    <description>
        ///     The Event member contains a <see cref="Win32.MouseEvent"/> structure with information about a mouse movement or button press event.
        ///    </description>
        ///   </item>
        ///   <item>
        ///    <term>WINDOW_BUFFER_SIZE_EVENT 0x0004</term>
        ///    <description>
        ///     The Event member contains a <see cref="Win32.WindowBufferSizeEvent"/> structure with information about the new size of the console screen buffer.
        ///    </description>
        ///   </item>
        ///   <item>
        ///    <term>MENU_EVENT 0x0008</term>
        ///    <description>
        ///     The Event member contains a <see cref="Win32.MenuEvent"/> structure. These events are used internally and should be ignored.
        ///    </description>
        ///   </item>
        ///   <item>
        ///    <term>FOCUS_EVENT 0x0010</term>
        ///    <description>
        ///     The Event member contains a <see cref="Win32.FocusEvent"/> structure. These events are used internally and should be ignored. 
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

        [FieldOffset(4)]
        public readonly MenuEvent MenuEvent;

        [FieldOffset(4)]
        public readonly FocusEvent FocusEvent;
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
        /// The state of the control keys. See <see cref="Win32.ControlKeyState"/>.
        /// </summary>
        public readonly DWORD ControlKeyState;

        /// <summary>
        /// The type of mouse event.
        /// </summary>
        public readonly DWORD EventFlags;

        public override string ToString() => $"{{ Pos: {MousePosition}, State: {Convert.ToString(ButtonState, 2).PadLeft(8, '0')}, Flags: {EventFlags}, Ctrl: {Convert.ToString(ControlKeyState, 2).PadLeft(9, '0')} }}";
    }

    /// <summary>
    /// Describes a keyboard input event in a console <see cref="InputEvent"/> structure.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Enhanced keys for the IBM® 101- and 102-key keyboards are the
    /// INS, DEL, HOME, END, PAGE UP, PAGE DOWN, and direction keys
    /// in the clusters to the left of the keypad; and the
    /// divide (/) and ENTER keys in the keypad.
    /// </para>
    /// <para>
    /// Keyboard input events are generated when any key, including control
    /// keys, is pressed or released. However, the ALT key when pressed
    /// and released without combining with another character, has special
    /// meaning to the system and is not passed through to the application.
    /// Also, the CTRL+C key combination is not passed through if the input
    /// handle is in processed mode (<c>ENABLE_PROCESSED_INPUT</c>).
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public readonly struct KeyEvent
    {
        /// <summary>
        /// If the key is pressed, this member is <see langword="true"/>.
        /// Otherwise, this member is <see langword="false"/> (the key is released).
        /// </summary>
        [FieldOffset(0)]
        public readonly BOOL IsDown;

        /// <summary>
        /// The repeat count, which indicates that a key is being held down.
        /// For example, when a key is held down, you might get five events with
        /// this member equal to 1, one event with this member equal to 5, or multiple
        /// events with this member greater than or equal to 1.
        /// </summary>
        [FieldOffset(4)]
        public readonly WORD RepeatCount;

        /// <summary>
        /// A <see href="https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes">virtual-key code</see> that identifies the given key in a device-independent manner.
        /// </summary>
        [FieldOffset(6)]
        public readonly WORD VirtualKeyCode;

        /// <summary>
        /// The virtual scan code of the given key that represents
        /// the device-dependent value generated by the keyboard hardware.
        /// </summary>
        [FieldOffset(8)]
        public readonly WORD VirtualScanCode;

        /// <summary>
        /// Translated Unicode character.
        /// </summary>
        [FieldOffset(10)]
        public readonly WCHAR UnicodeChar;

        /// <summary>
        /// Translated ASCII character.
        /// </summary>
        [FieldOffset(10)]
        public readonly CHAR AsciiChar;

        /// <summary>
        /// The state of the control keys. See <see cref="Win32.ControlKeyState"/>.
        /// </summary>
        [FieldOffset(12)]
        public readonly DWORD ControlKeyState;

        public override string ToString() => $"{{ '{UnicodeChar}' ({AsciiChar}) {RepeatCount}x, Down: {IsDown}, VKeyCode: {VirtualKeyCode}, VScanCode: {VirtualScanCode}, Ctrl: {Convert.ToString(ControlKeyState, 2).PadLeft(9, '0')} }}";
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

    /// <summary>
    /// Describes a focus event in a console <see cref="INPUT_RECORD"/> structure.
    /// These events are used internally and should be ignored.
    /// </summary>
    public readonly struct FocusEvent
    {
        /// <summary>
        /// Reserved.
        /// </summary>
        public readonly BOOL bSetFocus;
    }

    /// <summary>
    /// Describes a menu event in a console <see cref="INPUT_RECORD"/> structure.
    /// These events are used internally and should be ignored.
    /// </summary>
    public readonly struct MenuEvent
    {
        /// <summary>
        /// Reserved.
        /// </summary>
        public readonly UINT CommandId;
    }
}
