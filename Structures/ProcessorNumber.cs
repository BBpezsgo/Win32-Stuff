using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Win32
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessorNumber
    {
        /// <summary>
        /// The processor group to which the logical processor is assigned.
        /// </summary>
        public WORD Group;

        /// <summary>
        /// The number of the logical processor relative to the group.
        /// </summary>
        public BYTE Number;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly BYTE Reserved;

        readonly string GetDebuggerDisplay() => $"{{ Group: {Group} Number: {Number} }}";
    }
}
