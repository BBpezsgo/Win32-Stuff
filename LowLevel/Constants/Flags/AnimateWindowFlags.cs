namespace Win32.LowLevel
{
    /// <summary>
    /// Flags for <see cref="User32.AnimateWindow"/>
    /// </summary>
    [Flags]
    public enum AnimateWindowFlags : DWORD
    {
        /// <summary>
        /// Animates the window from left to right. This flag can be used with roll or slide animation. It is ignored when used with <see cref="CENTER"/> or <see cref="BLEND"/>.
        /// </summary>
        HOR_POSITIVE = 0x00000001,
        /// <summary>
        /// Animates the window from right to left. This flag can be used with roll or slide animation. It is ignored when used with <see cref="CENTER"/> or <see cref="BLEND"/>.
        /// </summary>
        HOR_NEGATIVE = 0x00000002,
        /// <summary>
        /// Animates the window from top to bottom. This flag can be used with roll or slide animation. It is ignored when used with <see cref="CENTER"/> or <see cref="BLEND"/>.
        /// </summary>
        VER_POSITIVE = 0x00000004,
        /// <summary>
        /// Animates the window from bottom to top. This flag can be used with roll or slide animation. It is ignored when used with <see cref="CENTER"/> or <see cref="BLEND"/>.
        /// </summary>
        VER_NEGATIVE = 0x00000008,
        /// <summary>
        /// Makes the window appear to collapse inward if <see cref="HIDE"/> is used or expand outward if the <see cref="HIDE"/> is not used. The various direction flags have no effect.
        /// </summary>
        CENTER = 0x00000010,
        /// <summary>
        /// Hides the window. By default, the window is shown.
        /// </summary>
        HIDE = 0x00010000,
        /// <summary>
        /// Activates the window. Do not use this value with <see cref="HIDE"/>.
        /// </summary>
        ACTIVATE = 0x00020000,
        /// <summary>
        /// Uses slide animation. By default, roll animation is used.This flag is ignored when used with <see cref="CENTER"/>.
        /// </summary>
        SLIDE = 0x00040000,
        /// <summary>
        /// Uses a fade effect. This flag can be used only if <c>hwnd</c> is a top-level window.
        /// </summary>
        BLEND = 0x00080000,
    }
}
