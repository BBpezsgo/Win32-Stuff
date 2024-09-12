namespace Win32.D2D1;

/// <summary>
/// Represents a complex shape that may be composed of arcs, curves, and lines.
/// </summary>
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("2cd906a5-12e2-11dc-9fed-001143a055f9")]
[SupportedOSPlatform("windows")]
public interface ID2D1PathGeometry : ID2D1Geometry
{
    /// <summary>
    /// Opens a geometry sink that will be used to create this path geometry.
    /// </summary>
    abstract HRESULT Open(
        [Out, MarshalAs(UnmanagedType.IUnknown)] out ID2D1GeometrySink geometrySink
    );

    /// <summary>
    /// Retrieve the contents of this geometry. The caller passes an implementation of a
    /// ID2D1GeometrySink interface to receive the data.
    /// </summary>
    abstract HRESULT Stream(
        [In, MarshalAs(UnmanagedType.IUnknown)] ID2D1GeometrySink geometrySink
    );

    abstract unsafe HRESULT GetSegmentCount(
        [Out] UINT32* count
    );

    abstract unsafe HRESULT GetFigureCount(
        [Out] UINT32* count
    );
}
