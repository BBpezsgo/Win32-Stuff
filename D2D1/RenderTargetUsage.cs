namespace Win32.D2D1;

/// <summary>
/// Describes how a render target is remoted and whether it should be
/// GDI-compatible. This enumeration allows a bitwise combination of its member
/// values.
/// </summary>
public enum RenderTargetUsage : DWORD
{
    None = 0x00000000,

    /// <summary>
    /// Rendering will occur locally, if a terminal-services session is established, the
    /// bitmap updates will be sent to the terminal services client.
    /// </summary>
    ForceBitmapRemoting = 0x00000001,

    /// <summary>
    /// The render target will allow a call to <see cref="User32.GetDC"/> on the <c>ID2D1GdiInteropRenderTarget</c>
    /// interface. Rendering will also occur locally.
    /// </summary>
    GDICompatible = 0x00000002,
}
