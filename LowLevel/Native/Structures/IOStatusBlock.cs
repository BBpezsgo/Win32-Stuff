using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Explicit)]
    public struct IOStatusBlock
    {
        [FieldOffset(0)]
        public NTSTATUS Status;
        [FieldOffset(0)]
        unsafe public void* Pointer;
        [FieldOffset(4)]
        public ULONG_PTR Information;
    }
}
