using System.Collections;
using System.Globalization;

namespace Win32;

[SupportedOSPlatform("windows")]
public readonly struct ModuleSnapshot :
    IEnumerable<ModuleEntry>,
    IDisposable,
    IEquatable<ModuleSnapshot>,
    System.Numerics.IEqualityOperators<ModuleSnapshot, ModuleSnapshot, bool>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly HANDLE Handle;

    ModuleSnapshot(HANDLE handle) => Handle = handle;

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Kernel32.CloseHandle(Handle) == FALSE)
        { throw WindowsException.Get(); }
    }

    public readonly IEnumerator<ModuleEntry> GetEnumerator() => new ModuleSnapshotEnumerator(Handle);
    readonly IEnumerator IEnumerable.GetEnumerator() => new ModuleSnapshotEnumerator(Handle);

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
    public override bool Equals(object? obj) => obj is ModuleSnapshot snapshot && Equals(snapshot);
    public bool Equals(ModuleSnapshot other) => Handle.Equals(other.Handle);
    public override int GetHashCode() => Handle.GetHashCode();

    public static bool operator ==(ModuleSnapshot left, ModuleSnapshot right) => left.Equals(right);
    public static bool operator !=(ModuleSnapshot left, ModuleSnapshot right) => !left.Equals(right);

    /// <exception cref="WindowsException"/>
    public static ModuleSnapshot CreateSnapshot(uint processId)
    {
        HANDLE hSnapshot = Kernel32.CreateToolhelp32Snapshot(TH32CS.SNAPMODULE, processId);
        if (Kernel32.InvalidHandle == hSnapshot)
        { throw WindowsException.Get(); }
        return new ModuleSnapshot(hSnapshot);
    }

    [SupportedOSPlatform("windows")]
    public struct ModuleSnapshotEnumerator : IEnumerator<ModuleEntry>
    {
        HANDLE _handle;
        ModuleEntry _current;
        bool _isStarted;

        public ModuleSnapshotEnumerator(HANDLE handle)
        {
            _handle = handle;
        }

        public readonly ModuleEntry Current => _current;
        readonly object IEnumerator.Current => _current;

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe void Reset()
        {
            ObjectDisposedException.ThrowIf(_handle == 0, this);

            _isStarted = true;

            ModuleEntry moduleEntry = ModuleEntry.Create();

            int result = Kernel32.Module32FirstW(_handle, ref moduleEntry);

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

            _current = moduleEntry;
        }

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe bool MoveNext()
        {
            ObjectDisposedException.ThrowIf(_handle == 0, this);

            ModuleEntry moduleEntry = ModuleEntry.Create();

            if (!_isStarted)
            {
                _isStarted = true;

                int result2 = Kernel32.Module32FirstW(_handle, ref moduleEntry);

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

                _current = moduleEntry;
            }

            int result = Kernel32.Module32NextW(_handle, out moduleEntry);
            if (result != TRUE) return false;
            _current = moduleEntry;
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
