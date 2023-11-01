using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct SECURITY_ATTRIBUTES
    {
        public DWORD nLength;
        public void* lpSecurityDescriptor;
        public BOOL bInheritHandle;
    }
}
