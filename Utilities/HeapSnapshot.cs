using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace Win32
{
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
            if (Kernel32.INVALID_HANDLE_VALUE == hSnapshot)
            { throw WindowsException.Get(); }
            return new HeapSnapshot(hSnapshot);
        }
    }

    [SupportedOSPlatform("windows")]
    public class HeapSnapshotEnumerator : IEnumerator<HeapList>
    {
        readonly HANDLE Handle;
        HeapList current;
        bool IsDisposed;
        bool IsStarted;

        public HeapSnapshotEnumerator(HANDLE handle)
        {
            Handle = handle;
        }

        public HeapList Current => current;
        object IEnumerator.Current => current;

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe void Reset()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);

            IsStarted = true;

            HeapList heapList = HeapList.Create();

            int result = Kernel32.Heap32ListFirst(Handle, &heapList);

            if (result != TRUE)
            {
                DWORD error = Kernel32.GetLastError();
                if (error == 0x12) // ERROR_NO_MORE_FILES
                {
                    current = default;
                    return;
                }
                Dispose();
                throw WindowsException.Get(error);
            }

            current = heapList;
        }

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe bool MoveNext()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);

            HeapList heapList = HeapList.Create();

            if (!IsStarted)
            {
                IsStarted = true;

                int result2 = Kernel32.Heap32ListFirst(Handle, &heapList);

                if (result2 != TRUE)
                {
                    DWORD error = Kernel32.GetLastError();
                    if (error == 0x12) // ERROR_NO_MORE_FILES
                    {
                        current = default;
                        return false;
                    }
                    Dispose();
                    throw WindowsException.Get(error);
                }

                current = heapList;
            }

            int result = Kernel32.Heap32ListNext(Handle, &heapList);
            if (result != TRUE) return false;
            current = heapList;
            return true;
        }

        /// <exception cref="WindowsException"/>
        void ActualDispose()
        {
            if (IsDisposed) return;

            if (Kernel32.CloseHandle(Handle) == FALSE)
            { throw WindowsException.Get(); }

            IsDisposed = true;
        }
        /// <exception cref="WindowsException"/>
        ~HeapSnapshotEnumerator() { ActualDispose(); }
        /// <exception cref="WindowsException"/>
        public void Dispose()
        {
            ActualDispose();
            GC.SuppressFinalize(this);
        }
    }
}
