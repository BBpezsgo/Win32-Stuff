using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    unsafe public struct StartupInfo
    {
        public DWORD cb;
        public WCHAR* lpReserved;
        public WCHAR* lpDesktop;
        public WCHAR* lpTitle;
        public DWORD dwX;
        public DWORD dwY;
        public DWORD dwXSize;
        public DWORD dwYSize;
        public DWORD dwXCountChars;
        public DWORD dwYCountChars;
        public DWORD dwFillAttribute;
        public DWORD dwFlags;
        public WORD wShowWindow;
        public WORD cbReserved2;
        public BYTE* lpReserved2;
        public HANDLE hStdInput;
        public HANDLE hStdOutput;
        public HANDLE hStdError;
    }
}
