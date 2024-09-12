namespace Win32;

/// <remarks>
/// <para>
/// Securable objects use an
/// <see href="https://learn.microsoft.com/en-us/windows/win32/secauthz/access-mask-format">access mask format</see>
/// in which the four high-order bits specify
/// generic access rights. Each type of securable
/// object maps these bits to a set of its standard
/// and object-specific access rights.
/// For example, a Windows file object maps the
/// <see cref="GenericRead"/> bit to the READ_CONTROL and
/// SYNCHRONIZE standard access rights and to the
/// FILE_READ_DATA, FILE_READ_EA, and FILE_READ_ATTRIBUTES
/// object-specific access rights. Other types of
/// objects map the <see cref="GenericRead"/> bit to whatever
/// set of access rights is appropriate for that
/// type of object.
/// </para>
/// <para>
/// You can use generic access rights to specify
/// the type of access you need when you are
/// opening a handle to an object. This is
/// typically simpler than specifying all the
/// corresponding standard and specific rights.
/// </para>
/// <para>
/// Applications that define private securable
/// objects can also use the generic access rights.
/// </para>
/// </remarks>
public static class AccessRight
{
    /// <summary>
    /// Read access
    /// </summary>
    public const uint GenericRead = 0x80000000;
    /// <summary>
    /// Write access
    /// </summary>
    public const uint GenericWrite = 0x40000000;
    /// <summary>
    /// Execute access
    /// </summary>
    public const uint GenericExecute = 0x20000000;
    /// <summary>
    /// All possible access rights
    /// </summary>
    public const uint GenericAll = 0x10000000;
}
