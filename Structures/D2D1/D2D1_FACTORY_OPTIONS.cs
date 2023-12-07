using System.Runtime.InteropServices;

namespace Win32.D2D1.LowLevel
{
    /// <summary>
    /// Contains the debugging level of an ID2D1Factory object.
    /// </summary>
    /// <remarks>
    /// To enable debugging, you must install the
    /// <see href="https://learn.microsoft.com/en-us/windows/win32/Direct2D/direct2ddebuglayer-overview">Direct2D Debug Layer</see>.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_FACTORY_OPTIONS
    {
        /// <summary>
        /// The debugging level of the ID2D1Factory object.
        /// </summary>
        public D2D1_DEBUG_LEVEL debugLevel;
    }
}
