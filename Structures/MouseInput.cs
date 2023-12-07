using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        public LONG dx;
        public LONG dy;
        public DWORD MouseData;
        public DWORD Flags;
        public DWORD Time;
        public ULONG_PTR ExtraInfo;
    }
}
