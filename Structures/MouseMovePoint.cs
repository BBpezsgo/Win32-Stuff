using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseMovePoint
    {
        public int X;
        public int Y;
        public DWORD Time;
        public ULONG_PTR ExtraInfo;
    }
}
