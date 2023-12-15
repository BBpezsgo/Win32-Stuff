using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [Flags]
    public enum ConsoleCharAttributes : WORD
    {
        ForegroundBlue = 0b_0000_0001,      // File color contains blue.
        ForegroundGreen = 0b_0000_0010,     // File color contains green.
        ForegroundRed = 0b_0000_0100,       // File color contains red.
        ForegroundBright = 0b_0000_1000,    // File color is intensified.

        BackgroundBlue = 0b_0001_0000,      // Background color contains blue.
        BackgroundGreen = 0b_0010_0000,     // Background color contains green.
        BackgroundRed = 0b_0100_0000,       // Background color contains red.
        BackgroundBright = 0b_1000_0000,    // Background color is intensified.

        CommonLVBLeadingByte = 0b_0000_0001_0000_0000,      // Leading byte.
        CommonLVBTrailingByte = 0b_0000_0010_0000_0000,     // Trailing byte.
        CommonLVBGridHorizontal = 0b_0000_0100_0000_0000,   // Top horizontal.
        CommonLVBGridLVertical = 0b_0000_1000_0000_0000,    // Left vertical.
        CommonLVBGridRVertical = 0b_0001_0000_0000_0000,    // Right vertical.
        CommonLVBReverseVideo = 0b_0100_0000_0000_0000,     // Reverse foreground and background attribute.
        CommonLVBUnderscore = 0b_1000_0000_0000_0000,       // Underscore.
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public struct ConsoleChar : IEquatable<ConsoleChar>, IEquatable<char>
    {
        [FieldOffset(0)] public char Char;
        [FieldOffset(2)] public WORD Attributes;

        public byte Color
        {
            readonly get => (byte)(Attributes & ByteColor.MASK_COLOR);
            set => Attributes = (ushort)((Attributes & ~ByteColor.MASK_COLOR) | (value & ByteColor.MASK_COLOR));
        }

        public byte Foreground
        {
            readonly get => (byte)(Attributes & ByteColor.MASK_FG);
            set => Attributes = (ushort)((Attributes & ~ByteColor.MASK_BG) | (value & ByteColor.MASK_FG));
        }

        public byte Background
        {
            readonly get => (byte)(Attributes >> 4);
            set => Attributes = (ushort)((Attributes & ~ByteColor.MASK_FG) | ((value << 4) & ByteColor.MASK_BG));
        }

        public static ConsoleChar Empty => new(' ', (WORD)0);

        public ConsoleChar(char @char, WORD attributes)
        {
            Char = @char;
            Attributes = attributes;
        }

        public ConsoleChar(char @char, ConsoleCharAttributes attributes)
        {
            Char = @char;
            Attributes = (WORD)attributes;
        }

        public ConsoleChar(char @char, byte foreground, byte background) : this(@char, ByteColor.Make(background, foreground))
        { }

        public ConsoleChar(char @char, ConsoleForegroundColor foreground, ConsoleBackgroundColor background) : this(@char, ByteColor.Make((byte)background, (byte)foreground))
        { }

        public ConsoleChar(char @char) : this(@char, (WORD)0)
        { }

        public override readonly bool Equals(object? obj) => obj is ConsoleChar charInfo && Equals(charInfo);
        public readonly bool Equals(ConsoleChar other) => Attributes == other.Attributes && Char == other.Char;
        public readonly bool Equals(char other) => Char == other;

        public override readonly int GetHashCode() => HashCode.Combine(Attributes, Char);

        public static bool operator ==(ConsoleChar a, ConsoleChar b) => a.Equals(b);
        public static bool operator !=(ConsoleChar a, ConsoleChar b) => !a.Equals(b);

        public static bool operator ==(ConsoleChar a, char b) => a.Char == b;
        public static bool operator !=(ConsoleChar a, char b) => a.Char != b;

        public static bool operator ==(char a, ConsoleChar b) => a == b.Char;
        public static bool operator !=(char a, ConsoleChar b) => a != b.Char;

        public static explicit operator ConsoleChar(char c) => new(c, 0b_0000_0111);
        public static implicit operator char(ConsoleChar c) => c.Char;
        public static implicit operator ConsoleChar(ValueTuple<char, ushort> c) => new(c.Item1, c.Item2);

        public override readonly string ToString() => Char.ToString();
        public readonly string GetDebuggerDisplay()
             => $"( \'{Char}\' 0b{Convert.ToString(Attributes, 2).PadLeft(8, '0')} )";
    }
}
