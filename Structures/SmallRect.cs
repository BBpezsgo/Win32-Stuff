﻿global using SMALL_RECT = Win32.SmallRect;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Defines the coordinates of the upper-left and lower-right corners of a rectangle.
    /// </summary>
    /// <remarks>
    /// This structure is used by console functions to specify rectangular areas of console
    /// screen buffers, where the coordinates specify the rows and columns of
    /// screen-buffer character cells.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct SmallRect :
        IEquatable<SMALL_RECT>,
        System.Numerics.IEqualityOperators<SMALL_RECT, SMALL_RECT, bool>,
        IEquatable<RECT>
    {
        /// <summary>The x-coordinate of the upper-left corner of the rectangle.</summary>
        SHORT left;
        /// <summary>The y-coordinate of the upper-left corner of the rectangle.</summary>
        SHORT top;
        /// <summary>The x-coordinate of the lower-right corner of the rectangle.</summary>
        SHORT right;
        /// <summary>The y-coordinate of the lower-right corner of the rectangle.</summary>
        SHORT bottom;

        public SHORT X
        {
            readonly get => left;
            set => left = value;
        }
        public SHORT Y
        {
            readonly get => top;
            set => top = value;
        }

        public SHORT Width
        {
            readonly get => (SHORT)(right - left + 1);
            set => right = (SHORT)(left + value);
        }
        public SHORT Height
        {
            readonly get => (SHORT)(bottom - top + 1);
            set => bottom = (SHORT)(top + value);
        }

        public SHORT Top
        {
            readonly get => top;
            set
            {
                top = value;
                bottom = Math.Max(top, bottom);
            }
        }
        public SHORT Left
        {
            readonly get => left;
            set
            {
                left = value;
                right = Math.Max(left, right);
            }
        }
        public SHORT Bottom
        {
            readonly get => bottom;
            set
            {
                bottom = value;
                top = Math.Min(top, bottom);
            }
        }
        public SHORT Right
        {
            readonly get => right;
            set
            {
                right = value;
                left = Math.Min(left, right);
            }
        }

        public COORD Position
        {
            readonly get => new(left, top);
            set
            {
                left = value.X;
                top = value.Y;
            }
        }
        public SIZE Size
        {
            readonly get => new(Width, Height);
            set
            {
                Width = (SHORT)value.Width;
                Height = (SHORT)value.Height;
            }
        }

        public SmallRect(COORD position, COORD size)
        {
            top = position.Y;
            left = position.X;
            bottom = (SHORT)(position.Y + size.Y);
            right = (SHORT)(position.X + size.X);
        }

        public SmallRect(SHORT x, SHORT y, SHORT width, SHORT height)
        {
            top = y;
            left = x;
            bottom = (SHORT)(y + height);
            right = (SHORT)(x + width);
        }

        public static implicit operator RECT(SMALL_RECT rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        public static implicit operator System.Drawing.Rectangle(SMALL_RECT rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        public static implicit operator System.Drawing.RectangleF(SMALL_RECT rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        /// <exception cref="OverflowException"/>
        public static explicit operator checked SMALL_RECT(System.Drawing.Rectangle rectangle) => new(checked((SHORT)rectangle.X), checked((SHORT)rectangle.Y), checked((SHORT)rectangle.Width), checked((SHORT)rectangle.Height));
        public static explicit operator SMALL_RECT(System.Drawing.Rectangle rectangle) => new((SHORT)rectangle.X, (SHORT)rectangle.Y, (SHORT)rectangle.Width, (SHORT)rectangle.Height);

        /// <exception cref="OverflowException"/>
        public static explicit operator checked SMALL_RECT(RECT rectangle) => new(checked((SHORT)rectangle.X), checked((SHORT)rectangle.Y), checked((SHORT)rectangle.Width), checked((SHORT)rectangle.Height));
        public static explicit operator SMALL_RECT(RECT rectangle) => new((SHORT)rectangle.X, (SHORT)rectangle.Y, (SHORT)rectangle.Width, (SHORT)rectangle.Height);

        public static bool operator ==(SMALL_RECT a, SMALL_RECT b) => a.Equals(b);
        public static bool operator !=(SMALL_RECT a, SMALL_RECT b) => !a.Equals(b);

        public override readonly string ToString() => $"{{ X: {X} Y: {Y} W: {Width} H: {Height} }}";
        public override readonly bool Equals(object? obj) => obj is SMALL_RECT rect && Equals(rect);
        public readonly bool Equals(SMALL_RECT other) =>
            left == other.left &&
            top == other.top &&
            right == other.right &&
            bottom == other.bottom;
        public readonly bool Equals(RECT other) =>
            left == other.Left &&
            top == other.Top &&
            right == other.Right &&
            bottom == other.Bottom;
        public override readonly int GetHashCode() => HashCode.Combine(left, top, right, bottom);

        #region Contains()

        public readonly bool Contains(POINT point) =>
            point.X >= left &&
            point.Y >= top &&
            point.X < right &&
            point.Y < bottom;

        public readonly bool Contains(COORD point) =>
            point.X >= left &&
            point.Y >= top &&
            point.X < right &&
            point.Y < bottom;

        public readonly bool Contains(System.Drawing.Point point) =>
            point.X >= left &&
            point.Y >= top &&
            point.X < right &&
            point.Y < bottom;

        public readonly bool Contains(System.Drawing.PointF point) =>
            point.X >= left &&
            point.Y >= top &&
            point.X < right &&
            point.Y < bottom;

        public readonly bool Contains(System.Numerics.Vector2 point) =>
            point.X >= left &&
            point.Y >= top &&
            point.X < right &&
            point.Y < bottom;

        public readonly bool Contains(int x, int y) =>
            x >= left &&
            y >= top &&
            x < right &&
            y < bottom;

        public readonly bool Contains(float x, float y) =>
            x >= left &&
            y >= top &&
            x < right &&
            y < bottom;

        #endregion
    }
}
