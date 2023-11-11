using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Win32.LowLevel
{
    public static class Kernel32
    {
        public static readonly HANDLE INVALID_HANDLE_VALUE = (HANDLE)(-1);

        /// <summary>
        /// Sets the current size and position of a console screen buffer's window.
        /// </summary>
        /// <param name="hConsoleOutput">
        /// A handle to the console screen buffer.
        /// The handle must have the <c>GENERIC_READ</c> access right.
        /// </param>
        /// <param name="bAbsolute">
        /// If this parameter is <c>TRUE</c>, the coordinates specify the new upper-left and
        /// lower-right corners of the window. If it is <c>FALSE</c>, the coordinates are
        /// relative to the current window-corner coordinates.
        /// </param>
        /// <param name="lpConsoleWindow">
        /// A pointer to a <see cref="SMALL_RECT"/> structure that specifies
        /// the new upper-left and lower-right corners of the window.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern BOOL SetConsoleWindowInfo(
          [In] HANDLE     hConsoleOutput,
          [In] BOOL       bAbsolute,
          [In] SMALL_RECT*lpConsoleWindow
        );

        /// <summary>
        /// Retrieves information about the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">
        /// A handle to the console screen buffer.
        /// The handle must have the <c>GENERIC_READ</c> access right.
        /// </param>
        /// <param name="lpConsoleScreenBufferInfo">
        /// A pointer to a <see cref="CONSOLE_SCREEN_BUFFER_INFO"/> structure
        /// that receives the console screen buffer information.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// The rectangle returned in the srWindow member of the
        /// <see cref="CONSOLE_SCREEN_BUFFER_INFO"/> structure can be modified
        /// and then passed to the <see cref="SetConsoleWindowInfo"/> function to
        /// scroll the console screen buffer in the window, to change
        /// the size of the window, or both.
        /// </para>
        /// <para>
        /// All coordinates returned in the <see cref="CONSOLE_SCREEN_BUFFER_INFO"/> structure are in
        /// character-cell coordinates, where the origin (0, 0) is at the upper-left
        /// corner of the console screen buffer.
        /// </para>
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern BOOL GetConsoleScreenBufferInfo(
           [In] HANDLE                      hConsoleOutput,
           [Out] CONSOLE_SCREEN_BUFFER_INFO*lpConsoleScreenBufferInfo
         );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern HGLOBAL GlobalReAlloc(
          [In] HGLOBAL hMem,
          [In] SIZE_T dwBytes,
          [In] UINT uFlags
        );

        /// <summary>
        /// <para>
        /// Writes data to the specified file or input/output (I/O) device.
        /// </para>
        /// <para>
        /// This function is designed for both synchronous and asynchronous operation.
        /// For a similar function designed solely for asynchronous operation, see <see cref="WriteFileEx"/>.
        /// </para>
        /// </summary>
        /// <param name="hFile"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nNumberOfBytesToWrite"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <param name="lpOverlapped"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern BOOL WriteFile(
          [In] HANDLE hFile,
          [In] void* lpBuffer,
          [In] DWORD nNumberOfBytesToWrite,
          [Out, Optional] DWORD* lpNumberOfBytesWritten,
          [In, Out, Optional] OVERLAPPED* lpOverlapped
        );

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">
        /// A valid handle to an open object.
        /// </param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL CloseHandle(
          [In] HANDLE hObject
        );

        /// <summary>
        /// <para>
        /// Frees the specified global memory object and invalidates its handle.
        /// </para>
        /// <para>
        /// <b>Note:</b>
        /// The global functions have greater overhead and provide fewer
        /// features than other memory management functions. New applications
        /// should use the heap functions unless documentation states that a
        /// global function should be used. For more information,
        /// see Global and Local Functions.
        /// </para>
        /// </summary>
        /// <param name="hMem"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HGLOBAL GlobalFree(
          [In] HGLOBAL hMem
        );

        /// <summary>
        /// <para>
        /// Decrements the lock count associated with a memory object that
        /// was allocated with <c>GMEM_MOVEABLE</c>. This function has no effect
        /// on memory objects allocated with <c>GMEM_FIXED</c>.
        /// </para>
        /// <para>
        /// <b>Note:</b>
        /// The global functions have greater overhead and provide fewer
        /// features than other memory management functions. New applications
        /// should use the heap functions unless documentation states that a
        /// global function should be used. For more information,
        /// see Global and Local Functions.
        /// </para>
        /// </summary>
        /// <param name="hMem"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL GlobalUnlock(
          [In] HGLOBAL hMem
        );

        /// <summary>
        /// <para>
        /// Creates or opens a file or I/O device. The most commonly used I/O devices
        /// are as follows: file, file stream, directory, physical disk, volume,
        /// console buffer, tape drive, communications resource, mailslot,
        /// and pipe. The function returns a handle that can be used to access
        /// the file or device for various types of I/O depending on the file
        /// or device and the flags and attributes specified.
        /// </para>
        /// <para>
        /// To perform this operation as a transacted operation, which results in a
        /// handle that can be used for transacted I/O, use the <see cref="CreateFileTransactedW"/> function.
        /// </para>
        /// </summary>
        /// <param name="lpFileName"></param>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="dwShareMode"></param>
        /// <param name="lpSecurityAttributes"></param>
        /// <param name="dwCreationDisposition"></param>
        /// <param name="dwFlagsAndAttributes"></param>
        /// <param name="hTemplateFile"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern HANDLE CreateFileW(
          [In] WCHAR* lpFileName,
          [In] DWORD dwDesiredAccess,
          [In] DWORD dwShareMode,
          [In, Optional] SECURITY_ATTRIBUTES* lpSecurityAttributes,
          [In] DWORD dwCreationDisposition,
          [In] DWORD dwFlagsAndAttributes,
          [In, Optional] HANDLE hTemplateFile
        );

        /// <summary>
        /// <para>
        /// Locks a global memory object and returns a pointer to the first byte of the object's memory block.
        /// </para>
        /// <para>
        /// <b>Note:</b>
        /// The global functions have greater overhead and provide fewer features
        /// than other memory management functions. New applications should use
        /// the heap functions unless documentation states that a global function
        /// should be used. For more information, see Global and Local Functions.
        /// </para>
        /// </summary>
        /// <param name="hMem"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern void* GlobalLock(
          [In] HGLOBAL hMem
        );

        /// <summary>
        /// <para>
        /// Allocates the specified number of bytes from the heap.
        /// </para>
        /// <para>
        /// <b>Note:</b>
        /// The global functions have greater overhead and
        /// provide fewer features than other memory management functions.
        /// New applications should use the heap functions unless
        /// documentation states that a global function should be used.
        /// For more information, see Global and Local Functions.
        /// </para>
        /// </summary>
        /// <param name="uFlags"></param>
        /// <param name="dwBytes"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HGLOBAL GlobalAlloc(
          [In] UINT uFlags,
          [In] SIZE_T dwBytes
        );

        /// <summary>
        /// Sets the last-error code for the calling thread.
        /// </summary>
        /// <param name="dwErrCode">
        /// The last-error code for the thread.
        /// </param>
        /// <remarks>
        /// <para>
        /// The last-error code is kept in thread local
        /// storage so that multiple threads do not overwrite each other's values.
        /// </para>
        /// <para>
        /// Most functions call <c>SetLastError</c> or <c>SetLastErrorEx</c> only
        /// when they fail. However, some system functions call
        /// <c>SetLastError</c> or <c>SetLastErrorEx</c> under conditions of
        /// success; those cases are noted in each function's documentation.
        /// </para>
        /// <para>
        /// Applications can optionally retrieve the value set by this function
        /// by using the <see cref="GetLastError"/> function immediately after a function fails.
        /// </para>
        /// <para>
        /// Error codes are 32-bit values (bit 31 is the most significant bit).
        /// Bit 29 is reserved for application-defined error codes;
        /// no system error code has this bit set.
        /// If you are defining an error code for your application,
        /// set this bit to indicate that the error code has
        /// been defined by your application and to ensure that
        /// your error code does not conflict with any system-defined error codes.
        /// </para>
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern void SetLastError(
          [In] DWORD dwErrCode
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL SetConsoleScreenBufferSize(
          [In] HANDLE hConsoleOutput,
          [In] COORD dwSize
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL GetConsoleSelectionInfo(
          [Out] out CONSOLE_SELECTION_INFO lpConsoleSelectionInfo
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HWND GetConsoleWindow();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL GetNumberOfConsoleMouseButtons(
          [Out] out DWORD lpNumberOfMouseButtons
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern COORD GetLargestConsoleWindowSize(
          [In] HANDLE hConsoleOutput
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL GetCurrentConsoleFont(
          [In] HANDLE hConsoleOutput,
          [In] BOOL bMaximumWindow,
          [Out] out CONSOLE_FONT_INFO lpConsoleCurrentFont
        );

        /// <summary>
        /// Retrieves extended information about the current console font.
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL GetCurrentConsoleFontEx(
          [In] HANDLE hConsoleOutput,
          [In] BOOL bMaximumWindow,
          /*[Out]*/ ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx
        );

        /// <summary>
        /// Sets extended information about the current console font.
        /// </summary>
        /// <param name="hConsoleOutput">
        /// A handle to the console screen buffer.
        /// The handle must have the <c>GENERIC_WRITE</c> access right.
        /// For more information, see <see href="https://learn.microsoft.com/en-us/windows/console/console-buffer-security-and-access-rights">Console Buffer Security and Access Rights</see>.
        /// </param>
        /// <param name="bMaximumWindow">
        /// If this parameter is <c>TRUE</c>, font information is set for the maximum window size.
        /// If this parameter is <c>FALSE</c>, font information is set for the current window size.
        /// </param>
        /// <param name="lpConsoleCurrentFontEx">
        /// A pointer to a <see cref="CONSOLE_FONT_INFOEX"/> structure that contains the font information.
        /// </param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL SetCurrentConsoleFontEx(
          [In] HANDLE hConsoleOutput,
          [In] BOOL bMaximumWindow,
          [In] ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx
        );

        /// <summary>
        /// Retrieves the current system date and time in Coordinated Universal Time (UTC) format.
        /// </summary>
        /// <param name="lpSystemTime">
        /// A pointer to a <see cref="SYSTEMTIME"/> structure to receive the current
        /// system date and time. The <paramref name="lpSystemTime"/> parameter must not
        /// be <c>NULL</c>. Using <c>NULL</c> will result in an access violation.
        /// </param>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern void GetSystemTime(
          [Out] SYSTEMTIME* lpSystemTime
        );

        /// <summary>
        /// Retrieves the current system date and time.
        /// The information is in Coordinated Universal Time (UTC) format.
        /// </summary>
        /// <param name="lpSystemTimeAsFileTime">
        /// A pointer to a <see cref="FILETIME"/> structure to receive the current
        /// system date and time in UTC format.
        /// </param>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern void GetSystemTimeAsFileTime(
          [Out] FILETIME* lpSystemTimeAsFileTime
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HLOCAL LocalFree(
          [In] HLOCAL hMem
        );

        /// <include file="Docs/Kernel32/ReadConsoleInput.xml" path="/*"/>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL ReadConsoleInput(
            [In] HANDLE hConsoleInput,
            [Out] InputEvent[] lpBuffer,
            [In] DWORD nLength,
            ref DWORD lpNumberOfEventsRead);


        /// <include file="Docs/Kernel32/WriteConsoleInput.xml" path="/*"/>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleInput(
            [In] HANDLE hConsoleInput,
            [In] InputEvent[] lpBuffer,
            [In] DWORD nLength,
            ref DWORD lpNumberOfEventsWritten);


        /// <include file="Docs/Kernel32/GetConsoleCP.xml" path="/*"/>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern UINT GetConsoleCP();

        /// <include file="Docs/Kernel32/GetConsoleOutputCP.xml" path="/*"/>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern UINT GetConsoleOutputCP();

        /// <include file="Docs/Kernel32/SetConsoleCP.xml" path="/*"/>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL SetConsoleCP(
            [In] UINT wCodePageID);

        /// <include file="Docs/Kernel32/SetConsoleOutputCP.xml" path="/*"/>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL SetConsoleOutputCP(
            [In] UINT wCodePageID);

        /// <include file="Docs/Kernel32/GetStdHandle.xml" path="/*"/>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HANDLE GetStdHandle(
            [In] DWORD nStdHandle);

        [DllImport("kernel32.dll", EntryPoint = "CreateFile", SetLastError = true, CharSet = CharSet.Unicode)]
        unsafe public static extern SafeFileHandle CreateFileSafe(
            [In] WCHAR* lpFileName,
            [In] DWORD dwDesiredAccess,
            [In] DWORD dwShareMode,
            [In, Optional] SECURITY_ATTRIBUTES* lpSecurityAttributes,
            [In] DWORD dwCreationDisposition,
            [In] DWORD dwFlagsAndAttributes,
            [In, Optional] HANDLE hTemplateFile);

        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", SetLastError = true, CharSet = CharSet.Unicode)]
        unsafe public static extern HANDLE CreateFile(
            [In] WCHAR* lpFileName,
            [In] DWORD dwDesiredAccess,
            [In] DWORD dwShareMode,
            [In, Optional] SECURITY_ATTRIBUTES* lpSecurityAttributes,
            [In] DWORD dwCreationDisposition,
            [In] DWORD dwFlagsAndAttributes,
            [In, Optional] HANDLE hTemplateFile);

        /// <include file="Docs/Kernel32/WriteConsoleOutput.xml" path="/*"/>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleOutput(
            [In] HANDLE hConsoleOutput,
            [In] CharInfo[] lpBuffer,
            [In] Coord dwBufferSize,
            [In] Coord dwBufferCoord,
            [In, Out] ref SmallRect lpWriteRegion);

        /// <include file="Docs/Kernel32/WriteConsoleOutput.xml" path="/*"/>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleOutputW(
            SafeFileHandle hConsoleOutput,
            [In] CharInfo[] lpBuffer,
            [In] Coord dwBufferSize,
            [In] Coord dwBufferCoord,
            [In, Out] ref SmallRect lpWriteRegion);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL GetConsoleMode(
            [In] HANDLE hConsoleInput,
            ref DWORD lpMode);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL SetConsoleMode(
            [In] HANDLE hConsoleInput,
            [In] DWORD dwMode);

        /// <include file="Docs/Kernel32/GetLastError.xml" path="/*"/>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern DWORD GetLastError();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern DWORD FormatMessage(
            [In] DWORD dwFlags,
            [In, Optional] IntPtr lpSource,
            [In] DWORD dwMessageId,
            [In] DWORD dwLanguageId,
            [Out] WCHAR* lpBuffer,
            [In] DWORD nSize,
            [In, Optional] IntPtr Arguments);
    }
}
