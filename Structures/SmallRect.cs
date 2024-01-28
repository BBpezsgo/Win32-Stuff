global using SMALL_RECT = Win32.SmallRect;

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
            set
            {
                SHORT width = Width;
                left = value;
                right = (SHORT)(value + width);
            }
        }
        public SHORT Y
        {
            readonly get => top;
            set
            {
                SHORT height = Height;
                top = value;
                bottom = (SHORT)(value + height);
            }
        }

        public SHORT Width
        {
            readonly get => (SHORT)(right - left);
            set => right = (SHORT)(left + value);
        }
        public SHORT Height
        {
            readonly get => (SHORT)(bottom - top);
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
                int offsetX = value.X - left;
                int offsetY = value.Y - top;
                left = value.X;
                top = value.Y;
                bottom = (SHORT)(bottom + offsetY);
                right = (SHORT)(right + offsetX);
            }
        }
        public SmallSize Size
        {
            readonly get => new(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public COORD Center
        {
            readonly get => new((left + right) / 2, (top + bottom) / 2);
            set
            {
                SHORT width = Width;
                SHORT height = Height;

                top = (SHORT)(value.Y - (height / 2));
                bottom = (SHORT)(top + height);

                left = (SHORT)(value.X - (width / 2));
                right = (SHORT)(left + width);
            }
        }

        public SmallRect(COORD position, COORD size)
        {
            top = position.Y;
            left = position.X;
            bottom = (SHORT)(position.Y + size.Y);
            right = (SHORT)(position.X + size.X);
        }

        public SmallRect(COORD position, SmallSize size)
        {
            top = position.Y;
            left = position.X;
            bottom = (SHORT)(position.Y + size.Height);
            right = (SHORT)(position.X + size.Width);
        }

        public SmallRect(SHORT x, SHORT y, SHORT width, SHORT height)
        {
            top = y;
            left = x;
            bottom = (SHORT)(y + height);
            right = (SHORT)(x + width);
        }

        public SmallRect(LONG x, LONG y, LONG width, LONG height)
        {
            top = (SHORT)y;
            left = (SHORT)x;
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

        #region Margin()

        public readonly SMALL_RECT Margin(int all) => Margin((short)all);
        public readonly SMALL_RECT Margin(short all)
        {
            SMALL_RECT result = this;

            result.top += all;
            result.left += all;
            result.bottom -= all;
            result.right -= all;

            SMALL_RECT.Fix(ref result);

            return result;
        }

        public readonly SMALL_RECT Margin(int vertical, int horizontal) => Margin((short)vertical, (short)horizontal);
        public readonly SMALL_RECT Margin(short vertical, short horizontal)
        {
            SMALL_RECT result = this;

            result.top += vertical;
            result.left += horizontal;
            result.bottom -= vertical;
            result.right -= horizontal;

            SMALL_RECT.Fix(ref result);

            return result;
        }

        #endregion

        #region Fix()

        static void Fix(ref SMALL_RECT rect)
        {
            if (rect.left > rect.right)
            {
                short middle = (short)(((int)rect.left + (int)rect.right) / 2);
                rect.left = middle;
                rect.right = middle;
            }

            if (rect.top > rect.bottom)
            {
                short middle = (short)(((int)rect.top + (int)rect.bottom) / 2);
                rect.top = middle;
                rect.bottom = middle;
            }
        }

        #endregion

        public readonly void Deconstruct(out SHORT x, out SHORT y, out SHORT width, out SHORT height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;
        }

        public readonly void Deconstruct(out COORD position, out SmallSize size)
        {
            position = Position;
            size = Size;
        }
    }
}
