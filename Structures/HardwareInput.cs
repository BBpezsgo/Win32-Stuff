using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HardwareInput
    {
        public DWORD Msg;
        public WORD ParamL;
        public WORD ParamH;
    }
}
