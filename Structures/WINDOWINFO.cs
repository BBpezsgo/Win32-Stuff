using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct WINDOWINFO
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// </summary>
        readonly DWORD cbSize;
        /// <summary>
        /// The coordinates of the window.
        /// </summary>
        public readonly RECT rcWindow;
        /// <summary>
        /// The coordinates of the client area.
        /// </summary>
        public readonly RECT rcClient;
        /// <summary>
        /// The window styles.
        /// </summary>
        public readonly DWORD dwStyle;
        /// <summary>
        /// The extended window styles.
        /// </summary>
        public readonly DWORD dwExStyle;
        /// <summary>
        /// The window status.
        /// If this member is <see cref="WS.ACTIVECAPTION"/> (0x0001),
        /// the window is active. Otherwise, this member is zero.
        /// </summary>
        public readonly DWORD dwWindowStatus;
        /// <summary>
        /// The width of the window border, in pixels.
        /// </summary>
        public readonly UINT cxWindowBorders;
        /// <summary>
        /// The height of the window border, in pixels.
        /// </summary>
        public readonly UINT cyWindowBorders;
        /// <summary>
        /// The window class atom (see <see cref="User32.RegisterClassExW"/>).
        /// </summary>
        public readonly ATOM atomWindowType;
        /// <summary>
        /// The Windows version of the application that created the window.
        /// </summary>
        public readonly WORD wCreatorVersion;

        WINDOWINFO(DWORD cbSize) : this() => this.cbSize = cbSize;
        unsafe public static WINDOWINFO Create() => new((uint)sizeof(WINDOWINFO));
    }
}
