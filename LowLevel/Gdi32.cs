global using COLORREF = System.UInt32;

using System.Runtime.InteropServices;

namespace Win32.Gdi32.LowLevel
{
    /// <summary>
    /// Windows GDI
    /// </summary>
    [SupportedOSPlatform("windows")]
    public static class Gdi32
    {
        public const uint GDI_ERROR = unchecked((uint)0xFFFFFFFFL);
        public static readonly nint HGDI_ERROR = unchecked((nint)GDI_ERROR);

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe int StretchDIBits(
          [In] HDC hdc,
          [In] int xDest,
          [In] int yDest,
          [In] int DestWidth,
          [In] int DestHeight,
          [In] int xSrc,
          [In] int ySrc,
          [In] int SrcWidth,
          [In] int SrcHeight,
          [In] void* lpBits,
          [In] BitmapInfo* lpbmi,
          [In] UINT iUsage,
          [In] DWORD rop
        );

        /// <summary>
        /// The GdiFlush function flushes the calling thread's current batch.
        /// </summary>
        /// <returns>
        /// <para>
        /// If all functions in the current batch succeed, the return value is nonzero.
        /// </para>
        /// <para>
        /// If not all functions in the current batch succeed, the return value is zero,
        /// indicating that at least one function returned an error.
        /// </para>
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL GdiFlush();

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL Ellipse(
          [In] HDC hdc,
          [In] int left,
          [In] int top,
          [In] int right,
          [In] int bottom
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe HBRUSH CreateDIBPatternBrushPt(
          [In] void* lpPackedDIB,
          [In] UINT iUsage
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern HBRUSH CreatePatternBrush(
          [In] HBITMAP hbm
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern HBRUSH CreateHatchBrush(
          [In] int iHatch,
          [In] COLORREF color
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL InvertRgn(
          [In] HDC hdc,
          [In] HRGN hrgn
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL FrameRgn(
          [In] HDC hdc,
          [In] HRGN hrgn,
          [In] HBRUSH hbr,
          [In] int w,
          [In] int h
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe BOOL RectInRegion(
          [In] HRGN hrgn,
          [In] RECT* lprect
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL PtInRegion(
          [In] HRGN hrgn,
          [In] int x,
          [In] int y
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL PaintRgn(
          [In] HDC hdc,
          [In] HRGN hrgn
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe BOOL GetTextExtentPoint32W(
          [In] HDC hdc,
          [In] WCHAR* lpString,
          [In] int c,
          [Out] SIZE* psizl
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern HGDIOBJ GetStockObject(
          [In] int i
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL Rectangle(
          [In] HDC hdc,
          [In] int left,
          [In] int top,
          [In] int right,
          [In] int bottom
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern HPEN CreatePen(
          [In] int iStyle,
          [In] int cWidth,
          [In] COLORREF color
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL FillRgn(
          [In] HDC hdc,
          [In] HRGN hrgn,
          [In] HBRUSH hbr
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern HBRUSH CreateSolidBrush(
          [In] COLORREF color
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe HRGN CreateRectRgnIndirect(
          [In] RECT* lprect
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern COLORREF GetBkColor(
          [In] HDC hdc
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern COLORREF SetBkColor(
          [In] HDC hdc,
          [In] COLORREF color
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL LineTo(
          [In] HDC hdc,
          [In] int x,
          [In] int y
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe BOOL MoveToEx(
          [In] HDC hdc,
          [In] int x,
          [In] int y,
          [Out] POINT* lppt
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe HDC CreateICW(
          [In] WCHAR* pszDriver,
          [In] WCHAR* pszDevice,
               WCHAR* pszPort,
          [In] DevMode* pdm
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern COLORREF GetDCPenColor(
          [In] HDC hdc
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern COLORREF SetDCPenColor(
          [In] HDC hdc,
          [In] COLORREF color
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern COLORREF SetDCBrushColor(
          [In] HDC hdc,
          [In] COLORREF color
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern COLORREF GetDCBrushColor(
          [In] HDC hdc
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe int EnumObjects(
          [In] HDC hdc,
          [In] int nType,
          [In] delegate*<void*, LPARAM, int> lpFunc,
          [In] LPARAM lParam
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern HGDIOBJ GetCurrentObject(
          [In] HDC hdc,
          [In] UINT type
        );

        /// <summary>
        /// The <c>CreateDIBSection</c> function creates a DIB that applications can
        /// write to directly. The function gives you a pointer to the location
        /// of the bitmap bit values. You can supply a handle to a file-mapping
        /// object that the function will use to create the bitmap, or you can
        /// let the system allocate the memory for the bitmap.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="pbmi"></param>
        /// <param name="usage"></param>
        /// <param name="ppvBits"></param>
        /// <param name="hSection"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe HBITMAP CreateDIBSection(
          [In] HDC hdc,
          [In] BitmapInfo* pbmi,
          [In] UINT usage,
          [Out] void** ppvBits,
          [In] HANDLE hSection,
          [In] DWORD offset
        );

        /// <summary>
        /// The <c>GetDIBits</c> function retrieves the bits of the specified compatible bitmap and copies them
        /// into a buffer as a DIB using the specified format.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="hbm"></param>
        /// <param name="start"></param>
        /// <param name="cLines"></param>
        /// <param name="lpvBits"></param>
        /// <param name="lpbmi"></param>
        /// <param name="usage"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe int GetDIBits(
          [In] HDC hdc,
          [In] HBITMAP hbm,
          [In] UINT start,
          [In] UINT cLines,
          [Out] void* lpvBits,
          [In, Out] BitmapInfo* lpbmi,
          [In] UINT usage
        );

        /// <summary>
        /// The <c>GetObject</c> function retrieves information for the specified graphics object.
        /// </summary>
        /// <param name="h"></param>
        /// <param name="c"></param>
        /// <param name="pv"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe int GetObject(
          [In] HANDLE h,
          [In] int c,
          [Out] void* pv
        );

        /// <summary>
        /// The <c>BitBlt</c> function performs a bit-block transfer of the color data
        /// corresponding to a rectangle of pixels from the specified source
        /// device context into a destination device context.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="hdcSrc"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="rop"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL BitBlt(
          [In] HDC hdc,
          [In] int x,
          [In] int y,
          [In] int cx,
          [In] int cy,
          [In] HDC hdcSrc,
          [In] int x1,
          [In] int y1,
          [In] DWORD rop
        );

        /// <summary>
        /// The <c>SelectObject</c> function selects an object into the specified device
        /// context (DC). The new object replaces the previous object of the same type.
        /// </summary>
        /// <param name="hdc">
        /// A handle to the DC.
        /// </param>
        /// <param name="h"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern HGDIOBJ SelectObject(
          [In] HDC hdc,
          [In] HGDIOBJ h
        );

        /// <summary>
        /// The <c>CreateCompatibleDC</c> function creates a memory
        /// device context (<c>DC</c>) compatible with the specified device.
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern HDC CreateCompatibleDC(
          [In] HDC hdc
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern int GetStretchBltMode(
          [In] HDC hdc
        );

        /// <summary>
        /// The <c>SetStretchBltMode</c> function sets the
        /// bitmap stretching mode in the specified device context.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern int SetStretchBltMode(
          [In] HDC hdc,
          [In] int mode
        );

        /// <summary>
        /// The <c>StretchBlt</c> function copies a bitmap from a source rectangle into
        /// a destination rectangle, stretching or compressing the bitmap to
        /// fit the dimensions of the destination rectangle, if necessary.
        /// The system stretches or compresses the bitmap according to the
        /// stretching mode currently set in the destination device context.
        /// </summary>
        /// <param name="hdcDest"></param>
        /// <param name="xDest"></param>
        /// <param name="yDest"></param>
        /// <param name="wDest"></param>
        /// <param name="hDest"></param>
        /// <param name="hdcSrc"></param>
        /// <param name="xSrc"></param>
        /// <param name="ySrc"></param>
        /// <param name="wSrc"></param>
        /// <param name="hSrc"></param>
        /// <param name="rop"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL StretchBlt(
          [In] HDC hdcDest,
          [In] int xDest,
          [In] int yDest,
          [In] int wDest,
          [In] int hDest,
          [In] HDC hdcSrc,
          [In] int xSrc,
          [In] int ySrc,
          [In] int wSrc,
          [In] int hSrc,
          [In] DWORD rop
        );

        /// <summary>
        /// The <c>CreateCompatibleBitmap</c> function creates a bitmap compatible
        /// with the device that is associated with the specified device context.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern HBITMAP CreateCompatibleBitmap(
          [In] HDC hdc,
          [In] int cx,
          [In] int cy
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL SetPixelV(
          [In] HDC hdc,
          [In] int x,
          [In] int y,
          [In] COLORREF color
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern COLORREF SetPixel(
          [In] HDC hdc,
          [In] int x,
          [In] int y,
          [In] COLORREF color
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern COLORREF GetPixel(
          [In] HDC hdc,
          [In] int x,
          [In] int y
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern unsafe HBITMAP CreateBitmap(
          [In] int nWidth,
          [In] int nHeight,
          [In] UINT nPlanes,
          [In] UINT nBitCount,
          [In] void* lpBits
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL DeleteDC(
          [In] HDC hdc
        );

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern BOOL DeleteObject(
          [In] HANDLE ho
        );
    }
}
