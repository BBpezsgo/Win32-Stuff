global using TPMPARAMS = Win32.TrackPopupMenuParams;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Contains extended parameters for the <see cref="User32.TrackPopupMenuEx"/> function.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TrackPopupMenuParams
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly DWORD StructSize;

        /// <summary>
        /// The rectangle to be excluded when positioning the window, in screen coordinates.
        /// </summary>
        public RECT Exclude;

        TrackPopupMenuParams(DWORD structSize) : this() => StructSize = structSize;

        public static unsafe TPMPARAMS Create() => new((DWORD)sizeof(TPMPARAMS));
    }
}
