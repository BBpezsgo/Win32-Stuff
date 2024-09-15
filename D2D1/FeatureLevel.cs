namespace Win32.D2D1;

/// <summary>
/// Describes the minimum DirectX support required for hardware rendering by a
/// render target.
/// </summary>
public enum FeatureLevel : DWORD
{
    /// <summary>
    /// The caller does not require a particular underlying D3D device level.
    /// </summary>
    Default = 0,

    /// <summary>
    /// The D3D device level is DX9 compatible.
    /// </summary>
    _9 = D3DFeatureLevel._9_1,

    /// <summary>
    /// The D3D device level is DX10 compatible.
    /// </summary>
    _10 = D3DFeatureLevel._10_0,
}
