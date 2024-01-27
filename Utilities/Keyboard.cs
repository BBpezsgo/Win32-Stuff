namespace Win32
{
    [Flags]
    public enum ShiftKeys : byte
    {
        /// <summary>
        /// Either SHIFT key is pressed.
        /// </summary>
        SHIFT = 1,
        /// <summary>
        /// Either CTRL key is pressed.
        /// </summary>
        CTRL = 2,
        /// <summary>
        /// Either ALT key is pressed.
        /// </summary>
        ALT = 4,
        /// <summary>
        /// The Hankaku key is pressed
        /// </summary>
        Hankaku = 8,
        // Reserved (defined by the keyboard layout driver).
        // 16,
        // Reserved (defined by the keyboard layout driver).
        // 32,
    }

    public static partial class Keyboard
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

        public static readonly VirtualKeyCode[] AsciiToVirtualKeyMap = new VirtualKeyCode[]
        {
             VirtualKeyCode.NONE, // (char)0x00
             VirtualKeyCode.NONE, // (char)0x01
             VirtualKeyCode.NONE, // (char)0x02
             VirtualKeyCode.NONE, // (char)0x03
             VirtualKeyCode.NONE, // (char)0x04
             VirtualKeyCode.NONE, // (char)0x05
             VirtualKeyCode.NONE, // (char)0x06
             VirtualKeyCode.NONE, // (char)0x07
             VirtualKeyCode.BACK, // '\b'
             VirtualKeyCode.TAB, // '\t'
             VirtualKeyCode.RETURN, // '\n'
             VirtualKeyCode.NONE, // (char)0x0B
             VirtualKeyCode.NONE, // (char)0x0C
             VirtualKeyCode.RETURN, // '\r'
             VirtualKeyCode.NONE, // (char)0x0E
             VirtualKeyCode.NONE, // (char)0x0F
             VirtualKeyCode.NONE, // (char)0x10
             VirtualKeyCode.NONE, // (char)0x11
             VirtualKeyCode.NONE, // (char)0x12
             VirtualKeyCode.NONE, // (char)0x13
             VirtualKeyCode.NONE, // (char)0x14
             VirtualKeyCode.NONE, // (char)0x15
             VirtualKeyCode.NONE, // (char)0x16
             VirtualKeyCode.NONE, // (char)0x17
             VirtualKeyCode.NONE, // (char)0x18
             VirtualKeyCode.NONE, // (char)0x19
             VirtualKeyCode.NONE, // (char)0x1A
             VirtualKeyCode.ESCAPE, // (char)0x1B
             VirtualKeyCode.NONE, // (char)0x1C
             VirtualKeyCode.NONE, // (char)0x1D
             VirtualKeyCode.NONE, // (char)0x1E
             VirtualKeyCode.NONE, // (char)0x1F
             VirtualKeyCode.SPACE, // ' '
             VirtualKeyCode.NONE, // '!'
             VirtualKeyCode.NONE, // '"'
             VirtualKeyCode.NONE, // '#'
             VirtualKeyCode.NONE, // '$'
             VirtualKeyCode.NONE, // '%'
             VirtualKeyCode.NONE, // '&'
             VirtualKeyCode.NONE, // '\''
             VirtualKeyCode.NONE, // '('
             VirtualKeyCode.NONE, // ')'
             VirtualKeyCode.MULTIPLY, // '*'
             VirtualKeyCode.OEM_PLUS, // '+'
             VirtualKeyCode.OEM_COMMA, // ','
             VirtualKeyCode.OEM_MINUS, // '-'
             VirtualKeyCode.OEM_PERIOD, // '.'
             VirtualKeyCode.NONE, // '/'
             VirtualKeyCode.VK_0, // '0'
             VirtualKeyCode.VK_1, // '1'
             VirtualKeyCode.VK_2, // '2'
             VirtualKeyCode.VK_3, // '3'
             VirtualKeyCode.VK_4, // '4'
             VirtualKeyCode.VK_5, // '5'
             VirtualKeyCode.VK_6, // '6'
             VirtualKeyCode.VK_7, // '7'
             VirtualKeyCode.VK_8, // '8'
             VirtualKeyCode.VK_9, // '9'
             VirtualKeyCode.NONE, // ':'
             VirtualKeyCode.NONE, // ';'
             VirtualKeyCode.NONE, // '<'
             VirtualKeyCode.NONE, // '='
             VirtualKeyCode.NONE, // '>'
             VirtualKeyCode.NONE, // '?'
             VirtualKeyCode.NONE, // '@'
             VirtualKeyCode.VK_A, // 'A'
             VirtualKeyCode.VK_B, // 'B'
             VirtualKeyCode.VK_C, // 'C'
             VirtualKeyCode.VK_D, // 'D'
             VirtualKeyCode.VK_E, // 'E'
             VirtualKeyCode.VK_F, // 'F'
             VirtualKeyCode.VK_G, // 'G'
             VirtualKeyCode.VK_H, // 'H'
             VirtualKeyCode.VK_I, // 'I'
             VirtualKeyCode.VK_J, // 'J'
             VirtualKeyCode.VK_K, // 'K'
             VirtualKeyCode.VK_L, // 'L'
             VirtualKeyCode.VK_M, // 'M'
             VirtualKeyCode.VK_N, // 'N'
             VirtualKeyCode.VK_O, // 'O'
             VirtualKeyCode.VK_P, // 'P'
             VirtualKeyCode.VK_Q, // 'Q'
             VirtualKeyCode.VK_R, // 'R'
             VirtualKeyCode.VK_S, // 'S'
             VirtualKeyCode.VK_T, // 'T'
             VirtualKeyCode.VK_U, // 'U'
             VirtualKeyCode.VK_V, // 'V'
             VirtualKeyCode.VK_W, // 'W'
             VirtualKeyCode.VK_X, // 'X'
             VirtualKeyCode.VK_Y, // 'Y'
             VirtualKeyCode.VK_Z, // 'Z'
             VirtualKeyCode.NONE, // '['
             VirtualKeyCode.NONE, // '\\'
             VirtualKeyCode.NONE, // ']'
             VirtualKeyCode.NONE, // '^'
             VirtualKeyCode.NONE, // '_'
             VirtualKeyCode.NONE, // '`'
             VirtualKeyCode.VK_A, // 'a'
             VirtualKeyCode.VK_B, // 'b'
             VirtualKeyCode.VK_C, // 'c'
             VirtualKeyCode.VK_D, // 'd'
             VirtualKeyCode.VK_E, // 'e'
             VirtualKeyCode.VK_F, // 'f'
             VirtualKeyCode.VK_G, // 'g'
             VirtualKeyCode.VK_H, // 'h'
             VirtualKeyCode.VK_I, // 'i'
             VirtualKeyCode.VK_J, // 'j'
             VirtualKeyCode.VK_K, // 'k'
             VirtualKeyCode.VK_L, // 'l'
             VirtualKeyCode.VK_M, // 'm'
             VirtualKeyCode.VK_N, // 'n'
             VirtualKeyCode.VK_O, // 'o'
             VirtualKeyCode.VK_P, // 'p'
             VirtualKeyCode.VK_Q, // 'q'
             VirtualKeyCode.VK_R, // 'r'
             VirtualKeyCode.VK_S, // 's'
             VirtualKeyCode.VK_T, // 't'
             VirtualKeyCode.VK_U, // 'u'
             VirtualKeyCode.VK_V, // 'v'
             VirtualKeyCode.VK_W, // 'w'
             VirtualKeyCode.VK_X, // 'x'
             VirtualKeyCode.VK_Y, // 'y'
             VirtualKeyCode.VK_Z, // 'z'
             VirtualKeyCode.NONE, // '{'
             VirtualKeyCode.NONE, // '|'
             VirtualKeyCode.NONE, // '}'
             VirtualKeyCode.NONE, // '~'
             VirtualKeyCode.NONE, // (char)0x7F
        };
    }
}
