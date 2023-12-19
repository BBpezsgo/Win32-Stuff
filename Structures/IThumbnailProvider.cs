using System.Runtime.InteropServices;

namespace Win32.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("e357fccd-a995-4576-b01f-234630154e96")]
    [SupportedOSPlatform("windows")]
    public interface IThumbnailProvider
    {
        abstract unsafe HRESULT GetThumbnail(
            [In] UINT cx,
            [Out] HBITMAP* phbmp,
            [Out] WTS_ALPHATYPE* pdwAlpha);
    }
}
