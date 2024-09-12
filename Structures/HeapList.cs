using System.Collections;

namespace Win32;

public enum HF32 : DWORD
{
    DEFAULT = 1,
    SHARED = 2,
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct HeapList : IEnumerable<HeapEntry>, IEquatable<HeapList>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly SIZE_T StructSize;
    /// <summary>
    /// The identifier of the process to be examined.
    /// </summary>
    public readonly DWORD ProcessId;   // owning process
    /// <summary>
    /// The heap identifier. This is not a handle, and has meaning only to the tool help functions.
    /// </summary>
    readonly ULONG_PTR HeapId;      // heap (in owning process's context!)
    public readonly HF32 Flags;

    HeapList(SIZE_T structSize) : this() => this.StructSize = structSize;

    public static unsafe HeapList Create() => new((SIZE_T)sizeof(HeapList));

    [SupportedOSPlatform("windows")]
    public IEnumerator<HeapEntry> GetEnumerator() => new HeapListEnumerator(ProcessId, HeapId);
    [SupportedOSPlatform("windows")]
    IEnumerator IEnumerable.GetEnumerator() => new HeapListEnumerator(ProcessId, HeapId);

    public override bool Equals(object? obj) => obj is HeapList list && Equals(list);
    public bool Equals(HeapList other) =>
        ProcessId == other.ProcessId &&
        HeapId == other.HeapId;
    public override int GetHashCode() => HashCode.Combine(ProcessId, HeapId);

    public static bool operator ==(HeapList left, HeapList right) => left.Equals(right);
    public static bool operator !=(HeapList left, HeapList right) => !(left == right);
}

[SupportedOSPlatform("windows")]
public struct HeapListEnumerator : IEnumerator<HeapEntry>
{
    readonly uint ProcessId;
    readonly nuint HeapId;
    bool IsStarted;

    public HeapEntry Current { get; private set; }
    readonly object IEnumerator.Current => Current;

    public HeapListEnumerator(uint processId, nuint heapId)
    {
        ProcessId = processId;
        HeapId = heapId;
    }

    public unsafe void Reset()
    {
        IsStarted = true;

        HeapEntry heapEntry = HeapEntry.Create();

        int result = Kernel32.Heap32First(ref heapEntry, ProcessId, HeapId);

        if (result != TRUE)
        {
            DWORD error = Kernel32.GetLastError();
            if (error == 0x12) // ERROR_NO_MORE_FILES
            {
                Current = default;
                return;
            }
            Dispose();
            throw WindowsException.Get(error);
        }

        Current = heapEntry;
    }

    public unsafe bool MoveNext()
    {
        HeapEntry heapEntry = HeapEntry.Create();

        if (!IsStarted)
        {
            IsStarted = true;

            int result2 = Kernel32.Heap32First(ref heapEntry, ProcessId, HeapId);

            if (result2 != TRUE)
            {
                DWORD error = Kernel32.GetLastError();
                if (error == 0x12) // ERROR_NO_MORE_FILES
                {
                    Current = default;
                    return false;
                }
                Dispose();
                throw WindowsException.Get(error);
            }

            Current = heapEntry;
        }

        int result = Kernel32.Heap32Next(out heapEntry);
        if (result != TRUE) return false;
        Current = heapEntry;
        return true;
    }

    public readonly void Dispose() { }
}
