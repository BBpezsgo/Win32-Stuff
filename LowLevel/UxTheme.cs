using System.Runtime.InteropServices;

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

namespace Win32.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static  class UxTheme
    {
        [DllImport("UxTheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern HRESULT SetWindowTheme(
          [In] HWND hwnd,
          [In] WCHAR* pszSubAppName,
          [In] WCHAR* pszSubIdList);

        unsafe public static HRESULT SetWindowTheme(
          [In] HWND hwnd,
          [In] string? pszSubAppName,
          [In] string? pszSubIdList)
        {
            fixed(WCHAR* pszSubAppNamePtr = pszSubAppName)
            fixed(WCHAR* pszSubIdListPtr = pszSubIdList)
            {
                return SetWindowTheme(hwnd, pszSubAppNamePtr, pszSubIdListPtr);
            }
        }
    }
}
