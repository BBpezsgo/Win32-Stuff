namespace Win32.COM;

[StructLayout(LayoutKind.Sequential)]
public struct StgStatistic
{
    public unsafe WCHAR* Name;
    public DWORD Type;
    public UINT64 Size;
    public FileTime LastModificationTime;
    public FileTime CreationTime;
    public FileTime LasAccessTime;
    public DWORD AccessMode;
    public DWORD LocksSupported;
    public Guid ClassIdentifier;
    public DWORD StateBits;
    readonly DWORD Reserved;
}
