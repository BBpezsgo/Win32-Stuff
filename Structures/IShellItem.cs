using System.Runtime.InteropServices;

namespace Win32.COM
{
    [Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
    [SupportedOSPlatform("windows")]
    public interface IShellItem
    {
        unsafe abstract HRESULT BindToHandler(
             [In, MarshalAs(UnmanagedType.IUnknown)] IBindCtx? pbc,
             [In] Guid bhid,
             [In] REFIID riid,
             [Out, MarshalAs(UnmanagedType.IUnknown)] out object? ppv
        );

        abstract HRESULT GetParent(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IShellItem? ppsi
        );

        unsafe abstract HRESULT GetDisplayName(
            [In] SIGDN sigdnName,
            [Out] WCHAR** ppszName
        );

        unsafe abstract HRESULT GetAttributes(
            [In] ULONG sfgaoMask,
            [Out] ULONG* psfgaoAttribs
        );

        unsafe abstract HRESULT Compare(
            [In, MarshalAs(UnmanagedType.IUnknown)] IShellItem psi,
            [In] DWORD hint,
            [Out] int* piOrder
        );
    }
}
