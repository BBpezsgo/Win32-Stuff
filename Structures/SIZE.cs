global using SIZE = Win32.Common.Size;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <summary>
    /// Defines the width and height of a rectangle.
    /// </summary>
    /// <remarks>
    /// The rectangle dimensions stored in this structure can correspond to
    /// viewport extents, window extents, text extents, bitmap dimensions,
    /// or the aspect-ratio filter for some extended functions.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct Size :
        IEquatable<SIZE>,
        System.Numerics.IEqualityOperators<SIZE, SIZE, bool>,
        System.Numerics.IMultiplyOperators<SIZE, int, SIZE>,
        System.Numerics.IDivisionOperators<SIZE, int, SIZE>
    {
        /// <summary>Specifies the rectangle's width. The units depend on which function uses this structure.</summary>
        public LONG Width;
        /// <summary>Specifies the rectangle's height. The units depend on which function uses this structure.</summary>
        public LONG Height;

        public Size(LONG width, LONG height)
        {
            Width = width;
            Height = height;
        }

        public static bool operator ==(SIZE a, SIZE b) => a.Equals(b);
        public static bool operator !=(SIZE a, SIZE b) => !a.Equals(b);

        public override readonly string ToString() => $"({Width} x {Height})";
        public override readonly bool Equals(object? obj) => obj is SIZE size && Equals(size);
        public readonly bool Equals(SIZE other) => Width == other.Width && Height == other.Height;
        public override readonly int GetHashCode() => HashCode.Combine(Width, Height);

        public static implicit operator ValueTuple<LONG, LONG>(SIZE size) => new(size.Width, size.Height);
        public static implicit operator System.Drawing.Size(SIZE size) => new(size.Width, size.Height);
        public static implicit operator System.Drawing.SizeF(SIZE size) => new(size.Width, size.Height);
        public static implicit operator System.Numerics.Vector2(SIZE size) => new(size.Width, size.Height);

        public static implicit operator SIZE(ValueTuple<LONG, LONG> size) => new(size.Item1, size.Item2);
        public static implicit operator SIZE(System.Drawing.Size size) => new(size.Width, size.Height);

        /// <exception cref="OverflowException"/>
        public static explicit operator checked SIZE(System.Drawing.SizeF size) => new(checked((LONG)size.Width), checked((LONG)size.Height));
        public static explicit operator SIZE(System.Drawing.SizeF size) => new((LONG)size.Width, (LONG)size.Height);

        /// <exception cref="OverflowException"/>
        public static explicit operator checked SIZE(System.Numerics.Vector2 size) => new(checked((LONG)size.X), checked((LONG)size.Y));
        public static explicit operator SIZE(System.Numerics.Vector2 size) => new((LONG)size.X, (LONG)size.Y);

        public readonly void Deconstruct(out LONG width, out LONG height)
        {
            width = Width;
            height = Height;
        }

        public static SIZE operator *(SIZE a, int b) => new(a.Width * b, a.Height * b);
        public static SIZE operator /(SIZE a, int b) => new(a.Width / b, a.Height / b);
    }
}
