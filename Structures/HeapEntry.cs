using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [Flags]
    public enum LF32 : DWORD
    {
        FIXED = 0x00000001,
        FREE = 0x00000002,
        MOVEABLE = 0x00000004,
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct HeapEntry
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly SIZE_T StructSize;
        public readonly HANDLE Handle;     // Handle of this heap block
        public readonly ULONG_PTR Address;   // Linear address of start of block
        public readonly SIZE_T BlockSize; // Size of block in bytes
        public readonly LF32 Flags;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly DWORD LockCount;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly DWORD Reserved;
        public readonly DWORD ProcessId;   // owning process
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly ULONG_PTR HeapId;      // heap block is in

        HeapEntry(SIZE_T structSize) : this() => this.StructSize = structSize;

        public static unsafe HeapEntry Create() => new((SIZE_T)sizeof(HeapEntry));
    }
}
