using System.Globalization;

namespace Win32
{
    public readonly struct Region : IDisposable
    {
        readonly HRGN _handle;

        Region(HRGN handle) => _handle = handle;

        unsafe public static Region CreateRectIndirect(Rect* rect)
        {
            HRGN region = Gdi32.CreateRectRgnIndirect(rect);
            if (region == HRGN.Zero)
            { throw new NotWindowsException($"Failed to create region"); }
            return new Region(region);
        }

        public void Dispose()
        {
            if (Gdi32.DeleteObject(_handle) == 0)
            { throw new NotWindowsException($"Failed to delete object ({nameof(Region)}) {this}"); }
        }

        public static implicit operator HRGN(Region region) => region._handle;

        public override string ToString() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        public void Use(HDC deviceContext) => Gdi32.SelectObject(deviceContext, _handle);
    }
}
