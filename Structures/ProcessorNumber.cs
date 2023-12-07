using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessorNumber
    {
        public WORD Group;
        public BYTE Number;
        readonly BYTE Reserved;
    }
}
