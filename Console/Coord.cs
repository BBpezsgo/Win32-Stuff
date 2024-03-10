global using COORD = Win32.Console.Coord;

namespace Win32.Console;

/// <summary>
/// Defines the coordinates of a character cell in a console screen buffer.
/// The origin of the coordinate system (0,0) is at the top, left cell of the buffer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
public struct Coord :
    IEquatable<COORD>,
    System.Numerics.IEqualityOperators<COORD, COORD, bool>,
    System.Numerics.IAdditionOperators<COORD, COORD, COORD>,
    System.Numerics.ISubtractionOperators<COORD, COORD, COORD>,
    System.Numerics.IMultiplyOperators<COORD, COORD, COORD>,
    System.Numerics.IDivisionOperators<COORD, COORD, COORD>,
    System.Numerics.IMultiplyOperators<COORD, int, COORD>,
    System.Numerics.IDivisionOperators<COORD, int, COORD>
{
    /// <summary>The horizontal coordinate or column value. The units depend on the function call.</summary>
    public SHORT X;
    /// <summary>The vertical coordinate or row value. The units depend on the function call.</summary>
    public SHORT Y;

    public static COORD One => new((SHORT)1, (SHORT)1);

    public Coord(SHORT x, SHORT y)
    {
        X = x;
        Y = y;
    }

    /// <exception cref="OverflowException"/>
    public Coord(LONG x, LONG y)
    {
        X = checked((SHORT)x);
        Y = checked((SHORT)y);
    }

    public static bool operator ==(COORD a, COORD b) => a.Equals(b);
    public static bool operator !=(COORD a, COORD b) => !a.Equals(b);

    public static COORD operator +(COORD a, COORD b) => new((SHORT)(a.X + b.X), (SHORT)(a.Y + b.Y));
    public static COORD operator -(COORD a, COORD b) => new((SHORT)(a.X - b.X), (SHORT)(a.Y - b.Y));
    public static COORD operator *(COORD a, COORD b) => new((SHORT)(a.X * b.X), (SHORT)(a.Y * b.Y));
    public static COORD operator /(COORD a, COORD b) => new((SHORT)(a.X / b.X), (SHORT)(a.Y / b.Y));
    public static COORD operator *(COORD a, int b) => new((SHORT)(a.X * b), (SHORT)(a.Y * b));
    public static COORD operator /(COORD a, int b) => new((SHORT)(a.X / b), (SHORT)(a.Y / b));

    public override readonly string ToString() => $"({X}, {Y})";
    public override readonly bool Equals(object? obj) => obj is COORD coord && Equals(coord);
    public readonly bool Equals(COORD other) => X == other.X && Y == other.Y;
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    public static implicit operator ValueTuple<SHORT, SHORT>(COORD size) => new(size.X, size.Y);
    public static implicit operator ValueTuple<LONG, LONG>(COORD size) => new(size.X, size.Y);
    public static implicit operator POINT(COORD size) => new(size.X, size.Y);
    public static implicit operator System.Drawing.Point(COORD size) => new(size.X, size.Y);
    public static implicit operator System.Numerics.Vector2(COORD size) => new(size.X, size.Y);

    public static implicit operator COORD(ValueTuple<SHORT, SHORT> size) => new(size.Item1, size.Item2);

    /// <exception cref="OverflowException"/>
    public static explicit operator checked COORD(ValueTuple<LONG, LONG> size) => new(checked((SHORT)size.Item1), checked((SHORT)size.Item2));
    /// <exception cref="OverflowException"/>
    public static explicit operator checked COORD(POINT size) => new(checked((SHORT)size.X), checked((SHORT)size.Y));
    /// <exception cref="OverflowException"/>
    public static explicit operator checked COORD(System.Drawing.Point size) => new(checked((SHORT)size.X), checked((SHORT)size.Y));
    /// <exception cref="OverflowException"/>
    public static explicit operator checked COORD(System.Numerics.Vector2 size) => new(checked((SHORT)size.X), checked((SHORT)size.Y));

    public static explicit operator COORD(ValueTuple<LONG, LONG> size) => new((SHORT)size.Item1, (SHORT)size.Item2);
    public static explicit operator COORD(POINT size) => new((SHORT)size.X, (SHORT)size.Y);
    public static explicit operator COORD(System.Drawing.Point size) => new((SHORT)size.X, (SHORT)size.Y);
    public static explicit operator COORD(System.Numerics.Vector2 size) => new((SHORT)size.X, (SHORT)size.Y);

    public static COORD Max(COORD a, COORD b) => new(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
    public static COORD Min(COORD a, COORD b) => new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));

    public readonly void Deconstruct(out SHORT x, out SHORT y)
    {
        x = X;
        y = Y;
    }
}
