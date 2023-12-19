using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct UnicodeString
    {
        public ushort Length;
        public ushort MaximumLength;
        public unsafe WCHAR* Buffer;
    }
}
