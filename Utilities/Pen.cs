using System.Globalization;

namespace Win32.Gdi32
{
    using LowLevel;

    public readonly struct Pen : 
        IDisposable,
        IEquatable<Pen>,
        System.Numerics.IEqualityOperators<Pen, Pen, bool>
    {
        readonly HPEN Handle;

        Pen(HPEN handle) => Handle = handle;

        /// <exception cref="GdiException"/>
        public static Pen Create(int style, int width, COLORREF color)
        {
            HPEN pen = Gdi32.CreatePen(style, width, color);
            if (pen == HPEN.Zero)
            { throw new GdiException($"Failed to create pen"); }
            return new Pen(pen);
        }

        public static implicit operator HPEN(Pen pen) => pen.Handle;

        /// <exception cref="GdiException"/>
        public HGDIOBJ Use(HDC deviceContext)
        {
            HGDIOBJ prevObj = Gdi32.SelectObject(deviceContext, Handle);
            if (prevObj == 0 || prevObj == Gdi32.HGDI_ERROR)
            { throw new GdiException($"Failed to select object {this} into DC {deviceContext} (error {prevObj})"); }
            return prevObj;
        }
        /// <exception cref="GdiException"/>
        public void Dispose()
        {
            if (Gdi32.DeleteObject(Handle) == 0)
            { throw new GdiException($"Failed to delete object ({nameof(Pen)}) {this}"); }
        }

        public static bool operator ==(Pen left, Pen right) => left.Equals(right);
        public static bool operator !=(Pen left, Pen right) => !(left == right);

        public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        public override bool Equals(object? obj) => obj is Pen pen && Equals(pen);
        public bool Equals(Pen other) => Handle == other.Handle;
        public override int GetHashCode() => Handle.GetHashCode();
    }
}
