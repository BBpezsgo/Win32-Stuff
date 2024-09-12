namespace Win32.Native;

public class NtException : Exception
{
    public NtException(NTSTATUS ntStatus) : base($"NT Error: {ntStatus}") { }

    /// <exception cref="NtException"/>
    public static void EnsureSuccess(NTSTATUS ntStatus)
    {
        if (ntStatus != NtStatuses.Success)
        { throw new NtException(ntStatus); }
    }
}
