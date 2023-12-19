using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("2cd9069f-12e2-11dc-9fed-001143a055f9")]
    [SupportedOSPlatform("windows")]
    public interface ID2D1GeometrySink : ID2D1SimplifiedGeometrySink
    {
        abstract void AddLine(
            D2D1_POINT_2F point
        );

        abstract unsafe void AddBezier(
            [In] D2D1_BEZIER_SEGMENT* bezier
        );

        // unsafe extern void AddQuadraticBezier(
        //     [In] D2D1_QUADRATIC_BEZIER_SEGMENT* bezier
        // );
        // 
        // unsafe extern void AddQuadraticBeziers(
        //     D2D1_QUADRATIC_BEZIER_SEGMENT* beziers,
        //     UINT32 beziersCount
        // );
        // 
        // unsafe extern void AddArc(
        //     [In] D2D1_ARC_SEGMENT* arc
        // );
    }
}
