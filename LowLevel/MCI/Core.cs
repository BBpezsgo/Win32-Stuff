using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    public static class MCI
    {
        [DllImport("Winmm.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe MCIERROR mciSendStringW(
           WCHAR* lpszCommand,
           WCHAR* lpszReturnString,
           UINT cchReturn,
           HANDLE hwndCallback
        );

        [DllImport("Winmm.dll", CharSet = CharSet.Unicode)]
        public static extern MCIERROR mciSendCommandW(
           MCIDEVICEID IDDevice,
           UINT uMsg,
           DWORD_PTR fdwCommand,
           DWORD_PTR dwParam);
    }
}
