using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public struct GroupAffinity
    {
        /// <summary>
        /// A bitmap that specifies the affinity for zero or more processors within the specified group.
        /// </summary>
        public ULONG_PTR Mask;
        /// <summary>
        /// The processor group number.
        /// </summary>
        public WORD Group;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly WORD Reserved1;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly WORD Reserved2;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly WORD Reserved3;

        readonly string GetDebuggerDisplay() => $"{{ Group: {Group} Mask: {Mask} }}";
    }
}
