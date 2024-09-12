namespace Win32.D2D1;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("2cd9069e-12e2-11dc-9fed-001143a055f9")]
[SupportedOSPlatform("windows")]
public interface ID2D1SimplifiedGeometrySink
{
    abstract void SetFillMode(
        FillMode fillMode
    );

    abstract void SetSegmentFlags(
        PathSegment vertexFlags
    );

    abstract void BeginFigure(
        Point2F startPoint,
        FigureBegin figureBegin
    );

    abstract unsafe void AddLines(
        Point2F* points,
        UINT32 pointsCount
    );

    abstract unsafe void AddBeziers(
        BezierSegment* beziers,
        UINT32 beziersCount
    );

    abstract void EndFigure(
        FigureEnd figureEnd
    );

    abstract HRESULT Close(
    );
}
