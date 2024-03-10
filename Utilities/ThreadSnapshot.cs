using System.Collections;
using System.Globalization;

namespace Win32;

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

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Kernel32.CloseHandle(Handle) == FALSE)
        { throw WindowsException.Get(); }
    }

    public readonly IEnumerator<ThreadEntry> GetEnumerator() => new ThreadSnapshotEnumerator(Handle);
    readonly IEnumerator IEnumerable.GetEnumerator() => new ThreadSnapshotEnumerator(Handle);

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
    public override bool Equals(object? obj) => obj is ThreadSnapshot snapshot && Equals(snapshot);
    public bool Equals(ThreadSnapshot other) => Handle.Equals(other.Handle);
    public override int GetHashCode() => Handle.GetHashCode();

    public static bool operator ==(ThreadSnapshot left, ThreadSnapshot right) => left.Equals(right);
    public static bool operator !=(ThreadSnapshot left, ThreadSnapshot right) => !left.Equals(right);

    /// <exception cref="WindowsException"/>
    public static ThreadSnapshot CreateSnapshot()
    {
        HANDLE hSnapshot = Kernel32.CreateToolhelp32Snapshot(TH32CS.SNAPTHREAD, default);
        if (Kernel32.InvalidHandle == hSnapshot)
        { throw WindowsException.Get(); }
        return new ThreadSnapshot(hSnapshot);
    }

    [SupportedOSPlatform("windows")]
    public struct ThreadSnapshotEnumerator : IEnumerator<ThreadEntry>
    {
        HANDLE _handle;
        ThreadEntry _current;
        bool _isStarted;

        public ThreadSnapshotEnumerator(HANDLE handle)
        {
            _handle = handle;
        }

        public readonly ThreadEntry Current => _current;
        readonly object IEnumerator.Current => _current;

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe void Reset()
        {
            ObjectDisposedException.ThrowIf(_handle == 0, this);

            _isStarted = true;

            ThreadEntry threadEntry = ThreadEntry.Create();

            int result = Kernel32.Thread32First(_handle, ref threadEntry);

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

            _current = threadEntry;
        }

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe bool MoveNext()
        {
            ObjectDisposedException.ThrowIf(_handle == 0, this);

            ThreadEntry threadEntry = ThreadEntry.Create();

            if (!_isStarted)
            {
                _isStarted = true;

                int result2 = Kernel32.Thread32First(_handle, ref threadEntry);

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

                _current = threadEntry;
            }

            int result = Kernel32.Thread32Next(_handle, out threadEntry);
            if (result != TRUE) return false;
            _current = threadEntry;
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
