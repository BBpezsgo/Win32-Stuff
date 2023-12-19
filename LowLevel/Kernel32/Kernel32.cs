using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Win32.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static class Kernel32
    {
        public static readonly HANDLE INVALID_HANDLE_VALUE = (HANDLE)(-1);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL GetExitCodeThread(
          [In] HANDLE hThread,
          [Out] DWORD* lpExitCode
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL GetExitCodeProcess(
          [In] HANDLE hProcess,
          [Out] DWORD* lpExitCode
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe HRESULT SetThreadDescription(
          [In] HANDLE hThread,
          [In] WCHAR* lpThreadDescription
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HANDLE OpenThread(
          [In] DWORD dwDesiredAccess,
          [In] BOOL bInheritHandle,
          [In] DWORD dwThreadId
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe HANDLE CreateThread(
          [In, Optional] SecurityAttributes* lpThreadAttributes,
          [In] SIZE_T dwStackSize,
          [In] delegate*<void*, DWORD> lpStartAddress,
          [In, Optional] void* lpParameter,
          [In] DWORD dwCreationFlags,
          [Out, Optional] DWORD* lpThreadId
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Toolhelp32ReadProcessMemory(
          [In] DWORD th32ProcessID,
          [In] void* lpBaseAddress,
          [Out] void* lpBuffer,
          [In] SIZE_T cbRead,
          [Out] SIZE_T* lpNumberOfBytesRead
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Thread32Next(
          [In] HANDLE hSnapshot,
          [Out] ThreadEntry* lpte
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Thread32First(
          [In] HANDLE hSnapshot,
          [In, Out] ThreadEntry* lpte
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Heap32Next(
          [Out] HeapEntry* lphe
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Heap32ListNext(
          [In] HANDLE hSnapshot,
          [Out] HeapList* lphl
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Heap32ListFirst(
          [In] HANDLE hSnapshot,
          [In, Out] HeapList* lphl
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Heap32First(
          [In, Out] HeapEntry* lphe,
          [In] DWORD th32ProcessID,
          [In] ULONG_PTR th32HeapID
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Module32NextW(
          [In] HANDLE hSnapshot,
          [Out] ModuleEntry* lpme
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Module32FirstW(
          [In] HANDLE hSnapshot,
          [In, Out] ModuleEntry* lpme
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL GetThreadIdealProcessorEx(
          [In] HANDLE hThread,
          [Out] ProcessorNumber* lpIdealProcessor
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL GetThreadGroupAffinity(
          [In] HANDLE hThread,
          [Out] GroupAffinity* GroupAffinity
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern DWORD GetThreadId(
          [In] HANDLE Thread
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern DWORD GetProcessIdOfThread(
          [In] HANDLE Thread
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe HRESULT GetThreadDescription(
          [In] HANDLE hThread,
          [Out] WCHAR** ppszThreadDescription
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Process32NextW(
          [In] HANDLE hSnapshot,
          [Out] ProcessEntry* lppe
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL Process32FirstW(
          [In] HANDLE hSnapshot,
          [In, Out] ProcessEntry* lppe
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HANDLE CreateToolhelp32Snapshot(
          [In] TH32CS dwFlags,
          [In] DWORD th32ProcessID
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe delegate*<INT_PTR> GetProcAddress(
          [In] HMODULE hModule,
          [In] CHAR* lpProcName
        );

        [DllImport("Psapi.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe DWORD GetDeviceDriverFileNameW(
          [In] void* ImageBase,
          [Out] WCHAR* lpFilename,
          [In] DWORD nSize
        );

        [DllImport("Psapi.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe DWORD GetDeviceDriverBaseNameW(
          [In] void* ImageBase,
               WCHAR* lpFilename,
          [In] DWORD nSize
        );

        [DllImport("Psapi.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL EnumDeviceDrivers(
          [Out] void** lpImageBase,
          [In] DWORD cb,
          [Out] DWORD* lpcbNeeded
        );

        [DllImport("Psapi.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL GetModuleInformation(
          [In] HANDLE hProcess,
          [In] HMODULE hModule,
          [Out] ModuleInfo* lpmodinfo,
          [In] DWORD cb
        );

        [DllImport("Psapi.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe DWORD GetModuleBaseNameW(
          [In] HANDLE hProcess,
          [In, Optional] HMODULE hModule,
          [Out] WCHAR* lpBaseName,
          [In] DWORD nSize
        );

        [DllImport("Psapi.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe DWORD GetModuleFileNameExW(
          [In] HANDLE hProcess,
          [In, Optional] HMODULE hModule,
          [Out] WCHAR* lpFilename,
          [In] DWORD nSize
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL CreateProcessW(
          [In, Optional] WCHAR* lpApplicationName,
          [In, Out, Optional] WCHAR* lpCommandLine,
          [In, Optional] SecurityAttributes* lpProcessAttributes,
          [In, Optional] SecurityAttributes* lpThreadAttributes,
          [In] BOOL bInheritHandles,
          [In] DWORD dwCreationFlags,
          [In, Optional] void* lpEnvironment,
          [In, Optional] WCHAR* lpCurrentDirectory,
          [In] STARTUPINFOW* lpStartupInfo,
          [Out] ProcessInformation* lpProcessInformation
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL QueryFullProcessImageNameW(
          [In] HANDLE hProcess,
          [In] DWORD dwFlags,
          [Out] WCHAR* lpExeName,
          [In, Out] DWORD* lpdwSize
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern DWORD GetProcessId(
          [In] HANDLE Process
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL GetProcessHandleCount(
          [In] HANDLE hProcess,
          [In, Out] DWORD* pdwHandleCount
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern DWORD GetProcessVersion(
          [In] DWORD ProcessId
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL IsImmersiveProcess(
          [In] HANDLE hProcess
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL TerminateThread(
          [In, Out] HANDLE hThread,
          [In] DWORD dwExitCode
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL TerminateProcess(
          [In] HANDLE hProcess,
          [In] UINT uExitCode
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern DWORD GetCurrentProcessId();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HANDLE GetCurrentProcess();

        [DllImport("Psapi.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL EnumProcessModules(
          [In] HANDLE hProcess,
          [Out] HMODULE* lphModule,
          [In] DWORD cb,
          [Out] DWORD* lpcbNeeded
        );

        [DllImport("Psapi.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL EnumProcesses(
          [Out] DWORD* lpidProcess,
          [In] DWORD cb,
          [Out] DWORD* lpcbNeeded
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL VirtualFree(
          [In] void* lpAddress,
          [In] SIZE_T dwSize,
          [In] DWORD dwFreeType
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe void* VirtualAlloc(
          [In, Optional] void* lpAddress,
          [In] SIZE_T dwSize,
          [In] DWORD flAllocationType,
          [In] DWORD flProtect
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL VirtualProtect(
          [In] void* lpAddress,
          [In] SIZE_T dwSize,
          [In] DWORD flNewProtect,
          [Out] DWORD* lpflOldProtect
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL VirtualProtectEx(
          [In] HANDLE hProcess,
          [In] void* lpAddress,
          [In] SIZE_T dwSize,
          [In] DWORD flNewProtect,
          [Out] DWORD* lpflOldProtect
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL VirtualFreeEx(
          [In] HANDLE hProcess,
          [In] void* lpAddress,
          [In] SIZE_T dwSize,
          [In] DWORD dwFreeType
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern DWORD WaitForSingleObject(
          [In] HANDLE hHandle,
          [In] DWORD dwMilliseconds
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe HANDLE CreateRemoteThread(
          [In] HANDLE hProcess,
          [In] SecurityAttributes* lpThreadAttributes,
          [In] SIZE_T dwStackSize,
          [In] delegate*<void*, DWORD>* lpStartAddress,
          [In] void* lpParameter,
          [In] DWORD dwCreationFlags,
          [Out] DWORD* lpThreadId
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe HANDLE CreateRemoteThreadEx(
          [In] HANDLE hProcess,
          [In, Optional] SecurityAttributes* lpThreadAttributes,
          [In] SIZE_T dwStackSize,
          [In] delegate*<void*, DWORD> lpStartAddress,
          [In, Optional] void* lpParameter,
          [In] DWORD dwCreationFlags,
          [In, Optional] void* lpAttributeList,
          [Out, Optional] DWORD* lpThreadId
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL WriteProcessMemory(
          [In] HANDLE hProcess,
          [In] void* lpBaseAddress,
          [In] void* lpBuffer,
          [In] SIZE_T nSize,
          [Out] SIZE_T* lpNumberOfBytesWritten
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe void* VirtualAllocEx(
          [In] HANDLE hProcess,
          [In, Optional] void* lpAddress,
          [In] SIZE_T dwSize,
          [In] DWORD flAllocationType,
          [In] DWORD flProtect
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern HANDLE OpenProcess(
          [In] DWORD dwDesiredAccess,
          [In] BOOL bInheritHandle,
          [In] DWORD dwProcessId
        );

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
        public static extern unsafe BOOL SetConsoleWindowInfo(
          [In] HANDLE hConsoleOutput,
          [In] BOOL bAbsolute,
          [In] SMALL_RECT* lpConsoleWindow
        );

        /// <summary>
        /// Retrieves information about the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">
        /// A handle to the console screen buffer.
        /// The handle must have the <c>GENERIC_READ</c> access right.
        /// </param>
        /// <param name="lpConsoleScreenBufferInfo">
        /// A pointer to a <see cref="ConsoleScreenBufferInfo"/> structure
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
        /// <see cref="ConsoleScreenBufferInfo"/> structure can be modified
        /// and then passed to the <see cref="SetConsoleWindowInfo"/> function to
        /// scroll the console screen buffer in the window, to change
        /// the size of the window, or both.
        /// </para>
        /// <para>
        /// All coordinates returned in the <see cref="ConsoleScreenBufferInfo"/> structure are in
        /// character-cell coordinates, where the origin (0, 0) is at the upper-left
        /// corner of the console screen buffer.
        /// </para>
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL GetConsoleScreenBufferInfo(
           [In] HANDLE hConsoleOutput,
           [Out] ConsoleScreenBufferInfo* lpConsoleScreenBufferInfo
         );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe HGLOBAL GlobalReAlloc(
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
        public static extern unsafe BOOL WriteFile(
          [In] HANDLE hFile,
          [In] void* lpBuffer,
          [In] DWORD nNumberOfBytesToWrite,
          [Out, Optional] DWORD* lpNumberOfBytesWritten,
          [In, Out, Optional] Overlapped* lpOverlapped
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
        public static extern unsafe HANDLE CreateFileW(
          [In] WCHAR* lpFileName,
          [In] DWORD dwDesiredAccess,
          [In] DWORD dwShareMode,
          [In, Optional] SecurityAttributes* lpSecurityAttributes,
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
        public static extern unsafe void* GlobalLock(
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
          [Out] out ConsoleSelectionInfo lpConsoleSelectionInfo
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
          [Out] out ConsoleFontInfo lpConsoleCurrentFont
        );

        /// <summary>
        /// Retrieves extended information about the current console font.
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL GetCurrentConsoleFontEx(
          [In] HANDLE hConsoleOutput,
          [In] BOOL bMaximumWindow,
          /*[Out]*/ ref ConsoleFontInfoEx lpConsoleCurrentFontEx
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
        /// A pointer to a <see cref="ConsoleFontInfoEx"/> structure that contains the font information.
        /// </param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern BOOL SetCurrentConsoleFontEx(
          [In] HANDLE hConsoleOutput,
          [In] BOOL bMaximumWindow,
          [In] ref ConsoleFontInfoEx lpConsoleCurrentFontEx
        );

        /// <summary>
        /// Retrieves the current system date and time in Coordinated Universal Time (UTC) format.
        /// </summary>
        /// <param name="lpSystemTime">
        /// A pointer to a <see cref="SystemTime"/> structure to receive the current
        /// system date and time. The <paramref name="lpSystemTime"/> parameter must not
        /// be <c>NULL</c>. Using <c>NULL</c> will result in an access violation.
        /// </param>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe void GetSystemTime(
          [Out] SystemTime* lpSystemTime
        );

        /// <summary>
        /// Retrieves the current system date and time.
        /// The information is in Coordinated Universal Time (UTC) format.
        /// </summary>
        /// <param name="lpSystemTimeAsFileTime">
        /// A pointer to a <see cref="FileTime"/> structure to receive the current
        /// system date and time in UTC format.
        /// </param>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe void GetSystemTimeAsFileTime(
          [Out] FileTime* lpSystemTimeAsFileTime
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

        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe SafeFileHandle CreateFileSafe(
            [In] WCHAR* lpFileName,
            [In] DWORD dwDesiredAccess,
            [In] DWORD dwShareMode,
            [In, Optional] SecurityAttributes* lpSecurityAttributes,
            [In] DWORD dwCreationDisposition,
            [In] DWORD dwFlagsAndAttributes,
            [In, Optional] HANDLE hTemplateFile);

        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe HANDLE CreateFile(
            [In] WCHAR* lpFileName,
            [In] DWORD dwDesiredAccess,
            [In] DWORD dwShareMode,
            [In, Optional] SecurityAttributes* lpSecurityAttributes,
            [In] CreateFileFlags dwCreationDisposition,
            [In] DWORD dwFlagsAndAttributes,
            [In, Optional] HANDLE hTemplateFile);

        /// <include file="Docs/Kernel32/WriteConsoleOutput.xml" path="/*"/>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleOutput(
            [In] HANDLE hConsoleOutput,
            [In] ConsoleChar[] lpBuffer,
            [In] Coord dwBufferSize,
            [In] Coord dwBufferCoord,
            [In, Out] ref SmallRect lpWriteRegion);

        /// <include file="Docs/Kernel32/WriteConsoleOutput.xml" path="/*"/>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern BOOL WriteConsoleOutputW(
            SafeFileHandle hConsoleOutput,
            [In] ConsoleChar[] lpBuffer,
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
        public static extern unsafe DWORD FormatMessage(
            [In] DWORD dwFlags,
            [In, Optional] IntPtr lpSource,
            [In] DWORD dwMessageId,
            [In] DWORD dwLanguageId,
            [Out] WCHAR* lpBuffer,
            [In] DWORD nSize,
            [In, Optional] IntPtr Arguments);
    }
}
