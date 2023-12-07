using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessEntry
    {
        public DWORD StructSize;
        public DWORD Usage;
        public DWORD ProcessID;          // this process
        public ULONG_PTR DefaultHeapID;
        public DWORD ModuleID;           // associated exe
        public DWORD ThreadCount;
        public DWORD ParentProcessID;    // this process's parent process
        public LONG PriClassBase;          // Base priority of process's threads
        public DWORD Flags;
        unsafe public fixed WCHAR ExeFile[260];
    }
}
