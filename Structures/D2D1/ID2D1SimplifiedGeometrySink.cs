using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("2cd9069e-12e2-11dc-9fed-001143a055f9")]
    [SupportedOSPlatform("windows")]
    public interface ID2D1SimplifiedGeometrySink
    {
        abstract void SetFillMode(
            D2D1_FILL_MODE fillMode
        );

        abstract void SetSegmentFlags(
            D2D1_PATH_SEGMENT vertexFlags
        );

        abstract void BeginFigure(
            D2D1_POINT_2F startPoint,
            D2D1_FIGURE_BEGIN figureBegin
        );

        abstract unsafe void AddLines(
            D2D1_POINT_2F* points,
            UINT32 pointsCount
        );

        abstract unsafe void AddBeziers(
            D2D1_BEZIER_SEGMENT* beziers,
            UINT32 beziersCount
        );

        abstract void EndFigure(
            D2D1_FIGURE_END figureEnd
        );

        abstract HRESULT Close(
        );
    }
}
