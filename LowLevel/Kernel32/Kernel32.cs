using Microsoft.Win32.SafeHandles;

namespace Win32;

[SupportedOSPlatform("windows")]
public static partial class Kernel32
{
    public static readonly HANDLE InvalidHandle = -1;

    [LibraryImport("kernel32.dll")]
    public static unsafe partial void* GetProcAddress(
      HMODULE hModule,
      WCHAR* lpProcName
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL FreeLibrary(
      HMODULE hLibModule
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HMODULE LoadLibraryExW(
      WCHAR* lpLibFileName,
      HANDLE hFile,
      DWORD dwFlags
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HMODULE LoadLibraryW(
      WCHAR* lpLibFileName
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL TlsSetValue(
      DWORD dwTlsIndex,
      [Optional] void* lpTlsValue
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial void* TlsGetValue(
      DWORD dwTlsIndex
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL TlsFree(
      DWORD dwTlsIndex
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD TlsAlloc();

    [LibraryImport("kernel32.dll")]
    public static partial DWORD GetCurrentThreadId();

    [LibraryImport("kernel32.dll")]
    public static partial HANDLE GetCurrentThread();

    [LibraryImport("kernel32.dll")]
    public static partial BOOL SwitchToThread();

    [LibraryImport("kernel32.dll")]
    public static partial DWORD SuspendThread(
      HANDLE hThread
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD SleepEx(
      DWORD dwMilliseconds,
      BOOL bAlertable
    );

    [LibraryImport("kernel32.dll")]
    public static partial void Sleep(
      DWORD dwMilliseconds
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL SetThreadPriorityBoost(
      HANDLE hThread,
      BOOL bDisablePriorityBoost
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL SetThreadPriority(
      HANDLE hThread,
      int nPriority
    );

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern unsafe BOOL SetThreadInformation(
      [In] HANDLE hThread,
      [In] ThreadInformationClass ThreadInformationClass,
           void* ThreadInformation,
      [In] DWORD ThreadInformationSize
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL SetThreadIdealProcessorEx(
      HANDLE hThread,
      ProcessorNumber* lpIdealProcessor,
      [Optional] out ProcessorNumber lpPreviousIdealProcessor
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD SetThreadIdealProcessor(
      HANDLE hThread,
      DWORD dwIdealProcessor
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL SetThreadGroupAffinity(
      HANDLE hThread,
      GroupAffinity* GroupAffinity,
      [Optional] out GroupAffinity PreviousGroupAffinity
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL GetProcessAffinityMask(
      HANDLE hProcess,
      out DWORD_PTR lpProcessAffinityMask,
      out DWORD_PTR lpSystemAffinityMask
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD_PTR SetThreadAffinityMask(
      HANDLE hThread,
      DWORD_PTR dwThreadAffinityMask
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD ResumeThread(
      HANDLE hThread
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL GetThreadTimes(
      HANDLE hThread,
      out FileTime lpCreationTime,
      out FileTime lpExitTime,
      out FileTime lpKernelTime,
      out FileTime lpUserTime
    );

    [LibraryImport("kernel32.dll")]
    public static partial int GetThreadPriority(
      HANDLE hThread
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL GetThreadPriorityBoost(
      HANDLE hThread,
      out BOOL pDisablePriorityBoost
    );

    [LibraryImport("kernel32.dll")]
    public static partial void ExitThread(
      DWORD dwExitCode
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HANDLE CreateFileMappingA(
      HANDLE hFile,
      [Optional] SecurityAttributes* lpFileMappingAttributes,
      DWORD flProtect,
      DWORD dwMaximumSizeHigh,
      DWORD dwMaximumSizeLow,
      [Optional] CHAR* lpName
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL UpdateResourceW(
      HANDLE hUpdate,
      WCHAR* lpType,
      WCHAR* lpName,
      WORD wLanguage,
      [Optional] void* lpData,
      DWORD cb
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HANDLE CreateFileMappingW(
      HANDLE hFile,
      [Optional] SecurityAttributes* lpFileMappingAttributes,
      DWORD flProtect,
      DWORD dwMaximumSizeHigh,
      DWORD dwMaximumSizeLow,
      [Optional] WCHAR* lpName
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial DWORD GetFileType(
      HANDLE hFile
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial void* VirtualAllocExNuma(
      HANDLE hProcess,
      [Optional] void* lpAddress,
      SIZE_T dwSize,
      DWORD flAllocationType,
      DWORD flProtect,
      DWORD nndPreferred
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL ReadFile(
      HANDLE hFile,
      void* lpBuffer,
      DWORD nNumberOfBytesToRead,
      [Optional] out DWORD lpNumberOfBytesRead,
      [Optional] ref Overlapped lpOverlapped
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL FreeConsole();

    [LibraryImport("kernel32.dll")]
    public static partial BOOL AttachConsole(
      DWORD dwProcessId
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL AllocConsole();

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL ReadConsole(
      HANDLE hConsoleInput,
      void* lpBuffer,
      DWORD nNumberOfCharsToRead,
      out DWORD lpNumberOfCharsRead,
      [Optional] void* pInputControl
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL SetStdHandle(
      DWORD nStdHandle,
      HANDLE hHandle
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL WriteConsole(
      HANDLE hConsoleOutput,
      void* lpBuffer,
      DWORD nNumberOfCharsToWrite,
      [Optional] out DWORD lpNumberOfCharsWritten,
      void* lpReserved = null
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HANDLE CreateFileTransactedW(
      WCHAR* lpFileName,
      DWORD dwDesiredAccess,
      DWORD dwShareMode,
      [Optional] SecurityAttributes* lpSecurityAttributes,
      DWORD dwCreationDisposition,
      DWORD dwFlagsAndAttributes,
      [Optional] HANDLE hTemplateFile,
      HANDLE hTransaction,
      [Optional] ushort* pusMiniVersion,
      void* lpExtendedParameter
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL WriteFileEx(
      HANDLE hFile,
      [Optional] void* lpBuffer,
      DWORD nNumberOfBytesToWrite,
      ref Overlapped lpOverlapped,
      void* lpCompletionRoutine // OVERLAPPED_COMPLETION_ROUTINE*
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL GetExitCodeThread(
      HANDLE hThread,
      out DWORD lpExitCode
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL GetExitCodeProcess(
      HANDLE hProcess,
      out DWORD lpExitCode
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HRESULT SetThreadDescription(
      HANDLE hThread,
      WCHAR* lpThreadDescription
    );

    [LibraryImport("kernel32.dll")]
    public static partial HANDLE OpenThread(
      DWORD dwDesiredAccess,
      BOOL bInheritHandle,
      DWORD dwThreadId
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HANDLE CreateThread(
      [Optional] SecurityAttributes* lpThreadAttributes,
      SIZE_T dwStackSize,
      delegate*<void*, DWORD> lpStartAddress,
      [Optional] void* lpParameter,
      DWORD dwCreationFlags,
      [Optional] out DWORD lpThreadId
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Toolhelp32ReadProcessMemory(
      DWORD th32ProcessID,
      void* lpBaseAddress,
      void* lpBuffer,
      SIZE_T cbRead,
      out SIZE_T lpNumberOfBytesRead
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Thread32Next(
      HANDLE hSnapshot,
      out ThreadEntry lpte
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Thread32First(
      HANDLE hSnapshot,
      ref ThreadEntry lpte
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Heap32Next(
      out HeapEntry lphe
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Heap32ListNext(
      HANDLE hSnapshot,
      out HeapList lphl
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Heap32ListFirst(
      HANDLE hSnapshot,
      ref HeapList lphl
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Heap32First(
      ref HeapEntry lphe,
      DWORD th32ProcessID,
      ULONG_PTR th32HeapID
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Module32NextW(
      HANDLE hSnapshot,
      out ModuleEntry lpme
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Module32FirstW(
      HANDLE hSnapshot,
      ref ModuleEntry lpme
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL GetThreadIdealProcessorEx(
      HANDLE hThread,
      out ProcessorNumber lpIdealProcessor
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL GetThreadGroupAffinity(
      HANDLE hThread,
      out GroupAffinity GroupAffinity
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD GetThreadId(
      HANDLE Thread
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD GetProcessIdOfThread(
      HANDLE Thread
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HRESULT GetThreadDescription(
      HANDLE hThread,
      out WCHAR* ppszThreadDescription
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Process32NextW(
      HANDLE hSnapshot,
      out ProcessEntry lppe
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL Process32FirstW(
      HANDLE hSnapshot,
      ref ProcessEntry lppe
    );

    [LibraryImport("kernel32.dll")]
    public static partial HANDLE CreateToolhelp32Snapshot(
      TH32CS dwFlags,
      DWORD th32ProcessID
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial delegate*<INT_PTR> GetProcAddress(
      HMODULE hModule,
      CHAR* lpProcName
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

    [LibraryImport("Psapi.dll")]
    public static unsafe partial BOOL GetModuleInformation(
      HANDLE hProcess,
      HMODULE hModule,
      out ModuleInfo lpmodinfo,
      DWORD cb
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

    [LibraryImport("kernel32.dll")]
    public static partial DWORD GetProcessId(
      HANDLE Process
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL GetProcessHandleCount(
      HANDLE hProcess,
      ref DWORD pdwHandleCount
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD GetProcessVersion(
      DWORD ProcessId
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL IsImmersiveProcess(
      HANDLE hProcess
    );

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern BOOL TerminateThread(
      [In, Out] HANDLE hThread,
      [In] DWORD dwExitCode
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL TerminateProcess(
      HANDLE hProcess,
      UINT uExitCode
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD GetCurrentProcessId();

    [LibraryImport("kernel32.dll")]
    public static partial HANDLE GetCurrentProcess();

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

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL VirtualFree(
      void* lpAddress,
      SIZE_T dwSize,
      DWORD dwFreeType
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial void* VirtualAlloc(
      [Optional] void* lpAddress,
      SIZE_T dwSize,
      DWORD flAllocationType,
      DWORD flProtect
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL VirtualProtect(
      void* lpAddress,
      SIZE_T dwSize,
      DWORD flNewProtect,
      out DWORD lpflOldProtect
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL VirtualProtectEx(
      HANDLE hProcess,
      void* lpAddress,
      SIZE_T dwSize,
      DWORD flNewProtect,
      out DWORD lpflOldProtect
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL VirtualFreeEx(
      HANDLE hProcess,
      void* lpAddress,
      SIZE_T dwSize,
      DWORD dwFreeType
    );

    [LibraryImport("kernel32.dll")]
    public static partial DWORD WaitForSingleObject(
      HANDLE hHandle,
      DWORD dwMilliseconds
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HANDLE CreateRemoteThread(
      HANDLE hProcess,
      SecurityAttributes* lpThreadAttributes,
      SIZE_T dwStackSize,
      delegate*<void*, DWORD>* lpStartAddress,
      void* lpParameter,
      DWORD dwCreationFlags,
      out DWORD lpThreadId
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HANDLE CreateRemoteThreadEx(
      HANDLE hProcess,
      [Optional] SecurityAttributes* lpThreadAttributes,
      SIZE_T dwStackSize,
      delegate*<void*, DWORD> lpStartAddress,
      [Optional] void* lpParameter,
      DWORD dwCreationFlags,
      [Optional] void* lpAttributeList,
      [Optional] out DWORD lpThreadId
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL WriteProcessMemory(
      HANDLE hProcess,
      void* lpBaseAddress,
      void* lpBuffer,
      SIZE_T nSize,
      out SIZE_T lpNumberOfBytesWritten
    );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial void* VirtualAllocEx(
      HANDLE hProcess,
      [Optional] void* lpAddress,
      SIZE_T dwSize,
      DWORD flAllocationType,
      DWORD flProtect
    );

    [LibraryImport("kernel32.dll")]
    public static partial HANDLE OpenProcess(
      DWORD dwDesiredAccess,
      BOOL bInheritHandle,
      DWORD dwProcessId
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
    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL SetConsoleWindowInfo(
      HANDLE hConsoleOutput,
      BOOL bAbsolute,
      SMALL_RECT* lpConsoleWindow
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
    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL GetConsoleScreenBufferInfo(
       HANDLE hConsoleOutput,
       out Console.ConsoleScreenBufferInfo lpConsoleScreenBufferInfo
     );

    [LibraryImport("kernel32.dll")]
    public static unsafe partial HGLOBAL GlobalReAlloc(
      HGLOBAL hMem,
      SIZE_T dwBytes,
      UINT uFlags
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
    [LibraryImport("kernel32.dll")]
    public static unsafe partial BOOL WriteFile(
      HANDLE hFile,
      void* lpBuffer,
      DWORD nNumberOfBytesToWrite,
      [Optional] out DWORD lpNumberOfBytesWritten,
      [Optional] ref Overlapped lpOverlapped
    );

    /// <summary>
    /// Closes an open object handle.
    /// </summary>
    /// <param name="hObject">
    /// A valid handle to an open object.
    /// </param>
    [LibraryImport("kernel32.dll")]
    public static partial BOOL CloseHandle(
      HANDLE hObject
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
    [LibraryImport("kernel32.dll")]
    public static partial HGLOBAL GlobalFree(
      HGLOBAL hMem
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
    [LibraryImport("kernel32.dll")]
    public static partial BOOL GlobalUnlock(
      HGLOBAL hMem
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
    [LibraryImport("kernel32.dll")]
    public static unsafe partial HANDLE CreateFileW(
      WCHAR* lpFileName,
      DWORD dwDesiredAccess,
      DWORD dwShareMode,
      [Optional] SecurityAttributes* lpSecurityAttributes,
      DWORD dwCreationDisposition,
      DWORD dwFlagsAndAttributes,
      [Optional] HANDLE hTemplateFile
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
    [LibraryImport("kernel32.dll")]
    public static unsafe partial void* GlobalLock(
      HGLOBAL hMem
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
    [LibraryImport("kernel32.dll")]
    public static partial HGLOBAL GlobalAlloc(
      UINT uFlags,
      SIZE_T dwBytes
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
    [LibraryImport("kernel32.dll")]
    public static partial void SetLastError(
      DWORD dwErrCode
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL SetConsoleScreenBufferSize(
      HANDLE hConsoleOutput,
      COORD dwSize
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL GetConsoleSelectionInfo(
      out Console.ConsoleSelectionInfo lpConsoleSelectionInfo
    );

    [LibraryImport("kernel32.dll")]
    public static partial HWND GetConsoleWindow();

    [LibraryImport("kernel32.dll")]
    public static partial BOOL GetNumberOfConsoleMouseButtons(
      out DWORD lpNumberOfMouseButtons
    );

    [LibraryImport("kernel32.dll")]
    public static partial COORD GetLargestConsoleWindowSize(
      HANDLE hConsoleOutput
    );

    [LibraryImport("kernel32.dll")]
    public static partial BOOL GetCurrentConsoleFont(
      HANDLE hConsoleOutput,
      BOOL bMaximumWindow,
      out Console.ConsoleFontInfo lpConsoleCurrentFont
    );

    /// <summary>
    /// Retrieves extended information about the current console font.
    /// </summary>
    [LibraryImport("kernel32.dll")]
    public static partial BOOL GetCurrentConsoleFontEx(
      HANDLE hConsoleOutput,
      BOOL bMaximumWindow,
      ref Console.ConsoleFontInfoEx lpConsoleCurrentFontEx
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
    [LibraryImport("kernel32.dll")]
    public static partial BOOL SetCurrentConsoleFontEx(
      HANDLE hConsoleOutput,
      BOOL bMaximumWindow,
      in Console.ConsoleFontInfoEx lpConsoleCurrentFontEx
    );

    /// <summary>
    /// Retrieves the current system date and time in Coordinated Universal Time (UTC) format.
    /// </summary>
    /// <param name="lpSystemTime">
    /// A pointer to a <see cref="SystemTime"/> structure to receive the current
    /// system date and time. The <paramref name="lpSystemTime"/> parameter must not
    /// be <c>NULL</c>. Using <c>NULL</c> will result in an access violation.
    /// </param>
    [LibraryImport("kernel32.dll")]
    public static unsafe partial void GetSystemTime(
      out SystemTime lpSystemTime
    );

    /// <summary>
    /// Retrieves the current system date and time.
    /// The information is in Coordinated Universal Time (UTC) format.
    /// </summary>
    /// <param name="lpSystemTimeAsFileTime">
    /// A pointer to a <see cref="FileTime"/> structure to receive the current
    /// system date and time in UTC format.
    /// </param>
    [LibraryImport("kernel32.dll")]
    public static unsafe partial void GetSystemTimeAsFileTime(
      out FileTime lpSystemTimeAsFileTime
    );

    [LibraryImport("kernel32.dll")]
    public static partial HLOCAL LocalFree(
      HLOCAL hMem
    );

    /// <include file="Docs/Kernel32/ReadConsoleInput.xml" path="/*"/>
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern BOOL ReadConsoleInput(
        [In] HANDLE hConsoleInput,
        [Out] Console.InputEvent[] lpBuffer,
        [In] DWORD nLength,
        ref DWORD lpNumberOfEventsRead);

    /// <include file="Docs/Kernel32/WriteConsoleInput.xml" path="/*"/>
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern BOOL WriteConsoleInput(
        [In] HANDLE hConsoleInput,
        [In] Console.InputEvent[] lpBuffer,
        [In] DWORD nLength,
        ref DWORD lpNumberOfEventsWritten);

    /// <include file="Docs/Kernel32/GetConsoleCP.xml" path="/*"/>
    [LibraryImport("Kernel32.dll")]
    public static partial UINT GetConsoleCP();

    /// <include file="Docs/Kernel32/GetConsoleOutputCP.xml" path="/*"/>
    [LibraryImport("Kernel32.dll")]
    public static partial UINT GetConsoleOutputCP();

    /// <include file="Docs/Kernel32/SetConsoleCP.xml" path="/*"/>
    [LibraryImport("Kernel32.dll")]
    public static partial BOOL SetConsoleCP(
      UINT wCodePageID
    );

    /// <include file="Docs/Kernel32/SetConsoleOutputCP.xml" path="/*"/>
    [LibraryImport("Kernel32.dll")]
    public static partial BOOL SetConsoleOutputCP(
        UINT wCodePageID);

    /// <include file="Docs/Kernel32/GetStdHandle.xml" path="/*"/>
    [LibraryImport("kernel32.dll")]
    public static partial HANDLE GetStdHandle(
        DWORD nStdHandle);

    [LibraryImport("kernel32.dll", EntryPoint = "CreateFileW", SetLastError = true)]
    public static unsafe partial SafeFileHandle CreateFileSafe(
        WCHAR* lpFileName,
        DWORD dwDesiredAccess,
        DWORD dwShareMode,
        [Optional] SecurityAttributes* lpSecurityAttributes,
        DWORD dwCreationDisposition,
        DWORD dwFlagsAndAttributes,
        [Optional] HANDLE hTemplateFile);

    [LibraryImport("kernel32.dll", EntryPoint = "CreateFileW", SetLastError = true)]
    public static unsafe partial HANDLE CreateFile(
        WCHAR* lpFileName,
        DWORD dwDesiredAccess,
        DWORD dwShareMode,
        [Optional] SecurityAttributes* lpSecurityAttributes,
        CreateFileFlags dwCreationDisposition,
        DWORD dwFlagsAndAttributes,
        [Optional] HANDLE hTemplateFile);

    /// <include file="Docs/Kernel32/WriteConsoleOutput.xml" path="/*"/>
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern BOOL WriteConsoleOutput(
        [In] HANDLE hConsoleOutput,
        [In] Console.ConsoleChar[] lpBuffer,
        [In] Console.SmallSize dwBufferSize,
        [In] COORD dwBufferCoord,
        [In, Out] ref SMALL_RECT lpWriteRegion);

    /// <include file="Docs/Kernel32/WriteConsoleOutput.xml" path="/*"/>
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern BOOL WriteConsoleOutputW(
        SafeFileHandle hConsoleOutput,
        [In] Console.ConsoleChar[] lpBuffer,
        [In] Console.SmallSize dwBufferSize,
        [In] COORD dwBufferCoord,
        [In, Out] ref SMALL_RECT lpWriteRegion);

    [LibraryImport("kernel32.dll")]
    public static partial BOOL GetConsoleMode(
        HANDLE hConsoleInput,
        out DWORD lpMode);

    [LibraryImport("kernel32.dll")]
    public static partial BOOL SetConsoleMode(
        HANDLE hConsoleInput,
        DWORD dwMode);

    /// <include file="Docs/Kernel32/GetLastError.xml" path="/*"/>
    [LibraryImport("kernel32.dll")]
    public static partial DWORD GetLastError();

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
