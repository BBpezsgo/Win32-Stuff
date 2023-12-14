global using STARTUPINFOW = Win32.StartupInfo;

using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    unsafe public struct StartupInfo
    {
        public DWORD StructSize;

        readonly WCHAR* Reserved;

        public WCHAR* Desktop;
        public WCHAR* Title;
        public DWORD dwX;
        public DWORD dwY;
        public DWORD dwXSize;
        public DWORD dwYSize;
        public DWORD dwXCountChars;
        public DWORD dwYCountChars;
        public DWORD FillAttribute;
        public DWORD Flags;
        public WORD ShowWindow;

        readonly WORD Reserved2;
        readonly BYTE* Reserved3;

        public HANDLE StdInput;
        public HANDLE StdOutput;
        public HANDLE StdError;
    }
}
