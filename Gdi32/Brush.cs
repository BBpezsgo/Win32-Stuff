using System.Globalization;

namespace Win32.Gdi32;

public enum HatchStyle : int
{
    /// <summary>
    /// -----
    /// </summary>
    Horizontal = 0,
    /// <summary>
    /// |||||
    /// </summary>
    Vertical = 1,
    /// <summary>
    /// \\\\\
    /// </summary>
    FDiagonal = 2,
    /// <summary>
    /// /////
    /// </summary>
    BDiagonal = 3,
    /// <summary>
    /// +++++
    /// </summary>
    Cross = 4,
    /// <summary>
    /// xxxxx
    /// </summary>
    DiagonalCross = 5,
}

public enum DIBType : uint
{
    /// <summary>
    /// A color table is provided and consists of an array
    /// of 16-bit indexes into the logical palette of the
    /// device context into which the brush is to be selected.
    /// </summary>
    PALColors = 1, /* color table in palette indices */
    /// <summary>
    /// A color table is provided and contains literal RGB values.
    /// </summary>
    RGBColors = 0, /* color table in RGBs */
}

[SupportedOSPlatform("windows")]
public readonly struct Brush :
    IDisposable,
    IEquatable<Brush>
{
    readonly HBRUSH Handle;

    Brush(HBRUSH handle) => Handle = handle;

    /// <exception cref="GdiException"/>
    public static Brush CreateSolid(byte red, byte green, byte blue)
        => Brush.CreateSolid(GdiColor.Make(red, green, blue));

    /// <exception cref="GdiException"/>
    public static Brush CreateSolid(COLORREF color)
    {
        HBRUSH brush = Gdi32.CreateSolidBrush(color);
        if (brush == HBRUSH.Zero)
        { throw new GdiException("Failed to create solid brush"); }
        return new Brush(brush);
    }

    /// <exception cref="GdiException"/>
    public static Brush CreateHatch(HatchStyle hatch, COLORREF color)
    {
        HBRUSH brush = Gdi32.CreateHatchBrush((int)hatch, color);
        if (brush == HBRUSH.Zero)
        { throw new GdiException("Failed to create hatch brush"); }
        return new Brush(brush);
    }

    /// <exception cref="GdiException"/>
    public static Brush CreatePattern(HBITMAP bitmap)
    {
        HBRUSH brush = Gdi32.CreatePatternBrush(bitmap);
        if (brush == HBRUSH.Zero)
        { throw new GdiException("Failed to create pattern brush"); }
        return new Brush(brush);
    }

    /// <exception cref="GdiException"/>
    public static unsafe Brush CreateDIBPattern(void* packedDIB, DIBType type)
    {
        HBRUSH brush = Gdi32.CreateDIBPatternBrushPt(packedDIB, (uint)type);
        if (brush == HBRUSH.Zero)
        { throw new GdiException("Failed to createDIB pattern brush"); }
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

    /// <exception cref="GdiException"/>
    public HGDIOBJ Use(HDC deviceContext)
    {
        HGDIOBJ prevObj = Gdi32.SelectObject(deviceContext, Handle);
        if (prevObj == 0 || prevObj == Gdi32.HGDIError)
        { throw new GdiException($"Failed to select object {this} into DC {deviceContext} (error {prevObj})"); }
        return prevObj;
    }

    /// <exception cref="GdiException"/>
    public void Dispose()
    {
        if (Gdi32.DeleteObject(Handle) == 0)
        { throw new GdiException($"Failed to delete object ({nameof(Brush)}) {this}"); }
    }

    public static implicit operator HBRUSH(Brush v) => v.Handle;
    public static explicit operator Brush(HBRUSH v) => new(v);

    public static bool operator ==(Brush left, Brush right) => left.Equals(right);
    public static bool operator !=(Brush left, Brush right) => !left.Equals(right);

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
    public override bool Equals(object? obj) => obj is Brush brush && Equals(brush);
    public bool Equals(Brush other) => Handle == other.Handle;
    public override int GetHashCode() => Handle.GetHashCode();
}
