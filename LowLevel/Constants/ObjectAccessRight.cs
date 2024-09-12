namespace Win32;

[Flags]
public enum ObjectAccessRight : DWORD
{
    /// <summary>
    /// Required to delete the object.
    /// </summary>
    Delete = 0x00010000,
    /// <summary>
    /// Required to read information in the security descriptor for
    /// the object, not including the information in the SACL.
    /// To read or write the SACL, you must request the
    /// <c>ACCESS_SYSTEM_SECURITY</c> access right.
    /// For more information, see SACL Access Right.
    /// </summary>
    ReadControl = 0x00020000,
    /// <summary>
    /// Required to modify the DACL in the security descriptor
    /// for the object.
    /// </summary>
    WriteDac = 0x00040000,
    /// <summary>
    /// Required to change the owner in the security descriptor
    /// for the object.
    /// </summary>
    WriteOwner = 0x00080000,
    /// <summary>
    /// The right to use the object for synchronization. This enables
    /// a thread to wait until the object is in the signaled state.
    /// </summary>
    Synchronize = 0x00100000,
}
