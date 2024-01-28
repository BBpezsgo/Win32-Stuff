using System.Numerics;

namespace Win32.Gdi32
{
    public readonly struct GdiColor :
        IEquatable<GdiColor>,
        IEqualityOperators<GdiColor, GdiColor, bool>,
        IShiftOperators<GdiColor, int, GdiColor>,
        IBitwiseOperators<GdiColor, GdiColor, GdiColor>,
        IAdditionOperators<GdiColor, GdiColor, GdiColor>,
        ISubtractionOperators<GdiColor, GdiColor, GdiColor>,
        IMultiplyOperators<GdiColor, GdiColor, GdiColor>,
        IMultiplyOperators<GdiColor, int, GdiColor>,
        IMultiplyOperators<GdiColor, float, GdiColor>,
        IDivisionOperators<GdiColor, int, GdiColor>,
        IDivisionOperators<GdiColor, float, GdiColor>
    {
        readonly uint v;

        public byte R => (byte)((v >> 16) & byte.MaxValue);
        public byte G => (byte)((v >> 8) & byte.MaxValue);
        public byte B => (byte)((v >> 0) & byte.MaxValue);

        GdiColor(uint v) => this.v = v;

        public GdiColor(byte r, byte g, byte b) => v = Macros.RGB(r, g, b);
        public GdiColor(int r, int g, int b) => v = Macros.RGB((byte)r, (byte)g, (byte)b);
        public GdiColor(float r, float g, float b) => v = Macros.RGB((byte)(r * 255f), (byte)(g * 255f), (byte)(b * 255f));

        public static implicit operator GdiColor(uint v) => new(v);
        public static implicit operator uint(GdiColor v) => v.v;
        public static implicit operator GdiColor(ValueTuple<byte, byte, byte> v) => new(Macros.RGB(v.Item1, v.Item2, v.Item3));
        public static implicit operator System.Drawing.Color(GdiColor v) => System.Drawing.Color.FromArgb(v.R, v.G, v.B);
        public static implicit operator GdiColor(System.Drawing.Color v) => new(v.R, v.G, v.B);
        public static explicit operator GdiColor(Vector3 v) => new(v.X, v.Y, v.Z);

        public static bool operator ==(GdiColor left, GdiColor right) => left.Equals(right);
        public static bool operator !=(GdiColor left, GdiColor right) => !left.Equals(right);

        public static GdiColor operator +(GdiColor a, GdiColor b) => new(a.R + b.R, a.G + b.G, a.B + b.B);
        public static GdiColor operator -(GdiColor a, GdiColor b) => new(a.R - b.R, a.G - b.G, a.B - b.B);
        public static GdiColor operator *(GdiColor a, GdiColor b) => new(a.R * b.R, a.G * b.G, a.B * b.B);
        public static GdiColor operator *(GdiColor a, int b) => new(a.R * b, a.G * b, a.B * b);
        public static GdiColor operator *(GdiColor a, float b) => new((byte)(a.R * b), (byte)(a.G * b), (byte)(a.B * b));
        public static GdiColor operator /(GdiColor a, int b) => new(a.R / b, a.G / b, a.B / b);
        public static GdiColor operator /(GdiColor a, float b) => new((byte)(a.R / b), (byte)(a.G / b), (byte)(a.B / b));

        public static GdiColor operator <<(GdiColor value, int shiftAmount) => new(value.v << shiftAmount);
        public static GdiColor operator >>(GdiColor value, int shiftAmount) => new(value.v >> shiftAmount);
        public static GdiColor operator >>>(GdiColor value, int shiftAmount) => new(value.v >>> shiftAmount);
        public static GdiColor operator &(GdiColor left, GdiColor right) => new(left.v & right.v);
        public static GdiColor operator |(GdiColor left, GdiColor right) => new(left.v | right.v);
        public static GdiColor operator ^(GdiColor left, GdiColor right) => new(left.v ^ right.v);
        public static GdiColor operator ~(GdiColor value) => new(~value.v);

        public override string ToString() => $"({R} {G} {B})";
        public override int GetHashCode() => unchecked((int)v);
        public override bool Equals(object? obj) => obj is GdiColor color && Equals(color);
        public bool Equals(GdiColor other) => v == other.v;

        public readonly void Deconstruct(out byte r, out byte g, out byte b)
        {
            r = R;
            g = G;
            b = B;
        }
    }
}
