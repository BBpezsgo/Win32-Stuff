global using HTHEME = nint;

namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public static partial class UxTheme
{
    [LibraryImport("UxTheme.dll", SetLastError = true)]
    public static unsafe partial HRESULT SetWindowTheme(HWND window, WCHAR* subAppName, WCHAR* subIdList);

    public static unsafe HRESULT SetWindowTheme(HWND window, string? subAppName, string? subIdList)
    {
        fixed (WCHAR* subAppNamePtr = subAppName)
        fixed (WCHAR* subIdListPtr = subIdList)
        { return SetWindowTheme(window, subAppNamePtr, subIdListPtr); }
    }

    [LibraryImport("UxTheme.dll", SetLastError = true)]
    public static partial HTHEME GetWindowTheme(HWND window);

    [LibraryImport("UxTheme.dll", SetLastError = true)]
    public static unsafe partial HTHEME OpenThemeData(HWND window, WCHAR* classList);

    public static unsafe HRESULT SetTheme(this Window window, string? subAppName, string? subIdList)
    {
        fixed (WCHAR* pszSubAppNamePtr = subAppName)
        fixed (WCHAR* pszSubIdListPtr = subIdList)
        { return SetWindowTheme(window, pszSubAppNamePtr, pszSubIdListPtr); }
    }

    public static unsafe HRESULT ClearTheme(this Window window)
    {
        fixed (WCHAR* pszSubAppNamePtr = "")
        fixed (WCHAR* pszSubIdListPtr = "")
        { return SetWindowTheme(window, pszSubAppNamePtr, pszSubIdListPtr); }
    }
}
