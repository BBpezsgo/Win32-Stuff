global using COLORREF = System.UInt32;

namespace Win32.Gdi32;

/// <summary>
/// Windows GDI
/// </summary>
[SupportedOSPlatform("windows")]
public static partial class Gdi32
{
    public const uint GDIError = unchecked((uint)0xFFFFFFFFL);
    public static readonly nint HGDIError = unchecked((nint)GDIError);

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial int StretchDIBits(
      HDC hdc,
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
      DWORD rop
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
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL GdiFlush();

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL Ellipse(
      HDC hdc,
      int left,
      int top,
      int right,
      int bottom
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial HBRUSH CreateDIBPatternBrushPt(
      void* lpPackedDIB,
      UINT iUsage
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial HBRUSH CreatePatternBrush(
      HBITMAP hbm
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial HBRUSH CreateHatchBrush(
      int iHatch,
      COLORREF color
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL InvertRgn(
      HDC hdc,
      HRGN hrgn
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL FrameRgn(
      HDC hdc,
      HRGN hrgn,
      HBRUSH hbr,
      int w,
      int h
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial BOOL RectInRegion(
      HRGN hrgn,
      RECT* lprect
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL PtInRegion(
      HRGN hrgn,
      int x,
      int y
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL PaintRgn(
      HDC hdc,
      HRGN hrgn
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial BOOL GetTextExtentPoint32W(
      HDC hdc,
      WCHAR* lpString,
      int c,
      out SIZE psizl
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial HGDIOBJ GetStockObject(
      int i
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL Rectangle(
      HDC hdc,
      int left,
      int top,
      int right,
      int bottom
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial HPEN CreatePen(
      int iStyle,
      int cWidth,
      COLORREF color
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL FillRgn(
      HDC hdc,
      HRGN hrgn,
      HBRUSH hbr
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial HBRUSH CreateSolidBrush(
      COLORREF color
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial HRGN CreateRectRgnIndirect(
      RECT* lprect
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial COLORREF GetBkColor(
      HDC hdc
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial COLORREF SetBkColor(
      HDC hdc,
      COLORREF color
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL LineTo(
      HDC hdc,
      int x,
      int y
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial BOOL MoveToEx(
      HDC hdc,
      int x,
      int y,
      out POINT lppt
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial HDC CreateICW(
      WCHAR* pszDriver,
      WCHAR* pszDevice,
           WCHAR* pszPort,
      DevMode* pdm
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial COLORREF GetDCPenColor(
      HDC hdc
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial COLORREF SetDCPenColor(
      HDC hdc,
      COLORREF color
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial COLORREF SetDCBrushColor(
      HDC hdc,
      COLORREF color
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial COLORREF GetDCBrushColor(
      HDC hdc
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial int EnumObjects(
      HDC hdc,
      int nType,
      delegate*<void*, LPARAM, int> lpFunc,
      LPARAM lParam
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial HGDIOBJ GetCurrentObject(
      HDC hdc,
      UINT type
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
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial HBITMAP CreateDIBSection(
      HDC hdc,
      BitmapInfo* pbmi,
      UINT usage,
      out void* ppvBits,
      HANDLE hSection,
      DWORD offset
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
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial int GetDIBits(
      HDC hdc,
      HBITMAP hbm,
      UINT start,
      UINT cLines,
      void* lpvBits, // out
      ref BitmapInfo lpbmi,
      UINT usage
    );

    /// <summary>
    /// The <c>GetObject</c> function retrieves information for the specified graphics object.
    /// </summary>
    /// <param name="h"></param>
    /// <param name="c"></param>
    /// <param name="pv"></param>
    /// <returns></returns>
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial int GetObject(
      HANDLE h,
      int c,
      void* pv // out
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
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL BitBlt(
      HDC hdc,
      int x,
      int y,
      int cx,
      int cy,
      HDC hdcSrc,
      int x1,
      int y1,
      DWORD rop
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
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial HGDIOBJ SelectObject(
      HDC hdc,
      HGDIOBJ h
    );

    /// <summary>
    /// The <c>CreateCompatibleDC</c> function creates a memory
    /// device context (<c>DC</c>) compatible with the specified device.
    /// </summary>
    /// <param name="hdc"></param>
    /// <returns></returns>
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial HDC CreateCompatibleDC(
      HDC hdc
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial int GetStretchBltMode(
      HDC hdc
    );

    /// <summary>
    /// The <c>SetStretchBltMode</c> function sets the
    /// bitmap stretching mode in the specified device context.
    /// </summary>
    /// <param name="hdc"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial int SetStretchBltMode(
      HDC hdc,
      int mode
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
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL StretchBlt(
      HDC hdcDest,
      int xDest,
      int yDest,
      int wDest,
      int hDest,
      HDC hdcSrc,
      int xSrc,
      int ySrc,
      int wSrc,
      int hSrc,
      DWORD rop
    );

    /// <summary>
    /// The <c>CreateCompatibleBitmap</c> function creates a bitmap compatible
    /// with the device that is associated with the specified device context.
    /// </summary>
    /// <param name="hdc"></param>
    /// <param name="cx"></param>
    /// <param name="cy"></param>
    /// <returns></returns>
    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial HBITMAP CreateCompatibleBitmap(
      HDC hdc,
      int cx,
      int cy
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL SetPixelV(
      HDC hdc,
      int x,
      int y,
      COLORREF color
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial COLORREF SetPixel(
      HDC hdc,
      int x,
      int y,
      COLORREF color
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial COLORREF GetPixel(
      HDC hdc,
      int x,
      int y
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static unsafe partial HBITMAP CreateBitmap(
      int nWidth,
      int nHeight,
      UINT nPlanes,
      UINT nBitCount,
      void* lpBits
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL DeleteDC(
      HDC hdc
    );

    [LibraryImport("Gdi32.dll", SetLastError = true)]
    public static partial BOOL DeleteObject(
      HANDLE ho
    );
}
