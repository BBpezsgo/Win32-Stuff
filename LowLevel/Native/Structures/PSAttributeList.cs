using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PSAttributeList
    {
        public SIZE_T TotalLength;
        public PSAttribute Attributes;
    }
}
