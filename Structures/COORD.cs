using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Defines the coordinates of a character cell in a console screen buffer.
    /// The origin of the coordinate system (0,0) is at the top, left cell of the buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct Coord : IEquatable<COORD>
    {
        /// <summary>The horizontal coordinate or column value. The units depend on the function call.</summary>
        public SHORT X;
        /// <summary>The vertical coordinate or row value. The units depend on the function call.</summary>
        public SHORT Y;

        public Coord(SHORT x, SHORT y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(COORD a, COORD b) => a.Equals(b);
        public static bool operator !=(COORD a, COORD b) => !a.Equals(b);

        public override readonly string ToString() => $"({X}, {Y})";
        public override readonly bool Equals(object? obj) => obj is COORD coord && Equals(coord);
        public readonly bool Equals(COORD other) => X == other.X && Y == other.Y;
        public override readonly int GetHashCode() => HashCode.Combine(X, Y);

        public static explicit operator COORD(System.Drawing.Point size) => new((SHORT)size.X, (SHORT)size.Y);
        public static implicit operator System.Drawing.Point(COORD size) => new(size.X, size.Y);

        public static implicit operator COORD(ValueTuple<SHORT, SHORT> size) => new(size.Item1, size.Item2);
        public static implicit operator ValueTuple<SHORT, SHORT>(COORD size) => new(size.X, size.Y);

        public static explicit operator COORD(POINT size) => new((SHORT)size.X, (SHORT)size.Y);
        public static implicit operator POINT(COORD size) => new(size.X, size.Y);

        public static explicit operator COORD(ValueTuple<LONG, LONG> size) => new((SHORT)size.Item1, (SHORT)size.Item2);
        public static implicit operator ValueTuple<LONG, LONG>(COORD size) => new(size.X, size.Y);
    }
}
