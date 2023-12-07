using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct ModuleInfo
    {
        public void* BaseOfDll;
        public DWORD SizeOfImage;
        public void* EntryPoint;
    }
}
