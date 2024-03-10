namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public readonly struct ThreadEntry :
    IEquatable<ThreadEntry>,
    System.Numerics.IEqualityOperators<ThreadEntry, ThreadEntry, bool>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD StructSize;
    /// <summary>
    /// This member is no longer used and is always set to zero.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD Usage;
    /// <summary>
    /// The thread identifier, compatible with the thread
    /// identifier returned by the <see cref="Kernel32.CreateProcessW"/> function.
    /// </summary>
    public readonly DWORD ThreadId;       // this thread
    /// <summary>
    /// The identifier of the process that created the thread.
    /// </summary>
    public readonly DWORD OwnerProcessId; // Process this thread is associated with
    /// <summary>
    /// The kernel base priority level assigned to the thread.
    /// The priority is a number from 0 to 31, with 0 representing
    /// the lowest possible thread priority.
    /// For more information, see KeQueryPriorityThread.
    /// </summary>
    public readonly LONG BasePriority;
    /// <summary>
    /// This member is no longer used and is always set to zero.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly LONG DeltaPriority;
    /// <summary>
    /// This member is no longer used and is always set to zero.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly DWORD Flags;

    public ThreadEntry(DWORD structSize) : this() => this.StructSize = structSize;

    public static unsafe ThreadEntry Create() => new((DWORD)sizeof(ThreadEntry));

    public override bool Equals(object? obj) => obj is ThreadEntry entry && Equals(entry);
    public bool Equals(ThreadEntry other) =>
        ThreadId == other.ThreadId &&
        OwnerProcessId == other.OwnerProcessId;
    public override int GetHashCode() => HashCode.Combine(ThreadId, OwnerProcessId);

    public static bool operator ==(ThreadEntry left, ThreadEntry right) => left.Equals(right);
    public static bool operator !=(ThreadEntry left, ThreadEntry right) => !left.Equals(right);

    [SupportedOSPlatform("windows")]
    public Thread Open(ThreadAccessRights accessRights) => Thread.Open(accessRights, ThreadId);
}
