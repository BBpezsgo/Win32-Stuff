using System.Runtime.InteropServices;

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

namespace Win32.D2D1.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static class D2D1
    {
        /// <summary>
        /// Creates a factory object that can be used to create Direct2D resources.
        /// </summary>
        [DllImport("D2d1.dll", SetLastError = true)]
        unsafe public static extern HRESULT D2D1CreateFactory(
            [In] D2D1_FACTORY_TYPE factoryType,
            [In] REFIID riid,
            [In, Optional] D2D1_FACTORY_OPTIONS* pFactoryOptions,
            [Out] void** ppIFactory
        );

        /// <summary>
        /// Creates a factory object that can be used to create Direct2D resources.
        /// </summary>
        [DllImport("D2d1.dll", SetLastError = true)]
        unsafe public static extern HRESULT D2D1CreateFactory(
            [In] D2D1_FACTORY_TYPE factoryType,
            [In] REFIID riid,
            [In, Optional] D2D1_FACTORY_OPTIONS* pFactoryOptions,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object? ppIFactory
        );

        /// <summary>
        /// Creates a factory object that can be used to create Direct2D resources.
        /// </summary>
        unsafe public static HRESULT D2D1CreateFactory<T>(
             D2D1_FACTORY_TYPE factoryType,
             REFIID riid,
             D2D1_FACTORY_OPTIONS* pFactoryOptions,
             out T? ppIFactory
        )
        {
            HRESULT result = D2D1.D2D1CreateFactory(factoryType, riid, pFactoryOptions, out object? ppIFactory_);
            ppIFactory = (T?)ppIFactory_;
            return result;
        }
    }
}
