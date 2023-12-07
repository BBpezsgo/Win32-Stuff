using System.Runtime.InteropServices;

namespace Win32.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000010-0000-0000-C000-000000000046")]
    public interface IRunningObjectTable
    {
        unsafe abstract HRESULT Register(
            [In] DWORD grfFlags,
            [In, MarshalAs(UnmanagedType.IUnknown)] object punkObject,
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkObjectName,
            [Out] DWORD* pdwRegister);

        abstract HRESULT Revoke(
            [In] DWORD dwRegister);

        abstract HRESULT IsRunning(
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkObjectName);

        abstract HRESULT GetObject(
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkObjectName,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppunkObject);

        unsafe abstract HRESULT NoteChangeTime(
            [In] DWORD dwRegister,
            [In] FileTime* pfiletime);

        unsafe abstract HRESULT GetTimeOfLastChange(
            [In, MarshalAs(UnmanagedType.IUnknown)] IMoniker pmkObjectName,
            [Out] FileTime* pfiletime);

        abstract HRESULT EnumRunning([Out, MarshalAs(UnmanagedType.IUnknown)] out IEnumMoniker ppenumMoniker);
    }
}
