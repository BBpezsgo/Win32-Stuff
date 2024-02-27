using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static class UxTheme
    {
        [DllImport("UxTheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe HRESULT SetWindowTheme(
          [In] HWND hwnd,
          [In] WCHAR* pszSubAppName,
          [In] WCHAR* pszSubIdList);

        public static unsafe HRESULT SetWindowTheme(
          [In] HWND hwnd,
          [In] string? pszSubAppName,
          [In] string? pszSubIdList)
        {
            fixed (WCHAR* pszSubAppNamePtr = pszSubAppName)
            fixed (WCHAR* pszSubIdListPtr = pszSubIdList)
            {
                return SetWindowTheme(hwnd, pszSubAppNamePtr, pszSubIdListPtr);
            }
        }
    }
}
