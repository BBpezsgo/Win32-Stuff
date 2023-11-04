using System.Globalization;

namespace Win32
{
    public readonly struct Brush : IDisposable
    {
        readonly HBRUSH _handle;

        Brush(HBRUSH handle) => _handle = handle;

        public static Brush CreateSolid(byte red, byte green, byte blue)
        {
            HBRUSH brush = Gdi32.CreateSolidBrush(Macros.RGB(red, green, blue));
            if (brush == HBRUSH.Zero)
            { throw new NotWindowsException($"Failed to create solid brush"); }
            return new Brush(brush);
        }

        public static Brush CreateSolid(COLORREF color)
        {
            HBRUSH brush = Gdi32.CreateSolidBrush(color);
            if (brush == HBRUSH.Zero)
            { throw new NotWindowsException($"Failed to create solid brush"); }
            return new Brush(brush);
        }

        public static HBRUSH GetSystem(int index)
        {
            HBRUSH brush = User32.GetSysColorBrush(index);
            if (brush == HBRUSH.Zero)
            { throw new NotSupportedException($"System brush {index} not supported by the current platform"); }
            return brush;
        }

        public void Dispose()
        {
            if (Gdi32.DeleteObject(_handle) == 0)
            { throw new NotWindowsException($"Failed to delete object ({nameof(Brush)}) {this}"); }
        }

        public static implicit operator HBRUSH(Brush brush) => brush._handle;

        public override string ToString() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        public void Use(HDC deviceContext) => Gdi32.SelectObject(deviceContext, _handle);
    }
}
