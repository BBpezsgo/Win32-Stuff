using System.Runtime.CompilerServices;

namespace Win32
{
    public enum StretchMode
    {
        /// <summary>
        /// Performs a Boolean AND operation using the color values
        /// for the eliminated and existing pixels. If the bitmap is a
        /// monochrome bitmap, this mode preserves black pixels
        /// at the expense of white pixels.
        /// </summary>
        BLACKONWHITE = 1,
        /// <summary>
        /// Deletes the pixels. This mode deletes all eliminated
        /// lines of pixels without trying to preserve their information.
        /// </summary>
        COLORONCOLOR = 3,
        /// <summary>
        /// Maps pixels from the source rectangle into blocks of
        /// pixels in the destination rectangle. The average color over
        /// the destination block of pixels approximates the color of the
        /// source pixels. After setting the <c>HALFTONE</c> stretching mode,
        /// an application must call the <c>SetBrushOrgEx</c> function to set
        /// the brush origin. If it fails to do so, brush misalignment occurs.
        /// </summary>
        HALFTONE = 4,
        /// <summary>
        /// Performs a Boolean OR operation using the color values
        /// for the eliminated and existing pixels. If the bitmap
        /// is a monochrome bitmap, this mode preserves white pixels
        /// at the expense of black pixels.
        /// </summary>
        WHITEONBLACK = 2,

        STRETCH_ANDSCANS = BLACKONWHITE,
        STRETCH_DELETESCANS = COLORONCOLOR,
        STRETCH_HALFTONE = HALFTONE,
        STRETCH_ORSCANS = WHITEONBLACK,
    }

    public enum ObjectType : uint
    {
        PEN = OBJ.PEN,
        BRUSH = OBJ.BRUSH,
        DC = OBJ.DC,
        METADC = OBJ.METADC,
        PAL = OBJ.PAL,
        FONT = OBJ.FONT,
        BITMAP = OBJ.BITMAP,
        REGION = OBJ.REGION,
        METAFILE = OBJ.METAFILE,
        MEMDC = OBJ.MEMDC,
        EXTPEN = OBJ.EXTPEN,
        ENHMETADC = OBJ.ENHMETADC,
        ENHMETAFILE = OBJ.ENHMETAFILE,
        COLORSPACE = OBJ.COLORSPACE,
    }

    public struct DC
    {
        /// <exception cref="NotWindowsException"/>
        public static void CopyTo(RECT sourceRect, HDC source, RECT destinationRect, HDC destination)
        {
            if (Gdi32.StretchBlt(destination,
                destinationRect.X, destinationRect.Y,
                destinationRect.Width, destinationRect.Height,
                source,
                sourceRect.X, sourceRect.Y,
                sourceRect.Width, sourceRect.Height,
                0x00CC0020) == 0) // SRCCOPY = 0x00CC0020
            { throw new NotWindowsException($"{nameof(Gdi32.StretchBlt)} has failed"); }
        }

        /// <exception cref="NotWindowsException"/>
        public static void CopyTo(POINT sourceStart, HDC source, RECT destinationRect, HDC destination)
        {
            if (Gdi32.BitBlt(destination,
                destinationRect.X, destinationRect.Y,
                destinationRect.Width, destinationRect.Height,
                source,
                sourceStart.X, sourceStart.Y,
                0x00CC0020) == 0) // SRCCOPY = 0x00CC0020
            { throw new NotWindowsException($"{nameof(Gdi32.BitBlt)} has failed"); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GdiColor GetPixel(HDC dc, int x, int y) => Gdi32.GetPixel(dc, x, y);

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetPixel(HDC dc, int x, int y, GdiColor color)
        {
            if (Gdi32.SetPixel(dc, x, y, color) == unchecked((uint)-1))
            { throw new NotWindowsException($"{nameof(Gdi32.SetPixel)} has failed"); }
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetPixelV(HDC dc, int x, int y, GdiColor color)
        {
            if (Gdi32.SetPixelV(dc, x, y, color) == 0)
            { throw new NotWindowsException($"{nameof(Gdi32.SetPixelV)} has failed"); }
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetStretchMode(HDC dc, StretchMode mode)
        {
            if (Gdi32.SetStretchBltMode(dc, (int)mode) == FALSE)
            { throw new NotWindowsException($"{nameof(Gdi32.SetStretchBltMode)} has failed"); }
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static StretchMode GetStretchMode(HDC dc)
        {
            StretchMode mode = (StretchMode)Gdi32.GetStretchBltMode(dc);
            if (!Enum.IsDefined(mode))
            { throw new NotWindowsException($"Invalid {nameof(StretchMode)} {mode}"); }
            return mode;
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GdiColor GetBrushColor(HDC dc)
        {
            COLORREF color = Gdi32.GetDCBrushColor(dc);
            if (color == 0xFFFFFFFF)
            { throw new NotWindowsException($"{nameof(Gdi32.GetDCBrushColor)} has failed"); }
            return color;
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetBrushColor(HDC dc, GdiColor color)
        {
            COLORREF prevColor = Gdi32.SetDCBrushColor(dc, color);
            if (prevColor == 0xFFFFFFFF)
            { throw new NotWindowsException($"{nameof(Gdi32.SetDCBrushColor)} has failed"); }
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GdiColor GetPenColor(HDC dc)
        {
            COLORREF color = Gdi32.GetDCPenColor(dc);
            if (color == 0xFFFFFFFF)
            { throw new NotWindowsException($"{nameof(Gdi32.GetDCPenColor)} has failed"); }
            return color;
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetPenColor(HDC dc, GdiColor color)
        {
            COLORREF prevColor = Gdi32.SetDCPenColor(dc, color);
            if (prevColor == 0xFFFFFFFF)
            { throw new NotWindowsException($"{nameof(Gdi32.SetDCPenColor)} has failed"); }
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public static void MoveTo(HDC dc, POINT point) => DC.MoveTo(dc, point.X, point.Y);
        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public static void MoveTo(HDC dc, int x, int y)
        {
            if (Gdi32.MoveToEx(dc, x, y, null) == 0)
            { throw new NotWindowsException($"{nameof(Gdi32.MoveToEx)} has failed"); }
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public static void LineTo(HDC dc, POINT point) => DC.LineTo(dc, point.X, point.Y);
        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public static void LineTo(HDC dc, int x, int y)
        {
            if (Gdi32.LineTo(dc, x, y) == 0)
            { throw new NotWindowsException($"{nameof(Gdi32.LineTo)} has failed"); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public static void DrawText(HDC dc, string text, RECT rect, uint format = DT.CENTER | DT.NOCLIP)
        {
            fixed (WCHAR* textPtr = text)
            { _ = User32.DrawTextW(dc, textPtr, text.Length, &rect, format); }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public static void DrawText(HDC dc, string text, RECT* rect, uint format = DT.CENTER | DT.NOCLIP)
        {
            fixed (WCHAR* textPtr = text)
            { _ = User32.DrawTextW(dc, textPtr, text.Length, rect, format); }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public static void DrawText(HDC dc, string text, ref RECT rect, uint format = DT.CENTER | DT.NOCLIP)
        {
            fixed (WCHAR* textPtr = text)
            { _ = User32.DrawTextW(dc, textPtr, text.Length, (RECT*)Unsafe.AsPointer(ref rect), format); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SelectObject(HDC dc, HGDIOBJ obj) => Gdi32.SelectObject(dc, obj);

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public static void DrawRect(HDC dc, RECT rect)
        {
            if (Gdi32.Rectangle(dc, rect.Left, rect.Top, rect.Right, rect.Bottom) == 0)
            { throw new NotWindowsException($"{nameof(Gdi32.Rectangle)} has failed"); }
        }
        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe public static void DrawRect(HDC dc, int left, int top, int right, int bottom)
        {
            if (Gdi32.Rectangle(dc, left, top, right, bottom) == 0)
            { throw new NotWindowsException($"{nameof(Gdi32.Rectangle)} has failed"); }
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBgColor(HDC dc, GdiColor color)
        {
            if (Gdi32.SetBkColor(dc, color) == 0xFFFFFFFF)
            { throw new NotWindowsException($"{nameof(Gdi32.SetBkColor)} has failed"); }
        }
        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GdiColor GetBgColor(HDC dc)
        {
            COLORREF color = Gdi32.GetBkColor(dc);
            if (color == 0xFFFFFFFF)
            { throw new NotWindowsException($"{nameof(Gdi32.GetBkColor)} has failed"); }
            return color;
        }

        /// <exception cref="NotWindowsException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill(HDC hdc, HRGN region, HBRUSH brush)
        {
            if (Gdi32.FillRgn(hdc, region, brush) == 0)
            { throw new NotWindowsException($"{nameof(Gdi32.FillRgn)} has failed"); }
        }
    }
}
