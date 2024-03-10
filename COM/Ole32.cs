namespace Win32.COM;

[SupportedOSPlatform("windows")]
public static partial class Ole32
{
    [LibraryImport("Ole32.dll", SetLastError = true)]
    public static partial void CoUninitialize();

    [LibraryImport("Ole32.dll", SetLastError = true)]
    public static unsafe partial HRESULT CoInitializeEx(
      [Optional] void* pvReserved,
       COMInit dwCoInit
    );

    [LibraryImport("Ole32.dll", SetLastError = true)]
    public static unsafe partial HRESULT CoCreateInstance(
      REFCLSID rclsid,
      [Optional] void** pUnkOuter,
      DWORD dwClsContext,
      REFIID riid,
      out void* ppv
    );

    [DllImport("Ole32.dll", SetLastError = true)]
    public static extern unsafe HRESULT CoCreateInstance(
      [In] REFCLSID rclsid,
      [In, Optional, MarshalAs(UnmanagedType.IUnknown)] in object? pUnkOuter,
      [In] DWORD dwClsContext,
      [In] REFIID riid,
      [Out, MarshalAs(UnmanagedType.Interface)] out object? ppv
    );

    [RequiresUnreferencedCode("COM interop")]
    public static unsafe HRESULT CoCreateInstance<T>(
      REFCLSID rclsid,
      in object? pUnkOuter,
      DWORD dwClsContext,
      REFIID riid,
      out T? ppv
    )
    {
        HRESULT result = Ole32.CoCreateInstance(rclsid, in pUnkOuter, dwClsContext, riid, out object? ppv_);
        ppv = ppv_ is null ? default : (T?)ppv_;
        return result;
    }
}
