namespace Win32.D2D1
{
    /// <summary>
    /// Describes how a render target is remoted and whether it should be
    /// GDI-compatible. This enumeration allows a bitwise combination of its member
    /// values.
    /// </summary>
    public enum D2D1_RENDER_TARGET_USAGE:DWORD
    {
        NONE = 0x00000000,

        /// <summary>
        /// Rendering will occur locally, if a terminal-services session is established, the
        /// bitmap updates will be sent to the terminal services client.
        /// </summary>
        FORCE_BITMAP_REMOTING = 0x00000001,

        /// <summary>
        /// The render target will allow a call to GetDC on the ID2D1GdiInteropRenderTarget
        /// interface. Rendering will also occur locally.
        /// </summary>
        GDI_COMPATIBLE = 0x00000002,
    }
}
