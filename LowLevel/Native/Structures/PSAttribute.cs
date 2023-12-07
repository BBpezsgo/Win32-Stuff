using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PSAttribute
    {
        public ULONG_PTR Attribute;
        public SIZE_T Size;
        unsafe public void* ValuePtr;
        unsafe public SIZE_T* ReturnLength;
    }
}
