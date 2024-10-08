﻿using System.Globalization;
using System.Runtime.CompilerServices;

namespace Win32.Gdi32;

[SupportedOSPlatform("windows")]
public abstract class DC : IDisposable, IEquatable<DC>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected HDC Handle;

    protected DC(HDC handle)
    {
        Handle = handle;
    }

    public static implicit operator HDC(DC dc) => dc.Handle;

    /// <exception cref="GdiException"/>
    public static void CopyTo(RECT sourceRect, HDC source, RECT destinationRect, HDC destination)
    {
        if (Gdi32.StretchBlt(destination,
            destinationRect.X, destinationRect.Y,
            destinationRect.Width, destinationRect.Height,
            source,
            sourceRect.X, sourceRect.Y,
            sourceRect.Width, sourceRect.Height,
            0x00CC0020) == 0) // SRCCOPY = 0x00CC0020
        { throw new GdiException($"{nameof(Gdi32.StretchBlt)} has failed"); }
    }

    /// <exception cref="GdiException"/>
    public static void CopyTo(POINT sourceStart, HDC source, RECT destinationRect, HDC destination)
    {
        if (Gdi32.BitBlt(destination,
            destinationRect.X, destinationRect.Y,
            destinationRect.Width, destinationRect.Height,
            source,
            sourceStart.X, sourceStart.Y,
            0x00CC0020) == 0) // SRCCOPY = 0x00CC0020
        { throw new GdiException($"{nameof(Gdi32.BitBlt)} has failed"); }
    }

    /// <exception cref="GdiException"/>
    public unsafe SIZE MeasureText(string text)
    {
        SIZE size = default;
        fixed (WCHAR* textPtr = text)
        {
            if (Gdi32.GetTextExtentPoint32W(Handle, textPtr, text.Length, out size) == FALSE)
            { throw new GdiException($"{nameof(Gdi32.GetTextExtentPoint32W)} has failed"); }
        }
        return size;
    }

    /// <exception cref="GdiException"/>
    public unsafe void MoveTo(POINT point)
    {
        if (Gdi32.MoveToEx(Handle, point.X, point.Y, out _) == FALSE)
        { throw new GdiException($"{nameof(Gdi32.MoveToEx)} has failed"); }
    }
    /// <exception cref="GdiException"/>
    public unsafe void MoveTo(int x, int y)
    {
        if (Gdi32.MoveToEx(Handle, x, y, out _) == 0)
        { throw new GdiException($"{nameof(Gdi32.MoveToEx)} has failed"); }
    }

    /// <exception cref="GdiException"/>
    public unsafe void LineTo(POINT point)
    {
        if (Gdi32.LineTo(Handle, point.X, point.Y) == FALSE)
        { throw new GdiException($"{nameof(Gdi32.LineTo)} has failed"); }
    }
    /// <exception cref="GdiException"/>
    public unsafe void LineTo(int x, int y)
    {
        if (Gdi32.LineTo(Handle, x, y) == FALSE)
        { throw new GdiException($"{nameof(Gdi32.LineTo)} has failed"); }
    }

    public unsafe void DrawText(string text, RECT rect, uint format = DrawTextFormat.Center | DrawTextFormat.NoClip)
    {
        fixed (WCHAR* textPtr = text)
        { _ = User32.DrawTextW(Handle, textPtr, text.Length, &rect, format); }
    }
    public unsafe void DrawText(string text, RECT* rect, uint format = DrawTextFormat.Center | DrawTextFormat.NoClip)
    {
        fixed (WCHAR* textPtr = text)
        { _ = User32.DrawTextW(Handle, textPtr, text.Length, rect, format); }
    }
    public unsafe void DrawText(string text, ref RECT rect, uint format = DrawTextFormat.Center | DrawTextFormat.NoClip)
    {
        fixed (WCHAR* textPtr = text)
        { _ = User32.DrawTextW(Handle, textPtr, text.Length, (RECT*)Unsafe.AsPointer(ref rect), format); }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SelectObject(HDC dc, HGDIOBJ obj) => Gdi32.SelectObject(dc, obj);

    /// <exception cref="GdiException"/>
    public unsafe void DrawRect(RECT rect)
    {
        if (Gdi32.Rectangle(Handle, rect.Left, rect.Top, rect.Right, rect.Bottom) == 0)
        { throw new GdiException($"{nameof(Gdi32.Rectangle)} has failed"); }
    }
    /// <exception cref="GdiException"/>
    public unsafe void DrawRect(int left, int top, int right, int bottom)
    {
        if (Gdi32.Rectangle(Handle, left, top, right, bottom) == 0)
        { throw new GdiException($"{nameof(Gdi32.Rectangle)} has failed"); }
    }

    /// <exception cref="GdiException"/>
    public void DrawEllipse(RECT rect)
    {
        if (Gdi32.Ellipse(Handle, rect.Left, rect.Top, rect.Right, rect.Bottom) == FALSE)
        { throw new GdiException($"{nameof(Gdi32.FrameRgn)} has failed"); }
    }
    /// <exception cref="GdiException"/>
    public void DrawEllipse(int left, int top, int right, int bottom)
    {
        if (Gdi32.Ellipse(Handle, left, top, right, bottom) == FALSE)
        { throw new GdiException($"{nameof(Gdi32.FrameRgn)} has failed"); }
    }

    /// <exception cref="GdiException"/>
    public void OutlineRegion(HRGN region, HBRUSH brush, int w, int h)
    {
        if (Gdi32.FrameRgn(Handle, region, brush, w, h) == FALSE)
        { throw new GdiException($"{nameof(Gdi32.FrameRgn)} has failed"); }
    }

    /// <exception cref="GdiException"/>
    public void InvertRegion(HRGN region)
    {
        if (Gdi32.InvertRgn(Handle, region) == FALSE)
        { throw new GdiException($"{nameof(Gdi32.FrameRgn)} has failed"); }
    }

    /// <exception cref="GdiException"/>
    public void Fill(HRGN region, HBRUSH brush)
    {
        if (Gdi32.FillRgn(Handle, region, brush) == 0)
        { throw new GdiException($"{nameof(Gdi32.FillRgn)} has failed"); }
    }
    /// <exception cref="GdiException"/>
    public void Fill(HRGN region)
    {
        if (Gdi32.PaintRgn(Handle, region) == 0)
        { throw new GdiException($"{nameof(Gdi32.PaintRgn)} has failed"); }
    }

    /// <exception cref="GdiException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public GdiColor BrushColor
    {
        get
        {
            COLORREF color = Gdi32.GetDCBrushColor(Handle);
            if (color == 0xFFFFFFFF)
            { throw new GdiException($"{nameof(Gdi32.GetDCBrushColor)} has failed"); }
            return color;
        }
        set
        {
            COLORREF prevColor = Gdi32.SetDCBrushColor(Handle, value);
            if (prevColor == 0xFFFFFFFF)
            { throw new GdiException($"{nameof(Gdi32.SetDCBrushColor)} has failed"); }
        }
    }

    /// <exception cref="GdiException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public GdiColor PenColor
    {
        get
        {
            COLORREF color = Gdi32.GetDCPenColor(Handle);
            if (color == 0xFFFFFFFF)
            { throw new GdiException($"{nameof(Gdi32.GetDCPenColor)} has failed"); }
            return color;
        }
        set
        {
            COLORREF prevColor = Gdi32.SetDCPenColor(Handle, value);
            if (prevColor == 0xFFFFFFFF)
            { throw new GdiException($"{nameof(Gdi32.SetDCPenColor)} has failed"); }
        }
    }

    /// <exception cref="GdiException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public GdiColor BackgroundColor
    {
        get
        {
            COLORREF color = Gdi32.GetBkColor(Handle);
            if (color == 0xFFFFFFFF)
            { throw new GdiException($"{nameof(Gdi32.GetBkColor)} has failed"); }
            return color;
        }
        set
        {
            if (Gdi32.SetBkColor(Handle, value) == 0xFFFFFFFF)
            { throw new GdiException($"{nameof(Gdi32.SetBkColor)} has failed"); }
        }
    }

    /// <exception cref="GdiException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public StretchMode StretchMode
    {
        get
        {
            StretchMode mode = (StretchMode)Gdi32.GetStretchBltMode(Handle);
            if (!Enum.IsDefined(mode))
            { throw new GdiException($"Invalid {nameof(StretchMode)} {mode}"); }
            return mode;
        }
        set
        {
            if (Gdi32.SetStretchBltMode(Handle, (int)value) == FALSE)
            { throw new GdiException($"{nameof(Gdi32.SetStretchBltMode)} has failed"); }
        }
    }

    public GdiColor GetPixel(int x, int y) => Gdi32.GetPixel(Handle, x, y);
    /// <exception cref="GdiException"/>
    public void SetPixel(int x, int y, GdiColor color)
    {
        if (Gdi32.SetPixel(Handle, x, y, color) == unchecked((uint)-1))
        { throw new GdiException($"{nameof(Gdi32.SetPixel)} has failed"); }
    }
    /// <exception cref="GdiException"/>
    public void SetPixelV(int x, int y, GdiColor color)
    {
        if (Gdi32.SetPixelV(Handle, x, y, color) == 0)
        { throw new GdiException($"{nameof(Gdi32.SetPixelV)} has failed"); }
    }

    public unsafe int StretchDIBits(
      int xDest,
      int yDest,
      int DestWidth,
      int DestHeight,
      int xSrc,
      int ySrc,
      int SrcWidth,
      int SrcHeight,
      void* lpBits,
      BitmapInfo* lpbmi,
      UINT iUsage,
      DWORD rop) =>
        Gdi32.StretchDIBits(
            Handle,
            xDest, yDest,
            DestWidth, DestHeight,
            xSrc, ySrc,
            SrcWidth, SrcHeight,
            lpBits,
            lpbmi,
            iUsage,
            rop);

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

    protected abstract void Dispose(bool disposing);
    ~DC() { Dispose(disposing: false); }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public override int GetHashCode() => Handle.GetHashCode();
    public override bool Equals(object? obj) => Equals(obj as DC);
    public bool Equals(DC? other) => other is not null && Handle == other.Handle;

    public static bool operator !=(DC? a, DC? b) => !(a == b);
    public static bool operator ==(DC? a, DC? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Handle == b.Handle;
    }
}
