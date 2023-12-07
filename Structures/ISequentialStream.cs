using System.Runtime.InteropServices;

namespace Win32.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d")]
    public interface ISequentialStream
    {
        unsafe abstract HRESULT Read(
            void* pv,
            [In] ULONG cb,
            [Out, Optional] ULONG* pcbRead
        );

        unsafe abstract HRESULT Write(
            void* pv,
            [In] ULONG cb,
            [Out, Optional] ULONG* pcbWritten
        );
    }
}
