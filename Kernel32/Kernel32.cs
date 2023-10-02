using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Win32
{
    public static class Kernel32
    {
        public static readonly HANDLE INVALID_HANDLE_VALUE = (HANDLE)(-1);

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

        /// <summary>
        /// Retrieves a handle to the specified standard device
        /// (standard input, standard output, or standard error).
        /// </summary>
        /// <param name="nStdHandle">
        /// The standard device. This parameter can be one of the following values.
        /// <list type="table">
        /// 
        /// <item>
        /// <term>
        /// <see cref="StdHandle.STD_INPUT_HANDLE"/> ((<see cref="DWORD"/>)-10)
        /// </term>
        /// <description>
        /// The standard input device.
        /// Initially, this is the console input buffer, <c>CONIN$</c>.
        /// </description>
        /// </item>
        /// 
        /// <item>
        /// <term>
        /// <see cref="StdHandle.STD_OUTPUT_HANDLE"/> ((<see cref="DWORD"/>)-11)
        /// </term>
        /// <description>
        /// The standard output device.
        /// Initially, this is the active console screen buffer, <c>CONOUT$</c>.
        /// </description>
        /// </item>
        /// 
        /// <item>
        /// <term>
        /// <see cref="StdHandle.STD_ERROR_HANDLE"/> ((<see cref="DWORD"/>)-12)
        /// </term>
        /// <description>
        /// The standard error device.
        /// Initially, this is the active console screen buffer, <c>CONOUT$</c>.
        /// </description>
        /// </item>
        /// 
        /// </list>
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is a handle to the
        /// specified device, or a redirected handle set by a previous call
        /// to <see cref="SetStdHandle"/>. The handle has <c>GENERIC_READ</c> and <c>GENERIC_WRITE</c> access rights,
        /// unless the application has used <see cref="SetStdHandle"/> to set a standard handle with lesser access.
        /// </para>
        /// <para>
        /// If the function fails, the return
        /// value is <see cref="INVALID_HANDLE_VALUE"/>. To get extended error information, call <see cref="GetLastError"/>.
        /// </para>
        /// <para>
        /// If an application does not have associated standard handles,
        /// such as a service running on an interactive desktop, and has not redirected them,
        /// the return value is <c>NULL</c>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// Handles returned by <c>GetStdHandle</c> can be used by applications that
        /// need to read from or write to the console. When a console is created,
        /// the standard input handle is a handle to the console's input buffer,
        /// and the standard output and standard error handles are handles of the
        /// console's active screen buffer. These handles can be used by the <see cref="ReadFile"/>
        /// and <see cref="WriteFile"/> functions, or by any of the console functions that access
        /// the console input buffer or a screen buffer (for example, the <see cref="ReadConsoleInput"/>,
        /// <see cref="WriteConsole"/>, or <see cref="GetConsoleScreenBufferInfo"/> functions).
        /// </para>
        /// <para>
        /// The standard handles of a process may be redirected by a call
        /// to <see cref="SetStdHandle"/>, in which case <c>GetStdHandle</c> returns the redirected
        /// handle. If the standard handles have been redirected, you can specify
        /// the <c>CONIN$</c> value in a call to the <see cref="CreateFile"/> function to get a handle
        /// to a console's input buffer. Similarly, you can specify the <c>CONOUT$</c> value
        /// to get a handle to a console's active screen buffer.
        /// </para>
        /// <para>
        /// The standard handles of a process on entry of the main method are dictated by
        /// the configuration of the <c>/SUBSYSTEM</c> flag passed to the linker when the application
        /// was built. Specifying <c>/SUBSYSTEM:CONSOLE</c> requests that the operating system fill
        /// the handles with a console session on startup, if the parent didn't already fill
        /// the standard handle table by inheritance. On the contrary, <c>/SUBSYSTEM:WINDOWS</c>
        /// implies that the application does not need a console and will likely not be making
        /// use of the standard handles. More information on handle inheritance can be found in
        /// the documentation for <c>STARTF_USESTDHANDLES</c>.
        /// </para>
        /// <para>
        /// Some applications operate outside the boundaries of their
        /// declared subsystem; for instance, a <c>/SUBSYSTEM:WINDOWS</c>
        /// application might check/use standard handles for logging
        /// or debugging purposes but operate normally with a graphical
        /// user interface. These applications will need to carefully probe the state
        /// of standard handles on startup and make use of <see cref="AttachConsole"/>, <see cref="AllocConsole"/>,
        /// and <see cref="FreeConsole"/> to add/remove a console if desired.
        /// </para>
        /// <para>
        /// Some applications may also vary their behavior on the type of inherited handle.
        /// Disambiguating the type between console, pipe, file, and others can be performed with <see cref="GetFileType"/>.
        /// </para>
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HANDLE GetStdHandle(
            [In] DWORD nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        unsafe public static extern SafeFileHandle CreateFile(
            [In] WCHAR* lpFileName,
            [In] DWORD dwDesiredAccess,
            [In] DWORD dwShareMode,
            [In, Optional] SECURITY_ATTRIBUTES* lpSecurityAttributes,
            [In] DWORD dwCreationDisposition,
            [In] DWORD dwFlagsAndAttributes,
            [In, Optional] HANDLE hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleOutput(
            [In] HANDLE hConsoleOutput,
            [In] CharInfo[] lpBuffer,
            [In] Coord dwBufferSize,
            [In] Coord dwBufferCoord,
            [In, Out] ref SmallRect lpWriteRegion);

        /// <summary>
        /// Writes character and color attribute data to a specified
        /// rectangular block of character cells in a console screen buffer.
        /// The data to be written is taken from a correspondingly sized rectangular
        /// block at a specified location in the source buffer.
        /// </summary>
        /// <param name="hConsoleOutput">
        /// A handle to the console screen buffer. The handle must have the <c>GENERIC_WRITE</c> access right.
        /// For more information, see 
        /// <see href="https://learn.microsoft.com/en-us/windows/console/console-buffer-security-and-access-rights">Console Buffer Security and Access Rights</see>.
        /// </param>
        /// <param name="lpBuffer">
        /// The data to be written to the console screen buffer.
        /// This pointer is treated as the origin of a two-dimensional
        /// array of <see cref="CharInfo"/> structures whose size is specified by the <paramref name="dwBufferSize"/> parameter.
        /// </param>
        /// <param name="dwBufferSize">
        /// The size of the buffer pointed to by the <paramref name="lpBuffer"/> parameter, in character cells.
        /// The X member of the <see cref="Coord"/> structure is the number of columns;
        /// the Y member is the number of rows.
        /// </param>
        /// <param name="dwBufferCoord">
        /// The coordinates of the upper-left cell in the buffer pointed to by
        /// the <paramref name="lpBuffer"/> parameter. The X member of the <see cref="Coord"/> structure is
        /// the column, and the Y member is the row.
        /// </param>
        /// <param name="lpWriteRegion">
        /// A pointer to a <see cref="SmallRect"/> structure. On input, the structure members
        /// specify the upper-left and lower-right coordinates of
        /// the console screen buffer rectangle to write to. On output,
        /// the structure members specify the actual rectangle that was used.
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
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleOutputW(
            SafeFileHandle hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

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

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
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
