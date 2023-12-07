namespace Win32.COM.LowLevel
{
    /// <summary>
    /// COM initialization flags; passed to <see cref="Ole32.CoInitializeEx"/>.
    /// </summary>
    [Flags]
    public enum COMInit : DWORD
    {
        /// <summary>
        /// Apartment model
        /// </summary>
        APARTMENTTHREADED = 0x2,
        /// <summary>
        /// OLE calls objects on any thread.
        /// </summary>
        MULTITHREADED = 0x0,
        /// <summary>
        ///  Don't use DDE for Ole1 support.
        /// </summary>
        DISABLE_OLE1DDE = 0x4,
        /// <summary>
        /// Trade memory for speed.
        /// </summary>
        SPEED_OVER_MEMORY = 0x8,
    }
}
