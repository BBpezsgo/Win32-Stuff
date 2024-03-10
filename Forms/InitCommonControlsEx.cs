namespace Win32.Forms;

/// <summary>
/// Carries information used to load common control classes from the dynamic-link library
/// (DLL). This structure is used with the <see cref="Comctl32.InitCommonControlsEx"/> function.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct InitCommonControlsEx
{
    /// <summary>
    /// The size of the structure, in bytes.
    /// </summary>
    readonly DWORD Size;
    /// <summary>
    /// The set of bit flags that indicate which common control classes
    /// will be loaded from the DLL. This can be a combination of the following values.
    /// </summary>
    public readonly IIC IIC;

    public InitCommonControlsEx(IIC iic)
    {
        Size = (DWORD)Marshal.SizeOf<InitCommonControlsEx>();
        IIC = iic;
    }
}
