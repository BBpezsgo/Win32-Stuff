namespace Win32;

public enum CreateFileFlags : DWORD
{
    /// <summary>
    /// Creates a new file, only if it does not already exist.
    /// If the specified file exists, the function fails and the last-error code is set
    /// to ERROR_FILE_EXISTS(80). If the specified file does not exist and is a valid
    /// path to a writable location, a new file is created.
    /// </summary>
    CreateNew = 1,
    /// <summary>
    /// Creates a new file, always.
    /// If the specified file exists and is writable, the function truncates the file,
    /// the function succeeds, and last-error code is set to ERROR_ALREADY_EXISTS(183).
    /// If the specified file does not exist and is a valid path, a new file is created, the function succeeds, and the last-error code is set to zero.For more information, see the Remarks section of this topic.
    /// </summary>
    CreateAlways = 2,
    /// <summary>
    /// Opens a file or device, only if it exists.
    /// If the specified file or device does not exist, the function fails and the
    /// last-error code is set to ERROR_FILE_NOT_FOUND (2). For more information
    /// about devices, see the Remarks section.
    /// </summary>
    OpenExisting = 3,
    /// <summary>
    /// Opens a file, always. If the specified file exists,
    /// the function succeeds and the last-error code is set to ERROR_ALREADY_EXISTS(183).
    /// If the specified file does not exist and is a valid path to a writable location,
    /// the function creates a file and the last-error code is set to zero.
    /// </summary>
    OpenAlways = 4,
    /// <summary>
    /// Opens a file and truncates it so that its size is zero bytes, only if it exists.
    /// If the specified file does not exist, the function fails and the last-error
    /// code is set to ERROR_FILE_NOT_FOUND (2). The calling process must open the
    /// file with the GENERIC_WRITE bit set as part of the dwDesiredAccess parameter.
    /// </summary>
    TruncateExisting = 5,
}
