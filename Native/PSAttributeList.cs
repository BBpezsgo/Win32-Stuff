namespace Win32.Native;

/// <summary>
/// This structure defines a list of process or thread creation attributes.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct PSAttributeList
{
    /// <summary>
    /// The size of the entire structure in bytes, including the header.
    /// To store <c>N</c> attributes, the value should
    /// be <c><see langword="sizeof"/>(<see cref="PSAttributeList"/>) + (N - 1) * <see langword="sizeof"/>(<see cref="PSAttribute"/>)</c>
    /// due to the header including one attribute by default.
    /// </summary>
    public SIZE_T TotalLength;
    /// <summary>
    /// The array of <see cref="PSAttribute"/> structures.
    /// </summary>
    public PSAttribute Attributes;
}
