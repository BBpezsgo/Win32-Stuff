#pragma warning disable RCS1085 // Use auto-implemented property
#pragma warning disable IDE0032 // Use auto property

global using RECT = Win32.Rect;
using System.Runtime.CompilerServices;

namespace Win32;

/// <summary>
/// defines the coordinates of the upper-left and lower-right corners of a rectangle.
/// </summary>
/// <remarks>
/// By convention, the right and bottom edges of the rectangle are normally
/// considered exclusive. In other words, the pixel whose coordinates
/// are ( right, bottom ) lies immediately outside of the rectangle.
/// For example, when <see cref="RECT"/> is passed to the <see cref="User32.FillRect"/> function, the
/// rectangle is filled up to, but not including, the right column and
/// bottom row of pixels. This structure is identical to the <see cref="RECTL"/> structure.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
public struct Rect :
    IEquatable<RECT>,
    System.Numerics.IEqualityOperators<RECT, RECT, bool>
{
    /// <summary>The x-coordinate of the upper-left corner of the rectangle.</summary>
    LONG left;
    /// <summary>The y-coordinate of the upper-left corner of the rectangle.</summary>
    LONG top;
    /// <summary>The x-coordinate of the lower-right corner of the rectangle.</summary>
    LONG right;
    /// <summary>The y-coordinate of the lower-right corner of the rectangle.</summary>
    LONG bottom;

    public LONG X
    {
        readonly get => left;
        set => left = value;
    }
    public LONG Y
    {
        readonly get => top;
        set => top = value;
    }

    public LONG Width
    {
        readonly get => Math.Max(left, right) - Math.Min(left, right);
        set => right = left + value;
    }
    public LONG Height
    {
        readonly get => Math.Max(top, bottom) - Math.Min(top, bottom);
        set => bottom = top + value;
    }

    public LONG Top
    {
        readonly get => top;
        set
        {
            top = value;
            bottom = Math.Max(top, bottom);
        }
    }
    public LONG Left
    {
        readonly get => left;
        set
        {
            left = value;
            right = Math.Max(left, right);
        }
    }
    public LONG Bottom
    {
        readonly get => bottom;
        set
        {
            bottom = value;
            top = Math.Min(top, bottom);
        }
    }
    public LONG Right
    {
        readonly get => right;
        set
        {
            right = value;
            left = Math.Min(left, right);
        }
    }

    public POINT Position
    {
        readonly get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }
    public SIZE Size
    {
        readonly get => new(Width, Height);
        set
        {
            Width = value.Width;
            Height = value.Height;
        }
    }

    public readonly bool IsEmpty => top == default && left == default && bottom == default && right == default;

    public Rect(LONG x, LONG y, LONG width, LONG height)
    {
        left = x;
        top = y;
        right = x + width;
        bottom = y + height;
    }

    public static implicit operator System.Drawing.Rectangle(RECT rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
    public static implicit operator System.Drawing.RectangleF(RECT rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

    public static implicit operator RECT(System.Drawing.Rectangle rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

    /// <exception cref="OverflowException"/>
    public static explicit operator checked RECT(System.Drawing.RectangleF rectangle) => new(checked((LONG)rectangle.X), checked((LONG)rectangle.Y), checked((LONG)rectangle.Width), checked((LONG)rectangle.Height));
    public static explicit operator RECT(System.Drawing.RectangleF rectangle) => new((LONG)rectangle.X, (LONG)rectangle.Y, (LONG)rectangle.Width, (LONG)rectangle.Height);

    public static bool operator ==(RECT a, RECT b) => a.Equals(b);
    public static bool operator !=(RECT a, RECT b) => !a.Equals(b);

    public override readonly string ToString() => $"{{ X: {X} Y: {Y} W: {Width} H: {Height} }}";
    public override readonly bool Equals(object? obj) => obj is RECT rect && Equals(rect);
    public readonly bool Equals(RECT other) =>
        left == other.left &&
        top == other.top &&
        right == other.right &&
        bottom == other.bottom;
    public override readonly int GetHashCode() => HashCode.Combine(left, top, right, bottom);

    #region Contains()

    public readonly bool Contains(POINT point) =>
        point.X >= left &&
        point.Y >= top &&
        point.X < right &&
        point.Y < Height;

    public readonly bool Contains(COORD point) =>
        point.X >= left &&
        point.Y >= top &&
        point.X < right &&
        point.Y < Height;

    public readonly bool Contains(System.Drawing.Point point) =>
        point.X >= left &&
        point.Y >= top &&
        point.X < right &&
        point.Y < Height;

    public readonly bool Contains(System.Drawing.PointF point) =>
        point.X >= left &&
        point.Y >= top &&
        point.X < right &&
        point.Y < Height;

    public readonly bool Contains(System.Numerics.Vector2 point) =>
        point.X >= left &&
        point.Y >= top &&
        point.X < right &&
        point.Y < Height;

    public readonly bool Contains(int x, int y) =>
        x >= left &&
        y >= top &&
        x < right &&
        y < Height;

    public readonly bool Contains(float x, float y) =>
        x >= left &&
        y >= top &&
        x < right &&
        y < Height;

    #endregion

    /// <exception cref="GeneralException"/>
    [SupportedOSPlatform("windows")]
    public static unsafe void SetRect(ref RECT rect, int left, int top, int right, int bottom)
    {
        if (User32.SetRect((RECT*)Unsafe.AsPointer(ref rect), left, top, right, bottom) == 0)
        { throw new GeneralException($"{nameof(User32.SetRect)} has failed"); }
    }

    /// <exception cref="GeneralException"/>
    [SupportedOSPlatform("windows")]
    public static unsafe void SetRect(RECT* rect, int left, int top, int right, int bottom)
    {
        if (User32.SetRect(rect, left, top, right, bottom) == 0)
        { throw new GeneralException($"{nameof(User32.SetRect)} has failed"); }
    }

    public readonly void Deconstruct(out LONG x, out LONG y, out LONG width, out LONG height)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    public readonly void Deconstruct(out POINT position, out SIZE size)
    {
        position = Position;
        size = Size;
    }

    public static implicit operator RECT(Maths.RectInt v) => new(v.X, v.Y, v.Width, v.Height);
    public static implicit operator Maths.RectInt(RECT v) => new(v.X, v.Y, v.Width, v.Height);
}

public static class RectExtensions
{
    public static RECT Round(this Maths.RectF rect) => new((LONG)MathF.Round(rect.X), (LONG)MathF.Round(rect.Y), (LONG)MathF.Round(rect.Width), (LONG)MathF.Round(rect.Height));
}
