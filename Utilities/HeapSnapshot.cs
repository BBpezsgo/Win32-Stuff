using System.Collections;
using System.Globalization;

namespace Win32;

[SupportedOSPlatform("windows")]
public readonly struct HeapSnapshot :
    IEnumerable<HeapList>,
    IDisposable,
    IEquatable<HeapSnapshot>,
    System.Numerics.IEqualityOperators<HeapSnapshot, HeapSnapshot, bool>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly HANDLE Handle;

    HeapSnapshot(HANDLE handle) => Handle = handle;

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Kernel32.CloseHandle(Handle) == FALSE)
        { throw WindowsException.Get(); }
    }

    public readonly IEnumerator<HeapList> GetEnumerator() => new HeapSnapshotEnumerator(Handle);
    readonly IEnumerator IEnumerable.GetEnumerator() => new HeapSnapshotEnumerator(Handle);

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
    public override bool Equals(object? obj) => obj is HeapList snapshot && Equals(snapshot);
    public bool Equals(HeapSnapshot other) => Handle.Equals(other.Handle);
    public override int GetHashCode() => Handle.GetHashCode();

    public static bool operator ==(HeapSnapshot left, HeapSnapshot right) => left.Equals(right);
    public static bool operator !=(HeapSnapshot left, HeapSnapshot right) => !left.Equals(right);

    /// <exception cref="WindowsException"/>
    public static HeapSnapshot CreateSnapshot(uint processId)
    {
        HANDLE hSnapshot = Kernel32.CreateToolhelp32Snapshot(TH32CS.SNAPHEAPLIST, processId);
        if (Kernel32.InvalidHandle == hSnapshot)
        { throw WindowsException.Get(); }
        return new HeapSnapshot(hSnapshot);
    }

    [SupportedOSPlatform("windows")]
    public struct HeapSnapshotEnumerator : IEnumerator<HeapList>
    {
        HANDLE _handle;
        HeapList _current;
        bool _isStarted;

        readonly HeapList IEnumerator<HeapList>.Current => _current;
        readonly object IEnumerator.Current => _current;

        public HeapSnapshotEnumerator(HANDLE handle)
        {
            _handle = handle;
        }

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe void Reset()
        {
            ObjectDisposedException.ThrowIf(_handle == 0, this);

            _isStarted = true;

            HeapList heapList = HeapList.Create();

            int result = Kernel32.Heap32ListFirst(_handle, ref heapList);

            if (result != TRUE)
            {
                DWORD error = Kernel32.GetLastError();
                if (error == 0x12) // ERROR_NO_MORE_FILES
                {
                    _current = default;
                    return;
                }
                Dispose();
                throw WindowsException.Get(error);
            }

            _current = heapList;
        }

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe bool MoveNext()
        {
            ObjectDisposedException.ThrowIf(_handle == 0, this);

            HeapList heapList = HeapList.Create();

            if (!_isStarted)
            {
                _isStarted = true;

                int result2 = Kernel32.Heap32ListFirst(_handle, ref heapList);

                if (result2 != TRUE)
                {
                    DWORD error = Kernel32.GetLastError();
                    if (error == 0x12) // ERROR_NO_MORE_FILES
                    {
                        _current = default;
                        return false;
                    }
                    Dispose();
                    throw WindowsException.Get(error);
                }

                _current = heapList;
            }

            int result = Kernel32.Heap32ListNext(_handle, out heapList);
            if (result != TRUE) return false;
            _current = heapList;
            return true;
        }

        /// <exception cref="WindowsException"/>
        public void Dispose()
        {
            if (Kernel32.CloseHandle(_handle) == FALSE)
            { throw WindowsException.Get(); }
            _handle = 0;
        }
    }
}
