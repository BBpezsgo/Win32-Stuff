using System.Globalization;

namespace Win32;

public unsafe delegate DWORD ThreadProc();
public unsafe delegate DWORD ThreadProc<T>(T* parameter) where T : unmanaged;

[SupportedOSPlatform("windows")]
[DebuggerDisplay($"{{{nameof(DebuggerDisplay)}(),nq}}")]
public readonly struct Process :
    IDisposable,
    IEquatable<Process>,
    System.Numerics.IEqualityOperators<Process, Process, bool>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly HANDLE Handle;

    Process(HANDLE handle) => Handle = handle;

    #region Static stuff

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public static unsafe Process CurrentProcess => new(Kernel32.GetCurrentProcess());

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public static unsafe DWORD CurrentProcessId => Kernel32.GetCurrentProcessId();

    /// <exception cref="WindowsException"/>
    public static unsafe DWORD[] GetProcesses(int maxCount = 128)
    {
        DWORD* processes = stackalloc DWORD[maxCount];
        int got = GetProcesses(processes, maxCount);
        DWORD[] result = new DWORD[got];
        for (int i = 0; i < got; i++)
        { result[i] = processes[i]; }
        return result;
    }

    /// <exception cref="WindowsException"/>
    public static unsafe int GetProcesses(DWORD[] buffer)
    {
        fixed (DWORD* bufferPtr = buffer)
        { return GetProcesses(bufferPtr, buffer.Length); }
    }

    /// <exception cref="WindowsException"/>
    public static unsafe int GetProcesses(Span<DWORD> buffer)
    {
        fixed (DWORD* bufferPtr = buffer)
        { return GetProcesses(bufferPtr, buffer.Length); }
    }

    /// <exception cref="WindowsException"/>
    public static unsafe int GetProcesses(DWORD* buffer, int bufferLength)
    {
        DWORD got = default;
        if (Kernel32.EnumProcesses(buffer, (uint)bufferLength * sizeof(DWORD), &got) == 0)
        { throw WindowsException.Get(); }
        got /= sizeof(DWORD);
        return (int)got;
    }

    public static ProcessSnapshot CreateSnapshot()
    {
        HANDLE hSnapshot = Kernel32.CreateToolhelp32Snapshot(TH32CS.SNAPPROCESS, 0);
        if (Kernel32.InvalidHandle == hSnapshot)
        { throw WindowsException.Get(); }
        return new ProcessSnapshot(hSnapshot);
    }

    /// <exception cref="WindowsException"/>
    public static Process Open(ProcessAccessRights accessRights, uint processId)
    {
        HANDLE handle = Kernel32.OpenProcess((uint)accessRights, FALSE, processId);
        if (handle == HANDLE.Zero)
        { throw WindowsException.Get(); }
        return new Process(handle);
    }

    #endregion

    public readonly ModuleSnapshot SnapModules() => ModuleSnapshot.CreateSnapshot(Id);
    public readonly HeapSnapshot SnapHeap() => HeapSnapshot.CreateSnapshot(Id);

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Kernel32.CloseHandle(Handle) == FALSE)
        { throw WindowsException.Get(); }
    }

    public static explicit operator Process(HANDLE handle) => new(handle);
    public static implicit operator HANDLE(Process handle) => handle.Handle;

    public static bool operator ==(Process a, Process b) => a.Equals(b);
    public static bool operator !=(Process a, Process b) => !a.Equals(b);

    #region Thread stuff

    /// <exception cref="WindowsException"/>
    /// <exception cref="NotSupportedException"/>
    public unsafe Thread CreateThread(ThreadProc startAddress)
        => CreateThread(startAddress, out _);

    /// <exception cref="WindowsException"/>
    /// <exception cref="NotSupportedException"/>
    public unsafe Thread CreateThread(ThreadProc startAddress, out DWORD threadId)
    {
        System.Reflection.MethodInfo method = startAddress.Method;

        if (method is System.Reflection.Emit.DynamicMethod)
        { throw new NotSupportedException("Dynamic method not supported"); }

        delegate*<void*, DWORD> functionPtr = (delegate*<void*, DWORD>)method.MethodHandle.GetFunctionPointer();

        if (startAddress.Target is { } || !method.IsStatic)
        {
            GCHandle gcHandle = GCHandle.Alloc(startAddress.Target, GCHandleType.WeakTrackResurrection);
            void* targetPtr = (void*)GCHandle.ToIntPtr(gcHandle);

            return CreateThread(functionPtr, out threadId, targetPtr);
        }
        else
        {
            return CreateThread(functionPtr, out threadId, null);
        }
    }

    /// <exception cref="WindowsException"/>
    /// <exception cref="NotSupportedException"/>
    public unsafe Thread CreateThread<T>(ThreadProc<T> startAddress, T* parameter)
        where T : unmanaged
        => CreateThread(startAddress, out _, parameter);

    /// <exception cref="WindowsException"/>
    /// <exception cref="NotSupportedException"/>
    public unsafe Thread CreateThread<T>(ThreadProc<T> startAddress, out DWORD threadId, T* parameter)
        where T : unmanaged
    {
        System.Reflection.MethodInfo method = startAddress.Method;

        if (method is System.Reflection.Emit.DynamicMethod)
        { throw new NotSupportedException("Dynamic method not supported"); }

        delegate*<void*, DWORD> functionPtr = (delegate*<void*, DWORD>)method.MethodHandle.GetFunctionPointer();

        if (startAddress.Target is { } || !method.IsStatic)
        { throw new NotSupportedException("Non-static method not supported when passing a parameter"); }

        return CreateThread(functionPtr, out threadId, parameter);
    }

    /// <exception cref="WindowsException"/>
    public unsafe Thread CreateThread(delegate*<DWORD> startAddress)
        => CreateThread((delegate*<void*, DWORD>)startAddress, out _, null);

    /// <exception cref="WindowsException"/>
    public unsafe Thread CreateThread(delegate*<void*, DWORD> startAddress, void* parameter)
        => CreateThread(startAddress, out _, parameter);

    /// <exception cref="WindowsException"/>
    public unsafe Thread CreateThread<T>(delegate*<T*, DWORD> startAddress, T* parameter)
        where T : unmanaged
        => CreateThread((delegate*<void*, DWORD>)startAddress, out _, parameter);

    /// <exception cref="WindowsException"/>
    public unsafe Thread CreateThread(delegate*<DWORD> startAddress, out DWORD threadId)
        => CreateThread((delegate*<void*, DWORD>)startAddress, out threadId, null);

    /// <exception cref="WindowsException"/>
    public unsafe Thread CreateThread<T>(delegate*<T*, DWORD> startAddress, out DWORD threadId, T* parameter)
        where T : unmanaged
        => CreateThread((delegate*<void*, DWORD>)startAddress, out threadId, parameter);

    /// <exception cref="WindowsException"/>
    public unsafe Thread CreateThread(delegate*<void*, DWORD> startAddress, out DWORD threadId, void* parameter)
    {
        HANDLE handle = Kernel32.CreateRemoteThreadEx(
                Handle,
                null,
                0,
                startAddress,
                parameter,
                0,
                null,
                out uint _threadId);
        threadId = _threadId;

        if (handle == HANDLE.Zero)
        { throw WindowsException.Get(); }

        return (Thread)handle;
    }

    #endregion

    #region Memory stuff

    /// <exception cref="WindowsException"/>
    public unsafe uint WriteMemory<T>(IntPtr address, ReadOnlySpan<T> buffer) where T : unmanaged
    {
        fixed (T* bufferPtr = buffer)
        { return this.WriteMemory(address, bufferPtr, (uint)(buffer.Length * sizeof(T))); }
    }

    /// <exception cref="WindowsException"/>
    public unsafe uint WriteMemory<T>(IntPtr address, T[] buffer) where T : unmanaged
    {
        fixed (T* bufferPtr = buffer)
        { return this.WriteMemory(address, bufferPtr, (uint)(buffer.Length * sizeof(T))); }
    }

    /// <exception cref="WindowsException"/>
    public unsafe uint WriteMemory(IntPtr address, void* buffer, uint size)
    {
        if (Kernel32.WriteProcessMemory(
            Handle,
            (void*)address,
            buffer,
            size,
            out nuint bytesWrote) == 0)
        { throw WindowsException.Get(); }
        return (uint)bytesWrote;
    }

    /// <exception cref="WindowsException"/>
    public unsafe IntPtr VirtualAlloc(uint size, DWORD protect, MemoryFlags allocationType = MemoryFlags.Reserve | MemoryFlags.Commit)
    {
        IntPtr buffer = (IntPtr)Kernel32.VirtualAllocEx(
            Handle,
            null,
            size,
            (DWORD)allocationType,
            protect);
        if (buffer == IntPtr.Zero)
        { throw WindowsException.Get(); }
        return buffer;
    }

    /// <exception cref="WindowsException"/>
    public unsafe void VirtualFree(IntPtr address, uint size, DWORD freeType)
    {
        if (Kernel32.VirtualFreeEx(
            Handle,
            (void*)address,
            size,
            freeType) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public unsafe DWORD VirtualProtect(IntPtr address, uint size, DWORD protect)
    {
        if (Kernel32.VirtualProtectEx(
            Handle,
            (void*)address,
            size,
            protect,
            out uint oldProtect) == FALSE)
        { throw WindowsException.Get(); }
        return oldProtect;
    }

    #endregion

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe DWORD ExitCode
    {
        get
        {
            if (Kernel32.GetExitCodeProcess(Handle, out uint exitCode) == FALSE)
            { throw WindowsException.Get(); }
            return exitCode;
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe IReadOnlyCollection<Module> Modules
    {
        get
        {
            HMODULE* modules = stackalloc HMODULE[128];
            DWORD got = default;
            if (Kernel32.EnumProcessModules(Handle, modules, 128 * (uint)sizeof(HMODULE), &got) == 0)
            { throw WindowsException.Get(); }
            got /= (uint)sizeof(HMODULE);
            Module[] result = new Module[got];
            for (int i = 0; i < got; i++)
            { result[i] = new Module(Handle, modules[i]); }
            return result;
        }
    }

    /// <exception cref="WindowsException"/>
    public void Terminate(UINT exitCode)
    {
        if (Kernel32.TerminateProcess(Handle, exitCode) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public DWORD Id
    {
        get
        {
            DWORD id = Kernel32.GetProcessId(Handle);
            if (id == 0)
            { throw WindowsException.Get(); }
            return id;
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe DWORD HandleCount
    {
        get
        {
            DWORD handleCount = default;
            if (Kernel32.GetProcessHandleCount(Handle, ref handleCount) == 0)
            { throw WindowsException.Get(); }
            return handleCount;
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe string FullProcessImageName
    {
        get
        {
            DWORD bufferSize = 128;
            fixed (WCHAR* buffer = new string('\0', (int)bufferSize))
            {
                if (Kernel32.QueryFullProcessImageNameW(Handle, 0, buffer, ref bufferSize) == FALSE)
                { throw WindowsException.Get(); }
                return new string(buffer, 0, (int)bufferSize);
            }
        }
    }

    /// <exception cref="WindowsException"/>
    public static unsafe ProcessInformation Create(
        string applicationName,
        string commandLine,
        SecurityAttributes processAttributes,
        SecurityAttributes threadAttributes,
        DWORD creationFlags,
        void* environment,
        string? currentDirectory,
        STARTUPINFOW startupInfo
        )
    {
        ProcessInformation result;
        fixed (WCHAR* applicationNamePtr = applicationName)
        fixed (WCHAR* commandLinePtr = commandLine)
        fixed (WCHAR* currentDirectoryPtr = currentDirectory)
        {
            if (Kernel32.CreateProcessW(
                applicationNamePtr,
                commandLinePtr,
                &processAttributes,
                &threadAttributes,
                FALSE,
                creationFlags,
                environment,
                currentDirectoryPtr,
                &startupInfo,
                out result
                ) == FALSE)
            { throw WindowsException.Get(); }
        }
        return result;
    }

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
    string DebuggerDisplay() => ToString();
    public override bool Equals(object? obj) => obj is Process handle && Equals(handle);
    public bool Equals(Process other) => Handle == other.Handle;
    public override int GetHashCode() => Handle.GetHashCode();
}
