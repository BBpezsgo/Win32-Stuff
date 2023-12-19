using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SecurityAttributes
    {
        public DWORD Length;
        public unsafe void* SecurityDescriptor;
        public BOOL InheritHandle;
    }
}
