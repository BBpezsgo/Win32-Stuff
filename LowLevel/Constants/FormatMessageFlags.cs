namespace Win32;

[Flags]
public enum FormatMessageFlags : DWORD
{
    /// <summary>
    /// <para>
    /// The function allocates a buffer large enough to hold
    /// the formatted message, and places a pointer to the
    /// allocated buffer at the address specified by <c>lpBuffer</c>.
    /// The <c>lpBuffer</c> parameter is a pointer to an <c>LPTSTR</c>; you
    /// must cast the pointer to an <c>LPTSTR</c> (for example, <c>(LPTSTR)&amp;lpBuffer</c>).
    /// The <c>nSize</c> parameter specifies the minimum number of <c>TCHAR</c>s
    /// to allocate for an output message buffer. The caller should
    /// use the LocalFree function to free the buffer when it is no longer needed.
    /// </para>
    /// <para>
    /// If the length of the formatted message exceeds 128K
    /// bytes, then FormatMessage will fail and a subsequent call
    /// to <see cref="Kernel32.GetLastError"/> will return <c>ERROR_MORE_DATA</c>.
    /// </para>
    /// <para>
    /// In previous versions of Windows, this value was not
    /// available for use when compiling Windows Store apps.
    /// As of Windows 10 this value can be used.
    /// </para>
    /// </summary>
    AllocateBuffer = 0x00000100,

    /// <summary>
    /// <para>
    /// The <c>Arguments</c> parameter is not a <c>va_list</c> structure,
    /// but is a pointer to an array of values that represent the arguments.
    /// </para>
    /// <para>
    /// This flag cannot be used with 64-bit integer values.
    /// If you are using a 64-bit integer, you must use the <c>va_list</c> structure.
    /// </para>
    /// </summary>
    ArgumentArray = 0x00002000,

    /// <summary>
    /// <para>
    /// The <c>lpSource</c> parameter is a module handle containing
    /// the message-table resource(s) to search. If this <c>lpSource</c>
    /// handle is <c>NULL</c>, the current process's application image
    /// file will be searched. This flag cannot be used with <see cref="FromString"/>.
    /// </para>
    /// <para>
    /// If the module has no message table resource, the function
    /// fails with <c>ERROR_RESOURCE_TYPE_NOT_FOUND</c>.
    /// </para>
    /// </summary>
    FromHModule = 0x00000800,
    /// <summary>
    /// The <c>lpSource</c> parameter is a pointer to a null-terminated
    /// string that contains a message definition. The message
    /// definition may contain insert sequences, just as the message
    /// text in a message table resource may.This flag cannot be
    /// used with <see cref="FromHModule"/> or <see cref="FromSystem"/>.
    /// </summary>
    FromSystem = 0x00001000,

    /// <summary>
    /// <para>
    /// The function should search the system message-table
    /// resource(s) for the requested message.If this flag is
    /// specified with <see cref="FromHModule"/>, the function
    /// searches the system message table if the message is not
    /// found in the module specified by <c>lpSource</c>.This flag cannot
    /// be used with <see cref="FromString"/>.
    /// </para>
    /// <para>
    /// If this flag is specified, an application can pass the
    /// result of the <see cref="Kernel32.GetLastError"/> function to retrieve the
    /// message text for a system-defined error.
    /// </para>
    /// </summary>
    FromString = 0x00000400,

    /// <summary>
    /// Insert sequences in the message definition such as %1 are
    /// to be ignored and passed through to the output buffer
    /// unchanged. This flag is useful for fetching a message
    /// for later formatting. If this flag is set, the <c>Arguments</c>
    /// parameter is ignored.
    /// </summary>
    IgnoreInserts = 0x00000200,

    /// <summary>
    /// There are no output line width restrictions.
    /// The function stores line breaks that are in the
    /// message definition text into the output buffer.
    /// </summary>
    _ = 0,

    /// <summary>
    /// The function ignores regular line breaks in the
    /// message definition text. The function stores hard-coded
    /// line breaks in the message definition text into the
    /// output buffer. The function generates no new line breaks.
    /// </summary>
    MaxWidthMask = 0x000000FF,
}
