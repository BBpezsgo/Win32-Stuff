using System.Globalization;

namespace Win32
{
    public enum HatchStyle : int
    {
        /// <summary>
        /// -----
        /// </summary>
        HORIZONTAL = 0,
        /// <summary>
        /// |||||
        /// </summary>
        VERTICAL = 1,
        /// <summary>
        /// \\\\\
        /// </summary>
        FDIAGONAL = 2,
        /// <summary>
        /// /////
        /// </summary>
        BDIAGONAL = 3,
        /// <summary>
        /// +++++
        /// </summary>
        CROSS = 4,
        /// <summary>
        /// xxxxx
        /// </summary>
        DIAGCROSS = 5,
        API_MAX = 12,
    }

    public enum DIBType : uint
    {
        /// <summary>
        /// A color table is provided and consists of an array
        /// of 16-bit indexes into the logical palette of the
        /// device context into which the brush is to be selected.
        /// </summary>
        PAL_COLORS = 1, /* color table in palette indices */
        /// <summary>
        /// A color table is provided and contains literal RGB values.
        /// </summary>
        RGB_COLORS = 0, /* color table in RGBs */
    }

    public readonly struct Brush : IDisposable, IEquatable<Brush>
    {
        readonly HBRUSH Handle;

        Brush(HBRUSH handle) => Handle = handle;

        /// <exception cref="GdiException"/>
        public static Brush CreateSolid(byte red, byte green, byte blue)
        {
            HBRUSH brush = Gdi32.CreateSolidBrush(Macros.RGB(red, green, blue));
            if (brush == HBRUSH.Zero)
            { throw new GdiException($"Failed to create solid brush"); }
            return new Brush(brush);
        }

        /// <exception cref="GdiException"/>
        public static Brush CreateSolid(COLORREF color)
        {
            HBRUSH brush = Gdi32.CreateSolidBrush(color);
            if (brush == HBRUSH.Zero)
            { throw new GdiException($"Failed to create solid brush"); }
            return new Brush(brush);
        }

        /// <exception cref="GdiException"/>
        public static Brush CreateHatch(HatchStyle hatch, COLORREF color)
        {
            HBRUSH brush = Gdi32.CreateHatchBrush((int)hatch, color);
            if (brush == HBRUSH.Zero)
            { throw new GdiException($"Failed to create hatch brush"); }
            return new Brush(brush);
        }

        /// <exception cref="GdiException"/>
        public static Brush CreatePattern(HBITMAP bitmap)
        {
            HBRUSH brush = Gdi32.CreatePatternBrush(bitmap);
            if (brush == HBRUSH.Zero)
            { throw new GdiException($"Failed to create pattern brush"); }
            return new Brush(brush);
        }

        /// <exception cref="GdiException"/>
        unsafe public static Brush CreateDIBPattern(void* packedDIB, DIBType type)
        {
            HBRUSH brush = Gdi32.CreateDIBPatternBrushPt(packedDIB, (uint)type);
            if (brush == HBRUSH.Zero)
            { throw new GdiException($"Failed to createDIB pattern brush"); }
            return new Brush(brush);
        }

        /// <exception cref="NotSupportedException"/>
        public static Brush GetSystem(int index)
        {
            HBRUSH brush = User32.GetSysColorBrush(index);
            if (brush == HBRUSH.Zero)
            { throw new NotSupportedException($"System brush {index} not supported by the current platform"); }
            return new Brush(brush);
        }

        public static implicit operator HBRUSH(Brush brush) => brush.Handle;

        public void Use(HDC deviceContext) => Gdi32.SelectObject(deviceContext, Handle);

        /// <exception cref="GdiException"/>
        public void Dispose()
        {
            if (Gdi32.DeleteObject(Handle) == 0)
            { throw new GdiException($"Failed to delete object ({nameof(Brush)}) {this}"); }
        }

        public static bool operator ==(Brush left, Brush right) => left.Equals(right);
        public static bool operator !=(Brush left, Brush right) => !(left == right);

        public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        public override bool Equals(object? obj) => obj is Brush brush && Equals(brush);
        public bool Equals(Brush other) => Handle.Equals(other.Handle);
        public override int GetHashCode() => Handle.GetHashCode();
    }
}
