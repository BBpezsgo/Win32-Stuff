using System.Runtime.InteropServices;

namespace Win32.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3d3e0379-f9de-4d58-bb6c-18d62992f1a6")]
    public interface IDXGIDeviceSubObject: IDXGIObject
    {
    }
}
