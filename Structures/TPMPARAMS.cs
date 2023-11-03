using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Contains extended parameters for the <see cref="User32.TrackPopupMenuEx"/> function.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TpmParams
    {
        /// <summary>
        /// The size of structure, in bytes.
        /// </summary>
        readonly DWORD cbSize;
        /// <summary>
        /// The rectangle to be excluded when positioning the window, in screen coordinates.
        /// </summary>
        public RECT rcExclude;

        TpmParams(DWORD cbSize) : this() => this.cbSize = cbSize;
        unsafe public static TpmParams Create() => new((uint)sizeof(TpmParams));
    }
}
