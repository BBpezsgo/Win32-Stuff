namespace Win32.D2D1
{
    /// <summary>
    /// Describes the minimum DirectX support required for hardware rendering by a
    /// render target.
    /// </summary>
    public enum D2D1_FEATURE_LEVEL : DWORD
    {
        /// <summary>
        /// The caller does not require a particular underlying D3D device level.
        /// </summary>
        DEFAULT = 0,

        /// <summary>
        /// The D3D device level is DX9 compatible.
        /// </summary>
        _9 = D3D_FEATURE_LEVEL._9_1,

        /// <summary>
        /// The D3D device level is DX10 compatible.
        /// </summary>
        _10 = D3D_FEATURE_LEVEL._10_0,
        FORCE_DWORD = 0xffffffff
    }
}
