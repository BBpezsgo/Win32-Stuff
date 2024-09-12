using System.Text;

namespace Win32.MCI;

public static class MCI
{
    [DllImport("Winmm.dll", CharSet = CharSet.Unicode)]
    public static extern unsafe MCIERROR mciSendStringW(
       WCHAR* lpszCommand,
       WCHAR* lpszReturnString,
       UINT cchReturn,
       HANDLE hwndCallback
    );

    public static unsafe MCIERROR mciSendStringW(
       string lpszCommand,
       StringBuilder lpszReturnString,
       UINT cchReturn,
       HANDLE hwndCallback
    )
    {
        fixed (WCHAR* lpszCommandPtr = lpszCommand)
        fixed (WCHAR* lpszReturnStringPtr = lpszReturnString.ToString())
        {
            MCIERROR error = MCI.mciSendStringW(lpszCommandPtr, lpszReturnStringPtr, cchReturn, hwndCallback);
            lpszReturnString.Clear();
            lpszReturnString.Append(new string(lpszReturnStringPtr));
            return error;
        }
    }

    [DllImport("Winmm.dll", CharSet = CharSet.Unicode)]
    public static extern MCIERROR mciSendCommandW(
       MCIDEVICEID IDDevice,
       UINT uMsg,
       DWORD_PTR fdwCommand,
       DWORD_PTR dwParam);
}
