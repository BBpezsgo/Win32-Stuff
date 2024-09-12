namespace Win32.COM;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("00000109-0000-0000-C000-000000000046")]
[SupportedOSPlatform("windows")]
public interface IPersistStream : IPersist
{
    abstract HRESULT IsDirty();
    abstract HRESULT Load([In, MarshalAs(UnmanagedType.IUnknown)] IStream pStm);
    abstract HRESULT Save([In, MarshalAs(UnmanagedType.IUnknown)] IStream pStm, [In] BOOL fClearDirty);
    abstract unsafe HRESULT GetSizeMax([Out] UINT64* pcbSize);
}
