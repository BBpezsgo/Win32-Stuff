﻿namespace Win32.Console;

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
/// handle is in processed mode (<see cref="InputMode.EnableProcessedInput"/>).
/// </para>
/// </remarks>
[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
public readonly struct KeyEvent
{
    /// <summary>
    /// If the key is pressed, this member is <see langword="true"/>.
    /// Otherwise, this member is <see langword="false"/> (the key is released).
    /// </summary>
    [FieldOffset(0)] public readonly BOOL IsDown;

    /// <summary>
    /// The repeat count, which indicates that a key is being held down.
    /// For example, when a key is held down, you might get five events with
    /// this member equal to 1, one event with this member equal to 5, or multiple
    /// events with this member greater than or equal to 1.
    /// </summary>
    [FieldOffset(4)] public readonly WORD RepeatCount;

    /// <summary>
    /// A <see href="https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes">virtual-key code</see> that identifies the given key in a device-independent manner.
    /// </summary>
    [FieldOffset(6)] public readonly VirtualKeyCode VirtualKeyCode;

    /// <summary>
    /// The virtual scan code of the given key that represents
    /// the device-dependent value generated by the keyboard hardware.
    /// </summary>
    [FieldOffset(8)] public readonly WORD VirtualScanCode;

    /// <summary>
    /// Translated Unicode character.
    /// </summary>
    [FieldOffset(10)] public readonly WCHAR UnicodeChar;

    /// <summary>
    /// Translated ASCII character.
    /// </summary>
    [FieldOffset(10)] public readonly CHAR AsciiChar;

    /// <summary>
    /// The state of the control keys.
    /// </summary>
    [FieldOffset(12)] public readonly ControlKeyState ControlKeyState;

    public KeyEvent(int isDown, ushort repeatCount, VirtualKeyCode virtualKeyCode, ushort virtualScanCode, char unicodeChar, ControlKeyState controlKeyState) : this()
    {
        IsDown = isDown;
        RepeatCount = repeatCount;
        VirtualKeyCode = virtualKeyCode;
        VirtualScanCode = virtualScanCode;
        UnicodeChar = unicodeChar;
        ControlKeyState = controlKeyState;
    }

    public static explicit operator KeyEvent(ConsoleKeyInfo keyInfo)
    {
        return new KeyEvent(
            1,
            1,
            (VirtualKeyCode)keyInfo.Key,
            (ushort)keyInfo.Key,
            keyInfo.KeyChar,
            keyInfo.Modifiers switch
            {
                ConsoleModifiers.None => default,
                ConsoleModifiers.Alt => ControlKeyState.LeftAlt | ControlKeyState.RightAlt,
                ConsoleModifiers.Shift => ControlKeyState.Shift,
                ConsoleModifiers.Control => ControlKeyState.LeftCtrl | ControlKeyState.RightCtrl,
                _ => throw new UnreachableException(),
            }
        );
    }

    public override string ToString() => $"{{ '{UnicodeChar.ToString().Replace("\0", "\\0", StringComparison.Ordinal).Replace("\t", "\\t", StringComparison.Ordinal).Replace("\r", "\\r", StringComparison.Ordinal).Replace("\n", "\\n", StringComparison.Ordinal)}' ({AsciiChar}) {RepeatCount}x, Down: {IsDown}, VKeyCode: {VirtualKeyCode}, VScanCode: {VirtualScanCode}, Ctrl: {ControlKeyState} }}";
}
