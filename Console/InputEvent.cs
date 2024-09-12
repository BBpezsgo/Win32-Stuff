namespace Win32.Console;

/// <summary>
/// Describes an input event in the console input buffer.
/// These records can be read from the input buffer by using
/// the <see cref="Kernel32.ReadConsoleInputW"/> or
/// <see cref="Kernel32.PeekConsoleInput"/> function, or
/// written to the input buffer by using
/// the <see cref="Kernel32.WriteConsoleInputW"/> function.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct InputEvent
{
    /// <summary>
    ///  <list type="table">
    ///   <item>
    ///    <term><see cref="EventType.Key"/></term>
    ///    <description>
    ///     The Event member contains a <see cref="Console.KeyEvent"/> structure with information about a keyboard event.
    ///    </description>
    ///   </item>
    ///   <item>
    ///    <term><see cref="EventType.Mouse"/></term>
    ///    <description>
    ///     The Event member contains a <see cref="Console.MouseEvent"/> structure with information about a mouse movement or button press event.
    ///    </description>
    ///   </item>
    ///   <item>
    ///    <term><see cref="EventType.WindowBufferSize"/></term>
    ///    <description>
    ///     The Event member contains a <see cref="Console.WindowBufferSizeEvent"/> structure with information about the new size of the console screen buffer.
    ///    </description>
    ///   </item>
    ///   <item>
    ///    <term><see cref="EventType.Menu"/></term>
    ///    <description>
    ///     The Event member contains a <see cref="Console.MenuEvent"/> structure. These events are used internally and should be ignored.
    ///    </description>
    ///   </item>
    ///   <item>
    ///    <term><see cref="EventType.Focus"/></term>
    ///    <description>
    ///     The Event member contains a <see cref="Console.FocusEvent"/> structure. These events are used internally and should be ignored.
    ///    </description>
    ///   </item>
    ///  </list>
    /// </summary>
    [FieldOffset(0)] public readonly EventType EventType;
    [FieldOffset(4)] public readonly KeyEvent KeyEvent;
    [FieldOffset(4)] public readonly MouseEvent MouseEvent;
    [FieldOffset(4)] public readonly WindowBufferSizeEvent WindowBufferSizeEvent;
    [FieldOffset(4)] public readonly MenuEvent MenuEvent;
    [FieldOffset(4)] public readonly FocusEvent FocusEvent;
}
