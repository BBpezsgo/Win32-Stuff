using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
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

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct CharInfo
    {
        [FieldOffset(0)] public char Char;
        [FieldOffset(2)] public WORD Attributes;

        public static CharInfo Empty => new(' ', 0);

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

        public CharInfo(char @char, byte foreground, byte background) : this(@char, (WORD)((foreground & MASK_FG) | ((background << 4) & MASK_BG)))
        { }

        public CharInfo(char @char, ForegroundColor foreground, BackgroundColor background) : this(@char, (WORD)(((byte)foreground & MASK_FG) | ((byte)background & MASK_BG)))
        { }

        public CharInfo(char @char) : this(@char, 0)
        { }

        public override readonly bool Equals(object? obj) => obj is CharInfo charInfo && Equals(charInfo);
        public readonly bool Equals(CharInfo other) =>
            Attributes == other.Attributes &&
            Char == other.Char;

        public override readonly int GetHashCode() => HashCode.Combine(Attributes, Char);

        public static bool operator ==(CharInfo a, CharInfo b) => a.Equals(b);
        public static bool operator !=(CharInfo a, CharInfo b) => !(a == b);

        public override readonly string ToString()
            => $"( \'{Char}\' 0b{Convert.ToString(Attributes, 2).PadLeft(8, '0')} )";
    }
}
