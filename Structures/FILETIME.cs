using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <summary>
    /// Contains a 64-bit value representing the number
    /// of 100-nanosecond intervals since January 1, 1601 (UTC).
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct FileTime :
        IEquatable<FileTime>,
        System.Numerics.IEqualityOperators<FileTime, FileTime, bool>
    {
        [FieldOffset(0)] readonly UINT64 DateTime;

        /// <summary>
        /// The low-order part of the file time.
        /// </summary>
        [FieldOffset(0)] public readonly DWORD LowDateTime;
        /// <summary>
        /// The high-order part of the file time.
        /// </summary>
        [FieldOffset(4)] public readonly DWORD HighDateTime;

        public static implicit operator UINT64(FileTime v) => v.DateTime;
        public static implicit operator DateTime(FileTime v) => System.DateTime.FromFileTimeUtc(unchecked((long)v.DateTime));

        public static bool operator ==(FileTime left, FileTime right) => left.Equals(right);
        public static bool operator !=(FileTime left, FileTime right) => !left.Equals(right);

        public override bool Equals(object? obj) => obj is FileTime time && Equals(time);
        public bool Equals(FileTime other) => DateTime == other.DateTime;
        public override int GetHashCode() => DateTime.GetHashCode();
    }
}
