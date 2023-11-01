using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct RgState
    {
        public readonly DWORD Flags;
        /// <summary>
        /// The element can accept the focus.
        /// </summary>
        public bool IsFocusable => (Flags & 0x00100000) != 0;
        /// <summary>
        /// The element is invisible.
        /// </summary>
        public bool IsInvisible => (Flags & 0x00008000) != 0;
        /// <summary>
        /// The element has no visible representation.
        /// </summary>
        public bool IsOffscreen => (Flags & 0x00010000) != 0;
        /// <summary>
        /// The element is unavailable.
        /// </summary>
        public bool IsUnavailable => (Flags & 0x00000001) != 0;
        /// <summary>
        /// The element is in the pressed state.
        /// </summary>
        public bool IsPressed => (Flags & 0x00000008) != 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct RgStates
    {
        public readonly RgState TitleBar;
        readonly DWORD Reserved;
        public readonly RgState MinimizeButton;
        public readonly RgState MaximizeButton;
        public readonly RgState HelpButton;
        public readonly RgState CloseButton;
    }

    [StructLayout(LayoutKind.Sequential)]
    /// <summary>
    /// Contains title bar information.
    /// </summary>
    public readonly struct TitleBarInfo
    {
        /// <summary>
        /// The size, in bytes, of the structure.
        /// The caller must set this member to <c>sizeof(TITLEBARINFO)</c>.
        /// </summary>
        readonly DWORD cbSize;
        /// <summary>
        /// The coordinates of the title bar.
        /// These coordinates include all title-bar elements except the window menu.
        /// </summary>
        public readonly RECT TitleBar;
        /// <summary>
        /// An array that receives a value for each element of the title bar.
        /// The following are the title bar elements represented by the array.
        /// </summary>
        public readonly RgStates RgState;

        TitleBarInfo(DWORD cbSize) : this() => this.cbSize = cbSize;

        unsafe public static TitleBarInfo Create() => new((uint)sizeof(TitleBarInfo));
    }
}
