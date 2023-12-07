using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ClientId
    {
        public HANDLE UniqueProcess;
        public HANDLE UniqueThread;
    }
}
