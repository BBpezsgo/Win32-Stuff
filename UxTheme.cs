using System.Runtime.InteropServices;

namespace Win32
{
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
