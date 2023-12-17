using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    public readonly struct ThreadSnapshot :
        IEnumerable<ThreadEntry>,
        IDisposable,
        IEquatable<ThreadSnapshot>,
        System.Numerics.IEqualityOperators<ThreadSnapshot, ThreadSnapshot, bool>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HANDLE Handle;

        public ThreadSnapshot(HANDLE handle) => Handle = handle;

        public void Dispose() => _ = Kernel32.CloseHandle(Handle);

        public readonly IEnumerator<ThreadEntry> GetEnumerator() => new ThreadSnapshotEnumerator(Handle);
        readonly IEnumerator IEnumerable.GetEnumerator() => new ThreadSnapshotEnumerator(Handle);

        public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        public override bool Equals(object? obj) => obj is ThreadSnapshot snapshot && Equals(snapshot);
        public bool Equals(ThreadSnapshot other) => Handle.Equals(other.Handle);
        public override int GetHashCode() => Handle.GetHashCode();

        public static bool operator ==(ThreadSnapshot left, ThreadSnapshot right) => left.Equals(right);
        public static bool operator !=(ThreadSnapshot left, ThreadSnapshot right) => !left.Equals(right);

        public static ThreadSnapshot CreateSnapshot()
        {
            HANDLE hSnapshot = Kernel32.CreateToolhelp32Snapshot(TH32CS.SNAPTHREAD, default);
            if (Kernel32.INVALID_HANDLE_VALUE == hSnapshot)
            { throw WindowsException.Get(); }
            return new ThreadSnapshot(hSnapshot);
        }
    }

    [SupportedOSPlatform("windows")]
    public class ThreadSnapshotEnumerator : IEnumerator<ThreadEntry>
    {
        readonly HANDLE Handle;
        ThreadEntry current;
        bool IsDisposed;
        bool IsStarted;

        public ThreadSnapshotEnumerator(HANDLE handle)
        {
            Handle = handle;
        }

        public ThreadEntry Current => current;
        object IEnumerator.Current => current;

        /// <exception cref="ObjectDisposedException"/>
        unsafe public void Reset()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(ThreadSnapshotEnumerator));
            IsStarted = true;

            ThreadEntry threadEntry = ThreadEntry.Create();

            int result = Kernel32.Thread32First(Handle, &threadEntry);

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

            current = threadEntry;
        }

        /// <exception cref="ObjectDisposedException"/>
        unsafe public bool MoveNext()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(ThreadSnapshotEnumerator));

            ThreadEntry threadEntry = ThreadEntry.Create();

            if (!IsStarted)
            {
                IsStarted = true;

                int result2 = Kernel32.Thread32First(Handle, &threadEntry);

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

                current = threadEntry;
            }

            int result = Kernel32.Thread32Next(Handle, &threadEntry);
            if (result != TRUE) return false;
            current = threadEntry;
            return true;
        }

        void ActualDispose()
        {
            if (IsDisposed) return;
            _ = Kernel32.CloseHandle(Handle);
            IsDisposed = true;
        }
        ~ThreadSnapshotEnumerator() { ActualDispose(); }
        public void Dispose()
        {
            ActualDispose();
            GC.SuppressFinalize(this);
        }
    }
}
