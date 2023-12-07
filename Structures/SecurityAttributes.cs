using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct SecurityAttributes
    {
        public DWORD Length;
        public void* SecurityDescriptor;
        public BOOL InheritHandle;
    }
}
