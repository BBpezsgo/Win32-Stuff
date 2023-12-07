using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct ConsoleFontInfo
    {
        public DWORD FontIndex;
        public COORD FontSize;
    }
}
