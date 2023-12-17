using System.Runtime.InteropServices;

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

namespace Win32.COM.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static class Shell32
    {
        [DllImport("Shell32.dll", SetLastError = true)]
        unsafe public static extern WCHAR* CommandLineToArgvW(
          [In] WCHAR* lpCmdLine,
          [Out] int* pNumArgs
        );

        [DllImport("Shell32.dll", SetLastError = true)]
        unsafe public static extern HRESULT SHCreateItemFromParsingName(
          [In] WCHAR* pszPath,
          [In, Optional, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
          [In] REFIID riid,
          [Out] void** ppv
        );

        [DllImport("Shell32.dll", SetLastError = true)]
        unsafe static extern HRESULT SHCreateItemFromParsingName(
          [In] WCHAR* pszPath,
          [In, Optional, MarshalAs(UnmanagedType.IUnknown)] IBindCtx pbc,
          [In] REFIID riid,
          [Out, MarshalAs(UnmanagedType.IUnknown)] out object? ppv
        );

        [DllImport("Shell32.dll", SetLastError = true)]
        unsafe static extern HRESULT SHCreateItemFromParsingName(
          [In] WCHAR* pszPath,
          [In, Optional] void** pbc,
          [In] REFIID riid,
          [Out, MarshalAs(UnmanagedType.IUnknown)] out object? ppv
        );

        unsafe public static HRESULT SHCreateItemFromParsingName<T>(
           WCHAR* pszPath,
           IBindCtx? pbc,
           out T? ppv
        )
        {
            HRESULT result;
            object? _ppv;
            if (pbc is null)
            { result = Shell32.SHCreateItemFromParsingName(pszPath, (void**)null, typeof(T).GUID, out _ppv); }
            else
            { result = Shell32.SHCreateItemFromParsingName(pszPath, pbc, typeof(T).GUID, out _ppv); }
            ppv = (T?)_ppv;
            return result;
        }
    }
}
