using System.Diagnostics;

namespace Win32.Common
{
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct SmallSize :
        IEquatable<SmallSize>,
        System.Numerics.IEqualityOperators<SmallSize, SmallSize, bool>
    {
        public SHORT Width;
        public SHORT Height;

        public SmallSize(SHORT width, SHORT height)
        {
            Width = width;
            Height = height;
        }

        public SmallSize(LONG width, LONG height)
        {
            Width = (SHORT)width;
            Height = (SHORT)height;
        }

        public static bool operator ==(SmallSize a, SmallSize b) => a.Equals(b);
        public static bool operator !=(SmallSize a, SmallSize b) => !a.Equals(b);

        public override readonly string ToString() => $"({Width} x {Height})";
        public override readonly bool Equals(object? obj) => obj is SmallSize size && Equals(size);
        public readonly bool Equals(SmallSize other) => Width == other.Width && Height == other.Height;
        public override readonly int GetHashCode() => HashCode.Combine(Width, Height);

        public static implicit operator ValueTuple<LONG, LONG>(SmallSize size) => new(size.Width, size.Height);
        public static implicit operator System.Drawing.Size(SmallSize size) => new(size.Width, size.Height);
        public static implicit operator System.Drawing.SizeF(SmallSize size) => new(size.Width, size.Height);
        public static implicit operator System.Numerics.Vector2(SmallSize size) => new(size.Width, size.Height);

        public static implicit operator SmallSize(ValueTuple<SHORT, SHORT> size) => new(size.Item1, size.Item2);

        /// <exception cref="OverflowException"/>
        public static explicit operator checked SmallSize(ValueTuple<LONG, LONG> size) => new(checked((SHORT)size.Item1), checked((SHORT)size.Item2));
        public static explicit operator SmallSize(ValueTuple<LONG, LONG> size) => new((SHORT)size.Item1, (SHORT)size.Item2);
        
        /// <exception cref="OverflowException"/>
        public static explicit operator checked SmallSize(System.Drawing.Size size) => new(checked((SHORT)size.Width), checked((SHORT)size.Height));
        public static explicit operator SmallSize(System.Drawing.Size size) => new((SHORT)size.Width, (SHORT)size.Height);

        /// <exception cref="OverflowException"/>
        public static explicit operator checked SmallSize(System.Drawing.SizeF size) => new(checked((SHORT)size.Width), checked((SHORT)size.Height));
        public static explicit operator SmallSize(System.Drawing.SizeF size) => new((SHORT)size.Width, (SHORT)size.Height);

        /// <exception cref="OverflowException"/>
        public static explicit operator checked SmallSize(System.Numerics.Vector2 size) => new(checked((SHORT)size.X), checked((SHORT)size.Y));
        public static explicit operator SmallSize(System.Numerics.Vector2 size) => new((SHORT)size.X, (SHORT)size.Y);
    }
}
