using System.Runtime.InteropServices;

namespace Win32.COM
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BindOptions
    {
        readonly DWORD StructSize;

        public DWORD Flags;
        public DWORD Mode;
        public DWORD TickCountDeadline;

        BindOptions(uint cbStruct) : this() => this.StructSize = cbStruct;

        unsafe public static BindOptions Create() => new((uint)sizeof(BindOptions));
    }
}
