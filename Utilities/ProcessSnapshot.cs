using System.Collections;
using System.Globalization;

namespace Win32;

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

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Kernel32.CloseHandle(Handle) == FALSE)
        { throw WindowsException.Get(); }
    }

    public readonly IEnumerator<ProcessEntry> GetEnumerator() => new ProcessSnapshotEnumerator(Handle);
    readonly IEnumerator IEnumerable.GetEnumerator() => new ProcessSnapshotEnumerator(Handle);

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
    public override bool Equals(object? obj) => obj is ProcessSnapshot snapshot && Equals(snapshot);
    public bool Equals(ProcessSnapshot other) => Handle == other.Handle;
    public override int GetHashCode() => Handle.GetHashCode();

    public static bool operator ==(ProcessSnapshot left, ProcessSnapshot right) => left.Equals(right);
    public static bool operator !=(ProcessSnapshot left, ProcessSnapshot right) => !left.Equals(right);

    [SupportedOSPlatform("windows")]
    public struct ProcessSnapshotEnumerator : IEnumerator<ProcessEntry>
    {
        HANDLE _handle;
        ProcessEntry _current;
        bool _isStarted;

        public ProcessSnapshotEnumerator(HANDLE handle)
        {
            _handle = handle;
        }

        public readonly ProcessEntry Current => _current;
        readonly object IEnumerator.Current => _current;

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe void Reset()
        {
            ObjectDisposedException.ThrowIf(_handle == 0, this);

            _isStarted = true;

            ProcessEntry processEntry = ProcessEntry.Create();

            int result = Kernel32.Process32FirstW(_handle, ref processEntry);

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

            _current = processEntry;
        }

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe bool MoveNext()
        {
            ObjectDisposedException.ThrowIf(_handle == 0, this);

            ProcessEntry processEntry = ProcessEntry.Create();

            if (!_isStarted)
            {
                _isStarted = true;

                int result2 = Kernel32.Process32FirstW(_handle, ref processEntry);

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

                _current = processEntry;
            }

            int result = Kernel32.Process32NextW(_handle, out processEntry);
            if (result != TRUE) return false;
            _current = processEntry;
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
