namespace Win32.D2D1
{
    /// <summary>
    /// Describes whether a render target uses hardware or software rendering, or if
    /// Direct2D should select the rendering mode.
    /// </summary>
    public enum D2D1_RENDER_TARGET_TYPE : DWORD
    {
        /// <summary>
        /// D2D is free to choose the render target type for the caller.
        /// </summary>
        DEFAULT = 0,

        /// <summary>
        /// The render target will render using the CPU.
        /// </summary>
        SOFTWARE = 1,

        /// <summary>
        /// The render target will render using the GPU.
        /// </summary>
        HARDWARE = 2,
    }
}
