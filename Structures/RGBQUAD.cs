using System.Runtime.InteropServices;

namespace Win32.Gdi32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct RGBQuad :
        IEquatable<RGBQuad>,
        System.Numerics.IEqualityOperators<RGBQuad, RGBQuad, bool>
    {
        public BYTE Blue;
        public BYTE Green;
        public BYTE Red;
        readonly BYTE Reserved;

        public RGBQuad(byte red, byte green, byte blue)
        {
            Blue = blue;
            Green = green;
            Red = red;
            Reserved = default;
        }

        public static explicit operator System.Drawing.Color(RGBQuad v) => System.Drawing.Color.FromArgb(v.Red, v.Green, v.Blue);
        public static explicit operator RGBQuad(System.Drawing.Color v) => new(v.R, v.G, v.B);

        public static bool operator ==(RGBQuad left, RGBQuad right) => left.Equals(right);
        public static bool operator !=(RGBQuad left, RGBQuad right) => !left.Equals(right);

        public override readonly bool Equals(object? obj) => obj is RGBQuad quad && Equals(quad);
        public readonly bool Equals(RGBQuad other) =>
            Blue == other.Blue &&
            Green == other.Green &&
            Red == other.Red;
        public override readonly int GetHashCode() => HashCode.Combine(Blue, Green, Red);
        public override readonly string ToString() => $"({Red}, {Green}, {Blue})";

        public readonly void Deconstruct(out BYTE r, out BYTE g, out BYTE b)
        {
            r = Red;
            g = Green;
            b = Blue;
        }
    }
}
