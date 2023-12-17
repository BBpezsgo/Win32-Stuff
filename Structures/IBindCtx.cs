using System.Runtime.InteropServices;

using OLECHAR = System.Char;

namespace Win32.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000000e-0000-0000-C000-000000000046")]
    [SupportedOSPlatform("windows")]
    public interface IBindCtx
    {
        abstract HRESULT RegisterObjectBound(
            [In, MarshalAs(UnmanagedType.IUnknown)] object punk
        );

        abstract HRESULT RevokeObjectBound(
            [In, MarshalAs(UnmanagedType.IUnknown)] object punk
        );

        abstract HRESULT ReleaseBoundObjects();

        unsafe abstract HRESULT SetBindOptions(
            [In] BindOptions* pbindopts
        );

        unsafe abstract HRESULT GetBindOptions(
            [In, Out] BindOptions* pbindopts
        );

        abstract HRESULT GetRunningObjectTable(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IRunningObjectTable pprot
        );

        unsafe abstract HRESULT RegisterObjectParam(
            [In] OLECHAR* pszKey,
            [In, MarshalAs(UnmanagedType.IUnknown)] object punk
        );

        unsafe abstract HRESULT GetObjectParam(
            [In] OLECHAR* pszKey,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object? ppunk
        );

        abstract HRESULT EnumObjectParam(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppenum // IEnumString
        );

        unsafe abstract HRESULT RevokeObjectParam(
            [In] OLECHAR* pszKey
        );
    }
}
