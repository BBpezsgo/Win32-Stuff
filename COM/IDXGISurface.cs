namespace Win32.COM;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("cafcb56c-6ac3-4889-bf47-9e23bbd260ec")]
[SupportedOSPlatform("windows")]
public interface IDXGISurface : IDXGIDeviceSubObject;
