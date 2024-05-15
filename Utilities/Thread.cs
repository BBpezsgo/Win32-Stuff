global using Thread = Win32.Thread_;

using System.Globalization;

namespace Win32;

[SupportedOSPlatform("windows")]
[DebuggerDisplay($"{{{nameof(DebuggerDisplay)}(),nq}}")]
[SuppressMessage("Naming", "CA1707")]
public readonly struct Thread_ :
    IDisposable,
    IEquatable<Thread>,
    System.Numerics.IEqualityOperators<Thread, Thread, bool>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly HANDLE Handle;

    Thread_(HANDLE handle) => Handle = handle;

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Kernel32.CloseHandle(Handle) == FALSE)
        { throw WindowsException.Get(); }
    }

    public static explicit operator Thread(HANDLE handle) => new(handle);
    public static implicit operator HANDLE(Thread handle) => handle.Handle;

    public static bool operator ==(Thread a, Thread b) => a.Equals(b);
    public static bool operator !=(Thread a, Thread b) => !a.Equals(b);

    /// <exception cref="WindowsException"/>
    public void WaitFor(uint timeout = uint.MaxValue)
    {
        DWORD result = Kernel32.WaitForSingleObject(Handle, timeout);
        if (result == 0xFFFFFFFF)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public void Terminate(DWORD exitCode)
    {
        if (Kernel32.TerminateThread(Handle, exitCode) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="HResultException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe string Description
    {
        get
        {
            HResult result = Kernel32.GetThreadDescription(Handle, out char* description);
            result.Throw();
            return new string(description);
        }
        set
        {
            fixed (WCHAR* description = value)
            {
                HResult result = Kernel32.SetThreadDescription(Handle, description);
                result.Throw();
            }
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe DWORD ProcessId
    {
        get
        {
            DWORD processId = Kernel32.GetProcessIdOfThread(Handle);
            if (processId == 0)
            { throw WindowsException.Get(); }
            return processId;
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe DWORD Id
    {
        get
        {
            DWORD processId = Kernel32.GetThreadId(Handle);
            if (processId == 0)
            { throw WindowsException.Get(); }
            return processId;
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe ProcessorNumber IdealProcessor
    {
        set
        {
            if (Kernel32.SetThreadIdealProcessorEx(Handle, in value, out _) == FALSE)
            { throw WindowsException.Get(); }
        }
        get
        {
            if (Kernel32.GetThreadIdealProcessorEx(Handle, out ProcessorNumber result) == FALSE)
            { throw WindowsException.Get(); }
            return result;
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe GroupAffinity GroupAffinity
    {
        set
        {
            if (Kernel32.SetThreadGroupAffinity(Handle, in value, out _) == FALSE)
            { throw WindowsException.Get(); }
        }
        get
        {
            if (Kernel32.GetThreadGroupAffinity(Handle, out GroupAffinity result) == FALSE)
            { throw WindowsException.Get(); }
            return result;
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe DWORD ExitCode
    {
        get
        {
            if (Kernel32.GetExitCodeThread(Handle, out uint exitCode) == FALSE)
            { throw WindowsException.Get(); }
            return exitCode;
        }
    }

    /// <exception cref="WindowsException"/>
    public static Thread Open(ThreadAccessRights accessRights, DWORD threadId)
    {
        HANDLE handle = Kernel32.OpenThread((DWORD)accessRights, FALSE, threadId);
        if (handle == HANDLE.Zero)
        { throw WindowsException.Get(); }
        return new Thread(handle);
    }

    /// <exception cref="WindowsException"/>
    public readonly unsafe void SetAffinityMask(DWORD_PTR value)
    {
        if (Kernel32.SetThreadAffinityMask(Handle, value) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public unsafe void SetMemoryPriority(ULONG memoryPriority)
    {
        if (Kernel32.SetThreadInformation(Handle, ThreadInformationClass.ThreadMemoryPriority, &memoryPriority, sizeof(ULONG)) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public unsafe void SetPowerThrottling(ThreadPowerThrottlingState powerThrottlingState)
    {
        if (Kernel32.SetThreadInformation(Handle, ThreadInformationClass.ThreadPowerThrottling, &powerThrottlingState, (uint)sizeof(ThreadPowerThrottlingState)) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public unsafe void SetInformation(ThreadInformationClass informationClass, void* information, uint informationSize)
    {
        if (Kernel32.SetThreadInformation(Handle, informationClass, information, informationSize) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public int Priority
    {
        set
        {
            if (Kernel32.SetThreadPriority(Handle, value) == FALSE)
            { throw WindowsException.Get(); }
        }
        get
        {
            int result = Kernel32.GetThreadPriority(Handle);
            if (result == 0x7fffffff)
            { throw WindowsException.Get(); }
            return result;
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe bool PriorityBoost
    {
        set
        {
            if (Kernel32.SetThreadPriorityBoost(Handle, value ? FALSE : TRUE) == FALSE)
            { throw WindowsException.Get(); }
        }
        get
        {
            if (Kernel32.GetThreadPriorityBoost(Handle, out int result) == FALSE)
            { throw WindowsException.Get(); }
            return result == FALSE;
        }
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public static Thread CurrentThread => new(Kernel32.GetCurrentThread());

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public static DWORD CurrentThreadId => Kernel32.GetCurrentThreadId();

    public static void Sleep(uint milliseconds) => Kernel32.Sleep(milliseconds);
    public static bool Sleep(uint milliseconds, bool alertable) => Kernel32.SleepEx(milliseconds, alertable ? TRUE : FALSE) != 0;

    /// <exception cref="WindowsException"/>
    public void Suspend()
    {
        if (Kernel32.SuspendThread(Handle) == unchecked((DWORD)(-1)))
        { throw WindowsException.Get(); }
    }

    public static bool SwitchToThread() => Kernel32.SwitchToThread() != 0;

    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
    string DebuggerDisplay() => ToString();
    public override bool Equals(object? obj) => obj is Thread handle && Equals(handle);
    public bool Equals(Thread other) => Handle == other.Handle;
    public override int GetHashCode() => Handle.GetHashCode();
}
