using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// The root factory interface for all of D2D's objects.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("06152247-6f50-465a-9245-118bfd3b6007")]
    [SupportedOSPlatform("windows")]
    public interface ID2D1Factory
    {
        /// <summary>
        /// Cause the factory to refresh any system metrics that it might have been snapped
        /// on factory creation.
        /// </summary>
        abstract HRESULT ReloadSystemMetrics();

        /// <summary>
        /// Retrieves the current desktop DPI. To refresh this, call ReloadSystemMetrics.
        /// </summary>
        [Obsolete("Deprecated. Use DisplayInformation::LogicalDpi for Windows Store Apps or GetDpiForWindow for desktop apps.")]
        abstract unsafe void GetDesktopDpi(
            [Out] FLOAT* dpiX,
            [Out] FLOAT* dpiY
        );

        abstract unsafe HRESULT CreateRectangleGeometry(
            [In] D2D1_RECT_F* rectangle,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object rectangleGeometry
        );

        abstract unsafe HRESULT CreateRoundedRectangleGeometry(
            [In] D2D1_ROUNDED_RECT* roundedRectangle,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object roundedRectangleGeometry
        );

        abstract unsafe HRESULT CreateEllipseGeometry(
            [In] D2D1_ELLIPSE* ellipse,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ellipseGeometry
        );

        /// <summary>
        /// Create a geometry which holds other geometries.
        /// </summary>
        abstract HRESULT CreateGeometryGroup(
            D2D1_FILL_MODE fillMode,
            [MarshalAs(UnmanagedType.IUnknown)] ref object geometries,
            UINT32 geometriesCount,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object geometryGroup
        );

        abstract unsafe HRESULT CreateTransformedGeometry(
            [In, MarshalAs(UnmanagedType.IUnknown)] ID2D1Geometry sourceGeometry,
            [In] D2D1_MATRIX_3X2_F* transform,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object transformedGeometry
        );

        /// <summary>
        /// Returns an initially empty path geometry interface. A geometry sink is created
        /// off the interface to populate it.
        /// </summary>
        abstract HRESULT CreatePathGeometry(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out ID2D1PathGeometry pathGeometry
        );

        /// <summary>
        /// Allows a non-default stroke style to be specified for a given geometry at draw
        /// time.
        /// </summary>
        abstract unsafe HRESULT CreateStrokeStyle(
            [In] D2D1_STROKE_STYLE_PROPERTIES* strokeStyleProperties,
            FLOAT* dashes,
            UINT32 dashesCount,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object strokeStyle
        );

        /// <summary>
        /// Creates a new drawing state block, this can be used in subsequent
        /// SaveDrawingState and RestoreDrawingState operations on the render target.
        /// </summary>
        abstract unsafe HRESULT CreateDrawingStateBlock(
            [In, Optional] D2D1_DRAWING_STATE_DESCRIPTION* drawingStateDescription,
            [In, Optional, MarshalAs(UnmanagedType.IUnknown)] COM.IDWriteRenderingParams textRenderingParams,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object drawingStateBlock
            );

        /// <summary>
        /// Creates a render target which is a source of bitmaps.
        /// </summary>
        abstract unsafe HRESULT CreateWicBitmapRenderTarget(
            [In, MarshalAs(UnmanagedType.IUnknown)] COM.IWICBitmap target,
            [In] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object renderTarget
        );

        /// <summary>
        /// Creates a render target that appears on the display.
        /// </summary>
        abstract unsafe HRESULT CreateHwndRenderTarget(
            [In] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties,
            [In] D2D1_HWND_RENDER_TARGET_PROPERTIES* hwndRenderTargetProperties,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object hwndRenderTarget
        );

        /// <summary>
        /// Creates a render target that draws to a DXGI Surface. The device that owns the
        /// surface is used for rendering.
        /// </summary>
        abstract unsafe HRESULT CreateDxgiSurfaceRenderTarget(
            [In, MarshalAs(UnmanagedType.IUnknown)] COM.IDXGISurface dxgiSurface,
            [In] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object renderTarget
        );

        /// <summary>
        /// Creates a render target that draws to a GDI device context.
        /// </summary>
        abstract unsafe HRESULT CreateDCRenderTarget(
            [In] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object dcRenderTarget
        );
    }
}
