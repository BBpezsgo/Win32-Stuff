namespace Win32.COM;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("0000010c-0000-0000-C000-000000000046")]
[SupportedOSPlatform("windows")]
public interface IPersist
{
    abstract unsafe HRESULT GetClassID([Out] Guid* pClassID);
}
