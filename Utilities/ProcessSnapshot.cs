using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    public readonly struct ProcessSnapshot :
        IEnumerable<ProcessEntry>,
        IDisposable,
        IEquatable<ProcessSnapshot>,
        System.Numerics.IEqualityOperators<ProcessSnapshot, ProcessSnapshot, bool>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HANDLE Handle;

        public ProcessSnapshot(HANDLE handle) => Handle = handle;

        public void Dispose() => _ = Kernel32.CloseHandle(Handle);

        public readonly IEnumerator<ProcessEntry> GetEnumerator() => new ProcessSnapshotEnumerator(Handle);
        readonly IEnumerator IEnumerable.GetEnumerator() => new ProcessSnapshotEnumerator(Handle);

        public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        public override bool Equals(object? obj) => obj is ProcessSnapshot snapshot && Equals(snapshot);
        public bool Equals(ProcessSnapshot other) => Handle == other.Handle;
        public override int GetHashCode() => Handle.GetHashCode();

        public static bool operator ==(ProcessSnapshot left, ProcessSnapshot right) => left.Equals(right);
        public static bool operator !=(ProcessSnapshot left, ProcessSnapshot right) => !left.Equals(right);
    }

    [SupportedOSPlatform("windows")]
    public class ProcessSnapshotEnumerator : IEnumerator<ProcessEntry>
    {
        readonly HANDLE Handle;
        ProcessEntry current;
        bool IsDisposed;
        bool IsStarted;

        public ProcessSnapshotEnumerator(HANDLE handle)
        {
            Handle = handle;
        }

        public ProcessEntry Current => current;
        object IEnumerator.Current => current;

        /// <exception cref="ObjectDisposedException"/>
        unsafe public void Reset()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(ProcessSnapshotEnumerator));
            IsStarted = true;

            ProcessEntry processEntry = ProcessEntry.Create();

            int result = Kernel32.Process32FirstW(Handle, &processEntry);

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

            current = processEntry;
        }

        /// <exception cref="ObjectDisposedException"/>
        unsafe public bool MoveNext()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(ProcessSnapshotEnumerator));

            ProcessEntry processEntry = ProcessEntry.Create();

            if (!IsStarted)
            {
                IsStarted = true;

                int result2 = Kernel32.Process32FirstW(Handle, &processEntry);

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

                current = processEntry;
            }

            int result = Kernel32.Process32NextW(Handle, &processEntry);
            if (result != TRUE) return false;
            current = processEntry;
            return true;
        }

        void ActualDispose()
        {
            if (IsDisposed) return;
            _ = Kernel32.CloseHandle(Handle);
            IsDisposed = true;
        }
        ~ProcessSnapshotEnumerator() { ActualDispose(); }
        public void Dispose()
        {
            ActualDispose();
            GC.SuppressFinalize(this);
        }
    }
}
