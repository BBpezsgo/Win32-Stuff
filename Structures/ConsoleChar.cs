using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [Flags]
    public enum ConsoleCharAttributes : WORD
    {
        ForegroundBlue = 0b_0000_0001,
        ForegroundGreen = 0b_0000_0010,
        ForegroundRed = 0b_0000_0100,
        ForegroundBright = 0b_0000_1000,

        BackgroundBlue = 0b_0001_0000,
        BackgroundGreen = 0b_0010_0000,
        BackgroundRed = 0b_0100_0000,
        BackgroundBright = 0b_1000_0000,

        /// <summary> Leading byte </summary>
        CommonLVBLeadingByte = 0b_0000_0001_0000_0000,
        /// <summary> Trailing byte </summary>
        CommonLVBTrailingByte = 0b_0000_0010_0000_0000,
        /// <summary> Top horizontal </summary>
        CommonLVBGridHorizontal = 0b_0000_0100_0000_0000,
        /// <summary> Left vertical </summary>
        CommonLVBGridLVertical = 0b_0000_1000_0000_0000,
        /// <summary> Right vertical </summary>
        CommonLVBGridRVertical = 0b_0001_0000_0000_0000,
        /// <summary> Reverse foreground and background attribute </summary>
        CommonLVBReverseVideo = 0b_0100_0000_0000_0000,
        /// <summary> Underscore </summary>
        CommonLVBUnderscore = 0b_1000_0000_0000_0000,
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public struct ConsoleChar :
        IEquatable<ConsoleChar>,
        IEquatable<char>,
        System.Numerics.IEqualityOperators<ConsoleChar, ConsoleChar, bool>,
        System.Numerics.IEqualityOperators<ConsoleChar, char, bool>
    {
        public static ConsoleChar Empty => new(' ', (WORD)0);

        [FieldOffset(0)] public char Char;
        [FieldOffset(2)] public WORD Attributes;

        public byte Color
        {
            readonly get => (byte)(Attributes & CharColor.MASK_COLOR);
            set => Attributes = (ushort)((Attributes & ~CharColor.MASK_COLOR) | (value & CharColor.MASK_COLOR));
        }

        public byte Foreground
        {
            readonly get => (byte)(Attributes & CharColor.MASK_FG);
            set => Attributes = (ushort)((Attributes & CharColor.MASK_BG) | (value & CharColor.MASK_FG));
        }

        public byte Background
        {
            readonly get => (byte)(Attributes >> 4);
            set => Attributes = (ushort)((Attributes & CharColor.MASK_FG) | ((value << 4) & CharColor.MASK_BG));
        }

        public ConsoleChar(char @char)
        {
            Char = @char;
            Attributes = 0;
        }

        public ConsoleChar(char @char, WORD attributes)
        {
            Char = @char;
            Attributes = attributes;
        }

        /// <param name="foreground"> See <see cref="CharColor"/> for values </param>
        /// <param name="background"> See <see cref="CharColor"/> for values </param>
        public ConsoleChar(char @char, byte foreground, byte background = CharColor.Black)
        {
            Char = @char;
            Attributes = CharColor.Make(background, foreground);
        }

        /// <inheritdoc/>
        public override readonly bool Equals(object? obj) => obj is ConsoleChar charInfo && Equals(charInfo);
        /// <inheritdoc/>
        public readonly bool Equals(ConsoleChar other) => Attributes == other.Attributes && Char == other.Char;
        /// <inheritdoc/>
        public readonly bool Equals(char other) => Char == other;

        /// <inheritdoc/>
        public override readonly int GetHashCode() => HashCode.Combine(Attributes, Char);

        /// <inheritdoc/>
        public static bool operator ==(ConsoleChar a, ConsoleChar b) => a.Equals(b);
        /// <inheritdoc/>
        public static bool operator !=(ConsoleChar a, ConsoleChar b) => !a.Equals(b);

        /// <inheritdoc/>
        public static bool operator ==(ConsoleChar a, char b) => a.Char == b;
        /// <inheritdoc/>
        public static bool operator !=(ConsoleChar a, char b) => a.Char != b;

        /// <inheritdoc cref="op_Equality(ConsoleChar, char)"/>
        public static bool operator ==(char a, ConsoleChar b) => a == b.Char;
        /// <inheritdoc cref="op_Inequality(ConsoleChar, char)"/>
        public static bool operator !=(char a, ConsoleChar b) => a != b.Char;

        public static explicit operator ConsoleChar(char c) => new(c, 0b_0000_0111);
        public static implicit operator char(ConsoleChar c) => c.Char;
        public static implicit operator ConsoleChar(ValueTuple<char, ushort> c) => new(c.Item1, c.Item2);

        /// <inheritdoc/>
        public override readonly string ToString() => Char.ToString();
        readonly string GetDebuggerDisplay()
             => $"( \'{Char switch
             {
                 '\0' => @"\0",
                 '\r' => @"\r",
                 '\n' => @"\n",
                 '\b' => @"\b",
                 '\v' => @"\v",
                 _ => Char.ToString(),
             }}\' 0b{Convert.ToString(Attributes, 2).PadLeft(8, '0')} )";

        public readonly void Deconstruct(out WCHAR character, out WORD attributes)
        {
            character = Char;
            attributes = Attributes;
        }

        public readonly void Deconstruct(out WCHAR character, out BYTE background, out BYTE foreground)
        {
            character = Char;
            background = Background;
            foreground = Foreground;
        }

        public readonly bool IsInvisible
        {
            get
            {
                if (Attributes == 0) return true;
                if (Background != 0) return false;
                if (Char <= ' ') return true;
                return false;
            }
        }
    }
}
