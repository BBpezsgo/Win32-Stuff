using System.Runtime.InteropServices;

#pragma warning disable CA1716 // Identifiers should not match keywords

namespace Win32.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000000f-0000-0000-C000-000000000046")]
    [SupportedOSPlatform("windows")]
    unsafe public interface IMoniker : IPersistStream
    {
        abstract HRESULT BindToObject(
            [In, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
            [In, Optional, MarshalAs(UnmanagedType.IUnknown)] IMoniker? pmkToLeft,
            [In] REFIID riidResult,
            [Out] void** ppvResult);

        abstract HRESULT BindToStorage(
            [In, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
            [In, Optional, MarshalAs(UnmanagedType.IUnknown)] IMoniker? pmkToLeft,
            [In] REFIID riid,
            [Out] void** ppvObj);

        abstract HRESULT Reduce(
            [In, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
            [In] DWORD dwReduceHowFar,
            [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref IMoniker ppmkToLeft,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IMoniker ppmkReduced);

        abstract HRESULT ComposeWith(
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkRight,
            [In] BOOL fOnlyIfNotGeneric,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IMoniker ppmkComposite);

        abstract HRESULT Enum(
            [In] BOOL fForward,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IEnumMoniker ppenumMoniker);

        abstract HRESULT IsEqual(
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkOtherMoniker);

        abstract HRESULT Hash(
            [Out] DWORD* pdwHash);

        abstract HRESULT IsRunning(
            [In, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkToLeft,
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkNewlyRunning);

        abstract HRESULT GetTimeOfLastChange(
            [In, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkToLeft,
            [Out] FileTime* pFileTime);

        abstract HRESULT Inverse(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IMoniker ppmk);

        abstract HRESULT CommonPrefixWith(
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkOther,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IMoniker ppmkPrefix);

        abstract HRESULT RelativePathTo(
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkOther,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IMoniker ppmkRelPath);

        abstract HRESULT GetDisplayName(
            [In, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkToLeft,
            [Out] WCHAR* ppszDisplayName);

        abstract HRESULT ParseDisplayName(
            [In, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkToLeft,
            [In] WCHAR* pszDisplayName,
            [Out] ULONG* pchEaten,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IMoniker ppmkOut);

        abstract HRESULT IsSystemMoniker(
            [Out] DWORD* pdwMksys);
    }
}
