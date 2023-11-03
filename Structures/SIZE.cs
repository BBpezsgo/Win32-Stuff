using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// The <c>SIZE</c> structure defines the width and height of a rectangle.
    /// </summary>
    /// <remarks>
    /// The rectangle dimensions stored in this structure can correspond to
    /// viewport extents, window extents, text extents, bitmap dimensions,
    /// or the aspect-ratio filter for some extended functions.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct Size : IEquatable<SIZE>
    {
        /// <summary>
        /// Specifies the rectangle's width. The units depend on which function uses this structure.
        /// </summary>
        public LONG Width;
        /// <summary>
        /// Specifies the rectangle's height. The units depend on which function uses this structure.
        /// </summary>
        public LONG Height;

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override readonly string ToString() => $"({Width} x {Height})";

        public override readonly bool Equals(object? obj) => obj is SIZE size && Equals(size);
        public readonly bool Equals(SIZE other) => Width == other.Width && Height == other.Height;
        public override readonly int GetHashCode() => HashCode.Combine(Width, Height);

        public static bool operator ==(SIZE left, SIZE right) => left.Equals(right);
        public static bool operator !=(SIZE left, SIZE right) => !(left == right);
    }
}
