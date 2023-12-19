global using STARTUPINFOW = Win32.StartupInfo;

using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct StartupInfo
    {
        public DWORD StructSize;

        readonly unsafe WCHAR* Reserved;

        public unsafe WCHAR* Desktop;
        public unsafe WCHAR* Title;
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
        readonly unsafe BYTE* Reserved3;

        public HANDLE StdInput;
        public HANDLE StdOutput;
        public HANDLE StdError;
    }
}
