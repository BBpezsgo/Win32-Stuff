﻿namespace Win32
{
    public struct ByteColor
    {
        public const byte Red = 0b_0100;
        public const byte Green = 0b_0010;
        public const byte Blue = 0b_0001;
        public const byte Yellow = 0b_0110;
        public const byte Cyan = 0b_0011;
        public const byte Magenta = 0b_0101;

        public const byte BrightRed = 0b_1100;
        public const byte BrightGreen = 0b_1010;
        public const byte BrightBlue = 0b_1001;
        public const byte BrightYellow = 0b_1110;
        public const byte BrightCyan = 0b_1011;
        public const byte BrightMagenta = 0b_1101;

        public const byte Black = 0b_0000;
        public const byte Silver = 0b_0111;
        public const byte Gray = 0b_1000;
        public const byte White = 0b_1111;

        public const WORD MASK_FG = 0b_0000_1111;
        public const WORD MASK_BG = 0b_1111_0000;
        public const WORD MASK_COLOR = 0b_1111_1111;

        public static WORD Make(byte background, byte foreground) => unchecked((WORD)((foreground & MASK_FG) | ((background << 4) & MASK_BG)));
    }
}
