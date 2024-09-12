namespace Win32.COM;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d")]
[SupportedOSPlatform("windows")]
public interface ISequentialStream
{
    abstract unsafe HRESULT Read(
        void* pv,
        [In] ULONG cb,
        [Out, Optional] ULONG* pcbRead
    );

    abstract unsafe HRESULT Write(
        void* pv,
        [In] ULONG cb,
        [Out, Optional] ULONG* pcbWritten
    );
}
