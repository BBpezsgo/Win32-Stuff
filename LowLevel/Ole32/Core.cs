using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Win32.COM.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static class Ole32
    {
        [DllImport("Ole32.dll", SetLastError = true)]
        public static extern void CoUninitialize();

        [DllImport("Ole32.dll", SetLastError = true)]
        public static extern unsafe HRESULT CoInitializeEx(
          [In, Optional] void* pvReserved,
          [In] COMInit dwCoInit
        );

        [DllImport("Ole32.dll", SetLastError = true)]
        public static extern unsafe HRESULT CoCreateInstance(
          [In] REFCLSID rclsid,
          [In, Optional] void** pUnkOuter,
          [In] DWORD dwClsContext,
          [In] REFIID riid,
          [Out] void** ppv
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
}
