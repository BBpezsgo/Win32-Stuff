using System.Runtime.InteropServices;

namespace Win32.DWrite
{
    /// <summary>
    /// The DWRITE_TRIMMING structure specifies the trimming option for text overflowing the layout box.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_TRIMMING
    {
        /// <summary>
        /// Text granularity of which trimming applies.
        /// </summary>
        public DWRITE_TRIMMING_GRANULARITY granularity;

        /// <summary>
        /// Character code used as the delimiter signaling the beginning of the portion of text to be preserved,
        /// most useful for path ellipsis, where the delimiter would be a slash. Leave this zero if there is no
        /// delimiter.
        /// </summary>
        public UINT32 delimiter;

        /// <summary>
        /// How many occurrences of the delimiter to step back. Leave this zero if there is no delimiter.
        /// </summary>
        public UINT32 delimiterCount;
    }
}
