using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Win32
{
    public static class Kernel32
    {
        public const uint STD_INPUT_HANDLE = unchecked((uint)-10);
        public const uint STD_OUTPUT_HANDLE = unchecked((uint)-11);
        public const uint STD_ERROR_HANDLE = unchecked((uint)-12);

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
