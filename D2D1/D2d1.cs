namespace Win32.D2D1;

/// <summary>
/// Direct2D
/// </summary>
[SupportedOSPlatform("windows")]
public static partial class D2D1
{
    /// <summary>
    /// Creates a factory object that can be used to create Direct2D resources.
    /// </summary>
    [LibraryImport("D2d1.dll", SetLastError = true)]
    public static unsafe partial HRESULT D2D1CreateFactory(
        FactoryType factoryType,
        REFIID riid,
        [Optional] FactoryOptions* pFactoryOptions,
        out void* ppIFactory
    );

    /// <summary>
    /// Creates a factory object that can be used to create Direct2D resources.
    /// </summary>
    [DllImport("D2d1.dll", SetLastError = true)]
    public static extern unsafe HRESULT D2D1CreateFactory(
        [In] FactoryType factoryType,
        [In] REFIID riid,
        [In, Optional] FactoryOptions* pFactoryOptions,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object? ppIFactory
    );

    /// <summary>
    /// Creates a factory object that can be used to create Direct2D resources.
    /// </summary>
    [RequiresUnreferencedCode("COM interop")]
    public static unsafe HRESULT D2D1CreateFactory<T>(
         FactoryType factoryType,
         REFIID riid,
         FactoryOptions* pFactoryOptions,
         out T? ppIFactory
    )
    {
        HRESULT result = D2D1.D2D1CreateFactory(factoryType, riid, pFactoryOptions, out object? ppIFactory_);
        ppIFactory = (T?)ppIFactory_;
        return result;
    }
}
