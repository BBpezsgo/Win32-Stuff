﻿using System.Globalization;
using System.Runtime.CompilerServices;

namespace Win32.Gdi32;

[SupportedOSPlatform("windows")]
public readonly struct Region :
    IDisposable,
    IEquatable<Region>,
    System.Numerics.IEqualityOperators<Region, Region, bool>
{
    readonly HRGN Handle;

    Region(HRGN handle) => Handle = handle;

    /// <exception cref="GdiException"/>
    public static unsafe Region Create(Rect* rect)
    {
        HRGN region = Gdi32.CreateRectRgnIndirect(rect);
        if (region == HRGN.Zero)
        { throw new GdiException("Failed to create region"); }
        return new Region(region);
    }

    public static implicit operator HRGN(Region region) => region.Handle;

    /// <exception cref="GdiException"/>
    public int Use(HDC deviceContext)
    {
        HGDIOBJ result = Gdi32.SelectObject(deviceContext, Handle);
        if (result == 0 || result == Gdi32.HGDIError)
        { throw new GdiException($"Failed to select object {this} into DC {deviceContext} (error {result})"); }
        return (int)result;
    }

    public bool Contains(int x, int y) => Gdi32.PtInRegion(Handle, x, y) != FALSE;
    public bool Contains(POINT point) => Gdi32.PtInRegion(Handle, point.X, point.Y) != FALSE;

    public unsafe bool Overlaps(RECT* rect) => Gdi32.RectInRegion(Handle, rect) != FALSE;
    public unsafe bool Overlaps(RECT rect) => Gdi32.RectInRegion(Handle, &rect) != FALSE;
    public unsafe bool Overlaps(ref RECT rect) => Gdi32.RectInRegion(Handle, (RECT*)Unsafe.AsPointer(ref rect)) != FALSE;

    /// <exception cref="GdiException"/>
    public void Dispose()
    {
        if (Gdi32.DeleteObject(Handle) == 0)
        { throw new GdiException($"Failed to delete object ({nameof(Region)}) {this}"); }
    }

    public static bool operator ==(Region left, Region right) => left.Equals(right);
    public static bool operator !=(Region left, Region right) => !(left == right);

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
    public override bool Equals(object? obj) => obj is Region region && Equals(region);
    public bool Equals(Region other) => Handle == other.Handle;
    public override int GetHashCode() => Handle.GetHashCode();
}
