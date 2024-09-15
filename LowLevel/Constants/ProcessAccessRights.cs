namespace Win32;

[Flags]
public enum ProcessAccessRights : DWORD
{
    /// <summary>
    /// Required to terminate a process using TerminateProcess.
    /// </summary>
    Terminate = 0x0001,
    /// <summary>
    /// Required to create a thread in the process.
    /// </summary>
    CreateThread = 0x0002,
    /// <summary>
    /// Required to perform an operation on the address space of a process (see VirtualProtectEx and WriteProcessMemory).
    /// </summary>
    VMOperation = 0x0008,
    /// <summary>
    /// Required to read memory in a process using ReadProcessMemory.
    /// </summary>
    VMReade = 0x0010,
    /// <summary>
    /// Required to write to memory in a process using WriteProcessMemory.
    /// </summary>
    VMWrite = 0x0020,
    /// <summary>
    /// Required to duplicate a handle using DuplicateHandle.
    /// </summary>
    DuplicateHandle = 0x0040,

    /// <summary>
    /// Required to use this process as the parent process with PROC_THREAD_ATTRIBUTE_PARENT_PROCESS.
    /// </summary>
    CreateProcess = 0x0080,
    /// <summary>
    /// Required to set memory limits using SetProcessWorkingSetSize.
    /// </summary>
    SetQuota = 0x0100,
    /// <summary>
    /// Required to set certain information about a process, such as its priority class (see SetPriorityClass).
    /// </summary>
    SetInformation = 0x0200,
    /// <summary>
    /// Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).
    /// </summary>
    QueryInformation = 0x0400,
    /// <summary>
    /// Required to suspend or resume a process.
    /// </summary>
    SuspendResume = 0x0800,
    /// <summary>
    /// Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName). A handle that has the PROCESS_QUERY_INFORMATION access right is automatically granted PROCESS_QUERY_LIMITED_INFORMATION.Windows Server 2003 and Windows XP: This access right is not supported.
    /// </summary>
    QueryLimitedInformation = 0x1000,
    /// <summary>
    /// Required to wait for the process to terminate using the wait functions.
    /// </summary>
    Synchronize = 0x00100000,
    /// <summary>
    /// All possible access rights for a process object.Windows Server 2003 and Windows XP: The size of the PROCESS_ALL_ACCESS flag increased on Windows Server 2008 and Windows Vista. If an application compiled for Windows Server 2008 and Windows Vista is run on Windows Server 2003 or Windows XP, the PROCESS_ALL_ACCESS flag is too large and the function specifying this flag fails with ERROR_ACCESS_DENIED. To avoid this problem, specify the minimum set of access rights required for the operation. If PROCESS_ALL_ACCESS must be used, set _WIN32_WINNT to the minimum operating system targeted by your application (for example, #define _WIN32_WINNT _WIN32_WINNT_WINXP). For more information, see Using the Windows Headers.
    /// </summary>
    AllAccess = 0x000F0000 | Synchronize | 0xFFFF,
}
