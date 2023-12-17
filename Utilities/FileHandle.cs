using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    public readonly struct FileHandle :
        IDisposable,
        IEquatable<FileHandle>,
        System.Numerics.IEqualityOperators<FileHandle, FileHandle, bool>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HANDLE Handle;

        public FileHandle(HANDLE handle) => Handle = handle;

        public static explicit operator FileHandle(HANDLE handle) => new(handle);
        public static implicit operator HANDLE(FileHandle handle) => handle.Handle;

        public static bool operator ==(FileHandle left, FileHandle right) => left.Equals(right);
        public static bool operator !=(FileHandle left, FileHandle right) => !left.Equals(right);

        public override bool Equals(object? obj) => obj is FileHandle handle && Equals(handle);
        public bool Equals(FileHandle other) => Handle == other.Handle;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        /// <exception cref="WindowsException"/>
        unsafe public static FileHandle Create(
            string fileName,
            DWORD desiredAccess,
            DWORD shareMode,
            SecurityAttributes* securityAttributes,
            CreateFileFlags creationDisposition,
            DWORD flagsAndAttributes,
            HANDLE templateFile)
        {

            HANDLE handle;
            fixed (char* fileNamePtr = fileName)
            {
                handle = Kernel32.CreateFile(
                    fileNamePtr,
                    desiredAccess,
                    shareMode,
                    securityAttributes,
                    creationDisposition,
                    flagsAndAttributes,
                    templateFile);
            }

            if (handle == Kernel32.INVALID_HANDLE_VALUE)
            { throw WindowsException.Get(); }

            return new FileHandle(handle);
        }

        /// <exception cref="WindowsException"/>
        unsafe public DWORD Write<T>(ref T data, Overlapped* overlapped = null)
            where T : unmanaged
            => this.Write(Unsafe.AsPointer(ref data), sizeof(T), overlapped);

        /// <exception cref="WindowsException"/>
        unsafe public DWORD Write<T>(T* data, Overlapped* overlapped = null)
            where T : unmanaged
            => this.Write(data, sizeof(T), overlapped);

        /// <exception cref="WindowsException"/>
        unsafe public DWORD Write<T>(T data, Overlapped* overlapped = null)
            where T : unmanaged
            => this.Write(&data, sizeof(T), overlapped);

        /// <exception cref="WindowsException"/>
        unsafe public DWORD Write(void* buffer, int byteCount, Overlapped* overlapped = null)
        {
            DWORD bytesWritten = default;
            int res = Kernel32.WriteFile(Handle, buffer, (DWORD)byteCount, &bytesWritten, overlapped);
            if (res == FALSE)
            {
                uint error = Kernel32.GetLastError();
                if (error != 0x3E5) // ERROR_IO_PENDING
                { throw WindowsException.Get(error); }
            }
            return bytesWritten;
        }

        /// <exception cref="WindowsException"/>
        public void Dispose()
        {
            if (Kernel32.CloseHandle(Handle) == FALSE)
            { throw WindowsException.Get(); }
        }
    }
}
