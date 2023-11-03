using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// The <see cref="POINT"/> structure defines the x- and y-coordinates of a point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct Point : IEquatable<POINT>
    {
        /// <summary>
        /// Specifies the x-coordinate of the point.
        /// </summary>
        public int X;
        /// <summary>
        /// Specifies the y-coordinate of the point.
        /// </summary>
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static explicit operator COORD(POINT p) => new(p.X, p.Y);
        public static explicit operator POINT(COORD p) => new(p.X, p.Y);

        public static bool operator ==(POINT left, POINT right) => left.Equals(right);
        public static bool operator !=(POINT left, POINT right) => !(left == right);

        public static POINT operator +(POINT a, POINT b) => new(a.X + b.X, a.Y + b.Y);
        public static POINT operator -(POINT a, POINT b) => new(a.X - b.X, a.Y - b.Y);

        public static POINT Zero => new(0, 0);

        public override readonly string ToString() => $"({X}, {Y})";

        public override readonly bool Equals(object? obj) => obj is POINT point && Equals(point);
        public readonly bool Equals(POINT other) => X == other.X && Y == other.Y;

        public override readonly int GetHashCode() => HashCode.Combine(X, Y);
    }
}
