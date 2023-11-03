using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Contains global cursor information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly struct CursorInfo
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// The caller must set this to <see langword="sizeof"/>(<see cref="CURSORINFO"/>).
        /// </summary>
        readonly DWORD cbSize;
        /// <summary>
        /// The cursor state. This parameter can be one of the following values.
        /// <list type="table">
        /// 
        /// <item>
        /// <term>
        /// 0
        /// </term>
        /// <description>
        /// The cursor is hidden.
        /// </description>
        /// </item>
        /// 
        /// <item>
        /// <term>
        /// CURSOR_SHOWING = 0x00000001
        /// </term>
        /// <description>
        /// The cursor is showing.
        /// </description>
        /// </item>
        /// 
        /// <item>
        /// <term>
        /// CURSOR_SUPPRESSED = 0x00000002
        /// </term>
        /// <description>
        /// <b>Windows 8:</b> The cursor is suppressed.
        /// This flag indicates that the system is
        /// not drawing the cursor because the user
        /// is providing input through touch or pen
        /// instead of the mouse.
        /// </description>
        /// </item>
        /// 
        /// </list>
        /// </summary>
        public readonly DWORD flags;
        /// <summary>
        /// A handle to the cursor.
        /// </summary>
        public readonly HCURSOR hCursor;
        /// <summary>
        /// A structure that receives the screen coordinates of the cursor.
        /// </summary>
        public readonly POINT ptScreenPos;

        CursorInfo(uint cbSize) : this() => this.cbSize = cbSize;
        unsafe public static CURSORINFO Create() => new((uint)sizeof(CURSORINFO));
    }
}
