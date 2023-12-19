using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace Win32
{
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
            if (Kernel32.INVALID_HANDLE_VALUE == hSnapshot)
            { throw WindowsException.Get(); }
            return new ModuleSnapshot(hSnapshot);
        }
    }

    [SupportedOSPlatform("windows")]
    public class ModuleSnapshotEnumerator : IEnumerator<ModuleEntry>
    {
        readonly HANDLE Handle;
        ModuleEntry current;
        bool IsDisposed;
        bool IsStarted;

        public ModuleSnapshotEnumerator(HANDLE handle)
        {
            Handle = handle;
        }

        public ModuleEntry Current => current;
        object IEnumerator.Current => current;

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe void Reset()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);

            IsStarted = true;

            ModuleEntry moduleEntry = ModuleEntry.Create();

            int result = Kernel32.Module32FirstW(Handle, &moduleEntry);

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

            current = moduleEntry;
        }

        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="WindowsException"/>
        public unsafe bool MoveNext()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);

            ModuleEntry moduleEntry = ModuleEntry.Create();

            if (!IsStarted)
            {
                IsStarted = true;

                int result2 = Kernel32.Module32FirstW(Handle, &moduleEntry);

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

                current = moduleEntry;
            }

            int result = Kernel32.Module32NextW(Handle, &moduleEntry);
            if (result != TRUE) return false;
            current = moduleEntry;
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
        ~ModuleSnapshotEnumerator() { ActualDispose(); }
        /// <exception cref="WindowsException"/>
        public void Dispose()
        {
            ActualDispose();
            GC.SuppressFinalize(this);
        }
    }
}
