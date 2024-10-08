﻿using Win32.Console;

namespace Win32;

[SupportedOSPlatform("windows")]
public static class Kernel32
{
    public static readonly HANDLE InvalidHandle = -1;

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe void* GetProcAddress(
      HMODULE hModule,
      WCHAR* lpProcName
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL FreeLibrary(
      HMODULE hLibModule
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HMODULE LoadLibraryExW(
      WCHAR* lpLibFileName,
      HANDLE hFile,
      DWORD dwFlags
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HMODULE LoadLibraryW(
      WCHAR* lpLibFileName
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL TlsSetValue(
      [In] DWORD dwTlsIndex,
      [In, Optional] void* lpTlsValue
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe void* TlsGetValue(
      [In] DWORD dwTlsIndex
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL TlsFree(
      [In] DWORD dwTlsIndex
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD TlsAlloc();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD GetCurrentThreadId();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern HANDLE GetCurrentThread();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL SwitchToThread();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD SuspendThread(
      [In] HANDLE hThread
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD SleepEx(
      [In] DWORD dwMilliseconds,
      [In] BOOL bAlertable
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern void Sleep(
      [In] DWORD dwMilliseconds
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL SetThreadPriorityBoost(
      [In] HANDLE hThread,
      [In] BOOL bDisablePriorityBoost
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL SetThreadPriority(
      [In] HANDLE hThread,
      [In] int nPriority
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL SetThreadInformation(
      [In] HANDLE hThread,
      [In] ThreadInformationClass ThreadInformationClass,
           void* ThreadInformation,
      [In] DWORD ThreadInformationSize
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL SetThreadIdealProcessorEx(
      [In] HANDLE hThread,
      [In] in ProcessorNumber lpIdealProcessor,
      [Out, Optional] out ProcessorNumber lpPreviousIdealProcessor
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD SetThreadIdealProcessor(
      [In] HANDLE hThread,
      [In] DWORD dwIdealProcessor
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL SetThreadGroupAffinity(
      [In] HANDLE hThread,
      [In] in GroupAffinity GroupAffinity,
      [Out, Optional] out GroupAffinity PreviousGroupAffinity
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetProcessAffinityMask(
      [In] HANDLE hProcess,
      [Out] out DWORD_PTR lpProcessAffinityMask,
      [Out] out DWORD_PTR lpSystemAffinityMask
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD_PTR SetThreadAffinityMask(
      [In] HANDLE hThread,
      [In] DWORD_PTR dwThreadAffinityMask
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD ResumeThread(
      [In] HANDLE hThread
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetThreadTimes(
      [In] HANDLE hThread,
      [Out] out FileTime lpCreationTime,
      [Out] out FileTime lpExitTime,
      [Out] out FileTime lpKernelTime,
      [Out] out FileTime lpUserTime
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int GetThreadPriority(
      [In] HANDLE hThread
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetThreadPriorityBoost(
      [In] HANDLE hThread,
      [Out] out BOOL pDisablePriorityBoost
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern void ExitThread(
      [In] DWORD dwExitCode
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HANDLE CreateFileMappingA(
      [In] HANDLE hFile,
      [In, Optional] SecurityAttributes* lpFileMappingAttributes,
      [In] DWORD flProtect,
      [In] DWORD dwMaximumSizeHigh,
      [In] DWORD dwMaximumSizeLow,
      [In, Optional] CHAR* lpName
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL UpdateResourceW(
      [In] HANDLE hUpdate,
      [In] WCHAR* lpType,
      [In] WCHAR* lpName,
      [In] WORD wLanguage,
      [In, Optional] void* lpData,
      [In] DWORD cb
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HANDLE CreateFileMappingW(
      [In] HANDLE hFile,
      [In, Optional] SecurityAttributes* lpFileMappingAttributes,
      [In] DWORD flProtect,
      [In] DWORD dwMaximumSizeHigh,
      [In] DWORD dwMaximumSizeLow,
      [In, Optional] WCHAR* lpName
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe DWORD GetFileType(
      [In] HANDLE hFile
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe void* VirtualAllocExNuma(
      [In] HANDLE hProcess,
      [In, Optional] void* lpAddress,
      [In] SIZE_T dwSize,
      [In] DWORD flAllocationType,
      [In] DWORD flProtect,
      [In] DWORD nndPreferred
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL ReadFile(
      [In] HANDLE hFile,
      [Out] void* lpBuffer,
      [In] DWORD nNumberOfBytesToRead,
      [Out, Optional] out DWORD lpNumberOfBytesRead,
      [In, Out, Optional] Overlapped* lpOverlapped
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL FreeConsole();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL AttachConsole(
      [In] DWORD dwProcessId
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL AllocConsole();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL ReadConsole(
      [In] HANDLE hConsoleInput,
      [Out] void* lpBuffer,
      [In] DWORD nNumberOfCharsToRead,
      [Out] out DWORD lpNumberOfCharsRead,
      [In, Optional] void* pInputControl
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL SetStdHandle(
      [In] DWORD nStdHandle,
      [In] HANDLE hHandle
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL WriteConsole(
      [In] HANDLE hConsoleOutput,
      [In] void* lpBuffer,
      [In] DWORD nNumberOfCharsToWrite,
      [Out, Optional] out DWORD lpNumberOfCharsWritten,
      void* lpReserved = null
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HANDLE CreateFileTransactedW(
      [In] WCHAR* lpFileName,
      [In] DWORD dwDesiredAccess,
      [In] DWORD dwShareMode,
      [In, Optional] SecurityAttributes* lpSecurityAttributes,
      [In] DWORD dwCreationDisposition,
      [In] DWORD dwFlagsAndAttributes,
      [In, Optional] HANDLE hTemplateFile,
      [In] HANDLE hTransaction,
      [In, Optional] in ushort pusMiniVersion,
                     void* lpExtendedParameter
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL WriteFileEx(
      [In] HANDLE hFile,
      [In, Optional] void* lpBuffer,
      [In] DWORD nNumberOfBytesToWrite,
      [In, Out] ref Overlapped lpOverlapped,
      [In] delegate*<DWORD, DWORD, ref Overlapped, void> lpCompletionRoutine
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetExitCodeThread(
      [In] HANDLE hThread,
      [Out] out DWORD lpExitCode
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetExitCodeProcess(
      [In] HANDLE hProcess,
      [Out] out DWORD lpExitCode
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HRESULT SetThreadDescription(
      [In] HANDLE hThread,
      [In] WCHAR* lpThreadDescription
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern HANDLE OpenThread(
      [In] DWORD dwDesiredAccess,
      [In] BOOL bInheritHandle,
      [In] DWORD dwThreadId
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HANDLE CreateThread(
      [In, Optional] SecurityAttributes* lpThreadAttributes,
      [In] SIZE_T dwStackSize,
      [In] delegate*<void*, DWORD> lpStartAddress,
      [In, Optional] void* lpParameter,
      [In] DWORD dwCreationFlags,
      [Out, Optional] out DWORD lpThreadId
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Toolhelp32ReadProcessMemory(
      [In] DWORD th32ProcessID,
      [In] void* lpBaseAddress,
      [Out] void* lpBuffer,
      [In] SIZE_T cbRead,
      [Out] out SIZE_T lpNumberOfBytesRead
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Thread32Next(
      [In] HANDLE hSnapshot,
      [Out] out ThreadEntry lpte
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Thread32First(
      [In] HANDLE hSnapshot,
      [In, Out] ref ThreadEntry lpte
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Heap32Next(
      [Out] out HeapEntry lphe
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Heap32ListNext(
      [In] HANDLE hSnapshot,
      [Out] out HeapList lphl
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Heap32ListFirst(
      [In] HANDLE hSnapshot,
      [In, Out] ref HeapList lphl
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Heap32First(
      [In, Out] ref HeapEntry lphe,
      [In] DWORD th32ProcessID,
      [In] ULONG_PTR th32HeapID
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Module32NextW(
      [In] HANDLE hSnapshot,
      [Out] out ModuleEntry lpme
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Module32FirstW(
      [In] HANDLE hSnapshot,
      [In, Out] ref ModuleEntry lpme
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetThreadIdealProcessorEx(
      [In] HANDLE hThread,
      [Out] out ProcessorNumber lpIdealProcessor
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetThreadGroupAffinity(
      [In] HANDLE hThread,
      [Out] out GroupAffinity GroupAffinity
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD GetThreadId(
      [In] HANDLE Thread
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD GetProcessIdOfThread(
      [In] HANDLE Thread
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HRESULT GetThreadDescription(
      [In] HANDLE hThread,
      [Out] out WCHAR* ppszThreadDescription
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Process32NextW(
      [In] HANDLE hSnapshot,
      [Out] out ProcessEntry lppe
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL Process32FirstW(
      [In] HANDLE hSnapshot,
      [In, Out] ref ProcessEntry lppe
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern HANDLE CreateToolhelp32Snapshot(
      [In] TH32CS dwFlags,
      [In] DWORD th32ProcessID
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe delegate*<INT_PTR> GetProcAddress(
      [In] HMODULE hModule,
      [In] CHAR* lpProcName
    );

    [DllImport("Psapi.dll", SetLastError = true)]
    public static extern unsafe DWORD GetDeviceDriverFileNameW(
      [In] void* ImageBase,
      [Out] WCHAR* lpFilename,
      [In] DWORD nSize
    );

    [DllImport("Psapi.dll", SetLastError = true)]
    public static extern unsafe DWORD GetDeviceDriverBaseNameW(
      [In] void* ImageBase,
           WCHAR* lpFilename,
      [In] DWORD nSize
    );

    [DllImport("Psapi.dll", SetLastError = true)]
    public static extern unsafe BOOL EnumDeviceDrivers(
      [Out] void** lpImageBase,
      [In] DWORD cb,
      [Out] out DWORD lpcbNeeded
    );

    [DllImport("Psapi.dll", SetLastError = true)]
    public static extern unsafe BOOL GetModuleInformation(
      [In] HANDLE hProcess,
      [In] HMODULE hModule,
      [Out] out ModuleInfo lpmodinfo,
      [In] DWORD cb
    );

    [DllImport("Psapi.dll", SetLastError = true)]
    public static extern unsafe DWORD GetModuleBaseNameW(
      [In] HANDLE hProcess,
      [In, Optional] HMODULE hModule,
      [Out] WCHAR* lpBaseName,
      [In] DWORD nSize
    );

    [DllImport("Psapi.dll", SetLastError = true)]
    public static extern unsafe DWORD GetModuleFileNameExW(
      [In] HANDLE hProcess,
      [In, Optional] HMODULE hModule,
      [Out] WCHAR* lpFilename,
      [In] DWORD nSize
    );

    [DllImport("kernel32.dll", SetLastError = true)]
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

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL QueryFullProcessImageNameW(
      [In] HANDLE hProcess,
      [In] DWORD dwFlags,
      [Out] WCHAR* lpExeName,
      [In, Out] ref DWORD lpdwSize
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD GetProcessId(
      [In] HANDLE Process
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetProcessHandleCount(
      [In] HANDLE hProcess,
      [In, Out] ref DWORD pdwHandleCount
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD GetProcessVersion(
      [In] DWORD ProcessId
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL IsImmersiveProcess(
      [In] HANDLE hProcess
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL TerminateThread(
      [In, Out] HANDLE hThread,
      [In] DWORD dwExitCode
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL TerminateProcess(
      [In] HANDLE hProcess,
      [In] UINT uExitCode
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD GetCurrentProcessId();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern HANDLE GetCurrentProcess();

    [DllImport("Psapi.dll", SetLastError = true)]
    public static extern unsafe BOOL EnumProcessModules(
      [In] HANDLE hProcess,
      [Out] HMODULE* lphModule,
      [In] DWORD cb,
      [Out] out DWORD lpcbNeeded
    );

    [DllImport("Psapi.dll", SetLastError = true)]
    public static extern unsafe BOOL EnumProcesses(
      [Out] DWORD* lpidProcess,
      [In] DWORD cb,
      [Out] out DWORD lpcbNeeded
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL VirtualFree(
      [In] void* lpAddress,
      [In] SIZE_T dwSize,
      [In] DWORD dwFreeType
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe void* VirtualAlloc(
      [In, Optional] void* lpAddress,
      [In] SIZE_T dwSize,
      [In] DWORD flAllocationType,
      [In] DWORD flProtect
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL VirtualProtect(
      [In] void* lpAddress,
      [In] SIZE_T dwSize,
      [In] DWORD flNewProtect,
      [Out] out DWORD lpflOldProtect
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL VirtualProtectEx(
      [In] HANDLE hProcess,
      [In] void* lpAddress,
      [In] SIZE_T dwSize,
      [In] DWORD flNewProtect,
      [Out] out DWORD lpflOldProtect
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL VirtualFreeEx(
      [In] HANDLE hProcess,
      [In] void* lpAddress,
      [In] SIZE_T dwSize,
      [In] DWORD dwFreeType
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD WaitForSingleObject(
      [In] HANDLE hHandle,
      [In] DWORD dwMilliseconds
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HANDLE CreateRemoteThread(
      [In] HANDLE hProcess,
      [In] SecurityAttributes* lpThreadAttributes,
      [In] SIZE_T dwStackSize,
      [In] delegate*<void*, DWORD>* lpStartAddress,
      [In] void* lpParameter,
      [In] DWORD dwCreationFlags,
      [Out] out DWORD lpThreadId
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HANDLE CreateRemoteThreadEx(
      [In] HANDLE hProcess,
      [In, Optional] SecurityAttributes* lpThreadAttributes,
      [In] SIZE_T dwStackSize,
      [In] delegate*<void*, DWORD> lpStartAddress,
      [In, Optional] void* lpParameter,
      [In] DWORD dwCreationFlags,
      [In, Optional] void* lpAttributeList,
      [Out, Optional] out DWORD lpThreadId
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL WriteProcessMemory(
      [In] HANDLE hProcess,
      [In] void* lpBaseAddress,
      [In] void* lpBuffer,
      [In] SIZE_T nSize,
      [Out] out SIZE_T lpNumberOfBytesWritten
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe void* VirtualAllocEx(
      [In] HANDLE hProcess,
      [In, Optional] void* lpAddress,
      [In] SIZE_T dwSize,
      [In] DWORD flAllocationType,
      [In] DWORD flProtect
    );

    [DllImport("kernel32.dll", SetLastError = true)]
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
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL SetConsoleWindowInfo(
      [In] HANDLE hConsoleOutput,
      [In] BOOL bAbsolute,
      [In] in SMALL_RECT lpConsoleWindow
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
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetConsoleScreenBufferInfo(
       [In] HANDLE hConsoleOutput,
       [Out] ConsoleScreenBufferInfo* lpConsoleScreenBufferInfo
     );

    [DllImport("kernel32.dll", SetLastError = true)]
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
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL WriteFile(
      [In] HANDLE hFile,
      [In] void* lpBuffer,
      [In] DWORD nNumberOfBytesToWrite,
      [Out, Optional] out DWORD lpNumberOfBytesWritten,
      [In, Out, Optional] ref Overlapped lpOverlapped
    );

    /// <summary>
    /// Closes an open object handle.
    /// </summary>
    /// <param name="hObject">
    /// A valid handle to an open object.
    /// </param>
    [DllImport("kernel32.dll", SetLastError = true)]
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
    [DllImport("kernel32.dll", SetLastError = true)]
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
    [DllImport("kernel32.dll", SetLastError = true)]
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
    [DllImport("kernel32.dll", SetLastError = true)]
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
    [DllImport("kernel32.dll", SetLastError = true)]
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
    [DllImport("kernel32.dll", SetLastError = true)]
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
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern void SetLastError(
      [In] DWORD dwErrCode
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL SetConsoleScreenBufferSize(
      [In] HANDLE hConsoleOutput,
      [In] COORD dwSize
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetConsoleSelectionInfo(
      [Out] out ConsoleSelectionInfo lpConsoleSelectionInfo
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern HWND GetConsoleWindow();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetNumberOfConsoleMouseButtons(
      [Out] out DWORD lpNumberOfMouseButtons
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern COORD GetLargestConsoleWindowSize(
      [In] HANDLE hConsoleOutput
    );

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetCurrentConsoleFont(
      [In] HANDLE hConsoleOutput,
      [In] BOOL bMaximumWindow,
      [Out] out ConsoleFontInfo lpConsoleCurrentFont
    );

    /// <summary>
    /// Retrieves extended information about the current console font.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetCurrentConsoleFontEx(
      [In] HANDLE hConsoleOutput,
      [In] BOOL bMaximumWindow,
      [Out] ConsoleFontInfoEx* lpConsoleCurrentFontEx
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
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL SetCurrentConsoleFontEx(
      [In] HANDLE hConsoleOutput,
      [In] BOOL bMaximumWindow,
      [In] in ConsoleFontInfoEx lpConsoleCurrentFontEx
    );

    /// <summary>
    /// Retrieves the current system date and time in Coordinated Universal Time (UTC) format.
    /// </summary>
    /// <param name="lpSystemTime">
    /// A pointer to a <see cref="SystemTime"/> structure to receive the current
    /// system date and time. The <paramref name="lpSystemTime"/> parameter must not
    /// be <c>NULL</c>. Using <c>NULL</c> will result in an access violation.
    /// </param>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe void GetSystemTime(
      [Out] out SystemTime lpSystemTime
    );

    /// <summary>
    /// Retrieves the current system date and time.
    /// The information is in Coordinated Universal Time (UTC) format.
    /// </summary>
    /// <param name="lpSystemTimeAsFileTime">
    /// A pointer to a <see cref="FileTime"/> structure to receive the current
    /// system date and time in UTC format.
    /// </param>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe void GetSystemTimeAsFileTime(
      [Out] out FileTime lpSystemTimeAsFileTime
    );

    /// <summary>
    /// Frees the specified local memory object and invalidates its handle.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern HLOCAL LocalFree(
      [In] HLOCAL hMem
    );

    /// <summary>
    /// Reads data from a console input buffer and removes it from the buffer.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL ReadConsoleInputW(
      [In] HANDLE hConsoleInput,
      [Out] InputEvent* lpBuffer,
      [In] DWORD nLength,
      [Out] out DWORD lpNumberOfEventsRead
    );

    /// <summary>
    /// Writes data directly to the console input buffer.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL WriteConsoleInputW(
      [In] HANDLE hConsoleInput,
      [In] InputEvent* lpBuffer,
      [In] DWORD nLength,
      [Out] out DWORD lpNumberOfEventsWritten
    );

    /// <summary>
    /// Retrieves the input code page used by the console
    /// associated with the calling process. A console uses its
    /// input code page to translate keyboard input into the
    /// corresponding character value.
    /// </summary>
    [DllImport("Kernel32.dll", SetLastError = true)]
    public static extern UINT GetConsoleCP();

    /// <summary>
    /// Retrieves the output code page used by the console associated with the calling
    /// process. A console uses its output code page to translate the
    /// character values written by the various output functions into
    /// the images displayed in the console window.
    /// </summary>
    [DllImport("Kernel32.dll", SetLastError = true)]
    public static extern UINT GetConsoleOutputCP();

    /// <summary>
    /// Sets the input code page used by the console associated with the
    /// calling process. A console uses its input code page to
    /// translate keyboard input into the corresponding character value.
    /// </summary>
    [DllImport("Kernel32.dll", SetLastError = true)]
    public static extern BOOL SetConsoleCP(
      [In] UINT wCodePageID
    );

    /// <summary>
    /// Sets the output code page used by the console associated
    /// with the calling process. A console uses its output code
    /// page to translate the character values written by the
    /// various output functions into the images displayed in
    /// the console window.
    /// </summary>
    [DllImport("Kernel32.dll", SetLastError = true)]
    public static extern BOOL SetConsoleOutputCP(
      [In] UINT wCodePageID
    );

    /// <summary>
    /// Retrieves a handle to the specified
    /// standard device (standard input, standard output, or standard error).
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern HANDLE GetStdHandle(
      [In] DWORD nStdHandle
    );

    /// <summary>
    /// Creates or opens a file or I/O device.
    /// The most commonly used I/O devices are as follows:
    /// file, file stream, directory, physical disk, volume, console buffer, tape drive,
    /// communications resource, mailslot, and pipe. The function returns a
    /// handle that can be used to access the file or device for various
    /// types of I/O depending on the file or device and the
    /// flags and attributes specified.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe HANDLE CreateFileW(
      [In] WCHAR* lpFileName,
      [In] DWORD dwDesiredAccess,
      [In] DWORD dwShareMode,
      [In, Optional] SecurityAttributes* lpSecurityAttributes,
      [In] CreateFileFlags dwCreationDisposition,
      [In] DWORD dwFlagsAndAttributes,
      [In, Optional] HANDLE hTemplateFile
    );

    /// <summary>
    /// Writes character and color attribute data to a specified
    /// rectangular block of character cells in a console screen buffer.
    /// The data to be written is taken from a correspondingly
    /// sized rectangular block at a specified location in the source buffer.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL WriteConsoleOutputW(
      [In] HANDLE hConsoleOutput,
      [In] ConsoleChar* lpBuffer,
      [In] SmallSize dwBufferSize,
      [In] Coord dwBufferCoord,
      [In, Out] ref SmallRect lpWriteRegion
    );

    /// <summary>
    /// Retrieves the current input mode of a console's
    /// input buffer or the current output mode of a console screen buffer.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe BOOL GetConsoleMode(
      [In] HANDLE hConsoleInput,
      [Out] out DWORD lpMode
    );

    /// <summary>
    /// Sets the input mode of a console's input buffer or the output mode of a console screen buffer.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern BOOL SetConsoleMode(
      [In] HANDLE hConsoleInput,
      [In] DWORD dwMode
    );

    /// <summary>
    /// Retrieves the calling thread's last-error code value.
    /// The last-error code is maintained on a per-thread basis.
    /// Multiple threads do not overwrite each other's last-error code.
    /// </summary>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern DWORD GetLastError();

    /// <summary>
    /// Formats a message string.
    /// The function requires a message definition as input.
    /// The message definition can come from a buffer passed into the function.
    /// It can come from a message table resource in an already-loaded module.
    /// Or the caller can ask the function to search the system's message table
    /// resource(s) for the message definition. The function finds the message
    /// definition in a message table resource based on a message identifier
    /// and a language identifier. The function copies the formatted message
    /// text to an output buffer, processing any embedded insert sequences
    /// if requested.
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern unsafe DWORD FormatMessageW(
      [In] FormatMessageFlags dwFlags,
      [In, Optional] IntPtr lpSource,
      [In] DWORD dwMessageId,
      [In] DWORD dwLanguageId,
      [Out] WCHAR* lpBuffer,
      [In] DWORD nSize,
      [In, Optional] IntPtr Arguments
    );
}
