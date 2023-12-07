using System.Runtime.InteropServices;

namespace Win32.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000010c-0000-0000-C000-000000000046")]
    public interface IPersist
    {
        unsafe abstract HRESULT GetClassID([Out] Guid* pClassID);
    }
}
