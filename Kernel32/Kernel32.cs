using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Win32
{
    public static class Kernel32
    {
        public const uint STD_INPUT_HANDLE = unchecked((uint)-10);
        public const uint STD_OUTPUT_HANDLE = unchecked((uint)-11);
        public const uint STD_ERROR_HANDLE = unchecked((uint)-12);

        /// <summary>
        /// Reads data from a console input buffer and removes it from the buffer.
        /// </summary>
        /// <param name="hConsoleInput">
        /// A handle to the console input buffer. The handle must have the <c>GENERIC_READ</c> access right. For more information, see <see href="https://learn.microsoft.com/en-us/windows/console/console-buffer-security-and-access-rights">Console Buffer Security and Access Rights</see>.
        /// </param>
        /// <param name="lpBuffer">
        /// A pointer to an array of <c>INPUT_RECORD</c> structures that receives the input buffer data.
        /// </param>
        /// <param name="nLength">
        /// The size of the array pointed to by the <paramref name="lpBuffer"/> parameter, in array elements.
        /// </param>
        /// <param name="lpNumberOfEventsRead">
        /// A pointer to a variable that receives the number of input records read.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If the number of records requested in the <paramref name="nLength"/> parameter exceeds the number of records available in the buffer, the number available is read. The function does not return until at least one input record has been read.
        /// </para>
        /// <para>
        /// A process can specify a console input buffer handle in one of the wait functions to determine when there is unread console input. When the input buffer is not empty, the state of a console input buffer handle is signaled.
        /// </para>
        /// <para>
        /// To determine the number of unread input records in a console's input buffer, use the GetNumberOfConsoleInputEvents function. To read input records from a console input buffer without affecting the number of unread records, use the PeekConsoleInput function. To discard all unread records in a console's input buffer, use the FlushConsoleInputBuffer function.
        /// </para>
        /// <para>
        /// This function uses either Unicode characters or 8-bit characters from the console's current code page. The console's code page defaults initially to the system's OEM code page. To change the console's code page, use the <see cref="SetConsoleCP"/> or <see cref="SetConsoleOutputCP"/> functions. Legacy consumers may also use the chcp or mode con cp select= commands, but it is not recommended for new development.
        /// </para>
        /// </remarks>
        /// <example>
        /// For an example, see <see href="https://learn.microsoft.com/en-us/windows/console/reading-input-buffer-events">Reading Input Buffer Events</see>.
        /// </example>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL ReadConsoleInput(
            [In] HANDLE hConsoleInput,
            [Out] InputEvent[] lpBuffer,
            [In] DWORD nLength,
            ref DWORD lpNumberOfEventsRead);

        /// <summary>
        /// Writes data directly to the console input buffer.
        /// </summary>
        /// <param name="hConsoleInput">
        /// A handle to the console input buffer. The handle must have the <c>GENERIC_WRITE</c> access right. For more information, see <see href="https://learn.microsoft.com/en-us/windows/console/console-buffer-security-and-access-rights">Console Buffer Security and Access Rights</see>.
        /// </param>
        /// <param name="lpBuffer">
        /// A pointer to an array of <c>INPUT_RECORD</c> structures that contain data to be written to the input buffer.
        /// </param>
        /// <param name="nLength">
        /// The number of input records to be written.
        /// </param>
        /// <param name="lpNumberOfEventsWritten">
        /// A pointer to a variable that receives the number of input records actually written.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// <c>WriteConsoleInput</c> places input records into the input buffer behind any pending events in the buffer. The input buffer grows dynamically, if necessary, to hold as many events as are written.
        /// </para>
        /// <para>
        /// This function uses either Unicode characters or 8-bit characters from the console's current code page. The console's code page defaults initially to the system's OEM code page. To change the console's code page, use the <see cref="SetConsoleCP"/> or <see cref="SetConsoleOutputCP"/> functions. Legacy consumers may also use the chcp or mode con cp select= commands, but it is not recommended for new development.
        /// </para>
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleInput(
            [In] HANDLE hConsoleInput,
            [In] InputEvent[] lpBuffer,
            [In] DWORD nLength,
            ref DWORD lpNumberOfEventsWritten);

        /// <summary>
        /// Retrieves the input code page used by the console associated with the calling process. A console uses its input code page to translate keyboard input into the corresponding character value.
        /// </summary>
        /// <returns>
        /// <para>
        /// The return value is a code that identifies the code page. For a list of identifiers, see <see href="https://learn.microsoft.com/en-us/windows/win32/intl/code-page-identifiers">Code Page Identifiers</see>.
        /// </para>
        /// <para>
        /// If the return value is zero, the function has failed. To get extended error information, call <see cref="GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// A code page maps 256 character codes to individual characters. Different code pages include different special characters, typically customized for a language or a group of languages. To retrieve more information about a code page, including it's name, see the GetCPInfoEx function.
        /// </para>
        /// <para>
        /// To set a console's input code page, use the <see cref="SetConsoleCP"/> function. To set and query a console's output code page, use the <see cref="SetConsoleOutputCP"/> and <see cref="GetConsoleOutputCP"/> functions.
        /// </para>
        /// </remarks>
        [DllImport("Kernel32.dll")]
        public static extern UINT GetConsoleCP();

        /// <summary>
        /// Retrieves the output code page used by the console associated with the calling process. A console uses its output code page to translate the character values written by the various output functions into the images displayed in the console window.
        /// </summary>
        /// <returns>
        /// <para>
        /// The return value is a code that identifies the code page. For a list of identifiers, see <see href="https://learn.microsoft.com/en-us/windows/win32/intl/code-page-identifiers">Code Page Identifiers</see>.
        /// </para>
        /// <para>
        /// If the return value is zero, the function has failed. To get extended error information, call <see cref="GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("Kernel32.dll")]
        public static extern UINT GetConsoleOutputCP();

        /// <summary>
        /// Sets the input code page used by the console associated with the calling process. A console uses its input code page to translate keyboard input into the corresponding character value.
        /// </summary>
        /// <param name="wCodePageID">
        /// The identifier of the code page to be set. For more information, see Remarks.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// A code page maps 256 character codes to individual characters. Different code pages include different special characters, typically customized for a language or a group of languages.
        /// </para>
        /// <para>
        /// To find the code pages that are installed or supported by the operating system, use the EnumSystemCodePages function. The identifiers of the code pages available on the local computer are also stored in the registry under the following key:
        /// <c>HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Nls\CodePage</c>
        /// </para>
        /// <para>
        /// However, it is better to use EnumSystemCodePages to enumerate code pages because the registry can differ in different versions of Windows.
        /// </para>
        /// <para>
        /// To determine whether a particular code page is valid, use the IsValidCodePage function. To retrieve more information about a code page, including its name, use the GetCPInfoEx function. For a list of available code page identifiers, see Code Page Identifiers.
        /// </para>
        /// <para>
        /// To determine a console's current input code page, use the <see cref="GetConsoleCP"/> function. To set and retrieve a console's output code page, use the <see cref="SetConsoleOutputCP"/> and <see cref="GetConsoleOutputCP"/> functions.
        /// </para>
        /// </remarks>
        [DllImport("Kernel32.dll")]
        public static extern BOOL SetConsoleCP([In] UINT wCodePageID);

        /// <summary>
        /// Sets the output code page used by the console associated with the calling process. A console uses its output code page to translate the character values written by the various output functions into the images displayed in the console window.
        /// </summary>
        /// <param name="wCodePageID">
        /// The identifier of the code page to set. For more information, see Remarks.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// A code page maps 256 character codes to individual characters. Different code pages include different special characters, typically customized for a language or a group of languages.
        /// </para>
        /// <para>
        /// If the current font is a fixed-pitch Unicode font, <c>SetConsoleOutputCP</c> changes the mapping of the character values into the glyph set of the font, rather than loading a separate font each time it is called. This affects how extended characters (ASCII value greater than 127) are displayed in a console window. However, if the current font is a raster font, <see cref="SetConsoleOutputCP"/> does not affect how extended characters are displayed.
        /// </para>
        /// <para>
        /// To find the code pages that are installed or supported by the operating system, use the EnumSystemCodePages function. The identifiers of the code pages available on the local computer are also stored in the registry under the following key:
        /// <c>HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Nls\CodePage</c>
        /// </para>
        /// <para>
        /// However, it is better to use EnumSystemCodePages to enumerate code pages because the registry can differ in different versions of Windows. To determine whether a particular code page is valid, use the IsValidCodePage function. To retrieve more information about a code page, including its name, use the GetCPInfoEx function. For a list of available code page identifiers, see Code Page Identifiers.
        /// </para>
        /// <para>
        /// To determine a console's current output code page, use the <see cref="GetConsoleOutputCP"/> function. To set and retrieve a console's input code page, use the <see cref="SetConsoleCP"/> and <see cref="GetConsoleCP"/> functions.
        /// </para>
        /// </remarks>
        [DllImport("Kernel32.dll")]
        public static extern BOOL SetConsoleOutputCP([In] UINT wCodePageID);

        [DllImport("kernel32.dll")]
        public static extern HANDLE GetStdHandle(DWORD nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] DWORD fileAccess,
            [MarshalAs(UnmanagedType.U4)] DWORD fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] DWORD flags,
            HANDLE template);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleOutput(
            HANDLE hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleOutputW(
            SafeFileHandle hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

        [DllImport("kernel32.dll")]
        public static extern BOOL GetConsoleMode(HANDLE hConsoleInput, ref DWORD lpMode);

        [DllImport("kernel32.dll")]
        public static extern BOOL SetConsoleMode(HANDLE hConsoleInput, DWORD dwMode);

        /// <summary>
        /// Retrieves the calling thread's last-error code value. The last-error code is maintained on a per-thread basis. Multiple threads do not overwrite each other's last-error code.
        /// </summary>
        /// <returns>
        /// <para>
        /// The return value is the calling thread's last-error code.
        /// </para>
        /// <para>
        /// The Return Value section of the documentation for each function that sets the last-error code notes the conditions under which the function sets the last-error code. Most functions that set the thread's last-error code set it when they fail. However, some functions also set the last-error code when they succeed. If the function is not documented to set the last-error code, the value returned by this function is simply the most recent last-error code to have been set; some functions set the last-error code to 0 on success and others do not.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// Functions executed by the calling thread set this value by calling the SetLastError function. You should call the <see cref="GetLastError"/> function immediately when a function's return value indicates that such a call will return useful data. That is because some functions call SetLastError with a zero when they succeed, wiping out the error code set by the most recently failed function.
        /// </para>
        /// <para>
        /// To obtain an error string for system error codes, use the FormatMessage function. For a complete list of error codes provided by the operating system, see <see href="https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes">System Error Codes</see>.
        /// </para>
        /// <para>
        /// The error codes returned by a function are not part of the Windows API specification and can vary by operating system or device driver. For this reason, we cannot provide the complete list of error codes that can be returned by each function. There are also many functions whose documentation does not include even a partial list of error codes that can be returned.
        /// </para>
        /// <para>
        /// Error codes are 32-bit values (bit 31 is the most significant bit). Bit 29 is reserved for application-defined error codes; no system error code has this bit set. If you are defining an error code for your application, set this bit to one. That indicates that the error code has been defined by an application, and ensures that your error code does not conflict with any error codes defined by the system.
        /// </para>
        /// <para>
        /// To convert a system error into an <c>HRESULT</c> value, use the <c>HRESULT_FROM_WIN32</c> macro.
        /// </para>
        /// </remarks>
        /// <example>
        /// For an example, see <see href="https://learn.microsoft.com/en-us/windows/win32/debug/retrieving-the-last-error-code">Retrieving the Last-Error Code</see>.
        /// </example>
        [DllImport("kernel32.dll")]
        public static extern DWORD GetLastError();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
        public static extern DWORD FormatMessage(
            DWORD dwFlags,
            IntPtr lpSource,
            DWORD dwMessageId,
            DWORD dwLanguageId,
            out StringBuilder lpBuffer,
            DWORD nSize,
            IntPtr Arguments);
    }

}