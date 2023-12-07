using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <summary>
    /// Contains a 64-bit value representing the number
    /// of 100-nanosecond intervals since January 1, 1601 (UTC).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct FileTime
    {
        /// <summary>
        /// The low-order part of the file time.
        /// </summary>
        public readonly DWORD LowDateTime;
        /// <summary>
        /// The high-order part of the file time.
        /// </summary>
        public readonly DWORD HighDateTime;
    }
}
