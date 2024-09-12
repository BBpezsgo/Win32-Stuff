namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct ProcessInformation
{
    public HANDLE ProcessHandle;
    public HANDLE ThreadHandle;
    public DWORD ProcessId;
    public DWORD ThreadId;
}
