namespace Win32;

[Flags]
public enum ShiftKeys : byte
{
    /// <summary>
    /// Either SHIFT key is pressed.
    /// </summary>
    Shift = 0x1,
    /// <summary>
    /// Either CTRL key is pressed.
    /// </summary>
    Ctrl = 0x2,
    /// <summary>
    /// Either ALT key is pressed.
    /// </summary>
    Alt = 0x4,
    /// <summary>
    /// The Hankaku key is pressed
    /// </summary>
    Hankaku = 0x8,
#pragma warning disable CA1700 // Do not name enum values 'Reserved'
    /// <summary>
    /// Reserved (defined by the keyboard layout driver).
    /// </summary>
    Reserved1 = 0x10,
    /// <summary>
    /// Reserved (defined by the keyboard layout driver).
    /// </summary>
    Reserved2 = 0x20,
#pragma warning restore CA1700 // Do not name enum values 'Reserved'
}

public static class Keyboard
{
    static HANDLE KeyboardLayout;

    public static (VirtualKeyCode Key, ShiftKeys Shift) ToVK(char ascii)
    {
        if (OperatingSystem.IsWindows())
        {
            if (KeyboardLayout == 0)
            { KeyboardLayout = User32.GetKeyboardLayout(0); }
            short result = User32.VkKeyScanExW(ascii, KeyboardLayout);
            byte vk = (byte)(result & 0xFF);
            byte shift = (byte)(result >> 8);

            return ((VirtualKeyCode)vk, (ShiftKeys)shift);
        }

        return (AsciiToVirtualKeyMap[ascii], 0);
    }

    public static readonly ImmutableArray<VirtualKeyCode> AsciiToVirtualKeyMap = ImmutableArray.Create<VirtualKeyCode>
    (
         VirtualKeyCode.None, // (char)0x00
         VirtualKeyCode.None, // (char)0x01
         VirtualKeyCode.None, // (char)0x02
         VirtualKeyCode.None, // (char)0x03
         VirtualKeyCode.None, // (char)0x04
         VirtualKeyCode.None, // (char)0x05
         VirtualKeyCode.None, // (char)0x06
         VirtualKeyCode.None, // (char)0x07
         VirtualKeyCode.Back, // '\b'
         VirtualKeyCode.Tab, // '\t'
         VirtualKeyCode.Return, // '\n'
         VirtualKeyCode.None, // (char)0x0B
         VirtualKeyCode.None, // (char)0x0C
         VirtualKeyCode.Return, // '\r'
         VirtualKeyCode.None, // (char)0x0E
         VirtualKeyCode.None, // (char)0x0F
         VirtualKeyCode.None, // (char)0x10
         VirtualKeyCode.None, // (char)0x11
         VirtualKeyCode.None, // (char)0x12
         VirtualKeyCode.None, // (char)0x13
         VirtualKeyCode.None, // (char)0x14
         VirtualKeyCode.None, // (char)0x15
         VirtualKeyCode.None, // (char)0x16
         VirtualKeyCode.None, // (char)0x17
         VirtualKeyCode.None, // (char)0x18
         VirtualKeyCode.None, // (char)0x19
         VirtualKeyCode.None, // (char)0x1A
         VirtualKeyCode.Escape, // (char)0x1B
         VirtualKeyCode.None, // (char)0x1C
         VirtualKeyCode.None, // (char)0x1D
         VirtualKeyCode.None, // (char)0x1E
         VirtualKeyCode.None, // (char)0x1F
         VirtualKeyCode.Space, // ' '
         VirtualKeyCode.None, // '!'
         VirtualKeyCode.None, // '"'
         VirtualKeyCode.None, // '#'
         VirtualKeyCode.None, // '$'
         VirtualKeyCode.None, // '%'
         VirtualKeyCode.None, // '&'
         VirtualKeyCode.None, // '\''
         VirtualKeyCode.None, // '('
         VirtualKeyCode.None, // ')'
         VirtualKeyCode.Multiply, // '*'
         VirtualKeyCode.OemPlus, // '+'
         VirtualKeyCode.OemComma, // ','
         VirtualKeyCode.OemMinus, // '-'
         VirtualKeyCode.OemPeriod, // '.'
         VirtualKeyCode.None, // '/'
         VirtualKeyCode.Number0, // '0'
         VirtualKeyCode.Number1, // '1'
         VirtualKeyCode.Number2, // '2'
         VirtualKeyCode.Number3, // '3'
         VirtualKeyCode.Number4, // '4'
         VirtualKeyCode.Number5, // '5'
         VirtualKeyCode.Number6, // '6'
         VirtualKeyCode.Number7, // '7'
         VirtualKeyCode.Number8, // '8'
         VirtualKeyCode.Number9, // '9'
         VirtualKeyCode.None, // ':'
         VirtualKeyCode.None, // ';'
         VirtualKeyCode.None, // '<'
         VirtualKeyCode.None, // '='
         VirtualKeyCode.None, // '>'
         VirtualKeyCode.None, // '?'
         VirtualKeyCode.None, // '@'
         VirtualKeyCode.A, // 'A'
         VirtualKeyCode.B, // 'B'
         VirtualKeyCode.C, // 'C'
         VirtualKeyCode.D, // 'D'
         VirtualKeyCode.E, // 'E'
         VirtualKeyCode.F, // 'F'
         VirtualKeyCode.G, // 'G'
         VirtualKeyCode.H, // 'H'
         VirtualKeyCode.I, // 'I'
         VirtualKeyCode.J, // 'J'
         VirtualKeyCode.K, // 'K'
         VirtualKeyCode.L, // 'L'
         VirtualKeyCode.M, // 'M'
         VirtualKeyCode.N, // 'N'
         VirtualKeyCode.O, // 'O'
         VirtualKeyCode.P, // 'P'
         VirtualKeyCode.Q, // 'Q'
         VirtualKeyCode.R, // 'R'
         VirtualKeyCode.S, // 'S'
         VirtualKeyCode.T, // 'T'
         VirtualKeyCode.U, // 'U'
         VirtualKeyCode.V, // 'V'
         VirtualKeyCode.W, // 'W'
         VirtualKeyCode.X, // 'X'
         VirtualKeyCode.Y, // 'Y'
         VirtualKeyCode.Z, // 'Z'
         VirtualKeyCode.None, // '['
         VirtualKeyCode.None, // '\\'
         VirtualKeyCode.None, // ']'
         VirtualKeyCode.None, // '^'
         VirtualKeyCode.None, // '_'
         VirtualKeyCode.None, // '`'
         VirtualKeyCode.A, // 'a'
         VirtualKeyCode.B, // 'b'
         VirtualKeyCode.C, // 'c'
         VirtualKeyCode.D, // 'd'
         VirtualKeyCode.E, // 'e'
         VirtualKeyCode.F, // 'f'
         VirtualKeyCode.G, // 'g'
         VirtualKeyCode.H, // 'h'
         VirtualKeyCode.I, // 'i'
         VirtualKeyCode.J, // 'j'
         VirtualKeyCode.K, // 'k'
         VirtualKeyCode.L, // 'l'
         VirtualKeyCode.M, // 'm'
         VirtualKeyCode.N, // 'n'
         VirtualKeyCode.O, // 'o'
         VirtualKeyCode.P, // 'p'
         VirtualKeyCode.Q, // 'q'
         VirtualKeyCode.R, // 'r'
         VirtualKeyCode.S, // 's'
         VirtualKeyCode.T, // 't'
         VirtualKeyCode.U, // 'u'
         VirtualKeyCode.V, // 'v'
         VirtualKeyCode.W, // 'w'
         VirtualKeyCode.X, // 'x'
         VirtualKeyCode.Y, // 'y'
         VirtualKeyCode.Z, // 'z'
         VirtualKeyCode.None, // '{'
         VirtualKeyCode.None, // '|'
         VirtualKeyCode.None, // '}'
         VirtualKeyCode.None, // '~'
         VirtualKeyCode.None  // (char)0x7F
    );
}
