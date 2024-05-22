global using POINT = Win32.Point;
global using POINTL = Win32.Point;

namespace Win32;

/// <summary>
/// Defines the x- and y-coordinates of a point.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
public struct Point :
    IEquatable<POINT>,
    System.Numerics.IEqualityOperators<POINT, POINT, bool>,
    System.Numerics.IAdditionOperators<POINT, POINT, POINT>,
    System.Numerics.ISubtractionOperators<POINT, POINT, POINT>,
    System.Numerics.IMultiplyOperators<POINT, POINT, POINT>,
    System.Numerics.IMultiplyOperators<POINT, int, POINT>,
    System.Numerics.IDivisionOperators<POINT, int, POINT>
{
    public static readonly Point Empty = new(0, 0);

    /// <summary>Specifies the x-coordinate of the point.</summary>
    public LONG X;
    /// <summary>Specifies the y-coordinate of the point.</summary>
    public LONG Y;

    public Point(LONG x, LONG y)
    {
        X = x;
        Y = y;
    }

    public static bool operator ==(POINT a, POINT b) => a.Equals(b);
    public static bool operator !=(POINT a, POINT b) => !a.Equals(b);

    public static POINT operator +(POINT a, POINT b) => new(a.X + b.X, a.Y + b.Y);
    public static POINT operator -(POINT a, POINT b) => new(a.X - b.X, a.Y - b.Y);
    public static POINT operator *(POINT a, POINT b) => new(a.X * b.X, a.Y * b.Y);
    public static POINT operator *(POINT a, int b) => new(a.X * b, a.Y * b);
    public static POINT operator /(POINT a, int b) => new(a.X / b, a.Y / b);

    public override readonly string ToString() => $"({X}, {Y})";
    public override readonly bool Equals(object? obj) => obj is POINT point && Equals(point);
    public readonly bool Equals(POINT other) => X == other.X && Y == other.Y;
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    public static implicit operator ValueTuple<LONG, LONG>(POINT v) => new(v.X, v.Y);
    public static implicit operator System.Drawing.Point(POINT v) => new(v.X, v.Y);
    public static implicit operator System.Numerics.Vector2(POINT v) => new(v.X, v.Y);

    public static implicit operator POINT(ValueTuple<LONG, LONG> v) => new(v.Item1, v.Item2);
    public static implicit operator POINT(System.Drawing.Point v) => new(v.X, v.Y);

    /// <exception cref="OverflowException"/>
    public static explicit operator checked POINT(System.Numerics.Vector2 v) => new(checked((LONG)v.X), checked((LONG)v.Y));

    public static explicit operator POINT(System.Numerics.Vector2 v) => new((LONG)v.X, (LONG)v.Y);

    public readonly void Deconstruct(out LONG x, out LONG y)
    {
        x = X;
        y = Y;
    }
}
