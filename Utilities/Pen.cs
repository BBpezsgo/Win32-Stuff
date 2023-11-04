using System.Globalization;

namespace Win32
{
    public readonly struct Pen : IDisposable
    {
        readonly HPEN _handle;

        Pen(HPEN handle) => _handle = handle;

        public static Pen Create(int style, int width, COLORREF color)
        {
            HPEN pen = Gdi32.CreatePen(style, width, color);
            if (pen == HPEN.Zero)
            { throw new NotWindowsException($"Failed to create pen"); }
            return new Pen(pen);
        }


        public void Dispose()
        {
            if (Gdi32.DeleteObject(_handle) == 0)
            { throw new NotWindowsException($"Failed to delete object ({nameof(Pen)}) {this}"); }
        }

        public static implicit operator HPEN(Pen pen) => pen._handle;

        public override string ToString() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        public void Use(HDC deviceContext) => Gdi32.SelectObject(deviceContext, _handle);
    }
}
