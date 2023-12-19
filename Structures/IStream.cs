using System.Runtime.InteropServices;

namespace Win32.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000000c-0000-0000-C000-000000000046")]
    [SupportedOSPlatform("windows")]
    public interface IStream : ISequentialStream
    {
        abstract unsafe HRESULT Seek(
            [In] INT64 dlibMove,
            [In] DWORD dwOrigin,
            [Out, Optional] UINT64* plibNewPosition);

        abstract HRESULT SetSize(
            [In] UINT64 libNewSize);

        abstract unsafe HRESULT CopyTo(
            [In, MarshalAs(UnmanagedType.IUnknown)] IStream pstm,
            [In] UINT64 cb,
            [Out, Optional] UINT64* pcbRead,
            [Out, Optional] UINT64* pcbWritten);

        abstract HRESULT Commit(
            [In] DWORD grfCommitFlags);

        abstract HRESULT Revert();

        abstract HRESULT LockRegion(
            [In] UINT64 libOffset,
            [In] UINT64 cb,
            [In] DWORD dwLockType);

        abstract HRESULT UnlockRegion(
            [In] UINT64 libOffset,
            [In] UINT64 cb,
            [In] DWORD dwLockType);

        abstract unsafe HRESULT Stat(
            [Out] StgStatistic* pstatstg,
            [In] DWORD grfStatFlag);

        abstract HRESULT Clone(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IStream ppstm);
    }
}
