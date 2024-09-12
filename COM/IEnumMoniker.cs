namespace Win32.COM;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("00000102-0000-0000-C000-000000000046")]
[SupportedOSPlatform("windows")]
public interface IEnumMoniker
{
    abstract unsafe HRESULT Next(
        [In] ULONG celt,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out IMoniker rgelt,
        [Out, Optional] ULONG* pceltFetched);
    abstract HRESULT Skip([In] ULONG celt);
    abstract HRESULT Reset();
    abstract HRESULT Clone([Out, MarshalAs(UnmanagedType.IUnknown)] out IEnumMoniker ppenum);
}
