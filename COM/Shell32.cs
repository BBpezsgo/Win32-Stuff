namespace Win32.COM;

[SupportedOSPlatform("windows")]
public static class Shell32
{
    [DllImport("Shell32.dll", SetLastError = true)]
    public static extern unsafe WCHAR* CommandLineToArgvW(
      WCHAR* lpCmdLine,
      out int pNumArgs
    );

    [DllImport("Shell32.dll", SetLastError = true)]
    public static extern unsafe HRESULT SHCreateItemFromParsingName(
      [In] WCHAR* pszPath,
      [In, Optional, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
      [In] REFIID riid,
      [Out] void** ppv
    );

    [DllImport("Shell32.dll", SetLastError = true)]
    public static extern unsafe HRESULT SHCreateItemFromParsingName(
      [In] WCHAR* pszPath,
      [In, Optional, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
      [In] REFIID riid,
      [Out, MarshalAs(UnmanagedType.IUnknown)] out object? ppv
    );

    [DllImport("Shell32.dll", SetLastError = true)]
    public static extern unsafe HRESULT SHCreateItemFromParsingName(
      [In] WCHAR* pszPath,
      [In, Optional] void** pbc,
      [In] REFIID riid,
      [Out, MarshalAs(UnmanagedType.IUnknown)] out object? ppv
    );

    [RequiresUnreferencedCode("COM interop")]
    public static unsafe HRESULT SHCreateItemFromParsingName<T>(
       WCHAR* pszPath,
       IBindCtx? pbc,
       out T? ppv
    )
    {
        HRESULT result;
        object? _ppv;
        result = pbc is null
            ? Shell32.SHCreateItemFromParsingName(pszPath, (void**)null, typeof(T).GUID, out _ppv)
            : Shell32.SHCreateItemFromParsingName(pszPath, pbc, typeof(T).GUID, out _ppv);
        ppv = (T?)_ppv;
        return result;
    }
}
