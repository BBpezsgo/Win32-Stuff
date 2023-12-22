using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    /// <summary>
    /// The <see cref="UnicodeString"/> structure is used to define Unicode strings.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct UnicodeString : IEquatable<UnicodeString>
    {
        /// <summary>
        /// The length, in bytes, of the string stored in <see cref="Buffer"/>.
        /// </summary>
        public ushort Length;
        /// <summary>
        /// The length, in bytes, of <see cref="Buffer"/>.
        /// </summary>
        public ushort MaximumLength;
        /// <summary>
        /// Pointer to a buffer used to contain a string of wide characters.
        /// </summary>
        public unsafe WCHAR* Buffer;

        public readonly unsafe Span<WCHAR> AsSpan() => new(Buffer, Length);

        /// <inheritdoc/>
        public override readonly bool Equals(object? obj) => obj is UnicodeString other && Equals(other);

        /// <inheritdoc/>
        public readonly unsafe bool Equals(UnicodeString other)
        {
            if (Length == other.Length &&
                MaximumLength == other.MaximumLength &&
                Buffer == other.Buffer)
            { return true; }
            return string.Equals(ToString(), other.ToString(), StringComparison.Ordinal);
        }
        /// <inheritdoc/>
        public override readonly int GetHashCode() => ToString().GetHashCode(StringComparison.Ordinal);

        /// <inheritdoc/>
        public override readonly unsafe string ToString() => new(Buffer, 0, Length);

        /// <inheritdoc cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Equality"/>
        public static bool operator ==(UnicodeString left, UnicodeString right) => left.Equals(right);
        /// <inheritdoc cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Inequality"/>
        public static bool operator !=(UnicodeString left, UnicodeString right) => !left.Equals(right);

        public static unsafe explicit operator string(UnicodeString v) => new(v.Buffer, 0, v.Length);
    }
}
