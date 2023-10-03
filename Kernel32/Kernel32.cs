using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Win32
{
    public static class Kernel32
    {
        public static readonly HANDLE INVALID_HANDLE_VALUE = (HANDLE)(-1);

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

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        unsafe public static extern SafeFileHandle CreateFile(
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
