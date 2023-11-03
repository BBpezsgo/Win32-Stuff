using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Defines the coordinates of a character cell in a console screen buffer. The origin of the coordinate system (0,0) is at the top, left cell of the buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct Coord : IEquatable<Coord>
    {
        /// <summary>
        /// The horizontal coordinate or column value. The units depend on the function call.
        /// </summary>
        public SHORT X;
        /// <summary>
        /// The vertical coordinate or row value. The units depend on the function call.
        /// </summary>
        public SHORT Y;

        public static COORD Zero => new(0, 0);

        public Coord(SHORT x, SHORT y)
        {
            X = x;
            Y = y;
        }
        public Coord(int x, int y) : this((SHORT)x, (SHORT)y) { }
        public Coord(System.Drawing.Point p) : this((SHORT)p.X, (SHORT)p.Y) { }
        public Coord(System.Drawing.PointF p) : this((SHORT)p.X, (SHORT)p.Y) { }

        public override readonly bool Equals(object? obj) => obj is Coord coord && Equals(coord);

        public readonly bool Equals(Coord other) => X == other.X && Y == other.Y;

        public override readonly int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(Coord a, Coord b) => a.Equals(b);
        public static bool operator !=(Coord a, Coord b) => !(a == b);

        public override readonly string ToString() => $"({X}, {Y})";
    }
}
