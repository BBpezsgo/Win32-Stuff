namespace Win32.COM;

/// <summary>
/// COM initialization flags; passed to <see cref="Ole32.CoInitializeEx"/>.
/// </summary>
[Flags]
public enum COMInit : DWORD
{
    /// <summary>
    /// OLE calls objects on any thread.
    /// </summary>
    MultiThreaded = 0x0,
    /// <summary>
    /// Apartment model
    /// </summary>
    ApartmentThreaded = 0x2,
    /// <summary>
    ///  Don't use DDE for Ole1 support.
    /// </summary>
    DisableOLE1DDE = 0x4,
    /// <summary>
    /// Trade memory for speed.
    /// </summary>
    SpeedOverMemory = 0x8,
}
