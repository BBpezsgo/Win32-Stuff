using System.Diagnostics;
using System.Globalization;

namespace Win32
{
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

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public static Process CurrentProcess => new(Kernel32.GetCurrentProcess());

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public static DWORD CurrentProcessId => Kernel32.GetCurrentProcessId();

        /// <exception cref="WindowsException"/>
        unsafe public static DWORD[] GetProcesses(int maxCount = 128)
        {
            DWORD* processes = stackalloc DWORD[maxCount];
            int got = GetProcesses(processes, maxCount);
            DWORD[] result = new DWORD[got];
            for (int i = 0; i < got; i++)
            { result[i] = processes[i]; }
            return result;
        }

        /// <exception cref="WindowsException"/>
        unsafe public static int GetProcesses(DWORD[] buffer)
        {
            fixed (DWORD* bufferPtr = buffer)
            { return GetProcesses(bufferPtr, buffer.Length); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public static int GetProcesses(Span<DWORD> buffer)
        {
            fixed (DWORD* bufferPtr = buffer)
            { return GetProcesses(bufferPtr, buffer.Length); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public static int GetProcesses(DWORD* buffer, int bufferLength)
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
            if (Kernel32.INVALID_HANDLE_VALUE == hSnapshot)
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

        public readonly ModuleSnapshot SnapModules() => ModuleSnapshot.CreateSnapshot(Id);
        public readonly HeapSnapshot SnapHeap() => HeapSnapshot.CreateSnapshot(Id);

        public void Dispose() => _ = Kernel32.CloseHandle(Handle);

        public static explicit operator Process(HANDLE handle) => new(handle);
        public static implicit operator HANDLE(Process handle) => handle.Handle;

        public static bool operator ==(Process a, Process b) => a.Equals(b);
        public static bool operator !=(Process a, Process b) => !a.Equals(b);

        /// <exception cref="WindowsException"/>
        unsafe public Thread CreateThread(delegate*<void*, uint> startAddress, out uint threadId)
            => CreateThread(startAddress, null, out threadId);

        /// <exception cref="WindowsException"/>
        unsafe public Thread CreateThread(delegate*<void*, uint> startAddress, void* parameter = null)
            => CreateThread(startAddress, parameter, out _);

        /// <exception cref="WindowsException"/>
        unsafe public Thread CreateThread(delegate*<void*, uint> startAddress, void* parameter, out uint threadId)
        {
            uint _threadId = default;
            HANDLE handle = Kernel32.CreateRemoteThreadEx(
                    Handle,
                    null,
                    0,
                    startAddress,
                    parameter,
                    0,
                    null,
                    &_threadId);
            threadId = _threadId;

            if (handle == HANDLE.Zero)
            { throw WindowsException.Get(); }

            return (Thread)handle;
        }

        /// <exception cref="WindowsException"/>
        unsafe public uint WriteMemory<T>(IntPtr address, ReadOnlySpan<T> buffer) where T : unmanaged
        {
            fixed (T* bufferPtr = buffer)
            { return this.WriteMemory(address, bufferPtr, (uint)(buffer.Length * sizeof(T))); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public uint WriteMemory<T>(IntPtr address, T[] buffer) where T : unmanaged
        {
            fixed (T* bufferPtr = buffer)
            { return this.WriteMemory(address, bufferPtr, (uint)(buffer.Length * sizeof(T))); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public uint WriteMemory(IntPtr address, void* buffer, uint size)
        {
            nuint bytesWrote = default;
            if (Kernel32.WriteProcessMemory(
                Handle,
                (void*)address,
                buffer,
                size,
                &bytesWrote) == 0)
            { throw WindowsException.Get(); }
            return (uint)bytesWrote;
        }

        /// <exception cref="WindowsException"/>
        unsafe public IntPtr VirtualAlloc(uint size, DWORD protect, DWORD allocationType = MEM.MEM_RESERVE | MEM.MEM_COMMIT)
        {
            IntPtr buffer = (IntPtr)Kernel32.VirtualAllocEx(
                Handle,
                null,
                size,
                allocationType,
                protect);
            if (buffer == IntPtr.Zero)
            { throw WindowsException.Get(); }
            return buffer;
        }

        /// <exception cref="WindowsException"/>
        unsafe public void VirtualFree(IntPtr address, uint size, DWORD freeType)
        {
            if (Kernel32.VirtualFreeEx(
                Handle,
                (void*)address,
                size,
                freeType) == FALSE)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public DWORD VirtualProtect(IntPtr address, uint size, DWORD protect)
        {
            DWORD oldProtect = default;
            if (Kernel32.VirtualProtectEx(
                Handle,
                (void*)address,
                size,
                protect,
                &oldProtect) == FALSE)
            { throw WindowsException.Get(); }
            return oldProtect;
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public Module[] Modules
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
        unsafe public DWORD HandleCount
        {
            get
            {
                DWORD handleCount = default;
                if (Kernel32.GetProcessHandleCount(Handle, &handleCount) == 0)
                { throw WindowsException.Get(); }
                return handleCount;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public string FullProcessImageName
        {
            get
            {
                DWORD bufferSize = 128;
                fixed (WCHAR* buffer = new string('\0', (int)bufferSize))
                {
                    if (Kernel32.QueryFullProcessImageNameW(Handle, 0, buffer, &bufferSize) == FALSE)
                    { throw WindowsException.Get(); }
                    return new string(buffer, 0, (int)bufferSize);
                }
            }
        }

        /// <exception cref="WindowsException"/>
        unsafe public static ProcessInformation Create(
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
            ProcessInformation result = default;
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
                    &result
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
}
