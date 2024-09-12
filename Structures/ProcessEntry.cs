namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct ProcessEntry
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD StructSize;
    /// <summary>
    /// This member is no longer used and is always set to zero.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD Usage;
    /// <summary>
    /// The process identifier.
    /// </summary>
    public readonly DWORD ProcessID;          // this process
    /// <summary>
    /// This member is no longer used and is always set to zero.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly ULONG_PTR DefaultHeapID;
    /// <summary>
    /// This member is no longer used and is always set to zero.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD ModuleID;           // associated exe
    /// <summary>
    /// The number of execution threads started by the process.
    /// </summary>
    public readonly DWORD ThreadCount;
    /// <summary>
    /// The identifier of the process that created this process (its parent process).
    /// </summary>
    public readonly DWORD ParentProcessID;    // this process's parent process
    /// <summary>
    /// The base priority of any threads created by this process.
    /// </summary>
    public readonly LONG PriClassBase;          // Base priority of process's threads
    /// <summary>
    /// This member is no longer used and is always set to zero.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD Flags;
    /// <summary>
    /// The name of the executable file for the process.
    /// To retrieve the full path to the executable file, call
    /// the <see cref="Kernel32.Module32FirstW"/> function and check
    /// the <c>szExePath</c> member
    /// of the <see cref="ModuleEntry"/> structure that is returned. However,
    /// if the calling process is a 32-bit process, you must call
    /// the <see cref="QueryFullProcessImageName"/> function to retrieve the full
    /// path of the executable file for a 64-bit process.
    /// </summary>
    public unsafe fixed WCHAR ExeFilePtr[260];

    public readonly unsafe string ExeFile
    {
        get
        {
            fixed (WCHAR* exeFilePtr = ExeFilePtr)
            { return new string(exeFilePtr); }
        }
    }

    ProcessEntry(DWORD structSize) : this() => StructSize = structSize;

    public static unsafe ProcessEntry Create() => new((DWORD)sizeof(ProcessEntry));

    public override readonly string ToString() => ProcessID.ToString(System.Globalization.CultureInfo.InvariantCulture);

    [SupportedOSPlatform("windows")]
    public readonly ModuleSnapshot SnapModules() => ModuleSnapshot.CreateSnapshot(ProcessID);
    [SupportedOSPlatform("windows")]
    public readonly HeapSnapshot SnapHeap() => HeapSnapshot.CreateSnapshot(ProcessID);

    [SupportedOSPlatform("windows")]
    public readonly Process Open(ProcessAccessRights accessRights) => Process.Open(accessRights, ProcessID);
}
