using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct SmallRect : IEquatable<SmallRect>
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;

        public short X
        {
            readonly get => Left;
            set => Left = value;
        }

        public short Y
        {
            readonly get => Top;
            set => Top = value;
        }

        public short Width
        {
            readonly get => (short)(Right - Left + 1);
            set => Right = (short)(Left + value);
        }
        public short Height
        {
            readonly get => (short)(Bottom - Top + 1);
            set => Bottom = (short)(Top + value);
        }

        public readonly Point Location => new(Left, Top);

        public static SmallRect Zero => default;

        public static SmallRect FromPosAndSize(int x, int y, int width, int height) => new()
        {
            Left = (short)x,
            Top = (short)y,
            Bottom = (short)(y + height),
            Right = (short)(x + width),
        };

        public override readonly bool Equals(object? obj) =>
            obj is SmallRect rect
            && Equals(rect);
        public readonly bool Equals(SmallRect other) =>
            Left == other.Left &&
            Top == other.Top &&
            Right == other.Right &&
            Bottom == other.Bottom;

        public override readonly int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);

        public static bool operator ==(SmallRect a, SmallRect b) => a.Equals(b);
        public static bool operator !=(SmallRect a, SmallRect b) => !(a == b);

        public override readonly string ToString()
            => $"{{ Left: {Left} Top: {Top} Bottom: {Bottom} Right: {Right} }}";

        public readonly bool Contains(POINT point) =>
            point.X >= Left &&
            point.Y >= Top &&
            point.X < Right &&
            point.Y < Height;

        public readonly bool Contains(COORD point) =>
            point.X >= Left &&
            point.Y >= Top &&
            point.X < Right &&
            point.Y < Height;

        public readonly bool Contains(int x, int y) =>
            x >= Left &&
            y >= Top &&
            x < Right &&
            y < Height;
    }

}
