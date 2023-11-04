using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Explicit)]
    public struct DUMMYUNION
    {
        [FieldOffset(0)]
        public DUMMYSTRUCTNAME DUMMYSTRUC;
        [FieldOffset(0)]
        public IntPtr Pointer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DUMMYSTRUCTNAME
    {
        public DWORD Offset;
        public DWORD OffsetHigh;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OVERLAPPED
    {
        public ULONG_PTR Internal;
        public ULONG_PTR InternalHigh;
        public DUMMYUNION DUMMYUNIONNAME;
        public HANDLE hEvent;
    }
}
