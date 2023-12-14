using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Win32.Gdi32
{
    public readonly struct GdiColor :
        IEquatable<GdiColor>,
        IFormattable,
        IParsable<GdiColor>,
        IEqualityOperators<GdiColor, GdiColor, bool>,
        IShiftOperators<GdiColor, int, GdiColor>,
        IBitwiseOperators<GdiColor, GdiColor, GdiColor>
    {
        readonly uint v;

        GdiColor(uint v) => this.v = v;

        public static implicit operator GdiColor(uint v) => new(v);
        public static implicit operator uint(GdiColor v) => v.v;
        public static implicit operator GdiColor(ValueTuple<byte, byte, byte> v) => new(Macros.RGB(v.Item1, v.Item2, v.Item3));

        public static bool operator ==(GdiColor left, GdiColor right) => left.Equals(right);
        public static bool operator !=(GdiColor left, GdiColor right) => !(left == right);
        public static GdiColor operator <<(GdiColor value, int shiftAmount) => new(value.v << shiftAmount);
        public static GdiColor operator >>(GdiColor value, int shiftAmount) => new(value.v >> shiftAmount);
        public static GdiColor operator >>>(GdiColor value, int shiftAmount) => new(value.v >>> shiftAmount);
        public static GdiColor operator &(GdiColor left, GdiColor right) => new(left.v & right.v);
        public static GdiColor operator |(GdiColor left, GdiColor right) => new(left.v | right.v);
        public static GdiColor operator ^(GdiColor left, GdiColor right) => new(left.v ^ right.v);
        public static GdiColor operator ~(GdiColor value) => new(~value.v);

        public override string ToString() => v.ToString(System.Globalization.CultureInfo.InvariantCulture);
        public override int GetHashCode() => v.GetHashCode();
        public override bool Equals(object? obj) => obj is GdiColor color && Equals(color);
        public bool Equals(GdiColor other) => v == other.v;
        public string ToString(string? format, IFormatProvider? formatProvider) => v.ToString(format, formatProvider);
   
        public static GdiColor Parse(string s, IFormatProvider? provider) => uint.Parse(s, provider);
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GdiColor result)
        {
            result = default;
            if (!uint.TryParse(s, provider, out uint _result)) return false;
            result = new GdiColor(_result);
            return true;
        }
    }
}
