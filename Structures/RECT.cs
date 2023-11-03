using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public struct Rect
    {
        public LONG Left;
        public LONG Top;
        public LONG Right;
        public LONG Bottom;

        public readonly POINT Position => new(Math.Min(Left, Right), Math.Min(Top, Bottom));

        public readonly LONG Width => Math.Max(Left, Right) - Math.Min(Left, Right);
        public readonly LONG Height => Math.Max(Top, Bottom) - Math.Min(Top, Bottom);

        public Rect(LONG top, LONG left, LONG bottom, LONG right)
        {
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
        }

        public override readonly string ToString() => $"{{ Left: {Left} Top: {Top} Bottom: {Bottom} Right: {Right} }}";
    }
}
