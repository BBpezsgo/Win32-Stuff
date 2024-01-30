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
        readonly COLORREF v;

        public byte R => (byte)((v >> 16) & byte.MaxValue);
        public byte G => (byte)((v >> 8) & byte.MaxValue);
        public byte B => (byte)((v >> 0) & byte.MaxValue);

        public static readonly GdiColor White = new GdiColor(255, 255, 255);
        public static readonly GdiColor Red = new GdiColor(255, 0, 0);
        public static readonly GdiColor Green = new GdiColor(0, 255, 0);
        public static readonly GdiColor Blue = new GdiColor(0, 0, 255);
        public static readonly GdiColor Yellow = new GdiColor(255, 255, 0);
        public static readonly GdiColor Cyan = new GdiColor(0, 255, 255);
        public static readonly GdiColor Magenta = new GdiColor(255, 0, 255);
        public static readonly GdiColor Black = new GdiColor(0, 0, 0);

        GdiColor(COLORREF v) => this.v = v;

        public GdiColor(byte r, byte g, byte b) => v = GdiColor.Make(r, g, b);
        public GdiColor(int r, int g, int b) => v = GdiColor.Make((byte)r, (byte)g, (byte)b);
        public GdiColor(float r, float g, float b) => v = GdiColor.Make((byte)(r * 255f), (byte)(g * 255f), (byte)(b * 255f));

        public static implicit operator GdiColor(COLORREF v) => new(v);
        public static implicit operator COLORREF(GdiColor v) => v.v;
        public static implicit operator GdiColor(ValueTuple<byte, byte, byte> v) => new(GdiColor.Make(v.Item1, v.Item2, v.Item3));
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

        public static COLORREF Make(BYTE r, BYTE g, BYTE b) => unchecked((COLORREF)(b | (g << 8) | (r << 16)));

        public readonly void Deconstruct(out byte r, out byte g, out byte b)
        {
            r = R;
            g = G;
            b = B;
        }
    }
}
